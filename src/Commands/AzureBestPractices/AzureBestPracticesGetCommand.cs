// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Arguments;
using AzureMcp.Models.Command;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;


namespace AzureMcp.Commands.BestPractices;

public sealed class AzureBestPracticesGetCommand(ILogger<AzureBestPracticesGetCommand> logger) : BaseCommand()
{
    private readonly ILogger<AzureBestPracticesGetCommand> _logger = logger;

    private static readonly string _bestPracticesText = LoadBestPracticesText();

    private static string GetBestPracticesText() => _bestPracticesText;

    private static string LoadBestPracticesText()
    {
        var assembly = typeof(AzureBestPracticesGetCommand).Assembly;
        const string resourceName = "AzureMcp.Resources.azure-best-practices.txt";

        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Unable to load embedded resource: {resourceName}");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    protected override string GetCommandName() => "get";

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
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var bestPractices = GetBestPracticesText();
        context.Response.Status = 200;
        context.Response.Results = ResponseResult.Create(new List<string> { bestPractices }, JsonSourceGenerationContext.Default.ListString);
        context.Response.Message = string.Empty;
        return Task.FromResult(context.Response);
    }
}
