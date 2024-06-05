using Model.Models;

namespace Services.Products
{
    public interface IMetaltypeService
    {
        /*Size addSize (Size size);
        void DeleteDiamond(int id);
        List<Diamond> GetAllDiamonds();*/
        Metaltype GetMetaltypeById(int id);
        //Diamond UpdateDiamond(Diamond diamond);
    }
}
