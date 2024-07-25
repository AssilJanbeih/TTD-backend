using MediatR;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest
{
    public UpdateUserCommand(UpdateUserRequest model)
    {
        Model = model;
    }

    public UpdateUserRequest Model { get; set; }
}