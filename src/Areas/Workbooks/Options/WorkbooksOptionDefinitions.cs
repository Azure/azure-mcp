// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Workbooks.Options;

public static class WorkbooksOptionDefinitions
{
    public const string MessageText = "message";
    public const string WorkbookIdText = "workbook-id";
    public const string TitleText = "title";
    public const string SerializedContentText = "serialized-content";
    public const string SourceIdText = "source-id";

    public static readonly Option<string> WorkbookId = new(
        $"--{WorkbookIdText}",
        "The Azure Resource ID of the workbook to retrieve."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Title = new(
        $"--{TitleText}",
        "The display name/title of the workbook."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> SerializedContent = new(
        $"--{SerializedContentText}",
        "The JSON serialized content/data of the workbook."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> SourceId = new(
        $"--{SourceIdText}",
        "The linked resource ID for the workbook. By default, this is 'azure monitor'."
    )
    {
        IsRequired = false,
    };

    // Command-specific variations for required fields
    public static readonly Option<string> TitleRequired = new(
        $"--{TitleText}",
        "The display name/title of the workbook.")
    {
        IsRequired = true
    };
    
    public static readonly Option<string> SerializedContentRequired = new(
        $"--{SerializedContentText}",
        "The serialized JSON content of the workbook.")
    {
        IsRequired = true
    };
}
