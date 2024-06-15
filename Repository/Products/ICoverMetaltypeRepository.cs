using Model.Models;

namespace Repository.Products
{
    public interface ICoverMetaltypeRepository
    {
        CoverMetaltype GetCoverMetaltype(int coverId, int metaltypeId);
        void AddCoverMetaType(CoverMetaltype c);
        void RemoveCoverMetalType(CoverMetaltype c);
        List<CoverMetaltype> getAll();
    }
}
