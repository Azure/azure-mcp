using AzureMCP.Models;
using System.Text.Json.Serialization;

namespace AzureMCP.Arguments.AppConfig;

public class BaseAppConfigArguments : BaseArgumentsWithSubscription
{
    [JsonPropertyName(ArgumentDefinitions.AppConfig.AccountName)]
    public string? Account { get; set; }
}