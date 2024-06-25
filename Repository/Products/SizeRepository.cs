using DAO;
using Model.Models;

namespace Repository.Products
{
    public class SizeRepository : ISizeRepository
    {
        private readonly DIAMOND_DBContext _context;

        public SizeRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public List<Size> GetAllSizes()
        {
            return _context.Sizes.ToList();
        }

        public Size GetSizeById(int id)
        {
            return _context.Sizes.Find(id);
        }
        public List<int> getSizesByCate(int cateId)
        {
            var sizeIds = (from s in _context.Sizes
                           join cs in _context.CoverSizes on s.SizeId equals cs.SizeId
                           join c in _context.Covers on cs.CoverId equals c.CoverId
                           join sc in _context.SubCategories on c.SubCategoryId equals sc.SubCategoryId
                           where sc.CategoryId == cateId
                           select s.SizeId).Distinct().ToList();
            return sizeIds;
        }

        /*public void Add(Size size)
        {
            _context.Sizes.Add(size);
            _context.SaveChanges();
        }

        public void Update(Size size)
        {
            _context.Sizes.Update(size);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var size = _context.Sizes.Find(id);
            if (size != null)
            {
                _context.Sizes.Remove(size);
                _context.SaveChanges();
            }
        }*/
    }
}
