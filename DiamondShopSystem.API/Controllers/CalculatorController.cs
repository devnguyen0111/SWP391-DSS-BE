using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Utility;

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

        [HttpGet("Cal-Did-Cid-V")]
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
                            var mnv=  _calculatorService.MinusVoucherQuantity(voucherName);
                            return Ok(new
                            {
                                DiamondID = diamondId,
                                CoverID = coverId,
                                VoucherName = voucherName,
                                VoucherCode = voucherEXP,
                                VoucherNote = $"Voucher valid | {mnv}",
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

        [HttpGet("Cal-Pid-V")]
        public async Task<IActionResult> SellingPriceV2(int productId, string voucherName = null)
        {
            try
            {
                decimal sellingPrice = await _calculatorService.CalculateSellingProductWithVoucher(productId, voucherName);
                string voucherNote = "No Voucher";
                if (voucherName != null)
                {
                    int voucherStatus = _calculatorService.CheckVoucherValid(voucherName);
                    switch (voucherStatus)
                    {
                        case 1:
                            _calculatorService.MinusVoucherQuantity(voucherName);
                            voucherNote = "Voucher applied";
                            break;
                        case 2:
                            voucherNote = "Voucher expired";
                            break;
                        case 3:
                            voucherNote = "Voucher out of stock";
                            // sellingPrice is without voucher discount
                            sellingPrice = await _calculatorService.CalculateSellingProductWithVoucher(productId, null);
                            break;
                        default:
                            voucherNote = "Voucher status unknown";
                            break;
                    }
                }

                return Ok(new
                {
                    ProductId = productId,
                    VoucherName = voucherName ?? "No Voucher",
                    VoucherNote = voucherNote,
                    SellingPrice = sellingPrice
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

