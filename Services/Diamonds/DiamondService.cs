using Model.Models;
using Repository.Diamonds;

namespace Services.Diamonds
{
    public class DiamondService : IDiamondService
    {
        private readonly IDiamondRepository _diamondRepository;

        public DiamondService(IDiamondRepository diamondRepository)
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
        }

        public Diamond GetDiamondById(int Id)
        {
            return _diamondRepository.getDiamondById(Id);
        }

        public Diamond UpdateDiamond(Diamond diamond)
        {
            return _diamondRepository.updateDiamond(diamond);
        }
    }
}
