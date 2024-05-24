using DiamondShopSystem.API.Models;
using DiamondShopSystem.API.DAO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiamondShopSystem.API.Controllers
{
    [Route("login")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public CustomerController() { }

        


        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            var customerDAO = new CustomerDAO();
            //print all students
            return customerDAO.getAllCustomers().ToArray();

        }

        // GET api/<CustomerController>/5
        [HttpGet("{name}")]
        public string Get(Customer name)
        {
            var customerDAO = new CustomerDAO();
            //print all students
            return customerDAO.getCustomerByName(name.Name).ToString();
        }

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
    }
}
