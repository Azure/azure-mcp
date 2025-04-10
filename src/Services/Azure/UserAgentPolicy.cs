// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Core.Pipeline;

namespace AzureMCP.Services.Azure;

public class UserAgentPolicy : HttpPipelineSynchronousPolicy
{
    public const string UserAgentHeader = "User-Agent";

    private readonly string _userAgent;

    public UserAgentPolicy(string userAgent)
    {
        if (string.IsNullOrWhiteSpace(userAgent))
        {
            throw new ArgumentException("User agent cannot be empty", nameof(userAgent));
        }
        _userAgent = userAgent;
    }

    public override void OnSendingRequest(HttpMessage message)
    {
        message.Request.Headers.SetValue(UserAgentHeader, _userAgent);

        base.OnSendingRequest(message);
    }
}