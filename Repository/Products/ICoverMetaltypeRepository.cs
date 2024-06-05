using Model.Models;

namespace Repository.Products
{
    public interface ICoverMetaltypeRepository
    {
        CoverMetaltype GetCoverMetaltype(int coverId, int metaltypeId);
    }
}
