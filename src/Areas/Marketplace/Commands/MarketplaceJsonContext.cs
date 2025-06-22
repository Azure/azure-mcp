// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Marketplace.Commands.Product;

namespace AzureMcp.Areas.Marketplace.Commands;

[JsonSerializable(typeof(ProductGetCommand.ProductGetCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class MarketplaceJsonContext : JsonSerializerContext
{
}