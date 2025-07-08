// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.BicepSchema.Services.ResourceProperties.Entities;

namespace AzureMcp.Areas.BicepSchema.Services
{
    public interface IBicepSchemaService
    {
        TypesDefinitionResult GetResourceTypeDefinitions(
        IServiceProvider serviceProvider,
        string resourceTypeName,
        string? apiVersion = null);
    }
}
