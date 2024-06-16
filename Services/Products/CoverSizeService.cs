using Model.Models;
using Repository.Products;

namespace Services.Products
{
    public class CoverSizeService : ICoverSizeService
    {
        private readonly ICoverSizeRepository _coverSizeRepository;

        public CoverSizeService(ICoverSizeRepository coverSizeRepository)
        {
            _coverSizeRepository = coverSizeRepository;
        }

        public CoverSize GetCoverSize(int coverId, int sizeId)
        {
            return _coverSizeRepository.GetCoverSize(coverId, sizeId);
        }
        public List<CoverSize>? GetCoverSizes(int coverId)
        {
            if(_coverSizeRepository.GetCoverSizes(coverId) == null)
            {
                return null;
            }
            return _coverSizeRepository.GetCoverSizes(coverId); 
        }
    }
}
