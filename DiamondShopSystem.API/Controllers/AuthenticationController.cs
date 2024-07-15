using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using Services.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] TokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokenModel.AccessToken;
            string refreshToken = tokenModel.RefreshToken;

            var principal =_authenticateService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var userEmail = principal.Identity.Name;
            var user = _authenticateService.GetUserByMail(userEmail);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = _authenticateService.GenerateJwtToken(user, 15);
            var newRefreshToken = _authenticateService.GenerateJwtToken(user, 60);

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(60);
            _authenticateService.UpdateUserPassword(user);

            return new ObjectResult(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] DTO.LoginRequest request)
        {

            var user = _authenticateService.GetUserByMail(request.Email);
            if (user == null)
            {
                return BadRequest("Email address is not registered ");
            }

            // check user is active
            if (user.Status != "active")
            {
                return BadRequest("Account has been Disable");
            }

            if ((bool)request.IsGoogleLogin)
            {
                // For Google login, bypass password check
                var token = _authenticateService.googleAuthen(user.Email); // Implement token generation logic
                return Ok(new { Token = token });
            }
            else
            {
                // Traditional email/password login
                var token = _authenticateService.Authenticate(request.Email, GetHashString(request.Password));
                if (token == null)
                {
                    return BadRequest("Wrong Email or Password");
                }
                return Ok(new { Token = token });
            }
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] registerRequest rq) {
            if (_authenticateService.GetUserByMail(rq.email)!=null)
            {
                return BadRequest("Email address is already registered");
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
        [HttpPost("checkMail")]
        public IActionResult checkMail(string mail)
        {
            if (_authenticateService.GetUserByMail(mail) != null)
            {
                return BadRequest("Email address is already registered.");
            }
            return Ok(mail);
        }
        [Authorize]
        [HttpPost("changePassword")]
        public IActionResult changeUserPassword([FromBody] DTO.PasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                return BadRequest(errors);
            }
            User updatingUser = _authenticateService.GetUserById(request.Id);
            if (updatingUser == null)
            {
                return BadRequest("Cannot find user with this id");
            }
            if (updatingUser.Password != GetHashString(request.OldPassword))
            {
                return BadRequest("Old password is not correct");
            }
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return BadRequest("The new password and confirmation password do not match");
            }
            updatingUser.Password =GetHashString(request.NewPassword);
            _authenticateService.UpdateUserPassword(updatingUser);
            return Ok(updatingUser);
            
        }
        [HttpPost("forgotPassword")]
        public IActionResult resetUserPassword([FromBody] PasswordRequestForgor huh)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                return BadRequest(errors);
            }
            
            User updatingUser = _authenticateService.GetUserByMail(huh.Email);
            if (updatingUser == null)
            {
                return BadRequest("There is no such user with this id");
            }
            if (huh.NewPassword != huh.ConfirmNewPassword)
            {
                return BadRequest("The new password and confirmation password do not match");
            }
            updatingUser.Password =GetHashString(huh.NewPassword);
            _authenticateService.UpdateUserPassword(updatingUser);
            return Ok(updatingUser);
            
        }
    }
}
