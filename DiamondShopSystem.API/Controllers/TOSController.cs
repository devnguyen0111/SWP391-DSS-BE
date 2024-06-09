using Microsoft.AspNetCore.Mvc;
using Services.OtherServices;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TOSController : Controller
    {
        private readonly ITOSService _TOSService;
        public TOSController(ITOSService tosService)
        {
            _TOSService = tosService;
        }

        [HttpGet]
        [Route("TOS")]
        public IActionResult getTOS()
        {
            _TOSService.createTOS();
            return Ok(_TOSService.uploadTOS());
        }
        
    }
}
