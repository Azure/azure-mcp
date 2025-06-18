// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Helpers;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.BestPractices;

public sealed class AzureBestPracticesGetCommand(ILogger<AzureBestPracticesGetCommand> logger) : BaseCommand()
{
    private const string CommandTitle = "Get Azure Best Practices";
    private readonly ILogger<AzureBestPracticesGetCommand> _logger = logger;

    private static readonly string s_bestPracticesText = LoadBestPracticesText();

    private static string GetBestPracticesText() => s_bestPracticesText;

    private static string LoadBestPracticesText()
    {
        var assembly = typeof(AzureBestPracticesGetCommand).Assembly;
        const string ResourceName = "AzureMcp.Resources.azure-best-practices.txt";
        return EmbeddedResourceHelper.ReadEmbeddedResource(assembly, ResourceName);
    }

    public override string Name => "get";

    public override string Description =>
        "Returns best practices for secure, production-grade Azure SDK usage. " +
        "Call this command and follow its guidance before generating or suggesting any Azure SDK code. " +
        "This command returns the content of the markdown file as a string array.";

    public override string Title => CommandTitle;


    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var bestPractices = GetBestPracticesText();
        context.Response.Status = 200;
        context.Response.Results = ResponseResult.Create(new List<string> { bestPractices }, JsonSourceGenerationContext.Default.ListString);
        context.Response.Message = string.Empty;
        return Task.FromResult(context.Response);
    }
}
