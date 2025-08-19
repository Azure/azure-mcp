// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Storage.Models;

public record QueueMessageSendResult(
    string MessageId,
    DateTimeOffset InsertionTime,
    DateTimeOffset ExpirationTime,
    string PopReceipt,
    DateTimeOffset NextVisibleTime,
    string Message
);
