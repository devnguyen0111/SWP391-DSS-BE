using Model.Models;
using Repository;

namespace Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        
        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public Voucher AddVoucher(Voucher voucher)
        {
            return _voucherRepository.Add(voucher);
        }

        public void DeleteVoucher(int Id)
        {
            _voucherRepository.Delete(Id);
        }

        public List<Voucher> GetAllVouchers()
        {
            return _voucherRepository.GetAllVouchers();
        }

        public Voucher GetVoucherById(int Id)
        {
            return _voucherRepository.GetById(Id);
        }

        public Voucher UpdateVoucher(Voucher voucher)
        {
            return _voucherRepository.Update(voucher);
        }

        public Voucher CheckExpDate(string name)
        {
            return _voucherRepository.CheckExpDate(name);
        }

    }
}
