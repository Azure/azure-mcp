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

$bashScript = @'
#!/usr/bin/env bash
set -euo pipefail

echo "Installing arm64 cross-compilation toolchain"

# turning on multi-arch support for arm64 on amd64 host
sudo dpkg --add-architecture arm64 || true

# Constrains the existing binary package repositories to provide only amd64 packages
sudo sed -i 's/^deb \([^[].*\)/deb [arch=amd64] \1/' /etc/apt/sources.list
# Constrains the existing source package repositories to provide only amd64 sources
sudo sed -i 's/^deb-src \([^[].*\)/deb-src [arch=amd64] \1/' /etc/apt/sources.list

# Adds package repositories that provide arm64 packages from ports.ubuntu.com
sudo tee /etc/apt/sources.list.d/arm64.list >/dev/null <<'EOF'
deb [arch=arm64] http://ports.ubuntu.com/ubuntu-ports/ jammy main restricted universe multiverse
deb [arch=arm64] http://ports.ubuntu.com/ubuntu-ports/ jammy-updates main restricted universe multiverse
deb [arch=arm64] http://ports.ubuntu.com/ubuntu-ports/ jammy-security main restricted universe multiverse
deb [arch=arm64] http://ports.ubuntu.com/ubuntu-ports/ jammy-backports main restricted universe multiverse
EOF

# Refresh package indices for both amd64 and arm64 architectures
sudo apt-get update -qq

# Find the 'gcc base package' name (there is only one 'gcc base package' name per Ubuntu series across all architectures)
GCC_BASE=$(apt-cache search --names-only '^gcc-[0-9]+-base$' | awk '{print $1}' | sort -V | tail -1)

if [[ -z "$GCC_BASE" ]]; then
  echo "ERROR: Could not determine gcc base package." >&2
  exit 1
fi

# Find versions of 'gcc base package' for amd64 and arm64
amd64_ver=$(apt-cache madison ${GCC_BASE}:amd64 | awk '{print $3}' | head -1)
arm64_ver=$(apt-cache madison ${GCC_BASE}:arm64 | awk '{print $3}' | head -1)

if [[ -z "$amd64_ver" && -z "$arm64_ver" ]]; then
  echo "ERROR: No candidate versions found for ${GCC_BASE} on either arch." >&2
  exit 1
fi

# Choose a common version (prefer matching; otherwise lowest common available)
if [[ -n "$amd64_ver" && "$amd64_ver" == "$arm64_ver" ]]; then
  common_ver="$amd64_ver"
else
  common_ver=$(printf "%s\n%s\n" "$amd64_ver" "$arm64_ver" | sed '/^$/d' | sort -V | head -1)
fi

echo "Using ${GCC_BASE} version: ${common_ver} (amd64=${amd64_ver:-N/A}, arm64=${arm64_ver:-N/A})"

# # Install gcc base libraries for both amd64 and arm64 with versions pinned
sudo DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
  ${GCC_BASE}:amd64=${common_ver} \
  ${GCC_BASE}:arm64=${common_ver} \
  libgcc-s1:amd64=${common_ver} \
  libgcc-s1:arm64=${common_ver}

# Install libc6 and rest of toolchain
sudo DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
  libc6:arm64 \
  clang \
  llvm \
  lld \
  binutils-aarch64-linux-gnu \
  gcc-aarch64-linux-gnu \
  zlib1g-dev:arm64

# Verification step
dpkg -l | grep -E '^(ii)\s+(libc6|libgcc-s1|gcc-[0-9]+-base):arm64' || true

'@

try {
    Write-Host "Installing arm64 cross-compilation toolchain..." -ForegroundColor Green
    bash -lc $bashScript

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
