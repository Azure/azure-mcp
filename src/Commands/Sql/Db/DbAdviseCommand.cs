// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Option;
using AzureMcp.Models.Sql;
using AzureMcp.Options.Sql.Database;
using AzureMcp.Services.Azure.Sql.Exceptions;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Sql.Db;

public sealed class DbAdviseCommand(ILogger<DbAdviseCommand> logger)
    : BaseDbCommand<DatabaseAdviseOptions>
{
    private const string _commandTitle = "Get SQL Database Advisor Recommendations";
    private readonly ILogger<DbAdviseCommand> _logger = logger;

    private readonly Option<string> _tableName = OptionDefinitions.Sql.Table;
    private readonly Option<int> _minImpact = OptionDefinitions.Sql.MinimumImpact;
    private readonly Option<string> _advisorType = OptionDefinitions.Sql.AdvisorType;

    public override string Name => "advise";
    public override string Title => _commandTitle; public override string Description =>
        """
        Optimize and improve performance of your Azure SQL database by getting intelligent advisor recommendations.
        This command analyzes your database and provides optimization suggestions to help improve performance, reduce costs, and enhance query efficiency.
        
        The Azure SQL Database advisor provides recommendations for:
        - Index optimization (create/drop suggestions to improve query performance)
        - Query performance tuning and optimization
        - Database parameterization for better plan reuse
        - Force last good execution plans for regression fixes
        - Performance bottleneck identification and resolution
        
        Use this command when you want to:
        - Optimize database performance
        - Improve slow running queries
        - Reduce database costs through better resource utilization
        - Get AI-powered insights for database tuning
        - Identify and fix performance regressions
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_tableName);
        command.AddOption(_minImpact);
        command.AddOption(_advisorType);
    }

    protected override DatabaseAdviseOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.TableName = parseResult.GetValueForOption(_tableName);
        options.MinimumImpact = parseResult.GetValueForOption(_minImpact);
        options.AdvisorType = parseResult.GetValueForOption(_advisorType);
        return options;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }
            var service = context.GetService<ISqlService>();
            var analysisResult = await service.GetRecommendationsAsync(
                options.Database!,
                options.ServerName!,
                options.ResourceGroup!,
                options.TableName,
                options.MinimumImpact,
                options.AdvisorType,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            // Create response that includes both recommendations and analysis metadata
            context.Response.Results = ResponseResult.Create<DbAdviseCommandResult>(
                new DbAdviseCommandResult(analysisResult),
                SqlJsonContext.Default.DbAdviseCommandResult);

            // Set appropriate message based on analysis result
            if (!analysisResult.AnalysisSuccessful)
            {
                context.Response.Status = 500;
                context.Response.Message = analysisResult.AnalysisSummary;
            }
            else if (analysisResult.HasRecommendations)
            {
                var advisorContext = !string.IsNullOrEmpty(options.AdvisorType)
                    ? $" from {options.AdvisorType} advisor"
                    : " from all supported advisors";
                context.Response.Message = $"Found {analysisResult.TotalRecommendations} recommendation(s){advisorContext} for database '{options.Database}' on server '{options.ServerName}'.";
            }
            else
            {
                var advisorContext = !string.IsNullOrEmpty(options.AdvisorType)
                    ? $" for {options.AdvisorType} advisor"
                    : "";
                context.Response.Message = $"Analysis completed{advisorContext} for database '{options.Database}' on server '{options.ServerName}'. {analysisResult.AnalysisSummary}";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting advisor recommendations. Database: {Database}, Server: {Server}, AdvisorType: {AdvisorType}, Options: {@Options}",
                options.Database, options.ServerName, options.AdvisorType, options);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        DatabaseNotFoundException => "Database not found. Verify the database exists and you have access.",
        _ => base.GetErrorMessage(ex)
    }; internal record DbAdviseCommandResult(SqlAnalysisResult Analysis) : IDbAdviseCommandResult
    {
        public SqlAnalysisResult Analysis { get; init; } = Analysis;

        public List<SqlRecommendation> Recommendations => Analysis.Recommendations;
    }
}
