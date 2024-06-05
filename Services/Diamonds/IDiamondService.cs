using Model.Models;

namespace Services.Diamonds
{
    public interface IDiamondService
    {
        Diamond AddDiamond(Diamond diamond);
        void DeleteDiamond(int id);
        List<Diamond> GetAllDiamonds();
        Diamond GetDiamondById(int id);
        Diamond UpdateDiamond(Diamond diamond);
    }
}
