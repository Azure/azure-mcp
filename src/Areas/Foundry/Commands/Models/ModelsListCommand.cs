// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Foundry.Models;
using AzureMcp.Areas.Foundry.Options.Models;
using AzureMcp.Areas.Foundry.Services;
using AzureMcp.Commands;

namespace AzureMcp.Areas.Foundry.Commands.Models;

public sealed class ModelsListCommand : GlobalCommand<ModelsListOptions>
{
    private const string CommandTitle = "List Models from Model Catalog";

    public override string Name => "list";

    public override string Description =>
        """
        Retrieves a list of supported models from the Azure AI Foundry catalog.
        This function is useful when a user requests a list of available Foundry models or Foundry Labs projects.
        It fetches models based on optional filters like whether the model supports free playground usage,
        the publisher name, and the license type. The function will return the list of models with useful fields.
        Usage:
            Use this function when users inquire about available models from the Azure AI Foundry catalog.
            It can also be used when filtering models by free playground usage, publisher name, or license type.
            If user didn't specify free playground or ask for models that support GitHub token, always explain that by default it will show the all the models but some of them would support free playground.
            Explain to the user that if they want to find models suitable for prototyping and free to use with support for free playground, they can look for models that supports free playground, or look for models that they can use with GitHub token.
        """;

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var service = context.GetService<IFoundryService>();
            var models = await service.ListModels(
                options.SearchForFreePlayground ?? false,
                options.PublisherName ?? "",
                options.LicenseName ?? "",
                options.ModelName ?? "",
                3,
                options.RetryPolicy);

            context.Response.Results = models?.Count > 0 ?
                ResponseResult.Create(
                    new ModelsListCommandResult(models),
                    FoundryJsonContext.Default.ModelsListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ModelsListCommandResult(IEnumerable<ModelInformation> Models);
}
