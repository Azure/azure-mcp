// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.LoadTesting.Commands.LoadTest;
using AzureMcp.LoadTesting.Commands.LoadTestResource;
using AzureMcp.LoadTesting.Commands.LoadTestRun;
using AzureMcp.LoadTesting.Models.LoadTest;
using AzureMcp.LoadTesting.Models.LoadTestRun;

namespace AzureMcp.LoadTesting.Commands;

[JsonSerializable(typeof(TestResourceListCommand.TestResourceListCommandResult))]
[JsonSerializable(typeof(TestRunGetCommand.TestRunGetCommandResult))]
[JsonSerializable(typeof(TestRunCreateCommand.TestRunCreateCommandResult))]
[JsonSerializable(typeof(TestRunListCommand.TestRunListCommandResult))]
[JsonSerializable(typeof(TestRunUpdateCommand.TestRunUpdateCommandResult))]
[JsonSerializable(typeof(TestGetCommand.TestGetCommandResult))]
[JsonSerializable(typeof(TestResourceCreateCommand.TestResourceCreateCommandResult))]
[JsonSerializable(typeof(TestCreateCommand.TestCreateCommandResult))]
[JsonSerializable(typeof(TestRun))]
[JsonSerializable(typeof(TestRunRequest))]
[JsonSerializable(typeof(TestRequestPayload))]
[JsonSerializable(typeof(Test))]
internal sealed partial class LoadTestJsonContext : JsonSerializerContext;
