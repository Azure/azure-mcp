// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.ApplicationInsights.Models.Query;
public class DistributedTraceQueryResponse
{
    public string? id { get; set; }
    public string? operation_ParentId { get; set; }
    public string? operation_Name { get; set; }
    public string? resultCode { get; set; }
    public string? itemType { get; set; }
    public string? success { get; set; }
    public DateTimeOffset? timestamp { get; set; }
    public double? duration { get; set; }
    public string? operation_Id { get; set; }
    public string? itemId { get; set; }
    public string? problemId { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? target { get; set; }
}
