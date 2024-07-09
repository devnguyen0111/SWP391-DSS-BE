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
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.getAllOrders();
            return Ok(orders);
        }

        [HttpGet("TotalRevenue")]
        public IActionResult GetTotalRevenue()
        {
            // Fetch all orders with status "Paid"
            var paidOrders = _orderService.getAllOrders().Where(order => order.Status == "Paid");

            // Calculate total revenue
            decimal totalRevenue = paidOrders.Sum(order => order.TotalAmount);

            return Ok(new { TotalRevenue = totalRevenue });
        }
    }
}
