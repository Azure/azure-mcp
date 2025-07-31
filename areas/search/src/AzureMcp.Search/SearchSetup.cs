// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Search.Commands.Index;
using AzureMcp.Search.Commands.Service;
using AzureMcp.Search.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Search;

public class SearchSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ISearchService, SearchService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var search = new CommandGroup("search", "Search operations - Commands for managing Azure AI Search services and indexes. Includes operations for listing search services, managing search indexes, querying indexed data, and describing index schemas and configurations.");
        rootGroup.AddSubGroup(search);

        var service = new CommandGroup("service", "Azure AI Search service operations - Commands for listing and managing search services in your Azure subscription.");
        search.AddSubGroup(service);

        service.AddCommand("list", new ServiceListCommand(loggerFactory.CreateLogger<ServiceListCommand>()));

        var index = new CommandGroup("index", "Azure AI Search index operations - Commands for listing and managing search indexes in a specific search service.");
        search.AddSubGroup(index);

        index.AddCommand("list", new IndexListCommand(loggerFactory.CreateLogger<IndexListCommand>()));
        index.AddCommand("describe", new IndexDescribeCommand(loggerFactory.CreateLogger<IndexDescribeCommand>()));
        index.AddCommand("query", new IndexQueryCommand(loggerFactory.CreateLogger<IndexQueryCommand>()));
    }
}
