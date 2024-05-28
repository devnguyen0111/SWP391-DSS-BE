using Model.Models;

namespace Services
{
    public interface IVoucherService
    {
        Voucher AddVoucher(Voucher voucher);
        void DeleteVoucher(int id);
        List<Voucher> GetAllVouchers();
        Voucher GetVoucherById(int id);
        Voucher UpdateVoucher(Voucher voucher);
        Voucher CheckExpDate(string name);
    }
}
