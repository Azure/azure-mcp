// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Storage.Models;

public record BatchSetTierResult(List<string> SuccessfulBlobs, List<string> FailedBlobs);