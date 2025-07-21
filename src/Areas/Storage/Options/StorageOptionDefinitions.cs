// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Storage.Options;

public static class StorageOptionDefinitions
{
    public const string AccountName = "account-name";
    public const string ContainerName = "container-name";
    public const string TableName = "table-name";
    public const string FileSystemName = "file-system-name";
    public const string PathPrefix = "path-prefix";

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

    public static readonly Option<string> FileSystem = new(
        $"--{FileSystemName}",
        "The name of the Data Lake file system to access within the storage account."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> PathPrefix = new(
        $"--path-prefix",
        "Optional path prefix to filter the results. If specified, only paths that start with this prefix will be returned."
    );
}
