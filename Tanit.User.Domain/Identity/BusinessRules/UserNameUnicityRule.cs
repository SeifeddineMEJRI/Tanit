using FluentResults;
using Microsoft.AspNetCore.Identity;
using Tanit.User.Domain.Identity.Model;
using Tanit.User.Domain.Identity.Request;

namespace Tanit.User.Domain.Identity.BusinessRules
{
    public class UserNameUnicityRule : IUserValidationRule
    {
        private readonly UserManager<TanitUser> _userManager;

        public UserNameUnicityRule(UserManager<TanitUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> IsValidAsync(UserRegistrationRequest request)
        {
            var userWithSameUsername = await _userManager.FindByNameAsync(request.UserName);

            if (userWithSameUsername is not null)
            {
                return Result.Fail("Username already taken.");
            }
            return Result.Ok();
        }
    }

}
