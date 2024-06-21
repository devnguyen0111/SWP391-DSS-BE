namespace DiamondShopSystem.API.DTO
{
    public class OrderHistoryResponse
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public string ShippingMethodName { get; set; }

        public string ProductName { get; set; }

        public string SizeName { get; set; }

        public string MetaltypeName { get; set; }

        public string DiamondName { get; set; }

        public decimal TotalAmount { get; set; }

        public List<orderHistoryItem> items { get; set; }

        public OrderHistoryResponse()
        {
            items = new List<OrderHistoryItem>();
        }
    }
    public class orderHistoryItem
    {
        public int oHId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public string img { get; set; }
    }

}