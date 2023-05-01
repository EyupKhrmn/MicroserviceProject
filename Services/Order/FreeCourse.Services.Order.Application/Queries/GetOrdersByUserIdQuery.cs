using Azure;
using CourseServiceCatalog.Shares;
using FreeCourse.Services.Order.Application.Dtos;
using MediatR;

namespace FreeCourse.Services.Order.Application.Queries;

public class GetOrdersByUserIdQuery : IRequest<ResponseDto<List<OrderDto>>>
{
    public string UserId { get; set; }
}