// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;


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

    protected virtual void HandleException(CommandResponse response, Exception ex)
    {
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

    /// <summary>
    /// Creates a ResponseResult from a collection, always returning a result with an empty collection if the input is null or empty.
    /// This ensures consistent JSON output that always includes a "results" field.
    /// </summary>
    /// <typeparam name="TItem">The type of items in the collection</typeparam>
    /// <typeparam name="TResult">The type of the result object</typeparam>
    /// <param name="collection">The collection that may be null or empty</param>
    /// <param name="resultFactory">A function that creates the result object from a non-null collection</param>
    /// <param name="jsonTypeInfo">The JsonTypeInfo for serialization</param>
    /// <returns>A ResponseResult that always contains a result object</returns>
    protected static ResponseResult CreateListResult<TItem, TResult>(
        IEnumerable<TItem>? collection,
        Func<List<TItem>, TResult> resultFactory,
        JsonTypeInfo<TResult> jsonTypeInfo)
    {
        var safeCollection = collection?.ToList() ?? [];
        var result = resultFactory(safeCollection);
        return ResponseResult.Create(result, jsonTypeInfo);
    }

    public virtual ValidationResult Validate(CommandResult commandResult, CommandResponse? commandResponse = null)
    {
        var result = new ValidationResult { IsValid = true };

        var missingOptions = commandResult.Command.Options
            .Where(o => o.IsRequired && commandResult.GetValueForOption(o) == null)
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
}
