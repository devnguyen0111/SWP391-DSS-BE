namespace DiamondShopSystem.API.DTO
{
    public class OrderHistoryResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingMethodName { get; set; }
        public List<OrderHistoryItem>? Items { get; set; }

        public OrderHistoryResponse()
        {
            Items = new List<OrderHistoryItem>();
        }
    }

    public class OrderHistoryItem
    {
        public int PId { get; set; }
        public bool? ReviewCheck { get; set; }
        public string Name { get; set; }
        public decimal Total {get; set; }
        public string SizeName { get; set; }
        public string MetaltypeName { get; set; }
        public string DiamondName { get; set; }
        public string Img { get; set; }
    }

}