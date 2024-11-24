namespace ZooShop.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    
        public Customer() { }

        // Конструктор із параметрами (якщо потрібен)
        public Customer(string name, string email, string address, string phoneNumber)
        {
            Name = name;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }
}