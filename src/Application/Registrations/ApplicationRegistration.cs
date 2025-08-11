using Application.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Registrations;

public class AssemblyReference;

public static class ApplicationRegistration
{
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<AssemblyReference>();
        });

        services.AddScoped<ErrorHandlingMiddleware>();

        return services;
    }

    public static IApplicationBuilder UseApplicationLayer(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();

        return app;
    }
}