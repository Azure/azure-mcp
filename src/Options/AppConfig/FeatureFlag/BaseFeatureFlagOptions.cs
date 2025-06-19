// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.AppConfig.FeatureFlag;

public class BaseFeatureFlagOptions : BaseAppConfigOptions
{
    [JsonPropertyName(OptionDefinitions.AppConfig.FeatureFlag.FeatureFlagNameString)]
    public string? FeatureFlagName { get; set; }

    [JsonPropertyName(OptionDefinitions.AppConfig.LabelName)]
    public string? Label { get; set; }
}
