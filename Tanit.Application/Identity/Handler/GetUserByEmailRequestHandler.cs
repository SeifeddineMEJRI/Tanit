using MediatR;
using Tanit.User.Application.Identity.Request;
using Tanit.User.Domain.Identity.Model;
using Tanit.User.Domain.Identity.Service;

namespace Tanit.User.Application.Identity.Handler
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
