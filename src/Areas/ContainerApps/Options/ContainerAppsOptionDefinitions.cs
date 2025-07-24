// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Option;

namespace AzureMcp.Areas.ContainerApps.Options;

public static class ContainerAppsOptionDefinitions
{
    public static readonly Option<string> OptionalResourceGroup = OptionDefinitions.Common.CreateOptionalResourceGroup();

    public static readonly Option<string> Environment = new(
        "--environment",
        "Name or resource ID of the container app's environment.")
    {
        IsRequired = false
    };
}
