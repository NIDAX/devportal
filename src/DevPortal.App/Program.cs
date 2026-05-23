using DevPortal.HealthCheck;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDevPortalHealthCheck();
var app = builder.Build();
app.UseRouting();
app.UseHttpMetrics();
app.UseMetricServer();
app.UseDevPortalHealthCheck();
app.Run();