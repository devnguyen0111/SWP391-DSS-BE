using Microsoft.AspNetCore.Http;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Services.Charge
{
    public class VnPay : Ivnpay
    {
        private readonly string vnp_TmnCode = "OYMZVOG7";
        private readonly string vnp_HashSecret = "MEIJ0KIOZC8Z8ZU2A5W28CT7RAC6K9I0";
        private readonly string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VnPay(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string CreatePaymentUrl(Order order, string returnUrl)
        {
            var vnPay = new VnPayLibrary();
            vnPay.AddRequestData("vnp_Version", "2.1.0");
            vnPay.AddRequestData("vnp_Command", "pay");
            vnPay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnPay.AddRequestData("vnp_Amount", ((int)order.TotalAmount * 1000).ToString());
            vnPay.AddRequestData("vnp_CreateDate", order.OrderDate.ToString("yyyyMMddHHmmss"));
            vnPay.AddRequestData("vnp_CurrCode", "VND");
            vnPay.AddRequestData("vnp_IpAddr", _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
            vnPay.AddRequestData("vnp_Locale", "vn");
            vnPay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang: {order.OrderId}");
            vnPay.AddRequestData("vnp_OrderType", "other");
            vnPay.AddRequestData("vnp_ReturnUrl", returnUrl);
            vnPay.AddRequestData("vnp_TxnRef", order.OrderId.ToString());

            string paymentUrl = vnPay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;

        }

        public bool ValidateSignature(string queryString, string vnp_HashSecret)
        {
            var vnPay = new VnPayLibrary();
            return vnPay.ValidateSignature(queryString, vnp_HashSecret);
        }
    }
}
