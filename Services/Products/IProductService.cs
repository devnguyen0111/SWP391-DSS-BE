using Model.Models;

namespace Services.Products
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int productId);
    }
}
