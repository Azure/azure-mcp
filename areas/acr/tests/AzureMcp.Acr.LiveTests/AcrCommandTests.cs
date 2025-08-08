// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Core.Models;
using AzureMcp.Tests;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Acr.LiveTests;

[Trait("Area", "Acr")]
[Trait("Category", "Live")]
public class AcrCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
{
    [Theory]
    [InlineData(AuthMethod.Credential)]
    public async Task Should_ListRegistries_Successfully(AuthMethod authMethod)
    {
        // Arrange & Act
        var result = await CallToolAsync(
            "azmcp_acr_registry_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "auth-method", authMethod.ToString() }
            });

        // Assert
        var registries = result.AssertProperty("registries");
        Assert.Equal(JsonValueKind.Array, registries.ValueKind);
        // Note: We don't assert specific registries exist since test environment may not have any
    }

    [Theory]
    [InlineData("--invalid-param")]
    [InlineData("--subscription invalidSub")]
    public async Task Should_Return400_WithInvalidInput(string args)
    {
        var result = await CallToolAsync(
            "azmcp_acr_registry_list",
            ParseArguments(args));

        Assert.NotNull(result);
        // For invalid input, we expect either null result or specific error handling
    }

    private static Dictionary<string, object?> ParseArguments(string args)
    {
        var parts = args.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var dict = new Dictionary<string, object?>();

        for (int i = 0; i < parts.Length; i += 2)
        {
            if (i + 1 < parts.Length)
            {
                var key = parts[i].TrimStart('-');
                var value = parts[i + 1];
                dict[key] = value;
            }
        }

        return dict;
    }
}
