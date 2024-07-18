using Model.Models;
using Repository.Shippings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Repository.Shippings.RequestRepository;

namespace Services.ShippingService
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<Request> CreateRequestAsync(CreateRequestDto requestDto)
        {
            var request = new Request
            {
                Title = requestDto.Title,
                Context = requestDto.Context,
                SStaffId = requestDto.SStaffId,
                ManId = requestDto.ManId,
                OrderId = requestDto.OrderId,
                RequestedDate = DateTime.UtcNow,
                RequestStatus = "Pending",
                ProcessStatus = "Pending"
            };

            return await _requestRepository.AddRequestAsync(request);
        }

        public async Task<IEnumerable<Request>> GetAllRequestsAsync()
        {
            return await _requestRepository.GetAllRequestsAsync();
        } 
        public async Task<Request> GetRequestDetailAsync(int requestId)
        {
            return await _requestRepository.GetRequestByIdAsync(requestId);
        }

        public async Task<bool> IsCompletedRequestAsync(int orderId)
        {
            var checkRe = await _requestRepository.GetRequestByOrderIdAsync(orderId);
            if (checkRe == null)
            {
                return false;
            }
            //if it is not completed
            return checkRe.ProcessStatus.ToLower() == "completed" ? true : false;
            
        }
        public async Task<bool> IsPendingRequestAsync(int orderId)
        {
            var checkRe = await _requestRepository.GetRequestByOrderIdAsync(orderId);
            if (checkRe == null)
            {
                return false;
            }
            //if it is not completed
            return checkRe.RequestStatus.ToLower() == "pending" ? true : false;
        }

        public async Task<bool> ApproveRequestAsync(int requestId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null) return false;
            if (request.RequestStatus != "Pending") return false;

            request.RequestStatus = "Approved";
            request.ProcessStatus = "Completed";
            await _requestRepository.UpdateRequestAsync(request);
            return true;
        }

        public async Task<bool> RejectRequestAsync(int requestId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null) return false;
            if (request.RequestStatus != "Pending") return false;

            request.RequestStatus = "Rejected";
            request.ProcessStatus = "Completed";
            await _requestRepository.UpdateRequestAsync(request);
            return true;
        }
    }

}
