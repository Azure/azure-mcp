// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Net;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Sql.Models;
using AzureMcp.Sql.Options;
using AzureMcp.Sql.Options.FirewallRule;
using AzureMcp.Sql.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Sql.Commands.FirewallRule;

public sealed class FirewallRuleCreateCommand(ILogger<FirewallRuleCreateCommand> logger)
    : BaseSqlCommand<FirewallRuleCreateOptions>(logger)
{
    private const string CommandTitle = "Create SQL Server Firewall Rule";

    public override string Name => "create";

    public override string Description =>
        """
        Creates a new firewall rule for a SQL server. This command allows you to specify 
        an IP address range that can access the SQL server. The rule will be created with 
        the specified name and IP range. Returns the created firewall rule object with 
        its properties.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = false };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(SqlOptionDefinitions.FirewallRuleName);
        command.AddOption(SqlOptionDefinitions.StartIp);
        command.AddOption(SqlOptionDefinitions.EndIp);
    }

    protected override FirewallRuleCreateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Name = parseResult.GetValueForOption(SqlOptionDefinitions.FirewallRuleName);
        options.StartIpAddress = parseResult.GetValueForOption(SqlOptionDefinitions.StartIp);
        options.EndIpAddress = parseResult.GetValueForOption(SqlOptionDefinitions.EndIp);
        return options;
    }

    public override ValidationResult Validate(CommandResult commandResult, CommandResponse? commandResponse = null)
    {
        var baseValidation = base.Validate(commandResult, commandResponse);
        if (!baseValidation.IsValid)
        {
            return baseValidation;
        }

        // For custom validation, we'll need the parsed options
        // This will be handled in ExecuteAsync method instead
        return new ValidationResult { IsValid = true };
    }

    private ValidationResult ValidateOptions(FirewallRuleCreateOptions options, CommandResponse? commandResponse = null)
    {
        // Validate rule name
        if (string.IsNullOrWhiteSpace(options.Name))
        {
            if (commandResponse != null)
            {
                commandResponse.Status = 400;
                commandResponse.Message = "Firewall rule name is required.";
            }
            return new ValidationResult { IsValid = false, ErrorMessage = "Firewall rule name is required." };
        }

        // Validate IP addresses
        if (!IsValidIpAddress(options.StartIpAddress))
        {
            if (commandResponse != null)
            {
                commandResponse.Status = 400;
                commandResponse.Message = $"Invalid start IP address: {options.StartIpAddress}";
            }
            return new ValidationResult { IsValid = false, ErrorMessage = $"Invalid start IP address: {options.StartIpAddress}" };
        }

        if (!IsValidIpAddress(options.EndIpAddress))
        {
            if (commandResponse != null)
            {
                commandResponse.Status = 400;
                commandResponse.Message = $"Invalid end IP address: {options.EndIpAddress}";
            }
            return new ValidationResult { IsValid = false, ErrorMessage = $"Invalid end IP address: {options.EndIpAddress}" };
        }

        // Validate IP range
        if (!IsValidIpRange(options.StartIpAddress!, options.EndIpAddress!))
        {
            if (commandResponse != null)
            {
                commandResponse.Status = 400;
                commandResponse.Message = "Start IP address must be less than or equal to end IP address.";
            }
            return new ValidationResult { IsValid = false, ErrorMessage = "Start IP address must be less than or equal to end IP address." };
        }

        return new ValidationResult { IsValid = true };
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            // Additional validation for our specific options
            var optionsValidation = ValidateOptions(options, context.Response);
            if (!optionsValidation.IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);

            var sqlService = context.GetService<ISqlService>();

            var firewallRule = await sqlService.CreateFirewallRuleAsync(
                options.Server!,
                options.Name!,
                options.StartIpAddress!,
                options.EndIpAddress!,
                options.ResourceGroup!,
                options.Subscription!,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new FirewallRuleCreateResult(firewallRule),
                SqlJsonContext.Default.FirewallRuleCreateResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error creating SQL server firewall rule. Server: {Server}, RuleName: {RuleName}, ResourceGroup: {ResourceGroup}, Options: {@Options}",
                options.Server, options.Name, options.ResourceGroup, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx when reqEx.Status == 404 =>
            "SQL server not found. Verify the server name, resource group, and that you have access.",
        Azure.RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed accessing the SQL server. Verify you have appropriate permissions. Details: {reqEx.Message}",
        Azure.RequestFailedException reqEx when reqEx.Status == 409 =>
            "A firewall rule with this name already exists. Use a different name or update the existing rule.",
        Azure.RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx => reqEx.Status,
        _ => base.GetStatusCode(ex)
    };

    private static bool IsValidIpAddress(string? ipAddress)
    {
        return !string.IsNullOrWhiteSpace(ipAddress) && IPAddress.TryParse(ipAddress, out _);
    }

    private static bool IsValidIpRange(string startIp, string endIp)
    {
        if (!IPAddress.TryParse(startIp, out var startIpAddr) ||
            !IPAddress.TryParse(endIp, out var endIpAddr))
        {
            return false;
        }

        // Convert to integers for comparison
        var startBytes = startIpAddr.GetAddressBytes();
        var endBytes = endIpAddr.GetAddressBytes();

        if (startBytes.Length != endBytes.Length)
        {
            return false;
        }

        // Compare byte by byte
        for (int i = 0; i < startBytes.Length; i++)
        {
            if (startBytes[i] < endBytes[i])
            {
                return true;
            }
            if (startBytes[i] > endBytes[i])
            {
                return false;
            }
        }

        // Equal addresses are valid (single IP range)
        return true;
    }

    internal record FirewallRuleCreateResult(SqlServerFirewallRule FirewallRule);
}
