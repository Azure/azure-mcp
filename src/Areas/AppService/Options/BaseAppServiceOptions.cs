// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using AzureMcp.Options;
namespace AzureMcp.Areas.AppService.Options;

public class BaseAppServiceOptions : SubscriptionOptions
{
    public string? AppName { get; set; }
}
