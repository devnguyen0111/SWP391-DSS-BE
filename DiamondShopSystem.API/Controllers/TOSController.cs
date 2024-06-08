using DAO;
using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.EmailServices;
using Services.OtherServices;
using Services.Products;
using Services.Users;
using Services.Utility;
using Order = Model.Models.Order;

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
