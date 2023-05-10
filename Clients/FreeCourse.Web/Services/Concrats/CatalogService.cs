using CourseServiceCatalog.Shares;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrats;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _client;

    public CatalogService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<CourseViewModel>> GetAllCourseAsync()
    {
        var courses = await _client.GetAsync("Course");
        if (!courses.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await courses.Content.ReadFromJsonAsync<ResponseDto<List<CourseViewModel>>>();

        return responseSuccess.Data;

    }

    public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
    {
        var categories = await _client.GetAsync("Category");
        if (!categories.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await categories.Content.ReadFromJsonAsync<ResponseDto<List<CategoryViewModel>>>();

        return responseSuccess.Data;
    }

    public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
    {
        //[controller]/GetAllByUserId/{userId}
        var course = await _client.GetAsync($"Course/GetAllByUserId/{userId}");
        if (!course.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await course.Content.ReadFromJsonAsync<ResponseDto<List<CourseViewModel>>>();

        return responseSuccess.Data;
    }

    public async Task<bool> DeleteCourseAsync(string courseId)
    {
        var response = await _client.DeleteAsync($"course/{courseId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<CourseViewModel> GetByCourseIdAsync(string courseId)
    {
        var course = await _client.GetAsync($"Course/{courseId}");
        if (!course.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await course.Content.ReadFromJsonAsync<ResponseDto<CourseViewModel>>();

        return responseSuccess.Data;
    }

    public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
    {
        var response = await _client.PostAsJsonAsync<CourseCreateInput>("course", courseCreateInput);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
    {
        var response = await _client.PutAsJsonAsync<CourseUpdateInput>("course",courseUpdateInput);
        return response.IsSuccessStatusCode;
    }
}