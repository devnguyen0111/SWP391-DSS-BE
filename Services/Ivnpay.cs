using Model.Models;
using Org.BouncyCastle.Asn1.X9;

namespace Services
{
    public interface Ivnpay
    {
        string CreatePaymentUrl(Order order, string returnUrl);
        bool ValidateSignature(string queryString, string vnp_HashSecret);
    }
}
