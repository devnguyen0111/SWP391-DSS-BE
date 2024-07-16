using Microsoft.AspNetCore.Mvc;
using Services.ShippingService;
using static Repository.Shippings.RequestRepository;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllRequests()
        {
            try
            {
                var requests = await _requestService.GetAllRequestsAsync();
                return Ok(requests);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] CreateRequestDto requestDto)
        {
            try
            {
                if (await _requestService.IsExistedRequestAsync(requestDto.OrderId))
                {
                    return BadRequest("This Order has already been submitted an approval request");
                }
                else
                {
                    var request = await _requestService.CreateRequestAsync(requestDto);
                    return Ok(new { message = "Request created successfully", request });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost("approve/{requestId}")]
        public async Task<IActionResult> ApproveRequest(int requestId)
        {
            try
            {
                var result = await _requestService.ApproveRequestAsync(requestId);
                if (!result)
                    return NotFound(new { message = "Request not found or have already been confirmed" });

                return Ok(new { message = "Request approved successfully" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpPost("reject/{requestId}")]
        public async Task<IActionResult> RejectRequest(int requestId)
        {
            try
            {
                var result = await _requestService.RejectRequestAsync(requestId);
                if (!result)
                    return NotFound(new { message = "Request not found or have already been confirmed" });

                return Ok(new { message = "Request rejected successfully" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        
    }
}
