using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace DAO
{
    public class VoucherDAO
    {
        private readonly DIAMOND_DBContext _context;

        public VoucherDAO(DIAMOND_DBContext context)
        {
            _context = context;
        }

        // Method to retrieve all vouchers
        public List<Voucher> GetVouchers()
        {
            return _context.Vouchers.ToList();
        }

        // Method to retrieve a voucher by ID
        public Voucher GetVoucherById(int id)
        {
            return _context.Vouchers.FirstOrDefault(v => v.VoucherId == id);
        }

        // Method to add a new voucher
        public Voucher AddVoucher(Voucher voucher)
        {
            _context.Vouchers.Add(voucher);
            _context.SaveChanges();
            return voucher;
        }

        // Method to update an existing voucher
        public Voucher UpdateVoucher(Voucher voucher)
        {
            _context.Entry(voucher).State = EntityState.Modified;
            _context.SaveChanges();
            return voucher;
        }

        // Method to delete an existing voucher
        public void DeleteVoucher(int id)
        {
            Voucher voucher = _context.Vouchers.FirstOrDefault(v => v.VoucherId == id);
            _context.Vouchers.Remove(voucher);
            _context.SaveChanges();
        }
    }
}
