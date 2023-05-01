using Azure;
using CourseServiceCatalog.Shares;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Application.Mapping;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Services.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreeCourse.Services.Order.Application.Handlers;

public class GetOrdersByUserIdHandler : IRequestHandler<GetOrdersByUserIdQuery,ResponseDto<List<OrderDto>>>
{

    private readonly OrderDbContext _dbContext;

    public GetOrdersByUserIdHandler(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _dbContext.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId)
            .ToListAsync();

        if (!orders.Any())
        {
            return ResponseDto<List<OrderDto>>.Success(new List<OrderDto>(), 200);
        }

        var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

        return ResponseDto<List<OrderDto>>.Success(ordersDto, 200);
    }
}