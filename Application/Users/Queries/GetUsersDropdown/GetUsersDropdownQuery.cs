using MediatR;

namespace Application.Users.Queries.GetUsersDropdown;

public class GetUsersDropdownQuery : IRequest<IReadOnlyList<GetUsersDropdownResponse>>
{
    public GetUsersDropdownQuery(GetUsersDropdownRequest model)
    {
        Model = model;
    }

    public GetUsersDropdownRequest Model { get; set; }
}