using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICoverMetaltypeService
    {
        CoverMetaltype GetCoverMetaltype(int coverId, int metaltypeId);
    }
}
