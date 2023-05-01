using System.Runtime.CompilerServices;
using CourseServiceCatalog.Shares;
using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.Model;
using FreeCourse.Services.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreeCourse.Services.Order.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand,ResponseDto<CreatedOrderDto>>
{
    private readonly OrderDbContext _dbContext;

    public CreateOrderCommandHandler(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address(request.Address.Province, request.Address.District,
            request.Address.Street, request.Address.ZipCode, request.Address.Line);

        Domain.Model.Order newOrder = new Domain.Model.Order(request.BuyerId, newAddress);

        request.OrderItems.ForEach(x =>
        {
            newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
        });

        _dbContext.Orders.Add(newOrder);

        await _dbContext.SaveChangesAsync();

        return ResponseDto<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id },200);
    }
}