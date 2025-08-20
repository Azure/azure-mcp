// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Deploy.Commands;

public static class AzureServiceConstants
{
    public enum AzureComputeServiceType
    {
        AppService,
        FunctionApp,
        ContainerApp,
        StaticWebApp,
        AKS
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
        AKS,
        AzureDatabaseForMySQL,
        AzureOpenAI,
        AzureDatabaseForPostgreSQL,
        AzurePrivateEndpoint,
        AzureCacheForRedis,
        AzureSQLDatabase,
        AzureStorageAccount,
        StaticWebApp,
        AzureServiceBus,
        AzureSignalRService,
        AzureVirtualNetwork,
        AzureWebPubSub
    }
}
