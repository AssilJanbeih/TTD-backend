using Domain;
using Domain.Abstractions;
using Domain.Constants;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Utils.CurrentUser;

namespace Application.Users.Queries.GetUsersDropdown;

public class GetUsersDropdownHandler : IRequestHandler<GetUsersDropdownQuery, IReadOnlyList<GetUsersDropdownResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserContextBaseService _userContextBaseService;

    public GetUsersDropdownHandler(IUnitOfWork unitOfWork, UserContextBaseService userContextBaseService)
    {
        _unitOfWork = unitOfWork;
        _userContextBaseService = userContextBaseService;
    }

    public async Task<IReadOnlyList<GetUsersDropdownResponse>> Handle(GetUsersDropdownQuery request, CancellationToken cancellationToken)
    {
        
        var model = request.Model;
        return await _unitOfWork
            .Repository<User>()
            .GetAll()
           
            .Where(u => (!model.JobType.HasValue || u.JobTypeId.Equals((int)model.JobType)) && !u.Id.Equals(Domain.Constants.Contracts.RootUserSeed.ID))
        
            .Select(u => new GetUsersDropdownResponse(u.Id, $"{u.Name}"))
            .ToListAsync(cancellationToken);
    }
}