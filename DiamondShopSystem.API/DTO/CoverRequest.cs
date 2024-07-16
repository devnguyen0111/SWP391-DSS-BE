using System.Diagnostics.CodeAnalysis;

namespace DiamondShopSystem.API.DTO
{
    public class CoverRequest1
    {
        public int CoverId { get; set; }
        public string CoverName { get; set; }
        public string Status { get; set; }
        public decimal? UnitPrice { get; set; }
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
    }
    public class CoverDTO
    {
        public int CoverId { get; set; }
        public string CoverName { get; set; }
        public string Status { get; set; }
        public decimal? UnitPrice { get; set; }
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }

    }


    public class CoverUpdateDTO
    {
        public string CoverName { get; set; }
        public string Status { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Category {  get; set; }
        public ICollection<CoverSizeUpdateDTO> CoverSizes { get; set; }
        public ICollection<CoverMetaltypeUpdateDTO> CoverMetaltypes { get; set; }
    }

    public class CoverSizeUpdateDTO
    {
        public int SizeId { get; set; }
        public string? Status { get; set; }
    }

    public class CoverMetaltypeUpdateDTO
    {
        
        [NotNull]

        public int MetaltypeId { get; set; }
        [NotNull]

        public string ImgUrl { get; set; }
        public string? Status { get; set; }
    }
    public class CoverMetaltypeDeleteDTO
    {
        [NotNull]
        public int coverId { get; set; }
        [NotNull]
        public int MetaltypeId { get; set; }

        
    }

}
