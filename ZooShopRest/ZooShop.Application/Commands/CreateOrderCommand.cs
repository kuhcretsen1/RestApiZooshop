namespace ZooShop.Application.Commands
{
    public class CreateOrderCommand
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<int> ProductIds { get; set; } = new List<int>();
        public decimal TotalAmount { get; set; }
    }
}