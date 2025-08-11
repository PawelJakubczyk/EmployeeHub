using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class EmployeeHubDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Vacation> Vacations { get; set; }
    public DbSet<VacationPackage> VacationPackages { get; set; }

    public EmployeeHubDbContext(DbContextOptions<EmployeeHubDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var infrastructureAssembly = typeof(EmployeeHubDbContext).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(infrastructureAssembly);
    }
}
