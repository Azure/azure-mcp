// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Redis.Commands;
using AzureMcp.Redis.Models.CacheForRedis;
using AzureMcp.Redis.Options.CacheForRedis;
using AzureMcp.Redis.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Redis.Commands.CacheForRedis;

/// <summary>
/// Lists the access policy assignments in the specified Azure cache.
/// </summary>
public sealed class AccessPolicyListCommand(ILogger<AccessPolicyListCommand> logger) : BaseCacheCommand<AccessPolicyListOptions>()
{
    private const string CommandTitle = "List Redis Cache Access Policy Assignments";
    private readonly ILogger<AccessPolicyListCommand> _logger = logger;

    public override string Name => "list";
    public override string Description =>
        $"""
        List the Access Policies and Assignments for the specified Redis cache. Returns an array of Redis Access Policy Assignment details.
        Use this command to explore which Access Policies have been assigned to which identities for your Redis cache.
        """;

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
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

            var redisService = context.GetService<IRedisService>() ?? throw new InvalidOperationException("Redis service is not available.");
            var accessPolicyAssignments = await redisService.ListAccessPolicyAssignmentsAsync(
                options.Cache!,
                options.ResourceGroup!,
                options.Subscription!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            context.Response.Results = accessPolicyAssignments.Any() ?
                ResponseResult.Create(
                    new AccessPolicyListCommandResult(accessPolicyAssignments),
                    RedisJsonContext.Default.AccessPolicyListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list Redis Access Policy Assignments");
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record AccessPolicyListCommandResult(IEnumerable<AccessPolicyAssignment> AccessPolicyAssignments);
}
