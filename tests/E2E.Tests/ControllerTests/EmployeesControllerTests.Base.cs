using App;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace E2E.Tests;

public abstract class E2ETestBase : IClassFixture<WebApplicationFactory<IApiMarker>>, IDisposable
{
    protected readonly WebApplicationFactory<IApiMarker> _factory;
    protected readonly HttpClient _client;
    protected readonly IServiceScope _scope;
    protected readonly EmployeeHubDbContext _dbContext;
    private readonly IDbContextTransaction _transaction;

    protected E2ETestBase(WebApplicationFactory<IApiMarker> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices((context, services) =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<EmployeeHubDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<EmployeeHubDbContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                });

                services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Warning));
            });
        });

        _client = _factory.CreateClient();
        _scope = _factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<EmployeeHubDbContext>();
        _transaction = _dbContext.Database.BeginTransaction();
    }

    public void Dispose()
    {
        _transaction.Rollback();
        _transaction.Dispose();
        _dbContext.Dispose();
        _scope.Dispose();
        _client.Dispose();
    }
}
