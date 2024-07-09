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

        public async Task<List<Shipping>> GetAllShippingAsync()
        {
            return await _context.Shippings.ToListAsync();
        }

        public async Task<List<Shipping>> GetShippingByStatusAsync(string status)
        {
            return await _context.Shippings
                .Where(s => s.Status == status)
                .ToListAsync();
        }

        public async Task<Shipping> GetShippingByIdAsync(int shippingId)
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
                .Where(s => s.SaleStaffId == saleStaffId)
                .Select(s => s.Order)
                .Where(o => o.Status == status)
                .ToListAsync();

            return orders;
        }

        public async Task<Order> GetOrderByOrderIdAsync(int orderId)
        {
            var shipping = await _context.Shippings
                .FirstOrDefaultAsync(s => s.OrderId == orderId);

            return shipping?.Order; // Assuming Order is a navigation property in Shipping
        }

        public async Task AssignShippingToDeliveryAsync(int orderId, int deliveryStaffId)
        {
            var shipping = await _context.Shippings
                .Include(s => s.Order)
                .FirstOrDefaultAsync(s => s.OrderId == orderId && s.Status == "Approve");
            if (shipping != null)
            {
                // Update the shipping and order status
                shipping.DeliveryStaffId = deliveryStaffId;
                shipping.Order.Status = "Shipping";
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Shipping with order ID {orderId} and status Pending not found.");
            }
        }
        public async Task UpdateShippingAsync(Shipping shipping)
        {
            _context.Shippings.Update(shipping);
            await _context.SaveChangesAsync();
        }

        public async Task<Shipping> GetShippingByOrderIdAsync(int orderId)
        {
            return await _context.Shippings
                .Include(s => s.Order)
                .FirstOrDefaultAsync(s => s.OrderId == orderId);
        }

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
}






