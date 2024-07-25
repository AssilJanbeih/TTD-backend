using Domain.Abstractions;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.JobTypes.Queries.GetJobTypesDropdown;

public class GetJobTypesDropdownHandler: IRequestHandler<GetJobTypesDropdownQuery, IReadOnlyList<GetJobTypesDropdownResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetJobTypesDropdownHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<GetJobTypesDropdownResponse>> Handle(GetJobTypesDropdownQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository<JobType>()
            .GetAll()
            .Select(jt => new GetJobTypesDropdownResponse(jt.EntityId, jt.Name))
            .ToListAsync(cancellationToken);
    }
}