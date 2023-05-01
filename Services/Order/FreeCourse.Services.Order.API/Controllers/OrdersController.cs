using CourseServiceCatalog.Shares;
using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Order.API.Controllers;

public class OrdersController : CustomBaseController
{
    private readonly IMediator _mediator;
    private readonly ISharedIdentityServer _sharedIdentityServer;

    public OrdersController(IMediator mediator, ISharedIdentityServer sharedIdentityServer)
    {
        _mediator = mediator;
        _sharedIdentityServer = sharedIdentityServer;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrder()
    {
        var response = await _mediator.Send(new GetOrdersByUserIdQuery{UserId = _sharedIdentityServer.GetUserId});

        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
    {
        var response = await _mediator.Send(createOrderCommand);

        return CreateActionResultInstance(response);
    }
}