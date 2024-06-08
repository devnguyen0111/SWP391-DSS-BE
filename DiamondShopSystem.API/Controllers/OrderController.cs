using DAO;
using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.EmailServices;
using Services.Products;
using Services.Users;
using Services.Utility;
using Order = Model.Models.Order;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IVoucherService _voucherService;
        private readonly IEmailService _emailService;
        private readonly IProductService _productService;
        public OrderController(IOrderService orderService,IVoucherService voucherService, IEmailService emailService, IProductService productService)
        {
            _orderService = orderService;
            _emailService = emailService;
            _voucherService = voucherService;
            _productService = productService;
        }
        [HttpPost]
        [Route("createOrderDirecly")]
        public IActionResult CreateOrder([FromBody] OrderRequest request)
        {
            Order o = _orderService.createOrderDirectly((int)request.CusId, request.pid, (int)request.ShippingMethodId);
            return Ok(o);
        }
        [HttpPost]
        [Route("createOrderFromCart")]
        public IActionResult CreateOrderFromCart([FromBody] OrderRequest request)
        {
            Order o = _orderService.createOrderFromCart((int)request.CusId,(int)request.ShippingMethodId);
            return Ok(o);
        }
        [HttpGet]
        [Route("GetOrderByStatus")]
        public IActionResult getOrders(int uid,string status)
        {
            List<Order> orders = _orderService.getOrderByStatus(uid, status);
            var orderReal = orders.Select(OrderMapper.MapToOrderResponse).ToList();
            return Ok(orderReal);

        }
        
    }
}
