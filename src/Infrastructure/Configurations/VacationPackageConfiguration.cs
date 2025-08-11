using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class VacationPackageConfiguration : IEntityTypeConfiguration<VacationPackage>
{
    public void Configure(EntityTypeBuilder<VacationPackage> builder)
    {
        builder.HasKey(vp => vp.VacationPackageId);
        
        builder.Property(vp => vp.VacationPackageName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(vp => vp.GrantedDays)
            .IsRequired();
            
        builder.Property(vp => vp.Year)
            .IsRequired();

        builder.HasMany(vp => vp.Employees)
            .WithOne(e => e.VacationPackage)
            .HasForeignKey(e => e.VacationPackageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}