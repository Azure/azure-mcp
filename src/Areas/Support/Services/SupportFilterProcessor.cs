// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text;
using System.Text.RegularExpressions;
using AzureMcp.Areas.Support.Models;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Tenant;
using Azure.ResourceManager.Support;

namespace AzureMcp.Areas.Support.Services;

public interface ISupportFilterProcessor
{
    Task<FilterProcessResult> ProcessFilterAsync(string? filter, FilterContext context);
}

public partial class SupportFilterProcessor : BaseAzureService, ISupportFilterProcessor
{

    // Regex patterns for supported OData operators
    [GeneratedRegex(@"\b(serviceName|serviceDisplayName)\s+(eq|ne)\s+['""]([^'""]+)['""]", RegexOptions.IgnoreCase)]
    private static partial Regex ServiceNameRegex();

    [GeneratedRegex(@"\b(problemClassificationName)\s+(eq|ne)\s+['""]([^'""]+)['""]", RegexOptions.IgnoreCase)]
    private static partial Regex ProblemClassificationRegex();

    [GeneratedRegex(@"\b(CreatedDate|Status|ProblemClassificationId|ServiceId)\s+(eq|ne|ge|le|gt|lt)\s+", RegexOptions.IgnoreCase)]
    private static partial Regex SupportedPropertyRegex();

    // Properties that are NOT supported by Azure Support API for OData filtering
    private static readonly HashSet<string> UnsupportedProperties = new(StringComparer.OrdinalIgnoreCase)
    {
        "serviceName", "serviceDisplayName", "title", "description", "severity", "contactDetails"
    };

    public SupportFilterProcessor(ITenantService tenantService) : base(tenantService)
    {
    }

    public async Task<FilterProcessResult> ProcessFilterAsync(string? filter, FilterContext context)
    {
        if (string.IsNullOrWhiteSpace(filter))
        {
            return FilterProcessResult.Success(string.Empty);
        }

        // Single-pass processing: parse and transform in one iteration
        var processedFilter = new StringBuilder(filter);
        var errors = new List<string>();

        try
        {
            // Process serviceName/serviceDisplayName replacements
            var serviceResult = await ProcessServiceNameReferencesAsync(processedFilter.ToString(), context);
            if (!serviceResult.IsSuccess)
            {
                errors.Add(serviceResult.ErrorMessage);
            }
            else if (serviceResult.RequiresReplacement)
            {
                processedFilter.Clear().Append(serviceResult.ReplacementValue);
            }

            // Process problemClassificationName replacements
            var classificationResult = await ProcessProblemClassificationReferencesAsync(processedFilter.ToString(), context);
            if (!classificationResult.IsSuccess)
            {
                errors.Add(classificationResult.ErrorMessage);
            }
            else if (classificationResult.RequiresReplacement)
            {
                processedFilter.Clear().Append(classificationResult.ReplacementValue);
            }

            // Validate no unsupported properties remain
            var validationResult = ValidateFilterProperties(processedFilter.ToString());
            if (!validationResult.IsSuccess)
            {
                errors.Add(validationResult.ErrorMessage);
            }

            if (errors.Count > 0)
            {
                return FilterProcessResult.Error(string.Join("; ", errors));
            }

            return FilterProcessResult.Success(processedFilter.ToString());
        }
        catch (Exception ex)
        {
            return FilterProcessResult.Error($"Filter processing failed: {ex.Message}");
        }
    }

    private async Task<PropertyProcessResult> ProcessServiceNameReferencesAsync(string filter, FilterContext context)
    {
        var matches = ServiceNameRegex().Matches(filter);
        if (matches.Count == 0)
        {
            return PropertyProcessResult.NoChange();
        }

        var processedFilter = filter;
        var hasChanges = false;

        foreach (Match match in matches.Cast<Match>().Reverse()) // Reverse to maintain string positions
        {
            var serviceName = match.Groups[3].Value;
            var @operator = match.Groups[2].Value;

            var serviceId = await ResolveServiceNameToIdAsync(serviceName, context);
            if (serviceId == null)
            {
                return PropertyProcessResult.Error($"Service '{serviceName}' not found");
            }

            var replacement = $"ServiceId {@operator} '{serviceId}'";
            processedFilter = processedFilter.Remove(match.Index, match.Length)
                                          .Insert(match.Index, replacement);
            hasChanges = true;
        }

        return hasChanges ? PropertyProcessResult.Success(processedFilter) : PropertyProcessResult.NoChange();
    }

    private async Task<PropertyProcessResult> ProcessProblemClassificationReferencesAsync(string filter, FilterContext context)
    {
        var matches = ProblemClassificationRegex().Matches(filter);
        if (matches.Count == 0)
        {
            return PropertyProcessResult.NoChange();
        }

        var processedFilter = filter;
        var hasChanges = false;

        foreach (Match match in matches.Cast<Match>().Reverse()) // Reverse to maintain string positions
        {
            var classificationName = match.Groups[3].Value;
            var @operator = match.Groups[2].Value;

            var classificationId = await ResolveProblemClassificationNameToIdAsync(classificationName, context);
            if (classificationId == null)
            {
                return PropertyProcessResult.Error($"Problem classification '{classificationName}' not found");
            }

            var replacement = $"ProblemClassificationId {@operator} '{classificationId}'";
            processedFilter = processedFilter.Remove(match.Index, match.Length)
                                          .Insert(match.Index, replacement);
            hasChanges = true;
        }

        return hasChanges ? PropertyProcessResult.Success(processedFilter) : PropertyProcessResult.NoChange();
    }

    private static PropertyProcessResult ValidateFilterProperties(string filter)
    {
        // Check for any remaining unsupported properties
        var unsupportedMatches = UnsupportedProperties
            .Where(prop => filter.Contains(prop, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (unsupportedMatches.Count > 0)
        {
            var unsupportedList = string.Join(", ", unsupportedMatches);
            return PropertyProcessResult.Error(
                $"Properties [{unsupportedList}] are not supported for OData filtering. " +
                "Supported properties: CreatedDate, Status, ProblemClassificationId, ServiceId");
        }

        return PropertyProcessResult.NoChange();
    }

    private async Task<string?> ResolveServiceNameToIdAsync(string serviceName, FilterContext context)
    {
        try
        {
            var armClient = await CreateArmClientAsync(context.TenantId, context.RetryPolicy);
            var tenantResource = armClient.GetTenants().First();
            var supportServices = tenantResource.GetSupportAzureServices();

            // Single pass with prioritized matching
            string? partialMatch = null;
            
            await foreach (var service in supportServices.GetAllAsync())
            {
                var displayName = service.Data.DisplayName;
                if (string.IsNullOrEmpty(displayName)) continue;

                // Exact match - return immediately
                if (string.Equals(serviceName, displayName, StringComparison.OrdinalIgnoreCase))
                {
                    return service.Data.Name;
                }

                // Store first partial match as fallback
                if (partialMatch == null && 
                    displayName.Contains(serviceName, StringComparison.OrdinalIgnoreCase))
                {
                    partialMatch = service.Data.Name;
                }
            }

            return partialMatch;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to resolve service name '{serviceName}': {ex.Message}");
            return null;
        }
    }

    private async Task<string?> ResolveProblemClassificationNameToIdAsync(string classificationName, FilterContext context)
    {
        try
        {
            var armClient = await CreateArmClientAsync(context.TenantId, context.RetryPolicy);
            var tenantResource = armClient.GetTenants().First();
            var supportServices = tenantResource.GetSupportAzureServices();

            // Single pass with prioritized matching
            string? partialMatch = null;

            await foreach (var service in supportServices.GetAllAsync())
            {
                // Check if service supports problem classifications first
                if (!await ServiceHasProblemClassificationsAsync(service))
                    continue;

                await foreach (var classification in service.GetProblemClassifications().GetAllAsync())
                {
                    var displayName = classification.Data.DisplayName;
                    if (string.IsNullOrEmpty(displayName)) continue;

                    // Exact match - return immediately
                    if (string.Equals(classificationName, displayName, StringComparison.OrdinalIgnoreCase))
                    {
                        return classification.Data.Name;
                    }

                    // Store first partial match as fallback
                    if (partialMatch == null && 
                        displayName.Contains(classificationName, StringComparison.OrdinalIgnoreCase))
                    {
                        partialMatch = classification.Data.Name;
                    }
                }
            }

            return partialMatch;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to resolve problem classification '{classificationName}': {ex.Message}");
            return null;
        }
    }

    private static async Task<bool> ServiceHasProblemClassificationsAsync(SupportAzureServiceResource service)
    {
        try
        {
            // Quick check - try to get first classification
            await using var enumerator = service.GetProblemClassifications().GetAllAsync().GetAsyncEnumerator();
            return await enumerator.MoveNextAsync();
        }
        catch
        {
            return false;
        }
    }
}
