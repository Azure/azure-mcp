using AzureMcp.Commands.Server.Tools;
using ModelContextProtocol.Client;

public record RemoveMcpServerMetadata
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
}

public sealed class RemoteMcpServer(RemoveMcpServerMetadata metadata) : IMcpClientProvider
{
    private readonly RemoveMcpServerMetadata _metadata = metadata;

    public Task<IMcpClient> CreateClientAsync(McpClientOptions clientOptions)
    {
        var transportOptions = new SseClientTransportOptions()
        {
            Name = _metadata.Name,
            Endpoint = new Uri(_metadata.Endpoint),
            TransportMode = HttpTransportMode.AutoDetect,
        };

        var clientTransport = new SseClientTransport(transportOptions);
        return McpClientFactory.CreateAsync(clientTransport, clientOptions);
    }

    public McpServerMetadata CreateMetadata()
    {
        return new McpServerMetadata
        {
            Id = _metadata.Name,
            Name = _metadata.Name,
            Description = _metadata.Description,
        };
    }
}
