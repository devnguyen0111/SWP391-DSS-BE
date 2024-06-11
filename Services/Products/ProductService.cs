using Model.Models;
using Repository.Products;

namespace Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(int productId)
        {
            return _productRepository.GetProductById(productId);
        }
        public List<Product> FilterProducts(
        int? categoryId = null,
        int? subCategoryId = null,
        int? metaltypeId = null,
        int? sizeId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null)
        {
            var filteredProducts = _productRepository.getAllProductsNotReally();

            if (categoryId.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Cover.CategoryId == categoryId.Value).ToList();
            }

            if (subCategoryId.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Cover.SubCategoryId == subCategoryId.Value).ToList();
            }

            if (metaltypeId.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Metaltype.MetaltypeId == metaltypeId.Value).ToList();
            }

            if (sizeId.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Size.SizeId == sizeId.Value).ToList();
            }

            if (minPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.UnitPrice >= minPrice.Value).ToList();
            }

            if (maxPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.UnitPrice <= maxPrice.Value).ToList();
            }

            return filteredProducts.ToList();
        }
    }
}
