using DAO;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
