using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
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
            Order o = _orderService.createOrderDirectly((int)request.CusId, request.pid, (int)request.ShippingMethodId,request.deliveryAddress,request.contactNumber);
            return Ok(o);
        }
        [HttpPost]
        [Route("createOrderFromCart")]
        public IActionResult CreateOrderFromCart([FromBody] OrderRequestCart request)
        {
            Order o = _orderService.createOrderFromCart((int)request.CusId,(int)request.ShippingMethodId, request.deliveryAddress, request.contactNumber);
            return Ok(o);
        }

        [HttpGet]
        [Route("GetOrderByStatus")]
        public IActionResult getOrders(int uid,string status)
        {
            List<Order> l = _orderService.getOrderByStatus(uid, status);
            return Ok(l.Select(OrderMapper.MapToOrderResponse).ToList());
        }

        [HttpGet("customer/{customerId}/history")]
        public ActionResult<List<OrderHistoryResponse>> GetOrderHistory(int customerId, string status = "Done")
        {
            try
            {
                var orders = _orderService.getOrderByStatus(customerId, status);

                if (orders == null || !orders.Any())
                {
                    return NotFound(new { Message = "No orders found for the given customer ID and status." });
                }

                var orderHistoryResponses = orders.Select(o => new OrderHistoryResponse
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    ShippingMethodName = o.ShippingMethod.MethodName,
                    TotalAmount = o.TotalAmount,
                    ProductName = o.ProductOrders.Select(po => po.Product.ProductName).FirstOrDefault(),
                    SizeName = o.ProductOrders.Select(po => po.Product.Size.SizeName).FirstOrDefault(),
                    MetaltypeName = o.ProductOrders.Select(po => po.Product.Metaltype.MetaltypeName).FirstOrDefault(),
                    DiamondName = o.ProductOrders.Select(po => po.Product.Diamond.DiamondName).FirstOrDefault(),
                    Items = o.ProductOrders.Select(po => new OrderHistoryItem
                    {
                        oHId = po.ProductId, // Assuming this maps to OrderHistoryItem's OHId
                        name = po.Product.ProductName,
                        quantity = po.Quantity,
                        img = "https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/illustration-gallery-icon_53876-27002.avif?alt=media&token=037e0d50-90ce-4dd4-87fc-f54dd3dfd567" // Adjust according to your actual model
                    }).ToList()
                }).ToList();

                return Ok(orderHistoryResponses);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework for this)
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        //[HttpGet]
        //public IActionResult getCheckOutDetail([FromQuery] int uid, [FromQuery]int? cid, [FromQuery]int? pid)
        //{

        //}
    }
}
