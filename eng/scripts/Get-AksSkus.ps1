param(
    [Parameter(Mandatory = $true)]
    [string]$Region = "westus2"
)

Write-Host "Region: $Region" -ForegroundColor Cyan

# 1. Pull all VM SKUs in the region
$rawSkus = az vm list-skus --location $Region --resource-type virtualMachines --output json `
            | ConvertFrom-Json -AsHashTable

Write-Host "Total SKUs: $($rawSkus.Count)"

# 2. Remove SKUs that carry explicit restrictions
$unrestricted = $rawSkus | Where-Object { -not $_['restrictions'] }
Write-Host "Unrestricted: $($unrestricted.Count)"

# 3. Keep only families AKS actually allows (broad brush)
$familyRegex = '^Standard_(D|DS|E|ES|F|FX|B|M|N|A)'
$aksFamilies = $unrestricted | Where-Object { $_['name'] -match $familyRegex }
Write-Host "AKS Families: $($aksFamilies.Count)"

# 4. Enforce minimum spec: 2 vCPUs, 4 GB RAM
$minSpec = $aksFamilies | Where-Object {
    $cpuCap = $_['capabilities'] | Where-Object { $_['name'] -eq 'vCPUs' }
    $memCap = $_['capabilities'] | Where-Object { $_['name'] -eq 'MemoryGB' }
    
    $cpu = if ($cpuCap) { $cpuCap['value'] -as [int] } else { 0 }
    $mem = if ($memCap) { $memCap['value'] -as [int] } else { 0 }
    
    $cpu -ge 2 -and $mem -ge 4
}
Write-Host "Min Spec: $($minSpec.Count)"

# 5. Sort by smallest VM first (vCPU, then RAM)
$sortedMinSpec = $minSpec | Sort-Object {
    $cpuCap = $_['capabilities'] | Where-Object { $_['name'] -eq 'vCPUs' }
    $cpu = if ($cpuCap) { $cpuCap['value'] -as [int] } else { 999 }
    $cpu
}, {
    $memCap = $_['capabilities'] | Where-Object { $_['name'] -eq 'MemoryGB' }
    $mem = if ($memCap) { $memCap['value'] -as [int] } else { 999 }
    $mem
}

# 6. Show the first 10 smallest AKS-compatible SKUs
Write-Host ''
Write-Host "First 10 smallest AKS-compatible SKUs in $Region (sorted by vCPU, then RAM):" -ForegroundColor Green
$sortedMinSpec[0..9] | ForEach-Object { 
    if ($_) {
        $cpuCap = $_['capabilities'] | Where-Object { $_['name'] -eq 'vCPUs' }
        $memCap = $_['capabilities'] | Where-Object { $_['name'] -eq 'MemoryGB' }
        $cpu = if ($cpuCap) { $cpuCap['value'] } else { '?' }
        $mem = if ($memCap) { $memCap['value'] } else { '?' }
        Write-Host "$($_['name']) - $cpu vCPU, $mem GB RAM"
    }
}

Write-Host ''
Write-Host "Total meeting minimum requirements: $($minSpec.Count)" -ForegroundColor Cyan
