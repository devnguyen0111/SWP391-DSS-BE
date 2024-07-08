namespace DiamondShopSystem.API.DTO
{
    public class VoucherRequest1
    {
        public List<ProductQuantity> Products { get; set; }
        public string? voucherName { get; set; }
    }
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
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public int ShippingMethodId { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNumber { get; set; }
        public List<ProductQuantity> Products { get; set; }
        public string? voucherName { get; set; }

    }

    public class ProductQuantity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
