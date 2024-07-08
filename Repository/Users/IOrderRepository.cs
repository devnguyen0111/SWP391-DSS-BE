﻿using Model.Models;

namespace Repository.Users
{
    public interface IOrderRepository
    {
        void createOrder(Order order);
        List<Order> getOrderby(int uid, string status);
        void updateOrder(int oid, string status);
        List<Order> getOrders(int uid);
        List<ShippingMethod> GetShippingMethods();
    }
}