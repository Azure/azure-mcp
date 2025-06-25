using System.Text.Json.Serialization;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

/// <summary>
/// Options for listing Azure AI Foundry projects
/// </summary>
public class ProjectListOptions : SubscriptionOptions
{
    // ResourceGroup is already available from base SubscriptionOptions via GlobalOptions
} 