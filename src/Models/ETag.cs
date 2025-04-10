// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMCP.Models;

public class ETag
{
    public string Value { get; set; } = string.Empty;
    public DateTime LastModified { get; set; } = DateTime.UtcNow;
}