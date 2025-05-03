// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Tests;

public static class SkipExtensions
{
    public const string RunningFromDotnetTestReason =
        "Test skipped when running from dotnet test. This test requires interactive environment.";

    public static bool IsRunningFromDotnetTest()
    {
        bool isVsCode = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("VSCODE_CLI")) ||
                       !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("VSCODE_PID")) ||
                       !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("VSCODE_CWD"));

        if (isVsCode)
        {
            return false;
        }

        // Check for environment variables that indicate we're running from dotnet test
        return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("VSTEST_HOST_DEBUG")) ||
               !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DOTNET_HOST_PATH"));
    }
}
