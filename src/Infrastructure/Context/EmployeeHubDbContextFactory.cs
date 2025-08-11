using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;

internal class EmployeeHubDbContextFactory : IDesignTimeDbContextFactory<EmployeeHubDbContext>
{
    public EmployeeHubDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(Constants.ConnectionStringsFile)
            .Build();

        var connectionString = configuration.GetConnectionString(Constants.DefaultConnection);

        var optionsBuilder = new DbContextOptionsBuilder<EmployeeHubDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new EmployeeHubDbContext(optionsBuilder.Options);
    }
}
