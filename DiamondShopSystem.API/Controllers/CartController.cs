using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.Models;

using Services.Diamonds;
using Services.Products;
using Services.Users;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICoverService _coverService;
        private readonly IMetaltypeService _metaltypeService;
        private readonly ISizeService _sizeService;
        private readonly IDiamondService _diamondService;    
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICoverMetaltypeService _coverMetaltypeService;
        public CartController(ICartService cartService, ICoverService coverService, IMetaltypeService metaltypeService, ISizeService sizeService,IDiamondService diamondService,IOrderService orderService,IProductService productService,
            ICoverMetaltypeService coverMetaltypeService)
        {
            _cartService = cartService;
            _coverService = coverService;
            _metaltypeService = metaltypeService;
            _sizeService = sizeService;
            _diamondService = diamondService;
            _orderService = orderService;
            _productService = productService;
            _coverMetaltypeService = coverMetaltypeService;
        }
        [HttpGet]
        public decimal getTotal(int id)
        {
            var ca = _cartService.GetCartFromCus(id).CartProducts;
            var cartItems = ca.Select(c =>
            {
                return new CartItemRespone
                {
                    pid = c.Product.ProductId,
                    name1 = c.Product.ProductName,
                    img = _coverMetaltypeService.GetCoverMetaltype(c.Product.CoverId, c.Product.MetaltypeId).ImgUrl,
                    price = _orderService.GetTotalPrice(_productService.GetProductById(c.ProductId)),
                    quantity = c.Quantity,
                    size = _sizeService.GetSizeById((int)c.Product.SizeId).SizeValue,
                    metal = _metaltypeService.GetMetaltypeById((int)c.Product.MetaltypeId).MetaltypeName,
                    cover = _coverService.GetCoverById((int)c.Product.CoverId).CoverName,
                    coverPrice = (decimal)_coverService.GetCoverById((int)c.Product.CoverId).UnitPrice + (decimal)_sizeService.GetSizeById((int)c.Product.SizeId).SizePrice + (decimal)_metaltypeService.GetMetaltypeById((int)c.Product.MetaltypeId).MetaltypePrice,
                    diamond = _diamondService.GetDiamondById((int)c.Product.DiamondId).DiamondName,
                    diamondPrice = _diamondService.GetDiamondById((int)c.Product.DiamondId).Price,
                    labor = (decimal)c.Product.UnitPrice,
                };
            }).ToList();
            CartRespone car = new CartRespone();
            car.items = (List<CartItemRespone>)cartItems;
            return car.TotalPrice;
        }
        [HttpGet("{id}")]
        public IActionResult getCart(int id)
        {
            if (_cartService.GetCartFromCus(id).CartProducts == null)
            {
                return BadRequest("Cart is empty");
            }
            
                var ca = _cartService.GetCartFromCus(id).CartProducts;
                var cartItems = ca.Select(c =>
                {
                    return new CartItemRespone
                    {
                        pid = c.Product.ProductId,
                        name1 = c.Product.ProductName,
                        price = _productService.GetProductTotal(c.ProductId),
                        img = _coverMetaltypeService.GetCoverMetaltype(c.Product.CoverId,c.Product.MetaltypeId).ImgUrl,
                        quantity = c.Quantity,
                        size = _sizeService.GetSizeById((int)c.Product.SizeId).SizeValue,
                        metal = _metaltypeService.GetMetaltypeById((int)c.Product.MetaltypeId).MetaltypeName,
                        cover = _coverService.GetCoverById((int)c.Product.CoverId).CoverName,
                        coverPrice = (decimal)_coverService.GetCoverById((int)c.Product.CoverId).UnitPrice + (decimal)_sizeService.GetSizeById((int)c.Product.SizeId).SizePrice + (decimal)_metaltypeService.GetMetaltypeById((int)c.Product.MetaltypeId).MetaltypePrice,
                        diamond = _diamondService.GetDiamondById((int)c.Product.DiamondId).DiamondName,
                        diamondPrice = _diamondService.GetDiamondById((int)c.Product.DiamondId).Price,
                        labor = (decimal)c.Product.UnitPrice,
                        coverId = c.Product.CoverId,
                        diamondId = c.Product.DiamondId,
                        status = c.Product.Status,
                    };
                }).ToList();
            
            CartRespone car = new CartRespone();
            car.items = cartItems;
            return Ok(car);
            //try
            //{
            //    // Get the cart from the customer
            //    var cart = _cartService.GetCartFromCus(id);

            //    // Check if the cart is null or empty
            //    if (cart == null || !cart.CartProducts.Any())
            //    {
            //        return NotFound("Cart not found or is empty.");
            //    }

            //    // Get cart products
            //    var ca = cart.CartProducts;

            //    // Map cart products to response
            //    var cartItems = ca.Select(c =>
            //    {
            //        var product = c.Product;

            //        // Check if the product is null
            //        if (product == null)
            //        {
            //            throw new Exception("Product not found in cart.");
            //        }

            //        var size = _sizeService.GetSizeById((int)product.SizeId);
            //        var metal = _metaltypeService.GetMetaltypeById((int)product.MetaltypeId);
            //        var cover = _coverService.GetCoverById((int)product.CoverId);
            //        var diamond = _diamondService.GetDiamondById((int)product.DiamondId);

            //        // Create response object
            //        return new CartItemRespone
            //        {
            //            pid = product.ProductId,
            //            name1 = product.ProductName,
            //            price = _productService.GetProductTotal(product.ProductId),
            //            quantity = c.Quantity,
            //            size = size?.SizeValue ?? "N/A",
            //            metal = metal?.MetaltypeName ?? "N/A",
            //            cover = cover?.CoverName ?? "N/A",
            //            coverPrice = (cover?.UnitPrice ?? 0) + (size?.SizePrice ?? 0) + (metal?.MetaltypePrice ?? 0),
            //            diamond = diamond?.DiamondName ?? "N/A",
            //            diamondPrice = diamond?.Price ?? 0,
            //            labor = product.UnitPrice ?? 0
            //        };
            //    }).ToList();

            //    // Create and return the cart response
            //    var car = new CartRespone
            //    {
            //        items = cartItems
            //    };
            //    return Ok(car);
        
            //catch (Exception ex)
            //{
            //    // Log the exception and return an error response
            //    // You can use a logging framework like Serilog or NLog
            //    // Log.Error(ex, "Error retrieving cart");

            //    return StatusCode(500, "An error occurred while processing your request.");
            //}
        }
        [HttpPost("addToCart")]
        public IActionResult addtoCart([FromBody] CartRequest c)
        {
            CartProduct p = _cartService.AddToCart(c.id, c.pid);
            return Ok(new {p.ProductId,p.Quantity});
        }
        [HttpPost("removeFromCart")]
        public IActionResult removeFromCartAsync([FromBody] CartRequest c)
        {
            _cartService.RemoveFromCart(c.id, c.pid);
            return Ok();
        }
        [HttpPut("updateCart")]

        public  IActionResult updateCart(int id,int pid,int quantity)
        {
            CartProduct p =_cartService.updateCart(id, pid,quantity);
            decimal total = getTotal(id);
            return Ok(new { p.Quantity, total });
        }
        [HttpPost("emptyCart")]

        public IActionResult emptyCart([FromBody]int id)//sẽ thêm vào order
        {
            _cartService.emptyCart(id);
            return Ok();
        }
        //public void MergeCarts(int userId, List<ProductQuantity> guestCart)
        //{
        //    var userCart = _cartService.GetCartFromCus(userId);
        //    if (userCart == null)
        //    {
        //        userCart = new Cart { CartId = userId, CartProducts = new List<CartProduct>() };
        //    }

        //    foreach (var guestItem in guestCart)
        //    {
        //        var existingItem = userCart.CartProducts.FirstOrDefault(cp => cp.ProductId == guestItem.ProductId);
        //        if (existingItem != null)
        //        {
        //            existingItem.Quantity += guestItem.Quantity;
        //        }
        //        else
        //        {
        //            _cartService.AddToCartMany(userId, guestItem.ProductId, guestItem.Quantity);
        //        }
        //    }
        //    _cartService.saveCart(userCart);
        //}
    }
}
