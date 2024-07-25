using Application.Abstraction.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Utils.CurrentUser;

namespace Application.Auth.Commands;

public sealed record LogoutCommand : ICommand;

public class LogoutHandler : ICommandHandler<LogoutCommand>
{
    private readonly UserContextBaseService _userContextBaseService;
    private readonly TTDContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutHandler(IUnitOfWork unitOfWork, TTDContext context, UserContextBaseService userContextBaseService)
    {
        _unitOfWork = unitOfWork;
        _context = context;
        _userContextBaseService = userContextBaseService;
    }

    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var userId =
            _userContextBaseService.GetValueFromClaim(Domain.Constants.Contracts.CustomClaimsNames.USER_ID_CLAIM);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId), cancellationToken);

        if (user is null)
        {
            throw new TTDException("User Not found");
        }

        
        await _unitOfWork.Complete(cancellationToken);

        return Unit.Value;
    }
}