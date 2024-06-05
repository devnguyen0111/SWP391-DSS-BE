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
            return _context.Covers.ToList();
        }

        public Cover GetCoverById(int coverId)
        {
            return _context.Covers.Find(coverId);
        }

        public void AddCover(Cover cover)
        {
            _context.Covers.Add(cover);
        }

        public void UpdateCover(Cover cover)
        {
            _context.Entry(cover).State = EntityState.Modified;
        }

        public void DeleteCover(int coverId)
        {
            var cover = _context.Covers.Find(coverId);
            if (cover != null)
            {
                _context.Covers.Remove(cover);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}
