using Model.Models;

namespace Services.Products
{
    public interface ICoverMetaltypeService
    {
        CoverMetaltype GetCoverMetaltype(int coverId, int metaltypeId);
    }
}
