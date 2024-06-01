using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IMetaltypeService
    {
        /*Size addSize (Size size);
        void DeleteDiamond(int id);
        List<Diamond> GetAllDiamonds();*/
        Metaltype GetSizeById(int id);
        //Diamond UpdateDiamond(Diamond diamond);
    }
}
