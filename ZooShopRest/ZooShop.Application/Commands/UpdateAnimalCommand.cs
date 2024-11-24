namespace ZooShop.Application.Commands
{
    public class UpdateAnimalCommand
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}