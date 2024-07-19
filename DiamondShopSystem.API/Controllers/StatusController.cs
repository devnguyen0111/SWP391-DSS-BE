using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.Models;
using Services.Diamonds;
using Services.Products;
using Services.Utility;
using System;
using System.ComponentModel;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICoverMetaltypeService _coverMetaltypeService;
        private readonly ISizeService _sizeService;
        private readonly IMetaltypeService _metaltypeService;
        private readonly ICoverService _coverService;
        private readonly IDiamondService _diamondService;
        private readonly IDisableService _statusService;

        public StatusController(IProductService productService, ICoverMetaltypeService coverMetaltypeService, ISizeService sizeService, IMetaltypeService metaltypeService, ICoverService coverService, IDiamondService diamondService, IDisableService disableService)
        {
            _productService = productService;
            _coverMetaltypeService = coverMetaltypeService;
            _sizeService = sizeService;
            _metaltypeService = metaltypeService;
            _coverService = coverService;
            _diamondService = diamondService;
            _statusService = disableService;
        }
        [HttpGet("checkChangeability")]
        public IActionResult getFact(string what,int id,int? subid)
        {
            switch (what.ToUpper())
            {
                case "PRODUCT":
                    var product = _productService.GetProductById(id);
                    if (product == null) return NotFound("Product not found");
                     var checker1 = _statusService.CanChangeProductStatus(product);
                    return Ok(new { checker1.Reason, checker1.CanChange });

                case "COVER":
                    var cover = _coverService.GetCoverById(id);
                    if (cover == null) return NotFound("Cover not found");
                    var checker3 = _statusService.CanChangeCoverStatus(cover);
                    return Ok(new { checker3.Reason, checker3.CanChange });

                case "SIZE":
                    var checker4 = _statusService.CanChangeSizeStatus(id);
                    return Ok(new { checker4.Reason, checker4.CanChange });

                case "METALTYPE":
                    var checker2 = _statusService.CanChangeMetalTypeStatus(id);
                    return Ok(new {checker2.Reason,checker2.CanChange});

                case "COVERMETALTYPE":
                    if (subid == null) return BadRequest("Subid is required for COVERMETALTYPE");
                    var checker5 = _statusService.CanChangeCoverMetalTypeStatus(id, (int)subid);
                    return Ok(new { checker5.Reason, checker5.CanChange });

                case "COVERSIZE":
                    if (subid == null) return BadRequest("Subid is required for COVERSIZE");
                    var checker6 = _statusService.CanChangeCoverSizeStatus(id, (int)subid);
                    return Ok(new { checker6.Reason, checker6.CanChange });
                case "DIAMOND":
                    var checker7 = _statusService.CanChangeDiamondStatus(_diamondService.GetDiamondById(id));
                    return Ok(new { checker7.Reason, checker7.CanChange });
                default:
                    return BadRequest("Invalid type specified");
            }
        }
        [HttpPut("UpdateStatusAdvanced")]
        public IActionResult update(string what,int id,int? subId,string? newStatus)
        {
            switch (what.ToUpper())
            {
                case "PRODUCT":
                    _statusService.UpdateProductStatus(_productService.GetProductById(id));
                    return Ok();
                case "COVER":
                    _statusService.UpdateCoverStatus(_coverService.GetCoverById(id));
                    return Ok();
                case "SIZE":
                    _statusService.UpdateSizeStatus(id, newStatus);
                    return Ok();
                case "METALTYPE":
                    _statusService.UpdateMetalTypeStatus(id, newStatus);
                    return Ok();
                case "COVERMETALTYPE":
                    _statusService.UpdateCoverMetalTypeStatus(id, (int)subId, newStatus);
                    return Ok();
                case "COVERSIZE":
                    _statusService.UpdateCoverSizeStatus(id, (int)subId, newStatus);
                    return Ok();
                case "DIAMOND":
                    _statusService.UpdateDiamondStatus(id, newStatus);
                    return Ok();
            }
            return Ok();
        }
    }
}
