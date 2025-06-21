// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas
{
    public interface IAreaSetup
    {
        void ConfigureServices(IServiceCollection services);
        void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory);
    }
}
