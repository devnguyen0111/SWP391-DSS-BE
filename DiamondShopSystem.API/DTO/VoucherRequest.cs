namespace DiamondShopSystem.API.DTO
{
    public class VoucherRequest
    {
        public string VoucherName { get; set; }
        public string Description { get; set; }
        public DateOnly ExpDate { get; set; }

        public int Quantity { get; set; }

        public int Rate { get; set; }

        public int CusId { get; set; }
    }
}
