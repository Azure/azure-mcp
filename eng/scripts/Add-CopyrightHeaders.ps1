#!/bin/env pwsh
#Requires -Version 7

# Script to add copyright headers to all files
param(
    [switch]$Force
)

$copyrightText = "Copyright (c) Microsoft Corporation."
$licenseText = "Licensed under the MIT License."

# Define comment style patterns
$commentStyles = @{
    'doubleslash' = @{
        extensions = @('.bicep', '.cpp', '.cs', '.dart', '.fs', '.glsl', '.go', '.groovy', 
                      '.java', '.js', '.kt', '.ll', '.mm', '.php', '.rs', '.scala', '.ts')
        style = @{
            prefix = "//"
            multi = $false
        }
    }
    'hash' = @{
        extensions = @('.cmake', '.coffee', '.jl', '.pl', '.ps1', '.py', '.r', '.rb', '.sh', 
                      '.yaml', '.yml', 'Makefile')
        style = @{
            prefix = "#"
            multi = $false
        }
    }
    'dash' = @{
        extensions = @('.lua', '.sql')
        style = @{
            prefix = "--"
            multi = $false
        }
    }
    'percent' = @{
        extensions = @('.matlab', '.tex')
        style = @{
            prefix = "%"
            multi = $false
        }
    }
    'semicolon' = @{
        extensions = @('.el')
        style = @{
            prefix = ";;"
            multi = $false
        }
    }
    'xml' = @{
        extensions = @('.html', '.md', '.xml')
        style = @{
            prefix = "<!--"
            suffix = "-->"
            multi = $true
        }
    }
    'c-style' = @{
        extensions = @('.c', '.h')
        style = @{
            prefix = "/*"
            middle = " *"
            suffix = " */"
            multi = $true
        }
    }
    'haskell' = @{
        extensions = @('.hs')
        style = @{
            prefix = "{-"
            suffix = "-}"
            multi = $true
        }
    }
    'ocaml' = @{
        extensions = @('.ml')
        style = @{
            prefix = "(*"
            suffix = "*)"
            multi = $true
        }
    }
    'batch' = @{
        extensions = @('.bat', '.cmd')
        style = @{
            prefix = "::"
            multi = $false
        }
    }
}

# Create extension to style lookup for faster access
$extensionStyles = @{}
foreach ($styleName in $commentStyles.Keys) {
    $style = $commentStyles[$styleName]
    foreach ($ext in $style.extensions) {
        $extensionStyles[$ext] = $style.style
    }
}

function Get-FileExtension {
    param (
        [string]$filePath
    )
    
    if ($filePath -match "Makefile$") {
        return "Makefile"
    }
    
    # Special handling for Matlab files
    if ($filePath -match "\.m$") {
        # Check if it's a Matlab file by looking for Matlab-specific keywords
        $content = Get-Content $filePath -Raw
        if ($content -match "function\s+|classdef\s+|^\s*%") {
            return ".matlab"
        }
        # Otherwise assume it's an Objective-C file
        return ".mm"  # Changed from .m to .mm for clarity
    }
    
    return [System.IO.Path]::GetExtension($filePath).ToLower()
}

function Get-CopyrightHeader {
    param (
        [string]$filePath
    )

    $ext = Get-FileExtension $filePath
    $style = $extensionStyles[$ext]

    if (-not $style) {
        Write-Warning "No comment style defined for extension: $ext"
        return $null
    }

    if ($style.multi) {
        if ($style.middle) {
            # C-style multi-line comment
            return @"
$($style.prefix)
$($style.middle) $copyrightText
$($style.middle) $licenseText
$($style.suffix)


"@
        }
        else {
            # HTML/XML-style or other multi-line comment
            return @"
$($style.prefix) $copyrightText
$($style.prefix) $licenseText $($style.suffix)


"@
        }
    }
    else {
        # Single-line comment style
        return @"
$($style.prefix) $copyrightText
$($style.prefix) $licenseText


"@
    }
}

Get-ChildItem -Path $PSScriptRoot\..\..\src, $PSScriptRoot\..\..\tests -Recurse -File | ForEach-Object {
    # Skip auto-generated files
    if ($_.FullName -like "*\obj\*" -or $_.FullName -like "*\bin\*") {
        Write-Host "Skipping generated file $($_.FullName)"
        return
    }

    $content = Get-Content $_.FullName -Raw
    if ([string]::IsNullOrEmpty($content)) {
        Write-Host "Skipping empty file $($_.FullName)"
        return
    }

    $header = Get-CopyrightHeader $_.FullName
    if (-not $header) {
        Write-Host "Skipping unsupported file type: $($_.FullName)"
        return
    }

    $ext = Get-FileExtension $_.FullName
    $style = $extensionStyles[$ext]
    
    # Check if file already has a copyright header
    $hasHeader = $false
    $lines = $content -split "`n"
    foreach ($line in $lines) {
        $trimmedLine = $line.TrimStart()
        if ($trimmedLine.Contains($copyrightText)) {
            $hasHeader = $true
            break
        }
    }

    if ($hasHeader -and -not $Force) {
        Write-Host "Copyright header already exists in $($_.FullName)"
        # Skip files that already have the header unless Force is specified
        return
    }

    if ($hasHeader -and $Force) {
        Write-Host "Forcing update of copyright header in $($_.FullName)"
        # Remove existing header while preserving shebang lines
        $newLines = @()
        $foundCopyright = $false
        $preserveShebang = $false
        $blankLineRemoved = $false
        
        for ($i = 0; $i -lt $lines.Count; $i++) {
            $line = $lines[$i]
            $trimmedLine = $line.TrimStart()
            
            # Always preserve shebang lines
            if ($trimmedLine -match '^#!' -and -not $preserveShebang) {
                $newLines += $line
                $preserveShebang = $true
                continue
            }
            
            # Look for copyright
            if ($trimmedLine.Contains($copyrightText)) {
                $foundCopyright = $true
                # Skip this line and next license line
                $i++ # Skip license line
                # Skip any blank lines after the header
                while (($i + 1) -lt $lines.Count -and [string]::IsNullOrWhiteSpace($lines[$i + 1].TrimStart())) {
                    $i++
                    $blankLineRemoved = $true
                }
                continue
            }
            
            if (-not $foundCopyright) {
                $newLines += $line
            }
            else {
                # Only add the line if we've found the copyright and processed any trailing blanks
                $newLines += $line
            }
        }
        
        $content = $newLines -join "`n"
    }

    # Add the new header (which includes its own blank line)
    if ($lines[0] -match '^#!' -and -not $hasHeader) {
        $shebangLine = $lines[0] + "`n"
        $content = $content.Substring($lines[0].Length).TrimStart()
        $newContent = $shebangLine + $header + $content
    }
    else {
        $content = $content.TrimStart()
        $newContent = $header + $content
    }

    Set-Content -Path $_.FullName -Value $newContent -NoNewline
    Write-Host "Updated copyright header in $($_.FullName)"
}