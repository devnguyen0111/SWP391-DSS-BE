namespace DiamondShopSystem.API.DTO
{
    public class FilterRespone
    {
        List<sizeFilter> sizes {  get; set; }
        List<metaltypeFilter> metaltypes { get; set; }
    }
    public class sizeFilter
    {
        public string value { get; set; }
    }
    public class metaltypeFilter
    {
        public string value { get; set; }
    }
}
