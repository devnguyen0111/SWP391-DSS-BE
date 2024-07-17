using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Utility;
using Services.Products;
using Services.Diamonds;

namespace DiamondShopSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IProductService _productService;
        private readonly ICoverMetaltypeService _coverMetaltypeService;

        public SearchController(ISearchService searchService, IProductService productService, ICoverMetaltypeService coverMetaltypeService)
        {
            _searchService = searchService;
            _productService = productService;
            _coverMetaltypeService = coverMetaltypeService;
        }

        [HttpGet("SearchAll")]
        public IActionResult searchAll([FromQuery] string search)
        {
            object result = null;
            if (!string.IsNullOrEmpty(search))
            {
                result =  _searchService.Search(search);
            }

            if (result == null)
            {
                return NotFound("No results found.");
            }
            return Ok(result);
        }

        [HttpGet("SearchProduct")]
        public async Task<IActionResult> seachProduct([FromQuery] string search)
        {
            object result = null;
            if (!string.IsNullOrEmpty(search))
            {
                result = await _searchService.SearchProduct(search);
            }

            if (result == null)
            {
                return NotFound("No results product found.");
            }
            return Ok(result);
        }

        [HttpGet("SearchDiamond")]

        public IActionResult searchDiamond([FromQuery] string search)
        {
            object result = null;
            if (!string.IsNullOrEmpty(search))
            {
                result = _searchService.SearchDiamond(search);
            }

            if (result == null)
            {
                return NotFound("No results diamond found.");
            }
            return Ok(result);
        }

        [HttpGet("SearchProductNameWithFilters")]
        public IActionResult SearchProductNameWithFilters(
    [FromQuery] string search,
    [FromQuery] int? categoryId,
    [FromQuery] int? subCategoryId,
    [FromQuery] int? metaltypeId,
    [FromQuery] int? sizeId,
    [FromQuery] decimal? minPrice,
    [FromQuery] decimal? maxPrice,
    [FromQuery] string? sortOrder,
    [FromQuery] int? pageNumber,
    [FromQuery] int? pageSize,
    [FromQuery] List<int>? sizeIds,
    [FromQuery] List<int>? metaltypeIds,
    [FromQuery] List<string>? diamondShapes)
        {
            if (string.IsNullOrEmpty(search))
            {
                return BadRequest("Search term is required.");
            }

            var filteredProducts = _productService.FilterProductsAd(
                categoryId,
                subCategoryId,
                metaltypeId,
                sizeId,
                minPrice,
                maxPrice,
                sortOrder,
                sizeIds,
                metaltypeIds,
                diamondShapes,
                pageNumber,
                pageSize)
                .Where(p => p.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase))
                .Select(c =>
                {
                    return new ProductRequest
                    {
                        ProductId = c.ProductId,
                        imgUrl = _coverMetaltypeService.GetCoverMetaltype(c.CoverId, c.MetaltypeId).ImgUrl,
                        ProductName = c.ProductName,
                        UnitPrice = _productService.GetProductTotal(c.ProductId),
                    };
                }).ToList();

            if (!filteredProducts.Any())
            {
                return NotFound("No products found matching your criteria.");
            }

            return Ok(filteredProducts);
        }

    }
}
