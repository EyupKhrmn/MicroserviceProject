using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CourseServiceCatalog.Shares
{
    public class SharedIdentityService : ISharedIdentityServer
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}