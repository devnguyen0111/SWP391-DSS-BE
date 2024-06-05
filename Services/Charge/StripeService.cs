using Stripe;
using Stripe.Checkout;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Services.Charge
{
    public class StripeService : IStripeService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StripeService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> CreatePaymentIntent(decimal amount, string currency)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Stripe requires the amount to be in cents
                Currency = currency,
                PaymentMethodTypes = new List<string> { "card" },
            };
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);
            return paymentIntent.ClientSecret;
        }

        public async Task<bool> HandlePaymentWebhook(string json)
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                _httpContextAccessor.HttpContext.Request.Headers["Stripe-Signature"],
                _configuration["Stripe:WebhookSecret"]
            );

            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                // Handle successful payment here
                if (paymentIntent == null)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
