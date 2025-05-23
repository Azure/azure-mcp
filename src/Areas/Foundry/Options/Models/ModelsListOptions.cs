// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;

namespace AzureMcp.Areas.Foundry.Options.Models;

public class ModelsListOptions : GlobalOptions
{
    public bool? SearchForFreePlayground { get; set; }
    public string? PublisherName { get; set; }
    public string? LicenseName { get; set; }
    public string? ModelName { get; set; }
}
