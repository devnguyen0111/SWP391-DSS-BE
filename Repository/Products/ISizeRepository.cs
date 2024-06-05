using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
