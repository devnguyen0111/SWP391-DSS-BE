using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Services.Products;
using Services.Utility;
using ShimSkiaSharp;
using System.Linq;

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

        [HttpGet("getAllCovers")]
        public ActionResult<IEnumerable<CoverDTO>> GetCovers()
        {
            var covers = _coverService.GetAllCovers();
            var coverDTOs = covers.Select(c => new CoverDTO
            {
                CoverId = c.CoverId,
                CoverName = c.CoverName,
                Status = _coverService.DetermineCoverStatus(c.CoverId),
                UnitPrice = c.UnitPrice,
                SubCategoryId = c.SubCategoryId,
                CategoryId = c.CategoryId,
            }).ToList();
            return Ok(coverDTOs);
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
                    prize = (decimal)_metaltypeService.GetMetaltypeById(metal.MetaltypeId).MetaltypePrice,
                    status = metal.Status,
                };
            }).ToList();
            List<CoverResponeSize> s = (List<CoverResponeSize>)sizes.Select(size =>
            {
                return new CoverResponeSize
                {
                    sizeId = size.SizeId,
                    name = _sizeService.GetSizeById(size.SizeId).SizeValue,
                    prices = (decimal)_sizeService.GetSizeById(size.SizeId).SizePrice,
                    status = size.Status,
                };
            }).ToList();
            CoverResponse cr = new CoverResponse()
            {
                
                coverId = cover.CoverId,
                categoryId =cover.CategoryId ,
                name = cover.CoverName,
                status = _coverService.DetermineCoverStatus(cover.CoverId), 
                prices = (decimal)cover.UnitPrice,
                metals = (List<CoverReponseMetal>)c,
                sizes = (List<CoverResponeSize>)s,
            };
            return Ok(cr);
        }

        [HttpPost("addCover")]
        public ActionResult<CoverDTO> PostCover(CoverUpdateDTO coverUpdateDto)
        {
            var covers = _coverService.GetAllCovers();
            if (covers.Any(c => StringUltis.AreEqualIgnoreCase(c.CoverName, coverUpdateDto.CoverName)))
            {
                string name = coverUpdateDto.CoverName;
                return BadRequest(name + " already exist!");
            }
            var cover = new Cover
            {
                CoverName = coverUpdateDto.CoverName,
                Status = coverUpdateDto.Status,
                UnitPrice = coverUpdateDto.UnitPrice,
                CategoryId = (int)coverUpdateDto.Category,
                SubCategoryId = (int)coverUpdateDto.Category,
            };
            cover.CoverSizes = coverUpdateDto.CoverSizes.Select(cs => new CoverSize
            {
                SizeId = cs.SizeId,
                Status = cs.Status,
                CoverId = cover.CoverId,
            }).ToList();
            cover.CoverMetaltypes = coverUpdateDto.CoverMetaltypes.Select(cm => new CoverMetaltype
            {
                MetaltypeId = cm.MetaltypeId,
                Status = cm.Status,
                ImgUrl = cm.ImgUrl,
                CoverId = cover.CoverId,
            }).ToList();
            
            _coverService.AddCover(cover);
            return Ok(cover.CoverId);
        }
        [HttpPut("BeforeUpdateCover")]
        public IActionResult PutCoverItems(int id)
        {
            _coverService.EmptyCover(id);
            return Ok();
        }
        [HttpPut("UpdateCover")]
        public IActionResult PutCover(int id, CoverUpdateDTO coverUpdateDto)
        {
            var cover = _coverService.GetCoverById(id);

            if (cover == null)
            {
                return NotFound();
            }
            _coverService.EmptyCover(id);
            // Update the cover properties
            cover.CoverName = coverUpdateDto.CoverName;
            cover.Status = coverUpdateDto.Status;
            cover.UnitPrice = coverUpdateDto.UnitPrice;

            // Update CoverSizes
            cover.CoverSizes = new List<CoverSize>();
            foreach (var cs in coverUpdateDto.CoverSizes)
            {
                cover.CoverSizes.Add(new CoverSize
                {
                    SizeId = cs.SizeId,
                    CoverId = id,
                    Status = cs.Status
                }); ;
            }

            // Update CoverMetaltypes
            cover.CoverMetaltypes = new List<CoverMetaltype>();
            foreach (var cm in coverUpdateDto.CoverMetaltypes)
            {
                cover.CoverMetaltypes.Add(new CoverMetaltype
                {
                    MetaltypeId = cm.MetaltypeId,
                    CoverId =id,
                    Status = cm.Status,
                    ImgUrl = cm.ImgUrl
                });
            }

            // Check for duplicate cover names
            var covers = _coverService.GetAllCovers().Where(d => d.CoverId != cover.CoverId);
            if (covers.Any(c => StringUltis.AreEqualIgnoreCase(c.CoverName, coverUpdateDto.CoverName)))
            {
                string name = coverUpdateDto.CoverName;
                return BadRequest("The name " + name + " already exists!");
            }

            try
            {
                string status1 = _coverService.DetermineCoverStatus1(cover);
                cover.Status = status1;
                _coverService.UpdateCover(cover);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_coverService.GetCoverById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            string status = _coverService.DetermineCoverStatus(cover.CoverId);
            return Ok("Cover updated successfully!");
        }


        //[HttpPost("addCoverMetalType")]
        //public IActionResult AddCoverMetalType([FromBody] CoverMetaltypeUpdateDTO dto)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest("Update Failed");
        //    }
        //    Cover c = _coverService.GetCoverById(dto.coverId);
        //    if(c == null)
        //    {
        //        return BadRequest("CoverId not exist");
        //    }
        //    CoverMetaltype cmt = new CoverMetaltype
        //    {
        //        CoverId = dto.coverId,
        //        MetaltypeId = dto.MetaltypeId,
        //        ImgUrl = dto.ImgUrl,
        //    };
        //    _coverMetaltypeService.AddCoverMetalType(cmt);
        //    return Ok("Added successfully");
        //}
        [HttpPost("deleteCoverMetalType")]
        public IActionResult DeleteCoverMetalType([FromBody] CoverMetaltypeDeleteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Delete Failed");
            }
            Cover c = _coverService.GetCoverById(dto.coverId);
            if (c == null)
            {
                return BadRequest("CoverId not exist");
            }
            CoverMetaltype cmt = new CoverMetaltype
            {
                CoverId = dto.coverId,
                MetaltypeId = dto.MetaltypeId,
            };
            _coverMetaltypeService.RemoveCoverMetalType(cmt);
            return Ok("Deleted successfully");
        }
        [HttpPut("SwitchCoverStatus")]
        public IActionResult SwitchCoverStatus(int id)
        {
            var cover = _coverService.GetCoverById(id);
            if (cover == null)
            {
                return NotFound();
            }
            if (StringUltis.AreEqualIgnoreCase(cover.Status, "available"))
            {
                if (_coverService.DetermineCoverStatus(id)=="Unavailable")
                {
                    return BadRequest("No CoverSizes or(and) CoverMetalTypes are available");
                }
                cover.Status = "Disabled";
            }
            else
            {
                cover.Status = "Available";
            }
            return Ok(cover.Status);
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
      [FromQuery] int? pageNumber,
      [FromQuery] int ?pageSize,
    [FromQuery] List<int>? sizeIds,
    [FromQuery] List<int>? metaltypeIds)
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

            if (metaltypeIds != null && metaltypeIds.Any())
            {
                filteredCovers = filteredCovers.Where(c => c.CoverMetaltypes.Any(cm => metaltypeIds.Contains(cm.MetaltypeId)));
            }

            if (sizeIds != null && sizeIds.Any())
            {
                filteredCovers = filteredCovers.Where(c => c.CoverSizes.Any(cs => sizeIds.Contains(cs.SizeId)));
            }

            var filteredCovers1 = filteredCovers.Select(c =>
            {
                var firstCoverSize = c.CoverSizes.FirstOrDefault();
                var firstCoverMetaltype = c.CoverMetaltypes.FirstOrDefault();
                return new CoverResponse
                {
                    coverId = c.CoverId,
                    name = c.CoverName,
                    prices = (decimal)(c.UnitPrice),
                    url = firstCoverMetaltype?.ImgUrl
                };
            });

            if (!string.IsNullOrEmpty(sortOrder))
            {
                filteredCovers1 = sortOrder.ToLower() == "desc" ?
                    filteredCovers1.OrderByDescending(d => d.prices) :
                    filteredCovers1.OrderBy(d => d.prices);
            }
            if (pageNumber != null && pageSize != null)
            {
                filteredCovers1 = filteredCovers1
                    .Skip((int)((pageNumber - 1) * pageSize))
                    .Take((int)pageSize);
            }

            return Ok(filteredCovers1.ToList());
        }
        [HttpGet("testSize")]
        public IActionResult getAllSizeByCate(int id)
        {
            return Ok(_sizeService.getSizeByCate(id));
        }
        [HttpGet("testMetalTypes")]
        public IActionResult getAllMetalByCate(int id)
        {
            return Ok(_metaltypeService.getMetalTypeByCate(id));
        }
    }
}
