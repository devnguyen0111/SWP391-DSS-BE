namespace DiamondShopSystem.API.DTO
{
    public class VnPay
    {
        public class RefundOrderDto
        {
            public string TxnRef { get; set; }
            public long Amount { get; set; }
            public string TransactionNo { get; set; }
            public string OrderId { get; set; }
            public string CreatedBy { get; set; }
        }

        public class VnpayRefundResponse
        {
            public string vnp_ResponseId { get; set; }
            public string vnp_Command { get; set; }
            public string vnp_ResponseCode { get; set; }
            public string vnp_Message { get; set; }
            public string vnp_TmnCode { get; set; }
            public string vnp_TxnRef { get; set; }
            public long vnp_Amount { get; set; }
            public string vnp_BankCode { get; set; }
            public string vnp_PayDate { get; set; }
            public string vnp_TransactionNo { get; set; }
            public string vnp_TransactionType { get; set; }
            public string vnp_TransactionStatus { get; set; }
            public string vnp_OrderInfo { get; set; }
            public string vnp_SecureHash { get; set; }
        }

    }
}
