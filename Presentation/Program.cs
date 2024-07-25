using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Seed;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.ConfigureMiddleware(app, app.Environment);
startup.ConfigureEndpoints(app, app.Services);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<TTDContext>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        // Checks if migrations are missing and run update db
        await context.Database.MigrateAsync();
        await TTDIdentitySeedContext.SeedAsync(context, userManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occured during migration");
    }
}

await app.RunAsync();