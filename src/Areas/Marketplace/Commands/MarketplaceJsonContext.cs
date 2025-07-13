// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Marketplace.Commands.Product;
using AzureMcp.Areas.Marketplace.Models;

namespace AzureMcp.Areas.Marketplace.Commands;

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

