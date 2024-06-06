using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Tnef;
using Model.Models;
using Services;
using Services.Users;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        //tạm
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult customer(int id)
        {
            return Ok(_customerService.GetCustomer(id));
        }
        [HttpGet("customer/{id}/profile")]
        public IActionResult profile(int id)
        {
            Address d = _customerService.getCustomerAddress(id);
            var ad = new
            {
                d.City,
                d.Country,
                d.State,
                d.Street,
                d.ZipCode,
            };
            var mail = _customerService.getmail(id);
            var customer = _customerService.GetCustomer(id);
            var customerinfo = new {customer.CusLastName,customer.CusFirstName,customer.CusPhoneNum,mail};
            return Ok(new {ad,customerinfo});
        }
        [Authorize]
        [HttpGet("address/{id}")]
        public IActionResult getCustomerAddress(int id)
        {
            Address d = _customerService.getCustomerAddress(id);
            return Ok(new
            {
                d.City,
                d.Country,
                d.State,
                d.Street,
                d.ZipCode,
            });
        }
        [Authorize]
        [HttpPost("address/update")]
        public IActionResult updateCustomerAddres([FromBody] AddressRequest request) {
            _customerService.updateAddress(request.id, request.street, request.city, request.state, request.zipcode);
            Address d = _customerService.getCustomerAddress(request.id);
            return Ok(new
            {
                d.Street,
                d.City,
                d.State,
                d.ZipCode,
            });
        }
        [Authorize]
        [HttpPost("profile/update")]
        public IActionResult updateCustomerProfile([FromBody] profileRequest pq)
        {
            Customer c = _customerService.GetCustomer(pq.id);
            c.CusPhoneNum = pq.phonenumber;
            c.CusLastName = pq.lastName;
            c.CusFirstName = pq.firstName;
            _customerService.updateCustomer(c);
            return Ok(_customerService.GetCustomer(pq.id));
        }
    }
}
