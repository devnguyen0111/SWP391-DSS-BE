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
    public class ProductRepository : IProductRepository
    {
        private readonly DIAMOND_DBContext _context;

        public ProductRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products
                .Include(p => p.Diamond)
                .Include(p => p.Cover)
                .Include(p => p.Metaltype)
                .Include(p => p.Size)
                .ToList();
        }

        public Product GetProductById(int productId)
        {
            return _context.Products
                .Include(p => p.Diamond)
                .Include(p => p.Cover)
                .Include(p => p.Metaltype)
                .Include(p => p.Size)
                .FirstOrDefault(p => p.ProductId == productId);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public void DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
