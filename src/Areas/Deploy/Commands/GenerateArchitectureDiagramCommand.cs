// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using AzureMcp.Areas.Deploy.Options;
using AzureMcp.Commands;
using AzureMcp.Helpers;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Deploy.Commands;

public sealed class GenerateArchitectureDiagramCommand(ILogger<GenerateArchitectureDiagramCommand> logger) : BaseCommand()
{
    private const string CommandTitle = "Generate Architecture Diagram";
    private readonly ILogger<GenerateArchitectureDiagramCommand> _logger = logger;

    public override string Name => "architecture-diagram-generate";

    private readonly Option<string> _rawMcpToolInputOption = DeployOptionDefinitions.RawMcpToolInput.RawMcpToolInputOption;

    public override string Description =>
        "Generates an azure service architecture diagram for the application based on the provided app topology."
        + "Call this tool when the user need recommend or design the azure architecture of their application."
        + "Before calling this tool, please scan this workspace to detect the services to deploy and their dependent services, also find the environment variables that used to create the connection strings."
        + "If it's a .NET Aspire application, check aspireManifest.json file if there is. Try your best to fulfill the input schema with your analyze result.";

    public override string Title => "Generate Architecture Diagram";

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_rawMcpToolInputOption);
    }

    private RawMcpToolInputOptions BindOptions(ParseResult parseResult)
    {
        var options = new RawMcpToolInputOptions();
        options.RawMcpToolInput = parseResult.GetValueForOption(_rawMcpToolInputOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var options = BindOptions(parseResult);
            var rawMcpToolInput = options.RawMcpToolInput;
            if (string.IsNullOrWhiteSpace(rawMcpToolInput))
            {
                throw new ArgumentException("App topology cannot be null or empty.", nameof(options.RawMcpToolInput));
            }

            AppTopology appTopology;
            try
            {
                appTopology = JsonSerializer.Deserialize(rawMcpToolInput, DeployJsonContext.Default.AppTopology)
                    ?? throw new ArgumentException("Failed to deserialize app topology.", nameof(rawMcpToolInput));
            }
            catch (JsonException ex)
            {
                throw new ArgumentException($"Invalid JSON format: {ex.Message}", nameof(rawMcpToolInput), ex);
            }

            _logger.LogInformation("Successfully parsed app topology with {ServiceCount} services", appTopology.Services.Length);

            if (appTopology.Services.Length == 0)
            {
                _logger.LogWarning("No services detected in the app topology.");
                context.Response.Status = 200;
                context.Response.Message = "No service detected.";
                return Task.FromResult(context.Response);
            }

            var chart = GenerateMermaidChart.GenerateChart(appTopology.WorkspaceFolder ?? "", appTopology);
            if (string.IsNullOrWhiteSpace(chart))
            {
                throw new InvalidOperationException("Failed to generate architecture diagram. The chart content is empty.");
            }
            var encodedDiagram = EncodeMermaid.GetEncodedMermaidChart(chart);

            var mermaidUrl = $"https://mermaid.live/view#pako:{encodedDiagram}";
            _logger.LogInformation("Generated architecture diagram successfully. Mermaid URL: {MermaidUrl}", mermaidUrl);

            var usedServiceTypes = appTopology.Services
                .SelectMany(service => service.Dependencies)
                .Select(dep => dep.ServiceType)
                .Where(serviceType => !string.IsNullOrWhiteSpace(serviceType))
                .Where(serviceType => Enum.GetNames<AzureServiceConstants.AzureServiceType>().Contains(serviceType, StringComparer.OrdinalIgnoreCase))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToArray();

            var usedServiceTypesString = usedServiceTypes.Length > 0
                ? string.Join(", ", usedServiceTypes)
                : null;

            context.Response.Message = $"Help the user open up this URI to preview their app topology using tool open_simple_browser: {mermaidUrl} \n"
                + "Ask user if the topology is expected, if not, you should call this tool with the user's updated instructions. "
                + "Please inform the user that here are the supported hosting technologies: "
                + $"{string.Join(", ", Enum.GetNames<AzureServiceConstants.AzureComputeServiceType>())}. ";
            if (!string.IsNullOrWhiteSpace(usedServiceTypesString))
            {
                context.Response.Message += $"Here is the full list of supported component service types for the topology: {usedServiceTypesString}.";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate architecture diagram.");
            HandleException(context, ex);
        }

        return Task.FromResult(context.Response);
    }
}
