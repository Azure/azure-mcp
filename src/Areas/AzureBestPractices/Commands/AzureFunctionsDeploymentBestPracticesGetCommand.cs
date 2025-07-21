// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AzureBestPractices.Commands;

public sealed class AzureFunctionsDeploymentBestPracticesGetCommand(ILogger<AzureFunctionsDeploymentBestPracticesGetCommand> logger) : BaseBestPracticesCommand<AzureFunctionsDeploymentBestPracticesGetCommand>(logger)
{
    private const string CommandTitle = "Get Azure Functions Deployment Best Practices";

    protected override string ResourceFileName => "azure-functions-deployment-best-practices.txt";

    public override string Name => "get-deployment";

    public override string Description =>
        "Returns best practices for secure, production-grade Azure Functions deployment. " +
        "Call this command and follow its guidance before generating or suggesting any Azure Functions code. " +
        "This command returns the content of the markdown file as a string array.";

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        return base.ExecuteAsync(context, parseResult);
    }
}
