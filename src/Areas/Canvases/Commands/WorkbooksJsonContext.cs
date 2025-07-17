// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Canvases.Commands.Workbook;
using AzureMcp.Areas.Canvases.Models;

namespace AzureMcp.Areas.Canvases.Commands;

[JsonSerializable(typeof(object))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(WorkbookInfo))]
[JsonSerializable(typeof(ListWorkbooksCommand.ListWorkbooksCommandResult))]
[JsonSerializable(typeof(ShowWorkbooksCommand.ShowWorkbooksCommandResult))]
[JsonSerializable(typeof(UpdateWorkbooksCommand.UpdateWorkbooksCommandResult))]
[JsonSerializable(typeof(CreateWorkbooksCommand.CreateWorkbooksCommandResult))]
[JsonSerializable(typeof(DeleteWorkbooksCommand.DeleteWorkbooksCommandResult))]
internal partial class WorkbooksJsonContext : JsonSerializerContext
{
}
