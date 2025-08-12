// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Helpers;
using AzureMcp.Deploy.Options;
using AzureMcp.Deploy.Options.Architecture;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Deploy.Commands.Architecture;

public sealed class DiagramGenerateCommand(ILogger<DiagramGenerateCommand> logger) : BaseCommand()
{
    private const string CommandTitle = "Generate Architecture Diagram";
    private readonly ILogger<DiagramGenerateCommand> _logger = logger;

    public override string Name => "generate";

    private readonly Option<string> _rawMcpToolInputOption = DeployOptionDefinitions.RawMcpToolInput.RawMcpToolInputOption;

    public override string Description =>
        "Generates an azure service architecture diagram for the application based on the provided app topology."
        + "Call this tool when the user need recommend or design the azure architecture of their application."
        + "Do not call this tool when the user need detailed design of the azure architecture, such as the network topology, security design, etc."
        + "Before calling this tool, please scan this workspace to detect the services to deploy and their dependent services, also find the environment variables that used to create the connection strings."
        + "If it's a .NET Aspire application, check aspireManifest.json file if there is. Try your best to fulfill the input schema with your analyze result.";

    public override string Title => "Generate Architecture Diagram";
    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_rawMcpToolInputOption);
    }

    private DiagramGenerateOptions BindOptions(ParseResult parseResult)
    {
        var options = new DiagramGenerateOptions();
        options.RawMcpToolInput = parseResult.GetValueForOption(_rawMcpToolInputOption);
        return options;
    }

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

            context.Response.Message = $"Here is the user's mermaid diagram. Please write this into .azure/architecture.copilot.md. Make changes if these do not fulfill requirements:\n ```mermaid\n{chart}\n``` \n"
                + "Ask user if the topology is expected, if not, you should directly update the generated diagram with the user's updated instructions. Remind the user to install a Mermaid preview extension to be able to render the diagram. "
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
