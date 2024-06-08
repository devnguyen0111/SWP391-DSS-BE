using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Users;
using System.Security.Cryptography;
using System.Text;

namespace DiamondShopSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly ICustomerService _customerService;
        private readonly ICartService _cartService;
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        public AuthenticationController(IAuthenticateService authenticateService, ICustomerService customerService, ICartService cartService)
        {
            _authenticateService = authenticateService;
            _customerService = customerService;
            _cartService = cartService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] DTO.LoginRequest request)
        {
            var token = _authenticateService.Authenticate(request.Email, GetHashString(request.Password));
            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] registerRequest rq) {
            if (_authenticateService.GetUserByMail(rq.email) != null)
            {
                return BadRequest("Email address is already registered.");
            }
            Customer c = new Customer
            {
                CusFirstName = rq.firstname,
                CusLastName = rq.lastname,
                CusPhoneNum = rq.phonenumber,
                Cus = new User
                {
                    Email = rq.email,
                    Password = GetHashString(rq.password),
                    Status = "active",
                    Role = "customer",
                    
                }
            };
            Address a = new Address
            {
                AddressId = c.CusId,
                State = "",
                City = "",
                Country = "VietNam",
                Street = "",
                ZipCode = "",

            };
            c.Address = a;
            _customerService.addCustomer(c);
            _cartService.createCart(c.CusId);
            return Ok(c);
        }


    }
}
