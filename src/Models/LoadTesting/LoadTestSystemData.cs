
namespace AzureMcp.Models.LoadTesting;

public class LoadTestSystemData
{
    public string? CreatedBy { get; set; } = string.Empty;
    public string? CreatedByType { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; set; } = null;
    public string? LastModifiedBy { get; set; } = string.Empty;
    public string? LastModifiedByType { get; set; } = string.Empty;
    public DateTimeOffset? LastModifiedAt { get; set; } = null;
}
