using FluentResults;
using MediatR;
using Tanit.User.Domain.Identity.Request;

namespace Tanit.User.Application.Identity.Command
{
    public class UserSubscribeCommand : UserRegistrationRequest, IRequest<Result>
    {
    }
}
