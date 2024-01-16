using FluentResults;
using Tanit.User.Domain.Identity.Model;
using Tanit.User.Domain.Identity.Request;

namespace Tanit.User.Domain.Identity.Service
{
    public interface IUserService
    {
        Task<Result> RegisterUserAsync(UserRegistrationRequest request);
        Task<IResult<TanitUser>> GetUserByIdAsync(string userId);
        Task<IResult<TanitUser>> GetUserByEmailAsync(string email);
        Task<Result> ConfirmUserEmailAsync(string email, string token);
    }
}
