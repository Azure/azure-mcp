// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.Core;
using Azure.Identity;
using AzureMcp.Arguments;
using AzureMcp.Models;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace AzureMcp.Commands;

internal static class TrimAnnotations
{
    public const DynamicallyAccessedMemberTypes CommandAnnotations =
        DynamicallyAccessedMemberTypes.PublicProperties
        | DynamicallyAccessedMemberTypes.NonPublicProperties;
}

public abstract class GlobalCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs> : BaseCommand
    where TArgs : GlobalArguments, new()
{
    protected readonly Option<string> _tenantOption = ArgumentDefinitions.Common.Tenant.ToOption();
    protected readonly Option<AuthMethod> _authMethodOption = ArgumentDefinitions.Common.AuthMethod.ToOption();
    protected readonly Option<string> _resourceGroupOption = ArgumentDefinitions.Common.ResourceGroup.ToOption();
    protected readonly Option<int> _retryMaxRetries = ArgumentDefinitions.RetryPolicy.MaxRetries.ToOption();
    protected readonly Option<double> _retryDelayOption = ArgumentDefinitions.RetryPolicy.Delay.ToOption();
    protected readonly Option<double> _retryMaxDelayOption = ArgumentDefinitions.RetryPolicy.MaxDelay.ToOption();
    protected readonly Option<RetryMode> _retryModeOption = ArgumentDefinitions.RetryPolicy.Mode.ToOption();
    protected readonly Option<double> _retryNetworkTimeoutOption = ArgumentDefinitions.RetryPolicy.NetworkTimeout.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);

        // Add global options
        command.AddOption(_tenantOption);
        command.AddOption(_authMethodOption);
        command.AddOption(_retryDelayOption);
        command.AddOption(_retryMaxDelayOption);
        command.AddOption(_retryMaxRetries);
        command.AddOption(_retryModeOption);
        command.AddOption(_retryNetworkTimeoutOption);
    }

    protected override void RegisterArguments()
    {
        // Must explicitly call base first
        base.RegisterArguments();

        // Register global arguments
        AddArgument(CreateAuthMethodArgument());
        AddArgument(CreateTenantArgument());
        foreach (var argument in CreateRetryArguments())
        {
            AddArgument(argument);
        }
    }

    protected ArgumentBuilder<TArgs>[] CreateRetryArguments()
    {
        return
        [
            ArgumentBuilder<TArgs>
                .Create(ArgumentDefinitions.RetryPolicy.MaxRetries.Name, ArgumentDefinitions.RetryPolicy.MaxRetries.Description)
                .WithValueAccessor(args => args.RetryPolicy == null ? string.Empty : args.RetryPolicy.MaxRetries.ToString())
                .WithDefaultValue(ArgumentDefinitions.RetryPolicy.MaxRetries.DefaultValue.ToString())
                .WithIsRequired(false),

            ArgumentBuilder<TArgs>
                .Create(ArgumentDefinitions.RetryPolicy.Delay.Name, ArgumentDefinitions.RetryPolicy.Delay.Description)
                .WithValueAccessor(args => args.RetryPolicy == null ? string.Empty : args.RetryPolicy.DelaySeconds.ToString())
                .WithDefaultValue(ArgumentDefinitions.RetryPolicy.Delay.DefaultValue.ToString())
                .WithIsRequired(false),

            ArgumentBuilder<TArgs>
                .Create(ArgumentDefinitions.RetryPolicy.MaxDelay.Name, ArgumentDefinitions.RetryPolicy.MaxDelay.Description)
                .WithValueAccessor(args => args.RetryPolicy == null ? string.Empty : args.RetryPolicy.MaxDelaySeconds.ToString())
                .WithDefaultValue(ArgumentDefinitions.RetryPolicy.MaxDelay.DefaultValue.ToString())
                .WithIsRequired(false),

            ArgumentBuilder<TArgs>
                .Create(ArgumentDefinitions.RetryPolicy.Mode.Name, ArgumentDefinitions.RetryPolicy.Mode.Description)
                .WithValueAccessor(args => args.RetryPolicy == null ? string.Empty : args.RetryPolicy.Mode.ToString())
                .WithDefaultValue(ArgumentDefinitions.RetryPolicy.Mode.DefaultValue.ToString())
                .WithIsRequired(false),

            ArgumentBuilder<TArgs>
                .Create(ArgumentDefinitions.RetryPolicy.NetworkTimeout.Name, ArgumentDefinitions.RetryPolicy.NetworkTimeout.Description)
                .WithValueAccessor(args => args.RetryPolicy == null ? string.Empty : args.RetryPolicy.NetworkTimeoutSeconds.ToString())
                .WithDefaultValue(ArgumentDefinitions.RetryPolicy.NetworkTimeout.DefaultValue.ToString())
                .WithIsRequired(false)
        ];
    }

    // Helper methods to create common arguments
    protected ArgumentBuilder<TArgs> CreateAuthMethodArgument()
    {
        return ArgumentBuilder<TArgs>
            .Create(ArgumentDefinitions.Common.AuthMethod.Name, ArgumentDefinitions.Common.AuthMethod.Description)
            .WithValueAccessor(args => args.AuthMethod?.ToString() ?? string.Empty)
            .WithDefaultValue(AuthMethodArgument.GetDefaultAuthMethod().ToString())
            .WithIsRequired(false);
    }

    protected ArgumentBuilder<TArgs> CreateTenantArgument()
    {
        return ArgumentBuilder<TArgs>
            .Create(ArgumentDefinitions.Common.Tenant.Name, ArgumentDefinitions.Common.Tenant.Description)
            .WithValueAccessor(args => args.Tenant ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Common.Tenant.Required);
    }

    private static async Task<List<ArgumentOption>> GetTenantOptions(CommandContext context)
    {
        var tenantService = context.GetService<ITenantService>();
        return await tenantService.GetTenants();
    }

    // Helper method to get auth method options
    protected virtual async Task<List<ArgumentOption>> GetAuthMethodOptions(CommandContext context)
    {
        // Use the helper method from AuthMethodArgument
        return AuthMethodArgument.GetAuthMethodOptions();
    }

    protected ArgumentBuilder<TArgs> CreateResourceGroupArgument() =>
        ArgumentBuilder<TArgs>
            .Create(ArgumentDefinitions.Common.ResourceGroup.Name, ArgumentDefinitions.Common.ResourceGroup.Description)
            .WithValueAccessor(args => (args as SubscriptionArguments)?.ResourceGroup ?? string.Empty)
            .WithIsRequired(true);

    protected async Task<List<ArgumentOption>> GetResourceGroupOptions(CommandContext context, string subscription, string tenant = "")
    {
        if (string.IsNullOrEmpty(subscription)) return [];

        var resourceGroupService = context.GetService<IResourceGroupService>();
        var resourceGroup = await resourceGroupService.GetResourceGroups(subscription, tenant);

        return resourceGroup?.Select(rg => new ArgumentOption { Name = rg.Name, Id = rg.Id }).ToList() ?? [];
    }

    // Helper to get the command path for examples
    protected virtual string GetCommandPath()
    {
        // Get the command type name without the "Command" suffix
        string commandName = GetType().Name.Replace("Command", "");

        // Get the namespace to determine the service name
        string namespaceName = GetType().Namespace ?? "";
        string serviceName = "";

        // Extract service name from namespace (e.g., AzureMcp.Commands.Cosmos -> cosmos)
        if (!string.IsNullOrEmpty(namespaceName) && namespaceName.Contains(".Commands."))
        {
            string[] parts = namespaceName.Split(".Commands.");
            if (parts.Length > 1)
            {
                string[] subParts = parts[1].Split('.');
                if (subParts.Length > 0)
                {
                    serviceName = subParts[0].ToLowerInvariant();
                }
            }
        }

        // Insert spaces before capital letters in the command name
        string formattedName = string.Concat(commandName.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).Trim();

        // Convert to lowercase and replace spaces with spaces (for readability in command examples)
        string commandPath = formattedName.ToLowerInvariant().Replace(" ", " ");

        // Prepend the service name if available
        if (!string.IsNullOrEmpty(serviceName))
        {
            commandPath = serviceName + " " + commandPath;
        }

        return commandPath;
    }

    protected async Task<bool> ProcessArguments(CommandContext context, TArgs args)
    {
        var arguments = GetArguments();

        // Ensure we have arguments to process
        if (arguments == null || !arguments.Any())
        {
            return true;
        }

        // First, add all arguments to the response and apply default values if needed
        foreach (var argDef in arguments)
        {
            if (argDef is ArgumentBuilder<TArgs> typedArgDef)
            {
                // Get the current value and handle "null" string case
                string value = typedArgDef.ValueAccessor(args) ?? string.Empty;
                value = value.Equals("null", StringComparison.OrdinalIgnoreCase) ? string.Empty : value;

                // Special handling for subscription when it's "default"
                if (typedArgDef.Name.Equals("subscription", StringComparison.OrdinalIgnoreCase) &&
                    value.Equals("default", StringComparison.OrdinalIgnoreCase))
                {
                    value = string.Empty;
                    // Update the args object if it's a subscription-based argument type
                    if (args is SubscriptionArguments baseArgs)
                    {
                        baseArgs.Subscription = string.Empty;
                    }
                }

                // If the value is empty but there's a default value, use the default value
                if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(typedArgDef.DefaultValue))
                {
                    // Try to set the default value on the args object using reflection
                    try
                    {
                        var prop = typeof(TArgs).GetProperty(typedArgDef.Name,
                            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance |
                            System.Reflection.BindingFlags.IgnoreCase);

                        if (prop != null && prop.CanWrite)
                        {
                            prop.SetValue(args, typedArgDef.DefaultValue);
                            value = typedArgDef.DefaultValue;
                        }
                    }
                    catch (Exception)
                    {
                        // Silently handle reflection errors
                    }
                }
            }
        }

        // Then, process required arguments that are missing values
        bool allRequiredArgumentsProvided = true;
        var missingArgs = new List<string>();

        foreach (var argDef in arguments)
        {
            if (argDef is ArgumentBuilder<TArgs> typedArgDef && typedArgDef.Required)
            {
                // Get the current value
                string value = typedArgDef.ValueAccessor(args) ?? string.Empty;

                // If the value is missing and this is a required argument
                if (string.IsNullOrEmpty(value))
                {
                    // Check if there's a default value
                    if (!string.IsNullOrEmpty(typedArgDef.DefaultValue))
                    {
                        // We consider this argument as provided since it has a default value
                        continue;
                    }

                    // Add to missing arguments list
                    missingArgs.Add(typedArgDef.Name);
                    allRequiredArgumentsProvided = false;
                }
            }
        }

        if (!allRequiredArgumentsProvided)
        {
            context.Response.Status = 400;
            context.Response.Message = $"Missing required arguments: {string.Join(", ", missingArgs)}";
        }

        return allRequiredArgumentsProvided;
    }

    protected virtual TArgs BindArguments(ParseResult parseResult)
    {
        var args = new TArgs
        {
            Tenant = parseResult.GetValueForOption(_tenantOption),
            AuthMethod = parseResult.GetValueForOption(_authMethodOption)
        };

        // Only create RetryPolicy if any retry options are specified
        if (parseResult.HasAnyRetryOptions())
        {
            args.RetryPolicy = new RetryPolicyArguments
            {
                MaxRetries = parseResult.GetValueForOption(_retryMaxRetries),
                DelaySeconds = parseResult.GetValueForOption(_retryDelayOption),
                MaxDelaySeconds = parseResult.GetValueForOption(_retryMaxDelayOption),
                Mode = parseResult.GetValueForOption(_retryModeOption),
                NetworkTimeoutSeconds = parseResult.GetValueForOption(_retryNetworkTimeoutOption)
            };
        }

        return args;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        AuthenticationFailedException authEx =>
            $"Authentication failed. Please run 'az login' to sign in to Azure. Details: {authEx.Message}",
        RequestFailedException rfEx => rfEx.Message,
        HttpRequestException httpEx =>
            $"Service unavailable or network connectivity issues. Details: {httpEx.Message}",
        _ => ex.Message  // Just return the actual exception message
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        AuthenticationFailedException => 401,
        RequestFailedException rfEx => rfEx.Status,
        HttpRequestException => 503,
        _ => 500
    };
}