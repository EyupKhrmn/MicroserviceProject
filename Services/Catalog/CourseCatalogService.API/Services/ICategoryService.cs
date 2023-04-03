using CourseCatalogService.API.Dtos;
using CourseServiceCatalog.Shares;

namespace CourseCatalogService.API.Services
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();

        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);

        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto category);
    }
}
