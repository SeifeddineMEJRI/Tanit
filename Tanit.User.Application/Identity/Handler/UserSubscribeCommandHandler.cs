using FluentResults;
using MediatR;
using Tanit.User.Application.Identity.Command;
using Tanit.User.Domain.Identity.Service;

namespace Tanit.User.Application.Identity.Handler
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
