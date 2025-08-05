// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using AzureMcp.Core.Options;
using System.Text.Json.Serialization;
namespace AzureMcp.AppService.Options;

public class BaseAppServiceOptions : SubscriptionOptions
{
    [JsonPropertyName(AppServiceOptionDefinitions.AppName)]
    public string? AppName { get; set; }
}
