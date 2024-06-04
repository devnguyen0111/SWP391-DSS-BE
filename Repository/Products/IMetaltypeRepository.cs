using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Products
{
    public interface IMetaltypeRepository
    {
        List<Metaltype> GetAllMetaltypes();
        Metaltype GetMetaltypeById(int id);
        /*void Add(Metaltype metaltype);
        void Update(Metaltype metaltype);
        void Delete(int id);*/
    }
}
