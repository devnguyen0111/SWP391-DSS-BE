//using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Model.Models;
using Microsoft.EntityFrameworkCore;
using DAO;

namespace Repository
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
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }
    }
}
