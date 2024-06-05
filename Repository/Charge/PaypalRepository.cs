using Model.Models;
using DAO;
using Microsoft.EntityFrameworkCore;

namespace Repository.Charge
{
    public class PaypalRepository : IPaypalRepository
    {
        private readonly DIAMOND_DBContext _context;

        public PaypalRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var OrderId = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            Console.WriteLine("OrderId : " + OrderId.OrderId);
            return OrderId;
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            order.Status = status;
            await _context.SaveChangesAsync();
        }
    }
}
