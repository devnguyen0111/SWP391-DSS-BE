using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using MimeKit.IO;
using Model.Models;
using Services.Products;
using System.Reflection.Metadata.Ecma335;

namespace DiamondShopSystem.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CoverController : ControllerBase
    {
        private readonly ICoverService _coverService;
        private readonly ICoverMetaltypeService _coverMetaltypeService;
        private readonly ICoverSizeService _coverSizeService;
        private readonly IMetaltypeService _metaltypeService;
        private readonly ISizeService _sizeService;
        public CoverController(ICoverService coverService, ICoverMetaltypeService coverMetaltypeService, ICoverSizeService coverSizeService, IMetaltypeService metaltypeService, ISizeService sizeService)
        {
            _coverService = coverService;
            _coverMetaltypeService = coverMetaltypeService;
            _coverSizeService = coverSizeService;
            _metaltypeService = metaltypeService;
            _sizeService = sizeService;
        }

        [HttpGet("getAllCover")]
        public IActionResult GetAllCovers()
        {
            var covers = _coverService.GetAllCovers();
            return Ok(covers);
        }
        //forcustomer
        [HttpGet("getCoverDetail")]
        public IActionResult GetCoverById(int id)
        {
            var cover = _coverService.GetCoverById(id);
            if (cover == null)
            {
                return BadRequest("Cover does not exist");
            }
            List<CoverSize> sizes = _coverSizeService.GetCoverSizes(id);
            List<CoverMetaltype> metals = _coverMetaltypeService.GetCoverMetaltypes(id);
            List<CoverReponseMetal> c = (List<CoverReponseMetal>)metals.Select(metal => 
            {
                return new CoverReponseMetal
                {
                    metalId = metal.MetaltypeId,
                    name = _metaltypeService.GetMetaltypeById(metal.MetaltypeId).MetaltypeName,
                    url = metal.ImgUrl,
                    prize = (decimal)_metaltypeService.GetMetaltypeById(metal.MetaltypeId).MetaltypePrice
                };
            }).ToList();
            List<CoverResponeSize> s = (List<CoverResponeSize>)sizes.Select(size =>
            {
                return new CoverResponeSize
                {
                    sizeId = size.SizeId,
                    name = _sizeService.GetSizeById(size.SizeId).SizeValue,
                    prices = (decimal)_sizeService.GetSizeById(size.SizeId).SizePrice,
                };
            }).ToList();
            CoverResponse cr = new CoverResponse()
            {
                
                coverId = cover.CoverId,
                name = cover.CoverName,
                prices = (decimal)cover.UnitPrice,
                metals = (List<CoverReponseMetal>)c,
                sizes = (List<CoverResponeSize>)s
            };
            return Ok(cr);
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
        [Route("getAllCoverWithFilter")]
        public IActionResult GetCoversByFilter(
      [FromQuery] string? status,
      [FromQuery] int? categoryId,
      [FromQuery] int? subCategoryId,
      [FromQuery] string? sortOrder,
      [FromQuery] decimal? minUnitPrice,
      [FromQuery] decimal? maxUnitPrice,
      [FromQuery] int pageNumber,
      [FromQuery] int pageSize,
      [FromBody] FilterRequest r)
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

            if (r.MetaltypeIds != null && r.MetaltypeIds.Any())
            {
                filteredCovers = filteredCovers.Where(c => c.CoverMetaltypes.Any(cm => r.MetaltypeIds.Contains(cm.MetaltypeId)));
            }

            if (r.SizeIds != null && r.SizeIds.Any())
            {
                filteredCovers = filteredCovers.Where(c => c.CoverSizes.Any(cs => r.SizeIds.Contains(cs.SizeId)));
            }

            var filteredCovers1 = filteredCovers.Select(c =>
            {
                var firstCoverSize = c.CoverSizes.FirstOrDefault();
                var firstCoverMetaltype = c.CoverMetaltypes.FirstOrDefault();

                return new CoverResponse
                {
                    coverId = c.CoverId,
                    name = c.CoverName,
                    prices = (decimal)(c.UnitPrice +
                                       (firstCoverSize != null ? _sizeService.GetSizeById(firstCoverSize.SizeId).SizePrice : 0) +
                                       (firstCoverMetaltype != null ? _metaltypeService.GetMetaltypeById(firstCoverMetaltype.MetaltypeId).MetaltypePrice : 0)),
                    url = firstCoverMetaltype?.ImgUrl
                };
            });

            if (!string.IsNullOrEmpty(sortOrder))
            {
                filteredCovers1 = sortOrder.ToLower() == "desc" ?
                    filteredCovers1.OrderByDescending(d => d.prices) :
                    filteredCovers1.OrderBy(d => d.prices);
            }

            filteredCovers1 = filteredCovers1
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return Ok(filteredCovers1.ToList());
        }
    }
}
