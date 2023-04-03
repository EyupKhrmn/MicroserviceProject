using System.Collections.Generic;
using System.Threading.Tasks;
using FreeCouse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace FreeCouse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var exisUser = await _userManager.FindByEmailAsync(context.UserName);

            if (exisUser == null)
            {
                var errors = new Dictionary<string, object>();
                
                errors.Add("errors",new List<string>{"Email veya Şifreniz yanlış !"});

                context.Result.CustomResponse = errors;
                
                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(exisUser, context.Password);

            if (passwordCheck == false)
            {
                var errors = new Dictionary<string, object>();
                
                errors.Add("errors",new List<string>{"Email veya Şifreniz yanlış !"});

                context.Result.CustomResponse = errors;
                return;
            }

            context.Result =
                new GrantValidationResult(exisUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}