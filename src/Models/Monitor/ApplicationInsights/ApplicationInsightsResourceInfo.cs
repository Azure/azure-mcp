using Azure.Core;

namespace AzureMcp.Models.Monitor.ApplicationInsights
{
    public class ApplicationInsightsResourceInfo
    {
        public required string Name { get; set; }
        public required ResourceIdentifier ResourceId { get; set; }
        public required string AppId { get; set; }
        public required string InstrumentationKey { get; set; }
    }
}
