using CourseServiceCatalog.Shares;
using FreeCourse.Services.Basket.Dtos;

namespace FreeCourse.Services.Basket.Services;

public interface IBasketService
{
    Task<ResponseDto<BasketDto>> GetBaskete(string userId);

    Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto);

    Task<ResponseDto<bool>> DeleteBasket(string userId);
}