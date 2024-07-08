namespace DiamondShopSystem.API.DTO
{
    public class orderRespone
    {
        public string status { get; set; }
        public decimal total { get; set; }
        public List<orderItem> items { get; set; }  
    }
    public class orderItem
    {
        public int pid { get; set; }
        public string name1 { get; set; }
        public int quantity { get; set; }
        public string img { get; set; }
    }
    public class OrderHistoryResponse1
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingMethodName { get; set; }
    }
}
