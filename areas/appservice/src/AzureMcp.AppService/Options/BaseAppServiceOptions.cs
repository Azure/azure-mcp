// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.AppService.Options;

public class BaseAppServiceOptions : SubscriptionOptions
{
    [JsonPropertyName(AppServiceOptionDefinitions.AppName)]
    public string? AppName { get; set; }
}
