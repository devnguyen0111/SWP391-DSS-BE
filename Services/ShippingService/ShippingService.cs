using Model.Models;
using Repository.Shippings;
using Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Shippings.ShippingRepository;
using static Repository.Users.DeliveryStaffRepository;
using static Services.ShippingService.IShippingService;
namespace Services.ShippingService
{
    public class ShippingService : IShippingService
    {
        private readonly IShippingRepository _shippingRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ISaleStaffRepository _saleStaffRepository;
        private readonly IDeliveryStaffRepository _deliveryStaffRepository;

        public ShippingService(IShippingRepository shippingRepository, IOrderRepository orderRepository, ISaleStaffRepository saleStaffRepository, IDeliveryStaffRepository deliveryStaffRepository)
        {
            _shippingRepository = shippingRepository;
            _orderRepository = orderRepository;
            _saleStaffRepository = saleStaffRepository;
            _deliveryStaffRepository = deliveryStaffRepository;
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
            var order = _orderRepository.GetOrderByIdAndStatus(orderId, "Processing");

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            // Update the order status to "pending"
            order.Status = "Pending";
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
        public async Task<List<OrderAssigned>> GetOrdersBySaleStaffIdAsync(int saleStaffId)
        {
            return await _shippingRepository.GetOrdersBySaleStaffIdAsync(saleStaffId);
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
        public async Task<List<OrderAssigned>> GetOrdersByDeliveryStaffIdAsync(int deliveryStaffId, string status)
        {
            return await _shippingRepository.GetOrdersByDeliveryStaffIdAsync(deliveryStaffId, status);
        }
        public async Task<List<OrderAssigned>> GetAllOrdersAsync()
        {
            return await _shippingRepository.GetAllOrdersAsync();
        }

        

        public IEnumerable<SaleStaff> GetSaleStaffByManagerId(int managerId)
        {
            return _saleStaffRepository.GetSaleStaffByManagerId(managerId);
        }
        public IEnumerable<DeliveryStaffStatus> GetDeliveryStaffByManagerId(int managerId)
        {
            return _deliveryStaffRepository.GetDeliveryStaffStatus(managerId);
        }
        public IEnumerable<DeliveryStaffStatus> GetAllDeliveryStaff()
        {
            return _deliveryStaffRepository.GetAllDeliveryStaff();
        }
        public IEnumerable<SaleStaff> GetAllSaleStaff()
        {
            return _saleStaffRepository.GetAllSaleStaff();
        }

        public void AssignOrderToStaff(int staffId, int orderId)
        {
            _saleStaffRepository.AssignOrderToStaff(staffId, orderId);
            _saleStaffRepository.Save();
        }
        public class StaffRequest
        {
            public int? StaffId { get; set; }
            public int? Count { get; set; }
            public string? StaffStatus { get; set; }
            public string? Name { get; set; }
            public string? Phone { get; set; }
            public string? Email { get; set; }
            public int? ManagerId { get; set; }
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
