// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Extension.Services;

public interface IExtensionService
{
    /// <summary>
    /// Calls a REST API to generate an Azure CLI command for the given intent.
    /// </summary>
    /// <param name="intent">The description of the goal to accomplish using Azure CLI.</param>
    /// <returns>The recommended Azure CLI command as a string.</returns>
    Task<string> GenerateAzCommandAsync(string intent);
}
