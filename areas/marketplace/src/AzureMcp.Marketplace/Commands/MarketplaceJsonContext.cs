// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Marketplace.Commands.Product;
using AzureMcp.Marketplace.Models;

namespace AzureMcp.Marketplace.Commands;

[JsonSerializable(typeof(ProductGetCommand.ProductGetCommandResult))]
[JsonSerializable(typeof(ProductDetails))]
[JsonSerializable(typeof(PlanDetails))]
[JsonSerializable(typeof(ArtifactDetails))]
[JsonSerializable(typeof(LinkDetails))]
[JsonSerializable(typeof(VideoDetails))]
[JsonSerializable(typeof(ImageGroupDetails))]
[JsonSerializable(typeof(ImageDetails))]
[JsonSerializable(typeof(FilterDetails))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class MarketplaceJsonContext : JsonSerializerContext;
