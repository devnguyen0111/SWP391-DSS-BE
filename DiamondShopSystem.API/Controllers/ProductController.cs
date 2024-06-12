
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

        public ProductController(IProductService productService)
        {
            _productService = productService;
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
            var productDetail = new ProductDetail
            {
                ProductId = product.ProductId,
                imgUrl = "hihi",
                ProductName = product.Cover.CoverName + product.Diamond.DiamondName,
                DiamondName = product.Diamond.DiamondName,
                CoverName = product.Cover.CoverName,
                MetaltypeName = product.Metaltype.MetaltypeName,
                SizeName = product.Cover.CoverName,
                Pp = product.Pp,
                UnitPrice = product.UnitPrice + product.Size.SizePrice + product.Diamond.Price + product.Cover.UnitPrice + product.Metaltype.MetaltypePrice + product.Diamond.Price + product.Cover.UnitPrice + product.Metaltype.MetaltypePrice,
            };
            if (product == null)
            {
                return NotFound();
            }
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
                    imgUrl = "hehe",
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
        [FromQuery] decimal? maxPrice)
        {
            var filteredProducts = _productService.FilterProducts(
                categoryId,
                subCategoryId,
                metaltypeId,
                sizeId,
                minPrice,
                maxPrice).Select(c =>
                {
                    return new ProductRequest
                    {
                        ProductId = c.ProductId,
                        imgUrl = "hehe",
                        ProductName = c.ProductName,
                        UnitPrice = c.UnitPrice+c.Size.SizePrice+c.Diamond.Price+c.Cover.UnitPrice+c.Metaltype.MetaltypePrice,
                    };
                }).ToList(); ;

            return Ok(filteredProducts);
        }
    }
}

