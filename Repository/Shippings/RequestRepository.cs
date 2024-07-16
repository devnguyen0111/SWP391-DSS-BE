using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Shippings
{
    public class RequestRepository : IRequestRepository
    {
        private readonly DIAMOND_DBContext _DBContext;

        public RequestRepository(DIAMOND_DBContext context)
        {
            _DBContext = context;
        }

        public async Task<Request> AddRequestAsync(Request request)
        {
            _DBContext.Requests.Add(request);
            await _DBContext.SaveChangesAsync();
            return request;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsAsync()
        {
            return await _DBContext.Requests.ToListAsync();
        }

        public async Task<Request> GetRequestByIdAsync(int requestId)
        {
            return await _DBContext.Requests.FindAsync(requestId);
        }
        public async Task<List<Request>> GetRequestByOrderIdAsync(int orderId)
        {
            return await _DBContext.Requests.Where(o => o.OrderId == orderId).ToListAsync();
        }

        public async Task UpdateRequestAsync(Request request)
        {
            _DBContext.Requests.Update(request);
            await _DBContext.SaveChangesAsync();
        }

        public class CreateRequestDto
        {
            public string Context { get; set; }
            public int SStaffId { get; set; }
            public int ManId { get; set; }
            public int OrderId { get; set; }
        }
    }
}

