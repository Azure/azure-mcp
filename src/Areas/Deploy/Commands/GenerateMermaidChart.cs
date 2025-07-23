// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text;
using AzureMcp.Areas.Deploy.Options;

namespace AzureMcp.Areas.Deploy.Commands;

public static class GenerateMermaidChart
{
    public static string GenerateChart(string workspaceFolder, AppTopology appTopology)
    {
        var chartComponents = new List<string>();

        chartComponents.Add("graph TD");

        chartComponents.Add("""
            %% Define styles
                classDef service fill:#50e5ff,stroke:#333,stroke-width:2px,color:#000
                classDef compute fill:#9cf00b,stroke:#333,stroke-width:2px,color:#000
                classDef binding fill:#fef200,stroke:#333,stroke-width:2px,color:#000
            """);

        var services = new List<string> { "%% Services" };
        var resources = new List<string> { "%% Resources" };
        var relationships = new List<string> { "%% Relationships" };

        foreach (var service in appTopology.Services)
        {
            var serviceName = new List<string> { $"Name: {service.Name}" };

            var projectRelativePath = Path.GetRelativePath(workspaceFolder, string.IsNullOrWhiteSpace(service.Path) ? workspaceFolder : service.Path);
            serviceName.Add($"Path: {projectRelativePath}");
            serviceName.Add($"Language: {service.Language}");
            serviceName.Add($"Port: {service.Port}");

            if (service.DockerSettings != null &&
                string.Equals(service.AzureComputeHost, "azurecontainerapp", StringComparison.OrdinalIgnoreCase))
            {
                serviceName.Add($"DockerFile: {service.DockerSettings.DockerFilePath}");
                serviceName.Add($"Docker Context: {service.DockerSettings.DockerContext}");
            }

            var serviceInternalName = $"svc-{service.Name}";

            services.Add(CreateComponentName(serviceInternalName, string.Join("\n", serviceName), "service", NodeShape.Rectangle));

            relationships.Add(CreateRelationshipString(serviceInternalName, service.Name, "hosted on", ArrowType.Solid));
        }

        foreach (var service in appTopology.Services)
        {
            foreach (var dependency in service.Dependencies)
            {
                var instanceInternalName = $"{FlattenServiceType(dependency.ServiceType)}.{dependency.Name}";
                var instanceName = $"{dependency.Name} ({dependency.ServiceType})";

                if (IsComputeResourceType(dependency.ServiceType))
                {
                    resources.Add(CreateComponentName(instanceInternalName, instanceName, "compute", NodeShape.RoundedRectangle));
                }
                else
                {
                    resources.Add(CreateComponentName(instanceInternalName, instanceName, "binding", NodeShape.Circle));
                }

                relationships.Add(CreateRelationshipString(service.Name, instanceInternalName, dependency.ConnectionType, ArrowType.Dotted));
            }
        }

        chartComponents.AddRange(services);
        chartComponents.AddRange(resources);
        chartComponents.AddRange(relationships);

        return string.Join("\n", chartComponents);
    }

    private static string CreateComponentName(string internalName, string name, string type, NodeShape nodeShape)
    {
        var nodeShapeBrackets = GetNodeShapeBrackets(nodeShape);
        return $"{EnsureUrlFriendlyName(internalName)}{nodeShapeBrackets[0]}\"`{name}`\"{nodeShapeBrackets[1]}:::{type}";
    }

    private static string CreateRelationshipString(string sourceName, string targetName, string connectionDescription, ArrowType arrowType)
    {
        var arrowSymbol = GetArrowSymbol(arrowType);
        return $"{EnsureUrlFriendlyName(sourceName)} {arrowSymbol} |\"{connectionDescription}\"| {EnsureUrlFriendlyName(targetName)}";
    }

    private static string EnsureUrlFriendlyName(string name)
    {
        return name.Replace('.', '_')
                  .Replace(" ", "_")
                  .Trim()
                  .ToLowerInvariant();
    }

    private static string[] GetNodeShapeBrackets(NodeShape nodeShape)
    {
        return nodeShape switch
        {
            NodeShape.Rectangle => ["[", "]"],
            NodeShape.Circle => ["((", "))"],
            NodeShape.RoundedRectangle => ["(", ")"],
            NodeShape.Cylinder => ["[(", ")]"],
            NodeShape.Hexagon => ["{{", "}}"],
            _ => ["[", "]"]
        };
    }

    private static string GetArrowSymbol(ArrowType arrowType)
    {
        return arrowType switch
        {
            ArrowType.Solid => "-->",
            ArrowType.Open => "->",
            ArrowType.Dotted => "-.->",
            _ => "-->"
        };
    }

    private static string FlattenServiceType(string serviceType)
    {
        return serviceType.ToLowerInvariant().Replace("azure", "");
    }

    private static bool IsComputeResourceType(string serviceType)
    {
        return Enum.GetNames<AzureServiceConstants.AzureComputeServiceType>().Contains(serviceType, StringComparer.OrdinalIgnoreCase);
    }
}

public enum NodeShape
{
    Rectangle,
    Circle,
    RoundedRectangle,
    Cylinder,
    Hexagon
}

public enum ArrowType
{
    Solid,
    Open,
    Dotted
}
