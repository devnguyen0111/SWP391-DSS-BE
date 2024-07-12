using Microsoft.AspNetCore.Mvc;
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
        public ProductService(IProductRepository r) {
            _productRepository = r;
        }

        public Category GetCategoryById(int categoryId)
        {
            return _productRepository.GetCategoryById(categoryId);
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
        //    public List<Product> FilterProductsAd(
        //int? categoryId = null,
        //int? subCategoryId = null,
        //int? metaltypeId = null,
        //int? sizeId = null,
        //decimal? minPrice = null,
        //decimal? maxPrice = null,
        //string? sortOrder = null,
        //List<int>? sizeIds = null,
        //List<int>? metaltypeIds = null,
        //List<string>? diamondShapes = null,
        //int? pageNumber = null,
        //int? pageSize = null)
        //    {
        //        var filteredProducts = _productRepository.GetAllProducts();

        //        if (categoryId.HasValue)
        //        {
        //            filteredProducts = filteredProducts.Where(p => p.Cover.CategoryId == categoryId.Value).ToList();
        //        }

        //        if (subCategoryId.HasValue)
        //        {
        //            filteredProducts = filteredProducts.Where(p => p.Cover.SubCategoryId == subCategoryId.Value).ToList();
        //        }

        //        if (metaltypeId.HasValue)
        //        {
        //            filteredProducts = filteredProducts.Where(p => p.Metaltype.MetaltypeId == metaltypeId.Value).ToList();
        //        }

        //        if (sizeId.HasValue)
        //        {
        //            filteredProducts = filteredProducts.Where(p => p.Size.SizeId == sizeId.Value).ToList();
        //        }

        //        if (sizeIds != null && sizeIds.Any())
        //        {
        //            filteredProducts = filteredProducts.Where(p => sizeIds.Contains(p.Size.SizeId)).ToList();
        //        }

        //        if (metaltypeIds != null && metaltypeIds.Any())
        //        {
        //            filteredProducts = filteredProducts.Where(p => metaltypeIds.Contains(p.Metaltype.MetaltypeId)).ToList();
        //        }

        //        if (diamondShapes != null && diamondShapes.Any())
        //        {
        //            filteredProducts = filteredProducts.Where(p => diamondShapes.Contains(p.Diamond.Shape)).ToList();
        //        }

        //        if (minPrice.HasValue)
        //        {
        //            filteredProducts = filteredProducts.Where(c => c.Cover.UnitPrice + c.UnitPrice + c.Diamond.Price >= minPrice.Value).ToList();
        //        }

        //        if (maxPrice.HasValue)
        //        {
        //            filteredProducts = filteredProducts.Where(c => c.Cover.UnitPrice + c.UnitPrice + c.Diamond.Price <= maxPrice.Value).ToList();
        //        }

        //        if (sortOrder != null)
        //        {
        //            if (sortOrder.Equals("asc"))
        //            {
        //                filteredProducts = filteredProducts.OrderBy(c => c.Cover.UnitPrice+c.UnitPrice+c.Diamond.Price).ToList();

        //            }
        //            else
        //            {
        //                filteredProducts = filteredProducts.OrderByDescending(c => c.Cover.UnitPrice + c.UnitPrice + c.Diamond.Price).ToList();
        //            }
        //        }

        //        if (pageNumber.HasValue && pageSize.HasValue)
        //        {
        //            filteredProducts = filteredProducts
        //                .Skip((pageNumber.Value - 1) * pageSize.Value)
        //                .Take(pageSize.Value)
        //                .ToList();
        //        }

        //        return filteredProducts;
        //    }
        public List<Product> FilterProductsAd(
        int? categoryId = null,
        int? subCategoryId = null,
        int? metaltypeId = null,
        int? sizeId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        string? sortOrder = null,
        List<int>? sizeIds = null,
        List<int>? metaltypeIds = null,
        List<string>? diamondShapes = null,
        int? pageNumber = null,
        int? pageSize = null)
        {
            var filteredProducts = _productRepository.GetAllProducts().AsQueryable();

            if (categoryId.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Cover.CategoryId == categoryId.Value);
            }

            if (subCategoryId.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Cover.SubCategoryId == subCategoryId.Value);
            }

            if (metaltypeId.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Metaltype.MetaltypeId == metaltypeId.Value);
            }

            if (sizeId.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Size.SizeId == sizeId.Value);
            }

            if (sizeIds != null && sizeIds.Any())
            {
                filteredProducts = filteredProducts.Where(p => sizeIds.Contains(p.Size.SizeId));
            }

            if (metaltypeIds != null && metaltypeIds.Any())
            {
                filteredProducts = filteredProducts.Where(p => metaltypeIds.Contains(p.Metaltype.MetaltypeId));
            }

            if (diamondShapes != null && diamondShapes.Any())
            {
                filteredProducts = filteredProducts.Where(p => diamondShapes.Contains(p.Diamond.Shape));
            }

            if (minPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(c => c.Cover.UnitPrice + c.UnitPrice + c.Diamond.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(c => c.Cover.UnitPrice + c.UnitPrice + c.Diamond.Price <= maxPrice.Value);
            }

            if (sortOrder != null)
            {
                if (sortOrder.Equals("asc"))
                {
                    filteredProducts = filteredProducts.OrderBy(c => c.Cover.UnitPrice + c.UnitPrice + c.Diamond.Price);
                }
                else
                {
                    filteredProducts = filteredProducts.OrderByDescending(c => c.Cover.UnitPrice + c.UnitPrice + c.Diamond.Price);
                }
            }

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                filteredProducts = filteredProducts
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return filteredProducts.Where(c => c.Pp != "custom").ToList();
        }


        public Product GetExistingProduct(int coverId, int diamondId, int metaltypeId, int sizeId)
        {
            return  _productRepository.GetAllProducts()
                .FirstOrDefault(p => p.CoverId == coverId && p.DiamondId == diamondId
                    && p.MetaltypeId == metaltypeId && p.SizeId == sizeId);
        }
        public Product UpdateProduct(Product produdct)
        {
            return _productRepository.UpdateProduct(produdct);
        }
        public Product AddProduct(Product produdct)
        {
            return _productRepository.AddProduct(produdct);
        }
    }
}
