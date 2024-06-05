using Model.Models;

namespace Services.Charge
{
    public interface IPaypalService
    {
        Task<string> CreatePaymentAsync(Order order, string returnUrl, string cancelUrl);
        Task<bool> ExecutePaymentAsync(string paymentId, string payerId);
    }
}
