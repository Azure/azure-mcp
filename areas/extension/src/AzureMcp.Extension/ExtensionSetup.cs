// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Extension.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Extension;

public sealed class ExtensionSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // No additional services needed for Extension area
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var extension = new CommandGroup("extension", "Extension commands for additional functionality");
        rootGroup.AddSubGroup(extension);

        extension.AddCommand("az", new AzCommand(loggerFactory.CreateLogger<AzCommand>()));
        extension.AddCommand("azd", new AzdCommand(loggerFactory.CreateLogger<AzdCommand>()));
        extension.AddCommand("azqr", new AzqrCommand(loggerFactory.CreateLogger<AzqrCommand>()));
    }
}
