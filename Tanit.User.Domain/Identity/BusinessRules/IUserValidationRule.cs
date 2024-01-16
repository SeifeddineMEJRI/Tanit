using FluentResults;
using Tanit.User.Domain.Identity.Request;

namespace Tanit.User.Domain.Identity.BusinessRules
{
    public interface IUserValidationRule
    {
        Task<Result> IsValidAsync(UserRegistrationRequest request);
    }

}
