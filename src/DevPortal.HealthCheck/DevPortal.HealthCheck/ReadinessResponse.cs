namespace DevPortal.HealthCheck;

public class ReadinessResponse
{
    public string Status { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public string Team { get; set; } = string.Empty;
}