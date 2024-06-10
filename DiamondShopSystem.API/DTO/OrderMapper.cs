using Model.Models;

namespace DiamondShopSystem.API.DTO
{
    public static class OrderMapper
    {
        public static orderRespone MapToOrderResponse(Order order)
        {
            return new orderRespone
            {
                status = order.Status,
                total = order.TotalAmount,
                items = order.ProductOrders.Select(po => new orderItem
                {
                    pid = po.Product.ProductId,
                    name1 = po.Product.ProductName,
                    quantity = po.Quantity,
                    img = po.Product.Pp,
                }).ToList()
            };
        }
        
    }
}
