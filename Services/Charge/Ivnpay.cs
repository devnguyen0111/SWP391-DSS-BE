using Model.Models;

namespace Services.Charge
{
    public interface Ivnpay
    {
        string CreatePaymentUrl(Order order, string returnUrl);
        bool ValidateSignature(string queryString, string vnp_HashSecret);
    }
}
