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
        private readonly IOrderRepository _orderRepository;

        public RequestsController(IRequestService requestService, ISaleStaffRepository saleStaffRepository, IOrderRepository orderRepository)
        {
            _requestService = requestService;
            _saleStaffRepository = saleStaffRepository;
            _orderRepository = orderRepository;
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
                    OrderStatus = _orderRepository.GetOrderByOrderId(r.OrderId).Status,
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
                        OrderStatus =  _orderRepository.GetOrderByOrderId(r.OrderId).Status,
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

                if (r == null)
                {
                    return BadRequest("No such Request existed");
                }
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
                    OrderStatus = _orderRepository.GetOrderByOrderId(r.OrderId).Status,
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
                Request request = await _requestService.GetRequestDetailByOrderIdAsync(requestDto.OrderId);
                if (request.ProcessStatus == "Pending")
                {
                    return BadRequest("This Order has already had a request, please wait for it to be completed");
                }

                if (request.ProcessStatus == "Completed" && request.RequestStatus == "Approved")
                {
                    return BadRequest("This Order has been Approved to be " + request.Title);
                }

                if (request == null || (request.ProcessStatus == "Completed" && request.RequestStatus == "Rejected"))
                {
                    CreateRequestDto createRequestDto = new CreateRequestDto
                    {

                        Context = requestDto.Context,
                        ManId = _saleStaffRepository.GetSaleStaffById(requestDto.SStaffId).ManagerId,
                        OrderId = requestDto.OrderId,
                        SStaffId = requestDto.SStaffId,
                        Title = requestDto.Title
                    };


                    var tempRequest = await _requestService.CreateRequestAsync(createRequestDto);
                    return Ok(new
                    {
                        message = "Request created successfully",
                        createRequestDto
                    });
                } else
                {
                    return BadRequest("There is something wrong");
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
