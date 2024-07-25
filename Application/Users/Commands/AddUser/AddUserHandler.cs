using Domain;
using Domain.Abstractions;
using Domain.Entities.Identity;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Users.Commands.AddUser;

public class AddUserHandler : IRequestHandler<AddUserCommand, AddUserResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public AddUserHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<AddUserResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        int jobTypeId = await _unitOfWork.Repository<JobType>()
            .GetAll()
            .Where(jt => jt.EntityId.Equals(model.JobTypeId))
            .Select(jt => jt.Id)
            .FirstOrDefaultAsync(cancellationToken);

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Title = model.Title,
            Active = model.Active,
            EmailConfirmed = true,
            JobTypeId = jobTypeId,
            CreatedAt = DateTime.UtcNow,
            
        };
        var addUserResult = await _userManager.CreateAsync(user, model.Password);
        if (!addUserResult.Succeeded)
        {
            throw new TTDException(String.Join(",", addUserResult.Errors.Select(e => e.Description)));
        }

        await _userManager.AddToRolesAsync(user, model.SecurityGroupsNames);

        return new AddUserResponse(user.Id);
    }
}