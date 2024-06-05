using Model.Models;

namespace Services.Products
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
    }
}
