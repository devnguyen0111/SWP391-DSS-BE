using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Users;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        public IActionResult GetManagers()
        {
            var managers = _managerService.GetManagers();
            if (managers == null || !managers.Any())
            {
                return NotFound("No managers found.");
            }

            return Ok(managers);
        }

        [HttpGet("{managerId}")]
        public IActionResult GetManagerById(int managerId)
        {
            var manager = _managerService.GetManagerById(managerId);
            if (manager == null)
            {
                return NotFound("Manager not found.");
            }

            return Ok(manager);
        }

        [HttpGet("managerRequest")]
        public IActionResult GetManagersRequest()
        {
            var managers = _managerService.GetManagers();
            if (managers == null || !managers.Any())
            {
                return NotFound("No managers found.");
            }

            var managerRequests = managers.Select(m => new ManagerRequest
            {
                ManId = m.ManId,
                ManName = m.ManName,
                ManPhone = m.ManPhone,
                SaleStaffId = m.SaleStaffs.FirstOrDefault()?.SStaffId ?? 0,
                DeliveryStaffId = m.DeliveryStaffs.FirstOrDefault()?.DStaffId ?? 0
            }).ToList();

            return Ok(managerRequests);
        }

        [HttpGet("managerRequest/{managerId}")]
        public IActionResult GetManagerRequestById(int managerId)
        {
            var manager = _managerService.GetManagerById(managerId);
            if (manager == null)
            {
                return NotFound("Manager not found.");
            }

            var managerRequest = new ManagerRequest
            {
                ManId = manager.ManId,
                ManName = manager.ManName,
                ManPhone = manager.ManPhone,
                SaleStaffId = manager.SaleStaffs.FirstOrDefault()?.SStaffId ?? 0,
                DeliveryStaffId = manager.DeliveryStaffs.FirstOrDefault()?.DStaffId ?? 0
            };

            return Ok(managerRequest);
        }
    }

}
