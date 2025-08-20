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

# turning on multi-arch support for arm64 on amd64 host
sudo dpkg --add-architecture arm64 || true

# Constrains the existing binary (and optionally source) package repositories to provide only amd64 packages,
# this ensures only amd64 stuffs are pulled from 'archive.ubuntu.com' and 'security.ubuntu.com'
sudo sed -i -E 's/^deb ([^[])/deb [arch=amd64] \1/' /etc/apt/sources.list
sudo sed -i -E 's/^deb-src ([^[])/deb-src [arch=amd64] \1/' /etc/apt/sources.list

# Adds package repositories that provide arm64 packages,
# this ensures the arm64 stuffs are pulled from 'ports.ubuntu.com'.
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

# Find 'gcc base package' candidate versions in both architectures
amd64_list=$(apt-cache madison "${GCC_BASE}:amd64" | awk '{print $3}')
arm64_list=$(apt-cache madison "${GCC_BASE}:arm64" | awk '{print $3}')

amd64_gcc_versions=$(printf "%s\n" "$amd64_list" | sort -V)
arm64_gcc_versions=$(printf "%s\n" "$arm64_list" | sort -V)

# Use the highest common version of 'gcc base package'
gcc_version=$(
  comm -12 \
    <(printf "%s\n" "$amd64_gcc_versions") \
    <(printf "%s\n" "$arm64_gcc_versions") \
  | tail -1
)

if [[ -z "$gcc_version" ]]; then
  echo "ERROR: No common ${GCC_BASE} version across amd64 and arm64." >&2
  echo "amd64 candidates:" >&2; printf "%s\n" "$amd64_gcc_versions" >&2
  echo "arm64 candidates:" >&2; printf "%s\n" "$arm64_gcc_versions" >&2
  exit 1
fi

echo "Using ${GCC_BASE} version: ${gcc_version} (selected from amd64 and arm64 candidates)"

# Install gcc base libraries for both amd64 and arm64 with versions pinned
# Note: 'libgcc-s1' and 'gcc-*-base' are 'Multi-Arch:same', which means they must be installed at the exact same version across all enabled architectures
sudo DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
  ${GCC_BASE}:amd64=${gcc_version} \
  ${GCC_BASE}:arm64=${gcc_version} \
  libgcc-s1:amd64=${gcc_version} \
  libgcc-s1:arm64=${gcc_version}

# Install libc6 and rest of toolchain
# Since we already installed and pinned 'libgcc-s1:arm64' to match the 'libgcc-s1:amd64' version, version alignment is guaranteed and 'libc6:arm64' can now be installed safely.
sudo DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
  libc6:arm64 \
  clang \
  llvm \
  lld \
  binutils-aarch64-linux-gnu \
  gcc-aarch64-linux-gnu \
  zlib1g-dev:arm64

# Verification step
# output the essential arm64 cross-compilation packages (libc6, libgcc-s1, gcc-*-base) installed on the amd64 host
# Note: In dpkg -l output, the first two characters indicate the package status: the desired state (first i = install requested) and the current state (second i = package is installed)
dpkg -l | grep -E '^(ii)\s+(libc6|libgcc-s1|gcc-[0-9]+-base):arm64' || true

'@

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