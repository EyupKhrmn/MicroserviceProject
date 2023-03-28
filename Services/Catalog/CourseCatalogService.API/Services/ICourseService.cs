using CourseCatalogService.API.Dtos;
using CourseServiceCatalog.Shared;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CourseCatalogService.API.Services
{
    public interface ICourseService
    {
        Task<ResponseDto<List<CourseDto>>> GetAllAsync();

        Task<ResponseDto<CourseDto>> GetByIdAsync(string id);

        Task<ResponseDto<List<CourseDto>>> GetAllByUserId(string id);

        Task<ResponseDto<CourseDto>> CreateAsync(CreateCourseDto createCourseDto);

        Task<ResponseDto<NoContent>> UpdateAsync(UpdateCourseDto updateCourseDto);

        Task<ResponseDto<NoContent>> DeleteAsync(string id);
    }
}
