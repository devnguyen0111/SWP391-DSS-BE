
using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Services;

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

        [HttpGet("diamonds")]
        public ActionResult<IEnumerable<Diamond>> GetDiamonds()
        {
            return _diamondService.GetAllDiamonds();
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

       [HttpPost]
        public ActionResult<Diamond> PostDiamomnd([FromBody] DTO.DiamondRequest diamond)
        {
            var newDiamond = new Diamond(diamond.DiamondName, diamond.CaratWeight, diamond.Color, diamond.Clarity, diamond.Cut, diamond.Shape, diamond.Price);

           _diamondService.AddDiamond(newDiamond);
          return Ok(newDiamond);
       }

        [HttpPut("{id}")]
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
    }
}

