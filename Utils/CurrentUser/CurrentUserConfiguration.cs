using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Utils.CurrentUser;

public static class CurrentUserConfiguration
{
    public static void RegisterCurrentUserServices(this IServiceCollection services)
    {
        services.AddScoped<UserContextBaseService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    } 
}