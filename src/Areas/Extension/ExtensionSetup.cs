// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Extension.Commands;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Extension;

internal class ExtensionSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var extension = new CommandGroup("extension", "Extension commands for additional functionality");
        rootGroup.AddSubGroup(extension);

        extension.AddCommand("az", new AzCommand(loggerFactory.CreateLogger<AzCommand>()));
        extension.AddCommand("azd", new AzdCommand(loggerFactory.CreateLogger<AzdCommand>()));
    }
}
