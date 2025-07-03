// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options.Common;

namespace AzureMcp.Areas.AppService.Options;

public class BaseAppServiceOptions : BaseOptions
{
    public string? ResourceGroup { get; set; }
    public string? AppName { get; set; }
}
