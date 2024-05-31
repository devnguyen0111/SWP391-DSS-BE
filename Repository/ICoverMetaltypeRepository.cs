using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICoverMetaltypeRepository
    {
        CoverMetaltype GetCoverMetaltype(int coverId, int metaltypeId);
    }
}
