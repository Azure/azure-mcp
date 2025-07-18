// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Reflection;
using AzureMcp.Helpers;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Caching;

namespace AzureMcp.Areas.AzureArc.Services
{
    public class ArcService : BaseAzureService, IArcServices
    {
        private const string PrerequisitesAksEdgeInstallation = "PrerequisitesAksEdgeInstallation.txt";
        private const string RemoveAksEdgeCompletely = "RemoveAksEdgeCompletely.ps1";
        private const string ValidateAndSetupSystemRequirements = "ValidateAndSetupSystemRequirements.ps1";
        private const string ValidateAndSetupSwRequirementScript = "ValidateAndSetupSoftwarerequirements.ps1";
        private const string AksEdgeQuickDeploy = "AksEdgeQuickDeploy.ps1";
        private const string DisconnectFromAzureArc = "DisconnectFromAzureArc.ps1";
        private const string ConfirmAksEdgeDeletion = "ConfirmAksEdgeDeletion.ps1";
        private const string OnboardClusterToArc = "OnboardClusterToArc.ps1";
        private readonly Assembly _assembly;

        public ArcService(ITenantService? tenantService = null) : base(tenantService)
        {
            _assembly = typeof(ArcService).Assembly;
        }

        public async Task<DeploymentResult> OnboardClusterToArcAsync(string clusterName, string resourceGroupName, string location, string subscriptionId, string tenantId, string kubeConfigPath, string userProvidedPath)
        {
            string tempScriptPath = Path.Combine(userProvidedPath, "OnboardClusterToArc.ps1");
            File.WriteAllText(tempScriptPath, LoadResourceFiles(OnboardClusterToArc));

            var arguments = $"-ExecutionPolicy Bypass -File \"{tempScriptPath}\" -Force";

            if (!string.IsNullOrEmpty(clusterName))
                arguments += $" -ClusterName {clusterName}";
            if (!string.IsNullOrEmpty(resourceGroupName))
                arguments += $" -ResourceGroupName {resourceGroupName}";
            if (!string.IsNullOrEmpty(subscriptionId))
                arguments += $" -SubscriptionId {subscriptionId}";
            if (!string.IsNullOrEmpty(tenantId))
                arguments += $" -TenantId {tenantId}";
            if (!string.IsNullOrEmpty(location))
                arguments += $" -Location {location}";
            if (!string.IsNullOrEmpty(kubeConfigPath))
                arguments += $" -KubeConfigPath \"{kubeConfigPath}\"";

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = arguments,
                UseShellExecute = true,
                Verb = "runas"
            };

            Process process = StartProcess(tempScriptPath, processStartInfo);
            await Task.Run(() => process.WaitForExit());

            return new DeploymentResult
            {
                Success = process.ExitCode == 0,
                Steps = process.ExitCode == 0 ? "Arc Onboarding completed successfully." : "Arc Onboarding failed."
            };

        }
        public async Task<DeploymentResult> DisconnectFromAzureArcAsync(string resourceGroupName, string clusterName, string userProvidedPath)
        {
            string tempScriptPath = Path.Combine(userProvidedPath, "DisconnectFromAzureArc.ps1");
            File.WriteAllText(tempScriptPath, LoadResourceFiles(DisconnectFromAzureArc));

            var arguments = $"-ExecutionPolicy Bypass -File \"{tempScriptPath}\" -Force";

            if (!string.IsNullOrEmpty(resourceGroupName))
                arguments += $" -ResourceGroupName {resourceGroupName}";
            if (!string.IsNullOrEmpty(clusterName))
                arguments += $" -ClusterName {clusterName}";

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = arguments,
                UseShellExecute = true,
                Verb = "runas"
            };

            Process process = StartProcess(tempScriptPath, processStartInfo);
            await Task.Run(() => process.WaitForExit());

            return new DeploymentResult
            {
                Success = process.ExitCode == 0,
                Steps = process.ExitCode == 0 ? "Disconnected from Azure Arc successfully." : "Failed to disconnect from Azure Arc."
            };
        }
        public async Task<bool> RemoveAksEdgeAsync(string userProvidedPath)
        {
            try
            {
                // Extract the embedded script to a temporary file
                string tempScriptPath = Path.Combine(userProvidedPath, "RemoveAksEdgeCompletely.ps1");
                File.WriteAllText(tempScriptPath, LoadResourceFiles(RemoveAksEdgeCompletely));

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-ExecutionPolicy Bypass -File \"{tempScriptPath}\" -Force",
                    UseShellExecute = true,
                    Verb = "runas"
                };

                using var process = StartProcess(tempScriptPath, processStartInfo);
                await process.WaitForExitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during AKS Edge removal: {ex.Message}");
            }

            try
            {
                // Extract the confirmation script to a temporary file
                string tempConfirmScriptPath = Path.Combine(userProvidedPath, "ConfirmAksEdgeDeletion.ps1");
                File.WriteAllText(tempConfirmScriptPath, LoadResourceFiles(ConfirmAksEdgeDeletion));

                var confirmProcessStartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-ExecutionPolicy Bypass -File \"{tempConfirmScriptPath}\" -Force",
                    UseShellExecute = true,
                    Verb = "runas"
                };

                using var confirmProcess = StartProcess(tempConfirmScriptPath, confirmProcessStartInfo);
                await confirmProcess.WaitForExitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during confirmation script execution: {ex.Message}");
            }

            return true;
        }
        public string LoadResourceFiles(string resourceName)
        {
            string actualResourceName = EmbeddedResourceHelper.FindEmbeddedResource(_assembly, resourceName);
            return EmbeddedResourceHelper.ReadEmbeddedResource(_assembly, actualResourceName);
        }

        public async Task<DeploymentResult> ValidatePrerequisitesForAksEdgeClusterAsync()
        {
            string prerequisiteSteps = await Task.Run(() => LoadResourceFiles(PrerequisitesAksEdgeInstallation));
            return new DeploymentResult
            {
                Success = true,
                Steps = prerequisiteSteps
            };
        }

        public async Task<DeploymentResult> ValidateSystemRequirementsAndSetupHyperVAsync(string userProvidedPath)
        {
            // Ensure the directory exists
            if (!Directory.Exists(userProvidedPath))
            {
                Directory.CreateDirectory(userProvidedPath);
            }

            string tempScriptPath = Path.Combine(userProvidedPath, "ValidateAndSetupSystemRequirements.ps1");
            File.WriteAllText(tempScriptPath, LoadResourceFiles(ValidateAndSetupSystemRequirements));

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-ExecutionPolicy Bypass -File \"{tempScriptPath}\" -Force",
                UseShellExecute = true,
                Verb = "runas"
            };

            using var process = StartProcess(tempScriptPath, processStartInfo);
            if (process == null)
            {
                throw new InvalidOperationException("Failed to start the system requirements validation script.");
            }

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"Script execution failed with exit code {process.ExitCode}.");
            }

            return new DeploymentResult
            {
                Success = true,
                Steps = "System requirements validated and Hyper-V setup successfully."
            };
        }

        public async Task<DeploymentResult> ValidateAndInstallSwRequirementAsync(string userProvidedPath)
        {
            // Ensure the directory exists
            if (!Directory.Exists(userProvidedPath))
            {
                Directory.CreateDirectory(userProvidedPath);
            }

            string tempScriptPath = Path.Combine(userProvidedPath, "ValidateAndSetupSoftwarerequirements.ps1");
            File.WriteAllText(tempScriptPath, LoadResourceFiles(ValidateAndSetupSwRequirementScript));

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-ExecutionPolicy Bypass -File \"{tempScriptPath}\" -Force",
                UseShellExecute = true,
                Verb = "runas"
            };

            using var process = StartProcess(tempScriptPath, processStartInfo);
            if (process == null)
            {
                throw new InvalidOperationException("Failed to start the software requirements validation and installation script.");
            }

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"Script execution failed with exit code {process.ExitCode}.");
            }

            return new DeploymentResult
            {
                Success = true,
                Steps = "Software requirements validated and installed successfully."
            };
        }

        public async Task<DeploymentResult> QuickDeployAksEdgeEssentialsAsync(string clusterName, string resourceGroupName, string subscriptionId, string tenantId, string location, string userProvidedPath)
        {
            // Ensure the directory exists
            if (!Directory.Exists(userProvidedPath))
            {
                Directory.CreateDirectory(userProvidedPath);
            }

            string tempScriptPath = Path.Combine(userProvidedPath, "AksEdgeQuickDeploy.ps1");
            File.WriteAllText(tempScriptPath, LoadResourceFiles(AksEdgeQuickDeploy));

            var arguments = $"-ExecutionPolicy Bypass -File \"{tempScriptPath}\" -Force";

            if (!string.IsNullOrEmpty(clusterName))
                arguments += $" -ClusterName {clusterName}";
            if (!string.IsNullOrEmpty(resourceGroupName))
                arguments += $" -ResourceGroupName {resourceGroupName}";
            if (!string.IsNullOrEmpty(subscriptionId))
                arguments += $" -SubscriptionId {subscriptionId}";
            if (!string.IsNullOrEmpty(tenantId))
                arguments += $" -TenantId {tenantId}";
            if (!string.IsNullOrEmpty(location))
                arguments += $" -Location {location}";

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = arguments,
                UseShellExecute = true,
                Verb = "runas"
            };

            Process process = StartProcess(tempScriptPath, processStartInfo);
            await Task.Run(() => process.WaitForExit());

            return new DeploymentResult
            {
                Success = process.ExitCode == 0,
                Steps = process.ExitCode == 0 ? "Deployment completed successfully." : "Deployment failed."
            };
        }
        public Process StartProcess(string scriptPath, ProcessStartInfo processInfo)
        {
            if (!File.Exists(scriptPath))
            {
                throw new FileNotFoundException("The specified script file was not found.", scriptPath);
            }

            var process = Process.Start(processInfo);
            if (process == null)
            {
                throw new InvalidOperationException("Failed to start the process.");
            }

            return process;
        }
    }

    public class DeploymentResult
    {
        public bool Success { get; set; }
        public string Steps { get; set; } = string.Empty;
        public string OutputPath { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
