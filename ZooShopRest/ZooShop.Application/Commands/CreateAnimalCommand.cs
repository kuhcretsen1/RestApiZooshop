namespace ZooShop.Application.Commands
{
    public class CreateAnimalCommand
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}