// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using AzureMcp.Arguments;
using Xunit;

namespace AzureMcp.Tests.Arguments;

public class RetryPolicyArgumentsTests
{
    [Fact]
    public void TestAreEqual()
    {
        // Arrange
        var policy1 = GetPolicy(3, RetryMode.Exponential, 1, 5, 30);
        var policy2 = GetPolicy(3, RetryMode.Exponential, 1, 5, 30);
        var policy3 = GetPolicy(123, RetryMode.Exponential, 1, 5, 30);

        // Assert
        Assert.True(RetryPolicyArguments.AreEqual(policy1, policy1));
        Assert.True(RetryPolicyArguments.AreEqual(policy1, policy2));
        Assert.True(RetryPolicyArguments.AreEqual(null, null));
        Assert.False(RetryPolicyArguments.AreEqual(policy1, null));
        Assert.False(RetryPolicyArguments.AreEqual(null, policy1));
        Assert.False(RetryPolicyArguments.AreEqual(policy1, policy3));
    }

    [Fact]
    public void TestInequality()
    {
        // Assert
        Assert.True(GetPolicy(3, RetryMode.Exponential, 1, 5, 30) != GetPolicy(999, RetryMode.Exponential, 1, 5, 30));
        Assert.True(GetPolicy(3, RetryMode.Exponential, 1, 5, 30) != GetPolicy(3, RetryMode.Fixed, 1, 5, 30));
        Assert.True(GetPolicy(3, RetryMode.Exponential, 1, 5, 30) != GetPolicy(3, RetryMode.Exponential, 999, 5, 30));
        Assert.True(GetPolicy(3, RetryMode.Exponential, 1, 5, 30) != GetPolicy(3, RetryMode.Exponential, 1, 999, 30));
        Assert.True(GetPolicy(3, RetryMode.Exponential, 1, 5, 30) != GetPolicy(3, RetryMode.Exponential, 1, 5, 999));
    }

    [Fact]
    public void TestEqualityOperators()
    {
        // Arrange
        var policy1 = GetPolicy(3, RetryMode.Exponential, 1, 5, 30);
        var policy2 = GetPolicy(3, RetryMode.Exponential, 1, 5, 30);

        // Assert
        Assert.True(policy1 == policy2);
        Assert.False(policy1 != policy2);
    }

    private static RetryPolicyArguments GetPolicy(int maxRetries, RetryMode mode, double delay, double maxDelay, double timeout)
    {
        return new RetryPolicyArguments
        {
            MaxRetries = maxRetries,
            Mode = mode,
            DelaySeconds = delay,
            MaxDelaySeconds = maxDelay,
            NetworkTimeoutSeconds = timeout
        };
    }
}