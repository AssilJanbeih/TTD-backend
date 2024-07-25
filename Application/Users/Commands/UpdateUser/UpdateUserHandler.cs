using Domain;
using Domain.Abstractions;
using Domain.Constants;
using Domain.Entities.Identity;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var user = await _unitOfWork.Repository<User>()
            .GetAll()
            .Where(u => u.Id.Equals(model.Id))
            .FirstOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            throw new TTDException("NOT FOUND");
        }


        int jobTypeId = await _unitOfWork.Repository<JobType>()
            .GetAll()
            .Where(jt => jt.EntityId.Equals(model.JobTypeId))
            .Select(jt => jt.Id)
            .FirstOrDefaultAsync(cancellationToken);

        user.Name = model.Name;
        user.Email = model.Email;
        user.Title = model.Title;
        user.Active = model.Active;
        user.JobTypeId = jobTypeId;
        await _userManager.UpdateAsync(user);

        if (!String.IsNullOrWhiteSpace(model.Password))
        {
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, model.Password);
        }

        return Unit.Value;
    }
}