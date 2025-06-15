using Azure;
using Azure.Core;
using AzureMcp.Models.Monitor;
using AzureMcp.Options;

namespace AzureMcp.Services.Interfaces
{
    public interface ILogsQueryService
    {
        Task<LogsQueryTable?> QueryResourceAsync(ResourceIdentifier resource, string kql, DateTimeOffset startTime, DateTimeOffset endTime, string? tenant = null, RetryPolicyOptions? retryPolicy = null);
    }
}
