#!/bin/env pwsh
#Requires -Version 7

[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'

$runtime = [System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier
$parts = $runtime.Split('-')
$os = $parts[0]
$arch = $parts[1]

if ($os -ne 'linux') {
    Write-Host "Skipping arm64 cross-compilation toolchain installation on non-Linux host (runtime: $runtime)" -ForegroundColor Yellow
    return
}

if ($arch -ne 'x64') {
    Write-Host "Skipping arm64 cross-compilation toolchain installation on non-x64 Linux host (runtime: $runtime)" -ForegroundColor Yellow
    return
}

$bashScript = @"
# x64 host building AOT targeting arm64 need cross-compilation toolchain.
sudo apt-get update
sudo apt-get install -y gcc-aarch64-linux-gnu g++-aarch64-linux-gnu libc6-dev-arm64-cross binutils-aarch64-linux-gnu

# make the generic 'objcopy' point to the 'objcopy' from the arm64 cross-compilation toolchain.
sudo ln -sf /usr/bin/aarch64-linux-gnu-objcopy /usr/local/bin/objcopy

echo "arm64 cross-compilation toolchain installed successfully"
"@

try {
    Write-Host "Installing arm64 cross-compilation toolchain..." -ForegroundColor Green
    bash -c $bashScript
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "arm64 cross-compilation toolchain installation completed successfully" -ForegroundColor Green
    } else {
        throw "Bash script execution failed with exit code: $LASTEXITCODE"
    }
}
catch {
    Write-Error "Failed to install cross-compilation toolchain: $_"
    exit 1
}
