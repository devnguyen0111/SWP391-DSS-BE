namespace DiamondShopSystem.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }

        public Customer()
        {
        }

        public Customer(int id, string name, string address, string phone, string email, string note)
        {
            Id = id;
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
            Note = note;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Address: {Address}, Phone: {Phone}, Email: {Email}, Note: {Note}";
        }

    }
}
