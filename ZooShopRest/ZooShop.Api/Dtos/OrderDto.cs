using ZooShop.Domain.Orders;
using ZooShop.Domain.Products;

namespace ZooShop.Api.Dtos;

    public record OrderDto(Guid? Id, DateTime OrderDate, decimal TotalAmount, List<ProductDto> Products)
    {
        public static OrderDto FromDomainModel(Order order)
            => new(
                order.Id.Value, 
                order.OrderDate, 
                order.TotalAmount, 
                order.Products.Select(p => ProductDto.FromDomainModel(p)).ToList());
    }

