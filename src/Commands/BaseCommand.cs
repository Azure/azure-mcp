using Azure;
using Azure.Core;
using Azure.Identity;
using AzureMCP.Arguments;
using AzureMCP.Extensions;
using AzureMCP.Models;
using AzureMCP.Models.Argument;
using AzureMCP.Models.Command;
using AzureMCP.Services.Interfaces;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace AzureMCP.Commands;

public abstract class BaseCommand<TArgs> : ICommand where TArgs : BaseArguments, new()
{
    protected readonly Option<string> _tenantOption;
    protected readonly Option<string> _subscriptionOption;
    protected readonly Option<AuthMethod> _authMethodOption;
    protected readonly Option<string> _resourceGroupOption;

    // Add retry policy options
    protected readonly Option<int> _retryMaxRetries;
    protected readonly Option<double> _retryDelayOption;
    protected readonly Option<double> _retryMaxDelayOption;
    protected readonly Option<RetryMode> _retryModeOption;
    protected readonly Option<double> _retryNetworkTimeoutOption;

    // Argument chain infrastructure
    protected List<ArgumentDefinition<string>> _argumentChain = [];

    protected BaseCommand()
    {
        // Initialize options
        _tenantOption = ArgumentDefinitions.Common.TenantId.ToOption();
        _subscriptionOption = ArgumentDefinitions.Common.Subscription.ToOption();
        _authMethodOption = ArgumentDefinitions.Common.AuthMethod.ToOption();
        _resourceGroupOption = ArgumentDefinitions.Common.ResourceGroup.ToOption();

        // Initialize retry policy options
        _retryDelayOption = ArgumentDefinitions.RetryPolicy.Delay.ToOption();
        _retryMaxDelayOption = ArgumentDefinitions.RetryPolicy.MaxDelay.ToOption();
        _retryMaxRetries = ArgumentDefinitions.RetryPolicy.MaxRetries.ToOption();
        _retryModeOption = ArgumentDefinitions.RetryPolicy.Mode.ToOption();
        _retryNetworkTimeoutOption = ArgumentDefinitions.RetryPolicy.NetworkTimeout.ToOption();
    }

    // Helper methods to create common arguments
    protected ArgumentChain<TArgs> CreateAuthMethodArgument()
    {
        return ArgumentChain<TArgs>
            .Create(ArgumentDefinitions.Common.AuthMethod.Name, ArgumentDefinitions.Common.AuthMethod.Description)
            .WithCommandExample(ArgumentDefinitions.GetCommandExample(GetCommandPath(), ArgumentDefinitions.Common.AuthMethod))
            .WithValueAccessor(args => args.AuthMethod?.ToString() ?? string.Empty)
            .WithValueLoader(async (context, args) => await GetAuthMethodOptions(context))
            .WithDefaultValue(AuthMethodArguments.GetDefaultAuthMethod().ToString())
            .WithIsRequired(false);
    }

    protected ArgumentChain<TArgs> CreateTenantIdArgument()
    {
        return ArgumentChain<TArgs>
            .Create(ArgumentDefinitions.Common.TenantId.Name, ArgumentDefinitions.Common.TenantId.Description)
            .WithCommandExample(ArgumentDefinitions.GetCommandExample(GetCommandPath(), ArgumentDefinitions.Common.TenantId))
            .WithValueAccessor(args => args.TenantId ?? string.Empty)
            .WithIsRequired(false);
    }

    protected ArgumentChain<TArgs>? CreateResourceGroupArgument()
    {
        if (!typeof(BaseArgumentsWithSubscription).IsAssignableFrom(typeof(TArgs)))
        {
            return null;
        }

        return ArgumentChain<TArgs>
            .Create(ArgumentDefinitions.Common.ResourceGroup.Name, ArgumentDefinitions.Common.ResourceGroup.Description)
            .WithCommandExample(ArgumentDefinitions.GetCommandExample(GetCommandPath(), ArgumentDefinitions.Common.ResourceGroup))
            .WithValueAccessor(args => (args as BaseArgumentsWithSubscription)?.ResourceGroup ?? string.Empty)
            .WithValueLoader(async (context, args) =>
            {
                var subArgs = args as BaseArgumentsWithSubscription;
                if (string.IsNullOrEmpty(subArgs?.Subscription))
                {
                    return [];
                }
                return await GetResourceGroupOptions(context, subArgs.Subscription);
            })
            .WithIsRequired(true);
    }

    // Helper method to get auth method options
    protected virtual async Task<List<ArgumentOption>> GetAuthMethodOptions(CommandContext context)
    {
        // Use the helper method from AuthMethodArguments
        return await Task.FromResult(AuthMethodArguments.GetAuthMethodOptions());
    }

    // Helper method to get subscription options
    protected virtual async Task<List<ArgumentOption>> GetSubscriptionOptions(CommandContext context)
    {
        try
        {
            var subscriptionService = context.GetService<ISubscriptionService>();
            var subscriptions = await subscriptionService.GetSubscriptions();
            return subscriptions ?? [];
        }
        catch
        {
            // Silently handle subscription fetch failures
            return [];
        }
    }

    protected async Task<List<ArgumentOption>> GetResourceGroupOptions(CommandContext context, string subscriptionId)
    {
        if (string.IsNullOrEmpty(subscriptionId)) return [];

        var resourceGroupService = context.GetService<IResourceGroupService>();
        var resourceGroup = await resourceGroupService.GetResourceGroups(subscriptionId);

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

        // Extract service name from namespace (e.g., AzureMCP.Commands.Cosmos -> cosmos)
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

    // Process the argument chain
    protected async Task<bool> ProcessArgumentChain(CommandContext context, TArgs args)
    {
        // Ensure we have arguments to process
        if (_argumentChain == null || _argumentChain.Count == 0)
        {
            return true;
        }

        // First, add all arguments to the response and apply default values if needed
        foreach (var argDef in _argumentChain)
        {
            if (argDef is ArgumentChain<TArgs> typedArgDef)
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
                    if (args is BaseArgumentsWithSubscription baseArgs)
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

                // Add the argument info to the response
                // Only include default value if no value is provided
                string? defaultToUse = string.IsNullOrEmpty(value) ? typedArgDef.DefaultValue : null;

                AddArgumentInfo(context, typedArgDef.Name, value,
                    typedArgDef.Description, typedArgDef.Command, defaultToUse, required: typedArgDef.Required);
            }
        }

        // Then, process required arguments that are missing values
        bool allRequiredArgumentsProvided = true;
        var missingArgs = new List<string>();

        foreach (var argDef in _argumentChain)
        {
            if (argDef is ArgumentChain<TArgs> typedArgDef && typedArgDef.Required)
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

                    // Find the argument in the response
                    var argInfo = context.Response.Arguments?.FirstOrDefault(a => a.Name == typedArgDef.Name);
                    if (argInfo != null && typedArgDef.ValueLoader != null)
                    {
                        // Load suggested values
                        var suggestedValues = await typedArgDef.ValueLoader(context, args);
                        if (suggestedValues?.Any() == true)
                        {
                            argInfo.Values = suggestedValues;
                        }
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

    protected RetryPolicyArguments GetRetryPolicyArguments(System.CommandLine.Parsing.ParseResult parseResult)
    {
        return new RetryPolicyArguments
        {
            DelaySeconds = parseResult.GetValueForOption(_retryDelayOption),
            MaxDelaySeconds = parseResult.GetValueForOption(_retryMaxDelayOption),
            MaxRetries = parseResult.GetValueForOption(_retryMaxRetries),
            Mode = parseResult.GetValueForOption(_retryModeOption),
            NetworkTimeoutSeconds = parseResult.GetValueForOption(_retryNetworkTimeoutOption)
        };
    }

    protected void AddRetryOptionsToCommand(Command command)
    {
        command.AddOption(_retryDelayOption);
        command.AddOption(_retryMaxDelayOption);
        command.AddOption(_retryMaxRetries);
        command.AddOption(_retryModeOption);
        command.AddOption(_retryNetworkTimeoutOption);
    }

    protected void AddCommonOptionsToCommand(Command command)
    {
        command.AddOption(_tenantOption);
        command.AddOption(_subscriptionOption);
        command.AddOption(_authMethodOption);
    }

    protected void AddBaseOptionsToCommand(Command command)
    {
        AddCommonOptionsToCommand(command);
        AddRetryOptionsToCommand(command);
    }

    public abstract Command GetCommand();

    public IEnumerable<ArgumentDefinition<string>>? GetArgumentChain() => _argumentChain?.ToList();

    public void ClearArgumentChain() => _argumentChain.Clear();

    public void AddArgumentToChain(ArgumentDefinition<string> argument) => _argumentChain.Add(argument);

    public abstract Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions);

    protected virtual TArgs BindArguments(ParseResult parseResult)
    {
        var args = new TArgs
        {
            TenantId = parseResult.GetValueForOption(_tenantOption),
            AuthMethod = parseResult.GetValueForOption(_authMethodOption)
        };

        if (args is BaseArgumentsWithSubscription baseArgs)
        {
            // Bind base arguments
            baseArgs.Subscription = parseResult.GetValueForOption(_subscriptionOption);
        }

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

    protected void AddArgumentInfo(CommandContext context, string name, string value, string description, string command, string? defaultValue = null, List<ArgumentOption>? values = null, bool required = false)
    {
        context.Response.Arguments ??= [];

        var argumentInfo = new ArgumentInfo(name, description, value, command, defaultValue, required: required);

        if (string.IsNullOrEmpty(value))
        {
            argumentInfo.Values = values;
        }

        context.Response.Arguments.Add(argumentInfo);
        context.Response.Arguments.SortArguments();
    }

    protected void HandleException(CommandResponse response, Exception ex)
    {
        response.Arguments ??= [];
        response.Status = GetStatusCode(ex);
        response.Message = GetErrorMessage(ex);
        response.Results = null;
        response.Arguments.SortArguments();
    }

    protected virtual string GetErrorMessage(Exception ex) => ex switch
    {
        AuthenticationFailedException authEx =>
            $"Authentication failed. Please run 'az login' to sign in to Azure. Details: {authEx.Message}",
        RequestFailedException rfEx => rfEx.Message,
        HttpRequestException httpEx =>
            $"Service unavailable or network connectivity issues. Details: {httpEx.Message}",
        _ => ex.Message  // Just return the actual exception message
    };

    protected virtual int GetStatusCode(Exception ex) => ex switch
    {
        AuthenticationFailedException => 401,
        RequestFailedException rfEx => rfEx.Status,
        HttpRequestException => 503,
        _ => 500
    };
}