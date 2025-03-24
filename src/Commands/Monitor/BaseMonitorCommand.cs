using AzureMCP.Arguments;
using AzureMCP.Models;
using AzureMCP.Services.Interfaces;
using System.CommandLine;

namespace AzureMCP.Commands.Monitor;

public abstract class BaseMonitorCommand<TArgs> : BaseCommand<TArgs> where TArgs : BaseArgumentsWithSubscriptionId, new()
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

    protected virtual ArgumentChain<TArgs> CreateWorkspaceIdArgument(
        Func<CommandContext, string, Task<List<ArgumentOption>>> workspaceOptionsLoader)
    {
        return ArgumentChain<TArgs>
            .Create(ArgumentDefinitions.Monitor.WorkspaceId.Name, ArgumentDefinitions.Monitor.WorkspaceId.Description)
            .WithCommandExample($"{GetCommandPath()} --workspace-id <workspace-id>")
            .WithValueAccessor(args =>
            {
                try
                {
                    return ((dynamic)args).WorkspaceId ?? string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            })
            .WithValueLoader(async (context, args) => await workspaceOptionsLoader(context, args.SubscriptionId ?? string.Empty))
            .WithIsRequired(true);
    }

    protected virtual ArgumentChain<TArgs> CreateWorkspaceNameArgument(
        Func<CommandContext, string, Task<List<ArgumentOption>>> workspaceOptionsLoader)
    {
        return ArgumentChain<TArgs>
            .Create(ArgumentDefinitions.Monitor.WorkspaceName.Name, ArgumentDefinitions.Monitor.WorkspaceName.Description)
            .WithCommandExample($"{GetCommandPath()} --workspace-name <workspace-name>")
            .WithValueAccessor(args =>
            {
                try
                {
                    return ((dynamic)args).WorkspaceName ?? string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            })
            .WithValueLoader(async (context, args) => await workspaceOptionsLoader(context, args.SubscriptionId ?? string.Empty))
            .WithIsRequired(true);
    }
}
