// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.ClientModel;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using Azure;
using Azure.AI.Agents.Persistent;
using Azure.AI.OpenAI;
using Azure.AI.Projects;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.CognitiveServices;
using Azure.ResourceManager.CognitiveServices.Models;
using Azure.ResourceManager.Resources;
using AzureMcp.Areas.Foundry.Commands;
using AzureMcp.Areas.Foundry.Models;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.AI.Evaluation;
using Microsoft.Extensions.AI.Evaluation.Quality;

#pragma warning disable AIEVAL001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

namespace AzureMcp.Areas.Foundry.Services;

public class FoundryService : BaseAzureService, IFoundryService
{
    private static readonly Dictionary<string, Func<IEvaluator>> AgentEvaluatorDictionary = new()
    {
        { "intent_resolution", () => new IntentResolutionEvaluator()},
        { "tool_call_accuracy", () => new ToolCallAccuracyEvaluator()},
        { "task_adherence", () => new TaskAdherenceEvaluator()},
    };

     private static readonly Dictionary<string, Func<IEnumerable<AITool>, EvaluationContext>> AgentEvaluatorContextDictionary = new()
    {
        { "intent_resolution", toolDefinitons => new IntentResolutionEvaluatorContext(toolDefinitons)},
        { "tool_call_accuracy", toolDefinitons => new ToolCallAccuracyEvaluatorContext(toolDefinitons)},
        { "task_adherence", toolDefinitons => new TaskAdherenceEvaluatorContext(toolDefinitons)},
    };

    public async Task<List<ModelInformation>> ListModels(
        bool searchForFreePlayground = false,
        string publisherName = "",
        string licenseName = "",
        string modelName = "",
        int maxPages = 3,
        RetryPolicyOptions? retryPolicy = null)
    {
        string url = "https://api.catalog.azureml.ms/asset-gallery/v1.0/models";
        var request = new ModelCatalogRequest { Filters = [new ModelCatalogFilter("labels", ["latest"], "eq")] };

        if (searchForFreePlayground)
        {
            request.Filters.Add(new ModelCatalogFilter("freePlayground", ["true"], "eq"));
        }

        if (!string.IsNullOrEmpty(publisherName))
        {
            request.Filters.Add(new ModelCatalogFilter("publisher", [publisherName], "contains"));
        }

        if (!string.IsNullOrEmpty(licenseName))
        {
            request.Filters.Add(new ModelCatalogFilter("license", [licenseName], "contains"));
        }

        if (!string.IsNullOrEmpty(modelName))
        {
            request.Filters.Add(new ModelCatalogFilter("name", [modelName], "eq"));
        }

        var modelsList = new List<ModelInformation>();
        int pageCount = 0;

        try
        {
            while (pageCount < maxPages)
            {
                pageCount++;
                try
                {
                    var content = new StringContent(
                        JsonSerializer.Serialize(request, FoundryJsonContext.Default.ModelCatalogRequest),
                        Encoding.UTF8,
                        "application/json");

                    var httpResponse = await new HttpClient().PostAsync(url, content);
                    httpResponse.EnsureSuccessStatusCode();

                    var responseText = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonSerializer.Deserialize(responseText,
                        FoundryJsonContext.Default.ModelCatalogResponse);
                    if (response == null || response.Summaries.Count == 0)
                    {
                        break;
                    }

                    foreach (var summary in response.Summaries)
                    {
                        try
                        {
                            summary.DeploymentInformation.IsFreePlayground = summary.PlaygroundLimits != null;
                            if (!string.IsNullOrEmpty(summary.Publisher) &&
                                summary.Publisher.Equals("openai", StringComparison.OrdinalIgnoreCase))
                            {
                                summary.DeploymentInformation.IsOpenAI = true;
                            }
                            else
                            {
                                if (summary.AzureOffers != null)
                                {
                                    summary.DeploymentInformation.IsServerlessEndpoint =
                                        summary.AzureOffers.Contains("standard-paygo");

                                    summary.DeploymentInformation.IsManagedCompute =
                                        summary.AzureOffers.Contains("VM") ||
                                        summary.AzureOffers.Contains("VM-withSurcharge");
                                }
                            }

                            modelsList.Add(summary);
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    if (string.IsNullOrEmpty(response.ContinuationToken))
                    {
                        break;
                    }

                    request.ContinuationToken = response.ContinuationToken;
                }
                catch (HttpRequestException)
                {
                    break;
                }
                catch (JsonException)
                {
                    break;
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception($"Error retrieving models from model catalog: {e.Message}");
        }

        return modelsList;
    }

    public async Task<List<Deployment>> ListDeployments(string endpoint, string? tenantId = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(endpoint);

        try
        {
            var credential = await GetCredential(tenantId);
            var deploymentsClient = new AIProjectClient(new Uri(endpoint), credential).GetDeploymentsClient();

            var deployments = new List<Deployment>();
            await foreach (var deployment in deploymentsClient.GetDeploymentsAsync())
            {
                deployments.Add(deployment);
            }

            return deployments;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to list deployments: {ex.Message}", ex);
        }
    }

    public async Task<Dictionary<string, object>> DeployModel(string deploymentName, string modelName, string modelFormat,
        string azureAiServicesName, string resourceGroup, string subscriptionId, string? modelVersion = null, string? modelSource = null,
        string? skuName = null, int? skuCapacity = null, string? scaleType = null, int? scaleCapacity = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(deploymentName, modelName, modelFormat, azureAiServicesName, resourceGroup, subscriptionId);

        try
        {
            ArmClient armClient = await CreateArmClientAsync(null, retryPolicy);

            var subscription =
                armClient.GetSubscriptionResource(SubscriptionResource.CreateResourceIdentifier(subscriptionId));
            var resourceGroupResource = await subscription.GetResourceGroupAsync(resourceGroup);

            var cognitiveServicesAccounts = resourceGroupResource.Value.GetCognitiveServicesAccounts();
            var cognitiveServicesAccount = await cognitiveServicesAccounts.GetAsync(azureAiServicesName);

            var deploymentData = new CognitiveServicesAccountDeploymentData
            {
                Properties = new CognitiveServicesAccountDeploymentProperties
                {
                    Model = new CognitiveServicesAccountDeploymentModel
                    {
                        Format = modelFormat,
                        Name = modelName,
                        Version = modelVersion
                    }
                }
            };

            if (!string.IsNullOrEmpty(modelSource))
            {
                deploymentData.Properties.Model.Source = modelSource;
            }

            if (!string.IsNullOrEmpty(skuName))
            {
                deploymentData.Sku = new CognitiveServicesSku(skuName);
                if (skuCapacity.HasValue)
                {
                    deploymentData.Sku.Capacity = skuCapacity;
                }
            }

            if (!string.IsNullOrEmpty(scaleType))
            {
                deploymentData.Properties.ScaleSettings = new CognitiveServicesAccountDeploymentScaleSettings
                {
                    ScaleType = scaleType,
                    Capacity = scaleCapacity
                };
            }

            var deploymentOperation = await cognitiveServicesAccount.Value.GetCognitiveServicesAccountDeployments()
                .CreateOrUpdateAsync(waitUntil: WaitUntil.Completed, deploymentName, deploymentData);

            CognitiveServicesAccountDeploymentResource deployment = deploymentOperation.Value;

            if (!deployment.HasData)
            {
                return new Dictionary<string, object>
                {
                    { "has_data", false },
                };
            }

            // Manually converting system data to a dictionary due to lack of available JsonSerializer support
            return new Dictionary<string, object>
            {
                { "has_data", true },
                { "id", deployment.Data.Id.ToString() },
                { "name", deployment.Data.Name },
                { "type", deployment.Data.ResourceType.ToString() },
                { "sku", deployment.Data.Sku },
                { "tags", deployment.Data.Tags },
                { "properties", deployment.Data.Properties },
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to deploy model: {ex.Message}", ex);
        }
    }

    public async Task<List<PersistentAgent>> ListAgents(string endpoint, string? tenantId = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(endpoint);

        try
        {
            var credential = await GetCredential(tenantId);
            var agentsClient = new AIProjectClient(new Uri(endpoint), credential).GetPersistentAgentsClient();

            var agents = new List<PersistentAgent>();
            await foreach (var agent in agentsClient.Administration.GetAgentsAsync())
            {
                if (agent != null)
                {
                    agents.Add(agent);
                }
            }

            return agents;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to list evaluation agents: {ex.Message}", ex);
        }
    }

    public async Task<Dictionary<string, object>> ConnectAgent(
        string agentId,
        string query,
        string endpoint,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        try
        {
            ValidateRequiredParameters(agentId, query, endpoint);

            var credential = await GetCredential(tenantId);
            var agentsClient = new AIProjectClient(new Uri(endpoint), credential).GetPersistentAgentsClient();

            var thread = await agentsClient.Threads.CreateThreadAsync();
            var threadId = thread.Value.Id;

            await agentsClient.Messages.CreateMessageAsync(threadId, MessageRole.User, query);

            var run = await agentsClient.Runs.CreateRunAsync(threadId, agentId);
            var runId = run.Value.Id;

            while (run.Value.Status == RunStatus.Queued || run.Value.Status == RunStatus.InProgress || run.Value.Status == RunStatus.RequiresAction)
            {
                await Task.Delay(1);
                run = await agentsClient.Runs.GetRunAsync(threadId, runId);
            }

            if (run.Value.Status == RunStatus.Failed)
            {
                string errorMsg = $"Agent run failed: {run.Value.LastError}";
                return new Dictionary<string, object>
                {
                    { "success", false },
                    { "error", errorMsg },
                    { "thread_id", threadId },
                    { "run_id", runId },
                    { "result", $"Error: {errorMsg}" }
                };
            }

            var (response, citations) = buildResponseAndCitations(agentsClient, threadId);

            return new Dictionary<string, object>
            {
                { "success", true },
                { "thread_id", threadId },
                { "run_id", runId },
                { "response", response.ToString().Trim() },
                { "citations", citations }
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to connect to agent: {ex.Message}", ex);
        }
    }

    public async Task<Dictionary<string, object>> QueryAndEvaluateAgent(string agentId, string query, string endpoint, string? tenant = null, List<string>? evaluatorNames = null, RetryPolicyOptions? retryPolicy = null)
    {
        try
        {
            ValidateRequiredParameters(agentId, query, endpoint);

            var credential = await GetCredential(tenant);

            var agentClient = new PersistentAgentsClient(endpoint, credential);

            var thread = await agentClient.Threads.CreateThreadAsync();

            var agentsChatClient = agentClient.AsIChatClient(agentId, thread.Value.Id);

            var chatResponse = await agentsChatClient.GetResponseAsync(new ChatMessage(ChatRole.User, query));
            var messages = await agentClient.Messages.GetMessagesAsync(thread.Value.Id, order: ListSortOrder.Ascending).ToListAsync();
            var runSteps = await agentClient.Runs.GetRunStepsAsync(thread.Value.Id, chatResponse.ResponseId, order: ListSortOrder.Ascending).ToListAsync();
            var run = await agentClient.Runs.GetRunAsync(thread.Value.Id, chatResponse.ResponseId);

            List<PersistentThreadMessage> requestMessages = [];
            List<PersistentThreadMessage> responseMessages = [];

            foreach (var message in messages)
            {
                if (message.Role == MessageRole.User)
                {
                    requestMessages.Add(message);
                }
                else
                {
                    responseMessages.Add(message);
                }
            }

            var convertedRequestMessages = ConvertMessages(requestMessages).ToList();
            convertedRequestMessages.Prepend(new ChatMessage(ChatRole.System, run.Value.Instructions));

            // full list of messages converted to Microsoft.Extensions.AI.ChatMessage for evaluation
            var convertedResponse = ConvertMessages(messages)
                .Concat(ConvertSteps(runSteps))
                .OrderBy(o => o.RawRepresentation switch
                {
                    PersistentThreadMessage m => m.CreatedAt,
                    RunStep s => s.CreatedAt,
                    _ => default,
                })
                .ThenBy(o => o.Contents!.OfType<FunctionCallContent>().Any() ? 0 : o.Contents!.OfType<FunctionResultContent>().Any() ? 1 : 2)
                .ToList();

            var convertedTools = ConvertTools(run.Value.Tools).ToList();

            List<IEvaluator> evaluators = [];
            List<EvaluationContext> evaluationContexts = [];

            if (evaluatorNames == null || evaluatorNames.Count == 0)
            {
                evaluatorNames = AgentEvaluatorDictionary.Keys.ToList();
            }

            foreach (var name in evaluatorNames)
            {
                var evaluatorName = name.ToLowerInvariant();
                if (AgentEvaluatorDictionary.TryGetValue(evaluatorName, out var createEvaluator))
                {
                    var evaluator = createEvaluator();
                    evaluators.Add(evaluator);
                    evaluationContexts.Add(AgentEvaluatorContextDictionary[evaluatorName](convertedTools));
                }
            }
            var compositeEvaluator = new CompositeEvaluator(evaluators);

            var azureOpenAIChatClient = GetAzureOpenAIChatClient(credential);

            var evaluationResult = await compositeEvaluator.EvaluateAsync(
                convertedRequestMessages,
                new ChatResponse(convertedResponse),
                new ChatConfiguration(azureOpenAIChatClient));

            var (result, citations) = buildResponseAndCitations(agentClient, thread.Value.Id);

            // Prepare the response
            var response = new Dictionary<string, object>
            {
                { "success", true },
                { "agent_id", agentId },
                { "thread_id", thread.Value.Id },
                { "run_id", chatResponse.ResponseId ?? string.Empty },
                { "result", result.ToString().Trim() },
                { "query", query },
                { "response", new ChatResponse(convertedResponse) },
                { "citations", citations },
                { "evaluation_metrics", evaluationResult }
            };

            return response;
        }
        catch (Exception ex)
        {
            return new Dictionary<string, object>
                {
                    { "success", false },
                    { "error", $"Error in query and evaluate: {ex.Message}" }
                };
        }
    }

    public async Task<Dictionary<string, object>> EvaluateAgent(string evaluatorName, string query, string? agentResponse, string? toolCalls, string? toolDefinitions, string? tenantId = null, RetryPolicyOptions? retryPolicy = null)
    {
        try
        {
            if (!AgentEvaluatorDictionary.ContainsKey(evaluatorName.ToLowerInvariant()))
            {
                return new Dictionary<string, object>
                {
                    { "success", false },
                    { "error", $"Unknown evaluator: {evaluatorName}" }
                };
            }

            var evaluator = AgentEvaluatorDictionary[evaluatorName.ToLowerInvariant()]();
            List<EvaluationContext> evaluationContext = [];

            switch (evaluator)
            {
                case IntentResolutionEvaluator:
                    evaluationContext.Add(new IntentResolutionEvaluatorContext(parseToolDefinitions(toolDefinitions)));
                    break;
                case ToolCallAccuracyEvaluator:
                    evaluationContext.Add(new ToolCallAccuracyEvaluatorContext(parseToolDefinitions(toolDefinitions)));
                    break;
                case TaskAdherenceEvaluator:
                    evaluationContext.Add(new TaskAdherenceEvaluatorContext(parseToolDefinitions(toolDefinitions)));
                    break;
            }

            var credential = await GetCredential(tenantId);

            var azureOpenAIChatClient = GetAzureOpenAIChatClient(credential);

            List<ChatMessage> chatRequest = [new(ChatRole.User, query)];

            var toolDefinitionsList = parseToolDefinitions(toolDefinitions);
            ChatResponse chatResponse;
            try
            {
                chatResponse = parseChatResponse(agentResponse);
            }
            catch (JsonException)
            {
                chatResponse = new(new ChatMessage(ChatRole.Assistant, agentResponse));
            }

            var result = await evaluator.EvaluateAsync(
                chatRequest,
                chatResponse,
                new ChatConfiguration(azureOpenAIChatClient),
                evaluationContext);

            return new Dictionary<string, object>
            {
                { "evaluator", evaluatorName },
                { "result", result }
            };
        }
        catch (Exception ex)
        {
            return new Dictionary<string, object>
            {
                { "success", false },
                { "error", $"Error in text evaluation: {ex.Message}" }
            };
        }
    }

    private static List<AIFunction> parseToolDefinitions(string? toolDefinitions)
    {
        if (string.IsNullOrEmpty(toolDefinitions))
        {
            return [];
        }

        using JsonDocument toolDefinitionsAsJson = JsonDocument.Parse(toolDefinitions, new() { AllowTrailingCommas = true });
        List<AIFunction> parseToolDefinitions = [];

        foreach (JsonElement jsonElement in toolDefinitionsAsJson.RootElement.EnumerateArray())
        {
            string? name = jsonElement.TryGetProperty("name", out var ne) ? ne.GetString() : null;
            string? description = jsonElement.TryGetProperty("description", out var de) ? de.GetString() : null;
            JsonElement schema = jsonElement.TryGetProperty("parameters", out var se) ? se : default;

            parseToolDefinitions.Add(new ToolDefinitionAIFunction(name ?? "", description ?? "", schema.Clone()));
        }

        return parseToolDefinitions;
    }

    private static ChatResponse parseChatResponse(string? agentResponse)
    {
        if (string.IsNullOrEmpty(agentResponse))
        {
            return new();
        }

        List<ChatMessage> messages = [];


        using JsonDocument responseJson = JsonDocument.Parse(agentResponse, new() { AllowTrailingCommas = true });
        foreach (JsonElement jsonElement in responseJson.RootElement.EnumerateArray())
        {
            ChatRole role = jsonElement.TryGetProperty("role", out var roleElement) ? new ChatRole(roleElement.GetRawText()) : ChatRole.User;
            List<AIContent> contents = [];

            if (jsonElement.TryGetProperty("content", out var content))
            {
                if (content.ValueKind == JsonValueKind.String)
                {
                    contents.Add(new TextContent(content.GetRawText()));
                }
                else if (content.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement contentElement in content.EnumerateArray())
                    {
                        if (!contentElement.TryGetProperty("type", out var typeElement) || typeElement.ValueKind != JsonValueKind.String)
                            continue;

                        switch (typeElement.GetString())
                        {
                            case "text":
                                if (contentElement.TryGetProperty("text", out var textElement))
                                {
                                    contents.Add(new TextContent(textElement.GetRawText()));
                                }
                                break;

                            case "tool_call":
                                if (contentElement.TryGetProperty("tool_call_id", out var callId) && callId.ValueKind == JsonValueKind.String &&
                                    contentElement.TryGetProperty("name", out var nameElement) && nameElement.ValueKind == JsonValueKind.String &&
                                    contentElement.TryGetProperty("arguments", out var argumentsElement))
                                {
                                    contents.Add(new FunctionCallContent(callId.GetRawText(), nameElement.GetString() ?? "",
                                        JsonSerializer.Deserialize(argumentsElement, DictionaryTypeInfo)));
                                }
                                break;

                            case "tool_result":
                                if (jsonElement.TryGetProperty("tool_call_id", out var resultId) && resultId.ValueKind == JsonValueKind.String &&
                                    contentElement.TryGetProperty("tool_result", out var toolResultElement))
                                {
                                    contents.Add(new FunctionResultContent(resultId.GetString() ?? "", toolResultElement.Clone()));
                                }
                                break;
                        }
                    }
                }
            }
            messages.Add(new(role, contents));
        }
        return new(messages);
    }

    private static IEnumerable<ChatMessage> ConvertMessages(IEnumerable<PersistentThreadMessage> messages)
    {
        foreach (PersistentThreadMessage message in messages)
        {
            ChatMessage result = new()
            {
                AuthorName = message.AssistantId,
                MessageId = message.Id,
                RawRepresentation = message,
                Role = message.Role == MessageRole.Agent ? ChatRole.Assistant : ChatRole.User,
            };

            foreach (var messageContent in message.ContentItems)
            {
                AIContent content = messageContent switch
                {
                    MessageTextContent mtc => new TextContent(mtc.Text),
                    _ => new AIContent(),
                };
                content.RawRepresentation = messageContent;
                result.Contents.Add(content);
            }

            yield return result;
        }
    }

    private static IEnumerable<ChatMessage> ConvertSteps(IEnumerable<RunStep> steps)
    {
        foreach (RunStep step in steps)
        {
            if (step.StepDetails is RunStepToolCallDetails { ToolCalls: not null } details)
            {
                foreach (RunStepToolCall toolCall in details.ToolCalls)
                {
                    ChatMessage CreateRequestMessage(string name, Dictionary<string, object?> arguments) =>
                        new(ChatRole.Assistant, [new FunctionCallContent(toolCall.Id, name, arguments)])
                        {
                            AuthorName = step.AssistantId,
                            MessageId = step.Id,
                            RawRepresentation = step,
                        };

                    ChatMessage CreateResponseMessage(object result) =>
                        new(ChatRole.Tool, [new FunctionResultContent(toolCall.Id, result)])
                        {
                            AuthorName = step.AssistantId,
                            MessageId = step.Id,
                            RawRepresentation = step,
                        };

                    switch (toolCall)
                    {
                        case RunStepFunctionToolCall function:
                            yield return CreateRequestMessage(function.Name, Parse(function.Arguments) ?? []);
                            // TODO: output doesn't appear to be available in the API

                            static Dictionary<string, object?>? Parse(string arguments)
                            {
                                try { return JsonSerializer.Deserialize(arguments, DictionaryTypeInfo); }
                                catch { return null; }
                            }
                            break;

                        case RunStepCodeInterpreterToolCall code:
                            yield return CreateRequestMessage("code_interpreter", new() { ["input"] = code.Input });
                            yield return CreateResponseMessage(string.Concat(code.Outputs.OfType<RunStepCodeInterpreterLogOutput>().Select(o => o.Logs)));
                            break;

                        case RunStepBingGroundingToolCall bing:
                            yield return CreateRequestMessage("bing_grounding", new() { ["requesturl"] = bing.BingGrounding["requesturl"] });
                            break;

                        case RunStepFileSearchToolCall fileSearch:
                            yield return CreateRequestMessage("file_search", new()
                            {
                                ["ranking_options"] = JsonSerializer.SerializeToElement(new()
                                {
                                    ["ranker"] = fileSearch.FileSearch.RankingOptions.Ranker,
                                    ["score_threshold"] = fileSearch.FileSearch.RankingOptions.ScoreThreshold,
                                }, DictionaryTypeInfo)
                            });
                            yield return CreateResponseMessage(fileSearch.FileSearch.Results.Select(r => new
                            {
                                file_id = r.FileId,
                                file_name = r.FileName,
                                score = r.Score,
                                content = r.Content,
                            }).ToList());
                            break;

                        case RunStepAzureAISearchToolCall aiSearch:
                            yield return CreateRequestMessage("azure_ai_search", new() { ["input"] = aiSearch.AzureAISearch["input"] });
                            yield return CreateResponseMessage(new Dictionary<string, object?>
                            {
                                ["output"] = aiSearch.AzureAISearch["output"]
                            });
                            break;

                        case RunStepMicrosoftFabricToolCall fabric:
                            yield return CreateRequestMessage("fabric_dataagent", new() { ["input"] = fabric.MicrosoftFabric["input"] });
                            yield return CreateResponseMessage(fabric.MicrosoftFabric["output"]);
                            break;
                    }
                }
            }
        }
    }

    private static IEnumerable<AITool> ConvertTools(IEnumerable<ToolDefinition> tools)
    {
        foreach (var tool in tools)
        {
            switch (tool)
            {
                case FunctionToolDefinition functionToolDefinition:
                    yield return new ToolDefinitionAIFunction(functionToolDefinition.Name,
                        functionToolDefinition.Description,
                        DeserializeToElement(functionToolDefinition.Parameters));
                    break;
                case CodeInterpreterToolDefinition codeInterpreter:
                    yield return new ToolDefinitionAIFunction(
                        "code_interpreter",
                        "Use code interpreter to read and interpret information from datasets, "
                        + "generate code, and create graphs and charts using your data. Supports "
                        + "up to 20 files.");
                    // codeInterpreter.InputSchema
                    break;
                case BingGroundingToolDefinition bingGrounding:
                    yield return new ToolDefinitionAIFunction(
                        "bing_grounding",
                        "Enhance model output with web data.");
                    break;
                case FileSearchToolDefinition fileSearch:
                    yield return new ToolDefinitionAIFunction(
                        "file_search",
                        "Search for data across uploaded files.");
                    // fileSearch.RankingOptions
                    break;
                case AzureAISearchToolDefinition azureAISearch:
                    yield return new ToolDefinitionAIFunction(
                        "azure_ai_search",
                        "Search an Azure AI Search index for relevant data.");
                    break;
                case MicrosoftFabricToolDefinition microsoftFabric:
                    yield return new ToolDefinitionAIFunction(
                        "microsoft_fabric",
                        "Connect to Microsoft Fabric data agents to retrieve data across different data sources.");
                    break;
            }
        }
    }

    private static (string messageContents, List<string> citations) buildResponseAndCitations(PersistentAgentsClient agentClient, string threadId)
    {
        var responseMessage = agentClient.Messages.GetMessagesAsync(threadId).FirstOrDefaultAsync(msg => msg.Role == MessageRole.Agent).Result;

        var result = new StringBuilder();
        var citations = new List<string>();

        if (responseMessage != null)
        {
            foreach (var messageContent in responseMessage.ContentItems)
            {
                if (messageContent is MessageTextContent messageTextContent)
                {
                    result.AppendLine(messageTextContent.Text);
                    foreach (var messageTextAnnotation in messageTextContent.Annotations)
                    {
                        if (messageTextAnnotation is MessageTextUriCitationAnnotation messageTextUriCitationAnnotation)
                        {
                            var citation = $"[{messageTextUriCitationAnnotation.UriCitation.Title}]({messageTextUriCitationAnnotation.UriCitation.Uri})";
                            if (!citations.Contains(citation))
                            {
                                citations.Add(citation);
                            }
                        }
                    }
                }
            }
        }

        if (citations.Count > 0)
        {
            result.AppendLine("\n\n## Sources");
            foreach (var citation in citations)
            {
                result.AppendLine($"- {citation}");
            }
        }
        return (result.ToString().Trim(), citations);
    }

    private static IChatClient GetAzureOpenAIChatClient(TokenCredential credential)
    {
        var azureOpenAIKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
        var azureOpenAIEndpointUri = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
        if (azureOpenAIEndpointUri == null)
        {
            throw new InvalidOperationException("AZURE_OPENAI_ENDPOINT environment variable is not set. Please set it to use evaluation features.");
        }


        var azureOpenAIDeployment = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT");
        if (azureOpenAIDeployment == null)
        {
            throw new InvalidOperationException("AZURE_OPENAI_DEPLOYMENT environment variable is not set. Please set it to use evaluation features.");
        }

        switch (azureOpenAIKey)
        {
            case null:
                return new AzureOpenAIClient(
                new Uri(azureOpenAIEndpointUri),
                credential).GetChatClient(azureOpenAIDeployment).AsIChatClient();
            default:
                return new AzureOpenAIClient(
                new Uri(azureOpenAIEndpointUri),
                new ApiKeyCredential(azureOpenAIKey)).GetChatClient(azureOpenAIDeployment).AsIChatClient();
        }
    }

    private static JsonTypeInfo<Dictionary<string, object?>> DictionaryTypeInfo { get; } =
        (JsonTypeInfo<Dictionary<string, object?>>)AIJsonUtilities.DefaultOptions.GetTypeInfo(typeof(Dictionary<string, object?>));

    private static JsonElement DeserializeToElement(BinaryData data) =>
        (JsonElement)JsonSerializer.Deserialize(data.ToMemory().Span, AIJsonUtilities.DefaultOptions.GetTypeInfo(typeof(JsonElement)))!;

    private sealed class ToolDefinitionAIFunction(string name, string description, JsonElement? schema = null) : AIFunction
    {
        public override string Name => name;
        public override string Description => description;
        public override JsonElement JsonSchema => schema ?? base.JsonSchema;
        protected override ValueTask<object?> InvokeCoreAsync(AIFunctionArguments arguments, CancellationToken cancellationToken) => throw new NotSupportedException();
    }
}
