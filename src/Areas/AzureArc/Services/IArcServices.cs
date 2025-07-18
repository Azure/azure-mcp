// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using AzureMcp.Options;

namespace AzureMcp.Areas.AzureArc.Services;

public interface IArcServices
{
    string LoadResourceFiles(string resourceName);
    Process StartProcess(string scriptPath, ProcessStartInfo processInfo);
    Task<bool> RemoveAksEdgeAsync(string userProvidedPath);
    Task<DeploymentResult> ValidatePrerequisitesForAksEdgeClusterAsync();
    Task<DeploymentResult> OnboardClusterToArcAsync(string clusterName, string resourceGroupName, string location, string subscriptionId, string tenantId, string kubeConfigPath, string userProvidedPath);
    Task<DeploymentResult> DisconnectFromAzureArcAsync(string resourceGroupName, string clusterName, string userProvidedPath);
    Task<DeploymentResult> ValidateSystemRequirementsAndSetupHyperVAsync(string userProvidedPath);
    Task<DeploymentResult> ValidateAndInstallSwRequirementAsync(string userProvidedPath);
    Task<DeploymentResult> QuickDeployAksEdgeEssentialsAsync(string clusterName, string resourceGroupName, string subscriptionId, string tenantId, string location, string userProvidedPath);
}
