// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Commands.LoadTesting.LoadTestResource;
using AzureMcp.Commands.LoadTesting.LoadTestRun;

namespace AzureMcp.Commands.LoadTesting;

[JsonSerializable(typeof(TestResourceListCommand.TestResourceListCommandResult))]
[JsonSerializable(typeof(LoadTestRunGetCommand.LoadTestRunGetCommandResult))]
[JsonSerializable(typeof(LoadTestRunCreateCommand.LoadTestRunCreateCommandResult))]
internal sealed partial class LoadTestJsonContext : JsonSerializerContext
{
}
