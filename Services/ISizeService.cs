﻿using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
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
