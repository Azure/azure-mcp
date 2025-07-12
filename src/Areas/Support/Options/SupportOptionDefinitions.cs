// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Support.Options;

public static class SupportOptionDefinitions
{
    public static readonly Option<string> Filter = new(
        ["--filter"],
        "OData filter for support tickets. Supported properties: CreatedDate, Status, ProblemClassificationId, ServiceId. Example: \"Status eq 'Open'\" or \"CreatedDate ge 2024-01-01T00:00:00Z\"")
    {
        ArgumentHelpName = "odata-filter"
    };

    public static readonly Option<int> Top = new(
        ["--top"],
        () => 100,
        "Maximum number of support tickets to return")
    {
        ArgumentHelpName = "count"
    };
}
