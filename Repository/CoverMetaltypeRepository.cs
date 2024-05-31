using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CoverMetaltypeRepository : ICoverMetaltypeRepository
    {
        private readonly DIAMOND_DBContext _context;

        public CoverMetaltypeRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public CoverMetaltype GetCoverMetaltype(int coverId, int metaltypeId)
        {
            return _context.CoverMetaltypes
                .Include(cs => cs.Cover)
                .Include(cs => cs.Metaltype)
                .SingleOrDefault(cs => cs.CoverId == coverId && cs.MetaltypeId == metaltypeId);
        }
    }
}
