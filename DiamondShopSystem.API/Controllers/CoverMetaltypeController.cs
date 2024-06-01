using Microsoft.AspNetCore.Mvc;
using Services;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverMetaltypeController : ControllerBase
    {
        private readonly ICoverMetaltypeService _coverMetaltypeService;

        public CoverMetaltypeController(ICoverMetaltypeService coverMetaltypeService)
        {
            _coverMetaltypeService = coverMetaltypeService;
        }

        // GET: api/CoverSize/{coverId}/{sizeId}
        [HttpGet("{coverId}/{metaltypeId}")]
        public IActionResult GetCoverMetaltype(int coverId, int metaltypeId)
        {
            var coverMetaltype = _coverMetaltypeService.GetCoverMetaltype(coverId, metaltypeId);

            if (coverMetaltype == null)
            {
                return NotFound();
            }

            return Ok(coverMetaltype);
        }
    }
}
