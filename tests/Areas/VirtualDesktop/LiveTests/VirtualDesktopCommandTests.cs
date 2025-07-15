// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Models;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Areas.VirtualDesktop.LiveTests;

[Trait("Area", "VirtualDesktop")]
public class VirtualDesktopCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_ListHostpools_WithSubscriptionId()
    {
        var result = await CallToolAsync(
            "azmcp-virtualdesktop-hostpool-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var hostpools = result.AssertProperty("hostpools");
        Assert.Equal(JsonValueKind.Array, hostpools.ValueKind);

        // Check results format if any hostpools exist
        foreach (var hostpool in hostpools.EnumerateArray())
        {
            Assert.True(hostpool.ValueKind == JsonValueKind.String);
            Assert.False(string.IsNullOrEmpty(hostpool.GetString()));
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_ListHostpools_WithSubscriptionName()
    {
        var result = await CallToolAsync(
            "azmcp-virtualdesktop-hostpool-list",
            new()
            {
                { "subscription", Settings.SubscriptionName }
            });

        var hostpools = result.AssertProperty("hostpools");
        Assert.Equal(JsonValueKind.Array, hostpools.ValueKind);

        // Check results format if any hostpools exist  
        foreach (var hostpool in hostpools.EnumerateArray())
        {
            Assert.True(hostpool.ValueKind == JsonValueKind.String);
            Assert.False(string.IsNullOrEmpty(hostpool.GetString()));
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_ListSessionHosts_WithSubscriptionId()
    {
        // First get available hostpools
        var hostpoolsResult = await CallToolAsync(
            "azmcp-virtualdesktop-hostpool-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var hostpools = hostpoolsResult.AssertProperty("hostpools");
        if (hostpools.GetArrayLength() > 0)
        {
            var firstHostpool = hostpools[0].GetString()!;
            
            var result = await CallToolAsync(
                "azmcp-virtualdesktop-hostpool-sessionhost-list",
                new()
                {
                    { "subscription", Settings.SubscriptionId },
                    { "hostpool-name", firstHostpool }
                });

            var sessionHosts = result.AssertProperty("sessionHosts");
            Assert.Equal(JsonValueKind.Array, sessionHosts.ValueKind);

            // Check results format if any session hosts exist
            foreach (var sessionHost in sessionHosts.EnumerateArray())
            {
                Assert.True(sessionHost.ValueKind == JsonValueKind.String);
                Assert.False(string.IsNullOrEmpty(sessionHost.GetString()));
            }
        }
        else
        {
            // Skip test if no hostpools are available
            Assert.True(true, "No hostpools available for testing session hosts");
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_ListSessionHosts_WithSubscriptionName()
    {
        // First get available hostpools
        var hostpoolsResult = await CallToolAsync(
            "azmcp-virtualdesktop-hostpool-list",
            new()
            {
                { "subscription", Settings.SubscriptionName }
            });

        var hostpools = hostpoolsResult.AssertProperty("hostpools");
        if (hostpools.GetArrayLength() > 0)
        {
            var firstHostpool = hostpools[0].GetString()!;
            
            var result = await CallToolAsync(
                "azmcp-virtualdesktop-hostpool-sessionhost-list",
                new()
                {
                    { "subscription", Settings.SubscriptionName },
                    { "hostpool-name", firstHostpool }
                });

            var sessionHosts = result.AssertProperty("sessionHosts");
            Assert.Equal(JsonValueKind.Array, sessionHosts.ValueKind);

            // Check results format if any session hosts exist
            foreach (var sessionHost in sessionHosts.EnumerateArray())
            {
                Assert.True(sessionHost.ValueKind == JsonValueKind.String);
                Assert.False(string.IsNullOrEmpty(sessionHost.GetString()));
            }
        }
        else
        {
            // Skip test if no hostpools are available
            Assert.True(true, "No hostpools available for testing session hosts");
        }
    }
}
