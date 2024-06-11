using Model.Models;

namespace Services.Products
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int productId);
        public List<Product> FilterProducts(
        int? categoryId = null,
        int? subCategoryId = null,
        int? metaltypeId = null,
        int? sizeId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null);
        
    }
}
