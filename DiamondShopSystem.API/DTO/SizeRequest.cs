namespace DiamondShopSystem.API.DTO
{
    public class SizeRequest
    {
        public int SizeId { get; set; }

        public string? SizeName { get; set; }

        public string? SizeValue { get; set; }

        public decimal? SizePrice { get; set; }
        public string status { get; set; }
    }
}
