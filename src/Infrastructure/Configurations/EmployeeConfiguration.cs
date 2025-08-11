using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.EmployeeId);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(e => e.Team)
            .WithMany(t => t.Employees)
            .HasForeignKey(e => e.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.VacationPackage)
            .WithMany(vp => vp.Employees)
            .HasForeignKey(e => e.VacationPackageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Superior)
            .WithMany(v => v.Subordinates)
            .HasForeignKey(v => v.SuperiorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}