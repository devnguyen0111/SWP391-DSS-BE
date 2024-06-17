using Model.Models;

namespace Repository.Products
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int productId);
        public List<ProductQuantity> GetMostOrderedProductsBySubCategory(int count, string subcate);
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        
    }
}