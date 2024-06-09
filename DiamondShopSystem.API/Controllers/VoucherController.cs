using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Utility;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        // GET: api/Voucher | get all vouchers
        [HttpGet("/get")]
        public ActionResult<IEnumerable<Voucher>> GetVouchers()
        {
            return _voucherService.GetAllVouchers();
        }

        // GET: api/Voucher?id=5 | get voucher by id
        [HttpGet("{id}")]
        public ActionResult<Voucher> GetVoucher(int id)
        {
            var voucher = _voucherService.getVoucherById(id);

            if (voucher == null)
            {
                return NotFound();
            }

            return voucher;
        }

        // POST: api/Voucher | add new voucher
        [HttpPost]
        public ActionResult<Voucher> PostVoucher([FromBody] DTO.VoucherRequest request)
        {
            int customerId = request.CusId == 0 ? 0 : request.CusId;

            _voucherService.createVoucher(_voucherService.createRandomNameVoucher(), request.Description, request.ExpDate, request.Quantity, request.Rate, customerId);
            return CreatedAtAction("GetVouchers", _voucherService.GetAllVouchers());
        }

        // PUT: api/Voucher?id=5 | update voucher by id
/*        [HttpPut("{id}")]
        public IActionResult PutVoucher(int id, [FromBody] DTO.VoucherRequest request)
        {
            var voucherId = _voucherService.getVoucherById(id);
            
        }*/

        // DELETE: api/Voucher?id=5 | delete voucher by id
        [HttpDelete("{id}")]
        public IActionResult DeleteVoucher(int id)
        {
            var voucher = _voucherService.getVoucherById(id);
            if (voucher == null)
            {
                return NotFound();
            }
            _voucherService.deleteVoucher(id);
            return NoContent();
        }
    }
}
