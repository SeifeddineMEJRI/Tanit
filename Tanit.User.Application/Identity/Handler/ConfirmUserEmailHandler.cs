using FluentResults;
using MediatR;
using Tanit.User.Application.Identity.Request;
using Tanit.User.Domain.Identity.Service;

namespace Tanit.User.Application.Identity.Handler
{

    public class ConfirmUserEmailHandler : IRequestHandler<ConfirmUserEmailRequest, Result>
    {
        private readonly IUserService _userService;

        public ConfirmUserEmailHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result> Handle(ConfirmUserEmailRequest request, CancellationToken cancellationToken)
        {
            return _userService.ConfirmUserEmailAsync(request.Email, request.Token);
        }
    }
}