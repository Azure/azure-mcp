// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Models;

public record QueueMessageSendResult(
    [property: JsonPropertyName("messageId")] string MessageId,
    [property: JsonPropertyName("insertionTime")] DateTimeOffset InsertionTime,
    [property: JsonPropertyName("expirationTime")] DateTimeOffset ExpirationTime,
    [property: JsonPropertyName("popReceipt")] string PopReceipt,
    [property: JsonPropertyName("nextVisibleTime")] DateTimeOffset NextVisibleTime,
    [property: JsonPropertyName("message")] string Message
);
