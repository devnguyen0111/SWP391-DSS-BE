using Model.Models;

namespace Services.Products
{
    public interface ISizeService
    {
        /*Size addSize (Size size);
        void DeleteDiamond(int id);
        List<Diamond> GetAllDiamonds();*/
        Size GetSizeById(int id);
        //Diamond UpdateDiamond(Diamond diamond);
    }
}
