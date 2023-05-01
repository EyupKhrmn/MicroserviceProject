using CourseServiceCatalog.Shares;
using FreeCourse.Services.Order.Application.Dtos;
using MediatR;

namespace FreeCourse.Services.Order.Application.Commands;

public class CreateOrderCommand : IRequest<ResponseDto<CreatedOrderDto>>
{
    public string BuyerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public AddressDto Address { get; set; }
}