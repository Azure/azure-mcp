// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.SignalR.Commands.CustomCertificate;
using AzureMcp.SignalR.Commands.CustomDomain;
using AzureMcp.SignalR.Commands.Key;
using AzureMcp.SignalR.Commands.SignalR;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.SignalR;

public class SignalRSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ISignalRService, SignalRService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var signalr = new CommandGroup("signalr",
            "Azure SignalR operations - Commands for managing Azure SignalR Service resources. Includes operations for listing SignalR services, managing hubs, configuring access keys, and scaling SignalR instances.");
        rootGroup.AddSubGroup(signalr);

        var service = new CommandGroup("service",
            "SignalR service operations - Commands for managing Azure SignalR Service resources.");
        signalr.AddSubGroup(signalr);

        service.AddCommand("list",
            new SignalRServiceListCommand(loggerFactory.CreateLogger<SignalRServiceListCommand>()));
        service.AddCommand("show", new SignalRShowCommand(loggerFactory.CreateLogger<SignalRShowCommand>()));

        // Azure SignalR Certificate
        var certificate = new CommandGroup("custom-certificate",
            "SignalR certificate operations - Commands for managing custom certificates in Azure SignalR Service resources.");
        signalr.AddSubGroup(certificate);

        certificate.AddCommand("show",
            new CustomCertificateShowCommand(loggerFactory.CreateLogger<CustomCertificateShowCommand>()));
        certificate.AddCommand("list",
            new CustomCertificateListCommand(loggerFactory.CreateLogger<CustomCertificateListCommand>()));

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
