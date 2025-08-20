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

# The bash script below follows the official documentation from .NET AOT Team.
# https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/cross-compile#linux

$bashScript = @"
sudo dpkg --add-architecture arm64

sudo bash -c 'cat > /etc/apt/sources.list.d/arm64.list <<EOF
deb [arch=arm64] http://ports.ubuntu.com/ubuntu-ports/ jammy main restricted
deb [arch=arm64] http://ports.ubuntu.com/ubuntu-ports/ jammy-updates main restricted
deb [arch=arm64] http://ports.ubuntu.com/ubuntu-ports/ jammy-backports main restricted universe multiverse
EOF'

sudo sed -i -e 's/deb http/deb [arch=amd64] http/g' /etc/apt/sources.list
sudo sed -i -e 's/deb mirror/deb [arch=amd64] mirror/g' /etc/apt/sources.list

sudo apt update
sudo apt install -y clang llvm binutils-aarch64-linux-gnu gcc-aarch64-linux-gnu zlib1g-dev:arm64
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