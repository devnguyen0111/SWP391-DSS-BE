namespace DiamondShopSystem.API.DTO
{
    public class ProductDetail
    {
        public int ProductId { get; set; }
        public string imgUrl { get; set; }

        public string? ProductName { get; set; }

        public decimal? UnitPrice { get; set; }

        public string? DiamondName { get; set; }

        public string? CoverName { get; set; }

        public string? MetaltypeName { get; set; }

        public string? SizeName { get; set; }

        public string Pp { get; set; }
    }
}
