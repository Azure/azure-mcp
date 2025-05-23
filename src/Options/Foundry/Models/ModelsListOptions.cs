// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Options.Foundry.Models;

public class ModelsListOptions : GlobalOptions
{
    public bool? SearchForFreePlayground { get; set; }
    public string? PublisherName { get; set; }
    public string? LicenseName { get; set; }
    public string? ModelName { get; set; }
}
