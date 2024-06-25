using Model.Models;
using Repository.Products;
using Repository.Users;

namespace Services.Users
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICoverRepository _coverRepository;
        private readonly IMetaltypeRepository _metaltypeRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository
            , IMetaltypeRepository metaltypeRepository, ICoverRepository coverRepository, ISizeRepository sizeRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _metaltypeRepository = metaltypeRepository;
            _coverRepository = coverRepository;
            _sizeRepository = sizeRepository;
            _productRepository = productRepository;
        }
        public decimal GetTotalPrice(Product product)
        {
            decimal totalPrice = product.UnitPrice ?? 0;
            Cover c = _coverRepository.GetCoverById((int)product.CoverId);
            Size s = _sizeRepository.GetSizeById((int)product.CoverId);
            Metaltype mt = _metaltypeRepository.GetMetaltypeById((int)product.MetaltypeId);
            totalPrice += (decimal)product.Diamond.Price;
            totalPrice += (decimal)c.UnitPrice;
            totalPrice += (decimal)mt.MetaltypePrice;
            totalPrice += (decimal)s.SizePrice;
            return totalPrice;
        }
        public Order createOrderFromCart(int uid, int sid,string address,string phonenum)
        {
            Cart cart = _cartRepository.getCartFromCustomer(uid);
            if (cart == null || !cart.CartProducts.Any())
            {
                throw new Exception("Cart is empty or not found.");
            }
            decimal totalAmount = cart.CartProducts.Sum(cp => cp.Quantity * GetTotalPrice(_productRepository.GetProductById(cp.ProductId)));
            Order newo = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = "processing",
                CusId = uid,
                ShippingMethodId = sid,
                DeliveryAddress = address,
                ContactNumber = phonenum,
                
            };
            var cartItems = cart.CartProducts;
            var orderProducts = cartItems.Select(c =>
            {
                return new ProductOrder
                {
                    ProductId = c.ProductId,
                    OrderId = newo.OrderId,
                    Quantity = c.Quantity,
                };
            });
            newo.ProductOrders = orderProducts.ToList();
            _orderRepository.createOrder(newo);
            return newo;
        }
        public Order createOrderDirectly(int uid, int pid, int sid,string address,string phonenum)
        {

            Order newo = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = GetTotalPrice(_productRepository.GetProductById(pid)),
                Status = "pending",
                CusId = uid,
                ShippingMethodId = sid,
                DeliveryAddress = address,
                ContactNumber = phonenum,
            };
            var orderProducts = new ProductOrder
            {
                ProductId = pid,
                OrderId = newo.OrderId,
                Quantity = 1,
            };
            List<ProductOrder> l = [orderProducts];
            newo.ProductOrders = l;
            _orderRepository.createOrder(newo);
            return newo;
        }
        public List<Order> getOrderByStatus(int uid, string status)
        {
            return _orderRepository.getOrderby(uid, status);
        }
        public List<Order> getOrders(int uid)
        {
            return _orderRepository.getOrders(uid);
        }
    }
}
