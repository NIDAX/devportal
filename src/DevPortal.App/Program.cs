using DevPortal.HealthCheck;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDevPortalHealthCheck();
var app = builder.Build();
app.UseDevPortalHealthCheck();
app.Run();
