using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class VacationConfiguration : IEntityTypeConfiguration<Vacation>
{
    public void Configure(EntityTypeBuilder<Vacation> builder)
    {
        builder.HasKey(v => v.VacationId);
        
        builder.Property(v => v.DateSince)
            .IsRequired();
            
        builder.Property(v => v.DateUntil)
            .IsRequired();
            
        builder.Property(v => v.NumberOfHours)
            .IsRequired();
            
        builder.Property(v => v.IsPartVacation)
            .IsRequired();

        builder.HasOne(v => v.Employee)
            .WithMany(e => e.Vacations)
            .HasForeignKey(v => v.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}