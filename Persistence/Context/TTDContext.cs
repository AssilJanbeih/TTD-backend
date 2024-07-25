using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Abstractions;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Persistence.Context;

public class TTDContext : IdentityDbContext<User>
{
    private readonly IDataSeeder _dataSeeder;

    public TTDContext(DbContextOptions<TTDContext> options, IDataSeeder dataSeeder)
        : base(options)
    {
        _dataSeeder = dataSeeder;
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<JobType> JobTypes { get; set; }
    public DbSet<Domain.Entities.Task> Tasks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _dataSeeder.SeedData(modelBuilder);
    }
}
