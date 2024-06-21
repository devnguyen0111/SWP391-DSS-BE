using Model.Models;
using DAO;
using Microsoft.EntityFrameworkCore;

namespace Repository.Charge
{
    public class VnPayRepository : IVnPayRepository
    {
        private readonly DIAMOND_DBContext _context;

        public VnPayRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public void SaveOrder(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }
    }
}
