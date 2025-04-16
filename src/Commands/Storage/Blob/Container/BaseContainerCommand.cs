// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Storage.Blob;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;

namespace AzureMcp.Commands.Storage.Blob.Container;

public abstract class BaseContainerCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : BaseStorageCommand<TArgs> where TArgs : BaseContainerArguments, new()
{
    protected readonly Option<string> _containerOption = ArgumentDefinitions.Storage.Container.ToOption();

    protected BaseContainerCommand()
    {
    }

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_containerOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateContainerArgument());
    }

    protected override TArgs BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Container = parseResult.GetValueForOption(_containerOption);
        return args;
    }

    protected ArgumentBuilder<TArgs> CreateContainerArgument()
    {
        return ArgumentBuilder<TArgs>
            .Create(ArgumentDefinitions.Storage.Container.Name, ArgumentDefinitions.Storage.Container.Description)
            .WithValueAccessor(args => args.Container ?? string.Empty)
            .WithSuggestedValuesLoader(async (context, args) =>
            {
                if (string.IsNullOrEmpty(args.Account) || string.IsNullOrEmpty(args.Subscription))
                {
                    return [];
                }

                var storageService = context.GetService<IStorageService>();
                var containers = await storageService.ListContainers(args.Account, args.Subscription);

                return containers?.Select(c => new ArgumentOption { Name = c, Id = c }).ToList() ?? [];
            })
            .WithIsRequired(ArgumentDefinitions.Storage.Container.Required);
    }
}