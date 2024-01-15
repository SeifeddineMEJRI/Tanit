using FluentResults;
using Infrastructure.Services.Identity;
using MediatR;
using Tanit.Application.Identity.Command;

namespace Tanit.Application.Identity.Handler
{
    public partial class UserSubscribeCommandHandler : IRequestHandler<UserSubscribeCommand, Result>
    {
        readonly IUserService _userService;

        public UserSubscribeCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result> Handle(UserSubscribeCommand request, CancellationToken cancellationToken)
        {
            return _userService.RegisterUserAsync(request);
        }
    }
}
