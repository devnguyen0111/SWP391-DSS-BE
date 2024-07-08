namespace DiamondShopSystem.API.DTO
{
    public class ReviewRespone
    {
        public string name {  get; set; }
        public string review { get; set; } 
        public decimal rate { get; set; }
        public DateOnly date { get; set; }
    }
}
