using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Logistic.Api.Extensions;

public static class HealthCheckEndpointExtensions
{
    public static WebApplication MapLogisticHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = registration => registration.Tags.Contains("ready")
        });

        app.MapHealthChecks("/health");

        return app;
    }
}
