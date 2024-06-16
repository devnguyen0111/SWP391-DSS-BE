using Model.Models;

namespace Repository.Products
{
    public interface ICoverSizeRepository
    {
        CoverSize GetCoverSize(int coverId, int sizeId);
        List<CoverSize> GetCoverSizes(int coverId);
    }
}
