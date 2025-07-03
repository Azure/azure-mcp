// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Models.LoadTesting.LoadTestResource;
public class TestResource
{
    public string? Id { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? Location { get; set; } = string.Empty;
    public string? DataPlaneUri { get; set; } = string.Empty;
    public string? ProvisioningState { get; set; } = string.Empty;
}
