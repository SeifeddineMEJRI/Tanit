using MediatR;
using Tanit.Domain.Identity.Model;

namespace Tanit.Application.Identity.Request
{
    public class GetUserByEmailRequest : IRequest<TanitUser>
    {
        public string Email { get; set; }
    }
}
