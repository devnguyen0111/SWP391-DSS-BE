namespace DiamondShopSystem.API.DTO
{
    public class OrderRequest
    {
        public int? CusId { get; set; }
        public int? ShippingMethodId { get; set; }
        public int  pid { get; set; }
        public string deliveryAddress { get; set; }
        public string contactNumber { get; set; }
    }
    public class OrderRequestCart
    {
        public int? CusId { get; set; }
        public int? ShippingMethodId { get; set; }
        public string deliveryAddress { get; set; }
        public string contactNumber { get; set; }
    }

}
