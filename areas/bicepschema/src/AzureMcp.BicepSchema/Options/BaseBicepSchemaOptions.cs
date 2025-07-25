// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.BicepSchema.Options
{
    public class BaseBicepSchemaOptions : SubscriptionOptions
    {
        [JsonPropertyName(BicepSchemaOptionDefinitions.ResourceType)]
        public string? ResourceType { get; set; }
    }
}
