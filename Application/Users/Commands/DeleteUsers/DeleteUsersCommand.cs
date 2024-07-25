using MediatR;

namespace Application.Users.Commands.DeleteUsers;

public class DeleteUsersCommand : IRequest
{
    public DeleteUsersCommand(DeleteUsersRequest model)
    {
        Model = model;
    }

    public DeleteUsersRequest Model { get; set; }
}