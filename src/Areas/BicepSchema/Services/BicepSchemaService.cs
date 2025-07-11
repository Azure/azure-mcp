// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.BicepSchema.Services.ResourceProperties;
using AzureMcp.Areas.BicepSchema.Services.ResourceProperties.Entities;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.BicepSchema.Support;
using Microsoft.Extensions.DependencyInjection;

namespace AzureMcp.Areas.BicepSchema.Services
{
    public class BicepSchemaService() : BaseAzureService, IBicepSchemaService
    {
        public TypesDefinitionResult GetResourceTypeDefinitions(IServiceProvider serviceProvider, string resourceTypeName, string? apiVersion = null)
        {
            ResourceVisitor resourceVisitor = serviceProvider.GetRequiredService<ResourceVisitor>();

            if (string.IsNullOrEmpty(apiVersion))
            {
                apiVersion = ApiVersionSelector.SelectLatestStable(resourceVisitor.GetResourceApiVersions(resourceTypeName));
            }

            return resourceVisitor.LoadSingleResource(resourceTypeName, apiVersion);
        }
    }
}
