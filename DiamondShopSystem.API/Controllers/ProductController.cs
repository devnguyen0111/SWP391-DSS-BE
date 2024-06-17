
using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Products;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICoverMetaltypeService _coverMetaltypeService;
        private readonly ISizeService _sizeService;
        private readonly IMetaltypeService _metaltypeService;

        public ProductController(IProductService productService,ICoverMetaltypeService c,ISizeService s,IMetaltypeService mt)
        {
            _productService = productService;
            _coverMetaltypeService = c;
            _sizeService = s;
            _metaltypeService = mt;
        }

        [HttpGet("products")]
        public ActionResult<List<ProductRequest>> GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            var productRequest = products.Select(c =>
            {
                return new ProductRequest
                {
                    ProductId = c.ProductId,
                    ProductName = c.ProductName,
                    UnitPrice = c.UnitPrice
                };
            }).ToList();
            return Ok(productRequest);
        }

        [HttpGet("productDetail/{id}")]
        public IActionResult GetProductDetailById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            //product.Cover.CoverName + product.Diamond.DiamondName
            var productDetail = new ProductDetail
            {
                ProductId = product.ProductId,
                imgUrl = "https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/illustration-gallery-icon_53876-27002.avif?alt=media&token=037e0d50-90ce-4dd4-87fc-f54dd3dfd567",
                ProductName = product.ProductName,
                DiamondName = product.Diamond.DiamondName,
                CoverName = product.Cover.CoverName,
                MetaltypeName = product.Metaltype.MetaltypeName,
                SizeName = product.Cover.CoverName,
                Pp = product.Pp,
                UnitPrice = product.UnitPrice + product.Size.SizePrice + product.Diamond.Price + product.Cover.UnitPrice + product.Metaltype.MetaltypePrice + product.Diamond.Price + product.Cover.UnitPrice + product.Metaltype.MetaltypePrice,
            };
            
            return Ok(productDetail);
        }
        [HttpGet("getMostSaleProduct")]
        public IActionResult getMostSaleProducrByCate(int count,string cate)
        {
            var products = _productService.getMostSaleProduct(count, cate);
            var productRequest = products.Select(c =>
            {
                return new ProductRequest
                {
                    ProductId = c.ProductId,
                    imgUrl = _coverMetaltypeService.GetCoverMetaltype(_productService.GetProductById(c.ProductId).CoverId, _productService.GetProductById(c.ProductId).MetaltypeId).ImgUrl,
                    ProductName = _productService.GetProductById(c.ProductId).ProductName,
                    UnitPrice =(decimal) _productService.GetProductById(c.ProductId).UnitPrice+
                    _productService.GetProductById(c.ProductId).Diamond.Price+ _productService.GetProductById(c.ProductId).Cover.UnitPrice+
                    _productService.GetProductById(c.ProductId).Size.SizePrice+ _productService.GetProductById(c.ProductId).Metaltype.MetaltypePrice,
                };
            }).ToList();
            return Ok(productRequest);
        }
        [HttpGet("GetFilteredProducts")]
        public IActionResult GetFilteredProducts(
        [FromQuery] int? categoryId,
        [FromQuery] int? subCategoryId,
        [FromQuery] int? metaltypeId,
        [FromQuery] int? sizeId,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,[FromQuery] string? sortOrder,
        [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var filteredProducts = _productService.FilterProducts(
                categoryId,
                subCategoryId,
                metaltypeId,
                sizeId,
                minPrice,
                maxPrice,sortOrder,pageNumber,pageSize).Select(c =>
                {
                    return new ProductRequest
                    {
                        ProductId = c.ProductId,
                        imgUrl = "https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/illustration-gallery-icon_53876-27002.avif?alt=media&token=037e0d50-90ce-4dd4-87fc-f54dd3dfd567"
                        ,
                        ProductName = c.ProductName,
                        UnitPrice = _productService.GetProductTotal(c.ProductId),
                    };
                }).ToList(); ;

            return Ok(filteredProducts);
        }
        [HttpGet("getFilterOption")]
        public IActionResult getFilterOption(string category)
        {
            var o = _metaltypeService.GetAllMetaltypes().Select(mt =>
            {
                return new metaltypeFilter
                {
                    value = mt.MetaltypeName,
                };
            }).ToList();
            var s = _sizeService.GetAllSizes().Where(c => c.SizeName.Contains(category)).Select(s =>
            {
                return new sizeFilter
                {
                    value = s.SizeValue,
                };
            }).ToList();
            return Ok(new {o,s});;
        }
    }
}

