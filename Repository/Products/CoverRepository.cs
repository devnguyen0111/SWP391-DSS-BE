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
            //Cover c = GetCoverById(cover.CoverId);
            //c.CoverMetaltypes = cover.CoverMetaltypes.ToList();
            //c.CoverSizes = cover.CoverSizes.ToList();
            _context.Update(cover);
            _context.SaveChanges();
        }
        public void EmptyCover(int id) { 
            var c = _context.Covers.Include(c=>c.CoverMetaltypes).Include(c=>c.CoverSizes).FirstOrDefault(c => c.CoverId == id);
            c.CoverMetaltypes.Clear();
            c.CoverSizes.Clear();
            //_context.SaveChanges();
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
        public void DetachLocalCover(Cover t, string entryId)
        {
            var local = _context.Set<Cover>()
                .Local
                .FirstOrDefault(entry => entry.CoverId.Equals(entryId));
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(t).State = EntityState.Modified;
        }
        public void DetachLocalSize (CoverSize t, string entryId)
        {
            var local = _context.Set<CoverSize>()
                .Local
                .FirstOrDefault(entry => entry.CoverId.Equals(entryId));
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(t).State = EntityState.Modified;
        }
        public void DetachLocalMetalType(CoverMetaltype t, string entryId)
        {
            var local = _context.Set<CoverMetaltype>()
                .Local
                .FirstOrDefault(entry => entry.CoverId.Equals(entryId));
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(t).State = EntityState.Modified;
        }
    }

}
