using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using ModelContextProtocol.Server;
using AzureMcp.Arguments.Foundry.Models;
using AzureMcp.Models.Argument;
using AzureMcp.Arguments.Cosmos;

namespace AzureMcp.Commands.Foundry.Models;

public sealed class ModelGuidanceCommand : GlobalCommand<ModelGuidanceArguments>
{
    private readonly Option<string> _inferanceNameOption = ArgumentDefinitions.Foundry.InferenceModelName.ToOption();
    protected override string GetCommandName() => "guidance";

    protected override string GetCommandDescription() =>
        $"""
        Detailed usage guidance (scripts, docs, etc) on how to implement a particular project from GitHub Models.
        Use this tool to get the implementation details of a project.
        Do not assume you know how to implement a project just because you know the project name.
        
        Required arguments:
        {ArgumentDefinitions.Foundry.InferenceModelNameText}.
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_inferanceNameOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateInferenceModelNameArgument());
    }

    protected override ModelGuidanceArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.InferenceModelName = parseResult.GetValueForOption(_inferanceNameOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var service = context.GetService<IFoundryService>();
            string modelGuidance = await service.GetModelGuidance(
                args.InferenceModelName,
                args.RetryPolicy);

            context.Response.Results = modelGuidance;
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    private static ArgumentBuilder<ModelGuidanceArguments> CreateInferenceModelNameArgument() =>
        ArgumentBuilder<ModelGuidanceArguments>
            .Create(ArgumentDefinitions.Foundry.InferenceModelName.Name, ArgumentDefinitions.Foundry.InferenceModelName.Description)
            .WithIsRequired(ArgumentDefinitions.Cosmos.Query.Required);
}