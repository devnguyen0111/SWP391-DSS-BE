using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Services;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : Controller
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        // GET: api/Voucher (Get all vouchers)
        [HttpGet]
        public ActionResult<IEnumerable<Voucher>> GetVouchers()
        {
            return _voucherService.GetAllVouchers();
        }

        // GET: api/Voucher?id=5 (Get voucher by id)
        [HttpGet("{id}")]
        public ActionResult<Voucher> GetVoucher(int id)
        {
            var voucher = _voucherService.GetVoucherById(id);

            if (voucher == null)
            {
                return NotFound();
            }

            return voucher;
        }


        // POST: api/Voucher (Add new voucher)
        [HttpPost]
        public ActionResult<Voucher> PostVoucher([FromBody] Voucher voucher)
        {
            _voucherService.AddVoucher(voucher);
            return Ok(voucher);
        }

        // PUT: api/Voucher?id=5 (Update voucher)
        [HttpPut("{id}")]
        public ActionResult<Voucher> PutVoucher(int id, [FromBody] Voucher voucher)
        {
            if (id != voucher.VoucherId)
            {
                return BadRequest();
            }

            try
            {
                _voucherService.UpdateVoucher(voucher);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_voucherService.GetVoucherById(id) == null
                    )
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

        // DELETE: api/Voucher?id=5 (Delete voucher)
        [HttpDelete("{id}")]
        public ActionResult<Voucher> DeleteVoucher(int id)
        {
            var voucher = _voucherService.GetVoucherById(id);
            if (voucher == null)
            {
                return NotFound();
            }

            _voucherService.DeleteVoucher(id);

            return voucher;
        }

        // GET: api/Voucher?name=abcxyz (Check if voucher is expired)
        [HttpGet("/check{name}")]
        public ActionResult<Voucher> CheckExpDate([FromQuery] string name)
        {
            var voucher = _voucherService.CheckExpDate(name);

            if (voucher == null)
            {
                return NotFound();
            }
            return voucher;
        }

    }
}
