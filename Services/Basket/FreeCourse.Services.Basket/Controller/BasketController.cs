using CourseServiceCatalog.Shares;
using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

namespace FreeCourse.Services.Basket.Controller;

public class BasketController : CustomBaseController
{
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityServer _sharedIdentityServer;

    public BasketController(IBasketService basketService, ISharedIdentityServer sharedIdentityServer)
    {
        _basketService = basketService;
        _sharedIdentityServer = sharedIdentityServer;
    }

    [HttpGet]
    public async Task<IActionResult> GetBasket()
    {
        var claims = User.Claims;
        
       return CreateActionResultInstance(await _basketService.GetBaskete(_sharedIdentityServer.GetUserId));
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
    {
        var response = await _basketService.SaveOrUpdate(basketDto);

        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasket()
    {
        return CreateActionResultInstance(await _basketService.DeleteBasket(_sharedIdentityServer.GetUserId));
    }
}