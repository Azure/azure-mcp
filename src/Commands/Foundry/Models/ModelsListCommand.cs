using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using ModelContextProtocol.Server;
using AzureMcp.Arguments.Foundry.Models;

namespace AzureMcp.Commands.Foundry.Models;

public sealed class ModelsListCommand : GlobalCommand<ModelsListArguments>
{
    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        "List all models available in the Foundry service.";

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var service = context.GetService<IFoundryService>();
            var models = await service.ListModels(
                args.RetryPolicy);

            context.Response.Results = models?.Count > 0 ? new { models } : null;
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}