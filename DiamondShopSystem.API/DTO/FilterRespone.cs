namespace DiamondShopSystem.API.DTO
{
    public class FilterRespone
    {
        List<sizeFilter> sizes {  get; set; }
        List<metaltypeFilter> metaltypes { get; set; }
        List<diamondShape> shapes { get; set; }
    }
    public class sizeFilter
    {
        public int id { get; set; }
        public string value { get; set; }
        public string status { get; set; }
        public decimal prices { get; set; }

    }
    public class metaltypeFilter
    {
        public int id { get; set; }
        public string value { get; set; }
        public string status { get; set; }
        public decimal price {  get; set; }
    }
    public class diamondShape
    {
        public string value { get; set; }
    }
}
