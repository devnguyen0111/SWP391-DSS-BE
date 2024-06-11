
using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
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
                ProductName = product.Cover.CoverName + product.Diamond.DiamondName,
                DiamondName = product.Diamond.DiamondName,
                CoverName = product.Cover.CoverName,
                MetaltypeName = product.Metaltype.MetaltypeName,
                SizeName = product.Cover.CoverName,
                Pp = product.Pp,
                UnitPrice = product.UnitPrice,
            };
            if (product == null)
            {
                return NotFound();
            }
            return Ok(productDetail);
        }

    }
}

