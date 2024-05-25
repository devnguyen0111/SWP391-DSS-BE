using Microsoft.AspNetCore.Mvc;
using DiamondShopSystem.API.Models;
using DiamondShopSystem.API.DAO;

namespace DiamondShopSystem.API.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly CustomerDAO _customerDAO;

        public LoginController()
        {
            _customerDAO = new CustomerDAO(); // Hoặc sử dụng Dependency Injection nếu cần
        }

        [HttpGet("api/customer")]
        public IActionResult Login([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest("Id is required.");
            }

            // Xử lý logic với biến 'name' tại đây
           
            var customer = _customerDAO.GetCustomerByID(id);
            Console.WriteLine($"check customer with name: {id}" );
            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            else
            {
                var customerName = customer.Name;
                return Ok($"hello, Customer {customerName}");   
            }
            //return Ok($"Hello, {name}!");
        }

    }
}
