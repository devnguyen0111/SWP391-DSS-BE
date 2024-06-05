using Model.Models;
using Repository.Products;

namespace Services.Products
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        /*public DiamondService(IDiamondRepository diamondRepository)
        {
            _diamondRepository = diamondRepository;
        }

        public Diamond AddDiamond(Diamond diamond)
        {
            return _diamondRepository.createDiamond(diamond);
        }

        public void DeleteDiamond(int Id)
        {
            _diamondRepository.deleteDiamond(Id);
        }

        public List<Diamond> GetAllDiamonds()
        {
            return _diamondRepository.getAllDiamonds();
        }*/

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
