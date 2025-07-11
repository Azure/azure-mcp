// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Services.Interfaces;

/// <summary>
/// Service for installing and managing external tools
/// </summary>
public interface IToolInstallationService
{
    /// <summary>
    /// Checks if kubectl is available in PATH or common installation locations
    /// </summary>
    /// <returns>Path to kubectl executable if found, null otherwise</returns>
    Task<string?> FindKubectlAsync();

    /// <summary>
    /// Attempts to download and install kubectl for the current platform
    /// </summary>
    /// <returns>Path to installed kubectl executable, or null if installation failed</returns>
    Task<string?> InstallKubectlAsync();

    /// <summary>
    /// Checks if kubectl is installed and optionally installs it
    /// </summary>
    /// <param name="autoInstall">Whether to automatically install kubectl if not found</param>
    /// <returns>Path to kubectl executable, or null if not found/installed</returns>
    Task<string?> EnsureKubectlAsync(bool autoInstall = false);
}
