using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Services;
//using Repository;

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
        [HttpGet]
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
        public ActionResult<Voucher> PostVoucher([FromBody] Voucher voucher)
        {
            if(voucher == null)
            {
                return BadRequest();
            }
            _voucherService.createVoucher(voucher);
            return Ok(voucher);
        }

        // PUT: api/Voucher?id=5 | update voucher by id
        [HttpPut("{id}")]
        public IActionResult PutVoucher(int id, Voucher voucher)
        {
            if (id != voucher.VoucherId)
            {
                return BadRequest();
            }
            try
            {
                _voucherService.updateVoucher(voucher);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_voucherService.getVoucherById(id) == null)
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
