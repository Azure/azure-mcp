// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using AzureMcp.Commands;
using AzureMcp.Helpers;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.TerraformBestPractices.Commands;

public sealed class TerraformBestPracticesGetCommand(ILogger<TerraformBestPracticesGetCommand> logger) : BaseCommand()
{

    private const string CommandTitle = "Get Terraform Best Practices";
    private readonly ILogger<TerraformBestPracticesGetCommand> _logger = logger;

    private static readonly string s_bestPracticesText = LoadBestPracticesText();

    private static string GetBestPracticesText() => s_bestPracticesText;

    private static string LoadBestPracticesText()
    {

        Assembly assembly = typeof(TerraformBestPracticesGetCommand).Assembly;
        string resourceName = EmbeddedResourceHelper.FindEmbeddedResource(assembly, "terraform-best-practices.txt");
        return EmbeddedResourceHelper.ReadEmbeddedResource(assembly, resourceName);
    }

    public override string Name => "get";

    public override string Description =>
        "Returns Terraform best practices. " +
        "Call this command and follow its guidance before generating or suggesting any Terraform code. " +
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
