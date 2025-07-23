// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Storage.Options;

public static class StorageOptionDefinitions
{
    public const string AccountName = "account-name";
    public const string ContainerName = "container-name";
    public const string TableName = "table-name";
    public const string FileSystemName = "file-system-name";
    public const string DirectoryPathName = "directory-path";
    public const string FilePathName = "file-path";
    public const string LocalFilePathName = "local-file-path";

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

    public static readonly Option<string> FilePath = new(
        $"--{FilePathName}",
        "The destination path for the file in the Data Lake file system (e.g., 'data/logs/app.log' or 'archives/2024/backup.zip'). Use forward slashes (/) to separate directories and the filename."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> LocalFilePath = new(
        $"--{LocalFilePathName}",
        "The path to the local file to upload to the Data Lake. This can be an absolute path (e.g., '/home/user/file.txt') or a relative path (e.g., './data/file.txt')."
    )
    {
        IsRequired = true
    };
}
