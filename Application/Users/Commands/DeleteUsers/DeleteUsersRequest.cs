using System.ComponentModel.DataAnnotations;

namespace Application.Users.Commands.DeleteUsers;

public sealed record DeleteUsersRequest([Required] List<string> UserIds);