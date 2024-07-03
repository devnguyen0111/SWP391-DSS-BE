using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using PayPal.Api;
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
        private readonly ICustomerService _customerService;
        public OrderController(IOrderService orderService,IVoucherService voucherService, IEmailService emailService, IProductService productService, ICustomerService customerService)
        {
            _orderService = orderService;
            _emailService = emailService;
            _voucherService = voucherService;
            _productService = productService;
            _customerService = customerService;
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
        public ActionResult<List<OrderHistoryResponse>> GetOrderHistory(int customerId, string? status)
        {
            try
            {
                var orders = new List<Order>();
                if(status.IsNullOrEmpty())
                {
                     orders = _orderService.getOrders(customerId);
                }
                else
                {
                     orders = _orderService.getOrderByStatus(customerId, status);

                }

                if (orders == null || !orders.Any())
                {
                    return NotFound(new { Message = "No orders found for the given customer ID and status." });
                }

                var orderHistoryResponses = orders.Select(o => new OrderHistoryResponse
                {
                    OrderId = o?.OrderId ?? 0, // Added null check
                    OrderDate = o?.OrderDate ?? DateTime.MinValue, // Added null check
                    Status = o?.Status ?? "Unknown", // Added null check
                    ShippingMethodName = o?.ShippingMethod?.MethodName ?? "Unknown Shipping Method", // Added null check
                    TotalAmount = o?.TotalAmount ?? 0, // Added null check
                    Items = o?.ProductOrders?.Select(po => new OrderHistoryItem
                    {
                        PId = po?.ProductId ?? 0, // Added null check
                        SizeName = po?.ProductId != null ? _productService.GetProductById(po.ProductId)?.Size?.SizeValue ?? "Unknown Size" : "Unknown Size", // Added null check
                        DiamondName = po?.ProductId != null ? _productService.GetProductById(po.ProductId)?.Diamond?.DiamondName ?? "Unknown Diamond" : "Unknown Diamond", // Added null check
                        MetaltypeName = po?.ProductId != null ? _productService.GetProductById(po.ProductId)?.Metaltype?.MetaltypeName ?? "Unknown Metal Type" : "Unknown Metal Type", // Added null check
                        Name = po?.Product?.ProductName ?? "Unknown Product", // Added null check
                        Total = po?.ProductId != null ? _productService.GetProductTotal(po.ProductId) : 0, // Added null check
                        Img = "https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/illustration-gallery-icon_53876-27002.avif?alt=media&token=037e0d50-90ce-4dd4-87fc-f54dd3dfd567" // Adjust according to your actual model
                    }).ToList() ?? new List<OrderHistoryItem>() // Added null check
                }).ToList() ?? new List<OrderHistoryResponse>(); // Added null check

                return Ok(orderHistoryResponses);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework for this)
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                Order newOrder = CreateOrderFromProducts1(request.UserId, request.ShippingMethodId, request.DeliveryAddress, request.ContactNumber, request.Products);
                return Ok(newOrder.OrderId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        private Order CreateOrderFromProducts1(int uid, int sid, string address, string phonenum, List<DTO.ProductQuantity> products)
        {
            if (products == null || !products.Any())
            {
                throw new Exception("Product list is empty or not provided.");
            }

            decimal totalAmount = products.Sum(pq => pq.Quantity * _productService.GetProductTotal(pq.ProductId));

            Order newOrder = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = "processing",
                CusId = uid,
                ShippingMethodId = sid,
                DeliveryAddress = address,
                ContactNumber = phonenum,
            };

            var orderProducts = products.Select(pq => new ProductOrder
            {
                ProductId = pq.ProductId,
                OrderId = newOrder.OrderId,
                Quantity = pq.Quantity,
            }).ToList();

            newOrder.ProductOrders = orderProducts;

            _orderService.addOrder(newOrder);
            return newOrder;
        }
        [HttpPost]
        [Route("checkoutInfo")]
        public IActionResult GetCheckoutInfo([FromBody] CheckoutRequest request)
        {
            if (request == null || request.Products == null || !request.Products.Any())
            {
                return BadRequest(new { message = "Invalid request or empty product list." });
            }

            try
            {
                var user = _customerService.GetCustomer(request.UserId);
                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }
                decimal totalAmount = request.Products.Sum(pq => pq.Quantity * _productService.GetProductTotal(pq.ProductId));

                var products = request.Products.Select(pq =>
                {
                    var product = _productService.GetProductById(pq.ProductId);
                    return new ProductInfo
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        imgUrl = "https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/illustration-gallery-icon_53876-27002.avif?alt=media&token=037e0d50-90ce-4dd4-87fc-f54dd3dfd567",
                        Price = _productService.GetProductTotal(product.ProductId),
                        Quantity = pq.Quantity
                    };
                }).ToList();

                var address = _customerService.getCustomerAddress(request.UserId);

                var checkoutInfo = new CheckoutInfoResponse
                {
                    UserInfo = new UserInfo
                    {
                        UserId = user.CusId,
                        firstName = user.CusFirstName,
                        lastName = user.CusLastName,
                        Email = user.Cus.Email,
                        phoneNum = user.CusPhoneNum,
                    },
                    Products = products,
                    total = totalAmount,
                    shippingMethods = _orderService.GetShippingMethods().Select(c => 
                    {
                        return new DTO.ShippingMethod1
                        {
                            id = c.ShippingMethodId,
                            cost = c.Cost,
                            name = c.MethodName,
                        };
                    }).ToList(),
                    ShippingAddress1 = new Address1
                    {
                        Street = address.Street,
                        City = address.City,
                        State = address.State,
                        ZipCode = address.ZipCode,
                        Country = address.Country,
                    },
                };

                return Ok(checkoutInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        //[HttpGet]
        //public IActionResult getCheckOutDetail([FromQuery] int uid, [FromQuery]int? cid, [FromQuery]int? pid)
        //{

        //}
    }
}
