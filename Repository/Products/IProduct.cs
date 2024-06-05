using Model.Models;

namespace Repository.Products
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
    }
}