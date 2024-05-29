using Model.Models;

namespace Repository
{
    public interface IVoucherRepository
    {
        Voucher createVoucher(Voucher voucher);
        List<Voucher> getAllVouchers();
        Voucher getVoucherById(int Id);
        void deleteVoucher(int Id);
        Voucher updateVoucher(Voucher voucher);
    }
}
