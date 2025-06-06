using AzureMcp.Commands;
using AzureMcp.Commands.Server.Tools;
using ModelContextProtocol.Client;

public class McpCommandGroup : IMcpClientProvider
{
    public McpCommandGroup(CommandGroup commandGroup)
    {
        _commandGroup = commandGroup ?? throw new ArgumentNullException(nameof(commandGroup));
    }

    private readonly CommandGroup _commandGroup;

    public async Task<IMcpClient> CreateClientAsync()
    {
        var arguments = new[] { "server", "start", "--service", _commandGroup.Name };
        var transportOptions = new StdioClientTransportOptions
        {
            Name = _commandGroup.Name,
            Command = "azmcp",
            Arguments = arguments,
        };

        var clientTransport = new StdioClientTransport(transportOptions);
        var clientOptions = new McpClientOptions { };
        return await McpClientFactory.CreateAsync(clientTransport, clientOptions);
    }

    public ClientMetadata CreateMetadata()
    {
        return new ClientMetadata
        {
            Id = _commandGroup.Name,
            Name = _commandGroup.Name,
            Description = _commandGroup.Description
        };
    }
}
