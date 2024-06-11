using Model.Models;

namespace Repository.Products
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int productId);
        public List<Product> getAllProductsNotReally();
    }
}