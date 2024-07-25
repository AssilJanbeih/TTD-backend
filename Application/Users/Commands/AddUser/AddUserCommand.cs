using MediatR;

namespace Application.Users.Commands.AddUser;

public class AddUserCommand : IRequest<AddUserResponse>
{
    public AddUserCommand(AddUserRequest model)
    {
        Model = model;
    }

    public AddUserRequest Model { get; set; }
}