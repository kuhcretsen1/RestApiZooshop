using ZooShop.Domain.Orders;
using ZooShop.Domain.Categorys;

    
namespace ZooShop.Domain.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Назва продукту
        public decimal Price { get; set; } // Ціна продукту
        public int StockQuantity { get; set; } // Кількість на складі (Додано)

        // Відношення з Category
        public int CategoryId { get; set; } // Ідентифікатор категорії
        public Category Category { get; set; } = null!; // Категорія, до якої належить продукт

        // Відношення з Order
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}