using Model.Models;

namespace Repository.Products
{
    public interface ISizeRepository
    {
        List<Size> GetAllSizes();
        Size GetSizeById(int id);
        /*void Add(Size size);
        void Update(Size size);
        void Delete(int id);*/
    }
}
