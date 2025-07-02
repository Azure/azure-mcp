// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Runtime.InteropServices;
using AzureMcp.Areas.Extension.Options;
using AzureMcp.Commands;
using AzureMcp.Services.ProcessExecution;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Extension.Commands;

public sealed class AzqrCommand(ILogger<AzqrCommand> logger, int processTimeoutSeconds = 300) : GlobalCommand<AzqrOptions>()
{
    private const string CommandTitle = "Azure Quick Review CLI Command";
    private readonly ILogger<AzqrCommand> _logger = logger;
    private readonly int _processTimeoutSeconds = processTimeoutSeconds;
    private readonly Option<string> _subscriptionIdOption = ExtensionOptionDefinitions.Azqr.SubscriptionId;
    private readonly Option<string> _resourceGroupNameOption = ExtensionOptionDefinitions.Azqr.ResourceGroupName;
    private static string? _cachedAzqrPath;

    public override string Name => "azqr";

    public override string Description =>
        """
        Runs Azure Quick Review CLI (azqr) commands to generate compliance/security reports for Azure resources.
        Requires a subscription id and optionally a resource group name. Returns the generated report file path and its content.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_subscriptionIdOption);
        command.AddOption(_resourceGroupNameOption);
    }

    protected override AzqrOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.SubscriptionId = parseResult.GetValueForOption(_subscriptionIdOption)!;
        options.ResourceGroupName = parseResult.GetValueForOption(_resourceGroupNameOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);
        var response = context.Response;
        try
        {
            if (!Validate(parseResult.CommandResult, response).IsValid)
                return response;

            ArgumentNullException.ThrowIfNull(options.SubscriptionId);

            var azqrPath = FindAzqrCliPath() ?? throw new FileNotFoundException("Azure Quick Review CLI (azqr) executable not found in PATH. Please ensure azqr is installed.");

            // Compose azqr command
            var command = $"scan --subscription-id {options.SubscriptionId}";
            if (!string.IsNullOrWhiteSpace(options.ResourceGroupName))
            {
                command += $" --resource-group {options.ResourceGroupName}";
            }

            var tempDir = Path.GetTempPath();
            var dateString = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
            var reportFileName = Path.Combine(tempDir, $"azqr-report-{options.SubscriptionId}-{dateString}");

            // TODO: Make Azure Quick Review CLI produce a json report that the LLM may read and summarize because the LLM doesn't support reading xlsx files.
            // Azure Quick Review always appends the file extension to the report file's name, we need to create a new path with the file extension to check for the existence of the report file.
            var reportFilePath = $"{reportFileName}.xlsx";
            command += $" --output-name \"{reportFileName}\"";

            var processService = context.GetService<IExternalProcessService>();
            var result = await processService.ExecuteAsync(azqrPath, command, _processTimeoutSeconds);

            if (result.ExitCode != 0)
            {
                response.Status = 500;
                response.Message = result.Error;
                return response;
            }

            if (!File.Exists(reportFilePath))
            {
                response.Status = 500;
                response.Message = $"Report file '{reportFilePath}' was not found after azqr execution.";
                return response;
            }
            var resultObj = new AzqrReportResult(reportFilePath, result.Output);
            response.Results = ResponseResult.Create(resultObj, JsonSourceGenerationContext.Default.AzqrReportResult);
            response.Status = 200;
            response.Message = "azqr report generated successfully.";
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred executing azqr command.");
            HandleException(response, ex);
            return response;
        }
    }

    private static string? FindAzqrCliPath()
    {
        // Return cached path if available and still exists
        if (!string.IsNullOrEmpty(_cachedAzqrPath) && File.Exists(_cachedAzqrPath))
        {
            return _cachedAzqrPath;
        }
        var exeName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "azqr.exe" : "azqr";
        var pathEnv = Environment.GetEnvironmentVariable("PATH");
        if (string.IsNullOrEmpty(pathEnv))
            return null;
        foreach (var dir in pathEnv.Split(Path.PathSeparator))
        {
            var fullPath = Path.Combine(dir.Trim(), exeName);
            if (File.Exists(fullPath))
            {
                _cachedAzqrPath = fullPath;
                return fullPath;
            }
        }
        return null;
    }
}
