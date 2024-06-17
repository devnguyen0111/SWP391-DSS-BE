using Model.Models;
using Repository.Products;

namespace Services.Products
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int productId);
        List<ProductQuantity> getMostSaleProduct(int count, string subcate);
         List<Product> FilterProducts(
        int? categoryId = null,
        int? subCategoryId = null,
        int? metaltypeId = null,
        int? sizeId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null, string? sortOrder = null,
        int? pageNumber = null, int? pageSize = null);
        decimal GetProductTotal(int productId);
        Product GetExistingProduct(int coverId, int diamondId, int metaltypeId, int sizeId);
        Product UpdateProduct(Product produdct);
        Product AddProduct(Product produdct);
    }
}
