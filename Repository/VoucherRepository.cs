using DAO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        //Dependency Injection.You can find this in program.cs
        private readonly DIAMOND_DBContext _context;

        public VoucherRepository(DIAMOND_DBContext context)
        {
            this._context = context;
        }

        public Voucher Add(Voucher entity)
        {
            _context.Vouchers.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            Voucher voucher = GetById(id);
            if (voucher != null)
            {
                _context.Vouchers.Remove(voucher);
                _context.SaveChanges();
            }
        }

        public List<Voucher> GetAllVouchers()
        {
            return _context.Vouchers.ToList();
        }

        public Voucher GetById(int id)
        {
            return _context.Vouchers.FirstOrDefault(c => c.VoucherId == id);
        }

        public Voucher Update(Voucher entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public Voucher CheckExpDate(string name)
        {
            Voucher voucher = _context.Vouchers.FirstOrDefault(c => c.Name == name);
            bool isExpired = false;
            if (voucher != null && voucher.ExpDate < DateOnly.FromDateTime(DateTime.Now))
            {
                // Expired
                isExpired = true;
            }
            else
            {
                // Not expired
                isExpired = false;
            }
           return isExpired ? null : voucher;
        }
    }
}
