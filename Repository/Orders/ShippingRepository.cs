using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Orders
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly DIAMOND_DBContext _context;

        public ShippingRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public async Task<Shipping> GetByIdAsync(int shippingId)
        {
            return await _context.Shippings.FindAsync(shippingId);
        }

        public async Task CreateAsync(Shipping shipping)
        {
            _context.Shippings.Add(shipping);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrdersBySaleStaffIdAndStatusAsync(int saleStaffId, string status)
        {
            var orders = await _context.Shippings
                .Where(s => s.SaleStaffId == saleStaffId && s.Status == status)
                .Select(s => s.Order)
                .ToListAsync();

            return orders;
        }

        public async Task<Order> GetOrderByOrderIdAsync(int orderId)
        {
            var shipping = await _context.Shippings
                .FirstOrDefaultAsync(s => s.OrderId == orderId);

            return shipping?.Order; // Assuming Order is a navigation property in Shipping
        }

        public async Task AssignOrderToDeliveryAsync(int orderId, int deliveryStaffId)
        {
            var shipping = await _context.Shippings.FirstOrDefaultAsync(s => s.OrderId == orderId && s.Status == "Pending");

            if (shipping != null)
            {
                shipping.Status = "Shipping";
                shipping.DeliveryStaffId = deliveryStaffId;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Order with ID {orderId} and status 'Pending' not found.");
            }
        }

        // Other repository methods as needed
    }

    //public async Task UpdateAsync(Shipping shipping)
    //{
    //    _context.Shippings.Update(shipping);
    //    await _context.SaveChangesAsync();
    //}

    //public async Task DeleteAsync(int shippingId)
    //{
    //    var shipping = await GetByIdAsync(shippingId);
    //    if (shipping != null)
    //    {
    //        _context.Shippings.Remove(shipping);
    //        await _context.SaveChangesAsync();
    //    }
    //}
}

