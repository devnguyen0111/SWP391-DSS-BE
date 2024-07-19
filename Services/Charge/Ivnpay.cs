using Model.Models;
using static Services.Charge.VnPay;

namespace Services.Charge
{
    public interface Ivnpay
    {
        string CreatePaymentUrl(Order order, string returnUrl);
        public string CreatePayment(Order order, string returnUrl);
        bool ValidateSignature(string queryString, string vnp_HashSecret);
        Task<HttpResponseMessage> SendRefundRequestAsync(VnpayRefundRequest request, string url);
        string GenerateSecureHash(VnpayRefundRequest request, string secretKey);
    }
}
