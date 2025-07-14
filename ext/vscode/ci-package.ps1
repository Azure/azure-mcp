
param(
    [string]$os,
    $PackageArguments
)

# # Define OS targets and corresponding RIDs
# $osMap = @{
#     "windows_x64"     = "win-x64"
#     "windows_arm64"   = "win-arm64"
#     "linux_x64"       = "linux-x64"
#     "linux_arm64"     = "linux-arm64"
#     "macOS_x64"       = "osx-x64"
#     "macOS_arm64"     = "osx-arm64"
# }

# if (-not $osMap.ContainsKey($os)) {
#     Write-Error "Unknown OS: $os. Valid options: $($osMap.Keys -join ', ')"
#     exit 1
# }
# # Define project and destination base paths
# $projectPath = Resolve-Path "../../src"
# $dstBase = Join-Path $PSScriptRoot "server"


# # Build and package for the specified OS only
# $dstDir = Join-Path $dstBase $os
# if (!(Test-Path $dstDir)) {
#     New-Item -ItemType Directory -Path $dstDir | Out-Null
# }
# dotnet publish $projectPath -c Release -r $($osMap[$os]) --self-contained true -o $dstDir

# Run the npm packaging step
Invoke-Expression "npm run ci-package -- $PackageArguments"
