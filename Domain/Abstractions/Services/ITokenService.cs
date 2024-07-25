using Domain.Entities.Identity;
using System.Threading.Tasks;

namespace Domain.Abstractions.Services;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}