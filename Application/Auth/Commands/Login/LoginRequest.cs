using System.ComponentModel.DataAnnotations;

namespace Application.Auth.Commands.Login;

public sealed record LoginRequest([Required] string Email, [Required] string Password);