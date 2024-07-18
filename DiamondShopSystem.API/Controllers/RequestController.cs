using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
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
                return Ok( requests.Select(r => new RequestDetail
                {
                    RequestId = r.RequestId,
                    Title = r.Title,
                    ProcessStatus = r.ProcessStatus,
                    RequestedDate = r.RequestedDate,
                    RequestStatus = r.RequestStatus,
                    Context = r.Context,
                    ManId = r.ManId,
                    OrderId = r.OrderId,
                    SStaffId = r.SStaffId
                }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpGet("detail/{requestId}")]
        public async Task<IActionResult> GetRequestDetail(int requestId)
        {
            try
            {
                var r = await _requestService.GetRequestDetailAsync(requestId);
                return Ok(new RequestDetail
                {
                    RequestId = r.RequestId,
                    Title = r.Title,
                    ProcessStatus = r.ProcessStatus,
                    RequestedDate = r.RequestedDate,
                    RequestStatus = r.RequestStatus,
                    Context = r.Context,
                    ManId = r.ManId,
                    OrderId = r.OrderId,
                    SStaffId = r.SStaffId
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRequest([FromBody] CreateRequestDto requestDto)
        {
            try
            {
                if (await _requestService.IsPendingRequestAsync(requestDto.OrderId))
                {
                    return BadRequest("This Order has already got a request");
                }
                else
                {
                    var request = await _requestService.CreateRequestAsync(requestDto);
                    return Ok(new
                    {
                        message = "Request created successfully",
                        CreateRequestDto = new CreateRequestDto
                        {
                            Title = request.Title,
                            Context = request.Context,
                            ManId = request.ManId,
                            OrderId = request.OrderId,
                            SStaffId = request.SStaffId
                        }
                    });
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
