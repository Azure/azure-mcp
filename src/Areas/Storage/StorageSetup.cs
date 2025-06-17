// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Storage.Commands.Account;
using AzureMcp.Areas.Storage.Commands.Blob;
using AzureMcp.Areas.Storage.Commands.Blob.Container;
using AzureMcp.Areas.Storage.Commands.Table;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Storage;

internal class StorageSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<Services.IStorageService, Services.StorageService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create Storage command group
        var storage = new CommandGroup("storage", "Storage operations - Commands for managing and accessing Azure Storage resources. Includes operations for containers, blobs, and tables.");
        rootGroup.AddSubGroup(storage);

        // Create Storage subgroups
        var storageAccount = new CommandGroup("account", "Storage account operations - Commands for listing and managing Storage account in your Azure subscription.");
        storage.AddSubGroup(storageAccount);
        storageAccount.AddCommand("list", new AccountListCommand(loggerFactory.CreateLogger<AccountListCommand>()));

        var tables = new CommandGroup("table", "Storage table operations - Commands for working with Azure Table Storage, including listing and querying table.");
        storage.AddSubGroup(tables);
        tables.AddCommand("list", new TableListCommand(loggerFactory.CreateLogger<TableListCommand>()));

        var blobs = new CommandGroup("blob", "Storage blob operations - Commands for uploading, downloading, and managing blob in your Azure Storage accounts.");
        storage.AddSubGroup(blobs);
        blobs.AddCommand("list", new BlobListCommand(loggerFactory.CreateLogger<BlobListCommand>()));

        // Create a containers subgroup under blobs
        var blobContainer = new CommandGroup("container", "Storage blob container operations - Commands for managing blob container in your Azure Storage accounts.");
        blobs.AddSubGroup(blobContainer);
        blobContainer.AddCommand("list", new ContainerListCommand(loggerFactory.CreateLogger<ContainerListCommand>()));
        blobContainer.AddCommand("details", new ContainerDetailsCommand(loggerFactory.CreateLogger<ContainerDetailsCommand>()));
    }
}
