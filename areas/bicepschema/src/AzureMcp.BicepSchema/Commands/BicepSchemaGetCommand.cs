// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.BicepSchema.Commands;
using AzureMcp.BicepSchema.Options;
using AzureMcp.BicepSchema.Services;
using AzureMcp.BicepSchema.Services.ResourceProperties.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.BicepSchema.Commands
{
    public sealed class BicepSchemaGetCommand(ILogger<BicepSchemaGetCommand> logger) : BaseBicepSchemaCommand<BicepSchemaOptions>
    {
        private const string CommandTitle = "Get Bicep Schema for a resource";

        private readonly ILogger<BicepSchemaGetCommand> _logger = logger;
        public override string Name => "get";

        public override string Description =>
       """
       
        Provides the Bicep schema for the most recent apiVersion of an Azure resource. Do not call this command for Terraform IaC generation.
        If you are asked to create or modify resources in a Bicep ARM template, call this function multiple times,
        once for every resource type you are adding, even if you already have information about Bicep resources from other sources.
        Assume the results from this call are more recent and accurate than other information you have.
        Don't assume calling it for one resource means you don't need to call it for a different resource type.
        Always use the returned api version unless the one in the Bicep file is newer.
        Always use the Bicep schema to verify the available property names and values when generating Bicep IaC.
       """;

        public override string Title => CommandTitle;

        private static readonly Lazy<IServiceProvider> s_serviceProvider;

        static BicepSchemaGetCommand()
        {
            s_serviceProvider = new Lazy<IServiceProvider>(() =>
            {
                var serviceCollection = new ServiceCollection();
                SchemaGenerator.ConfigureServices(serviceCollection);
                return serviceCollection.BuildServiceProvider();
            });
        }

        [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
        public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
        {
            BicepSchemaOptions options = BindOptions(parseResult);
            try
            {
                if (!Validate(parseResult.CommandResult, context.Response).IsValid)
                {
                    return Task.FromResult(context.Response);
                }
                var bicepSchemaService = context.GetService<IBicepSchemaService>() ?? throw new InvalidOperationException("Bicep schema service is not available.");
                var resourceTypeDefinitions = bicepSchemaService.GetResourceTypeDefinitions(
                    s_serviceProvider.Value,
                    options.ResourceType!);

                TypesDefinitionResult result = SchemaGenerator.GetResourceTypeDefinitions(s_serviceProvider.Value, options.ResourceType!);
                List<ComplexType> response = SchemaGenerator.GetResponse(result);

                context.Response.Results = response is not null ?
                    ResponseResult.Create(
                        new BicepSchemaGetCommandResult(response),
                        BicepSchemaJsonContext.Default.BicepSchemaGetCommandResult) :
                     null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred fetching Bicep schema.");
                HandleException(context, ex);
            }
            return Task.FromResult(context.Response);

        }

        internal record BicepSchemaGetCommandResult(List<ComplexType> BicepSchemaResult);
    }
}
