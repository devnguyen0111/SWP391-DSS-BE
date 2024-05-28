using Microsoft.AspNetCore.Mvc;
using Services;

namespace DiamondShopSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        
        private readonly CalculatorService _calculatorService;

        public ProductsController(CalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpGet("{id}/price")]
        public async Task<IActionResult> GetProductPrice(int id, [FromQuery] decimal markupRate)
        {
            try
            {
                decimal sellingPrice = await _calculatorService.CalculateSellingPrice(id, markupRate);
                return Ok(new { ProductId = id, SellingPrice = sellingPrice });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

