using System.CommandLine;

namespace AzureMcp.Areas.AzureIsv.Datadog.Options;

public static class DatadogOptionDefinitions
{
    public const string DatadogResourceParam = "datadog-resource";

    public static readonly Option<string> DatadogResourceName = new(
        $"--{DatadogResourceParam}",
        "The name of the Datadog resource to use. This is the unique name you chose for your Datadog resource in Azure."
    )
    {
        IsRequired = true
    };
}
