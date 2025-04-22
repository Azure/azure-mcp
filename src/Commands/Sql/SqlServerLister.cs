using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Sql;

namespace AzureMcp.Commands.Sql;

public sealed class SqlServerLister() : ISqlServerLister
{
    public static string GetResourceGroupName(string resourceId)
    {
        var parts = resourceId.Split('/');
        for (int i = 0; i < parts.Length - 1; i++)
        {
            if (parts[i].Equals("resourceGroups", System.StringComparison.OrdinalIgnoreCase) && i + 1 < parts.Length)
                return parts[i + 1];
        }
        return string.Empty;
    }

    public async Task<IReadOnlyList<object>> ListSqlServersAsync()
    {
        var credential = new DefaultAzureCredential();
        var armClient = new ArmClient(credential);
        var result = new List<object>();
        await foreach (var sub in armClient.GetSubscriptions().GetAllAsync())
        {
            var sqlServers = sub.GetSqlServers();
            foreach (var server in sqlServers)
            {
                result.Add(new
                {
                    Name = server.Data.Name,
                    Location = server.Data.Location.ToString(),
                    ResourceGroup = GetResourceGroupName(server.Id.ToString()),
                    FullyQualifiedDomainName = server.Data.FullyQualifiedDomainName
                });
            }
        }
        return result;
    }
}
