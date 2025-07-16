// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.ClientModel;
using System.Text;
using Azure;
using Azure.AI.Agents.Persistent;
using Azure.AI.OpenAI;
using Azure.AI.Projects;
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
using Microsoft.Extensions.AI.Evaluation.Safety;

namespace AzureMcp.Areas.Foundry.Services;

public class FoundryService : BaseAzureService, IFoundryService
{
    private static readonly Dictionary<string, Func<IEvaluator>> TextEvaluatorDictionary = new()
    {
        { "coherence", () => new CoherenceEvaluator() },
        { "completeness", () => new CompletenessEvaluator() },
        { "equivalence", () => new EquivalenceEvaluator() },
        { "fluency", () => new FluencyEvaluator() },
        { "groundedness", () => new GroundednessEvaluator() },
        { "relevance", () => new RelevanceEvaluator() },
        //{"relevance_truth_and_completeness", () => new RelevanceTruthAndCompletenessEvaluator()},
        { "retrieval", () => new RetrievalEvaluator() },

        { "code_vulnerability", () => new CodeVulnerabilityEvaluator() },
        { "content_harm", () => new ContentHarmEvaluator() },
        { "groundedness_pro", () => new GroundednessProEvaluator() },
        { "hate_and_unfairness", () => new HateAndUnfairnessEvaluator() },
        { "indirect_attack", () => new IndirectAttackEvaluator() },
        { "protected_material", () => new ProtectedMaterialEvaluator() },
        { "self_harm", () => new SelfHarmEvaluator() },
        { "sexual", () => new SexualEvaluator() },
        { "ungrounded_attributes", () => new UngroundedAttributesEvaluator() },
        { "violence", () => new ViolenceEvaluator() },
    };
    private static readonly Dictionary<string, Func<IEvaluator>> AgentEvaluatorDictionary = new()
    {
#pragma warning disable AIEVAL001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        { "intent_resolution", () => new IntentResolutionEvaluator()},
        { "tool_call_accuracy", () => new ToolCallAccuracyEvaluator()},
        { "task_adherence", () => new TaskAdherenceEvaluator()},
#pragma warning restore AIEVAL001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
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

            var responseMessage = await agentsClient.Messages.GetMessagesAsync(threadId).FirstOrDefaultAsync(msg => msg.Role == MessageRole.Agent);

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
                            if (messageTextAnnotation is MessageTextUriCitationAnnotation
                                messageTextUriCitationAnnotation)
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

            return new Dictionary<string, object>
            {
                { "success", true },
                { "thread_id", threadId },
                { "run_id", runId },
                { "result", result.ToString().Trim() },
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

            List<ChatMessage> chatRequest = [new(ChatRole.User, query)];
            var chatResponse = await agentsChatClient.GetResponseAsync(chatRequest);
            var messageList = await agentClient.Messages.GetMessagesAsync(thread.Value.Id).ToListAsync();


            var evaluationClient = new AIProjectClient(new Uri(endpoint), credential);

            List<IEvaluator> evaluators = [];
            var evaluationConfig = new Dictionary<string, EvaluatorConfiguration>();

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
                }
            }
            var compositeEvaluator = new CompositeEvaluator(evaluators);

            var azureOpenAIKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
            var azureOpenAIEndpointUri = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
            if (azureOpenAIEndpointUri == null)
            {
                throw new InvalidOperationException("AZURE_OPENAI_ENDPOINT environment variable is not set.");
            }


            var azureOpenAIDeployment = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT");
            if (azureOpenAIDeployment == null)
            {
                throw new InvalidOperationException("AZURE_OPENAI_DEPLOYMENT environment variable is not set.");
            }

            IChatClient azureOpenAIChatClient;

            switch (azureOpenAIKey)
            {
                case null:
                    azureOpenAIChatClient = new AzureOpenAIClient(
                    new Uri(azureOpenAIEndpointUri),
                    credential).GetChatClient(azureOpenAIDeployment).AsIChatClient();
                    break;
                default:
                    azureOpenAIChatClient = new AzureOpenAIClient(
                    new Uri(azureOpenAIEndpointUri),
                    new ApiKeyCredential(azureOpenAIKey)).GetChatClient(azureOpenAIDeployment).AsIChatClient();
                    break;
            }

            var evaluationResult = await compositeEvaluator.EvaluateAsync(
                chatRequest,
                chatResponse,
                new ChatConfiguration(azureOpenAIChatClient));

            // Prepare the response
            var response = new Dictionary<string, object>
            {
                { "success", true },
                { "agent_id", agentId },
                { "thread_id", thread.Value.Id },
                { "run_id", chatResponse.ResponseId ?? string.Empty },
                { "query", query },
                { "response", chatResponse },
                // { "citations", messageList.Contains("citations") ? messageList["citations"] : new List<string>() },
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

    // public async Task<Dictionary<string, object>> EvaluateText(
    //     List<string>? evaluatorNames = null,
    //     string? filePath = null,
    //     string? content = null,
    //     bool includeStudioUrl = true,
    //     bool returnRowResults = false,
    //     string? endpoint = null,
    //     string? tenantId = null,
    //     RetryPolicyOptions? retryPolicy = null)
    // {
    //     try
    //     {
    //         if (string.IsNullOrEmpty(filePath) && string.IsNullOrEmpty(content))
    //         {
    //             return new Dictionary<string, object>
    //             {
    //                 { "success", false },
    //                 { "error", "Either filePath or content must be provided" }
    //             };
    //         }
    //
    //         evaluatorNames ??= TextEvaluatorDictionary.Keys.ToList();
    //
    //         if (evaluatorNames.Count == 0)
    //         {
    //             return new Dictionary<string, object>
    //             {
    //                 { "success", false },
    //                 { "error", "At least one evaluator name must be provided" }
    //             };
    //         }
    //
    //         foreach (var name in evaluatorNames)
    //         {
    //             if (!TextEvaluatorDictionary.ContainsKey(name.ToLowerInvariant()))
    //             {
    //                 return new Dictionary<string, object>
    //                 {
    //                     { "success", false },
    //                     { "error", $"Unknown evaluator: {name}" }
    //                 };
    //             }
    //         }
    //
    //         string? tempFile = null;
    //         int rowCount = 0;
    //         string inputFile;
    //
    //         try
    //         {
    //             if (!string.IsNullOrEmpty(filePath))
    //             {
    //                 if (!File.Exists(filePath))
    //                 {
    //                     return new Dictionary<string, object>
    //                     {
    //                         { "success", false },
    //                         { "error", $"File not found: {filePath}" }
    //                     };
    //                 }
    //
    //                 inputFile = filePath;
    //
    //                 using (var fileReader = new StreamReader(inputFile))
    //                 {
    //                     while (await fileReader.ReadLineAsync() != null)
    //                     {
    //                         rowCount++;
    //                     }
    //                 }
    //             }
    //             else if (!string.IsNullOrEmpty(content))
    //             {
    //                 tempFile = Path.GetTempFileName();
    //                 await File.WriteAllTextAsync(tempFile, content);
    //
    //                 inputFile = tempFile;
    //                 rowCount = content.Split('\n').Count(line => !string.IsNullOrWhiteSpace(line));
    //             }
    //
    //             Console.WriteLine($"Processing {rowCount} rows for {evaluatorNames.Length} evaluator(s)");
    //
    //             var evaluators = new Dictionary<string, IEvaluator>();
    //             var evaluationConfig = new Dictionary<string, EvaluatorConfiguration>();
    //
    //             foreach (var name in evaluatorNames)
    //             {
    //                 var evaluatorName = name.ToLowerInvariant();
    //                 if (TextEvaluatorDictionary.TryGetValue(evaluatorName, out var createEvaluator))
    //                 {
    //                     var evaluator = createEvaluator();
    //                     evaluators.Add(evaluatorName, evaluator);
    //
    //                     var requirements = TextEvaluatorRequirements[evaluatorName];
    //                     var columnMapping = new Dictionary<string, string>();
    //                     foreach (var fieldRequirement in requirements)
    //                     {
    //                         var field = fieldRequirement.Key;
    //                         var requirement = fieldRequirement.Value;
    //
    //                         if (requirement == "Required")
    //                         {
    //                             columnMapping[field] = $"{{data.{field}}}";
    //                         }
    //                     }
    //
    //                     evaluationConfig[evaluatorName] = new EvaluatorConfiguration { ColumnMapping = columnMapping };
    //                 }
    //             }
    //
    //             var evaluationSettings = new EvaluationSettings
    //             {
    //                 DataSource = inputFile,
    //                 Evaluators = evaluators,
    //                 EvaluatorConfigurations = evaluationConfig
    //             };
    //
    //             if (includeStudioUrl && !string.IsNullOrEmpty(endpoint))
    //             {
    //                 evaluationSettings.AzureAiProjectEndpoint = new Uri(endpoint);
    //
    //                 if (!string.IsNullOrEmpty(tenantId))
    //                 {
    //                     var credential = await GetCredential(tenantId);
    //                     evaluationSettings.AzureAiProjectCredential = credential;
    //                 }
    //             }
    //
    //             var evaluationResult = await EvaluationHelper.EvaluateAsync(evaluationSettings);
    //
    //             // Prepare response
    //             var response = new Dictionary<string, object>
    //             {
    //                 { "success", true },
    //                 { "evaluators", evaluatorNames },
    //                 { "row_count", rowCount },
    //                 { "metrics", evaluationResult.Metrics ?? new Dictionary<string, object>() }
    //             };
    //
    //             // Only include detailed row results if explicitly requested
    //             if (returnRowResults && evaluationResult.RowResults != null)
    //             {
    //                 response["row_results"] = evaluationResult.RowResults;
    //             }
    //
    //             // Include studio URL if available
    //             if (includeStudioUrl && !string.IsNullOrEmpty(evaluationResult.StudioUrl))
    //             {
    //                 response["studio_url"] = evaluationResult.StudioUrl;
    //             }
    //
    //             return response;
    //         }
    //         catch (Exception ex)
    //         {
    //             return new Dictionary<string, object>
    //             {
    //                 { "success", false },
    //                 { "error", $"Evaluation error: {ex.Message}" }
    //             };
    //         }
    //         finally
    //         {
    //             // Clean up temp file if we created one
    //             if (!string.IsNullOrEmpty(tempFile) && File.Exists(tempFile))
    //             {
    //                 try
    //                 {
    //                     File.Delete(tempFile);
    //                 }
    //                 catch
    //                 {
    //                     // Ignore errors in cleanup
    //                 }
    //             }
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return new Dictionary<string, object>
    //         {
    //             { "success", false },
    //             { "error", $"Error in text evaluation: {ex.Message}" }
    //         };
    //     }
    // }

    public async Task<Dictionary<string, object>> EvaluateAgent(string evaluatorName, string query, string agentResponse, string azureEndpoint, string deploymentName, string? tenantId = null, RetryPolicyOptions? retryPolicy = null)
    {
        try
        {
            if (!AgentEvaluatorDictionary.ContainsKey(evaluatorName.ToLowerInvariant()))
            {
                return new Dictionary<string, object>
                {
                    { "error", $"Unknown evaluator: {evaluatorName}" }
                };
            }

            var evaluator = AgentEvaluatorDictionary[evaluatorName.ToLowerInvariant()]();

            List<ChatMessage> messages =
            [
                new ChatMessage(ChatRole.User, query)
            ];

            var credential = await GetCredential(tenantId);

            var azureOpenAIClient = new AzureOpenAIClient(new Uri(azureEndpoint), credential);
            var chatClient = azureOpenAIClient.GetChatClient(deploymentName).AsIChatClient();
            var chatConfiguration = new ChatConfiguration(chatClient);

            var result = await evaluator.EvaluateAsync(
                messages,
                new ChatResponse(new ChatMessage(ChatRole.Assistant, agentResponse)),
                chatConfiguration);

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
}
