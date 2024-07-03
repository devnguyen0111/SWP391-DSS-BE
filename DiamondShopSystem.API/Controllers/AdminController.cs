using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Repository;
using Repository.Users;
using Services.Admin;

namespace DiamondShopSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminService _adminService;
        public AdminController(IUserRepository userRepository, IAdminService adminService)
        {
            _userRepository = userRepository;
            _adminService = adminService;
        }

        [HttpGet("users")]
        public ActionResult<IEnumerable<User>> GetUsersWithourAdmin()
        {
            List<User> users = _userRepository.GetAll();
            return users;
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
    }
}
