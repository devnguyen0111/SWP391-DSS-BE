namespace Services.Charge
{
    public interface IStripeService
    {
        Task<string> CreatePaymentIntent(decimal amount, string currency);
        Task<bool> HandlePaymentWebhook(string json);
    }
}
