using System.Text;
using Application.Configuration;
using Domain.Constants;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Presentation.Extensions;
using Presentation.MiddleWares;
using Persistence.Configurations;
using Persistence.Context;
using Utils.CurrentUser;
using Domain;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions.Services;

namespace Presentation;

public class Startup
{
    private IConfiguration Config { get; }


    public Startup(IConfiguration config)
    {
        Config = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString());
        });

        ConfigureIdentity(services);
        services.RegisterPersistenceConfiguration(Config);
        services.RegisterApplicationServices();
        services.RegisterCurrentUserServices();
        services.RegisterAppSettings(Config);
        ConfigureCors(services);
        ConfigureApiBehaviour(services);
    }

    private void ConfigureIdentity(IServiceCollection services)
    {
        // JWT Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Token:Key"])),
                    ValidIssuer = Config["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });

        // Identity Configuration
        services.AddDbContext<TTDContext>(options =>
            options.UseSqlServer(Config.GetConnectionString("DefaultConnection")));
      
        services.AddIdentity<User, IdentityRole>(options =>
        {
        })
        .AddEntityFrameworkStores<TTDContext>()
        .AddDefaultTokenProviders();
    }


    private void ConfigureCors(IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy(Generic.CORS_POLICY, policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(Config["FrontEndUrl"]);
            });
        });
    }

    private void ConfigureApiBehaviour(IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            };
        });
    }

    public void ConfigureMiddleware(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseMiddleware<ExceptionMiddleWare>();
        app.UseHttpsRedirection();

        string basePath = Config.GetSection("AppStaticFilesSettings")["BasePath"];
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(basePath)
        });

        app.UseCors(Generic.CORS_POLICY);

        app.UseAuthentication();
        app.UseAuthorization();
    }

    public void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services)
    {
        app.MapControllers();
    }
}
