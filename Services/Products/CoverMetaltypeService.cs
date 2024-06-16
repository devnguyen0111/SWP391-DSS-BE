using Model.Models;
using Repository.Products;

namespace Services.Products
{
    public class CoverMetaltypeService : ICoverMetaltypeService
    {
        private readonly ICoverMetaltypeRepository _coverSizeRepository;

        public CoverMetaltypeService(ICoverMetaltypeRepository coverSizeRepository)
        {
            _coverSizeRepository = coverSizeRepository;
        }

        public CoverMetaltype GetCoverMetaltype(int coverId, int metaltypeId)
        {
            return _coverSizeRepository.GetCoverMetaltype(coverId, metaltypeId);
        }
        public List<CoverMetaltype> GetCoverMetaltypes(int coverId) {
            return _coverSizeRepository.getAll().Where(c => c.CoverId == coverId).ToList();
        }
        public void AddCoverMetalType(CoverMetaltype c)
        {
             _coverSizeRepository.AddCoverMetaType(c);
        }
        public void RemoveCoverMetalType(CoverMetaltype c)
        {
            _coverSizeRepository.RemoveCoverMetalType(c);
        }
    }
}
