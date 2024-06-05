using Model.Models;
using Repository.Utility;

namespace Services.Utility
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public void createVoucher(string name, string description, DateOnly expdate, int quantity, int rate, int cusId)
        {
            _voucherRepository.createVoucher(name, description, expdate, quantity, rate, cusId);
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
