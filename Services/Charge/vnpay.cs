using Model.Models;
using Microsoft.Extensions.Configuration;
using static System.Net.WebRequestMethods;

namespace Services.Charge
{
    public class VnPay : Ivnpay
    {
        private readonly string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly IConfiguration _configuration;


        public VnPay(IConfiguration configuration)
        {
            _configuration = configuration;
             vnp_HashSecret = _configuration["VnPay:HashSecret"];
             vnp_TmnCode = _configuration["VnPay:TmnCode"];
        }

        private readonly string vnp_HashSecret;
        private readonly string vnp_TmnCode;
        public string CreatePaymentUrl(Order order, string returnUrl)
        {
            //var orderType = 250000;
            //string data = $"{order.OrderId}|2.1.0|pay|{vnp_TmnCode}|{order.OrderId}|{order.OrderDate:yyyyMMddHHmmss}|{order.OrderDate:yyyyMMddHHmmss}|{"127.0.0.1"}|Thanh toan don hang: {order.OrderId}";

            //var ExpireDate = order.OrderDate.AddMinutes(15).ToString("yyyyMMddHHmmss");
            //string checksum = Utils.HmacSHA512(vnp_HashSecret, data);
            //decimal convertMoney = order.TotalAmount * 25000;
            returnUrl = "https://google.com.vn";

            var vnPay = new VnPayLibrary();
            
            vnPay.AddRequestData("vnp_Amount", ((int)order.TotalAmount * 100000).ToString());
            vnPay.AddRequestData("vnp_Command", "pay");
            vnPay.AddRequestData("vnp_CreateDate", order.OrderDate.ToString("yyyyMMddHHmmss"));
            vnPay.AddRequestData("vnp_CurrCode", "VND");
            vnPay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnPay.AddRequestData("vnp_Locale", "vn");
            vnPay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang: {order.OrderId}");
            vnPay.AddRequestData("vnp_OrderType", "other");
            vnPay.AddRequestData("vnp_ReturnUrl", returnUrl);
            vnPay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnPay.AddRequestData("vnp_TxnRef", order.OrderId.ToString());
            vnPay.AddRequestData("vnp_Version", "2.1.0");
            /*vnPay.AddRequestData("vnp_Amount", convertMoney.ToString());*/

            string paymentUrl = vnPay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;

        }
        public string CreatePayment(Order order, string returnUrl)
        {
            //var orderType = 250000;
            //string data = $"{order.OrderId}|2.1.0|pay|{vnp_TmnCode}|{order.OrderId}|{order.OrderDate:yyyyMMddHHmmss}|{order.OrderDate:yyyyMMddHHmmss}|{"127.0.0.1"}|Thanh toan don hang: {order.OrderId}";

            //var ExpireDate = order.OrderDate.AddMinutes(15).ToString("yyyyMMddHHmmss");
            //string checksum = Utils.HmacSHA512(vnp_HashSecret, data);
            //decimal convertMoney = order.TotalAmount * 25000;
            returnUrl = "https://localhost:7262/api/Payment/PaymentReturn-VNPAY";

            var vnPay = new VnPayLibrary();

            vnPay.AddRequestData("vnp_Amount", ((int)order.TotalAmount*250000).ToString());
            vnPay.AddRequestData("vnp_Command", "pay");
            vnPay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPay.AddRequestData("vnp_CurrCode", "VND");
            vnPay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnPay.AddRequestData("vnp_Locale", "vn");
            vnPay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang: {order.OrderId}");
            vnPay.AddRequestData("vnp_OrderType", "other");
            vnPay.AddRequestData("vnp_ReturnUrl", returnUrl);
            vnPay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnPay.AddRequestData("vnp_TxnRef", order.OrderId.ToString());
            vnPay.AddRequestData("vnp_Version", "2.1.0");
            /*vnPay.AddRequestData("vnp_Amount", convertMoney.ToString());*/

            string paymentUrl = vnPay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;

        }
        public bool ValidateSignature(string queryString, string vnp_HashSecret)
        {
            var vnPay = new VnPayLibrary();

            // check signature
            return vnPay.ValidateSignature(queryString, vnp_HashSecret);
        }
    }
}
