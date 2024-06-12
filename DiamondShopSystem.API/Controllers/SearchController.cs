using Microsoft.AspNetCore.Mvc;
using Services.Utility;

namespace DiamondShopSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
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
    }
}
