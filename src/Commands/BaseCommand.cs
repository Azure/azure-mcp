// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;

namespace AzureMcp.Commands;

public abstract class BaseCommand : IBaseCommand
{
    protected readonly HashSet<string> _registeredArgumentNames = [];
    protected readonly List<ArgumentDefinition<string>> _arguments = [];

    private readonly Command? _command;

    protected BaseCommand()
    {
        _command = new Command(GetCommandName(), GetCommandDescription());
        RegisterOptions(_command);
        RegisterArguments();
    }

    public Command GetCommand() => _command ?? throw new InvalidOperationException("Command not initialized");

    protected abstract string GetCommandName();
    protected abstract string GetCommandDescription();

    protected virtual void RegisterOptions(Command command)
    {
        // Base implementation is empty, derived classes will add their options
    }

    protected virtual void RegisterArguments()
    {
        // Base implementation is empty, but derived classes must explicitly call base
    }

    public abstract Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions);

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

    public IEnumerable<ArgumentDefinition<string>>? GetArguments() => _arguments;

    public void ClearArguments() => _arguments.Clear();

    public virtual void AddArgument(ArgumentDefinition<string> argument)
    {
        if (argument != null && _registeredArgumentNames.Add(argument.Name))
        {
            _arguments.Add(argument);
        }
    }
}
