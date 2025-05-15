// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using AzureMcp.Arguments.Server;

namespace AzureMcp.Models.Argument;

public static class ArgumentDefinitions
{
    public static class Common
    {
        public const string TenantName = "tenant";
        public const string SubscriptionName = "subscription";
        public const string ResourceGroupName = "resource-group";
        public const string AuthMethodName = "auth-method";

        public static readonly ArgumentDefinition<string> Tenant = new(
            TenantName,
            "The Azure Active Directory tenant ID or name. This can be either the GUID identifier or the display name of your Azure AD tenant.",
            required: false
        );

        public static readonly ArgumentDefinition<string> Subscription = new(
            SubscriptionName,
            "The Azure subscription ID or name. This can be either the GUID identifier or the display name of the Azure subscription to use.",
            required: true
        );

        public static readonly ArgumentDefinition<AuthMethod> AuthMethod = new(
            AuthMethodName,
            "Authentication method to use. Options: 'credential' (Azure CLI/managed identity), 'key' (access key), or 'connectionString'.",
            defaultValue: Models.AuthMethod.Credential,
            required: false
        );

        public static readonly ArgumentDefinition<string> ResourceGroup = new(
            ResourceGroupName,
            "The name of the Azure resource group. This is a logical container for Azure resources.",
            required: true
        );
    }

    public static class RetryPolicy
    {
        public const string DelayName = "retry-delay";
        public const string MaxDelayName = "retry-max-delay";
        public const string MaxRetriesName = "retry-max-retries";
        public const string ModeName = "retry-mode";
        public const string NetworkTimeoutName = "retry-network-timeout";

        public static readonly ArgumentDefinition<double> Delay = new(
            DelayName,
            "Initial delay in seconds between retry attempts. For exponential backoff, this value is used as the base.",
            defaultValue: 2.0,
            required: false,
            hidden: true
        );

        public static readonly ArgumentDefinition<double> MaxDelay = new(
            MaxDelayName,
            "Maximum delay in seconds between retries, regardless of the retry strategy.",
            defaultValue: 10.0,
            required: false,
            hidden: true
        );

        public static readonly ArgumentDefinition<int> MaxRetries = new(
            MaxRetriesName,
            "Maximum number of retry attempts for failed operations before giving up.",
            defaultValue: 3,
            required: false,
            hidden: true
        );

        public static readonly ArgumentDefinition<RetryMode> Mode = new(
            ModeName,
            "Retry strategy to use. 'fixed' uses consistent delays, 'exponential' increases delay between attempts.",
            defaultValue: Azure.Core.RetryMode.Exponential,
            required: false,
            hidden: true
        );

        public static readonly ArgumentDefinition<double> NetworkTimeout = new(
            NetworkTimeoutName,
            "Network operation timeout in seconds. Operations taking longer than this will be cancelled.",
            defaultValue: 100.0,
            required: false,
            hidden: true
        );
    }

    public static class Storage
    {
        public const string AccountName = "account-name";
        public const string ContainerName = "container-name";
        public const string TableName = "table-name";

        public static readonly ArgumentDefinition<string> Account = new(
            AccountName,
            "The name of the Azure Storage account. This is the unique name you chose for your storage account (e.g., 'mystorageaccount').",
            required: true
        );

        public static readonly ArgumentDefinition<string> Container = new(
            ContainerName,
            "The name of the container to access within the storage account.",
            required: true
        );

        public static readonly ArgumentDefinition<string> Table = new(
            TableName,
            "The name of the table to access within the storage account.",
            required: true
        );
    }

    public static class Cosmos
    {
        public const string AccountName = "account-name";
        public const string DatabaseName = "database-name";
        public const string ContainerName = "container-name";
        public const string QueryText = "query";

        public static readonly ArgumentDefinition<string> Account = new(
            AccountName,
            "The name of the Cosmos DB account to query (e.g., my-cosmos-account).",
            required: true
        );

        public static readonly ArgumentDefinition<string> Database = new(
            DatabaseName,
            "The name of the database to query (e.g., my-database).",
            required: true
        );

        public static readonly ArgumentDefinition<string> Container = new(
            ContainerName,
            "The name of the container to query (e.g., my-container).",
            required: true
        );

        public static readonly ArgumentDefinition<string> Query = new(
            QueryText,
            "SQL query to execute against the container. Uses Cosmos DB SQL syntax.",
            defaultValue: "SELECT * FROM c",
            required: false
        );
    }

    public static class Postgres
    {
        public const string UserName = "user-name";
        public const string ServerName = "server";
        public const string DatabaseName = "database";
        public const string TableName = "table";
        public const string QueryText = "query";
        public const string ParamName = "param";

        public static readonly ArgumentDefinition<string> User = new(
            UserName,
            "The user name to access PostgreSQL server.",
            required: true
        );

        public static readonly ArgumentDefinition<string> Server = new(
            ServerName,
            "The PostgreSQL server to be accessed.",
            required: true
        );

        public static readonly ArgumentDefinition<string> Database = new(
            DatabaseName,
            "The PostgreSQL database to be access.",
            required: true
        );

        public static readonly ArgumentDefinition<string> Table = new(
            TableName,
            "The PostgreSQL table to be access.",
            required: true
        );

        public static readonly ArgumentDefinition<string> Query = new(
            QueryText,
            "Query to be executed against a PostgreSQL database.",
            required: true
        );

        public static readonly ArgumentDefinition<string> Param = new(
            ParamName,
            "The PostgreSQL parameter to be accessed.",
            required: true
        );
    }

    public static class Search
    {
        public const string ServiceName = "service-name";
        public const string IndexName = "index-name";
        public const string QueryName = "query";

        public static readonly ArgumentDefinition<string> Service = new(
            ServiceName,
            "The name of the Azure AI Search service (e.g., my-search-service).",
            required: true
        );

        public static readonly ArgumentDefinition<string> Index = new(
            IndexName,
            "The name of the search index within the Azure AI Search service.",
            required: true
        );

        public static readonly ArgumentDefinition<string> Query = new(
            QueryName,
            "The search query to execute against the Azure AI Search index.",
            required: true
        );
    }

    public static class Monitor
    {
        public const string WorkspaceIdOrName = "workspace";
        public const string TableNameName = "table-name";
        public const string TableTypeName = "table-type";
        public const string QueryTextName = "query";
        public const string HoursName = "hours";
        public const string LimitName = "limit";

        public static readonly ArgumentDefinition<string> Workspace = new(
            WorkspaceIdOrName,
            "The Log Analytics workspace ID or name. This can be either the unique identifier (GUID) or the display name of your workspace.",
            required: true
        );

        public static readonly ArgumentDefinition<string> TableType = new(
            TableTypeName,
            "The type of table to query. Options: 'CustomLog', 'AzureMetrics', etc.",
            defaultValue: "CustomLog",
            required: true
        );

        public static readonly ArgumentDefinition<string> TableName = new(
            TableNameName,
            "The name of the table to query. This is the specific table within the workspace.",
            required: true
        );

        public static readonly ArgumentDefinition<string> Query = new(
            QueryTextName,
            "The KQL query to execute against the Log Analytics workspace. You can use predefined queries by name:\n" +
            "- 'recent': Shows most recent logs ordered by TimeGenerated\n" +
            "- 'errors': Shows error-level logs ordered by TimeGenerated\n" +
            "Otherwise, provide a custom KQL query.",
            required: true
        );

        public static readonly ArgumentDefinition<int> Hours = new(
            HoursName,
            "The number of hours to query back from now.",
            defaultValue: 24,
            required: true
        );

        public static readonly ArgumentDefinition<int> Limit = new(
            LimitName,
            "The maximum number of results to return.",
            defaultValue: 20,
            required: true
        );
    }

    public static class Service
    {
        public const string TransportName = "transport";
        public const string PortName = "port";

        public static readonly ArgumentDefinition<string> Transport = new(
            TransportName,
            "Transport mechanism to use for Azure MCP Server.",
            defaultValue: TransportTypes.StdIo,
            required: false
            );

        public static readonly ArgumentDefinition<int> Port = new(
            PortName,
            "Port to use for Azure MCP Server.",
            defaultValue: 5008,
            required: false
            );
    }

    public static class AppConfig
    {
        public const string AccountName = "account-name";
        public const string KeyName = "key";
        public const string ValueName = "value";
        public const string LabelName = "label";

        public static readonly ArgumentDefinition<string> Account = new(
            AccountName,
            "The name of the App Configuration store (e.g., my-appconfig).",
            required: true
        );

        public static readonly ArgumentDefinition<string> Key = new(
            KeyName,
            "The name of the key to access within the App Configuration store.",
            required: true
        );

        public static readonly ArgumentDefinition<string> Value = new(
            ValueName,
            "The value to set for the configuration key.",
            required: true
        );

        public static readonly ArgumentDefinition<string> Label = new(
            LabelName,
            "The label to apply to the configuration key. Labels are used to group and organize settings.",
            required: false
        );

        public static class KeyValueList
        {
            public static readonly ArgumentDefinition<string> Key = new(
                KeyName,
                "Specifies the key filter, if any, to be used when retrieving key-values. The filter can be an exact match, for example a filter of \"foo\" would get all key-values with a key of \"foo\", or the filter can include a '*' character at the end of the string for wildcard searches (e.g., 'App*'). If omitted all keys will be retrieved.",
                required: false
            );

            public static readonly ArgumentDefinition<string> Label = new(
                LabelName,
                "Specifies the label filter, if any, to be used when retrieving key-values. The filter can be an exact match, for example a filter of \"foo\" would get all key-values with a label of \"foo\", or the filter can include a '*' character at the end of the string for wildcard searches (e.g., 'Prod*'). This filter is case-sensitive. If omitted, all labels will be retrieved.",
                required: false
            );
        }
    }

    public static class Kusto
    {
        public const string ClusterName = "cluster-name";
        public const string ClusterUriName = "cluster-uri";
        public const string DatabaseName = "database-name";
        public const string TableName = "table-name";
        public const string LimitName = "limit";
        public const string QueryText = "query";


        public static readonly ArgumentDefinition<string> Cluster = new(
            ClusterName,
            "Kusto Cluster name.",
            required: false
        );

        public static readonly ArgumentDefinition<string> ClusterUri = new(
            ClusterUriName,
            "Kusto Cluster URI.",
            required: false
        );

        public static readonly ArgumentDefinition<string> Database = new(
            DatabaseName,
            "Kusto Database name.",
            required: true
        );

        public static readonly ArgumentDefinition<string> Table = new(
            TableName,
            "Kusto Table name.",
            required: true
        );

        public static readonly ArgumentDefinition<int> Limit = new(
            LimitName,
            "The maximum number of results to return.",
            defaultValue: 10,
            required: true
        );

        public static readonly ArgumentDefinition<string> Query = new(
            QueryText,
            "Kusto query to execute. Uses KQL syntax.",
            required: true
        );
    }

    public static class Extension
    {
        public static class Az
        {
            public const string CommandName = "command";

            public static readonly ArgumentDefinition<string> Command = new(
                CommandName,
                "The Azure CLI command to execute (without the 'az' prefix). For example: 'group list'.",
                required: true
            );
        }

        public static class Azd
        {
            public const string CommandName = "command";

            public static readonly ArgumentDefinition<string> Command = new(
                CommandName,
                "The Azure Developer CLI command to execute (without the 'azd' prefix). For example: 'up'.",
                required: true
            );
        }
    }

    public static class KeyVault
    {
        public const string VaultNameParam = "vault";
        public const string KeyNameParam = "key";
        public const string KeyTypeParam = "key-type";
        public const string IncludeManagedKeysParam = "include-managed";

        public static readonly ArgumentDefinition<string> VaultName = new(
            VaultNameParam,
            "The name of the Key Vault.",
            required: true);

        public static readonly ArgumentDefinition<string> KeyName = new(
            KeyNameParam,
            "The name of the key to retrieve/modify from the Key Vault.",
            required: true);

        public static readonly ArgumentDefinition<string> KeyType = new(
            KeyTypeParam,
            "The type of key to create (RSA, EC).",
            required: true);

        public static readonly ArgumentDefinition<bool> IncludeManagedKeys = new(
            IncludeManagedKeysParam,
            "Whether or not to include managed keys in results.",
            required: false);
    }

    public static class ServiceBus
    {
        public const string NamespaceName = "namespace";
        public const string QueueName = "queue-name";
        public const string MaxMessagesName = "max-messages";
        public const string TopicName = "topic-name";
        public const string SubscriptionName = "subscription-name";

        public static readonly ArgumentDefinition<string> Namespace = new(
            NamespaceName,
            "The Service Bus namespace name.",
            required: true);

        public static readonly ArgumentDefinition<string> Queue = new(
            QueueName,
            "The queue name to peek messages from.",
            required: true);

        public static readonly ArgumentDefinition<string> Subscription = new(
            SubscriptionName,
            "The name of subscription to peek messages from.",
            required: true);

        public static readonly ArgumentDefinition<string> Topic = new(
            TopicName,
            "The name of the topic containing the subscription.",
            required: true);

        public static readonly ArgumentDefinition<int> MaxMessages = new(
            MaxMessagesName,
            "The maximum number of messages to return.",
            defaultValue: 1,
            required: false
        );
    }
}
