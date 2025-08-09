// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Integration;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace AzureMcp.Communication.LiveTests;

[Trait("Area", "Communication")]
[Trait("Category", "Live")]
public class CommunicationCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
{
    [Fact]
    public async Task Should_SendSms_WithValidParameters()
    {
        Skip.If(string.IsNullOrEmpty(Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING")),
            "Communication Services connection string not available for live testing");

        var connectionString = Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING");
        var fromPhone = Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_FROM_PHONE");
        var toPhone = Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_TO_PHONE");

        Skip.If(string.IsNullOrEmpty(fromPhone), "From phone number not configured for live testing");
        Skip.If(string.IsNullOrEmpty(toPhone), "To phone number not configured for live testing");

        var result = await CallToolAsync(
            "azmcp_communication_sms_send",
            new()
            {
                { "connection-string", connectionString },
                { "from", fromPhone },
                { "to", new[] { toPhone } },
                { "message", "Test SMS from Azure MCP Live Test" },
                { "enable-delivery-report", true },
                { "tag", "live-test" }
            });

        Assert.Equal(200, result.GetProperty("status").GetInt32());

        var resultsProperty = result.AssertProperty("results");
        Assert.Equal(JsonValueKind.Array, resultsProperty.ValueKind);

        foreach (var smsResult in resultsProperty.EnumerateArray())
        {
            Assert.True(smsResult.TryGetProperty("messageId", out _));
            Assert.True(smsResult.TryGetProperty("successful", out _));
            Assert.True(smsResult.TryGetProperty("httpStatusCode", out _));
            Assert.True(smsResult.TryGetProperty("to", out _));
        }
    }

    [Theory]
    [InlineData("--invalid-connection-string test")]
    [InlineData("--connection-string")]
    [InlineData("--connection-string cs --from")]
    [InlineData("--connection-string cs --from +1234567890")]
    [InlineData("--connection-string cs --from +1234567890 --to +1987654321")]
    public async Task Should_Return400_WithInvalidInput(string args)
    {
        var result = await CallToolAsync(
            "azmcp_communication_sms_send",
            new()
            {
                { "args", args }
            });

        Assert.NotEqual(200, result.GetProperty("status").GetInt32());
        var message = result.GetProperty("message").GetString()!;
        Assert.True(message.Contains("required", StringComparison.OrdinalIgnoreCase) ||
                   message.Contains("validation", StringComparison.OrdinalIgnoreCase));
    }
}