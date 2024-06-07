﻿using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DIAMOND_DBContext _context;
        public OrderRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }
        public List<Order> getOrderby(int uid, string status)
        {
            return _context.Orders.Include(c => c.ProductOrders).ThenInclude(c => c.Product)
            .Where(o => o.Status == status && o.CusId == uid).ToList();
        }
        public void createOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void updateOrder(int oid, string status)
        {
            _context.Orders.FirstOrDefault(c => c.OrderId == oid).Status = status;
            _context.SaveChanges();
        }
    }
}