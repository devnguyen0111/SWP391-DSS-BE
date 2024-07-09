using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Services.OrdersManagement;
using Services.Users;
using static Repository.Orders.ShippingRepository;

namespace DiamondShopSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignController : ControllerBase
    {
        private readonly IShippingService _shippingService;
        private readonly IAssignOrderService _assignOrderService;
        private readonly IManagerService _managerService;
        private readonly IOrderService _orderService;

        public AssignController(IShippingService shippingService, IAssignOrderService assignOrderService, IManagerService managerService, IOrderService orderService)
        {
            _shippingService = shippingService;
            _assignOrderService = assignOrderService;
            _managerService = managerService;
            _orderService = orderService;
        }
        [HttpGet("getAllOrdersFromShipping")]
        public async Task<ActionResult<List<OrderAssigned>>> GetAllOrdersFromShipping()
        {
            try
            {
                var orders = await _shippingService.GetAllOrdersAsync();

                if (orders == null || !orders.Any())
                {
                    return NotFound(new { Message = "No orders found for the given status." });
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework for this)
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
        [HttpGet("Shippings")]
        public async Task<ActionResult<List<Shipping>>> GetAllShipping()
        {
            var shippingList = await _shippingService.GetAllShippingAsync();

            if (shippingList == null || shippingList.Count == 0)
            {
                return NotFound();
            }

            return Ok(shippingList);
        }

        [HttpGet("shippingsByStatus/{status}")]
        public async Task<ActionResult<List<Shipping>>> GetShippingByStatus(string status)
        {
            var shippingList = await _shippingService.GetShippingByStatusAsync(status);

            if (shippingList == null || shippingList.Count == 0)
            {
                return NotFound();
            }

            return Ok(shippingList);
        }

        [HttpGet("shippingById/{shippingId}")]
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
        [HttpGet("saleStaffListByManagerId/{managerId}")]
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
        //Get orders in the shipping table base on the saleStaffId, let the staff see what are their Orders
        [HttpGet("ordersFromSaleStaffId/{saleStaffId}")]
        public async Task<ActionResult<List<OrderAssigned>>> GetOrdersBySaleStaffId(int saleStaffId)
        {
            try
            {
                var orders = await _shippingService.GetOrdersBySaleStaffIdAsync(saleStaffId);

                if (orders == null || orders.Count == 0)
                {
                    return NotFound("There are no orders matching your given data.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework for this)
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        //Get orders in the shipping table base on the saleStaffId, let the staff see what are their Orders
        [HttpGet("ordersFromSaleStaffIdAndStatus/{saleStaffId}/{status}")]
        public async Task<ActionResult<List<OrderAssigned>>> GetOrdersBySaleStaffIdAndStatus(int saleStaffId, string status)
        {
            try
            {
                var orders = await _shippingService.GetOrdersBySaleStaffIdAndStatusAsync(saleStaffId, status);

                if (orders == null || orders.Count == 0)
                {
                    return NotFound("There are no orders matching your given data.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework for this)
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }


        [HttpGet("getOrdersByDeliveryStaffId/{deliveryStaffId}/{status}")]
        public async Task<IActionResult> GetOrdersByDeliveryStaffId(int deliveryStaffId, string status = "Shipping")
        {
            try
            {
                var orders = await _orderService.GetOrdersByDeliveryStaffIdAsync(deliveryStaffId, status);
                if (orders == null || !orders.Any())
                {
                    return NotFound($"No orders found for delivery staff ID {deliveryStaffId}");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("assignStaff")]
        public async Task<IActionResult> CreateShipping(int orderId, int saleStaffId, string status = "Approve")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var shipping = await _shippingService.AssignOrderAsync(status, orderId, saleStaffId);
                return CreatedAtAction(nameof(GetShippingById), new { shippingId = shipping.ShippingId }, shipping);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //Assgin to deliveryStaff and change the status
        [HttpPost("assignDelivery")]
        public async Task<IActionResult> AssignShippingToDelivery(int orderId, int deliveryStaffId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _shippingService.AssignShippingToDeliveryAsync(orderId, deliveryStaffId);
                return Ok($"Shipping with order ID {orderId} assigned to delivery successfully.");
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

        [HttpPost("confirmFinishOrder/{orderId}")]
        public async Task<IActionResult> IsConfirmFinishOrder(int orderId)
        {
            var result = await _shippingService.IsConfirmFinishShippingAsync(orderId);
            if (!result)
            {
                return NotFound(new { Message = "Shipping not found." });
            }

            return Ok(new { Message = "Order and Shipping statuses updated successfully." });
        }

        
    }

}
