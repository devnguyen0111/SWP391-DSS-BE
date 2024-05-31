using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICoverService
    {
        IEnumerable<Cover> GetAllCovers();
        Cover GetCoverById(int coverId);
        void AddCover(Cover cover);
        void UpdateCover(Cover cover);
        void DeleteCover(int coverId);
    }

}
