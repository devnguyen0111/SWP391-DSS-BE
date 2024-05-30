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

        public Voucher createVoucher(Voucher voucher)
        {
            return _voucherRepository.createVoucher(voucher);
        }

        public void deleteVoucher(int Id)
        {
            _voucherRepository.deleteVoucher(Id);
        }

        public List<Voucher> GetAllVouchers()
        {
            return _voucherRepository.getAllVouchers();
        }

        public Voucher getVoucherById(int Id)
        {
            return _voucherRepository.getVoucherById(Id);
        }

        public Voucher updateVoucher(Voucher voucher)
        {
            return _voucherRepository.updateVoucher(voucher);
        }
    }
}
