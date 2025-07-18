// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


using System.Diagnostics;
using static AzureMcp.Services.Telemetry.TelemetryConstants;

namespace AzureMcp.Commands;

public abstract class BaseCommand : IBaseCommand
{
    private readonly Command _command;

    protected BaseCommand()
    {
        _command = new Command(Name, Description);
        RegisterOptions(_command);
    }

    public Command GetCommand() => _command;

    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract string Title { get; }

    protected virtual void RegisterOptions(Command command)
    {
    }

    public abstract Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult);

    protected virtual void HandleException(CommandContext context, Exception ex)
    {
        context.Activity?.SetStatus(ActivityStatusCode.Error)?.AddTag(TagName.ErrorDetails, ex.Message);

        var response = context.Response;
        var result = new ExceptionResult(
            Message: ex.Message,
            StackTrace: ex.StackTrace,
            Type: ex.GetType().Name);

        response.Status = GetStatusCode(ex);
        response.Message = GetErrorMessage(ex) + ". To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";
        response.Results = ResponseResult.Create(result, JsonSourceGenerationContext.Default.ExceptionResult);
    }

    internal record ExceptionResult(
        string Message,
        string? StackTrace,
        string Type);

    protected virtual string GetErrorMessage(Exception ex) => ex.Message;

    protected virtual int GetStatusCode(Exception ex) => 500;

    public virtual ValidationResult Validate(CommandResult commandResult, CommandResponse? commandResponse = null)
    {
        var result = new ValidationResult { IsValid = true };

        var missingOptions = commandResult.Command.Options
            .Where(o => o.IsRequired && IsOptionValueMissing(commandResult.GetValueForOption(o)))
            .Select(o => $"--{o.Name}")
            .ToList();

        if (missingOptions.Count > 0 || !string.IsNullOrEmpty(commandResult.ErrorMessage))
        {
            result.IsValid = false;
            result.ErrorMessage = missingOptions.Count > 0
                ? $"Missing Required options: {string.Join(", ", missingOptions)}"
                : commandResult.ErrorMessage;

            if (commandResponse != null && !result.IsValid)
            {
                commandResponse.Status = 400;
                commandResponse.Message = result.ErrorMessage!;
            }
        }

        return result;
    }

    private static bool IsOptionValueMissing(object? value)
    {
        return value == null || (value is string str && string.IsNullOrWhiteSpace(str));
    }
}
