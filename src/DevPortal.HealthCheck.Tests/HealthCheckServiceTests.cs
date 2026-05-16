using Xunit;

namespace DevPortal.HealthCheck.Tests;

public class HealthCheckServiceTests
{
    private readonly HealthCheckService _service;

    public HealthCheckServiceTests()
    {
        _service = new HealthCheckService();
    }

    [Fact]
    public void GetHealth_ReturnsHealthyStatus()
    {
        var result = _service.GetHealth();
        Assert.Equal("healthy", result.Status);
    }

    [Fact]
    public void GetHealth_ReturnsAllRequiredFields()
    {
        var result = _service.GetHealth();
        Assert.NotNull(result.Service);
        Assert.NotNull(result.Team);
        Assert.NotNull(result.Version);
        Assert.NotNull(result.Environment);
        Assert.NotEqual(default, result.Timestamp);
    }

    [Fact]
    public void GetHealth_ReadsEnvironmentVariables()
    {
        Environment.SetEnvironmentVariable("SERVICE_NAME", "test-service");
        Environment.SetEnvironmentVariable("TEAM_NAME", "test-team");

        var result = _service.GetHealth();

        Assert.Equal("test-service", result.Service);
        Assert.Equal("test-team", result.Team);

        // Clean up
        Environment.SetEnvironmentVariable("SERVICE_NAME", null);
        Environment.SetEnvironmentVariable("TEAM_NAME", null);
    }

    [Fact]
    public void GetReadiness_ReturnsReadyStatus()
    {
        var result = _service.GetReadiness();
        Assert.Equal("ready", result.Status);
    }
}