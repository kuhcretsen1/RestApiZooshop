namespace ZooShop.Application.Commands
{
    public class UpdateOrderCommand
    {
        public int OrderId { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>();
        public decimal TotalAmount { get; set; }
    }
}