using MediatR;

namespace Application.Users.Queries.GetUser;

public class GetUserQuery : IRequest<GetUserResponse>
{
    public GetUserQuery(GetUserRequest model)
    {
        Model = model;
    }

    public GetUserRequest Model { get; set; }
}