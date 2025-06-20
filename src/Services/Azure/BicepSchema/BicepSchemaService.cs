// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Services.Azure.BicepSchema.ResourceProperties;
using AzureMcp.Services.Azure.BicepSchema.ResourceProperties.Entities;
using AzureMcp.Services.Azure.BicepSchema.Support;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AzureMcp.Services.Azure.BicepSchema
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
