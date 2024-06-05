using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.EmailServices;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public IActionResult SendEmail(Email request)
        {
            _emailService.SendEmail(request);

            return Ok();
        }


    }
}
