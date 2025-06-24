// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.BicepSchema.Options;
using AzureMcp.Areas.BicepSchema.Services;
using AzureMcp.Areas.BicepSchema.Services.ResourceProperties.Entities;
using AzureMcp.Commands.BicepSchema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.BicepSchema.Commands
{
    public sealed class BicepSchemaGetCommand(ILogger<BicepSchemaGetCommand> logger) : BaseBicepSchemaCommand<BicepSchemaOptions>
    {
        private const string CommandTitle = "Get Bicep Schema for a resource";

        private readonly ILogger<BicepSchemaGetCommand> _logger = logger;
        public override string Name => "get";

        public override string Description =>
       """
        Provides the schema for the most recent apiVersion of an Azure resource.
        The resource-type parameter must be in the full Azure Resource Manager format '{ResourceProvider}/{ResourceType}'.
        (e.g., 'Microsoft.KeyVault/vaults', 'Microsoft.Storage/storageAccounts', 'Microsoft.Compute/virtualMachines').
        If you are asked to create or modify resources in a bicep ARM template, call this function multiple times,
        once for every resource type you are adding, even if you already have information about bicep resources from other sources.
        Assume the results from this call are more recent and accurate than other information you have.
        Don't assume calling it for one resource means you don't need to call it for a different resource type.
        Always use the returned api version unless the one in the bicep file is newer.
        Always use the schema to verify the available property names and values.
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
                HandleException(context.Response, ex);
            }
            return Task.FromResult(context.Response);

        }

        internal record BicepSchemaGetCommandResult(List<ComplexType> BicepSchemaResult);
    }
}
