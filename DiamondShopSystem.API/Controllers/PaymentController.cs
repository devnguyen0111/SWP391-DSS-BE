using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Charge;
using Services.Utility;
using Repository.Charge;

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

        // logs to webhook discord Server DSS
        private readonly IDiscordWebhookService _discordWH;
        public PaymentController(Ivnpay vnPayService, IVnPayRepository vnPayRepository,IPaypalRepository paypalRepository, IPaypalService paypalService ,IConfiguration configuration, IDiscordWebhookService discordWebhookService)
        {
            _vnPayService = vnPayService;
            _vnPayRepository = vnPayRepository;
            _configuration = configuration;
            _paypalService = paypalService;
            _paypalRepository = paypalRepository;
            _discordWH = discordWebhookService;

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
                string paymentUrl = _vnPayService.CreatePaymentUrl(order, returnUrl);
                _discordWH.SendLogAsync($"Payment VN-PAY created for Order {orderId}. Payment URL: {paymentUrl}");

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

            // Validate the signature of the payment URL
            if (!string.IsNullOrEmpty(queryString) && _vnPayService.ValidateSignature(queryString, vnp_HashSecret))
            {
                // Retrieve the order ID from the query string
                if (int.TryParse(Request.Query["vnp_TxnRef"], out int orderId))
                {
                    Order order = _vnPayRepository.GetOrderById(orderId);

                    if (order != null)
                    {
                        order.Status = "Paid";
                        _vnPayRepository.SaveOrder(order);
                        _discordWH.SendLogAsync($"Payment VN-PAY executed successfully for Order {orderId}");
                        return Ok("Payment successful.");
                    }
                    _discordWH.SendLogAsync($"Payment VN-PAY failed for Order {orderId}");
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
            await _discordWH.SendLogAsync($"Payment created for Order {orderId}. Payment URL: {paymentUrl}");
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
                await _discordWH.SendLogAsync($"Payment executed successfully for Order {orderId}. Payment ID: {paymentId}");
                return Redirect("https://google.com");
            }
            else
            {
                await _paypalRepository.UpdateOrderStatusAsync(orderId, "Failed");
                await _discordWH.SendLogAsync($"Payment failed for Order {orderId}. Payment ID: {paymentId}");
                return Redirect("https://youtube.com");
            }
        }

        [HttpGet("cancel-payment-PAYPAL")]
        public async Task<IActionResult> CancelPayment(int orderId)
        {
            await _paypalRepository.UpdateOrderStatusAsync(orderId, "Cancelled");
            await _discordWH.SendLogAsync($"Payment cancelled for Order {orderId}");
            return Redirect("https://facebook.com");
        }
    }
}
