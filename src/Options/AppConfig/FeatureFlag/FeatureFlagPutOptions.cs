// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.AppConfig.FeatureFlag;

public class FeatureFlagPutOptions : BaseFeatureFlagOptions
{
    [JsonPropertyName(OptionDefinitions.AppConfig.FeatureFlag.EnabledName)]
    public bool? Enabled { get; set; }

    [JsonPropertyName(OptionDefinitions.AppConfig.FeatureFlag.DescriptionName)]
    public string? Description { get; set; }

    [JsonPropertyName(OptionDefinitions.AppConfig.FeatureFlag.DisplayNameName)]
    public string? DisplayName { get; set; }    [JsonPropertyName(OptionDefinitions.AppConfig.FeatureFlag.ConditionsName)]
    public string? Conditions { get; set; }
}
