// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Arguments;
using AzureMcp.Models.Command;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.BestPractices;

public sealed class AzureBestPracticesCommand(ILogger<AzureBestPracticesCommand> logger) : GlobalCommand<GlobalArguments>
{
    private static readonly string FilePath = Path.Combine(
        AppContext.BaseDirectory,
        "content", "resources", "azure-best-practices.md"
    );
    private readonly ILogger<AzureBestPracticesCommand> _logger = logger;

    protected override string GetCommandName() => "azure-sdk-best-practices";

    protected override string GetCommandDescription() =>
        "[LLM-MANDATORY] Returns best practices for secure, production-grade Azure SDK usage. " +
        "Call this command and follow its guidance before generating or suggesting any Azure SDK code. " +
        "This command returns the content of the markdown file as a string array. " +
        "#llm #best-practices #code-generation #azure #mandatory";

    protected override void RegisterOptions(Command command)
    {
        // Do not register any options for this command.
    }

    protected override void RegisterArguments()
    {
        // Do not register any arguments for this command.
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                context.Response.Status = 404;
                context.Response.Message = "Best practices file not found.";
                context.Response.Results = null;
                return context.Response;
            }

            var content = await File.ReadAllTextAsync(FilePath);
            context.Response.Status = 200;
            context.Response.Results = ResponseResult.Create(new List<string> { content }, JsonSourceGenerationContext.Default.ListString);
            context.Response.Message = string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading best practices file.");
            HandleException(context.Response, ex);
        }
        return context.Response;
    }
}
