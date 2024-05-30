using Microsoft.AspNetCore.Mvc;
using Services;

namespace DiamondShopSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : Controller
    {

        private readonly CalculatorService _calculatorService;

        public CalculatorController(CalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        /*[HttpGet("{id}/price")]
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
        }*/


        [HttpGet("sellingprice")]
        public async Task<IActionResult> SellingPrice(int diamondId, int coverId, string voucherName = null)
        {
            try
            {
                decimal SellingPrice = await _calculatorService.CalculateSellingPriceAsync(diamondId, coverId, voucherName);
                if (voucherName == null)
                {
                    return Ok(new
                    {
                        DiamondID = diamondId,
                        CoverID = coverId,
                        VoucherName = "No Voucher",
                        VoucherEXP = false,
                        VoucherNote = "No Voucher",
                        sellingPrice = SellingPrice
                    });
                }
                else
                {
                    int voucherEXP = _calculatorService.CheckVoucherValid(voucherName);
                    switch (voucherEXP)
                    {
                        case 0:
                            return Ok(new
                            {
                                DiamondID = diamondId,
                                CoverID = coverId,
                                VoucherName = voucherName,
                                VoucherCode = voucherEXP,
                                VoucherNote = "Voucher not found",
                                sellingPrice = SellingPrice
                            });
                        case 1:
                            return Ok(new
                            {
                                DiamondID = diamondId,
                                CoverID = coverId,
                                VoucherName = voucherName,
                                VoucherCode = voucherEXP,
                                VoucherNote = "Voucher valid",
                                sellingPrice = SellingPrice
                            });
                        case 2:
                            return Ok(new
                            {
                                DiamondID = diamondId,
                                CoverID = coverId,
                                VoucherName = voucherName,
                                VoucherCode = voucherEXP,
                                VoucherNote = "Voucher expired",
                                sellingPrice = SellingPrice
                            });
                        case 3:
                            return Ok(new
                            {
                                DiamondID = diamondId,
                                CoverID = coverId,
                                VoucherName = voucherName,
                                VoucherCode = voucherEXP,
                                VoucherNote = "Voucher dont have customer",
                                sellingPrice = SellingPrice
                            });
                    }
                    
                    return Ok(new
                    {
                        DiamondID = diamondId,
                        CoverID = coverId,
                        VoucherName = voucherName,
                        VoucherCode = voucherEXP,
                        VoucherNote = "....",
                        sellingPrice = SellingPrice
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

