using Model.Models;
using Repository.Products;

namespace Services.Products
{
    public class MetaltypeService : IMetaltypeService
    {
        private readonly IMetaltypeRepository _metaltypeRepository;

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

        public Metaltype GetMetaltypeById(int Id)
        {
            return _metaltypeRepository.GetMetaltypeById(Id);
        }

        /*public Diamond UpdateDiamond(Diamond diamond)
        {
            return _diamondRepository.updateDiamond(diamond);
        }*/
    }
}
