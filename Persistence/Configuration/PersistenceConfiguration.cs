using Domain;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.DataAccess;
using Persistence.Seed;

namespace Persistence.Configurations;

public static class PersistenceConfiguration
{
    public static void RegisterPersistenceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TTDContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
            ).UseSnakeCaseNamingConvention()
        );
        services.AddIdentityCore<User>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<TTDContext>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IDataSeeder, TTDDataSeeder>();
    }
}