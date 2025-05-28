// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using AzureMcp.Models.Option;
using AzureMcp.Options.Monitor.HealthModels.Entity;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Monitor.HealthModels.Entity;

public sealed class EntityGetHealthCommand(ILogger<EntityGetHealthCommand> logger) : BaseMonitorHealthModelsCommand<EntityGetHealthOptions>
{
    private const string _commandTitle = "Get the health of an entity in a health model";
    private const string _commandName = "gethealth";
    public override string Name => _commandName;

    public override string Description =>
         $"""
        Gets the health of an entity from a specified Azure Monitor Health Model.
        Returns entity health information.
        
        Required arguments:
        - {OptionDefinitions.Monitor.Health.Entity.Name}: The entity to get health for
        - {OptionDefinitions.Monitor.Health.HealthModel.Name}: The health model name
        """;

    public override string Title => _commandTitle;

    private readonly ILogger<EntityGetHealthCommand> _logger = logger;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_entityOption);
        command.AddOption(_healthModelOption);
        command.AddOption(_resourceGroupOption);
    }

    protected override EntityGetHealthOptions BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Entity = parseResult.GetValueForOption(_entityOption);
        args.HealthModelName = parseResult.GetValueForOption(_healthModelOption);
        args.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle, Name = _commandName)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindOptions(parseResult);

        try
        {
            var validationResult = Validate(parseResult.CommandResult);

            if (!validationResult.IsValid)
            {
                context.Response.Status = 400;
                context.Response.Message = validationResult.ErrorMessage!;
                return context.Response;
            }

            var service = context.GetService<IMonitorHealthModelService>();
            var result = await service.GetEntityHealth(
                args.Entity!,
                args.HealthModelName!,
                args.ResourceGroup!,
                args.Subscription!,
                args.AuthMethod,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = ResponseResult.Create<JsonNode>(result, JsonSourceGenerationContext.Default.JsonNode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An exception occurred getting health for entity: {Entity} in healthModel: {HealthModelName}, resourceGroup: {ResourceGroup}, subscription: {Subscription}, authMethod: {AuthMethod}"
                + ", tenant: {Tenant}.",
                args.Entity,
                args.HealthModelName,
                args.ResourceGroup,
                args.Subscription,
                args.AuthMethod,
                args.Tenant);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        KeyNotFoundException => $"Entity or health model not found. Please check the entity ID, health model name, and resource group.",
        ArgumentException argEx => $"Invalid argument: {argEx.Message}",
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        KeyNotFoundException => 404,
        ArgumentException => 400,
        _ => base.GetStatusCode(ex)
    };
}
