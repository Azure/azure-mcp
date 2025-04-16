// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using System.CommandLine;
using System.CommandLine.Parsing;

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
        response.Message = GetErrorMessage(ex);
        response.Results = new
        {
            ex.Message,
            ex.StackTrace,
            Type = ex.GetType().Name
        };
    }

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