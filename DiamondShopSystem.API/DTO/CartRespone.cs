using Model.Models;
using System.Runtime.CompilerServices;

namespace DiamondShopSystem.API.DTO
{
    public class CartRespone
    {
        public int quantity => items.Count;
        public decimal TotalPrice => items.Sum(item => item.price * item.quantity);
        public List<CartItemRespone> items { get; set; }
    }
    public class CartItemRespone
    {
        
        public int pid { get; set; }
        public string name1 { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public decimal total {
            get
            { return quantity * price; }

            set { 
            
            }

        }
        public string img { get; set; }
        public string size { get; set; }
        public string metal { get; set; }
        public string diamond { get; set; }
        public decimal diamondPrice { get; set; }
        public string cover { get; set; }
        public decimal coverPrice { get; set; }
        public decimal labor {  get; set; }
        public CartItemRespone()
        {
        }

        public CartItemRespone(int id,string name1, decimal price, int quantity, string size1, string metal1)
        {
            this.pid = id;
            this.name1 = name1;
            this.price = price;
            this.quantity = quantity;
            this.total = price * quantity;
            this.img = size1 + metal1;
            this.size = size1;
            this.metal = metal1;
        }
    }
}
