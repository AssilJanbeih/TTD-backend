
using Domain;
using Domain.Abstractions;
using Domain.Constants;
using Domain.Entities.Identity;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seed;

public class TTDDataSeeder : IDataSeeder
{
    public void SeedData(ModelBuilder modelBuilder)
    {
        #region RootUser And Permissions

        User rootUser = new()
        {
            Id = Contracts.RootUserSeed.ID,

            NormalizedUserName = Contracts.RootUserSeed.USERNAME.ToUpper(),
            Email = Contracts.RootUserSeed.EMAIL,
            NormalizedEmail = Contracts.RootUserSeed.EMAIL.ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = Contracts.RootUserSeed.SECURITYSTAMP,
            ConcurrencyStamp = Contracts.RootUserSeed.CONCURRENCYSTAMP,
            LockoutEnabled = true,
            Title = Contracts.RootUserSeed.TITLE,
            Name = Contracts.RootUserSeed.NAME,
            Active = true,
        };
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        rootUser.PasswordHash = passwordHasher.HashPassword(rootUser, Contracts.RootUserSeed.PASSWORD);

        modelBuilder.Entity<User>().HasData(rootUser);

        #endregion

        #region Job Types

        modelBuilder.Entity<JobType>().HasData(
            new JobType((int)JobTypeEnum.ProjectManager, Contracts.JobTypes.PROJECT_MANAGER,
                "0b4415a0-7fc0-4d47-a6c4-6e4a43c5a532"),
            new JobType((int)JobTypeEnum.DesignLead, Contracts.JobTypes.DESIGN_LEAD,
                "f3219bd4-9042-4e64-893f-e9547fc11ed6"),
            new JobType((int)JobTypeEnum.TechLead, Contracts.JobTypes.TECH_LEAD,
                "5d80db27-4d8e-48fe-87f6-4b319365ec8f"),
            new JobType((int)JobTypeEnum.Designer, Contracts.JobTypes.DESIGNER,
                "6c4437cef6b6426d855c21757f5cff60"),
            new JobType((int)JobTypeEnum.WebDeveloper, Contracts.JobTypes.WEB_DEVELOPER,
                "f14d34f3-4f1e-48b9-86f2-34f23d21e420")
        );

        #endregion


    }
}