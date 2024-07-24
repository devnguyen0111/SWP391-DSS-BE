
using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using Services.Diamonds;
using Services.Utility;

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

        [HttpGet("CountDiamond")]
        public IActionResult CountDiamond()
        {
            int count = _diamondService.GetAllDiamonds().Count();
            return Ok(count);
        }

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

        [HttpPost("AddDiamond")]
        public ActionResult<Diamond> PostDiamomnd([FromBody] DTO.DiamondRequest diamond)
        {
            var diamond1 = new Diamond
            {
                DiamondName = diamond.DiamondName,
                CaratWeight = diamond.CaratWeight,
                Clarity = diamond.Clarity,
                Color = diamond.Color,
                Cut = diamond.Cut,
                Price = diamond.Price,
                Shape = diamond.Shape,
                Status = "Available"
            };
            
            _diamondService.AddDiamond(diamond1);
            return Ok(diamond1);
        }

        [HttpPut("UpdateDiamond")]
        public IActionResult PutDiamond(int id, [FromBody] DiamondRequest diamond)
        {
            var diamond1 = new Diamond
            {
                DiamondId = id,
                DiamondName = diamond.DiamondName,
                CaratWeight = diamond.CaratWeight,
                Clarity = diamond.Clarity,
                Color = diamond.Color,
                Cut = diamond.Cut,
                Price = diamond.Price,
                Shape = diamond.Shape,
                Status = diamond.status,
            };
            try
            {
                _diamondService.UpdateDiamond(diamond1);
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

            return Ok(id);
        }

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
    [FromQuery] string? sortBy,
    [FromQuery] List<string>? clarityRange,
    [FromQuery] List<string>? colorRange,
    [FromQuery] List<string>? cutRange,
    [FromQuery] decimal? minCaratWeight,
    [FromQuery] decimal? maxCaratWeight,
    [FromQuery] decimal? minPrice,
    [FromQuery] decimal? maxPrice,
    [FromQuery] string? sortOrder,
    [FromQuery] int? diamondCode,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10
    )
        {
            var diamonds = _diamondService.GetAllDiamonds().Where(c => StringUltis.AreEqualIgnoreCase(c.Status, "Available"));

            // Filter by diamond code if provided
            if (diamondCode.HasValue)
            {
                var singleDiamond = diamonds.FirstOrDefault(d => d.DiamondId == diamondCode.Value);
                if (singleDiamond == null)
                {
                    return NotFound(new { Message = "Diamond with the given code not found." });
                }
                return Ok(new
                {
                    totalDiamonds = 1,
                    Diamonds = new List<Diamond> { singleDiamond }
                });
            }

            // Apply filters based on the provided criteria
            if (!string.IsNullOrEmpty(sortBy))
            {
                diamonds = diamonds.Where(d => StringUltis.AreEqualIgnoreCase(d.Shape, sortBy));
            }

            if (clarityRange != null && clarityRange.Any())
            {
                diamonds = diamonds.Where(d => clarityRange.Contains(d.Clarity));
            }

            if (colorRange != null && colorRange.Any())
            {
                diamonds = diamonds.Where(d => colorRange.Contains(d.Color));
            }

            if (cutRange != null && cutRange.Any())
            {
                diamonds = diamonds.Where(d => cutRange.Contains(d.Cut));
            }

            if (minCaratWeight.HasValue)
            {
                diamonds = diamonds.Where(d => d.CaratWeight >= minCaratWeight.Value);
            }

            if (maxCaratWeight.HasValue)
            {
                diamonds = diamonds.Where(d => d.CaratWeight <= maxCaratWeight.Value);
            }

            if (minPrice.HasValue)
            {
                diamonds = diamonds.Where(d => d.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                diamonds = diamonds.Where(d => d.Price <= maxPrice.Value);
            }

            int totalDiamond = diamonds.Count();

            // Sort the filtered diamonds based on price and order
            if (!string.IsNullOrEmpty(sortOrder) && sortOrder.ToLower() == "desc")
            {
                diamonds = diamonds.OrderByDescending(d => d.Price);
            }
            else
            {
                diamonds = diamonds.OrderBy(d => d.Price);
            }

            // Pagination
            var paginatedDiamonds = diamonds
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new
            {
                totalDiamond,
                Diamonds = paginatedDiamonds
            };

            return Ok(result);
        }

        [HttpGet("ForManager1")]
        public IActionResult GetDiamonds123(
    [FromQuery] string? sortBy,
    [FromQuery] List<string>? clarityRange,
    [FromQuery] List<string>? colorRange,
    [FromQuery] List<string>? cutRange,
    [FromQuery] decimal? minCaratWeight,
    [FromQuery] decimal? maxCaratWeight,
    [FromQuery] decimal? minPrice,
    [FromQuery] decimal? maxPrice,
    [FromQuery] string? sortOrder,
    [FromQuery] int? diamondCode,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10
    )
        {
            var diamonds = _diamondService.GetAllDiamonds().AsEnumerable();

            // Filter by diamond code if provided
            if (diamondCode.HasValue)
            {
                var singleDiamond = diamonds.FirstOrDefault(d => d.DiamondId == diamondCode.Value);
                if (singleDiamond == null)
                {
                    return NotFound(new { Message = "Diamond with the given code not found." });
                }
                return Ok(new
                {
                    totalDiamonds = 1,
                    Diamonds = new List<Diamond> { singleDiamond }
                });
            }

            // Apply filters based on the provided criteria
            if (!string.IsNullOrEmpty(sortBy))
            {
                diamonds = diamonds.Where(d => StringUltis.AreEqualIgnoreCase(d.Shape, sortBy));
            }

            if (clarityRange != null && clarityRange.Any())
            {
                diamonds = diamonds.Where(d => clarityRange.Contains(d.Clarity));
            }

            if (colorRange != null && colorRange.Any())
            {
                diamonds = diamonds.Where(d => colorRange.Contains(d.Color));
            }

            if (cutRange != null && cutRange.Any())
            {
                diamonds = diamonds.Where(d => cutRange.Contains(d.Cut));
            }

            if (minCaratWeight.HasValue)
            {
                diamonds = diamonds.Where(d => d.CaratWeight >= minCaratWeight.Value);
            }

            if (maxCaratWeight.HasValue)
            {
                diamonds = diamonds.Where(d => d.CaratWeight <= maxCaratWeight.Value);
            }

            if (minPrice.HasValue)
            {
                diamonds = diamonds.Where(d => d.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                diamonds = diamonds.Where(d => d.Price <= maxPrice.Value);
            }

            int totalDiamond = diamonds.Count();

            // Sort the filtered diamonds based on price and order
            if (!string.IsNullOrEmpty(sortOrder) && sortOrder.ToLower() == "desc")
            {
                diamonds = diamonds.OrderByDescending(d => d.Price);
            }
            else
            {
                diamonds = diamonds.OrderBy(d => d.Price);
            }

            // Pagination
            var paginatedDiamonds = diamonds
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new
            {
                totalDiamond,
                Diamonds = paginatedDiamonds
            };

            return Ok(result);
        }
        [HttpGet("ForManager")]
        public IActionResult GetDiamonds1(
    [FromQuery] string sortBy,
    [FromQuery] List<string>? clarityRange,
    [FromQuery] List<string>? colorRange,
    [FromQuery] List<string>? cutRange,
    [FromQuery] decimal? minCaratWeight,
    [FromQuery] decimal? maxCaratWeight,
    [FromQuery] decimal? minPrice,
    [FromQuery] decimal? maxPrice,
    [FromQuery] string? sortOrder,
    [FromQuery] int? diamondCode,
    [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            // Define custom sorting orders
            var diamonds = _diamondService.GetAllDiamonds().AsEnumerable();

            // Filter by diamond code if provided
            if (diamondCode.HasValue)
            {
                var singleDiamond = diamonds.FirstOrDefault(d => d.DiamondId == diamondCode.Value);
                if (singleDiamond == null)
                {
                    return NotFound(new { Message = "Diamond with the given code not found." });
                }
                return Ok(new
                {
                    totalDiamonds = 1,
                    Diamonds = new List<Diamond> { singleDiamond }
                });
            }

            // Apply filters based on the provided criteria
            if (!string.IsNullOrEmpty(sortBy))
            {
                diamonds = diamonds.Where(d => StringUltis.AreEqualIgnoreCase(d.Shape, sortBy));
            }

            if (clarityRange != null && clarityRange.Any())
            {
                diamonds = diamonds.Where(d => clarityRange.Contains(d.Clarity));
            }

            if (colorRange != null && colorRange.Any())
            {
                diamonds = diamonds.Where(d => colorRange.Contains(d.Color));
            }

            if (cutRange != null && cutRange.Any())
            {
                diamonds = diamonds.Where(d => cutRange.Contains(d.Cut));
            }

            if (minCaratWeight.HasValue)
            {
                diamonds = diamonds.Where(d => d.CaratWeight >= minCaratWeight.Value);
            }

            if (maxCaratWeight.HasValue)
            {
                diamonds = diamonds.Where(d => d.CaratWeight <= maxCaratWeight.Value);
            }

            if (minPrice.HasValue)
            {
                diamonds = diamonds.Where(d => d.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                diamonds = diamonds.Where(d => d.Price <= maxPrice.Value);
            }

            int totalDiamond = diamonds.Count();

            // Sort the filtered diamonds based on price and order
            if (!string.IsNullOrEmpty(sortOrder) && sortOrder.ToLower() == "desc")
            {
                diamonds = diamonds.OrderByDescending(d => d.Price);
            }
            else
            {
                diamonds = diamonds.OrderBy(d => d.Price);
            }

            // Pagination
            var paginatedDiamonds = diamonds
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new
            {
                totalDiamond,
                Diamonds = paginatedDiamonds
            };

            return Ok(result);
        }
    }
}

