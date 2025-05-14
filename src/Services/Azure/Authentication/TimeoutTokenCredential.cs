// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;

public class TimeoutTokenCredential : TokenCredential
{
    private readonly TokenCredential _innerCredential;
    private readonly TimeSpan _timeout;

    public TimeoutTokenCredential(TokenCredential innerCredential, TimeSpan timeout)
    {
        _innerCredential = innerCredential;
        _timeout = timeout;
    }

    public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(_timeout);

        try
        {
            return _innerCredential.GetToken(requestContext, cts.Token);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            throw new TimeoutException($"Authentication timed out after {_timeout.TotalSeconds} seconds.");
        }
    }

    public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(_timeout);

        try
        {
            return await _innerCredential.GetTokenAsync(requestContext, cts.Token).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            throw new TimeoutException($"Authentication timed out after {_timeout.TotalSeconds} seconds.");
        }
    }
}
