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

        public string createRandomNameVoucher()
        {
            return _voucherRepository.createRandomNameVoucher();
        }

        public void createVoucher(string name, string description, DateOnly expdate, int quantity, int rate,decimal bottom,decimal top)
        {
            _voucherRepository.createVoucher(name, description, expdate, quantity, rate,top,bottom);
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
        public Voucher GetVoucherByName(string name)
        {
            return _voucherRepository.getAllVouchers().FirstOrDefault(v => v.Name == name);
        }
        public Voucher updateVoucher(Voucher voucher)
        {
            return _voucherRepository.updateVoucher(voucher);
        }
    }
}
