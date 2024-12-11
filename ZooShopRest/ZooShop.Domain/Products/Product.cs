using ZooShop.Domain.Categorys;  // Додайте цей using, якщо це ще не зроблено
using ZooShop.Domain.Orders;

namespace ZooShop.Domain.Products
{
    public class Product
    {
        public ProductId Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public CategoryId CategoryId { get; private set; }  

        public Product(ProductId id, string name, string description, decimal price, int stockQuantity, CategoryId categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            CategoryId = categoryId;
        }

        public static Product New(ProductId id, string name, string description, decimal price, int stockQuantity, CategoryId categoryId)
        {
            return new Product(id, name, description, price, stockQuantity, categoryId);
        }




        public void UpdateDetails(string name, string description, decimal price, int stockQuantity)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public void AdjustStock(int quantity)
        {
            StockQuantity += quantity;
        }
    }
}