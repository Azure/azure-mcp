// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using Azure.ResourceManager.CognitiveServices.Models;
using AzureMcp.Areas.Foundry.Commands.Evaluation;
using AzureMcp.Areas.Foundry.Commands.Models;
using AzureMcp.Areas.Foundry.Models;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.AI.Evaluation;

namespace AzureMcp.Areas.Foundry.Commands;

[JsonSerializable(typeof(ModelsListCommand.ModelsListCommandResult))]
[JsonSerializable(typeof(DeploymentsListCommand.DeploymentsListCommandResult))]
[JsonSerializable(typeof(ModelDeploymentCommand.ModelDeploymentCommandResult))]
[JsonSerializable(typeof(AgentsListCommand.AgentsListCommandResult))]
[JsonSerializable(typeof(AgentsConnectCommand.AgentsConnectCommandResult))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(ModelCatalogFilter))]
[JsonSerializable(typeof(ModelCatalogRequest))]
[JsonSerializable(typeof(ModelCatalogResponse))]
[JsonSerializable(typeof(ModelDeploymentInformation))]
[JsonSerializable(typeof(ModelInformation))]
[JsonSerializable(typeof(CognitiveServicesAccountSku))]
[JsonSerializable(typeof(CognitiveServicesAccountDeploymentProperties))]
[JsonSerializable(typeof(QueryAndEvaluateAgentCommand.QueryAndEvaluateAgentCommandResult))]
[JsonSerializable(typeof(List<ChatMessage>))]
[JsonSerializable(typeof(EvaluationResult))]
[JsonSerializable(typeof(EvaluateAgentCommand.EvaluateAgentCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)]
internal sealed partial class FoundryJsonContext : JsonSerializerContext;
