using System.Diagnostics;

namespace AzureMcp.Services.Telemetry;

public interface ITelemetryService : IDisposable
{
    Activity? StartActivity(string activityName);
}
