using FreeCourse.Services.Order.Domain.Model;

namespace FreeCourse.Services.Order.Application.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    
    public DateTime CreatedDate { get;  set; }

    public Address Address { get;  set; }

    public string BuyerId { get;  set; }

    public List<OrderItemDto> OrderItems { get; set; }
}