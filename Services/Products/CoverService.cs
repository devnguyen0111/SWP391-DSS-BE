using Model.Models;
using Repository.Products;
using Services.Utility;

namespace Services.Products
{
    public class CoverService : ICoverService
    {
        private readonly ICoverRepository _coverRepository;

        public CoverService(ICoverRepository coverRepository)
        {
            _coverRepository = coverRepository;
        }

        //for customer
        public IEnumerable<Cover> GetAllCovers()
        {
            return _coverRepository.GetAllCovers();
        }
        public bool CombinationExists(int coverId, int sizeId, int metaltypeId)
        {
            var cover = _coverRepository.GetCoverById(coverId);
            if (cover == null) return false;

            return cover.CoverSizes.Any(cs => cs.SizeId == sizeId) && cover.CoverMetaltypes.Any(cm => cm.MetaltypeId == metaltypeId);
        }
        public Cover GetCoverById(int coverId)
        {
            return _coverRepository.GetCoverById(coverId);
        }

        public void AddCover(Cover cover)
        {
            _coverRepository.AddCover(cover);
        }

        public void UpdateCover(Cover cover)
        {
            _coverRepository.UpdateCover(cover);
        }

        public void DeleteCover(int coverId)
        {
            _coverRepository.DeleteCover(coverId);
        }
        public string DetermineCoverStatus(int coverId)
        {
            
            var cover =_coverRepository.GetCoverById(coverId);
            if (StringUltis.AreEqualIgnoreCase(cover.Status, "Unavailable"))
            {
                return "Unavailable";
            }
            bool isAvailable = cover.CoverSizes.Any(cs => cs.Status == "Available") ||
                          cover.CoverMetaltypes.Any(cm => cm.Status == "Available");
            return isAvailable ? "Available" : "Unavailable";
        }
        public string DetermineCoverStatus1(Cover cover)
        {

            if (StringUltis.AreEqualIgnoreCase(cover.Status, "Unavailable"))
            {
                return "Unavailable";
            }
            bool isAvailable = cover.CoverSizes.Any(cs => cs.Status == "Available") ||
                          cover.CoverMetaltypes.Any(cm => cm.Status == "Available");
            return isAvailable ? "Available" : "Unavailable";
        }
    }

}
