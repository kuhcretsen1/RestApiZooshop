namespace ZooShop.Domain.Customers
{
    public class Customer
    {
        public CustomerId Id { get; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }

        private Customer(CustomerId id, string name, string email, string address, string phoneNumber)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public static Customer New(CustomerId id, string name, string email, string address, string phoneNumber)
            => new(id, name, email, address, phoneNumber);

        public void UpdateDetails(string name, string email, string address, string phoneNumber)
        {
            Name = name;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }
}