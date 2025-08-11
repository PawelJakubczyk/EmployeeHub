using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Registrations;

public static class InfrastructureRegistration
{
    public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EmployeeHubDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString(Constants.DefaultConnection);
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}