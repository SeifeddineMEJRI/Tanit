using Infrastructure.Services.Identity;
using MediatR;
using Tanit.Application.Identity.Request;
using Tanit.Domain.Identity.Model;

namespace Tanit.Application.Identity.Handler
{
    public class GetUserByEmailRequestHandler : IRequestHandler<GetUserByEmailRequest, TanitUser>
    {
        readonly IUserService _userService;

        public GetUserByEmailRequestHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<TanitUser> Handle(GetUserByEmailRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUserByEmailAsync(request.Email);
            return result.Value;
        }
    }
}
