using System.Text.Json;
using AzureMcp.Areas.VirtualDesktop.Commands;
using AzureMcp.Areas.VirtualDesktop.Commands.SessionHost;
using AzureMcp.Areas.VirtualDesktop.Models;

var sessionHosts = new List<SessionHost>
{
    new() { Name = "test1", Status = "Available" },
    new() { Name = "test2", Status = "Available" }
};

var result = new SessionHostListCommand.SessionHostListCommandResult(sessionHosts);

try
{
    var json = JsonSerializer.Serialize(result, VirtualDesktopJsonContext.Default.SessionHostListCommandResult);
    Console.WriteLine("Serialization successful:");
    Console.WriteLine(json);
}
catch (Exception ex)
{
    Console.WriteLine($"Serialization failed: {ex}");
}
