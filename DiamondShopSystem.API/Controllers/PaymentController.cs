using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services;
using DAO;
using Model.Models;
using Repository;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        // payment service VNPAY
        private readonly Ivnpay _vnPayService;
        private readonly IVnPayRepository _vnPayRepository;

        // payment service Stripe
        private readonly IStripeService _stripeService;
        private readonly IStripeRepository _stripeRepository;
        private readonly IConfiguration _configuration;

        public PaymentController(Ivnpay vnPayService, IVnPayRepository vnPayRepository, IStripeService stripeService, IStripeRepository stripeRepository, IConfiguration configuration)
        {
            _vnPayService = vnPayService;
            _vnPayRepository = vnPayRepository;
            _stripeService = stripeService;
            _stripeRepository = stripeRepository;
            _configuration = configuration;

        }

        [HttpPost("CreatePayment")]
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
                string returnUrl = Url.Action("PaymentReturn", "Checkout", null, Request.Scheme);
                string paymentUrl = _vnPayService.CreatePaymentUrl(order, returnUrl);

                return Ok(new { url = paymentUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("PaymentReturn")]
        public IActionResult PaymentReturn()
        {
            string queryString = Request.QueryString.Value;

            if (_vnPayService.ValidateSignature(queryString, "MEIJ0KIOZC8Z8ZU2A5W28CT7RAC6K9I0"))
            {
                // Retrieve the order ID from the query string
                int orderId = int.Parse(Request.Query["vnp_TxnRef"]);
                Order order = _vnPayRepository.GetOrderById(orderId);

                if (order != null)
                {
                    order.Status = "Paid";
                    _vnPayRepository.SaveOrder(order);
                    return Ok("Payment successful.");
                }
            }

            return BadRequest("Invalid payment.");
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent(int orderId)
        {
            var order = await _stripeRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            var clientSecret = await _stripeService.CreatePaymentIntent(order.TotalAmount, "usd");
            var publishableKey = _configuration["Stripe:PublishableKey"];
            var returnData = new { clientSecret, publishableKey };
            return Json(returnData);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var isHandled = await _stripeService.HandlePaymentWebhook(json);

            if (isHandled)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
