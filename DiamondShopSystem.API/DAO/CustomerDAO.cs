using DiamondShopSystem.API.Models;

namespace DiamondShopSystem.API.DAO
{
    public class CustomerDAO
    {
        private readonly string filePath = "D:\\DiamondShopSystem\\SWP391-DSS-BE\\DiamondShopSystem.API\\temp\\customers.txt";

        public List<Customer> getAllCustomers()
        {
            var customers = new List<Customer>();
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var customerInfo = line.Split(',');
                    customers.Add(new Customer
                    {
                        Id = int.Parse(customerInfo[0]),
                        Name = customerInfo[1],
                        Address = customerInfo[2],
                        Phone = customerInfo[3],
                        Email = customerInfo[4],
                        Note = customerInfo[5]
                    });
                }
            }
            return customers;
        }

        public Customer GetCustomerByID(int id)
        {
            var customer = getAllCustomers().FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            return customer;
        }
    }
    }

