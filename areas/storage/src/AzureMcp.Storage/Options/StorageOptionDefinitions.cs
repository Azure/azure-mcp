// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Storage.Options;

public static class StorageOptionDefinitions
{
    public const string AccountName = "account";
    public const string ContainerName = "container";
    public const string TableName = "table";
    public const string FileSystemName = "file-system";
    public const string DirectoryPathName = "directory-path";
    public const string TierName = "tier";
    public const string BlobNamesParam = "blob-names";
    public const string FilterPathName = "filter-path";
    public const string RecursiveName = "recursive";
    public const string ShareName = "share";
    public const string PrefixName = "prefix";

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

    public static readonly Option<string> Tier = new(
        $"--{TierName}",
        "The access tier to set for the blobs. Valid values include Hot, Cool, Archive, and others depending on the storage account type. See Azure documentation for the complete list of supported access tiers."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string[]> BlobNames = new(
        $"--{BlobNamesParam}",
        "The names of the blobs to set the access tier for. Provide multiple blob names separated by spaces. Each blob name should be the full path within the container (e.g., 'file1.txt' or 'folder/file2.txt')."
    )
    {
        IsRequired = true,
        AllowMultipleArgumentsPerToken = true
    };

    public static readonly Option<string> FilterPath = new(
        $"--{FilterPathName}",
        "The prefix to filter paths in the Data Lake. Only paths that start with this prefix will be listed."
    )
    {
        IsRequired = false
    };

    public static readonly Option<bool> Recursive = new(
        $"--{RecursiveName}",
        () => false,
        "Flag to indicate whether the command will operate recursively on all subdirectories."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> Share = new(
        $"--{ShareName}",
        "The name of the file share to access within the storage account."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Prefix = new(
        $"--{PrefixName}",
        "Optional prefix to filter results. Only items that start with this prefix will be returned.")
    {
        IsRequired = false
    };
}
