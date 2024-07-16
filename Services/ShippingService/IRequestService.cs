using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Shippings.RequestRepository;

namespace Services.ShippingService
{
    public interface IRequestService
    {
        Task<Request> CreateRequestAsync(CreateRequestDto requestDto);
        Task<IEnumerable<Request>> GetAllRequestsAsync();
        Task<bool> IsExistedRequestAsync(int requestId);
        Task<bool> ApproveRequestAsync(int requestId);
        Task<bool> RejectRequestAsync(int requestId);
    }

}
