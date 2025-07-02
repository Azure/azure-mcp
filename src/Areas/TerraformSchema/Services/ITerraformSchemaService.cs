// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.TerraformSchema.Services;
public interface ITerraformSchemaService
{
    string GetResourceSchema(
    string resourceTypeName,
    string providerName,
    string workspacePath);
}
