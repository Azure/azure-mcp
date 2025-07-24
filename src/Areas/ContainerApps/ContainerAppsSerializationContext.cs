// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.ContainerApps.Commands.App;
using AzureMcp.Areas.ContainerApps.Models;
using AzureMcp.Areas.ContainerApps.Options;

namespace AzureMcp.Areas.ContainerApps;

[JsonSerializable(typeof(List<ContainerApp>))]
[JsonSerializable(typeof(ContainerApp))]
[JsonSerializable(typeof(ContainerAppConfiguration))]
[JsonSerializable(typeof(ContainerAppIngress))]
[JsonSerializable(typeof(ContainerAppSecret))]
[JsonSerializable(typeof(ContainerAppRegistry))]
[JsonSerializable(typeof(ContainerAppTemplate))]
[JsonSerializable(typeof(ContainerAppContainer))]
[JsonSerializable(typeof(ContainerAppResources))]
[JsonSerializable(typeof(ContainerAppScale))]
[JsonSerializable(typeof(AppListCommand.AppListCommandResult))]
[JsonSerializable(typeof(BaseContainerAppsOptions))]
[JsonSerializable(typeof(Dictionary<string, string>))]
[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Metadata)]
public partial class ContainerAppsSerializationContext : JsonSerializerContext
{
}
