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
    }
}
