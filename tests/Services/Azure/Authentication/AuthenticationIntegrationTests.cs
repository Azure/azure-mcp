// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Identity;
using Azure.Identity.Broker;
using AzureMCP.Models.Argument;
using AzureMCP.Services.Azure.Subscription;
using AzureMCP.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace AzureMCP.Tests.Services.Azure.Authentication;

public class AuthenticationIntegrationTests : IAsyncLifetime
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISubscriptionService _subscriptionService;
    private readonly ITestOutputHelper _output;

    public AuthenticationIntegrationTests(ITestOutputHelper output)
    {
        _output = output;

        // Set up real service dependencies for integration test
        var services = new ServiceCollection();
        services.AddSingleton(Substitute.For<ICacheService>());
        services.AddSingleton(Substitute.For<ITenantService>());
        services.AddSingleton<ISubscriptionService, SubscriptionService>();

        _serviceProvider = services.BuildServiceProvider();
        _subscriptionService = _serviceProvider.GetRequiredService<ISubscriptionService>();
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;

    [SkipIfDotnetTestFact]
    public async Task LoginWithIdentityBroker_ThenListSubscriptions_ShouldSucceed()
    {
        // Test implementation remains unchanged since we handle skipping in the attribute
        _output.WriteLine("Testing InteractiveBrowserCredential with identity broker...");

        var browserCredential = await AuthenticateWithBrokerAsync();
        _output.WriteLine("Successfully authenticated with identity broker");

        // Step 2: Now test the subscription service which will use our CustomChainedCredential internally
        _output.WriteLine("Testing subscription listing with authenticated credential...");

        var subscriptions = await _subscriptionService.GetSubscriptions();
        ValidateAndLogSubscriptions(subscriptions);
    }

    private static async Task<TokenCredential> AuthenticateWithBrokerAsync()
    {
        var browserCredential = new InteractiveBrowserCredential(
            new InteractiveBrowserCredentialBrokerOptions(WindowHandleProvider.GetWindowHandle())
        );

        // Verify the credential works by requesting a token
        var armScope = "https://management.azure.com/.default";
        var context = new TokenRequestContext([armScope]);
        var token = await browserCredential.GetTokenAsync(context);

        Assert.NotNull(token.Token);
        Assert.NotEqual(default, token.ExpiresOn);

        return browserCredential;
    }

    private void ValidateAndLogSubscriptions(List<ArgumentOption> subscriptions)
    {
        // Validate subscriptions
        Assert.NotNull(subscriptions);
        Assert.NotEmpty(subscriptions);

        // Verify subscription data structure
        foreach (var subscription in subscriptions)
        {
            Assert.NotNull(subscription.Id);
            Assert.NotEmpty(subscription.Id);
            Assert.NotNull(subscription.Name);
            Assert.NotEmpty(subscription.Name);
        }

        // Output subscriptions for manual verification
        var jsonString = JsonSerializer.Serialize(subscriptions, new JsonSerializerOptions { WriteIndented = true });
        _output.WriteLine($"Retrieved {subscriptions.Count} subscriptions:");
        _output.WriteLine(jsonString);
    }
}