// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Search.Commands.Index;
using AzureMcp.Areas.Search.Commands.Service;
using AzureMcp.Areas.Search.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Search;

public class SearchSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ISearchService, SearchService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var search = new CommandGroup("search", "Search operations - Commands for managing and listing Azure AI Search services.");
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
