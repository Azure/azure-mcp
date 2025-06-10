namespace AzureMcp.Models.LoadTesting.LoadTestRun;

public class LoadTestRunResource
{
    public string TestId { get; set; } = string.Empty;
    public string? TestRunId { get; set; } = string.Empty;
    public string? DisplayName { get; set; } = string.Empty;
    public int? VirtualUsers { get; set; } = 0;
    public string? Status { get; set; } = string.Empty;
    public DateTimeOffset? StartDateTime { get; set; } = null;
    public DateTimeOffset? EndDateTime { get; set; } = null;
    public DateTimeOffset? ExecutedDateTime { get; set; } = null;
    public string? PortalUrl { get; set; } = string.Empty;
    public int? Duration { get; set; } = 0;
    public DateTimeOffset? CreatedDateTime { get; set; } = null;
    public string? CreatedBy { get; set; } = string.Empty;
    public DateTimeOffset? LastModifiedDateTime { get; set; } = null;
    public string? LastModifiedBy { get; set; } = string.Empty;
}
