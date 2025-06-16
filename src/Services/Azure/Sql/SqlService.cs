// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Sql;
using AzureMcp.Helpers;
using AzureMcp.Options;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Services.Azure.Sql;

public sealed class SqlService(
    ISubscriptionService subscriptionService,
    ITenantService tenantService,
    ICacheService cacheService,
    ILogger<SqlService> logger) : BaseAzureService(tenantService), ISqlService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    private readonly ILogger<SqlService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromHours(1);

    public async Task<Models.Sql.SqlAnalysisResult> GetRecommendationsAsync(
        string database,
        string server,
        string resourceGroup,
        string? tableName,
        int? minImpact,
        string? advisorType,
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroup, server, database);
        var analysisResult = new Models.Sql.SqlAnalysisResult
        {
            Database = database,
            Server = server,
            AnalysisTimestamp = DateTimeOffset.UtcNow
        };

        try
        {
            // Get subscription resource using the subscription service
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy)
                ?? throw new SqlServiceException($"Subscription '{subscription}' not found");

            // Create ARM client using base service method
            var armClient = await CreateArmClientAsync(tenant, retryPolicy);
            var resourceGroupResource = armClient.GetResourceGroupResource(
                ResourceGroupResource.CreateResourceIdentifier(subscriptionResource.Data.SubscriptionId, resourceGroup));

            // Find the server resource
            var serverResponse = await resourceGroupResource.GetSqlServerAsync(server);

            if (serverResponse?.Value == null)
            {
                throw new SqlResourceNotFoundException($"SQL Server '{server}' not found in resource group '{resourceGroup}' and subscription '{subscription}'");
            }

            var serverResource = serverResponse.Value;

            // Get database resource
            var databaseResponse = await serverResource.GetSqlDatabaseAsync(database);

            if (databaseResponse?.Value == null)
            {
                throw new SqlResourceNotFoundException($"Database '{database}' not found on server '{server}'");
            }

            var databaseResource = databaseResponse.Value;
            var recommendations = new List<Models.Sql.SqlRecommendation>();
            int advisorsChecked = 0;

            try
            {
                // Get the collection of advisors for the database
                var advisorCollection = databaseResource.GetSqlDatabaseAdvisors();

                var advisorStatuses = new List<Models.Sql.SqlAdvisorStatus>();

                await foreach (var advisor in advisorCollection.GetAllAsync())
                {
                    advisorsChecked++;
                    var advisorName = advisor.Data.Name ?? "Unknown";
                    _logger.LogDebug("Checking advisor: {AdvisorName}", advisorName);

                    // Skip this advisor if advisor type filter is specified and doesn't match
                    if (!string.IsNullOrEmpty(advisorType) &&
                        !string.Equals(advisorName, advisorType, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // Get advisor status information
                    var recommendationsStatus = advisor.Data.RecommendationsStatus ?? "Unknown";
                    var autoExecuteStatus = advisor.Data.AutoExecuteStatus?.ToString() ?? "Not Available";
                    var recommendedActionsCount = advisor.Data.RecommendedActions?.Count() ?? 0;
                    var hasRecommendations = recommendedActionsCount > 0;

                    // Determine which advisor types are supported
                    var supportedAdvisors = new[] { "CreateIndex", "DropIndex", "ForceLastGoodPlan", "DbParameterization" };
                    var isSupported = supportedAdvisors.Contains(advisorName, StringComparer.OrdinalIgnoreCase);
                    var notes = string.Empty;

                    // Process recommendations if this advisor has them and is supported
                    if (hasRecommendations && isSupported)
                    {
                        foreach (var action in advisor.Data.RecommendedActions!)
                        {
                            var actionDetails = action.Details?.ToString() ?? string.Empty;
                            var createIndexSql = action.ImplementationDetails?.Script ?? string.Empty;
                            var extractedTableName = SqlStatementParser.ExtractTableNameFromDetails(actionDetails);
                            if (string.IsNullOrEmpty(extractedTableName))
                            {
                                extractedTableName = SqlStatementParser.ExtractTableNameFromSql(createIndexSql);
                            }

                            // Apply filters if specified
                            if (!string.IsNullOrEmpty(tableName))
                            {
                                // Skip if tableName filter is specified but this action doesn't match
                                if (!actionDetails.Contains(tableName, StringComparison.OrdinalIgnoreCase) &&
                                    !extractedTableName.Contains(tableName, StringComparison.OrdinalIgnoreCase))
                                {
                                    continue;
                                }
                            }

                            // Extract impact from the estimated impact data if available
                            var estimatedImpact = 0.0;
                            if (action.EstimatedImpact != null)
                            {
                                // Find the CPU impact record
                                var cpuImpact = action.EstimatedImpact.FirstOrDefault(i =>
                                    string.Equals(i.DimensionName, "Cpu", StringComparison.OrdinalIgnoreCase));
                                if (cpuImpact != null && double.TryParse(cpuImpact.AbsoluteValue?.ToString(), out var cpuValue))
                                {
                                    estimatedImpact = cpuValue;
                                }
                            }
                            // Generate DROP INDEX statement if we have a CREATE INDEX
                            var indexName = SqlStatementParser.ExtractIndexNameFromCreateSql(createIndexSql);
                            var dropIndexSql = SqlStatementParser.GenerateDropIndexSql(indexName, extractedTableName);// Map the recommended action to your SqlRecommendation model
                            recommendations.Add(new Models.Sql.SqlRecommendation
                            {
                                Name = action.Name ?? string.Empty,
                                Description = actionDetails,
                                Impact = (int)Math.Round(estimatedImpact),
                                TableName = extractedTableName,
                                ImplementationSql = createIndexSql,
                                RevertSql = dropIndexSql,
                                ImplementationDetails = action.ImplementationDetails?.Method?.ToString() ?? "TSql",
                                ExpectedImprovementPercent = estimatedImpact,
                                RecommendationStatus = recommendationsStatus,
                                RecommendationType = advisorName
                            });
                        }
                        notes = $"Processed {advisor.Data.RecommendedActions.Count()} recommendation(s)";
                    }
                    else if (hasRecommendations && !isSupported)
                    {
                        notes = "Recommendations available but not yet supported by this tool";
                    }
                    else
                    {
                        notes = "No recommendations found";
                    }

                    // Add advisor status to collection
                    advisorStatuses.Add(new Models.Sql.SqlAdvisorStatus
                    {
                        AdvisorName = advisorName,
                        RecommendationsStatus = recommendationsStatus,
                        AutoExecuteStatus = autoExecuteStatus,
                        RecommendedActionsCount = recommendedActionsCount,
                        HasRecommendations = hasRecommendations,
                        IsSupported = isSupported,
                        Notes = notes
                    });
                }

                // Apply minimum impact filter if specified
                if (minImpact.HasValue && minImpact.Value > 0)
                {
                    recommendations = recommendations.Where(r => r.Impact >= minImpact.Value).ToList();
                }

                // Build analysis summary for JSON output
                var analysisSummary = string.Empty;
                if (recommendations.Count > 0)
                {
                    analysisSummary = $"Found {recommendations.Count} actionable recommendation(s). Check the 'ImplementationSql' property for T-SQL commands.";
                }
                else if (advisorStatuses.Any(s => s.HasRecommendations))
                {
                    analysisSummary = "Some advisors have recommendations, but none match current filters or are supported yet.";
                }
                else
                {
                    analysisSummary = "No recommendations found. Database may be well-optimized or needs more query activity.";
                }

                // Update analysis result with success information
                analysisResult = analysisResult with
                {
                    AnalysisSuccessful = true,
                    AdvisorsChecked = advisorsChecked,
                    Recommendations = recommendations,
                    AdvisorStatuses = advisorStatuses,
                    AnalysisSummary = analysisSummary
                };
                _logger.LogInformation(
                    "Analysis completed for database {Database} on server {Server}. Advisors checked: {AdvisorsChecked}, Recommendations found: {RecommendationCount}",
                    database, server, advisorsChecked, recommendations.Count);

                return analysisResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving advisor recommendations for database {Database} on server {Server}",
                    database, server);

                // Return analysis result with error information
                return analysisResult with
                {
                    AnalysisSuccessful = false,
                    AdvisorsChecked = advisorsChecked,
                    AnalysisSummary = $"Error retrieving advisor recommendations: {ex.Message}"
                };
            }
        }
        catch (Exception ex) when (ex is not SqlServiceException)
        {
            var message = $"Error getting recommendations for database '{database}' on server '{server}'";
            _logger.LogError(ex, message);

            // Return analysis result with error information
            return analysisResult with
            {
                AnalysisSuccessful = false,
                AnalysisSummary = $"Analysis failed: {ex.Message}"
            };
        }
    }

    public async Task<List<string>> ListServers(string subscription, string? tenant = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);
        try
        {
            // Get subscription resource using the subscription service
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
            var sqlServers = new List<string>();

            foreach (var server in subscriptionResource.GetSqlServers())
            {
                sqlServers.Add(server.Data.Name);
            }

            return sqlServers;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing SQL servers for subscription {Subscription}", subscription);
            throw new SqlServiceException($"Error listing SQL servers: {ex.Message}", ex);
        }
    }

    public async Task<List<string>> ListDatabases(string server, string resourceGroup, string subscription, string? tenant = null, AuthMethod? authMethod = null, RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription, resourceGroup, server);
        try
        {
            // Get subscription resource using the subscription service
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);

            // Create ARM client using base service method
            var armClient = await CreateArmClientAsync(tenant, retryPolicy);
            var resourceGroupResource = armClient.GetResourceGroupResource(ResourceGroupResource.CreateResourceIdentifier(subscriptionResource.Id, resourceGroup));
            var serverResponse = await resourceGroupResource.GetSqlServerAsync(server);

            if (serverResponse?.Value == null)
            {
                throw new SqlResourceNotFoundException($"SQL Server '{server}' not found in resource group '{resourceGroup}' and subscription '{subscription}'");
            }

            var serverResource = serverResponse.Value;
            var databases = new List<string>();
            await foreach (var db in serverResource.GetSqlDatabases().GetAllAsync())
            {
                databases.Add(db.Data.Name);
            }
            return databases;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing SQL databases for server {Server} in resource group {ResourceGroup}", server, resourceGroup);
            throw new SqlServiceException($"Error listing SQL databases: {ex.Message}", ex);
        }
    }
}

public class SqlServiceException : Exception
{
    public SqlServiceException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}

public class SqlResourceNotFoundException : SqlServiceException
{
    public SqlResourceNotFoundException(string message)
        : base(message)
    {
    }
}

/// <summary>
/// Thrown when SQL authorization fails.
/// </summary>
public class SqlAuthorizationException : Exception
{
    public SqlAuthorizationException(string message) : base(message) { }
}
