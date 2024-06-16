using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;

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
        public List<CoverSize> GetCoverSizes(int coverId)
        {
            return _context.CoverSizes.Where(c => c.CoverId == coverId).ToList();

        }
    }
}
