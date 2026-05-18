using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DevPortal.HealthCheck;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddDevPortalHealthCheck(
        this IServiceCollection services)
    {
        services.AddSingleton<HealthCheckService>();
        return services;
    }

    public static IApplicationBuilder UseDevPortalHealthCheck(
        this IApplicationBuilder app)
    {
        app.Map("/health", healthApp =>
        {
            healthApp.Run(async context =>
            {
                var service = context.RequestServices
                    .GetRequiredService<HealthCheckService>();
                var response = service.GetHealth();
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(response);
            });
        });

        app.Map("/ready", readyApp =>
        {
            readyApp.Run(async context =>
            {
                var service = context.RequestServices
                    .GetRequiredService<HealthCheckService>();
                var response = service.GetReadiness();
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(response);
            });
        });

        return app;
    }
}
