using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureMcp.Configuration;

public class AzureMcpServerConfiguration(string name, string version)
{
    public string Name { get; } = name;
    public string Version { get; } = version;
}
