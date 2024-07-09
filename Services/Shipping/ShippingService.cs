using Model.Models;
using Repository.Orders;
using Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Orders.ShippingRepository;
using static Services.OrdersManagement.IShippingService;

namespace Services.OrdersManagement
{
    public class ShippingService : IShippingService
    {
        private readonly IShippingRepository _shippingRepository;
        private readonly IOrderRepository _orderRepository;

        public ShippingService(IShippingRepository shippingRepository, IOrderRepository orderRepository)
        {
            _shippingRepository = shippingRepository;
            _orderRepository = orderRepository;
        }

        public async Task<List<Shipping>> GetAllShippingAsync()
        {
            return await _shippingRepository.GetAllShippingAsync();
        }

        public async Task<List<Shipping>> GetShippingByStatusAsync(string status)
        {
            return await _shippingRepository.GetShippingByStatusAsync(status);
        }

        public async Task<Shipping> GetShippingByIdAsync(int shippingId)
        {
            return await _shippingRepository.GetShippingByIdAsync(shippingId);
        }

        public async Task<Shipping> AssignOrderAsync(string status, int orderId, int saleStaffId)
        {
            var order = _orderRepository.GetOrderByIdAndStatus(orderId, "processing");

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            // Update the order status to "pending"
            order.Status = "pending";
            await _orderRepository.UpdateOrderAsync(order);

            // Create the shipping record
            var shipping = new Shipping
            {
                Status = status,
                OrderId = orderId,
                SaleStaffId = saleStaffId,
            };

            await _shippingRepository.CreateAsync(shipping);

            return shipping;
        }

        public async Task<List<OrderAssigned>> GetOrdersBySaleStaffIdAndStatusAsync(int saleStaffId, string status)
        {
            return await _shippingRepository.GetOrdersBySaleStaffIdAndStatusAsync(saleStaffId, status);
        }

        public async Task<Order> GetOrderByOrderIdAsync(int orderId)
        {
            return await _shippingRepository.GetOrderByOrderIdAsync(orderId);
        }

        public async Task AssignShippingToDeliveryAsync(int orderId, int deliveryStaffId)
        {
            await _shippingRepository.AssignShippingToDeliveryAsync(orderId, deliveryStaffId);
        }
        public async Task<bool> IsConfirmFinishShippingAsync(int orderId)
        {
            var shipping = await _shippingRepository.GetShippingByOrderIdAsync(orderId);
            if (shipping == null)
            {
                return false;
            }

            // Update the shipping status to "Finish"
            shipping.Status = "Finish";

            // Update the order status to "Delivered"
            shipping.Order.Status = "Delivered";

            await _shippingRepository.UpdateShippingAsync(shipping);
            return true;
        }

        public async Task<List<OrderAssigned>> GetAllOrdersAsync()
        {
            return await _shippingRepository.GetAllOrdersAsyn();
        }

        //public async Task UpdateShippingAsync(Shipping shipping)
        //{
        //    await _shippingRepository.UpdateAsync(shipping);
        //}

        //public async Task DeleteShippingAsync(int shippingId)
        //{
        //    await _shippingRepository.DeleteAsync(shippingId);
        //}
    }

}
