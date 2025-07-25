// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Storage.Options;

public static class StorageOptionDefinitions
{
    public const string AccountName = "account-name";
    public const string ContainerName = "container-name";
    public const string TableName = "table-name";
    public const string FileSystemName = "file-system-name";
    public const string DirectoryPathName = "directory-path";

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

    public static readonly Option<string> DirectoryPath = new(
        $"--{DirectoryPathName}",
        "The full path of the directory to create in the Data Lake, including the file system name (e.g., 'myfilesystem/data/logs' or 'myfilesystem/archives/2024'). Use forward slashes (/) to separate the file system name from the directory path and for subdirectories."
    )
    {
        IsRequired = true
    };
}
