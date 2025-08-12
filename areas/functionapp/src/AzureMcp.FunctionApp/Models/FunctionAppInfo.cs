// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.FunctionApp.Models;

/// <summary>
/// Information about an Azure Function App resource.
/// </summary>
public record FunctionAppInfo(
    string? Name,
    string? ResourceGroupName,
    string? Location,
    string? AppServicePlanName,
    string? Status,
    string? DefaultHostName,
    IDictionary<string, string>? Tags
);
