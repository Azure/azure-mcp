// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using AzureMcp.Services.Azure;

namespace AzureMcp.Areas.TerraformSchema.Services;


public class TerraformSchemaService() : BaseAzureService, ITerraformSchemaService
{
    public string GetResourceSchema(string resourceTypeName, string providerName, string workspacePath)
    {
        string fullSchemaJson = GetTerraformSchema(workspacePath);

        using var doc = JsonDocument.Parse(fullSchemaJson);

        var root = doc.RootElement;
        if (root.TryGetProperty("provider_schemas", out JsonElement providerSchemas))
        {
            foreach (var providerEntry in providerSchemas.EnumerateObject())
            {
                string providerNameInJson = providerEntry.Name;
                if (!providerNameInJson.Equals(providerName, StringComparison.OrdinalIgnoreCase))
                {
                    continue; // Skip if provider name does not match
                }
                var providerContent = providerEntry.Value;
                if (providerContent.TryGetProperty("resource_schemas", out var resourceSchemas))
                {
                    foreach (var resourceEntry in resourceSchemas.EnumerateObject())
                    {
                        string resourceTypeNameInJson = resourceEntry.Name;
                        if (!resourceTypeNameInJson.Equals(resourceTypeName, StringComparison.OrdinalIgnoreCase))
                        {
                            continue; // Skip if resource type name does not match
                        }
                        return resourceEntry.Value.GetRawText();
                    }
                }
            }
        }
        return $"Resource '{resourceTypeName}' not found in the schema.";
    }

    private string GetTerraformSchema(string workspacePath)
    {
        string schemaFilePath = Path.Combine(workspacePath, "terraform-schema.json");
        // How long to cache the schema file/ when to regenerate/ delete it
        if (File.Exists(schemaFilePath))
        {
            var fileInfo = new FileInfo(schemaFilePath);
            return File.ReadAllText(schemaFilePath);
        }
        //string tempPath = Path.GetTempPath(); comment for now
        string command = $"terraform providers schema -json > \"{workspacePath}\"";
        bool success = RunCommand("cmd.exe", "/c " + command, workspacePath);

        if (!success)
        {
            return string.Empty;
        }

        // Wait a moment for file system to sync
        Thread.Sleep(100);

        if (File.Exists(schemaFilePath))
        {
            try
            {
                string schemaContent = File.ReadAllText(schemaFilePath);
                if (!string.IsNullOrWhiteSpace(schemaContent))
                {
                    return schemaContent;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading schema file: {ex.Message}", ex);
            }
        }
        return $"Schema file '{schemaFilePath}' not found or empty. Ensure Terraform is installed and the command executed successfully.";
    }

    private bool RunCommand(string fileName, string arguments, string workingDirectory)
    {
        int timeoutMs = 20000;

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory,
                CreateNoWindow = true,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
            }
        };

        try
        {
            process.Start();

            if (!process.WaitForExit(timeoutMs))
            {
                process.Kill(true);
                return false;
            }

            return process.ExitCode == 0;
        }
        catch (Exception)
        {
            return false;
        }

    }
}

