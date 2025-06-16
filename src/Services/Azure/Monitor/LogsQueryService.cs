using Azure;
using Azure.Core;
using Azure.Monitor.Query;
using Azure.Monitor.Query.Models;
using AzureMcp.Models.Monitor;
using AzureMcp.Options;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Services.Azure.Monitor
{
    internal class LogsQueryService : BaseAzureService, ILogsQueryService
    {
        public async Task<LogsQueryTable?> QueryResourceAsync(ResourceIdentifier resource, string kql, DateTimeOffset startTime, DateTimeOffset endTime, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
        {
            ValidateRequiredParameters(resource, kql);

            var options = AddDefaultPolicies(new LogsQueryClientOptions());

            if (retryPolicy != null)
            {
                options.Retry.Delay = TimeSpan.FromSeconds(retryPolicy.DelaySeconds);
                options.Retry.MaxDelay = TimeSpan.FromSeconds(retryPolicy.MaxDelaySeconds);
                options.Retry.MaxRetries = retryPolicy.MaxRetries;
                options.Retry.Mode = retryPolicy.Mode;
                options.Retry.NetworkTimeout = TimeSpan.FromSeconds(retryPolicy.NetworkTimeoutSeconds);
            }

            var credential = await GetCredential(tenant);

            var queryTimeRange = new QueryTimeRange(startTime, endTime);
            LogsQueryClient logsQueryClient = new LogsQueryClient(credential, options);

            var response = await logsQueryClient.QueryResourceAsync(resource, kql, queryTimeRange);

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

            if (response.Value != null)
            {
                // Get column indexes for easier access
                var columnIndexes = new Dictionary<int, string>();
                for (int i = 0; i < response.Value.Table.Columns.Count; i++)
                {
                    columnIndexes[i] = response.Value.Table.Columns[i].Name;
                }

                // Process each row in the query results
                foreach (var row in response.Value.Table.Rows)
                {
                    var columnValues = new Dictionary<string, object>();
                    for (int i = 0; i < row.Count; i++)
                    {
                        // Use column indexes to map values to column names
                        columnValues[columnIndexes[i]] = row[i];
                    }
                    rows.Add(columnValues);
                }

                return new LogsQueryTable
                {
                    Rows = rows
                };
            }
            else
            {
                return null;
            }
        }
    }
}
