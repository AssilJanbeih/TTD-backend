using Domain;
using Domain.Abstractions;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.DeleteUsers;

public class DeleteUsersHandler : IRequestHandler<DeleteUsersCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUsersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var users = await _unitOfWork.Repository<User>()
            .GetAll()
            .Where(u => model.UserIds.Contains(u.Id))
            .ToListAsync(cancellationToken);
        
        for (var index = 0; index < users.Count; index++)
        {
            var user = users[index];
            _unitOfWork.Repository<User>().Delete(user);
        }

        await _unitOfWork.Complete();
        
        return Unit.Value;
    }
}