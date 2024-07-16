namespace DiamondShopSystem.API.DTO
{
    public class CoverResponse
    {
        public int coverId {  get; set; }
        public int categoryId { get; set; }
        public string name {  get; set; }
        public string status { get; set; }
        public decimal prices { get; set; }
        public string? url { get; set; }
        public List<CoverReponseMetal>? metals { get; set; }
        public List<CoverResponeSize>? sizes { get; set; }            
        
    }
    public class CoverReponseMetal
    {
        public int metalId { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string url { get; set; }
        public decimal prize { get; set; }
        
    }
    public class CoverResponeSize
    {
        public int sizeId { get; set; }
        public string name {  set; get; }
        public decimal prices { get; set; }
        public string status { get; set; }


    }
}
