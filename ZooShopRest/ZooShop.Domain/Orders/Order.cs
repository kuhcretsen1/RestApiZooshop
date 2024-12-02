using ZooShop.Domain.Products;
using ZooShop.Domain.Categorys;
using ZooShop.Domain.Orders;

namespace ZooShop.Domain.Orders
{
    public class Order
    {
        public OrderId Id { get; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }

        private readonly List<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        private Order(OrderId id, decimal totalAmount)
        {
            Id = id;
            OrderDate = DateTime.UtcNow;
            TotalAmount = totalAmount;
        }

        public static Order New(OrderId id, decimal totalAmount) => new(id, totalAmount);

        public void AddProduct(Product product) => _products.Add(product);
        public void UpdateTotalAmount(decimal totalAmount)
        {
            TotalAmount = totalAmount;
        }
    }
}