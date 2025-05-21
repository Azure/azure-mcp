// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Helpers;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.BestPractices;

public sealed class AzureBestPracticesGetCommand(ILogger<AzureBestPracticesGetCommand> logger) : BaseCommand()
{
    private const string _commandTitle = "Get Azure Best Practices";
    private readonly ILogger<AzureBestPracticesGetCommand> _logger = logger;

    private static readonly string _bestPracticesText = LoadBestPracticesText();

    private static string GetBestPracticesText() => _bestPracticesText;

    private static string LoadBestPracticesText()
    {
        var assembly = typeof(AzureBestPracticesGetCommand).Assembly;
        const string resourceName = "AzureMcp.Resources.azure-best-practices.txt";
        return EmbeddedResourceHelper.ReadEmbeddedResource(assembly, resourceName);
    }

    public override string Name => "get";

    public override string Description =>
        "Returns best practices for secure, production-grade Azure SDK usage. " +
        "Call this command and follow its guidance before generating or suggesting any Azure SDK code. " +
        "This command returns the content of the markdown file as a string array.";

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var bestPractices = GetBestPracticesText();
        context.Response.Status = 200;
        context.Response.Results = ResponseResult.Create(new List<string> { bestPractices }, JsonSourceGenerationContext.Default.ListString);
        context.Response.Message = string.Empty;
        return Task.FromResult(context.Response);
    }

    // Dummy argument ensures OpenAPI generator emits a non-empty `parameters` schema
    protected override void RegisterArguments()
    {
        base.RegisterArguments();

        AddArgument(new ArgumentDefinition<string>(
            name: "_dummy",
            description: "Placeholder argument to ensure OpenAPI schema is valid.",
            value: "",
            defaultValue: "",
            required: false,
            hidden: true // optional: hide from UI/CLI help
        ));
    }
}
