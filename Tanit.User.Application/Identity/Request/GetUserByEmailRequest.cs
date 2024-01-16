using FluentResults;
using MediatR;
using Tanit.User.Domain.Identity.Model;

namespace Tanit.User.Application.Identity.Request
{
    public class GetUserByEmailRequest : IRequest<IResult<TanitUser>>
    {
        public string Email { get; set; }
    }
}
