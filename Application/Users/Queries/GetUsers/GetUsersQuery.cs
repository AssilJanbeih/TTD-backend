using MediatR;

namespace Application.Users.Queries.GetUsers;

public class GetUsersQuery : IRequest<IReadOnlyList<GetUsersResponse>>
{
    
}