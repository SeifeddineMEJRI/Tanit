using FluentResults;
using Microsoft.AspNetCore.Identity;
using Tanit.User.Domain.Identity.Model;
using Tanit.User.Domain.Identity.Request;

namespace Tanit.User.Domain.Identity.BusinessRules
{
    public class UserEmailUnicityRule : IUserValidationRule
    {
        private readonly UserManager<TanitUser> _userManager;

        public UserEmailUnicityRule(UserManager<TanitUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> IsValidAsync(UserRegistrationRequest request) 
        {
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

            if (userWithSameEmail is not null)
            {
                return Result.Fail("Email already taken.");
            }
            return Result.Ok();
        }
    }

}
