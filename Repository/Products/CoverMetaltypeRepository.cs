using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Repository.Products
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
        public void AddCoverMetaType(CoverMetaltype c)
        {
            var metaltype = _context.CoverMetaltypes.FirstOrDefault(m => m.CoverId == c.CoverId);
            if (metaltype != null)
            {
                _context.CoverMetaltypes.Add(c);
                _context.SaveChanges();
            }
        }
        public void RemoveCoverMetalType(CoverMetaltype c)
        {
            var metaltype = _context.CoverMetaltypes.FirstOrDefault(m => m.CoverId == c.CoverId);
            if (metaltype != null)
            {
                _context.CoverMetaltypes.Remove(c);
                _context.SaveChanges();
            }
        }
        public List<CoverMetaltype> getAll()
        {
            return _context.CoverMetaltypes.Include(c => c.Metaltype).Include(c => c.Cover).ToList();
        }
    }
}
