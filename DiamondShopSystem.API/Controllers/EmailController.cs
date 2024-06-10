using Microsoft.AspNetCore.Mvc;
using Services.EmailServices;
using Services.OtherServices;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IPINCode _pinCode;
        public EmailController(IEmailService emailService, IPINCode pinCode)
        {
            _emailService = emailService;
            _pinCode = pinCode;
        }

        [HttpPost]
        [Route("send/email")]
        public IActionResult SendEmail(Email request)
        {
            _emailService.SendEmail(request);

            return Ok();
        }
        [HttpPost]
        [Route("send/PIN")]
        public IActionResult SendPinCode(string email)
        {
            string pin = _pinCode.GeneratedPinCode();
            _emailService.SendPinCode(email, pin);
            return Ok(pin);
        }

        [HttpPost]
        [Route("send/confirmation")]

        public IActionResult SendConfirmation(string email, string url)
        {
            _emailService.SendConfirmation(email, url);

            return Ok();
        }

        [HttpPost]
        [Route("send/attachment")]

        public IActionResult SendAttachment(string email)
        {
            _emailService.SendAttachment(email);

            return Ok();
        }

    }
}
