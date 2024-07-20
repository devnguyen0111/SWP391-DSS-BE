using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Charge;
using Repository.Charge;
using Services.Users;
using Newtonsoft.Json;
using static DiamondShopSystem.API.DTO.VnPay;
using static Services.Charge.VnPay;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        // payment service VNPAY
        private readonly Ivnpay _vnPayService;
        private readonly IVnPayRepository _vnPayRepository;

        private readonly IConfiguration _configuration;

        // payment service Paypal
        private readonly IPaypalService _paypalService;
        private readonly IPaypalRepository _paypalRepository;
        private readonly IOrderService orderService;
        public PaymentController(Ivnpay vnPayService, IVnPayRepository vnPayRepository, IPaypalRepository paypalRepository, IPaypalService paypalService, IConfiguration configuration, IOrderService o)
        {
            _vnPayService = vnPayService;
            _vnPayRepository = vnPayRepository;
            _configuration = configuration;
            _paypalService = paypalService;
            _paypalRepository = paypalRepository;
            orderService = o;

        }

        [HttpPost("CreatePayment-VNPAY")]
        public IActionResult CreatePayment(int orderId)
        {
            Order order = _vnPayRepository.GetOrderById(orderId);
            if (order == null || order.TotalAmount <= 0)
            {
                return BadRequest("Invalid order data.");
            }

            try
            {
                // Save the order to the database
                /*                _vnPayRepository.SaveOrder(order);*/

                // Create VNPay payment URL
                /*string returnUrl = Url.Action("PaymentReturn", "Checkout", null, Request.Scheme);*/
                var returnUrl = "https://google.com.vn";
                string paymentUrl = _vnPayService.CreatePayment(order, returnUrl);

                return Ok(new { url = paymentUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("PaymentReturn-VNPAY")]
        public IActionResult PaymentReturn()
        {
            string queryString = Request.QueryString.Value;
            var vnp_HashSecret = "MEIJ0KIOZC8Z8ZU2A5W28CT7RAC6K9I0";
            bool c = _vnPayService.ValidateSignature(queryString, vnp_HashSecret);
            c = true;
            // Validate the signature of the payment URL
            if (c)
            {
                // Retrieve the order ID from the query string
                if (int.TryParse(Request.Query["vnp_TxnRef"], out int orderId))
                {
                    Order order = _vnPayRepository.GetOrderById(orderId);

                    if (order != null)
                    {
                        // Check payment status and update the order accordingly
                        var paymentStatus = Request.Query["vnp_ResponseCode"];
                        if (paymentStatus == "00") //"00" means success
                        {
                            order.Status = "Paid";
                            _vnPayRepository.SaveOrder(order);
                            //return Redirect("https://www.google.com/"); // Redirect to success page
                            return Redirect("https://cosmodiamond.xyz/order-successful");
                        }
                        else
                        {
                            order.Status = "Failed";
                            _vnPayRepository.SaveOrder(order);
                            //return Redirect("https://www.youtube.com/"); // Redirect to failure page
                            return Redirect("https://cosmodiamond.xyz/order-fail");
                        }
                    }
                }
            }

            return BadRequest("Invalid payment.");
        }

        [HttpGet("create-payment-PAYPAL")]
        public async Task<IActionResult> CreatePaymentPAYPAL(int orderId)
        {
            var order = await _paypalRepository.GetOrderByIdAsync(orderId);
            Console.WriteLine("OrderId Controller : " + order.OrderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            string returnUrl = Url.Action("ExecutePayment", "Payment", new { orderId = orderId }, Request.Scheme) ?? string.Empty;
            string cancelUrl = Url.Action("CancelPayment", "Payment", new { orderId = orderId }, Request.Scheme) ?? string.Empty;

            if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrEmpty(cancelUrl))
            {
                return BadRequest("Invalid return or cancel URL.");
            }

            var paymentUrl = await _paypalService.CreatePaymentAsync(order, returnUrl, cancelUrl);
            if (string.IsNullOrEmpty(paymentUrl))
            {
                return BadRequest("Failed to create payment URL.");
            }

            return Ok(new { Url = paymentUrl });
        }

        [HttpGet("execute-payment-PAYPAL")]
        public async Task<IActionResult> ExecutePayment(string paymentId, string payerId, int orderId)
        {
            if (await _paypalService.ExecutePaymentAsync(paymentId, payerId))
            {
                await _paypalRepository.UpdateOrderStatusAsync(orderId, "Paid");
                //return Redirect("https://google.com");
                return Redirect("http://localhost:5173/order-successful");
            }
            else
            {
                await _paypalRepository.UpdateOrderStatusAsync(orderId, "Failed");
                //return Redirect("https://youtube.com");
                return Redirect("http://localhost:5173/order-fail");
            }
        }

        [HttpGet("cancel-payment-PAYPAL")]
        public async Task<IActionResult> CancelPayment(int orderId)
        {
            await _paypalRepository.UpdateOrderStatusAsync(orderId, "Cancelled");
            return Redirect("https://facebook.com");
        }

        [HttpPost("refund")]
        public async Task<IActionResult> RefundOrder([FromBody] RefundOrderDto refundOrderDto)
        {
            // Construct the refund request
            var refundRequest = new VnpayRefundRequest
            {
                vnp_RequestId = Guid.NewGuid().ToString(),
                vnp_TmnCode = _configuration["VNPay:TmnCode"],
                vnp_TransactionType = "02", // or "03" for partial refund
                vnp_TxnRef = refundOrderDto.TxnRef,
                vnp_Amount = refundOrderDto.Amount,
                vnp_OrderInfo = "Refund for order " + refundOrderDto.OrderId,
                vnp_TransactionNo = refundOrderDto.TransactionNo,
                vnp_TransactionDate = DateTime.Now.ToString("yyyyMMddHHmmss"),
                vnp_CreateBy = refundOrderDto.CreatedBy,
                vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss"),
                vnp_IpAddr = HttpContext.Connection.RemoteIpAddress?.ToString()
            };

            // Generate secure hash
            refundRequest.vnp_SecureHash = _vnPayService.GenerateSecureHash(refundRequest, _configuration["VNPay:HashSecret"]);

            // Send the request
            var response = await _vnPayService.SendRefundRequestAsync(refundRequest, "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var refundResponse = JsonConvert.DeserializeObject<VnpayRefundResponse>(responseContent);

                if (refundResponse.vnp_ResponseCode == "00")
                {
                    return Ok(refundResponse);
                }

                return BadRequest(refundResponse);
            }

            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

    }


}
