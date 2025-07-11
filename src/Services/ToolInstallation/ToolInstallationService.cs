// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Runtime.InteropServices;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Services.ToolInstallation;

/// <summary>
/// Service for installing and managing external tools like kubectl
/// </summary>
public class ToolInstallationService(ILogger<ToolInstallationService> logger) : IToolInstallationService
{
    private readonly ILogger<ToolInstallationService> _logger = logger;
    private const string KubectlVersion = "v1.31.0"; // Current stable version

    public async Task<string?> FindKubectlAsync()
    {
        await Task.CompletedTask; // Make method properly async
        var kubectlName = GetKubectlExecutableName();

        // Check PATH first
        var pathEnv = Environment.GetEnvironmentVariable("PATH");
        if (!string.IsNullOrEmpty(pathEnv))
        {
            foreach (var path in pathEnv.Split(Path.PathSeparator))
            {
                var kubectlPath = Path.Combine(path.Trim(), kubectlName);
                if (File.Exists(kubectlPath))
                {
                    _logger.LogDebug("Found kubectl at: {KubectlPath}", kubectlPath);
                    return kubectlPath;
                }
            }
        }

        // Check common installation locations
        var commonPaths = GetCommonKubectlPaths();
        foreach (var commonPath in commonPaths)
        {
            var kubectlPath = Path.Combine(commonPath, kubectlName);
            if (File.Exists(kubectlPath))
            {
                _logger.LogDebug("Found kubectl at common location: {KubectlPath}", kubectlPath);
                return kubectlPath;
            }
        }

        _logger.LogDebug("kubectl not found in PATH or common locations");
        return null;
    }
    public async Task<string?> InstallKubectlAsync()
    {
        try
        {
            _logger.LogInformation("kubectl not found. Attempting to install kubectl version {Version}...", KubectlVersion);

            var installDir = GetKubectlInstallDirectory();
            Directory.CreateDirectory(installDir);

            var kubectlPath = Path.Combine(installDir, GetKubectlExecutableName());

            // Skip if already exists
            if (File.Exists(kubectlPath))
            {
                _logger.LogInformation("kubectl already installed at: {KubectlPath}", kubectlPath);
                return kubectlPath;
            }

            var downloadUrl = GetKubectlDownloadUrl();
            _logger.LogInformation("Downloading kubectl from: {DownloadUrl}", downloadUrl);

            using var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(5); // Set a reasonable timeout
            var response = await httpClient.GetAsync(downloadUrl);
            response.EnsureSuccessStatusCode();

            await using var fileStream = File.Create(kubectlPath);
            await response.Content.CopyToAsync(fileStream);            // Set executable permissions on Unix-like systems
            if (!OperatingSystem.IsWindows())
            {
                SetExecutablePermissions(kubectlPath);
            }

            // Validate the installation by checking version
            if (await ValidateKubectlInstallation(kubectlPath))
            {
                _logger.LogInformation("kubectl installed successfully at: {KubectlPath}", kubectlPath);
                _logger.LogInformation("You can add {InstallDir} to your PATH to use kubectl system-wide.", installDir);
                return kubectlPath;
            }
            else
            {
                _logger.LogError("kubectl installation validation failed");
                File.Delete(kubectlPath); // Clean up invalid installation
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to install kubectl. Error: {ErrorMessage}", ex.Message);
            return null;
        }
    }

    public async Task<string?> EnsureKubectlAsync(bool autoInstall = false)
    {
        var kubectlPath = await FindKubectlAsync();

        if (kubectlPath != null)
        {
            return kubectlPath;
        }

        if (!autoInstall)
        {
            return null;
        }

        return await InstallKubectlAsync();
    }

    private static string GetKubectlExecutableName()
    {
        return OperatingSystem.IsWindows() ? "kubectl.exe" : "kubectl";
    }

    private static string[] GetCommonKubectlPaths()
    {
        if (OperatingSystem.IsWindows())
        {
            return
            [
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "kubectl"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".kube"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kubectl")
            ];
        }
        else
        {
            return
            [
                "/usr/local/bin",
                "/usr/bin",
                "/opt/kubectl/bin",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local", "bin"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".kube")
            ];
        }
    }
    private static string GetKubectlInstallDirectory()
    {
        if (OperatingSystem.IsWindows())
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localAppData, "azure-mcp", "tools");
        }
        else
        {
            var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(homeDir, ".local", "share", "azure-mcp", "tools");
        }
    }

    private static string GetKubectlDownloadUrl()
    {
        var (os, arch) = GetPlatformInfo();
        return $"https://dl.k8s.io/release/{KubectlVersion}/bin/{os}/{arch}/kubectl{(OperatingSystem.IsWindows() ? ".exe" : "")}";
    }

    private static (string os, string arch) GetPlatformInfo()
    {
        string os;
        string arch;

        if (OperatingSystem.IsWindows())
        {
            os = "windows";
        }
        else if (OperatingSystem.IsLinux())
        {
            os = "linux";
        }
        else if (OperatingSystem.IsMacOS())
        {
            os = "darwin";
        }
        else
        {
            throw new PlatformNotSupportedException("Unsupported operating system for kubectl installation");
        }

        arch = RuntimeInformation.ProcessArchitecture switch
        {
            Architecture.X64 => "amd64",
            Architecture.Arm64 => "arm64",
            Architecture.X86 => "386",
            _ => throw new PlatformNotSupportedException($"Unsupported architecture: {RuntimeInformation.ProcessArchitecture}")
        };

        return (os, arch);
    }
    private static void SetExecutablePermissions(string filePath)
    {
        try
        {
            // Use chmod to set executable permissions (Unix/Linux/macOS)
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "chmod",
                    Arguments = $"+x \"{filePath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
        }
        catch (Exception ex)
        {
            // Log but don't fail - the file might still be executable
            Console.WriteLine($"Warning: Failed to set executable permissions: {ex.Message}");
        }
    }

    private async Task<bool> ValidateKubectlInstallation(string kubectlPath)
    {
        try
        {
            _logger.LogDebug("Validating kubectl installation at: {KubectlPath}", kubectlPath);

            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = kubectlPath,
                    Arguments = "version --client --output=json",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            process.Start();

            var outputTask = process.StandardOutput.ReadToEndAsync();
            var errorTask = process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            var output = await outputTask;
            var error = await errorTask;

            if (process.ExitCode == 0 && !string.IsNullOrEmpty(output))
            {
                _logger.LogDebug("kubectl validation successful");
                return true;
            }
            else
            {
                _logger.LogWarning("kubectl validation failed. Exit code: {ExitCode}, Error: {Error}", process.ExitCode, error);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating kubectl installation");
            return false;
        }
    }
}
