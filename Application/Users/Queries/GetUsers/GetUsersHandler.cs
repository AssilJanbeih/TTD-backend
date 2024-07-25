using Domain;
using Domain.Abstractions;
using Domain.Constants;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, IReadOnlyList<GetUsersResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository<User>()
            .GetAll()
            .AsNoTracking()
            .Where(u => !u.Id.Equals(Domain.Constants.Contracts.RootUserSeed.ID))
            .Select(u => new GetUsersResponse(u.Id, u.Name, u.Email, u.PhoneNumber, u.Active ? Generic.ACTIVE : Generic.NOT_ACTIVE, u.Title))
            .ToListAsync(cancellationToken);
    }
}