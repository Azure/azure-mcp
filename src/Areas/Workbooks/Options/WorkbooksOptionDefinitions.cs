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

    public static readonly Option<string> Message = new(
        $"--{MessageText}",
        "The message text to display in the hello world command."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> WorkbookId = new(
        $"--{WorkbookIdText}",
        "The Azure resource ID of the workbook to retrieve."
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
        "The serialized content/data of the workbook."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> SourceId = new(
        $"--{SourceIdText}",
        "The source ID for the workbook."
    )
    {
        IsRequired = false
    };
}
