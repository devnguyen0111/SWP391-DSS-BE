using Model.Models;
using Repository.Products;

namespace Services.Products
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }
/*
        public Diamond AddDiamond(Diamond diamond)
        {
            return _diamondRepository.createDiamond(diamond);
        }

        public void DeleteDiamond(int Id)
        {
            _diamondRepository.deleteDiamond(Id);
        }*/

        public List<Size> GetAllSizes()
        {
            return _sizeRepository.GetAllSizes();
        }

        public Size GetSizeById(int Id)
        {
            return _sizeRepository.GetSizeById(Id);
        }

        /*public Diamond UpdateDiamond(Diamond diamond)
        {
            return _diamondRepository.updateDiamond(diamond);
        }*/
    }
}
