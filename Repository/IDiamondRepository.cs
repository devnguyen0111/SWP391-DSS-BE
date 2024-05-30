using Model.Models;

namespace Repository
{
    public interface IDiamondRepository
    {
        Diamond createDiamond(Diamond diamond);
        Diamond getDiamondById(int id);
        List<Diamond> getAllDiamonds();
        Diamond updateDiamond(Diamond diamond);
        void deleteDiamond(int id);
    }
}
