using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace TTD_Backend.Configurations
{
    public class JobTypeConfiguration : IEntityTypeConfiguration<JobType>
    {
        public void Configure(EntityTypeBuilder<JobType> builder)
        {
            builder.HasKey(j => j.Id);
            builder.Property(j => j.Name).IsRequired().HasMaxLength(100);

            builder.HasMany(j => j.Users)
                   .WithOne(u => u.JobType)
                   .HasForeignKey(u => u.JobTypeId);
        }
    }
}
