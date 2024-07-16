using Model.Models;
using Repository.Shippings;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Context = requestDto.Context,
                SStaffId = requestDto.SStaffId,
                ManId = requestDto.ManId,
                OrderId = requestDto.OrderId,
                RequestedDate = DateTime.UtcNow,
                Status = "Pending"
            };

            return await _requestRepository.AddRequestAsync(request);
        }

        public async Task<IEnumerable<Request>> GetAllRequestsAsync()
        {
            return await _requestRepository.GetAllRequestsAsync();
        }

        public async Task<bool> IsExistedRequestAsync(int orderId)
        {
            var checkRe = _requestRepository.GetRequestByOrderIdAsync(orderId);
            if (checkRe == null)
            {
                return false;
            }
            else { return true; }
        }

        public async Task<bool> ApproveRequestAsync(int requestId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null) return false;
            if (request.Status != "Pending") return false;

            request.Status = "Approved";
            await _requestRepository.UpdateRequestAsync(request);
            return true;
        }

        public async Task<bool> RejectRequestAsync(int requestId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null) return false;
            if (request.Status != "Pending") return false;

            request.Status = "Rejected";
            await _requestRepository.UpdateRequestAsync(request);
            return true;
        }
    }

}
