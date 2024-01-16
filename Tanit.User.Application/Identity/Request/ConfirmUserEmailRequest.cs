using FluentResults;
using MediatR;

namespace Tanit.User.Application.Identity.Request
{
    public class ConfirmUserEmailRequest : IRequest<Result>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
