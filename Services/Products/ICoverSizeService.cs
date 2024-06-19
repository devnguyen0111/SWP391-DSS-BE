using Model.Models;

namespace Services.Products
{
    public interface ICoverSizeService
    {
        CoverSize GetCoverSize(int coverId, int sizeId);
        List<CoverSize> GetCoverSizes(int coverId);
    }
}
