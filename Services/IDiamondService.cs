using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
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
