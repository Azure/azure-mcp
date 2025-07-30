// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Extension.Models;

public sealed class GenerateAzCommandPayload
{
    public string Question { get; set; } = string.Empty;
    public bool EnableParameterInjection { get; set; } = true;
}
