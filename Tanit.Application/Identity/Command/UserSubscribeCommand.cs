using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanit.Domain.Identity.Request;

namespace Tanit.Application.Identity.Command
{
    public class UserSubscribeCommand : UserRegistrationRequest, IRequest<Result>
    {
    }
}
