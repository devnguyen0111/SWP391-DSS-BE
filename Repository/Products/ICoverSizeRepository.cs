using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Products
{
    public interface ICoverSizeRepository
    {
        CoverSize GetCoverSize(int coverId, int sizeId);
    }
}
