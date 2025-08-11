using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(t => t.TeamId);
        
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(t => t.Employees)
            .WithOne(e => e.Team)
            .HasForeignKey(e => e.TeamId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}