using CourseServiceCatalog.Shares;

namespace FreeCourse.Services.Discount.Services;

public interface IDiscountService
{
    Task<ResponseDto<List<Entities.Discount>>> GetAll();

    Task<ResponseDto<Entities.Discount>> GetById(int id);

    Task<ResponseDto<NoContent>> Save(Entities.Discount discount);

    Task<ResponseDto<NoContent>> Update(Entities.Discount discount);

    Task<ResponseDto<NoContent>> Delete(int id);

    Task<ResponseDto<Entities.Discount>> GetByCodeAndUserId(string code, string userId);
}