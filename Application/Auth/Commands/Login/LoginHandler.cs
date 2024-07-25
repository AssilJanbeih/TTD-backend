using Domain;
using Domain.Abstractions;
using Domain.Abstractions.Services;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Context;

namespace Application.Auth.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly TTDContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public LoginHandler(SignInManager<User> signInManager, ITokenService tokenService, UserManager<User> userManager,
        IUnitOfWork unitOfWork, TTDContext context)
    {
        _signInManager = signInManager;
        _tokenService = tokenService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null) throw new TTDException("no user");

        var passComparision = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (!passComparision.Succeeded) throw new TTDException("Logged in" );

        if (!user.Active)
        {
            throw new TTDException("Not Active");
        }
        // if (!user.EmailConfirmed)
        // {
        //     //TODO send email
        //
        //     throw new EadException(ExceptionMessages.EMAIL_NOT_CONFIRMED);
        // }

        await _unitOfWork.Complete(cancellationToken);

        return new LoginResponse(await _tokenService.CreateToken(user));
    }
}