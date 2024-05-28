using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
