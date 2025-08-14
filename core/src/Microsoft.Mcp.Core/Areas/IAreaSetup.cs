// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Mcp.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Mcp.Core.Areas
{
    public interface IAreaSetup
    {
        void ConfigureServices(IServiceCollection services);
        void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory);
    }
}
