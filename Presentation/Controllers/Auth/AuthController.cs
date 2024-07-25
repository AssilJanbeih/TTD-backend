using System.Net;
using Application.Auth.Commands;
using Application.Auth.Commands.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Auth;

public class AuthController : BaseController
{
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = new LoginCommand(request);
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        var command = new LogoutCommand();
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    [Route("verify-password")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> VerifyPassword([FromBody] VerifyPasswordRequest request)
    {
        var command = new VerifyPasswordCommand(request);
        return Ok(await Mediator.Send(command));
    }
}