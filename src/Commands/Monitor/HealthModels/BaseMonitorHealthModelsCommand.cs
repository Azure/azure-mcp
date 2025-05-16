// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments;
using AzureMcp.Arguments.Monitor.HealthModels.Entity;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Monitor.HealthModels;

public abstract class BaseMonitorHealthModelsCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : SubscriptionCommand<TArgs>
    where TArgs : SubscriptionArguments, new()
{
    protected readonly Option<string> _entityOption = ArgumentDefinitions.Monitor.Health.Entity.ToOption();
    protected readonly Option<string> _healthModelOption = ArgumentDefinitions.Monitor.Health.HealthModel.ToOption();

    protected BaseMonitorHealthModelsCommand() : base()
    {
    }

    protected ArgumentBuilder<EntityGetHealthArguments> CreateEntityArgument()
    {
        return ArgumentBuilder<EntityGetHealthArguments>
            .Create(ArgumentDefinitions.Monitor.Health.Entity.Name, ArgumentDefinitions.Monitor.Health.Entity.Description)
            .WithValueAccessor(args => args.Entity ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Monitor.Health.Entity.Required);
    }

    protected ArgumentBuilder<EntityGetHealthArguments> CreateHealthModelArgument()
    {
        return ArgumentBuilder<EntityGetHealthArguments>
            .Create(ArgumentDefinitions.Monitor.HealthModelName, ArgumentDefinitions.Monitor.Health.HealthModel.Description)
            .WithValueAccessor(args => args.HealthModelName ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Monitor.Health.HealthModel.Required);
    }
}
