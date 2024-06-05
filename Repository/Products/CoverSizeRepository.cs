using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Products
{
    public class CoverSizeRepository : ICoverSizeRepository
    {
        private readonly DIAMOND_DBContext _context;

        public CoverSizeRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public CoverSize GetCoverSize(int coverId, int sizeId)
        {
            return _context.CoverSizes
                .Include(cs => cs.Cover)
                .Include(cs => cs.Size)
                .SingleOrDefault(cs => cs.CoverId == coverId && cs.SizeId == sizeId);
        }
    }
}
