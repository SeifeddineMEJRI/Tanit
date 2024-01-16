using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tanit.User.Application.Identity.Command;
using Tanit.User.Application.Identity.Request;

namespace Tanit.WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [Route("Subscribe")]
    public async Task<IActionResult> SubscribeAsync(UserSubscribeCommand subscribeCommand)
    {
        var result = await _mediator.Send(subscribeCommand);
        if (result.IsSuccess)
        {
             return Ok();
        }
        return BadRequest(result.Errors);
    }

    [HttpGet]
    [Route("{email}")]
    public async Task<IActionResult> GetUserByEmailAsync(string email)
    {
        var query = new GetUserByEmailRequest()
        {
            Email = email
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("ConfirmEmailUser")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var query = new ConfirmUserEmailRequest()
        {
            Email = email,
            Token = token
        };
        await _mediator.Send(query);
        return Ok();
    }
}