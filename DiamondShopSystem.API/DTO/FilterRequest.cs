namespace DiamondShopSystem.API.DTO
{
    public class FilterRequest
    {
        public List<int>? SizeIds { get; set; }
        public List<int>? MetaltypeIds { get; set; }
        public List<string>? DiamondShapes { get; set; }
    }
}
