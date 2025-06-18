// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.KeyVault.Commands.Key;
using AzureMcp.Areas.KeyVault.Commands.Secret;
using AzureMcp.Areas.KeyVault.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.KeyVault;

internal class KeyVaultSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IKeyVaultService, KeyVaultService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var keyVault = new CommandGroup("keyvault", "Key Vault operations - Commands for managing and accessing Azure Key Vault resources.");
        rootGroup.AddSubGroup(keyVault);

        AddKeysGroup(keyVault, loggerFactory);
        AddSecretGroup(keyVault, loggerFactory);
    }

    private static void AddSecretGroup(CommandGroup keyVault, ILoggerFactory loggerFactory)
    {
        var secret = new CommandGroup("secret", "Key Vault secret operations - Commands for managing and accessing secrets in Azure Key Vault.");
        keyVault.AddSubGroup(secret);
        secret.AddCommand("get", new SecretGetCommand(loggerFactory.CreateLogger<SecretGetCommand>()));
    }

    private static void AddKeysGroup(CommandGroup keyVault, ILoggerFactory loggerFactory)
    {
        var keys = new CommandGroup("key", "Key Vault key operations - Commands for managing and accessing keys in Azure Key Vault.");
        keyVault.AddSubGroup(keys);

        keys.AddCommand("list", new KeyListCommand(loggerFactory.CreateLogger<KeyListCommand>()));
        keys.AddCommand("get", new KeyGetCommand(loggerFactory.CreateLogger<KeyGetCommand>()));
        keys.AddCommand("create", new KeyCreateCommand(loggerFactory.CreateLogger<KeyCreateCommand>()));
    }
}
