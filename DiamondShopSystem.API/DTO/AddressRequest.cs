namespace DiamondShopSystem.API.DTO
{
    public class AddressRequest
    {
        public int id { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
    }
}
