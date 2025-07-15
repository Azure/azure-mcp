// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager;
using AzureMcp.Options;

namespace AzureMcp.Services.Azure;

public partial class AzureClientService
{
    /// <summary>
    /// Creates an Azure Resource Manager client with the specified token credential and options
    /// </summary>
    /// <param name="tokenCredential">The token credential to use for authentication</param>
    /// <param name="options">The ARM client options including retry policy</param>
    /// <returns>An ArmClient instance</returns>
    public virtual ArmClient GetArmClient(TokenCredential tokenCredential, ArmClientOptions options)
    {
        return new ArmClient(tokenCredential, default, options);
    }
}