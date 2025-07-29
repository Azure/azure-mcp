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
    public const string FilePathName = "file-path";
    public const string SourceFilePathName = "source-file-path";
    public const string OverwriteName = "overwrite";

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
        "The destination path for the file in the Data Lake (e.g., 'data/myfile.txt' or 'logs/2024/app.log'). Use forward slashes (/) to separate directories."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> SourceFilePath = new(
        $"--{SourceFilePathName}",
        "The local file path to upload to the Data Lake. Must be a valid file path accessible to the application."
    )
    {
        IsRequired = true
    };

    public static readonly Option<bool> Overwrite = new(
        $"--{OverwriteName}",
        "Whether to overwrite the file if it already exists in the Data Lake. Default is false."
    )
    {
        IsRequired = false
    };
}
