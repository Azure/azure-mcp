// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.BicepSchema.Services.ResourceProperties;
using AzureMcp.BicepSchema.Services.ResourceProperties.Entities;
using AzureMcp.BicepSchema.Services.Support;
using AzureMcp.Core.Services.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace AzureMcp.BicepSchema.Services
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
