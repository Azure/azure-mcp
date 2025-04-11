// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments;

/// <summary>
/// Represents retry policy configuration for Azure SDK clients
/// </summary>
public class RetryPolicyArguments : IComparable<RetryPolicyArguments>, IEquatable<RetryPolicyArguments>
{
    [JsonPropertyName(ArgumentDefinitions.RetryPolicy.DelayName)]
    public double DelaySeconds { get; set; }

    [JsonPropertyName(ArgumentDefinitions.RetryPolicy.MaxDelayName)]
    public double MaxDelaySeconds { get; set; }

    [JsonPropertyName(ArgumentDefinitions.RetryPolicy.MaxRetriesName)]
    public int MaxRetries { get; set; }

    [JsonPropertyName(ArgumentDefinitions.RetryPolicy.ModeName)]
    public RetryMode Mode { get; set; }

    [JsonPropertyName(ArgumentDefinitions.RetryPolicy.NetworkTimeoutName)]
    public double NetworkTimeoutSeconds { get; set; }

    /// <summary>
    /// Compares this retry policy with another policy to check if all settings match
    /// </summary>
    /// <param name="other">The retry policy to compare with</param>
    /// <returns>True if both policies have identical settings or are both null, false otherwise</returns>
    public static bool AreEqual(RetryPolicyArguments? policy1, RetryPolicyArguments? policy2)
    {
        if (ReferenceEquals(policy1, policy2))
        {
            return true;
        }

        if (policy1 == null || policy2 == null)
        {
            return false;
        }

        return policy1.MaxRetries == policy2.MaxRetries &&
               policy1.Mode == policy2.Mode &&
               policy1.DelaySeconds == policy2.DelaySeconds &&
               policy1.MaxDelaySeconds == policy2.MaxDelaySeconds &&
               policy1.NetworkTimeoutSeconds == policy2.NetworkTimeoutSeconds;
    }

    public int CompareTo(RetryPolicyArguments? other)
    {
        if (other == null) return 1;

        // Compare by MaxRetries first
        var retryComparison = MaxRetries.CompareTo(other.MaxRetries);
        if (retryComparison != 0) return retryComparison;

        // Then by Mode
        var modeComparison = Mode.CompareTo(other.Mode);
        if (modeComparison != 0) return modeComparison;

        // Then by delay settings
        var delayComparison = DelaySeconds.CompareTo(other.DelaySeconds);
        if (delayComparison != 0) return delayComparison;

        var maxDelayComparison = MaxDelaySeconds.CompareTo(other.MaxDelaySeconds);
        if (maxDelayComparison != 0) return maxDelayComparison;

        // Finally by network timeout
        return NetworkTimeoutSeconds.CompareTo(other.NetworkTimeoutSeconds);
    }

    public bool Equals(RetryPolicyArguments? other) => AreEqual(this, other);

    public override bool Equals(object? obj) => obj is RetryPolicyArguments other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(MaxRetries, Mode, DelaySeconds, MaxDelaySeconds, NetworkTimeoutSeconds);

    public static bool operator ==(RetryPolicyArguments? left, RetryPolicyArguments? right) => AreEqual(left, right);

    public static bool operator !=(RetryPolicyArguments? left, RetryPolicyArguments? right) => !(left == right);

    public static bool operator <(RetryPolicyArguments? left, RetryPolicyArguments? right) =>
        left is null ? right is not null : left.CompareTo(right) < 0;

    public static bool operator <=(RetryPolicyArguments? left, RetryPolicyArguments? right) =>
        left is null || left.CompareTo(right) <= 0;

    public static bool operator >(RetryPolicyArguments? left, RetryPolicyArguments? right) => !(left <= right);

    public static bool operator >=(RetryPolicyArguments? left, RetryPolicyArguments? right) => !(left < right);
}