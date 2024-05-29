using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly DIAMOND_DBContext _context;

        public VoucherRepository(DIAMOND_DBContext context)
        {
            this._context = context;
        }

        // Method to create a new voucher
        public Voucher createVoucher(Voucher voucher)
        {
            _context.Vouchers.Add(voucher);
            _context.SaveChanges();
            return voucher;
        }

        // Method to retrieve all vouchers
        public List<Voucher> getAllVouchers()
        {
            return _context.Vouchers.ToList();
        }

        // Method to retrieve a voucher by ID
        public Voucher getVoucherById(int Id)
        {
            return _context.Vouchers.FirstOrDefault(v => v.VoucherId == Id);
        }

        // Method to delete a voucher by ID
        public void deleteVoucher(int Id)
        {
            Voucher voucher = getVoucherById(Id);
            if (voucher != null)
            {
                _context.Vouchers.Remove(voucher);
                _context.SaveChanges();
            }
        }

        // Method to update an existing voucher
        public Voucher updateVoucher(Voucher voucher)
        {
            _context.Entry(voucher).State = EntityState.Modified;
            _context.SaveChanges();
            return voucher;
        }


    }
}
