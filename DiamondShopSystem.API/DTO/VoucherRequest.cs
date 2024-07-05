namespace DiamondShopSystem.API.DTO
{
    public class VoucherRequest
    {
        public string Description { get; set; }
        public DateOnly ExpDate { get; set; }

        public int Quantity { get; set; }
        public decimal? TopPrice { get; set; }

        public decimal? BottomPrice { get; set; }
        public int Rate { get; set; }

    }
}
