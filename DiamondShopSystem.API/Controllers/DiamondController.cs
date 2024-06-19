
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Services.Diamonds;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiamondController : ControllerBase
    {
        private readonly IDiamondService _diamondService;

        public DiamondController(IDiamondService diamondService)
        {
            _diamondService = diamondService;
        }

/*        [HttpGet("diamonds")]
        public ActionResult<IEnumerable<Diamond>> GetDiamonds()
        {
            return _diamondService.GetAllDiamonds();
        }*/

        [HttpGet("{id}")]
        public ActionResult<Diamond> GetDiamond(int id)
        {
            var diamond = _diamondService.GetDiamondById(id);

            if (diamond == null)
            {
                return NotFound();
            }

            return diamond;
        }

       /*[HttpPost]
        public ActionResult<Diamond> PostDiamomnd([FromBody] DTO.DiamondRequest diamond)
        {
            var newDiamond = new Diamond(diamond.DiamondName, diamond.CaratWeight, diamond.Color, diamond.Clarity, diamond.Cut, diamond.Shape, diamond.Price);

           _diamondService.AddDiamond(newDiamond);
          return Ok(newDiamond);
       }*/

/*        [HttpPut("{id}")]
        public IActionResult PutDiamond(int id, [FromBody] Diamond diamondDTO)
        {
            if (id != diamondDTO.DiamondId)
            {
                return BadRequest();
            }
            var newDiamond = new Diamond(diamondDTO.DiamondName, diamondDTO.CaratWeight, diamondDTO.Color, diamondDTO.Clarity, diamondDTO.Cut, diamondDTO.Shape, diamondDTO.Price);

            try
            {
                _diamondService.UpdateDiamond(newDiamond);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_diamondService.GetDiamondById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        [HttpDelete("{id}")]
        public IActionResult DeleteDiamond(int id)
        {
            var diamond = _diamondService.GetDiamondById(id);
            if (diamond == null)
            {
                return NotFound();
            }

            _diamondService.DeleteDiamond(id);

            return NoContent();
        }
        [HttpGet("getDiamondDetail")]
        public IActionResult getDiamondDetail(int id)
        {
            var diamond = _diamondService.GetDiamondById(id);
            if (!diamond.Equals(null))
            {
                return Ok(diamond);
            }
            return BadRequest("Diamond not exist");
        }
        [HttpGet]
        public IActionResult GetDiamonds(
            [FromQuery] string sortBy,
            [FromQuery] List<string>? clarityRange,
            [FromQuery] List<string>? colorRange,
            [FromQuery] List<string>? cutRange,
            [FromQuery] decimal? minCaratWeight,
            [FromQuery] decimal? maxCaratWeight,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string?sortOrder,
            [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            // Define custom sorting orders
            Dictionary<string, int> colorOrder = new Dictionary<string, int>
            {
                {"D", 1}, {"E", 2}, {"F", 3}, {"G", 4}, {"H", 5}, {"I", 6}, {"J", 7}, {"K", 8}
            };

            Dictionary<string, int> clarityOrder = new Dictionary<string, int>
            {
                {"FL", 1}, {"IF", 2}, {"VVS1", 3}, {"VVS2", 4}, {"VS1", 5}, {"VS2", 6}, {"SI1", 7}, {"SI2", 8}
            };

            Dictionary<string, int> cutOrder = new Dictionary<string, int>
            {
                {"Astor Ideal", 1}, {"Ideal", 2}, {"VeryGood", 3}, {"Good", 4}
            };

            // Filter diamonds based on the provided criteria
            IEnumerable<Diamond> filteredDiamonds = _diamondService.GetAllDiamonds();

            if (clarityRange != null && clarityRange.Any())
            {
                filteredDiamonds = filteredDiamonds.Where(d => clarityRange.Contains(d.Clarity));
            }

            if (colorRange != null && colorRange.Any())
            {
                filteredDiamonds = filteredDiamonds.Where(d => colorRange.Contains(d.Color));
            }

            if (cutRange != null && cutRange.Any())
            {
                filteredDiamonds = filteredDiamonds.Where(d => cutRange.Contains(d.Cut));
            }

            if (minCaratWeight.HasValue)
            {
                filteredDiamonds = filteredDiamonds.Where(d => d.CaratWeight >= minCaratWeight.Value);
            }

            if (maxCaratWeight.HasValue)
            {
                filteredDiamonds = filteredDiamonds.Where(d => d.CaratWeight <= maxCaratWeight.Value);
            }

            if (minPrice.HasValue)
            {
                filteredDiamonds = filteredDiamonds.Where(d => d.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filteredDiamonds = filteredDiamonds.Where(d => d.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrEmpty(sortOrder))
            {
                if (sortOrder.ToLower() == "desc")
                {
                    filteredDiamonds = filteredDiamonds.OrderByDescending(d => d.Price);
                }
                else
                {
                    filteredDiamonds = filteredDiamonds.OrderBy(d => d.Price);
                }
            }
            // Sort the list of diamonds
            var sortedDiamonds = filteredDiamonds.Where(c =>c.Shape==sortBy).ToList();
            var paginatedDiamonds = sortedDiamonds
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();

            return Ok(paginatedDiamonds);
        }
    }
}

