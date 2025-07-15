// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AzureBestPractices.Commands;

public sealed class AzureBestPracticesGetCommand(ILogger<AzureBestPracticesGetCommand> logger) : BaseBestPracticesCommand<AzureBestPracticesGetCommand>(logger)
{
    private const string CommandTitle = "Get Azure Best Practices";

    protected override string ResourceFileName => "azure-best-practices.txt";

    public override string Name => "get";

    public override string Description =>
        "Returns best practices for secure, production-grade Azure SDK usage. " +
        "Call this command and follow its guidance before generating or suggesting any Azure SDK code. " +
        "This command returns the content of the markdown file as a string array.";

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        return base.ExecuteAsync(context, parseResult);
    }
}
