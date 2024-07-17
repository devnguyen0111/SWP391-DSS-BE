using DAO;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Shippings
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

        public async Task<List<OrderAssigned>> GetOrdersBySaleStaffIdAndStatusAsync(int saleStaffId, string status)
        {
            var orders = await _context.Shippings
                .Include(s => s.Order)
                .Include(s => s.SaleStaff)
                .Include(s => s.DeliveryStaff)
                .Include(s => s.Order.ShippingMethod)
                .Where(s => s.SaleStaffId == saleStaffId && s.Order.Status == status)
                .Select(s => new OrderAssigned
                {
                    OrderId = s.Order.OrderId,
                    StaffId = s.SaleStaff != null ? s.SaleStaff.SStaffId : (int?)null,
                    DeliveryId = s.DeliveryStaff != null ? s.DeliveryStaff.DStaffId : (int?)null,
                    OrderDate = s.Order.OrderDate,
                    Status = s.Order.Status,
                    ShippingMethodName = s.Order.ShippingMethod != null ? s.Order.ShippingMethod.MethodName : "Unknown",
                    TotalAmount = s.Order.TotalAmount
                }).ToListAsync();

            return orders;
        }
        public async Task<List<OrderAssigned>> GetOrdersBySaleStaffIdAsync(int saleStaffId)
        {
            var orders = await _context.Shippings
                .Include(s => s.Order)
                .Include(s => s.SaleStaff)
                .Include(s => s.DeliveryStaff)
                .Include(s => s.Order.ShippingMethod)
                .Where(s => s.SaleStaffId == saleStaffId)
                .Select(s => new OrderAssigned
                {
                    OrderId = s.Order.OrderId,
                    StaffId = s.SaleStaff != null ? s.SaleStaff.SStaffId : (int?)null,
                    DeliveryId = s.DeliveryStaff != null ? s.DeliveryStaff.DStaffId : (int?)null,
                    OrderDate = s.Order.OrderDate,
                    Status = s.Order.Status,
                    ShippingMethodName = s.Order.ShippingMethod != null ? s.Order.ShippingMethod.MethodName : "Unknown",
                    TotalAmount = s.Order.TotalAmount
                }).ToListAsync();

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

        public async Task<List<OrderAssigned>> GetAllOrdersAsync()
        {
            var orders = await _context.Shippings
                .Include(s => s.Order)
                .Include(s => s.SaleStaff)
                .Include(s => s.DeliveryStaff)
                .Include(s => s.Order.ShippingMethod)
                .Select(s => new OrderAssigned
                {
                    OrderId = s.Order.OrderId,
                    StaffId = s.SaleStaff != null ? s.SaleStaff.SStaffId : (int?)null,
                    DeliveryId = s.DeliveryStaff != null ? s.DeliveryStaff.DStaffId : (int?)null,
                    OrderDate = s.Order.OrderDate,
                    Status = s.Order.Status,
                    ShippingMethodName = s.Order.ShippingMethod != null ? s.Order.ShippingMethod.MethodName : "Unknown",
                    TotalAmount = s.Order.TotalAmount
                }).ToListAsync();

            return orders;
        }

        public async Task<List<OrderAssigned>> GetOrdersByDeliveryStaffIdAsync(int deliveryStaffId, string status)
        {
            return await _context.Shippings
        .Where(s => s.DeliveryStaffId == deliveryStaffId && s.Order.Status == status)
        .Include(s => s.Order)
        .ThenInclude(o => o.ShippingMethod)
        .Select(s => new OrderAssigned
        {
            OrderId = s.Order.OrderId,
            StaffId = s.SaleStaffId, // Default value for nullable StaffId
            DeliveryId = s.DeliveryStaffId, // Default value for nullable DeliveryId
            OrderDate = s.Order.OrderDate,
            Status = s.Order.Status ?? "Unknown", // Default value for nullable Status
            ShippingMethodName = s.Order.ShippingMethod != null ? s.Order.ShippingMethod.MethodName : "Unknown Shipping Method", // Handle nullable ShippingMethod
            TotalAmount = s.Order.TotalAmount  // Default value for nullable TotalAmount
        }).ToListAsync();

            //--get count--
            //var orderAssigned = await _context.Shippings
            //    .Where(s => s.DeliveryStaffId == deliveryStaffId && s.Order.Status == status)
            //    .Include(s => s.Order)
            //    .ThenInclude(o => o.ShippingMethod)
            //    .Select(s => new OrderAssigned
            //    {
            //        OrderId = s.Order.OrderId,
            //        StaffId = s.SaleStaffId,
            //        DeliveryId = s.DeliveryStaffId,
            //        OrderDate = s.Order.OrderDate,
            //        Status = s.Order.Status,
            //        ShippingMethodName = s.Order.ShippingMethod != null ? s.Order.ShippingMethod.MethodName : "Unknown",
            //        TotalAmount = s.Order.TotalAmount
            //    }).ToListAsync();

            //var OrderCount = orderAssigned.Count();

            //StaffOrder staffOrder = new StaffOrder();
            //staffOrder.Count = OrderCount;
            //if (OrderCount >= 10)
            //{
            //    staffOrder.StaffStatus = "Busy";
            //}
            //else { staffOrder.StaffStatus = "Available"; }
            //staffOrder.OrdersAssigned = orderAssigned;
            //return staffOrder;
        }

        public class StaffOrder
        {
            public int Count { get; set; }
            public string? StaffStatus { get; set; }
            public List<OrderAssigned> OrdersAssigned { get; set; }
        }

        public class OrderAssigned
        {
            public int OrderId { get; set; }
            public int? StaffId { get; set; }
            public int? DeliveryId { get; set; }
            public DateTime OrderDate { get; set; }
            public string Status { get; set; }
            public decimal TotalAmount { get; set; }
            public string ShippingMethodName { get; set; }
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






