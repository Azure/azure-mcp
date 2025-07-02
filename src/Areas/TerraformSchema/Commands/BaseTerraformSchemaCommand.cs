// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.TerraformSchema.Options;
using AzureMcp.Commands;

namespace AzureMcp.Areas.TerraformSchema.Commands;

public abstract class BaseTerraformSchemaCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions> : GlobalCommand<TOptions>
    where TOptions : BaseTerraformSchemaOptions, new()
{
    protected readonly Option<string> _resourceTypeName = TerraformSchemaOptionDefinitions.ResourceTypeName;
    protected readonly Option<string> _providerName = TerraformSchemaOptionDefinitions.ProviderName;
    protected readonly Option<string> _userDirectoryPath = TerraformSchemaOptionDefinitions.UserDirectoryPath;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceTypeName);
        command.AddOption(_providerName);
        command.AddOption(_userDirectoryPath);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceType = parseResult.GetValueForOption(_resourceTypeName);
        options.ProviderName = parseResult.GetValueForOption(_providerName);
        options.UserDirectory = parseResult.GetValueForOption(_userDirectoryPath);
        return options;
    }
}

