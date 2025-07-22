// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Deploy.Commands;

public static class AzureServiceConstants
{
    public enum AzureComputeServiceType
    {
        AppService,
        FunctionApp,
        ContainerApp,
        StaticWebApp
    }

    public enum AzureServiceType
    {
        AzureAISearch,
        AzureAIServices,
        AppService,
        AzureApplicationInsights,
        AzureBotService,
        AzureContainerApp,
        AzureCosmosDB,
        AzureFunctionApp,
        AzureKeyVault,
        AzureDatabaseForMySQL,
        AzureOpenAI,
        AzureDatabaseForPostgreSQL,
        AzurePrivateEndpoint,
        AzureRedisCache,
        AzureSQLDatabase,
        AzureStorageAccount,
        StaticWebApp,
        AzureServiceBus,
        AzureSignalRService,
        AzureVirtualNetwork,
        AzureWebPubSub
    }
}
