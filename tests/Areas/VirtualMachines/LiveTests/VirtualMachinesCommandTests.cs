// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Models.Command;
using AzureMcp.Tests.Common;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace AzureMcp.Tests.Areas.VirtualMachines.LiveTests;

[Trait("Area", "VirtualMachines")]
[Trait("Category", "Live")]
public class VirtualMachinesCommandTests : CommandTestsBase, IClassFixture<LiveTestFixture>
{
    protected const string TenantNameReason = "Service principals cannot use TenantName for lookup";
    protected LiveTestSettings Settings { get; }
    protected StringBuilder FailureOutput { get; } = new();
    protected ITestOutputHelper Output { get; }
    protected IMcpClient Client { get; }

    public VirtualMachinesCommandTests(LiveTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
        Client = fixture.Client;
        Settings = fixture.Settings;
        Output = output;
    }

    [Theory]
    [InlineData(AuthMethod.Credential)]
    public async Task Should_ListVirtualMachines_WithAuth(AuthMethod authMethod)
    {
        // Arrange
        var result = await CallToolAsync(
            "azmcp-virtualmachines-vm-list",
            new()
            {
                { "subscription", Settings.Subscription },
                { "auth-method", authMethod.ToString().ToLowerInvariant() }
            });

        // Assert
        var virtualMachines = result.AssertProperty("virtualMachines");
        Assert.Equal(JsonValueKind.Array, virtualMachines.ValueKind);

        // Check results format (array might be empty if no VMs exist)
        foreach (var vm in virtualMachines.EnumerateArray())
        {
            Assert.True(vm.TryGetProperty("name", out _));
            Assert.True(vm.TryGetProperty("resourceGroup", out _));
            Assert.True(vm.TryGetProperty("location", out _));
            Assert.True(vm.TryGetProperty("status", out _));
        }
    }

    [Theory]
    [InlineData(AuthMethod.Credential)]
    public async Task Should_ListVirtualMachines_WithResourceGroup_WithAuth(AuthMethod authMethod)
    {
        // Arrange
        var result = await CallToolAsync(
            "azmcp-virtualmachines-vm-list",
            new()
            {
                { "subscription", Settings.Subscription },
                { "resource-group", Settings.ResourceGroup },
                { "auth-method", authMethod.ToString().ToLowerInvariant() }
            });

        // Assert
        var virtualMachines = result.AssertProperty("virtualMachines");
        Assert.Equal(JsonValueKind.Array, virtualMachines.ValueKind);

        // Check results format (array might be empty if no VMs exist in this RG)
        foreach (var vm in virtualMachines.EnumerateArray())
        {
            Assert.True(vm.TryGetProperty("name", out _));
            Assert.True(vm.TryGetProperty("resourceGroup", out _));
            Assert.True(vm.TryGetProperty("location", out _));
            Assert.True(vm.TryGetProperty("status", out _));
            
            // Verify VM is in the requested resource group
            var vmResourceGroup = vm.GetProperty("resourceGroup").GetString();
            Assert.Equal(Settings.ResourceGroup, vmResourceGroup);
        }
    }

    [Theory]
    [InlineData("--invalid-param")]
    [InlineData("--subscription invalidSub")]
    public async Task Should_Return400_WithInvalidInput(string args)
    {
        var result = await CallToolAsync(
            $"azmcp-virtualmachines-vm-list {args}");

        Assert.Equal(400, result.GetProperty("status").GetInt32());
        Assert.Contains("required",
            result.GetProperty("message").GetString()!.ToLower());
    }
}