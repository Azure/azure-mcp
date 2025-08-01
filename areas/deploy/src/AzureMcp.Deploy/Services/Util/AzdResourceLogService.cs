using System.Diagnostics.CodeAnalysis;
using Azure.Core;
using YamlDotNet.Serialization;

namespace Areas.Deploy.Services.Util;

public static class AzdResourceLogService
{
    private const string AzureYamlFileName = "azure.yaml";

    [RequiresDynamicCode("Uses YamlDotNet for deserialization.")]
    public static async Task<string> GetAzdResourceLogsAsync(
        TokenCredential credential,
        string workspaceFolder,
        string azdEnvName,
        string subscriptionId,
        int? limit = null)
    {
        var toolErrorLogs = new List<string>();
        var appLogs = new List<string>();

        try
        {
            var azdAppLogRetriever = new AzdAppLogRetriever(credential, subscriptionId, azdEnvName);
            await azdAppLogRetriever.InitializeAsync();
            await azdAppLogRetriever.GetLogAnalyticsWorkspacesInfoAsync();

            var services = GetServicesFromAzureYaml(workspaceFolder);

            foreach (var (serviceName, service) in services)
            {
                try
                {
                    if (service.Host != null)
                    {
                        var resourceType = ResourceTypeExtensions.GetResourceTypeFromHost(service.Host);
                        var logs = await azdAppLogRetriever.QueryAppLogsAsync(resourceType, serviceName, limit);
                        appLogs.Add(logs);
                    }
                }
                catch (Exception ex)
                {
                    toolErrorLogs.Add($"Error finding app logs for service {serviceName}: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            toolErrorLogs.Add(ex.Message);
        }

        if (appLogs.Count > 0)
        {
            return $"App logs retrieved:\n{string.Join("\n\n", appLogs)}";
        }

        if (toolErrorLogs.Count > 0)
        {
            return $"Error during retrieval of app logs of azd project:\n{string.Join("\n", toolErrorLogs)}";
        }

        return "No logs found.";
    }

    [RequiresDynamicCode("Calls YamlDotNet.Serialization.DeserializerBuilder.DeserializerBuilder()")]
    private static Dictionary<string, Service> GetServicesFromAzureYaml(string workspaceFolder)
    {
        var azureYamlPath = Path.Combine(workspaceFolder, AzureYamlFileName);

        if (!File.Exists(azureYamlPath))
        {
            throw new FileNotFoundException($"Azure YAML file not found at {azureYamlPath}");
        }

        var yamlContent = File.ReadAllText(azureYamlPath);
        var deserializer = new DeserializerBuilder().Build();
        var azureYaml = deserializer.Deserialize<Dictionary<string, object>>(yamlContent);

        if (!azureYaml.TryGetValue("services", out var servicesObj))
        {
            throw new InvalidOperationException("No services section found in azure.yaml");
        }

        var servicesDict = (Dictionary<object, object>)servicesObj;
        var result = new Dictionary<string, Service>();

        foreach (var (key, value) in servicesDict)
        {
            var serviceName = key.ToString()!;
            var serviceDict = (Dictionary<object, object>)value;

            var service = new Service(
                Host: serviceDict.TryGetValue("host", out var host) ? host?.ToString() : null,
                Project: serviceDict.TryGetValue("project", out var project) ? project?.ToString() : null,
                Language: serviceDict.TryGetValue("language", out var language) ? language?.ToString() : null
            );

            result[serviceName] = service;
        }

        return result;
    }
}
public record Service(
    string? Host = null,
    string? Project = null,
    string? Language = null
);
