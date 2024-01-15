using Infrastructure.Services.Identity;
using MediatR;
using Tanit.Application.Identity.Request;

namespace Tanit.Application.Identity.Handler
{

    public class ConfirmUserEmailHandler : IRequestHandler<ConfirmUserEmailRequest>
    {
        private readonly IUserService _userService;

        public ConfirmUserEmailHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task Handle(ConfirmUserEmailRequest request, CancellationToken cancellationToken)
        {
            return _userService.ConfirmUserEmailAsync(request.Email, request.Token);
        }
    }
}