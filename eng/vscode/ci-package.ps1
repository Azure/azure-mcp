param(
    $os,
    $Version,
    $PackageArguments
)

# Map input to vsce target naming
$targetMap = @{
    "windows_x64"   = "win32-x64"
    "windows_arm64" = "win32-arm64"
    "linux_x64"     = "linux-x64"
    "linux_arm64"   = "linux-arm64"
    "macOS_x64"     = "darwin-x64"
    "macOS_arm64"   = "darwin-arm64"
}

if (-not $targetMap.ContainsKey($os)) {
    Write-Error "Unknown OS: $os. Valid options: $($targetMap.Keys -join ', ')"
    exit 1
}

$target = $targetMap[$os]
$vsixName = "vscode-azure-mcp-extension-$target-$Version.vsix"
$ignoreFile = ".vscodeignore"

# Build the vsce package command
$vsceCmd = "vsce package --target $target --out $vsixName --ignoreFile $ignoreFile"
if ($PackageArguments) {
    $vsceCmd += " $PackageArguments"
}

# Run the packaging step
Invoke-Expression $vsceCmd

# Clean up node_modules
Remove-Item -Recurse -Force node_modules