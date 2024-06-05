using DAO;
using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly DIAMOND_DBContext _context;
        public OrderController(DIAMOND_DBContext dbcontext)
        {
            _context = dbcontext;
        }
        [HttpPost]
        [Route("createOrder")]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderRequest request)
        {
            // Calculate the total amount
            decimal totalAmount = 1000;

            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = "Pending",
                CusId = request.CusId,
                ShippingMethodId = request.ShippingMethodId,
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
            var order1 = _context.Orders.FirstOrDefault(o => o.CusId == request.CusId);
            _context.Add(new ProductOrder(request.pid, order1.OrderId,1));
            _context.SaveChanges();
            return Ok();
        }
    }
}
