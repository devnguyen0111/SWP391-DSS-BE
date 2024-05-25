using DiamondShopSystem.API.Models;
using DiamondShopSystem.API.DAO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiamondShopSystem.API.Controllers
{
    [Route("dss")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerDAO _customerDAO;

        public CustomerController()
        {
            _customerDAO = new CustomerDAO(); // Hoặc sử dụng Dependency Injection nếu cần
        }


        // GET: api/<CustomerController>
        [HttpGet("api/customers")]
        public IEnumerable<Customer> Get()
        {
            //print all customer
            return _customerDAO.getAllCustomers();

        }

        // GET api/<CustomerController>/5
        [HttpGet("getCustomer")]
        public IActionResult GetCustomer([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest("Id is required.");
            }

            try
            {
                var customer = _customerDAO.GetCustomerByID(id);
                if (customer == null)
                {
                    return NotFound("Customer not found");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

        /*

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }

