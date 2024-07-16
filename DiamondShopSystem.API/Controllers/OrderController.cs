using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using PayPal.Api;
using Services.EmailServices;
using Services.Products;
using Services.Users;
using Services.Utility;
using static Repository.Shippings.ShippingRepository;
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
        private readonly ICoverMetaltypeService _coverMetaltypeService;
        private readonly IReviewService _reviewService;
        public OrderController(IOrderService orderService,IVoucherService voucherService, IEmailService emailService, IProductService productService, ICustomerService customerService,
            ICoverMetaltypeService coverMetaltypeService,IReviewService i)
        {
            _orderService = orderService;
            _emailService = emailService;
            _voucherService = voucherService;
            _productService = productService;
            _customerService = customerService;
            _coverMetaltypeService = coverMetaltypeService;
            _reviewService = i;
        }

        //[HttpPost]
        //[Route("createOrderDirecly")]
        //public IActionResult CreateOrder([FromBody] OrderRequest request)
        //{
        //    Order o = _orderService.createOrderDirectly((int)request.CusId, request.pid, (int)request.ShippingMethodId,request.deliveryAddress,request.contactNumber);
        //    return Ok(o);
        //}
        [HttpPost]
        [Route("createOrderFromCart")]
        public IActionResult CreateOrderFromCart([FromBody] OrderRequestCart request)
        {
            Order o = _orderService.createOrderFromCart((int)request.CusId, (int)request.ShippingMethodId, request.deliveryAddress, request.contactNumber);
            return Ok(o);
        }
        //[Authorize]
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
                if(status == "Delivered" || status == null)
                {
                    var orderHistoryResponses2 = orders.Select(o => new OrderHistoryResponse
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
                            ReviewCheck = _reviewService.HasReview(po.ProductId,customerId),
                            Total = po?.ProductId != null ? _productService.GetProductTotal(po.ProductId) : 0, // Added null check
                            Img = _coverMetaltypeService.GetCoverMetaltype(_productService.GetProductById(po.ProductId).CoverId, _productService.GetProductById(po.ProductId).MetaltypeId).ImgUrl // Adjust according to your actual model

                        }).ToList() ?? new List<OrderHistoryItem>() // Added null check
                    }).ToList() ?? new List<OrderHistoryResponse>(); // Added null check

                    return Ok(orderHistoryResponses2);
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
                        Img =_coverMetaltypeService.GetCoverMetaltype(_productService.GetProductById(po.ProductId).CoverId, _productService.GetProductById(po.ProductId).MetaltypeId).ImgUrl // Adjust according to your actual model
                       
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
        [HttpGet("getOrderDetail")]
        public ActionResult<List<OrderHistoryResponse>> getOrderDetail(int orderId)
        {
            try
            {
                var o = new Order();
              o =_orderService.getAllOrders().FirstOrDefault(c => c.OrderId == orderId);

                if (o == null)
                {
                    return NotFound(new { Message = "No orders found for the given customer ID and status." });
                }

                var orderHistoryResponses = new OrderHistoryResponse
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
                        Img = _coverMetaltypeService.GetCoverMetaltype(_productService.GetProductById(po.ProductId).CoverId, _productService.GetProductById(po.ProductId).MetaltypeId).ImgUrl // Adjust according to your actual model
                    }).ToList() ?? new List<OrderHistoryItem>() // Added null check
                }; // Added null check

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
                Order newOrder = CreateOrderFromProducts1(request.UserId, request.ShippingMethodId, request.DeliveryAddress, request.ContactNumber, request.Products,request.voucherName);
                return Ok(newOrder.OrderId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("applyVoucher")]
        public IActionResult applyVoucher([FromBody] VoucherRequest1 vq)
        {
            Voucher voucher = _voucherService.GetVoucherByName(vq.voucherName); // Assuming you have a service to get voucher by name
            if (voucher != null)
            {
                decimal totalAmount = vq.Products.Sum(pq => pq.Quantity * _productService.GetProductTotal(pq.ProductId));
                if (voucher.Quantity <= 0)
                {
                    return BadRequest("Voucher is no longer valid.");
                }

                if (totalAmount >= voucher.BottomPrice && totalAmount <= voucher.TopPrice)
                {
                    totalAmount =totalAmount - ApplyVoucherDiscount(totalAmount, voucher);
                   
                    return Ok(totalAmount);
                }
                else
                {
                    return BadRequest("Total amount does not meet the voucher's price requirements.");
                }
            }
            else
            {
                return BadRequest("Voucher not found.");
            }
        }
        
        private Order CreateOrderFromProducts1(int uid, int sid, string address, string phonenum, List<DTO.ProductQuantity> products, string voucherName)
        {
            if (products == null || !products.Any())
            {
                throw new Exception("Product list is empty or not provided.");
            }

            decimal totalAmount = products.Sum(pq => pq.Quantity * _productService.GetProductTotal(pq.ProductId));

            // Check for voucher
            Voucher voucher = null;
            if (!string.IsNullOrEmpty(voucherName))
            {
                voucher = _voucherService.GetVoucherByName(voucherName); // Assuming you have a service to get voucher by name
                if (voucher != null)
                {
                    if (voucher.Quantity <= 0)
                    {
                        throw new Exception("Voucher is no longer valid.");
                    }

                    if (totalAmount >= voucher.BottomPrice && totalAmount <= voucher.TopPrice)
                    {
                        totalAmount = ApplyVoucherDiscount(totalAmount, voucher);
                        voucher.Quantity -= 1; // Decrement voucher quantity
                        _voucherService.updateVoucher(voucher); // Update the voucher in the database
                    }
                    else
                    {
                        throw new Exception("Total amount does not meet the voucher's price requirements.");
                    }
                }
                else
                {
                    throw new Exception("Voucher not found.");
                }
            }

            Order newOrder = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = "Paid",
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

        private decimal ApplyVoucherDiscount(decimal totalAmount, Voucher voucher)
        {
            // Assuming the voucher has a Rate property that represents a percentage discount
            return (decimal)(totalAmount - (totalAmount * (voucher.Rate / 100m)));
        }

        [HttpGet]
        [Route("getAllOrders")]
        public ActionResult<List<OrderHistoryResponse1>> getAllOrders(string? status)
        {
            try
            {
                var orders = new List<Order>();
                if (status.IsNullOrEmpty())
                {
                    orders = _orderService.getAllOrders();
                } else
                {
                    orders = _orderService.getAllOrders().Where(o => o.Status == status).ToList();
                }
                

                if (orders == null || !orders.Any())
                {
                    return NotFound(new { Message = "No orders found for the given status." });
                }

                var orderHistoryResponses = orders.Select(o => new OrderHistoryResponse1
                {
                    OrderId = o?.OrderId ?? 0, 
                    OrderDate = o?.OrderDate ?? DateTime.MinValue, 
                    Status = o?.Status ?? "Unknown", 
                    ShippingMethodName = o?.ShippingMethod?.MethodName ?? "Unknown Shipping Method", 
                    TotalAmount = o?.TotalAmount ?? 0, 
                     
                }).ToList() ?? new List<OrderHistoryResponse1>(); 

                return Ok(orderHistoryResponses);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
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
                        imgUrl = _coverMetaltypeService.GetCoverMetaltype(product.CoverId,product.MetaltypeId).ImgUrl,
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

        [HttpPost("cancelOrderByOrderId/{orderId}")]
        public async Task<IActionResult> CancelOrderByOrderId(string orderId)
        {
            try
            {
                bool result = await _orderService.CancelOrderAsync(orderId);

                if (!result)
                {
                    return NotFound(new { message = "Order not found" });
                }

                return Ok(new { message = "Order cancelled successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        //[HttpGet]
        //public IActionResult getCheckOutDetail([FromQuery] int uid, [FromQuery]int? cid, [FromQuery]int? pid)
        //{

        //}
    }
}
