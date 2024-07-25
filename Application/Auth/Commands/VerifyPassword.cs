using System.ComponentModel.DataAnnotations;
using Application.Abstraction.Messaging;
using Domain;
using Domain.Constants;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Utils.CurrentUser;

namespace Application.Auth.Commands;

public sealed record VerifyPasswordRequest(
    [Required] string Password
);

public sealed record VerifyPasswordCommand(
    VerifyPasswordRequest Request
) : ICommand<bool>;

public class VerifyPasswordHandler : ICommandHandler<VerifyPasswordCommand, bool>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly UserContextBaseService _userContextBaseService;

    public VerifyPasswordHandler(SignInManager<User> signInManager, UserManager<User> userManager,
        UserContextBaseService userContextBaseService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _userContextBaseService = userContextBaseService;
    }

    public async Task<bool> Handle(VerifyPasswordCommand command, CancellationToken cancellationToken)
    {
        var userId =
            _userContextBaseService.GetValueFromClaim(Domain.Constants.Contracts.CustomClaimsNames.USER_ID_CLAIM);

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            throw new TTDException("User Not found");
        }

        var passComparision = await _signInManager.CheckPasswordSignInAsync(user, command.Request.Password, false);

        return passComparision.Succeeded;
    }
}