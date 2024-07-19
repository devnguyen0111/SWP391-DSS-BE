using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Repository.Users;
using Services.ShippingService;
using Services.Users;
using static Repository.Shippings.RequestRepository;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly ISaleStaffRepository _saleStaffRepository;

        public RequestsController(IRequestService requestService, ISaleStaffRepository saleStaffRepository)
        {
            _requestService = requestService;
            _saleStaffRepository = saleStaffRepository;
        }

        [HttpGet("requests")]
        public async Task<IActionResult> GetAllRequests()
        {
            try
            {
                var requests = await _requestService.GetAllRequestsAsync();
                return Ok(requests.Select(r => new RequestDetail
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

        [HttpGet("requests/{userId}")]
        public async Task<IActionResult> GetRequestByUserId(int userId)
        {
            try
            {
                var requests = await _requestService.GetAllRequestsAsync();
                return Ok(requests
                    .Where(r => r.SStaffId == userId || r.ManId == userId)
                    .Select(r => new RequestDetail
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
        public async Task<IActionResult> CreateRequest([FromBody] CreateRequest requestDto)
        {
            try
            {
                if (await _requestService.IsPendingRequestAsync(requestDto.OrderId))
                {
                    return BadRequest("This Order has already got a request");
                }
                else
                {
                    CreateRequestDto createRequestDto = new CreateRequestDto
                    {

                        Context = requestDto.Context,
                        ManId = _saleStaffRepository.GetSaleStaffById(requestDto.SStaffId).ManagerId,
                        OrderId = requestDto.OrderId,
                        SStaffId = requestDto.SStaffId,
                        Title = requestDto.Title
                    };


                    var request = await _requestService.CreateRequestAsync(createRequestDto);
                    return Ok(new
                    {
                        message = "Request created successfully",
                        createRequestDto
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
