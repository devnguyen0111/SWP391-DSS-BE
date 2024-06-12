using Microsoft.AspNetCore.Mvc;
using MimeKit.IO;
using Model.Models;
using Services.Products;

namespace DiamondShopSystem.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CoverController : ControllerBase
    {
        private readonly ICoverService _coverService;

        public CoverController(ICoverService coverService)
        {
            _coverService = coverService;
        }

        [HttpGet]
        public IActionResult GetAllCovers()
        {
            var covers = _coverService.GetAllCovers();
            return Ok(covers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCoverById(int id)
        {
            var cover = _coverService.GetCoverById(id);
            if (cover == null)
            {
                return NotFound();
            }
            return Ok(cover);
        }

        [HttpPost]
        public IActionResult AddCover([FromBody] Cover cover)
        {
            if (cover == null)
            {
                return BadRequest();
            }
            _coverService.AddCover(cover);
            return CreatedAtAction(nameof(GetCoverById), new { id = cover.CoverId }, cover);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCover(int id, [FromBody] Cover cover)
        {
            if (cover == null || cover.CoverId != id)
            {
                return BadRequest();
            }
            var existingCover = _coverService.GetCoverById(id);
            if (existingCover == null)
            {
                return NotFound();
            }
            _coverService.UpdateCover(cover);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCover(int id)
        {
            var cover = _coverService.GetCoverById(id);
            if (cover == null)
            {
                return NotFound();
            }
            _coverService.DeleteCover(id);
            return NoContent();
        }

        [HttpGet]
        [Route("CoverFilter")]
        public IActionResult GetCoversByFilter(
            [FromQuery] string sortBy,
            [FromQuery] string status,
            [FromQuery] decimal? minUnitPrice,
            [FromQuery] decimal? maxUnitPrice,
            [FromQuery] int? categoryId,
            [FromQuery] int? subCategoryId)
        {
            // Fetch all covers
            IEnumerable<Cover> filteredCovers = _coverService.GetAllCovers();

            // Apply filters
            if (!string.IsNullOrEmpty(status))
            {
                filteredCovers = filteredCovers.Where(c => c.Status == status);
            }

            if (minUnitPrice.HasValue)
            {
                filteredCovers = filteredCovers.Where(c => c.UnitPrice >= minUnitPrice.Value);
            }

            if (maxUnitPrice.HasValue)
            {
                filteredCovers = filteredCovers.Where(c => c.UnitPrice <= maxUnitPrice.Value);
            }

            if (categoryId.HasValue)
            {
                filteredCovers = filteredCovers.Where(c => c.CategoryId == categoryId.Value);
            }

            if (subCategoryId.HasValue)
            {
                filteredCovers = filteredCovers.Where(c => c.SubCategoryId == subCategoryId.Value);
            }

            // Sort the list of covers
            var sortedDiamonds = filteredCovers.Where(c => c.CoverName == sortBy).ToList();

            return Ok(filteredCovers);
        }
    }
}
