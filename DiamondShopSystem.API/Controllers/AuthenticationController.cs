using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services;
using System.Net.NetworkInformation;

namespace DiamondShopSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] DTO.LoginRequest request)
        {
            var token = _authenticateService.Authenticate(request.Email, request.Password);
            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
        [Authorize]
        [HttpGet("test")]
        public IActionResult test()
        {

            return Ok(new int[] {1,2,3,4,5});
        }
        [HttpGet("test1")]
        public IActionResult test1()
        {
            var multipledata = new {name = "string", num = 123 };
            return Ok(multipledata);
        }
    }
}
