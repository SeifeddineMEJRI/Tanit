﻿using MediatR;

namespace Tanit.Application.Identity.Request
{
    public class ConfirmUserEmailRequest : IRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
