using MediatR;

namespace Application.Auth.Commands.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public LoginCommand(LoginRequest model)
    {
        Model = model;
    }

    public LoginRequest Model { get; set; }
}