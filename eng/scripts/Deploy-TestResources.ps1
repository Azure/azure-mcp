param(
    [Parameter(Mandatory=$true)]
    [string[]]$Areas,
    
    [string]$SubscriptionId,
    [string]$ResourceGroupName,
    [string]$BaseName,
    [int]$DeleteAfterHours = 12,
    [switch]$Unique
)

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/../common/scripts/common.ps1"

function Test-AreaHasTestResources {
    <#
    .SYNOPSIS
    Helper function to check if an area has test resources.
    #>
    param(
        [Parameter(Mandatory=$true)]
        [string]$AreaPath
    )
    
    $testResourcesPath = "$AreaPath/tests/test-resources.bicep"
    return (Test-Path $testResourcesPath)
}

function Get-AvailableAreas {
    <#
    .SYNOPSIS
    Discovers all available areas that have test resources.
    #>
    $areasWithTestResources = @()
    $areasDir = "$RepoRoot/areas"
    
    if (Test-Path $areasDir) {
        Get-ChildItem -Path $areasDir -Directory | ForEach-Object {
            $areaName = $_.Name.ToLower()
            if (Test-AreaHasTestResources -AreaPath $_.FullName) {
                $areasWithTestResources += $areaName
            }
        }
    }
    
    return $areasWithTestResources | Sort-Object
}

function Test-AreaExists {
    <#
    .SYNOPSIS
    Validates that the specified area exists and has test resources.
    #>
    param(
        [Parameter(Mandatory=$true)]
        [string]$AreaName
    )
    
    $areaPath = "$RepoRoot/areas/$($AreaName.ToLower())"
    
    if (!(Test-Path $areaPath)) {
        Write-Error "Area '$AreaName' does not exist at path '$areaPath'."
        return $false
    }
    
    if (!(Test-AreaHasTestResources -AreaPath $areaPath)) {
        Write-Error "Area '$AreaName' does not have test resources. Expected bicep template at '$areaPath/tests/test-resources.bicep'."
        return $false
    }
    
    return $true
}

function Get-AreasToProcess {
    <#
    .SYNOPSIS
    Determines which areas to process based on the provided parameters.
    #>
    param(
        [string[]]$Areas
    )
    
    # Check if "All" is specified in the areas
    if ($Areas -contains "All") {
        # If "All" is specified with other areas, just use All logic
        if ($Areas.Count -gt 1) {
            Write-Warning "Both 'All' and specific areas specified. Using 'All' to deploy all available areas."
        }
        
        $availableAreas = Get-AvailableAreas
        if ($availableAreas.Count -eq 0) {
            Write-Error "No areas with test resources found."
            return @()
        }
        Write-Host "Deploying test resources for all available areas: $($availableAreas -join ', ')" -ForegroundColor Cyan
        return $availableAreas
    }
    else {
        # Multiple specific areas specified
        $processAreas = @()
        foreach ($areaName in $Areas) {
            $normalizedArea = $areaName.ToLower()
            if (Test-AreaExists -AreaName $normalizedArea) {
                $processAreas += $normalizedArea
            } else {
                throw "Area validation failed for '$areaName'"
            }
        }
        return $processAreas
    }
}

function Deploy-AreaTestResources {
    <#
    .SYNOPSIS
    Deploys test resources for a specific area.
    #>
    param(
        [Parameter(Mandatory=$true)]
        [string]$AreaName,
        [Parameter(Mandatory=$true)]
        [string]$AccountId,
        [string]$SubscriptionId,
        [string]$ResourceGroupName,
        [string]$BaseName,
        [int]$DeleteAfterHours,
        [switch]$Unique
    )
    
    $testResourcesDirectory = Resolve-Path -Path "$RepoRoot/areas/$AreaName/tests" -ErrorAction SilentlyContinue
    $bicepPath = "$testResourcesDirectory/test-resources.bicep"
    
    if(!(Test-Path -Path $bicepPath)) {
        Write-Error "Test resources bicep template '$bicepPath' does not exist."
        return $false
    }

    # Base the user hash on the user's account ID, subscription, and area being deployed to
    if($Unique) {
        $hash = [guid]::NewGuid().ToString()
    } else {
        $hash = (New-StringHash $AccountId, $SubscriptionId, $AreaName)
    }

    $suffix = $hash.ToLower().Substring(0, 8)

    $areaBaseName = if($BaseName) { $BaseName } else { "mcp$($suffix)" }
    $username = $AccountId.Split('@')[0].ToLower()
    $areaResourceGroupName = if($ResourceGroupName) { $ResourceGroupName } else { "$username-mcp$($suffix)" }

    Write-Host @"

=== Deploying Area: $($AreaName.ToUpper()) ===
    SubscriptionId: '$SubscriptionId'
    SubscriptionName: '$subscriptionName'
    ResourceGroupName: '$areaResourceGroupName'
    BaseName: '$areaBaseName'
    DeleteAfterHours: $DeleteAfterHours
    TestResourcesDirectory: '$testResourcesDirectory'
"@ -ForegroundColor Green

    try {
        ./eng/common/TestResources/New-TestResources.ps1 `
            -SubscriptionId $SubscriptionId `
            -ResourceGroupName $areaResourceGroupName `
            -BaseName $areaBaseName `
            -TestResourcesDirectory $testResourcesDirectory `
            -DeleteAfterHours $DeleteAfterHours `
            -Force
        
        Write-Host "✅ Successfully deployed test resources for area '$AreaName'" -ForegroundColor Green
        return $true
    }
    catch {
        Write-Error "❌ Failed to deploy test resources for area '$AreaName': $($_.Exception.Message)"
        return $false
    }
}

function New-StringHash([string[]]$strings) {
    $string = $strings -join ' '
    $hash = [System.Security.Cryptography.SHA1]::Create()
    $bytes = [System.Text.Encoding]::UTF8.GetBytes($string)
    $hashBytes = $hash.ComputeHash($bytes)
    return [BitConverter]::ToString($hashBytes) -replace '-', ''
}

# Determine which areas to process
$areasToProcess = Get-AreasToProcess -Areas $Areas

if ($areasToProcess.Count -eq 0) {
    Write-Error "No valid areas to process."
    exit 1
}

# Set up Azure context
if($SubscriptionId) {
    Select-AzSubscription -Subscription $SubscriptionId | Out-Null
    $context = Get-AzContext
} else {
    # We don't want New-TestResources to conditionally pick a subscription for us
    # If the user didn't specify a subscription, we explicitly use the current context's subscription
    $context = Get-AzContext
    $SubscriptionId = $context.Subscription.Id
}
$subscriptionName = $context.Subscription.Name
$account = $context.Account

# Calculate username for default resource group naming
$username = $account.Id.Split('@')[0].ToLower()

Push-Location $RepoRoot
try {
    $successCount = 0
    $totalCount = $areasToProcess.Count
    
    Write-Host "`n🚀 Starting deployment of test resources for $totalCount area(s)" -ForegroundColor Cyan
    
    if ($totalCount -eq 1) {
        # Single area - process synchronously for better output
        $currentArea = $areasToProcess[0]
        Write-Progress -Activity "Deploying Test Resources" -Status "Processing area: $currentArea" -PercentComplete 0
        
        $success = Deploy-AreaTestResources `
            -AreaName $currentArea `
            -AccountId $account.Id `
            -SubscriptionId $SubscriptionId `
            -ResourceGroupName $ResourceGroupName `
            -BaseName $BaseName `
            -DeleteAfterHours $DeleteAfterHours `
            -Unique:$Unique
        
        if ($success) {
            $successCount++
        }
        
        Write-Progress -Activity "Deploying Test Resources" -Completed
    } else {
        # Multiple areas - process in parallel using background jobs
        $jobs = @{}
        $completedJobs = @()
        $failedAreas = @()
        
        # Start background jobs for each area
        foreach ($area in $areasToProcess) {
            Write-Host "Starting deployment job for area: $area" -ForegroundColor Yellow
            
            $job = Start-Job -ScriptBlock {
                param($AreaName, $AccountId, $SubscriptionId, $ResourceGroupName, $BaseName, $DeleteAfterHours, $Unique, $RepoRoot)
                
                # Set working directory
                Set-Location $RepoRoot
                
                # Import required modules and scripts
                $ErrorActionPreference = 'Stop'
                . "$RepoRoot/eng/common/scripts/common.ps1"
                
                function Test-AreaHasTestResources {
                    param([string]$AreaPath)
                    $testResourcesPath = "$AreaPath/tests/test-resources.bicep"
                    return (Test-Path $testResourcesPath)
                }
                
                function New-StringHash([string[]]$strings) {
                    $string = $strings -join ' '
                    $hash = [System.Security.Cryptography.SHA1]::Create()
                    $bytes = [System.Text.Encoding]::UTF8.GetBytes($string)
                    $hashBytes = $hash.ComputeHash($bytes)
                    return [BitConverter]::ToString($hashBytes) -replace '-', ''
                }
                
                # Deploy area test resources
                $testResourcesDirectory = Resolve-Path -Path "$RepoRoot/areas/$AreaName/tests" -ErrorAction SilentlyContinue
                $bicepPath = "$testResourcesDirectory/test-resources.bicep"
                
                if(!(Test-Path -Path $bicepPath)) {
                    throw "Test resources bicep template '$bicepPath' does not exist."
                }

                # Base the user hash on the user's account ID, subscription, and area being deployed to
                if($Unique) {
                    $hash = [guid]::NewGuid().ToString()
                } else {
                    $hash = (New-StringHash $AccountId, $SubscriptionId, $AreaName)
                }

                $suffix = $hash.ToLower().Substring(0, 8)

                $areaBaseName = if($BaseName) { $BaseName } else { "mcp$($suffix)" }
                $username = $AccountId.Split('@')[0].ToLower()
                $areaResourceGroupName = if($ResourceGroupName) { $ResourceGroupName } else { "$username-mcp$($suffix)" }

                Write-Output @"

=== Deploying Area: $($AreaName.ToUpper()) ===
    AccountId: '$AccountId'
    SubscriptionId: '$SubscriptionId'
    ResourceGroupName: '$areaResourceGroupName'
    BaseName: '$areaBaseName'
    DeleteAfterHours: $DeleteAfterHours
    TestResourcesDirectory: '$testResourcesDirectory'
"@

                try {
                    & "$RepoRoot/eng/common/TestResources/New-TestResources.ps1" `
                        -SubscriptionId $SubscriptionId `
                        -ResourceGroupName $areaResourceGroupName `
                        -BaseName $areaBaseName `
                        -TestResourcesDirectory $testResourcesDirectory `
                        -DeleteAfterHours $DeleteAfterHours `
                        -Force
                    
                    Write-Output "✅ Successfully deployed test resources for area '$AreaName'"
                    return $true
                }
                catch {
                    Write-Error "❌ Failed to deploy test resources for area '$AreaName': $($_.Exception.Message)"
                    throw
                }
            } -ArgumentList $area, $account.Id, $SubscriptionId, $ResourceGroupName, $BaseName, $DeleteAfterHours, $Unique.IsPresent, $RepoRoot
            
            $jobs[$area] = $job
        }
        
        # Poll job status and report progress
        while ($jobs.Count -gt $completedJobs.Count) {
            $completedCount = 0
            $runningJobs = @()
            
            foreach ($areaName in $jobs.Keys) {
                $job = $jobs[$areaName]
                
                if ($job.State -eq 'Completed' -and $areaName -notin $completedJobs) {
                    $completedJobs += $areaName
                    
                    # Check if job succeeded
                    try {
                        $result = Receive-Job -Job $job -ErrorAction Stop
                        Write-Host $result -ForegroundColor Green
                        $successCount++
                    }
                    catch {
                        Write-Host "❌ Job for area '$areaName' failed: $($_.Exception.Message)" -ForegroundColor Red
                        $failedAreas += $areaName
                    }
                    finally {
                        Remove-Job -Job $job -Force
                    }
                }
                elseif ($job.State -in @('Running', 'NotStarted')) {
                    $runningJobs += $areaName
                }
                elseif ($job.State -eq 'Failed' -and $areaName -notin $completedJobs) {
                    $completedJobs += $areaName
                    $failedAreas += $areaName
                    
                    try {
                        $result = Receive-Job -Job $job -ErrorAction SilentlyContinue
                        if ($result) {
                            Write-Host $result -ForegroundColor Red
                        }
                    }
                    catch {
                        Write-Host "❌ Job for area '$areaName' failed: $($_.Exception.Message)" -ForegroundColor Red
                    }
                    finally {
                        Remove-Job -Job $job -Force
                    }
                }
            }
            
            $progressPercent = [int](($completedJobs.Count / $totalCount) * 100)
            $runningJobsText = if ($runningJobs.Count -gt 0) { "Running: $($runningJobs -join ', ')" } else { "All jobs completed" }
            
            Write-Progress -Activity "Deploying Test Resources" -Status "Completed: $($completedJobs.Count)/$totalCount areas. $runningJobsText" -PercentComplete $progressPercent
            
            if ($completedJobs.Count -lt $totalCount) {
                Start-Sleep -Seconds 2
            }
        }
        
        Write-Progress -Activity "Deploying Test Resources" -Completed
        
        # Clean up any remaining jobs
        foreach ($job in $jobs.Values) {
            if ($job.State -ne 'Completed') {
                Remove-Job -Job $job -Force
            }
        }
    }
    
    Write-Host "`n📊 Deployment Summary:" -ForegroundColor Cyan
    Write-Host "   Total areas processed: $totalCount" -ForegroundColor White
    Write-Host "   Successful deployments: $successCount" -ForegroundColor Green
    Write-Host "   Failed deployments: $($totalCount - $successCount)" -ForegroundColor Red
    
    if ($successCount -eq $totalCount) {
        Write-Host "`n🎉 All deployments completed successfully!" -ForegroundColor Green
    } else {
        Write-Host "`n⚠️  Some deployments failed. Check the output above for details." -ForegroundColor Yellow
        exit 1
    }
}
finally {
    Pop-Location
}
