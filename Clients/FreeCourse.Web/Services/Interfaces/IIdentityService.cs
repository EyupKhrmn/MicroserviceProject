using CourseServiceCatalog.Shares;
using FreeCourse.Web.Models;
using IdentityModel;
using IdentityModel.Client;

namespace FreeCourse.Web.Services.Interfaces;

public interface IIdentityService
{
    Task<ResponseDto<bool>> SignIn(SignInInput signInInput);

    Task<TokenResponse> GetAccessTokenByRefreshToken();

    Task RevokeRefreshToken();
}