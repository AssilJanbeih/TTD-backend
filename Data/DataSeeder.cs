using Microsoft.AspNetCore.Identity;
using Models;
using TTD_Backend;

public static class DataSeeder
{
    public static async System.Threading.Tasks.Task SeedAsync(TTTDContext context, UserManager<User> userManager)
    {
        if (!context.JobTypes.Any())
        {
            var jobTypes = new[]
            {
                new JobType("Developer"),
                new JobType("Designer"),
                new JobType("Project Manager")
            };

            context.JobTypes.AddRange(jobTypes);
            await context.SaveChangesAsync();
        }

        if (!userManager.Users.Any())
        {
            var user = new User
            {
                UserName = "admin@domain.com",
                Email = "admin@domain.com",
                Name = "Admin",
                Title = "Administrator",
                Active = true
            };

            await userManager.CreateAsync(user, "Admin@123");
        }
    }
}
