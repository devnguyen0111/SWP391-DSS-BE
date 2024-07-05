using Model.Models;

namespace Repository.Utility
{
    public interface IVoucherRepository
    {
        string createRandomNameVoucher();
        void createVoucher(string name, string description, DateOnly expdate, int quantity, int rate, decimal topPrice, decimal bottomPrice);        List<Voucher> getAllVouchers();
        Voucher getVoucherById(int Id);
        void deleteVoucher(int Id);
        Voucher updateVoucher(Voucher voucher);
    }
}
