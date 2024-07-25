using Domain;
using Domain.Constants;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Persistence.Context;

namespace Persistence.Seed;

public class TTDIdentitySeedContext
{
    public static async Task SeedAsync(TTDContext context , UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                Email = "assiljanbeih@gmail.com",
                Name="Assil",
                EmailConfirmed = true,
                Title="Full Stack",
                Active = true
            };
            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}