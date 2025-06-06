// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using AzureMcp.Options.Server;

namespace AzureMcp.Models.Option;

public static class OptionDefinitions
{
    public static class Common
    {
        public const string TenantName = "tenant";
        public const string SubscriptionName = "subscription";
        public const string ResourceGroupName = "resource-group";
        public const string AuthMethodName = "auth-method";

        public static readonly Option<string> Tenant = new(
            $"--{TenantName}",
            "The Azure Active Directory tenant ID or name. This can be either the GUID identifier or the display name of your Azure AD tenant."
        )
        {
            IsRequired = false,
            IsHidden = true
        };

        public static readonly Option<string> Subscription = new(
            $"--{SubscriptionName}",
            "The Azure subscription ID or name. This can be either the GUID identifier or the display name of the Azure subscription to use."
        )
        {
            IsRequired = true
        };

        public static readonly Option<AuthMethod> AuthMethod = new(
            $"--{AuthMethodName}",
            () => Models.AuthMethod.Credential,
            "Authentication method to use. Options: 'credential' (Azure CLI/managed identity), 'key' (access key), or 'connectionString'."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> ResourceGroup = new(
            $"--{ResourceGroupName}",
            "The name of the Azure resource group. This is a logical container for Azure resources."
        )
        {
            IsRequired = true
        };
    }

    public static class RetryPolicy
    {
        public const string DelayName = "retry-delay";
        public const string MaxDelayName = "retry-max-delay";
        public const string MaxRetriesName = "retry-max-retries";
        public const string ModeName = "retry-mode";
        public const string NetworkTimeoutName = "retry-network-timeout";

        public static readonly Option<double> Delay = new(
            $"--{DelayName}",
            () => 2.0,
            "Initial delay in seconds between retry attempts. For exponential backoff, this value is used as the base."
        )
        {
            IsRequired = false,
            IsHidden = true
        };

        public static readonly Option<double> MaxDelay = new(
            $"--{MaxDelayName}",
            () => 10.0,
            "Maximum delay in seconds between retries, regardless of the retry strategy."
        )
        {
            IsRequired = false,
            IsHidden = true
        };

        public static readonly Option<int> MaxRetries = new(
            $"--{MaxRetriesName}",
            () => 3,
            "Maximum number of retry attempts for failed operations before giving up."
        )
        {
            IsRequired = false,
            IsHidden = true
        };

        public static readonly Option<RetryMode> Mode = new(
            $"--{ModeName}",
            () => RetryMode.Exponential,
            "Retry strategy to use. 'fixed' uses consistent delays, 'exponential' increases delay between attempts."
        )
        {
            IsRequired = false,
            IsHidden = true
        };

        public static readonly Option<double> NetworkTimeout = new(
            $"--{NetworkTimeoutName}",
            () => 100.0,
            "Network operation timeout in seconds. Operations taking longer than this will be cancelled."
        )
        {
            IsRequired = false,
            IsHidden = true
        };
    }

    public static class Storage
    {
        public const string AccountName = "account-name";
        public const string ContainerName = "container-name";
        public const string TableName = "table-name";

        public static readonly Option<string> Account = new(
            $"--{AccountName}",
            "The name of the Azure Storage account. This is the unique name you chose for your storage account (e.g., 'mystorageaccount')."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Container = new(
            $"--{ContainerName}",
            "The name of the container to access within the storage account."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Table = new(
            $"--{TableName}",
            "The name of the table to access within the storage account."
        )
        {
            IsRequired = true
        };
    }

    public static class Cosmos
    {
        public const string AccountName = "account-name";
        public const string DatabaseName = "database-name";
        public const string ContainerName = "container-name";
        public const string QueryText = "query";

        public static readonly Option<string> Account = new(
            $"--{AccountName}",
            "The name of the Cosmos DB account to query (e.g., my-cosmos-account)."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Database = new(
            $"--{DatabaseName}",
            "The name of the database to query (e.g., my-database)."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Container = new(
            $"--{ContainerName}",
            "The name of the container to query (e.g., my-container)."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Query = new(
            $"--{QueryText}",
            () => "SELECT * FROM c",
            "SQL query to execute against the container. Uses Cosmos DB SQL syntax."
        )
        {
            IsRequired = false
        };
    }

    public static class Postgres
    {
        public const string UserName = "user-name";
        public const string ServerName = "server";
        public const string DatabaseName = "database";
        public const string TableName = "table";
        public const string QueryText = "query";
        public const string ParamName = "param";

        public static readonly Option<string> User = new(
            $"--{UserName}",
            "The user name to access PostgreSQL server."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Server = new(
            $"--{ServerName}",
            "The PostgreSQL server to be accessed."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Database = new(
            $"--{DatabaseName}",
            "The PostgreSQL database to be access."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Table = new(
            $"--{TableName}",
            "The PostgreSQL table to be access."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Query = new(
            $"--{QueryText}",
            "Query to be executed against a PostgreSQL database."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Param = new(
            $"--{ParamName}",
            "The PostgreSQL parameter to be accessed."
        )
        {
            IsRequired = true
        };
    }

    public static class Search
    {
        public const string ServiceName = "service-name";
        public const string IndexName = "index-name";
        public const string QueryName = "query";

        public static readonly Option<string> Service = new(
            $"--{ServiceName}",
            "The name of the Azure AI Search service (e.g., my-search-service)."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Index = new(
            $"--{IndexName}",
            "The name of the search index within the Azure AI Search service."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Query = new(
            $"--{QueryName}",
            "The search query to execute against the Azure AI Search index."
        )
        {
            IsRequired = true
        };
    }

    public static class Monitor
    {
        public const string WorkspaceIdOrName = "workspace";
        public const string TableNameName = "table-name";
        public const string TableTypeName = "table-type";
        public const string QueryTextName = "query";
        public const string HoursName = "hours";
        public const string LimitName = "limit";

        public const string EntityName = "entity";
        public const string HealthModelName = "model-name";

        public static readonly Option<string> Workspace = new(
            $"--{WorkspaceIdOrName}",
            "The Log Analytics workspace ID or name. This can be either the unique identifier (GUID) or the display name of your workspace."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> TableType = new(
            $"--{TableTypeName}",
            () => "CustomLog",
            "The type of table to query. Options: 'CustomLog', 'AzureMetrics', etc."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> TableName = new(
            $"--{TableNameName}",
            "The name of the table to query. This is the specific table within the workspace."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Query = new(
            $"--{QueryTextName}",
            "The KQL query to execute against the Log Analytics workspace. You can use predefined queries by name:\n" +
            "- 'recent': Shows most recent logs ordered by TimeGenerated\n" +
            "- 'errors': Shows error-level logs ordered by TimeGenerated\n" +
            "Otherwise, provide a custom KQL query."
        )
        {
            IsRequired = true
        };

        public static readonly Option<int> Hours = new(
            $"--{HoursName}",
            () => 24,
            "The number of hours to query back from now."
        )
        {
            IsRequired = true
        };

        public static readonly Option<int> Limit = new(
            $"--{LimitName}",
            () => 20,
            "The maximum number of results to return."
        )
        {
            IsRequired = true
        };

        public static class Health
        {
            public static readonly Option<string> Entity = new(
                $"--{EntityName}",
                "The entity to get health for."
            )
            {
                IsRequired = true
            };

            public static readonly Option<string> HealthModel = new(
                $"--{HealthModelName}",
                "The name of the health model for which to get the health."
            )
            {
                IsRequired = true
            };
        }
    }

    public static class Service
    {
        public const string TransportName = "transport";
        public const string PortName = "port";
        public const string ServiceName = "service";

        public static readonly Option<string> Transport = new(
            $"--{TransportName}",
            () => TransportTypes.StdIo,
            "Transport mechanism to use for Azure MCP Server."
        )
        {
            IsRequired = false
        };

        public static readonly Option<int> Port = new(
            $"--{PortName}",
            () => 5008,
            "Port to use for Azure MCP Server."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string?> ServiceType = new(
            $"--{ServiceName}",
            () => null,
            "The service to expose on the MCP server."
        )
        {
            IsRequired = false,
        };
    }

    public static class AppConfig
    {
        public const string AccountName = "account-name";
        public const string KeyName = "key";
        public const string ValueName = "value";
        public const string LabelName = "label";

        public static readonly Option<string> Account = new(
            $"--{AccountName}",
            "The name of the App Configuration store (e.g., my-appconfig)."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Key = new(
            $"--{KeyName}",
            "The name of the key to access within the App Configuration store."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Value = new(
            $"--{ValueName}",
            "The value to set for the configuration key."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Label = new(
            $"--{LabelName}",
            "The label to apply to the configuration key. Labels are used to group and organize settings."
        )
        {
            IsRequired = false
        };

        public static class KeyValueList
        {
            public static readonly Option<string> Key = new(
                $"--{KeyName}",
                "Specifies the key filter, if any, to be used when retrieving key-values. The filter can be an exact match, for example a filter of \"foo\" would get all key-values with a key of \"foo\", or the filter can include a '*' character at the end of the string for wildcard searches (e.g., 'App*'). If omitted all keys will be retrieved."
            )
            {
                IsRequired = false
            };

            public static readonly Option<string> Label = new(
                $"--{LabelName}",
                "Specifies the label filter, if any, to be used when retrieving key-values. The filter can be an exact match, for example a filter of \"foo\" would get all key-values with a label of \"foo\", or the filter can include a '*' character at the end of the string for wildcard searches (e.g., 'Prod*'). This filter is case-sensitive. If omitted, all labels will be retrieved."
            )
            {
                IsRequired = false
            };
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


        public static readonly Option<string> Cluster = new(
            $"--{ClusterName}",
            "Kusto Cluster name."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> ClusterUri = new(
            $"--{ClusterUriName}",
            "Kusto Cluster URI."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> Database = new(
            $"--{DatabaseName}",
            "Kusto Database name."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Table = new(
            $"--{TableName}",
            "Kusto Table name."
        )
        {
            IsRequired = true
        };

        public static readonly Option<int> Limit = new(
            $"--{LimitName}",
            () => 10,
            "The maximum number of results to return."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Query = new(
            $"--{QueryText}",
            "Kusto query to execute. Uses KQL syntax."
        )
        {
            IsRequired = true
        };
    }

    public static class Redis
    {
        public const string CacheName = "cache";
        public const string ClusterName = "cluster";

        public static readonly Option<string> Cache = new(
            $"--{CacheName}",
            "The name of the Redis cache (e.g., my-redis-cache)."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Cluster = new(
            $"--{ClusterName}",
            "The name of the Redis cluster (e.g., my-redis-cluster)."
        )
        {
            IsRequired = true
        };
    }

    public static class Extension
    {
        public static class Az
        {
            public const string CommandName = "command";

            public static readonly Option<string> Command = new(
                $"--{CommandName}",
                "The Azure CLI command to execute (without the 'az' prefix). For example: 'group list'."
            )
            {
                IsRequired = true
            };
        }

        public static class Azd
        {
            public const string CommandName = "command";

            public static readonly Option<string> Command = new(
                $"--{CommandName}",
                """
                The Azure Developer CLI command and arguments to execute (without the 'azd' prefix).
                Examples:
                - up
                - env list
                - env get-values
                """
            )
            {
                IsRequired = false
            };

            public const string CwdName = "cwd";

            public static readonly Option<string> Cwd = new(
                $"--{CwdName}",
                "The current working directory for the command. This is the directory where the command will be executed."
            )
            {
                IsRequired = true
            };

            public const string EnvironmentName = "environment";
            public static readonly Option<string> Environment = new(
                $"--{EnvironmentName}",
                """
                The name of the azd environment to use. This is typically the name of the Azure environment (e.g., 'prod', 'dev', 'test', 'staging').
                Always set environments for azd commands that support -e, --environment argument.
                """
            )
            {
                IsRequired = false
            };

            public const string LearnName = "learn";
            public static readonly Option<bool> Learn = new(
                $"--{LearnName}",
                () => false,
                """
                Flag to indicate whether to learn best practices and usage patterns for azd tool.
                Always run this command with learn=true and empty command on first run.
                """
            )
            {
                IsRequired = false
            };
        }
    }

    public static class KeyVault
    {
        public const string VaultNameParam = "vault";
        public const string KeyNameParam = "key";
        public const string KeyTypeParam = "key-type";
        public const string IncludeManagedKeysParam = "include-managed";
        public const string SecretNameParam = "secret";

        public static readonly Option<string> VaultName = new(
            $"--{VaultNameParam}",
            "The name of the Key Vault."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> KeyName = new(
            $"--{KeyNameParam}",
            "The name of the key to retrieve/modify from the Key Vault."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> KeyType = new(
            $"--{KeyTypeParam}",
            "The type of key to create (RSA, EC)."
        )
        {
            IsRequired = true
        };

        public static readonly Option<bool> IncludeManagedKeys = new(
            $"--{IncludeManagedKeysParam}",
            "Whether or not to include managed keys in results."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> SecretName = new(
            $"--{SecretNameParam}",
            "The name of the secret."
        )
        {
            IsRequired = true
        };
    }

    public static class ServiceBus
    {
        public const string NamespaceName = "namespace";
        public const string QueueName = "queue-name";
        public const string MaxMessagesName = "max-messages";
        public const string TopicName = "topic-name";
        public const string SubscriptionName = "subscription-name";

        public static readonly Option<string> Namespace = new(
            $"--{NamespaceName}",
            "The fully qualified Service Bus namespace host name. (This is usually in the form <namespace>.servicebus.windows.net)"
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Queue = new(
            $"--{QueueName}",
            "The queue name to peek messages from."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Subscription = new(
            $"--{SubscriptionName}",
            "The name of subscription to peek messages from."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> Topic = new(
            $"--{TopicName}",
            "The name of the topic containing the subscription."
        )
        {
            IsRequired = true
        };

        public static readonly Option<int> MaxMessages = new(
            $"--{MaxMessagesName}",
            () => 1,
            "The maximum number of messages to return."
        )
        {
            IsRequired = false
        };
    }
}
