using Model.Models;
using Repository.Products;

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
            return _coverRepository.GetAllCovers().Where(c => c.Status.ToLower()=="available");
        }

        public Cover GetCoverById(int coverId)
        {
            return _coverRepository.GetCoverById(coverId);
        }

        public void AddCover(Cover cover)
        {
            _coverRepository.AddCover(cover);
            _coverRepository.Save();
        }

        public void UpdateCover(Cover cover)
        {
            _coverRepository.UpdateCover(cover);
            _coverRepository.Save();
        }

        public void DeleteCover(int coverId)
        {
            _coverRepository.DeleteCover(coverId);
            _coverRepository.Save();
        }
    }

}
