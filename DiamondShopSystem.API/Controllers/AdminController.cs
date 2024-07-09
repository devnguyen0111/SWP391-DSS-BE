using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Users;
using Services.Admin;
using Services.Diamonds;
using Services.Products;
using Repository.Users;
namespace DiamondShopSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminService _adminService;
        private readonly IOrderService _orderService;
        public AdminController(IUserRepository userRepository, IAdminService adminService, IOrderService orderService)
        {
            _userRepository = userRepository;
            _adminService = adminService;
            _orderService = orderService;
        }

        [HttpGet("users")]
        public ActionResult<IEnumerable<User>> GetUsersWithourAdmin()
        {
            List<User> users = _userRepository.GetAll();
            return users;
        }

        [HttpGet("CountUser")]
        public IActionResult CountUser()
        {
            List<User> users = _userRepository.GetAll();
            return Ok(users.Count);
        }

        [HttpGet("usersWithoutAdmin")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            List<User> users = _userRepository.GetAll().Where(u => u.Role != "Admin").ToList();
            return users;
        }

        [HttpPost("statusManagement/{userId}")]
        public IActionResult ChangeStatus(int userId) 
        { 
            User tempUser = _userRepository.GetById(userId);
            if(tempUser != null)
            {
                string status = _adminService.ChangeStatusUser(userId);
                return Ok("User " + userId + " has been " + status);
            } else
            {
                return NotFound("There is NO Such User");
            }
        }

        [HttpGet("GetAllOrders")]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            List<Order> orders = _orderService.getAllOrders();
            return orders;
        }

        [HttpGet("CountOrders")]
        public IActionResult TotalOrders()
        {
            List<Order> orders = _orderService.getAllOrders();
            return Ok(orders.Count);
        }

        [HttpGet("TodayOrders")]
        public IActionResult TodayOrders()
        {
            List<Order> orders = _orderService.getAllOrders();
            int count = 0;
            decimal totalAmount = 0;
            foreach(Order order in orders)
            {
                if(order.OrderDate.Day == DateTime.Now.Day)
                {
                    count++;
                    totalAmount += order.TotalAmount;
                }
            }
            return Ok(new
            {
                Count = count,
                TodayOrder = totalAmount

            });
        }
        [HttpGet("ThisMonthOrders")]
        public IActionResult ThisMonthOrders()
        {
            List<Order> orders = _orderService.getAllOrders();
            int count = 0;
            decimal totalAmount = 0;
            foreach (Order order in orders)
            {
                if (order.OrderDate.Month == DateTime.Now.Month)
                {
                    count++;
                    totalAmount += order.TotalAmount;
                }
            }
            return Ok(new
            {
                Count = count,
                ThisMonthOrder = totalAmount
            });
        }

        [HttpGet("TotalOrder")]
        public IActionResult TotalOrder()
        {
            List<Order> orders = _orderService.getAllOrders();
            decimal totalAmount = 0;
            foreach (Order order in orders)
            {
                totalAmount += order.TotalAmount;
            }
            return Ok(totalAmount);
        }

        [HttpGet("OrderCheckStatus")]
        public IActionResult OrderCheckStatus()
        {
            List<Order> orders = _orderService.getAllOrders();
            int processing = 0;
            int shipping = 0;
            int delivered = 0;
            int cancelled = 0;
            int paid = 0;
            foreach (Order order in orders)
            {
                if (order.Status == "processing")
                {
                    processing++;
                }
                else if (order.Status == "shipping")
                {
                    shipping++;
                }
                else if (order.Status == "delivered")
                {
                    delivered++;
                }
                else if (order.Status == "cancelled")
                {
                    cancelled++;
                }
                else if (order.Status == "Paid")
                {

                   paid++;
                }
            }
            return Ok(new
            {
                Processing = processing,
                Shipping = shipping,
                Delivered = delivered,
                Cancelled = cancelled,
                Paid = paid
            });

        }

        //[HttpGet("TopRevenueProducts")]
                
    }
}
