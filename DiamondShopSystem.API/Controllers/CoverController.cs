using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services;

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
    }
}
