using Microsoft.AspNetCore.Mvc;
using Services.Products;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverSizeController : ControllerBase
    {
        private readonly ICoverSizeService _coverSizeService;

        public CoverSizeController(ICoverSizeService coverSizeService)
        {
            _coverSizeService = coverSizeService;
        }

        // GET: api/CoverSize/{coverId}/{sizeId}
        [HttpGet("{coverId}/{sizeId}")]
        public IActionResult GetCoverSize(int coverId, int sizeId)
        {
            var coverSize = _coverSizeService.GetCoverSize(coverId, sizeId);

            if (coverSize == null)
            {
                return NotFound();
            }

            return Ok(coverSize);
        }
    }
}
