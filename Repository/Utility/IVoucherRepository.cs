using Model.Models;

namespace Repository.Utility
{
    public interface IVoucherRepository
    {
        void createVoucher(string name, string description, DateOnly expdate, int quantity, int rate, int cusId);
        List<Voucher> getAllVouchers();
        Voucher getVoucherById(int Id);
        void deleteVoucher(int Id);
        Voucher updateVoucher(Voucher voucher);
    }
}
