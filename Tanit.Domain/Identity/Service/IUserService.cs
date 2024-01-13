using FluentResults;
using Tanit.Domain.Identity.Model;
using Tanit.Domain.Identity.Request;

namespace Infrastructure.Services.Identity
{
    public interface IUserService
    {
        Task<Result> RegisterUserAsync(UserRegistrationRequest request);
        Task<IResult<TanitUser>> GetUserByIdAsync(string userId);
        Task<IResult<TanitUser>> GetUserByEmailAsync(string email);
    }
}
