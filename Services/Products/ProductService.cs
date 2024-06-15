using Model.Models;
using Repository.Diamonds;
using Repository.Products;

namespace Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICoverRepository _coverRepository;
        private readonly IMetaltypeRepository _metaltypeRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IDiamondRepository _diamondRepository;
    
        public ProductService(IProductRepository productRepository, ICoverRepository coverRepository, IMetaltypeRepository metaltypeRepository, ISizeRepository sizeRepository, IDiamondRepository diamondRepository)
        {
            _productRepository = productRepository;
            _coverRepository = coverRepository;
            _metaltypeRepository = metaltypeRepository;
            _sizeRepository = sizeRepository;
            _diamondRepository = diamondRepository;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(int productId)
        {
            return _productRepository.GetProductById(productId);
        }
        public decimal GetProductTotal(int productId)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                return 0;
            }

            var size = _sizeRepository.GetSizeById((int)product.SizeId).SizeValue;
            var metal = _metaltypeRepository.GetMetaltypeById((int)product.MetaltypeId).MetaltypeName;
            var cover = _coverRepository.GetCoverById((int)product.CoverId).CoverName;
            var coverPrice = (decimal)_coverRepository.GetCoverById((int)product.CoverId).UnitPrice;
            var sizePrice = (decimal)_sizeRepository.GetSizeById((int)product.SizeId).SizePrice;
            var metalPrice = (decimal)_metaltypeRepository.GetMetaltypeById((int)product.MetaltypeId).MetaltypePrice;
            var diamondPrice = _diamondRepository.getDiamondById((int)product.DiamondId).Price;
            var labor = (decimal)product.UnitPrice;

            return coverPrice + sizePrice + metalPrice + diamondPrice + labor;
        }
        public List<ProductQuantity> getMostSaleProduct(int count, string subcate)
        {
            return _productRepository.GetMostOrderedProductsBySubCategory(count, subcate);
        }
        public List<Product> FilterProducts(
        int? categoryId = null,
        int? subCategoryId = null,
        int? metaltypeId = null,
        int? sizeId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null)
        {
            var filteredProducts = _productRepository.GetAllProducts();

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
