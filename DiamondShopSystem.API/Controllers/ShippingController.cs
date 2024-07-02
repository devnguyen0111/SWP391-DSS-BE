using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Services.OrdersManagement;
using Services.Users;

namespace DiamondShopSystem.API.Controllers
{
    [ApiController]
    [Route("api/shipping")]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService _shippingService;
        private readonly IAssignOrderService _assignOrderService;
        private readonly IManagerService _managerService;

        public ShippingController(IShippingService shippingService, IAssignOrderService assignOrderService, IManagerService managerService)
        {
            _shippingService = shippingService;
            _assignOrderService = assignOrderService;
            _managerService = managerService;
        }

        [HttpGet("shipping/{shippingId}")]
        public async Task<ActionResult<Shipping>> GetShippingById(int shippingId)
        {
            var shipping = await _shippingService.GetShippingByIdAsync(shippingId);
            if (shipping == null)
            {
                return NotFound("There is no shipping match this Id:" + shippingId);
            }
            return Ok(shipping);
        }
        //Get the list of saleStaff base on the managerId
        [HttpGet("saleStaffList/{managerId}")]
        public IActionResult GetSaleStaffByManagerId(int managerId)
        {
            var saleStaffs = _assignOrderService.GetSaleStaffByManagerId(managerId);

            if (_managerService.GetManagerById(managerId) == null)
            {
                return BadRequest("No sales staff found for the given manager ID.");
            }

            if (saleStaffs == null || !saleStaffs.Any())
            {
                return BadRequest("No sales staff found for the given manager ID.");
            }

            var saleStaffRequests = saleStaffs.Select(s => new SaleStaffRequest
            {
                SStaffId = s.SStaffId,
                Name = s.Name,
                Phone = s.Phone,
                Email = s.Email,
                ManagerId = s.ManagerId
            }).ToList();

            return Ok(saleStaffRequests);
        }
        //Assign staff the order and make the shipping
        [HttpPost("assignStaff")]
        public async Task<IActionResult> CreateShipping(string status, int orderId, int saleStaffId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var shipping = await _shippingService.AssignOrderAsync(status = "Pending",orderId,saleStaffId);
                return CreatedAtAction(nameof(GetShippingById), new { shippingId = shipping.ShippingId }, shipping);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //Get orders in the shipping table base on the saleStaffId, let the staff see what are their Orders
        [HttpGet("ordersFromShipping/{saleStaffId}")]
        public async Task<ActionResult<List<Order>>> GetOrdersBySaleStaffIdAndStatus(int saleStaffId)
        {
            string status = "Pending"; 

            var orders = await _shippingService.GetOrdersBySaleStaffIdAndStatusAsync(saleStaffId, status);

            if (orders == null || orders.Count == 0)
            {
                return NotFound("There is no Orders match your given data!!");
            }

            return Ok(orders);
        }

        //Get OrderDetail by OrderId from shipping
        [HttpGet("orderFromShipping/{orderId}")]
        public async Task<ActionResult<Order>> GetOrderByOrderId(int orderId)
        {
            var order = await _shippingService.GetOrderByOrderIdAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        //Assgin to deliveryStaff and change the status
        [HttpPost("assignDelivery")]
        public async Task<IActionResult> AssignOrderToDelivery([FromBody] int orderId, int deliveryStaffId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _shippingService.AssignOrderToDeliveryAsync(orderId,deliveryStaffId);
                return Ok("Order assigned to delivery successfully.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }

}
