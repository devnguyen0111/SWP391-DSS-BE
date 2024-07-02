namespace DiamondShopSystem.API.DTO
{
    public class CheckoutRequest
    {
        public int UserId { get; set; }
        public List<ProductQuantity> Products { get; set; }
    }

    public class CheckoutInfoResponse
    {
        public UserInfo UserInfo { get; set; }
        public decimal total {  get; set; }
        public List<ProductInfo> Products { get; set; }
        public List<ShippingMethod1> shippingMethods { get; set; }
        public Address1 ShippingAddress1 { get; set; }
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Email { get; set; }
        public string phoneNum { get; set; }
    }

    public class ProductInfo
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string imgUrl {  get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class Address1
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
    public class ShippingMethod1
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal cost { get; set; }
    }

}
