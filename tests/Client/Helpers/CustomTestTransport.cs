// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using ModelContextProtocol.Protocol.Messages;
using ModelContextProtocol.Protocol.Transport;
using System.Threading.Channels;

namespace AzureMcp.Tests.Client.Helpers;

public class CustomTestTransport : ITransport
{
    private readonly Channel<IJsonRpcMessage> _messageChannel;

    public bool IsConnected { get; set; }

    public ChannelReader<IJsonRpcMessage> MessageReader => _messageChannel;

    public List<IJsonRpcMessage> SentMessages { get; } = [];

    public Action<IJsonRpcMessage>? MessageListener { get; set; }

    public CustomTestTransport()
    {
        _messageChannel = Channel.CreateUnbounded<IJsonRpcMessage>(new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = true,
        });
        IsConnected = true;
    }

    public ValueTask DisposeAsync()
    {
        _messageChannel.Writer.TryComplete();
        IsConnected = false;
        return ValueTask.CompletedTask;
    }

    public async Task SendMessageAsync(IJsonRpcMessage message, CancellationToken cancellationToken = default)
    {
        SentMessages.Add(message);
        if (message is JsonRpcRequest request)
        {
            await WriteMessageAsync(request, cancellationToken);
            MessageListener?.Invoke(message);
        }
        else if (message is JsonRpcNotification notification)
        {
            await WriteMessageAsync(notification, cancellationToken);
            MessageListener?.Invoke(message);
        }
        else if (message is JsonRpcResponse response)
        {
            MessageListener?.Invoke(message);
        }
        else
        {
            throw new NotSupportedException($"Message type {message.GetType()} is not supported.");
        }
    }

    private async Task WriteMessageAsync(IJsonRpcMessage message, CancellationToken cancellationToken = default)
    {
        await _messageChannel.Writer.WriteAsync(message, cancellationToken);
    }
}