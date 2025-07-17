# Spelling Check Scripts

This directory contains scripts to run cspell (Code Spell Checker) on the repository using the dependencies defined in the adjacent `package*.json` files.

## Available Scripts

### PowerShell Version (Windows)
- **File**: `Invoke-Cspell.ps1`
- **Usage**: For Windows PowerShell environments

### Bash Version (Linux/macOS)
- **File**: `invoke-cspell.sh`
- **Usage**: For Linux and macOS bash environments

## Usage Examples

### PowerShell
```powershell
# Check all files (default)
./eng/common/spelling/Invoke-Cspell.ps1

# Check specific files
./eng/common/spelling/Invoke-Cspell.ps1 -ScanGlobs 'sdk/*/*/PublicAPI/**/*.md'

# Check multiple globs
./eng/common/spelling/Invoke-Cspell.ps1 -ScanGlobs @('sdk/storage/**', 'sdk/keyvault/**')

# Check single file
./eng/common/spelling/Invoke-Cspell.ps1 -ScanGlobs './README.md'
```

### Bash
```bash
# Check all files (default)
./eng/common/spelling/invoke-cspell.sh

# Check specific files
./eng/common/spelling/invoke-cspell.sh --scan-globs 'sdk/*/*/PublicAPI/**/*.md'

# Check multiple globs
./eng/common/spelling/invoke-cspell.sh --scan-globs 'sdk/storage/**' 'sdk/keyvault/**'

# Check single file
./eng/common/spelling/invoke-cspell.sh --scan-globs './README.md'

# Get help
./eng/common/spelling/invoke-cspell.sh --help
```

## Parameters

Both scripts support similar functionality:

- **Job Type**: The cspell command to run (default: `lint`)
- **Scan Globs**: File patterns to check for spelling
- **Config Path**: Location of the cspell.json configuration file
- **Spell Check Root**: Root directory for relative paths
- **Package Cache**: Working directory for npm dependencies
- **Leave Cache**: Option to preserve the npm package cache

## Requirements

- Node.js and npm must be installed
- The `.vscode/cspell.json` configuration file must exist
- `jq` command-line JSON processor (for bash version)

## How It Works

1. Creates a temporary working directory for npm packages
2. Copies `package.json` and `package-lock.json` to the working directory
3. Installs npm dependencies using `npm ci`
4. Modifies the cspell configuration to include specified file globs
5. Runs cspell with the modified configuration
6. Restores the original configuration
7. Cleans up temporary files

The scripts ensure that a LICENSE file (or temporary file) is always included in the scan to meet cspell's requirements for the "files" array.
