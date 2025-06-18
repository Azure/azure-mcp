using System.CommandLine;

namespace AzureMcp.Areas.Authorization.Options;

public static class AuthorizationOptionDefinitions
{
    public const string ScopeName = "scope";

    public static readonly Option<string> Scope = new(
        $"--{ScopeName}",
        "Scope at which the role assignment or definition applies to, e.g., /subscriptions/0b1f6471-1bf0-4dda-aec3-111122223333, /subscriptions/0b1f6471-1bf0-4dda-aec3-111122223333/resourceGroups/myGroup, or /subscriptions/0b1f6471-1bf0-4dda-aec3-111122223333/resourceGroups/myGroup/providers/Microsoft.Compute/virtualMachines/myVM."
    )
    {
        IsRequired = true,
    };
}
