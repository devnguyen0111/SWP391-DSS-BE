using Model.Models;

namespace Services.Products
{
    public interface ICoverSizeService
    {
        CoverSize GetCoverSize(int coverId, int sizeId);
    }
}
