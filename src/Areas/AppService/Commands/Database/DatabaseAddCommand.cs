// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AppService.Models;
using AzureMcp.Areas.AppService.Options;
using AzureMcp.Areas.AppService.Options.Database;
using AzureMcp.Areas.AppService.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AppService.Commands.Database;

public sealed class DatabaseAddCommand : BaseAppServiceCommand<DatabaseAddOptions>
{
    private const string CommandTitle = "Add Database to App Service";
    private readonly ILogger<DatabaseAddCommand> _logger;

    // Define options from AppServiceOptionDefinitions
    private readonly Option<string> _databaseTypeOption = AppServiceOptionDefinitions.DatabaseType;
    private readonly Option<string> _databaseServerOption = AppServiceOptionDefinitions.DatabaseServer;
    private readonly Option<string> _databaseNameOption = AppServiceOptionDefinitions.DatabaseName;
    private readonly Option<string> _connectionStringOption = AppServiceOptionDefinitions.ConnectionString;

    public DatabaseAddCommand(ILogger<DatabaseAddCommand> logger)
    {
        _logger = logger;
    }

    public override string Name => "add";

    public override string Description =>
        """
        Adds a database connection to an Azure App Service application.
        Creates or updates the database connection string in the App Service configuration.
        Returns the connection string details and configuration status.
          Required options:
        - app-name: The name of the App Service application
        - resource-group: The resource group containing the App Service
        - database-type: The type of database (SqlServer, MySql, PostgreSql, CosmosDb)
        - database-server: The database server name or endpoint
        - database-name: The name of the database
          Optional options:
        - connection-string: Custom connection string (will be generated if not provided)
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_databaseTypeOption);
        command.AddOption(_databaseServerOption);
        command.AddOption(_databaseNameOption);
        command.AddOption(_connectionStringOption);
    }

    protected override DatabaseAddOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.DatabaseType = parseResult.GetValueForOption(_databaseTypeOption);
        options.DatabaseServer = parseResult.GetValueForOption(_databaseServerOption);
        options.DatabaseName = parseResult.GetValueForOption(_databaseNameOption);
        options.ConnectionString = parseResult.GetValueForOption(_connectionStringOption);
        return options;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = false,
        Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            // Required validation step
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            // Get the appropriate service from DI
            var service = context.GetService<IAppServiceService>();

            // Call service operation with required parameters
            var result = await service.AddDatabaseAsync(
                options.AppName!,
                options.ResourceGroup!,
                options.DatabaseType!,
                options.DatabaseServer!,
                options.DatabaseName!,
                options.ConnectionString!,
                options.Subscription!,
                options.RetryPolicy!);

            // Set results
            context.Response.Results = ResponseResult.Create(
                new DatabaseAddCommandResult(result),
                AppServiceJsonContext.Default.DatabaseAddCommandResult);
        }
        catch (Exception ex)
        {
            // Log error with all relevant context
            _logger.LogError(ex,
                "Error adding database to App Service. AppName: {AppName}, DatabaseType: {DatabaseType}, DatabaseServer: {DatabaseServer}, DatabaseName: {DatabaseName}, Options: {@Options}",
                options.AppName, options.DatabaseType, options.DatabaseServer, options.DatabaseName, options);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    // Implementation-specific error handling
    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        UnauthorizedAccessException => "Access denied. Verify you have Contributor permissions on the App Service.",
        ArgumentException argEx => $"Invalid parameter: {argEx.Message}",
        InvalidOperationException => "The App Service is not in a valid state for this operation.",
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        UnauthorizedAccessException => 403,
        ArgumentException => 400,
        InvalidOperationException => 409,
        _ => base.GetStatusCode(ex)
    };

    // Strongly-typed result record
    public record DatabaseAddCommandResult(DatabaseConnectionInfo DatabaseConnection);
}
