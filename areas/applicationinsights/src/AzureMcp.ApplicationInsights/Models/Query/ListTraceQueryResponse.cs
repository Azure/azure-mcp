// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.ApplicationInsights.Models.Query;

public class ListTraceQueryResponse
{
    public string? problemId { get; set; }
    public string? target { get; set; }

    public string? location { get; set; }

    public string? name { get; set; }

    public string? type { get; set; }

    public string? operation_Name { get; set; }
    public string? resultCode { get; set; }
    public string? traces { get; set; }
}
