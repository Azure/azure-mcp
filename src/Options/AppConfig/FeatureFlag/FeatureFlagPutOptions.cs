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
    public string? DisplayName { get; set; }

    [JsonPropertyName(OptionDefinitions.AppConfig.FeatureFlag.ConditionsName)]
    public string? Conditions { get; set; }

    [JsonPropertyName(OptionDefinitions.AppConfig.FeatureFlag.VariantsName)]
    public string? Variants { get; set; }

    [JsonPropertyName(OptionDefinitions.AppConfig.FeatureFlag.AllocationName)]
    public string? Allocation { get; set; }

    [JsonPropertyName(OptionDefinitions.AppConfig.FeatureFlag.TelemetryName)]
    public string? Telemetry { get; set; }
}
