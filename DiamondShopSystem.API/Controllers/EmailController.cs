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
        private readonly IEmailRelatedService _pinCode;
        public EmailController(IEmailService emailService, IEmailRelatedService pinCode)
        {
            _emailService = emailService;
            _pinCode = pinCode;
        }

        [HttpPost]
        [Route("send/email")]
        public IActionResult SendEmail(Email email)
        {
            _emailService.SendEmail(email);

            return Ok("Email has successfully been sent to " + email.To + " !!");
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
            _emailService.SendConfirmationLink(email, url);

            return Ok("A confirmation has been sent succesfully to " + email + "!!");
        }

        [HttpPost]
        [Route("send/attachment")]

        // ///this is it
        public IActionResult SendAttachment(string email)
        {
            _emailService.SendAttachment(email);

            return Ok("An attachment has been sent successfully to " + email + "!!");
        }

        [HttpPost]
        [Route("send/resetPasswordLink")]

        public IActionResult SendResetPasswordLink(string email, string url)
        {
            _emailService.SendResetLink(email, url);

            return Ok("A confirmation has been sent succesfully to " + email + "!!");
        }

        [HttpPost]
        [Route("send/GIA")]

        // ///this is it
        public IActionResult SendGIA(string email)
        {
            _emailService.SendGIA(email);

            return Ok("An GIA Report has been sent successfully to " + email + "!!");
        }
    }
}
