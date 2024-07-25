using Domain;
using Domain.Abstractions;
using Domain.Constants;
using Domain.Entities.Identity;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUser;

public class GetUserHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public GetUserHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var user = await _unitOfWork.Repository<User>()
            .GetAll()
            .AsNoTracking()
            .Include(u => u.JobType)
            .Where(u => u.Id.Equals(model.UserEntityId))
            .FirstOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            throw new TTDException("not found");
        }
        return new GetUserResponse
        {
                Id = user.Id,
                Name = user.Name,
                Title = user.Title,    
                Email = user.Email,
                Active = user.Active,
                JobTypeId = user.JobType?.EntityId,
        };
    }
}