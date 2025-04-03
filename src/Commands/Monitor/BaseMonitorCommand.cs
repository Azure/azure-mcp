using AzureMCP.Arguments;
using AzureMCP.Arguments.Monitor;
using AzureMCP.Models;
using AzureMCP.Services.Interfaces;
using System.CommandLine;

namespace AzureMCP.Commands.Monitor;

public abstract class BaseMonitorCommand<TArgs> : BaseCommandWithSubscription<TArgs> where TArgs : BaseArgumentsWithSubscription, new()
{
    protected readonly Option<string> _workspaceIdOption;
    protected readonly Option<string> _workspaceNameOption;

    protected BaseMonitorCommand()
        : base()
    {
        _workspaceIdOption = ArgumentDefinitions.Monitor.WorkspaceId.ToOption();
        _workspaceNameOption = ArgumentDefinitions.Monitor.WorkspaceName.ToOption();
    }

    protected async Task<List<ArgumentOption>> GetWorkspaceOptions(CommandContext context, string subscriptionId)
    {
        if (string.IsNullOrEmpty(subscriptionId)) return [];

        var monitorService = context.GetService<IMonitorService>();
        var workspaces = await monitorService.ListWorkspaces(subscriptionId, null);

        return [.. workspaces.Select(w => new ArgumentOption
        {
            Name = w.Name,
            Id = w.CustomerId?.ToString() ?? string.Empty
        })];
    }

    protected virtual ArgumentChain<LogQueryArguments> CreateWorkspaceIdArgument()
    {
        return ArgumentChain<LogQueryArguments>
            .Create(ArgumentDefinitions.Monitor.WorkspaceId.Name, ArgumentDefinitions.Monitor.WorkspaceId.Description)
            .WithCommandExample(ArgumentDefinitions.GetCommandExample(GetCommandPath(), ArgumentDefinitions.Monitor.WorkspaceId))
            .WithValueAccessor(args =>
            {
                try
                {
                    return args.WorkspaceId ?? string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            })
            .WithValueLoader(async (context, args) => await GetWorkspaceOptions(context, args.Subscription ?? string.Empty))
            .WithIsRequired(ArgumentDefinitions.Monitor.WorkspaceId.Required);
    }

    protected virtual ArgumentChain<TableListArguments> CreateWorkspaceNameArgument()
    {
        return ArgumentChain<TableListArguments>
            .Create(ArgumentDefinitions.Monitor.WorkspaceName.Name, ArgumentDefinitions.Monitor.WorkspaceName.Description)
            .WithCommandExample(ArgumentDefinitions.GetCommandExample(GetCommandPath(), ArgumentDefinitions.Monitor.WorkspaceName))
            .WithValueAccessor(args =>
            {
                try
                {
                    return args.WorkspaceName ?? string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            })
            .WithValueLoader(async (context, args) => await GetWorkspaceOptions(context, args.Subscription ?? string.Empty))
            .WithIsRequired(ArgumentDefinitions.Monitor.WorkspaceName.Required);
    }
}
