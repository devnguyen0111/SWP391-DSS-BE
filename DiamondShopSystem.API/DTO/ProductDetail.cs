namespace DiamondShopSystem.API.DTO
{//4c of diamond,cover price,diamond price
    public class ProductDetail
    {
        public int ProductId { get; set; }
        public string imgUrl { get; set; }
        public int categoryId { get; set; }
        public string? CoverStatus { get; set; }
        public string ?DiamondStatus { get; set; }
        public string? ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? DiamondName { get; set; }
        public string? CoverName { get; set; }
        public string? MetaltypeName { get; set; }
        public string? SizeName { get; set; }
        public decimal? diamondPrice { get; set; }
        public decimal? coverPrice { get; set; }
        public string shape { get; set; }  
        public string clarity { get; set; }
        public string color { get; set; }
        public string cut { get; set; }
        public decimal carat {  get; set; }
        public string Pp { get; set; }
        
    }
}
