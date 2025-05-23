// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


namespace AzureMcp.Commands;

public abstract class BaseCommand : IBaseCommand
{
    private readonly Command? _command;

    protected BaseCommand()
    {
        _command = new Command(Name, Description);
        RegisterOptions(_command);
    }

    public Command GetCommand() => _command ?? throw new InvalidOperationException("Command not initialized");

    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract string Title { get; }

    protected virtual void RegisterOptions(Command command)
    {
        // Base implementation is empty, derived classes will add their options
    }

    public abstract Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult);

    protected virtual void HandleException(CommandResponse response, Exception ex)
    {
        // Don't clear arguments when handling exceptions
        response.Status = GetStatusCode(ex);
        response.Message = GetErrorMessage(ex) + ". To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";
        response.Results = ResponseResult.Create(new ExceptionResult(
            ex.Message,
            ex.StackTrace,
            ex.GetType().Name), JsonSourceGenerationContext.Default.ExceptionResult);
    }

    internal record ExceptionResult(
        string Message,
        string? StackTrace,
        string Type);

    protected virtual string GetErrorMessage(Exception ex) => ex.Message;

    protected virtual int GetStatusCode(Exception ex) => 500;

    public virtual ValidationResult Validate(CommandResult commandResult)
    {
        var result = new ValidationResult();

        var missingParameters = commandResult.Command.Options
            .Where(o => o.IsRequired && commandResult.GetValueForOption(o) == null)
            .Select(o => $"--{o.Name}")
            .ToList();

        if (missingParameters.Count > 0)
        {
            result.IsValid = false;
            result.ErrorMessage = $"Missing required arguments: {string.Join(", ", missingParameters)}";
            return result;
        }

        if (!string.IsNullOrEmpty(commandResult.ErrorMessage))
        {
            result.IsValid = false;
            result.ErrorMessage = commandResult.ErrorMessage;
            return result;
        }

        result.IsValid = true;
        return result;
    }
}
