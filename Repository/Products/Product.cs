using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Repository.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly DIAMOND_DBContext _context;

        public ProductRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
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
                .Include(p => p.Cover).ThenInclude(p=>p.CoverMetaltypes)
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

        public List<ProductQuantity> GetMostOrderedProductsBySubCategory(int count, string subcate)
        {
            var result = _context.ProductOrders.Include(c => c.Product).ThenInclude(c => c.Cover).ThenInclude(c => c.SubCategory)
                .Where(po => po.Product.Cover.SubCategory.SubCategoryName == subcate)
                .GroupBy(po => po.ProductId)
                .Select(g => new ProductQuantity
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(po => po.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(count)
                .ToList();
            return result;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
    public class ProductQuantity
    {
        public int ProductId { get; set; }
        public int TotalQuantity { get; set; }
    }
}
