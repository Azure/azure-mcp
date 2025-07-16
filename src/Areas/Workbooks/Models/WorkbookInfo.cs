// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Workbooks.Models;

public sealed record WorkbookInfo(
    string WorkbookId,
    // Display name can be null if not set.
    string? WorkbookDisplayName,
    string? Description,
    string? Category,
    string? Location,
    string? Kind,
    string? Tags,
    string? SerializedData,
    string? Version,
    DateTimeOffset? TimeModified,
    string? UserId,
    string? SourceId
);
