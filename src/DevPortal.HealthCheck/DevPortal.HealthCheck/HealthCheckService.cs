namespace DevPortal.HealthCheck;

public class HealthCheckService
{
    public HealthCheckResponse GetHealth()
    {
        return new HealthCheckResponse
        {
            Status = "healthy",
            Service = Environment.GetEnvironmentVariable("SERVICE_NAME") ?? "unknown",
            Team = Environment.GetEnvironmentVariable("TEAM_NAME") ?? "unknown",
            Version = Environment.GetEnvironmentVariable("SERVICE_VERSION") ?? "1.0.0",
            Environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "development",
            Timestamp = DateTime.UtcNow
        };
    }

    public ReadinessResponse GetReadiness()
    {
        return new ReadinessResponse
        {
            Status = "ready",
            Service = Environment.GetEnvironmentVariable("SERVICE_NAME") ?? "unknown",
            Team = Environment.GetEnvironmentVariable("TEAM_NAME") ?? "unknown"
        };
    }
}