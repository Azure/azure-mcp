// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AzureBestPractices.Commands;

public sealed class AzureFunctionsCodeGenBestPracticesGetCommand(ILogger<AzureFunctionsCodeGenBestPracticesGetCommand> logger) : BaseBestPracticesCommand<AzureFunctionsCodeGenBestPracticesGetCommand>(logger)
{
    private const string CommandTitle = "Get Azure Functions Code Generation Best Practices";

    protected override string ResourceFileName => "azure-functions-codegen-best-practices.txt";

    public override string Name => "get-code-generation";

    public override string Description =>
        "Returns best practices for secure, high-quality Azure Functions code generation. " +
        "Call this command and follow its guidance before generating or suggesting any Azure Functions code. " +
        "This command returns the content of the best practices file as a string array.";

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        return base.ExecuteAsync(context, parseResult);
    }
}
