using DAO;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Shippings
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> GetAllRequestsAsync();
        Task<Request> GetRequestByIdAsync(int requestId);
        Task<Request> GetRequestByOrderIdAsync(int orderId);
        Task UpdateRequestAsync(Request request);
        Task<Request> AddRequestAsync(Request request);
    }
}
