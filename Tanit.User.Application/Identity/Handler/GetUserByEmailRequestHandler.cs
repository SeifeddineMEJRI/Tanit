using FluentResults;
using MediatR;
using Tanit.User.Application.Identity.Request;
using Tanit.User.Domain.Identity.Model;
using Tanit.User.Domain.Identity.Service;

namespace Tanit.User.Application.Identity.Handler
{
    public class GetUserByEmailRequestHandler : IRequestHandler<GetUserByEmailRequest, IResult<TanitUser>>
    {
        readonly IUserService _userService;

        public GetUserByEmailRequestHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult<TanitUser>> Handle(GetUserByEmailRequest request, CancellationToken cancellationToken)
        {
            return  await _userService.GetUserByEmailAsync(request.Email);
        }
    }
}
