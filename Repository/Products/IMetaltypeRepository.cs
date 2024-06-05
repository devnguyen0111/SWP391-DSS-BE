using Model.Models;

namespace Repository.Products
{
    public interface IMetaltypeRepository
    {
        IEnumerable<Metaltype> GetAll();
        Metaltype GetMetaltypeById(int id);
        /*void Add(Metaltype metaltype);
        void Update(Metaltype metaltype);
        void Delete(int id);*/
    }
}
