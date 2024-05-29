using Model.Models;
namespace Services
{
    public interface IVoucherService
    {
        Voucher createVoucher(Voucher voucher);
        void deleteVoucher(int Id);
        List<Voucher> GetAllVouchers();
        Voucher getVoucherById(int Id);
        Voucher updateVoucher(Voucher voucher);
    }
}
