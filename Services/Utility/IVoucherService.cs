using Model.Models;

namespace Services.Utility
{
    public interface IVoucherService
    {
        string createRandomNameVoucher();
        void createVoucher(string name, string description, DateOnly expdate, int quantity, int rate, decimal bottom, decimal top);       void deleteVoucher(int Id);
        List<Voucher> GetAllVouchers();
        Voucher getVoucherById(int Id);
        Voucher GetVoucherByName(string name);
        Voucher updateVoucher(Voucher voucher);
    }
}
