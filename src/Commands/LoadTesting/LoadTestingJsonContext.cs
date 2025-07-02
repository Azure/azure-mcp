// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Commands.LoadTesting.LoadTestResource;
using AzureMcp.Commands.LoadTesting.LoadTestRun;

namespace AzureMcp.Commands.LoadTesting;

[JsonSerializable(typeof(TestResourceListCommand.TestResourceListCommandResult))]
[JsonSerializable(typeof(TestRunGetCommand.TestRunGetCommandResult))]
[JsonSerializable(typeof(TestRunCreateCommand.TestRunCreateCommandResult))]
[JsonSerializable(typeof(TestRunListCommand.TestRunListCommandResult))]
[JsonSerializable(typeof(TestRunUpdateCommand.TestRunUpdateCommandResult))]
internal sealed partial class LoadTestJsonContext : JsonSerializerContext
{
}
