// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.AzureSignalR.Commands.SignalR;
using AzureMcp.AzureSignalR.Commands.Certificate;
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
        var signalr = new CommandGroup("signalr", "Azure SignalR operations - Commands for managing Azure SignalR Service resources. Includes operations for listing SignalR services, managing hubs, configuring access keys, and scaling SignalR instances.");
        rootGroup.AddSubGroup(signalr);

        // Azure SignalR Service
        var service = new CommandGroup("service", "SignalR Service resource operations - Commands for listing and managing SignalR Service resources in your Azure subscription.");
        signalr.AddSubGroup(service);

        service.AddCommand("list", new SignalRServiceListCommand(loggerFactory.CreateLogger<SignalRServiceListCommand>()));

        // Azure SignalR Certificate
        var certificate = new CommandGroup("certificate", "SignalR certificate operations - Commands for managing custom certificates in Azure SignalR Service resources.");
        signalr.AddSubGroup(certificate);

        certificate.AddCommand("show", new CertificateShowCommand(loggerFactory.CreateLogger<CertificateShowCommand>()));
    }
}
