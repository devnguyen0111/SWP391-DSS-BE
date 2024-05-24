using DiamondShopSystem.API.Models;

namespace DiamondShopSystem.API.DAO
{
    public class CustomerDAO
    {
        private readonly string filePath = "D:\\StudyWith-Buda\\StudentManagement\\StudentManagement\\StudentManagement\\DAO\\students.txt";

        public List<Customer> getAllCustomers()
        {
            var customer = new List<Customer>();
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var customerInfo = line.Split(',');
                    customer.Add(new Customer
                    {
                        Id = int.Parse(customerInfo[0]),
                        Name = customerInfo[1],
                        Note = customerInfo[2]
                    });
                }
            }
            return customer;
        }

        public Customer getCustomerByName(string name)
        {
            getAllCustomers().FirstOrDefault(c => c.Name == name);
            return getCustomerByName(name);

        }
    }
}
