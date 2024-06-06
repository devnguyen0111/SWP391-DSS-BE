using Model.Models;

namespace DiamondShopSystem.API.DTO
{
    public static class OrderMapper
    {
        public static orderRespone MapToOrderResponse(Order order)
        {
            return new orderRespone
            {
                id = order.OrderId,
                status = order.Status,
                items = order.ProductOrders.Select(po => new orderItem
                {
                    pid = po.Product.ProductId,
                    name1 = po.Product.ProductName,
                    quantity = po.Quantity,
                    total = po.Quantity,
                    img = po.Product.Pp,
                    size = "hihi",  // will fix later :P
                    metal = "hihi"  //  will fix later :P
                }).ToList()
            };
        }
    }
}
