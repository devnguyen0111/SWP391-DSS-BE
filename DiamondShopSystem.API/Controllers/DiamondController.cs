
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

        [HttpGet]
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

        

        [HttpPut("{id}")]
        public IActionResult PutDiamond(int id, Diamond diamond)
        {
            if (id != diamond.DiamondId)
            {
                return BadRequest();
            }

            try
            {
                _diamondService.UpdateDiamond(diamond);
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

