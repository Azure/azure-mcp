# PowerShell script to check for unused properties in Services/Models that are not used in Models mapping
param(
    [string]$AreaName = "",
    [switch]$Verbose = $false
)

function Get-PropertyUsageFromMapping {
    param(
        [string]$ServiceFilePath,
        [string]$ServicesModelClassName
    )
    
    if (-not (Test-Path $ServiceFilePath)) {
        return @()
    }
    
    $content = Get-Content $ServiceFilePath -Raw
    
    # Handle special cases based on class name patterns
    if ($ServicesModelClassName -match "Properties$") {
        # For Properties classes, we need to find the specific variable that uses this Properties type
        # First, find the corresponding Data class name (e.g., SqlDatabaseProperties -> SqlDatabaseData)
        $dataClassName = $ServicesModelClassName -replace "Properties$", "Data"

        # Look for variable declaration pattern: DataType? variableName = DataType.FromJson(...)
        $variablePattern = "\b$dataClassName\?\s+(\w+)\s*=\s*$dataClassName\.FromJson"
        $variableMatch = [regex]::Match($content, $variablePattern)

        if (-not $variableMatch.Success) {
            # If no variable uses this Data class, then all Properties are unused
            return @()
        }

        $variableName = $variableMatch.Groups[1].Value

        # Look for nested property access: variableName.Properties?.PropertyName
        $propertyPattern = "\b$variableName\.Properties\?\.\s*([A-Z][a-zA-Z0-9_]*)"
        $propertyMatches = [regex]::Matches($content, $propertyPattern)
        
        $usedProperties = @()
        foreach ($match in $propertyMatches) {
            $propName = $match.Groups[1].Value
            if ($propName -notmatch '^(ToString|GetHashCode|Equals|GetType|Split|LastOrDefault)$') {
                $usedProperties += $propName
            }
        }

        return $usedProperties | Sort-Object -Unique
    }
    elseif ($ServicesModelClassName -eq "SqlSku") {
        # Look for Sku property access: variable.Sku.PropertyName
        $propertyPattern = "\w+\.Sku\.\s*([A-Z][a-zA-Z0-9_]*)"
        $propertyMatches = [regex]::Matches($content, $propertyPattern)
        
        $usedProperties = @()
        foreach ($match in $propertyMatches) {
            $propName = $match.Groups[1].Value
            if ($propName -notmatch '^(ToString|GetHashCode|Equals|GetType|Split|LastOrDefault)$') {
                $usedProperties += $propName
            }
        }
        
        return $usedProperties | Sort-Object -Unique
    }
    elseif ($ServicesModelClassName -eq "SqlElasticPoolPerDatabaseSettings") {
        # Look for PerDatabaseSettings property access: variable.Properties.PerDatabaseSettings.PropertyName
        $propertyPattern = "\w+\.Properties\.PerDatabaseSettings\.\s*([A-Z][a-zA-Z0-9_]*)"
        $propertyMatches = [regex]::Matches($content, $propertyPattern)
        
        $usedProperties = @()
        foreach ($match in $propertyMatches) {
            $propName = $match.Groups[1].Value
            if ($propName -notmatch '^(ToString|GetHashCode|Equals|GetType|Split|LastOrDefault)$') {
                $usedProperties += $propName
            }
        }
        
        return $usedProperties | Sort-Object -Unique
    }
    
    # For regular classes, look for variable assignment pattern: Type? variableName = Type.FromJson(...)
    $variablePattern = "\b$ServicesModelClassName\?\s+(\w+)\s*=\s*$ServicesModelClassName\.FromJson"
    $variableMatch = [regex]::Match($content, $variablePattern)
    
    if (-not $variableMatch.Success) {
        # If no variable uses this Services/Models class, then all its properties are unused
        return @()
    }
    
    $variableName = $variableMatch.Groups[1].Value
    
    # Look for usage of this variable's properties: variableName.PropertyName or variableName?.PropertyName
    $propertyPattern = "\b$variableName(?:\.|->|\?\.)\s*([A-Z][a-zA-Z0-9_]*)"
    $propertyMatches = [regex]::Matches($content, $propertyPattern)
    
    $usedProperties = @()
    foreach ($match in $propertyMatches) {
        $propName = $match.Groups[1].Value
        if ($propName -notmatch '^(ToString|GetHashCode|Equals|GetType|Split|LastOrDefault)$') {
            $usedProperties += $propName
        }
    }
    
    return $usedProperties | Sort-Object -Unique
}

function Get-PropertiesFromServicesModel {
    param(
        [string]$FilePath
    )
    
    if (-not (Test-Path $FilePath)) {
        return @()
    }
    
    $content = Get-Content $FilePath -Raw
    
    # Extract public properties using regex
    $propertyPattern = '\s*(?:public|internal)\s+[^{};]+?\s+([A-Z][a-zA-Z0-9_]*)\s*\{\s*get;?\s*set;?\s*\}'
    $propertyMatches = [regex]::Matches($content, $propertyPattern)
    
    $properties = @()
    foreach ($match in $propertyMatches) {
        $properties += $match.Groups[1].Value
    }
    
    return $properties | Sort-Object -Unique
}

function Check-AreaPropertyUsage {
    param(
        [string]$AreaPath
    )
    
    $areaName = Split-Path $AreaPath -Leaf
    Write-Host "`n=== Checking Area: $areaName ===" -ForegroundColor Cyan
    
    $srcPath = Join-Path $AreaPath "src"
    if (-not (Test-Path $srcPath)) {
        Write-Warning "No src folder found in $AreaPath"
        return
    }
    
    $projectFolders = Get-ChildItem $srcPath -Directory | Where-Object { $_.Name -like "AzureMcp.*" }
    
    if ($projectFolders.Count -eq 0) {
        Write-Warning "No AzureMcp project folder found in $srcPath"
        return
    }
    
    $projectPath = $projectFolders[0].FullName
    $servicesPath = Join-Path $projectPath "Services"
    $modelsPath = Join-Path $projectPath "Models"
    $servicesModelsPath = Join-Path $servicesPath "Models"
    
    if (-not (Test-Path $servicesModelsPath)) {
        Write-Host "No Services/Models folder found in $projectPath" -ForegroundColor Yellow
        return
    }
    
    if (-not (Test-Path $modelsPath)) {
        Write-Host "No Models folder found in $projectPath" -ForegroundColor Yellow
        return
    }
    
    # Find the service file
    $serviceFiles = Get-ChildItem $servicesPath -Filter "*Service.cs" | Where-Object { $_.Name -notlike "I*Service.cs" }
    if ($serviceFiles.Count -eq 0) {
        Write-Warning "No service implementation file found in $servicesPath"
        return
    }
    
    $serviceFilePath = $serviceFiles[0].FullName
    
    # Get all model files in the Models folder
    $modelFiles = Get-ChildItem $modelsPath -Filter "*.cs"
    
    $allUnusedProperties = @{}
    $totalServicesModelFiles = 0
    $totalUnusedProperties = 0
    
    # Get all Services/Models files
    $servicesModelFiles = Get-ChildItem $servicesModelsPath -Filter "*.cs"
    
    foreach ($servicesModelFile in $servicesModelFiles) {
        $totalServicesModelFiles++
        $fileName = $servicesModelFile.BaseName
        
        if ($Verbose) {
            Write-Host "  Checking Services/Models/$fileName.cs" -ForegroundColor Gray
        }
        
        # Get properties from Services/Models file
        $servicesProperties = Get-PropertiesFromServicesModel $servicesModelFile.FullName
        
        if ($servicesProperties.Count -eq 0) {
            if ($Verbose) {
                Write-Host "    No properties found in $fileName" -ForegroundColor Gray
            }
            continue
        }
        
        # Check usage for this specific Services/Models class
        $usedProperties = Get-PropertyUsageFromMapping $serviceFilePath $fileName
        
        if ($Verbose) {
            Write-Host "    Properties in $fileName`: $($servicesProperties -join ', ')" -ForegroundColor Gray
            Write-Host "    Used properties for $fileName`: $($usedProperties -join ', ')" -ForegroundColor Gray
        }
        
        # Find unused properties
        $unusedProperties = $servicesProperties | Where-Object { $_ -notin $usedProperties }
        
        if ($unusedProperties.Count -gt 0) {
            $allUnusedProperties[$fileName] = $unusedProperties
            $totalUnusedProperties += $unusedProperties.Count
            
            Write-Host "  $fileName.cs:" -ForegroundColor Yellow
            foreach ($prop in $unusedProperties) {
                Write-Host "    - $prop" -ForegroundColor Red
            }
        }
        elseif ($Verbose) {
            Write-Host "    All properties are used" -ForegroundColor Green
        }
    }
    
    # Summary for this area
    Write-Host "`n  Summary for ${areaName}:" -ForegroundColor Cyan
    Write-Host "    Services/Models files checked: $totalServicesModelFiles"
    Write-Host "    Files with unused properties: $($allUnusedProperties.Count)"
    Write-Host "    Total unused properties: $totalUnusedProperties"
    
    return @{
        AreaName = $areaName
        UnusedProperties = $allUnusedProperties
        TotalFiles = $totalServicesModelFiles
        FilesWithUnused = $allUnusedProperties.Count
        TotalUnused = $totalUnusedProperties
    }
}

# Main execution
$rootPath = Join-Path $PSScriptRoot ".." ".."
$areasPath = Join-Path $rootPath "areas"

if (-not (Test-Path $areasPath)) {
    Write-Error "Areas folder not found at $areasPath"
    exit 1
}

$results = @()

if ($AreaName) {
    $areaPath = Join-Path $areasPath $AreaName
    if (Test-Path $areaPath) {
        $results += Check-AreaPropertyUsage $areaPath
    } else {
        Write-Error "Area '$AreaName' not found"
        exit 1
    }
} else {
    $areas = Get-ChildItem $areasPath -Directory | Sort-Object Name
    
    foreach ($area in $areas) {
        $results += Check-AreaPropertyUsage $area.FullName
    }
}

# Overall summary
Write-Host "`n === OVERALL SUMMARY ===" -ForegroundColor Cyan

$totalAreas = $results.Count
$totalServicesModelFiles = ($results | Measure-Object -Property TotalFiles -Sum).Sum
$totalFilesWithUnused = ($results | Measure-Object -Property FilesWithUnused -Sum).Sum
$totalUnusedProperties = ($results | Measure-Object -Property TotalUnused -Sum).Sum

Write-Host "Areas checked: $totalAreas"
Write-Host "Total Services/Models files: $totalServicesModelFiles"
Write-Host "Files with unused properties: $totalFilesWithUnused"
Write-Host "Total unused properties: $totalUnusedProperties"

if ($totalUnusedProperties -gt 0) {
    Write-Host "`nAreas with unused properties:" -ForegroundColor Yellow
    foreach ($result in $results | Where-Object { $_.TotalUnused -gt 0 }) {
        Write-Host "  $($result.AreaName): $($result.TotalUnused) unused properties in $($result.FilesWithUnused) files" -ForegroundColor Yellow
        
        foreach ($fileName in $result.UnusedProperties.Keys) {
            Write-Host "    $fileName.cs:" -ForegroundColor Gray
            foreach ($prop in $result.UnusedProperties[$fileName]) {
                Write-Host "      - $prop" -ForegroundColor Red
            }
        }
    }
} else {
    Write-Host "`nAll properties are being used! ✅" -ForegroundColor Green
}
