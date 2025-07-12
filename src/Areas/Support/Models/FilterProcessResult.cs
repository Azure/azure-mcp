// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Support.Models;

public record FilterProcessResult
{
    public bool IsSuccess { get; init; }
    public string ProcessedFilter { get; init; } = string.Empty;
    public string ErrorMessage { get; init; } = string.Empty;

    public static FilterProcessResult Success(string processedFilter) => 
        new() { IsSuccess = true, ProcessedFilter = processedFilter };

    public static FilterProcessResult Error(string errorMessage) => 
        new() { IsSuccess = false, ErrorMessage = errorMessage };
}

public record PropertyProcessResult
{
    public bool IsSuccess { get; init; }
    public bool RequiresReplacement { get; init; }
    public string ReplacementValue { get; init; } = string.Empty;
    public string ErrorMessage { get; init; } = string.Empty;

    public static PropertyProcessResult Success(string replacementValue) => 
        new() { IsSuccess = true, RequiresReplacement = true, ReplacementValue = replacementValue };

    public static PropertyProcessResult NoChange() => 
        new() { IsSuccess = true, RequiresReplacement = false };

    public static PropertyProcessResult Error(string errorMessage) => 
        new() { IsSuccess = false, ErrorMessage = errorMessage };
}
