namespace DiamondShopSystem.API.DTO
{
    public class FeebackDTO
    {
        public string cusName {  get; set; }
        public decimal ratings { get; set; }
        public DateOnly datePost {  get; set; }
        public string feedback { get; set; }
        public string productName { get; set; }
        public string imgUrl { get; set; }
    }
}
