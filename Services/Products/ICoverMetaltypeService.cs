using Model.Models;

namespace Services.Products
{
    public interface ICoverMetaltypeService
    {
        CoverMetaltype GetCoverMetaltype(int coverId, int metaltypeId);
        List<CoverMetaltype> GetCoverMetaltypes(int coverId);
        void AddCoverMetalType(CoverMetaltype c);

        void RemoveCoverMetalType(CoverMetaltype c);
    }
}
