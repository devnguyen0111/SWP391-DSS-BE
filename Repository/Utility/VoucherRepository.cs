using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Utility;

namespace Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly DIAMOND_DBContext _context;

        public VoucherRepository(DIAMOND_DBContext context)
        {
            this._context = context;
        }

        public string createRandomNameVoucher()
        {
            var patern = "CD-XXXXXX";
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var randomString = patern.Replace("XXXXXX", new string(Enumerable.Repeat(alphabet, 6).Select(s => s[random.Next(s.Length)]).ToArray()));
            Console.WriteLine(randomString);
            return randomString;
        }

        // Method to create a new voucher
        public void createVoucher( string name, string description, DateOnly expdate, int quantity, int rate)
        {
            // add new voucher to the database with the provided information (name, description, expDate, quantity, rate, cusId)
            Voucher voucher = new Voucher
            {
                Name = createRandomNameVoucher(),
                Description = description,
                ExpDate = expdate,
                Quantity = quantity,
                Rate = rate,
            };
            _context.Vouchers.Add(voucher);
            _context.SaveChanges();

        }

        // Method to retrieve all vouchers
        public List<Voucher> getAllVouchers()
        {
            // Fix rate int32 to decimal
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
