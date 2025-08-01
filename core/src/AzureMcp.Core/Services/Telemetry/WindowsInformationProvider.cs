using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace AzureMcp.Core.Services.Telemetry;

[SupportedOSPlatform("windows")]
internal class WindowsInformationProvider(ILogger<WindowsInformationProvider> logger)
    : MachineInformationProviderBase(logger)
{
    // Construct the parts necessary to cache the ids in the registry.
    // The final path is HKCU/SOFTWARE/Microsoft/DeveloperTools
    private const RegistryHive Hive = RegistryHive.CurrentUser;
    private const string RegistryPathRoot = $"SOFTWARE\\{MicrosoftDirectory}\\{DeveloperToolsDirectory}";

    private readonly ILogger<WindowsInformationProvider> _logger = logger;

    public override Task<string?> GetOrCreateDeviceId()
    {
        return Task.Run(() =>
        {
            try
            {
                if (TryGetRegistryValue(RegistryPathRoot, DeviceId, out var existingDeviceId))
                {
                    return existingDeviceId;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to fetch {Key} value from {RegistryRoot}.", DeviceId, RegistryPathRoot);
            }

            var newDeviceId = GenerateDeviceId();

            try
            {
                if (TrySetRegistryValue(RegistryPathRoot, DeviceId, newDeviceId))
                {
                    return newDeviceId;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to persist {Key} in {RegistryPath}.", DeviceId, RegistryPathRoot);
            }

            return newDeviceId;
        });
    }

    private static bool TryGetRegistryValue(string registryRoot, string keyName, out string value)
    {
        using var registry = RegistryKey.OpenBaseKey(Hive, RegistryView.Registry64);
        using var key = registry.OpenSubKey(registryRoot);

        if (key == null)
        {
            value = string.Empty;
            return false;
        }

        var matchingKeyName = key.GetValueNames().SingleOrDefault(x => string.Equals(x, keyName), null);
        if (matchingKeyName != null)
        {
            var existingValue = key.GetValue(matchingKeyName)?.ToString();
            value = existingValue ?? string.Empty;

            return !string.IsNullOrEmpty(existingValue);
        }

        value = string.Empty;
        return true;
    }

    private static bool TrySetRegistryValue(string registryRoot, string keyName, string value)
    {
        using var registry = RegistryKey.OpenBaseKey(Hive, RegistryView.Registry64);
        using var key = registry.OpenSubKey(registryRoot);

        if (key == null)
        {
            return false;
        }

        key.SetValue(keyName, value, RegistryValueKind.String);

        return true;
    }
}
