# devportal

A platform engineering POC of how a shared standard library reduces chaos across development teams.

---

## The Problem

In large engineering organisations, development teams make their own decisions about how to implement health checks, logging, and observability. The result is inconsistency — some teams have proper health reporting, others have nothing. 

This is a way to demonstrates how a good standard can solves that problem.

---

## The Solution

A shared .NET library that any service can adopt with two lines of code. The library provides:

- A standardised `/health` endpoint — used by Kubernetes for liveness probes
- A standardised `/ready` endpoint — used by Kubernetes for readiness probes  
- A `/metrics` endpoint — scraped by Prometheus every 15 seconds

Every service that adopts the library immediately appears in Grafana with full observability.

```csharp
// Two lines. That's all a development team needs to do.
builder.Services.AddDevPortalHealthCheck();
app.UseDevPortalHealthCheck();
```

---

## Architecture

```mermaid
flowchart TD
    subgraph Library ["Shared .NET Library — DevPortal.HealthCheck"]
        A[HealthCheckResponse.cs] --> D[HealthCheckExtensions.cs]
        B[ReadinessResponse.cs] --> D
        C[HealthCheckService.cs\nReads env variables] --> D
    end

    subgraph Pipeline ["GitHub Actions Pipeline"]
        E[Build] --> F[Test]
        F --> G[Security Scan — Trivy]
        G --> H[Docker Build linux/amd64]
        H --> I[Push to Docker Hub]
    end

    subgraph Cluster ["Kubernetes on GCP — GKE"]
        J[devteam1 — loan-service]
        K[devteam2 — pension-service]
        L[devteam3 — payment-service]
        M[devteam4 — customer-service]
        N[devteam5 — reporting-service]
    end

    subgraph Observability ["Observability"]
        O[Prometheus\nScrapes /metrics every 15s]
        P[Grafana\nAll teams visible]
        O --> P
    end

    Library --> Pipeline
    Pipeline --> Cluster
    Cluster --> O
```

---

## Tech Stack

| Technology |
|---|
| .NET 8, C# |
| Trivy |
| Docker |
| CitHub Actions |
| Kubernetes — GKE on GCP |
| Terraform |
| Prometheus |
| Grafana |

---

## The Health Check Library

The library returns response from every service that includes it:

```json
{
  "status": "healthy",
  "service": "loan-service",
  "team": "devteam1",
  "version": "1.0.0",
  "environment": "production",
  "timestamp": "2026-05-23T18:40:22Z"
}
```



