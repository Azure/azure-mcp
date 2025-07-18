// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using Azure.ResourceManager.CognitiveServices.Models;
using AzureMcp.Foundry.Commands.Models;
using AzureMcp.Foundry.Models;

namespace AzureMcp.Foundry.Commands;

[JsonSerializable(typeof(ModelsListCommand.ModelsListCommandResult))]
[JsonSerializable(typeof(DeploymentsListCommand.DeploymentsListCommandResult))]
[JsonSerializable(typeof(ModelDeploymentCommand.ModelDeploymentCommandResult))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(ModelCatalogFilter))]
[JsonSerializable(typeof(ModelCatalogRequest))]
[JsonSerializable(typeof(ModelCatalogResponse))]
[JsonSerializable(typeof(ModelDeploymentInformation))]
[JsonSerializable(typeof(ModelInformation))]
[JsonSerializable(typeof(CognitiveServicesAccountSku))]
[JsonSerializable(typeof(CognitiveServicesAccountDeploymentProperties))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)]
internal sealed partial class FoundryJsonContext : JsonSerializerContext;
