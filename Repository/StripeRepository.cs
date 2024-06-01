using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Model.Models;

namespace Repository
{
    public class StripeRepository : IStripeRepository
    {
        private readonly DIAMOND_DBContext _context;

        public StripeRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            order.Status = status;
            _context.SaveChanges();
        }
    }
}
