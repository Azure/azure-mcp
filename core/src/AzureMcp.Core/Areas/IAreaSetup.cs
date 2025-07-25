// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Core.Areas
{
    public interface IAreaSetup
    {
        void ConfigureServices(IServiceCollection services);
        void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory);
    }
}
