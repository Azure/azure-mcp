// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Support.Options;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Support.Commands;

public abstract class BaseSupportCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>(ILogger<BaseSupportCommand<TOptions>> logger)
    : SubscriptionCommand<TOptions> where TOptions : BaseSupportOptions, new()
{
    protected readonly ILogger<BaseSupportCommand<TOptions>> _logger = logger;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        
        command.AddValidator(result =>
        {
            var validationResult = Validate(result);
            if (!validationResult.IsValid)
            {
                result.ErrorMessage = validationResult.ErrorMessage;
                return;
            }
        });
    }

    public override ValidationResult Validate(CommandResult parseResult, CommandResponse? commandResponse = null)
    {
        var validationResult = new ValidationResult { IsValid = true };
        
        var subscriptionResult = parseResult.FindResultFor(_subscriptionOption);
        if (subscriptionResult == null || subscriptionResult.IsImplicit || 
            subscriptionResult.Tokens.Count == 0 || 
            string.IsNullOrWhiteSpace(subscriptionResult.Tokens[0].Value))
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Subscription is required for Azure Support operations. Please specify the Azure subscription ID or name using the --subscription parameter. Example: --subscription \"my-subscription-name\" or --subscription \"12345678-1234-1234-1234-123456789012\"";

            if (commandResponse != null)
            {
                commandResponse.Status = 400;
                commandResponse.Message = validationResult.ErrorMessage;
            }
        }

        if (validationResult.IsValid)
            return base.Validate(parseResult, commandResponse);

        return validationResult;
    }
}
