// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Authorization.Models;
using AzureMcp.Areas.Authorization.Options;
using AzureMcp.Areas.Authorization.Services;
using AzureMcp.Commands.Authorization;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Authorization.Commands;

public sealed class RoleAssignmentListCommand(ILogger<RoleAssignmentListCommand> logger) : SubscriptionCommand<RoleAssignmentListOptions>
{
    private const string _commandTitle = "List Role Assignments";
    private readonly ILogger<RoleAssignmentListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List role assignments. This command retrieves and displays all Azure RBAC role assignments
        in the specified scope. Results include role definition IDs and principal IDs, returned as a JSON array.
        """;

    public override string Title => _commandTitle;

    private readonly Option<string> _scopeOption = OptionDefinitions.Authorization.Scope;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_scopeOption);
    }

    protected override RoleAssignmentListOptions BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Scope = parseResult.GetValueForOption(_scopeOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);

            var authService = context.GetService<IAuthorizationService>();
            var assignments = await authService.ListRoleAssignments(
                options.Scope,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = assignments?.Count > 0 ?
                ResponseResult.Create(
                    new RoleAssignmentListCommandResult(assignments),
                    AuthorizationJsonContext.Default.RoleAssignmentListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing role assignments.");
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record RoleAssignmentListCommandResult(List<RoleAssignment> Assignments);
}
