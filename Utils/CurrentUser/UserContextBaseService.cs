using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Utils.CurrentUser;

public class UserContextBaseService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextBaseService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public string GetValueFromClaim(string claimType)
    {
        return _httpContextAccessor.HttpContext.User.FindFirst(claimType)?.Value;
    }
    
    public string GetBaseUrl()
    {
        return $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}{_httpContextAccessor.HttpContext?.Request.PathBase}";
    }
}