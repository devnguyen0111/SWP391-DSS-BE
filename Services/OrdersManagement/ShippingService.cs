using Model.Models;
using Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.OrdersManagement.IShippingService;

namespace Services.OrdersManagement
{
    public class ShippingService : IShippingService
    {
        private readonly IShippingRepository _shippingRepository;

        public ShippingService(IShippingRepository shippingRepository)
        {
            _shippingRepository = shippingRepository;
        }

        public async Task<Shipping> GetShippingByIdAsync(int shippingId)
        {
            return await _shippingRepository.GetByIdAsync(shippingId);
        }

        public async Task<Shipping> AssignOrderAsync(string status, int orderId, int saleStaffId)  //tạo trong shipping luôn
        {

            var shipping = new Shipping
            {
                Status = status,
                OrderId = orderId,
                SaleStaffId = saleStaffId,
                DeliveryStaffId = 1,                              //nhớ sửa database
            };

            // Save the shipping record
            await _shippingRepository.CreateAsync(shipping);

            return shipping;
        }

        public async Task<List<Order>> GetOrdersBySaleStaffIdAndStatusAsync(int saleStaffId, string status)
        {
            return await _shippingRepository.GetOrdersBySaleStaffIdAndStatusAsync(saleStaffId, status);
        }

        public async Task<Order> GetOrderByOrderIdAsync(int orderId)
        {
            return await _shippingRepository.GetOrderByOrderIdAsync(orderId);
        }
        public async Task AssignOrderToDeliveryAsync(int orderId, int deliveryStaffId)
        {
            await _shippingRepository.AssignOrderToDeliveryAsync(orderId, deliveryStaffId);
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
