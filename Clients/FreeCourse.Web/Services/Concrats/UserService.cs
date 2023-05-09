using System.Runtime.CompilerServices;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrats;

public class UserService : IUserService
{

    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    public Task<UserViewModel> GetUser()
    {
        return _client.GetFromJsonAsync<UserViewModel>("/api/User/GetUser");
    }
}