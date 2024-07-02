using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Pkcs;

namespace DiamondShopSystem.API.DTO
{
    public class ShippingRequest
    {
        public string Status { get; set; }
        public int? OrderId { get; set; }
        public int SaleStaffId { get; set; }
    }
}
