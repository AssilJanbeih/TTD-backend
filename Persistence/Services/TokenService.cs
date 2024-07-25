using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Domain.Abstractions;
using Domain.Abstractions.Services;
using Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Persistence.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    private readonly SymmetricSecurityKey _key;

    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public TokenService(IConfiguration config, UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> CreateToken(User user)
    {
       

        var claims = new List<Claim>
        {
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.GivenName, $"{user.Name}"),
            new (Contracts.CustomClaimsNames.USER_ID_CLAIM , user.Id),
            new (Contracts.CustomClaimsNames.JOB_TYPE_CLAIM , user.JobTypeId.ToString()),
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = _config["Token:Issuer"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
