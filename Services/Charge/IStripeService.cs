using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Charge
{
    public interface IStripeService
    {
        Task<string> CreatePaymentIntent(decimal amount, string currency);
        Task<bool> HandlePaymentWebhook(string json);
    }
}
