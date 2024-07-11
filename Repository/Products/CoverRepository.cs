using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Repository.Products
{
    public class CoverRepository : ICoverRepository
    {
        private readonly DIAMOND_DBContext _context;

        public CoverRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Cover> GetAllCovers()
        {
            return _context.Covers.Include(c => c.CoverMetaltypes).Include(c => c.CoverSizes).ToList();
        }

        public Cover GetCoverById(int coverId)
        {
            return _context.Covers.FirstOrDefault(c => c.CoverId == coverId);
        }

        public void AddCover(Cover cover)
        {
            _context.Covers.Add(cover);
            _context.SaveChanges();
        }

        public void UpdateCover(Cover cover)
        {
            _context.Entry(cover).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCover(int coverId)
        {
            var cover = _context.Covers.Find(coverId);
            if (cover != null)
            {
                cover.Status = "Disabled";
                UpdateCover(cover);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}
