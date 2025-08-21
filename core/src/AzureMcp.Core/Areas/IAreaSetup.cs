// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Core.Areas
{
    public interface IAreaSetup
    {
        /// <summary>
        /// Gets the name of the area.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Configure any dependencies.
        /// </summary>
        void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Register the area's commands to the root group.
        /// </summary>
        void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory);
    }
}
