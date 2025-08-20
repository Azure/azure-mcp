// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.AzureSignalR.Commands.SignalR;
using AzureMcp.AzureSignalR.Commands.Certificate;
using AzureMcp.AzureSignalR.Commands.CustomDomain;
using AzureMcp.AzureSignalR.Commands.Key;
using AzureMcp.AzureSignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.AzureSignalR;

public class AzureSignalRSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IAzureSignalRService, AzureSignalRService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var signalr = new CommandGroup("signalr",
            "Azure SignalR operations - Commands for managing Azure SignalR Service resources. Includes operations for listing SignalR services, managing hubs, configuring access keys, and scaling SignalR instances.");
        rootGroup.AddSubGroup(signalr);

        signalr.AddCommand("list",
            new SignalRServiceListCommand(loggerFactory.CreateLogger<SignalRServiceListCommand>()));
        signalr.AddCommand("show", new SignalRShowCommand(loggerFactory.CreateLogger<SignalRShowCommand>()));

        // Azure SignalR Certificate
        var certificate = new CommandGroup("custom-certificate",
            "SignalR certificate operations - Commands for managing custom certificates in Azure SignalR Service resources.");
        signalr.AddSubGroup(certificate);

        certificate.AddCommand("show",
            new CertificateShowCommand(loggerFactory.CreateLogger<CertificateShowCommand>()));
        certificate.AddCommand("list",
            new CertificateListCommand(loggerFactory.CreateLogger<CertificateListCommand>()));

        var customDomain = new CommandGroup("custom-domain",
            "SignalR custom domain operations - Commands for managing custom domains in Azure SignalR Service resources.");
        signalr.AddSubGroup(customDomain);

        customDomain.AddCommand("show",
            new CustomDomainShowCommand(loggerFactory.CreateLogger<CustomDomainShowCommand>()));
        customDomain.AddCommand("list",
            new CustomDomainListCommand(loggerFactory.CreateLogger<CustomDomainListCommand>()));

        var key = new CommandGroup("key",
            "SignalR key operations - Commands for managing access keys in Azure SignalR Service resources.");
        signalr.AddSubGroup(key);

        key.AddCommand("list", new KeyListCommand(loggerFactory.CreateLogger<KeyListCommand>()));
    }
}
