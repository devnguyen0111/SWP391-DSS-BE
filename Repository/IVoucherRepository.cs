using Model.Models;

namespace Repository
{
    public interface IVoucherRepository
    {
        Voucher Add(Voucher voucher);
        void Delete(int id);
        List<Voucher> GetAllVouchers();
        Voucher GetById(int id);
        Voucher Update(Voucher voucher);
        Voucher CheckExpDate(string name);
    }
}
