# cSpell:ignore nusing

$RepositoryRoot = $PSScriptRoot
$workPath = "$RepositoryRoot\.work"
$slnPath = "$RepositoryRoot\AzureMcp.sln"

$ErrorActionPreference = "Stop"
function CreateTestProject {
    param (
        [string]$Path,
        [string[]]$projectDependencies = @()
    )
    Write-Host "- Creating $Template $Path..." -ForegroundColor Cyan
    New-Item -Path $Path -ItemType Directory -Force | Out-Null

    Push-Location $Path
    try {
        $name = Split-Path -Path $Path -Leaf
        $projFile = "$Path\$name.csproj"

        $projectReferences = $ProjectDependencies
        | ForEach-Object {"<ProjectReference Include=`"$(Resolve-Path -Path $_ -Relative)`" />"}

@"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <IsTestProject>true</IsTestProject>
    <OutputType>Exe</OutputType>
  </PropertyGroup>
  <ItemGroup>
    $($projectReferences -join "`n    ")
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="NSubstitute" />
    <PackageReference Include="NSubstitute.Analyzers.CSharp" />
    <PackageReference Include="xunit.v3" />
    <PackageReference Include="xunit.runner.visualstudio" />
    <PackageReference Include="coverlet.collector" />
  </ItemGroup>
</Project>
"@ | Set-Content -Path $projFile

        #dotnet sln $slnPath add $projFile | Out-Host
    }
    finally {
        Pop-Location
    }
    return $projFile
}

function CreateLibraryProject {
    param (
        [string]$Path,
        [switch]$MapEmbeddedResources,
        [string[]]$ProjectDependencies = @(),
        [bool]$IsAotCompatible = $true
    )
    Write-Host "- Creating $Template $Path..." -ForegroundColor Cyan
    New-Item -Path $Path -ItemType Directory -Force | Out-Null
    Push-Location $Path
    try {
        $name = Split-Path -Path $Path -Leaf
        $projFile = "$Path\$name.csproj"

        $projectReferences = $ProjectDependencies
        | ForEach-Object {"<ProjectReference Include=`"$(Resolve-Path -Path $_ -Relative)`" />"}

        $projectReferenceItemGroup = @"
  <ItemGroup>
    $($projectReferences -join "`n    ")
  </ItemGroup>
"@

        $embeddedResourceItemGroup = @"
  <ItemGroup>
    <EmbeddedResource Include="**\Resources\*.txt" />
    <EmbeddedResource Include="**\Resources\*.json" />
  </ItemGroup>
"@

@"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <IsAotCompatible>$($IsAotCompatible ? 'true' : 'false')</IsAotCompatible>
  </PropertyGroup>
$(if($MapEmbeddedResources) { $embeddedResourceItemGroup })
$(if($ProjectDependencies) { $projectReferenceItemGroup })
</Project>
"@ | Set-Content -Path $projFile
    }
    finally {
        Pop-Location
    }
    return $projFile
}

function CreateCliProject {
    param (
        [string]$Path,
        [string[]]$projectDependencies = @()
    )
    Write-Host "- Creating $Template $Path..." -ForegroundColor Cyan
    New-Item -Path $Path -ItemType Directory -Force | Out-Null
    Push-Location $Path
    try {
        $name = Split-Path -Path $Path -Leaf
        $projFile = "$Path\$name.csproj"

        $projectReferences = $projectDependencies
        | ForEach-Object {"<ProjectReference Include=`"$(Resolve-Path -Path $_ -Relative)`" />"}

@"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <CliName>azmcp</CliName>
    <AssemblyName>`$(CliName)</AssemblyName>
    <AssemblyTitle>Azure MCP Server</AssemblyTitle>

    <!-- Publishing settings -->
    <IsAotCompatible>true</IsAotCompatible>
    <PublishSingleFile>false</PublishSingleFile>
    <SelfContained>false</SelfContained>
    <PublishReadyToRun>false</PublishReadyToRun>
    <PublishTrimmed>false</PublishTrimmed>
    <IncludeNativeLibrariesForSelfExtract>true</IncludeNativeLibrariesForSelfExtract>
    <DebugType>embedded</DebugType>
  </PropertyGroup>

  <!-- AOT compilation flags -->
  <PropertyGroup>
    <IlcFoldIdenticalMethodBodies>true</IlcFoldIdenticalMethodBodies>
  </PropertyGroup>

  <!-- Debug configuration -->
  <PropertyGroup Condition="'`$(Configuration)'=='Debug'">
    <PublishSingleFile>false</PublishSingleFile>
    <SelfContained>false</SelfContained>
    <DebugType>portable</DebugType>
    <DebugSymbols>true</DebugSymbols>
  </PropertyGroup>

  <ItemGroup>
    <FrameworkReference Include="Microsoft.AspNetCore.App" />
  </ItemGroup>

  <ItemGroup>
    $($projectReferences -join "`n    ")
  </ItemGroup>
</Project>
"@ | Set-Content -Path $projFile
    }
    finally {
        Pop-Location
    }
    return $projFile
}

function CreateArea{
    param(
        [string]$cliProjectPath,
        [string]$areaName,
        [string]$areaProjectName,
        [string[]]$libDependencies = @(),
        [string[]]$testDependencies = @(),
        [hashtable]$packageVersions
    )

    Write-Host "Creating area $areaName..." -ForegroundColor Yellow
    $areaPath = "$RepositoryRoot\areas\$areaName"

    $projPath = CreateLibraryProject -Path "$areaPath\src\$areaProjectName" -projectDependencies $libDependencies -MapEmbeddedResources
    CreateTestProject -Path "$areaPath\tests\$areaProjectName.UnitTests" -projectDependencies $testDependencies, $projPath
    CreateTestProject -Path "$areaPath\tests\$areaProjectName.LiveTests" -projectDependencies $cliProjectPath, $testDependencies

    Copy-Item -Path "$workPath\src\GlobalUsings.cs" -Destination "$areaPath\src\$areaProjectName" -Force

    @"
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("$areaProjectName.UnitTests")]
[assembly: InternalsVisibleTo("$areaProjectName.LiveTests")]
"@ | Set-Content -Path "$areaPath\src\$areaProjectName\AssemblyInfo.cs"


    UpdatePackageReferences -Path "$areaPath\src\$areaProjectName" -PackageVersions $packageVersions
    UpdatePackageReferences -Path "$areaPath\tests\$areaProjectName.UnitTests" -PackageVersions $packageVersions
    UpdatePackageReferences -Path "$areaPath\tests\$areaProjectName.LiveTests" -PackageVersions $packageVersions
}

function UpdatePackageReferences {
    param(
        [string]$Path,
        [string[]]$AdditionalPackages = @(),
        [hashtable]$PackageVersions
    )
    Write-Host "Adding package references to $Path..." -ForegroundColor Cyan
    $projFile = Get-ChildItem -Path $Path -Filter "*.csproj" | Select-Object -ExpandProperty FullName -First 1
    if(!$projFile) {
        Write-Host "No project file found in $Path." -ForegroundColor Yellow
        return;
    }

    $sourceFiles = Get-ChildItem -Path $Path -Recurse -Include *.cs

    $usedNamespaces = $sourceFiles
    | Get-Content
    | Where-Object { $_ -match '(global )?using\s+([a-zA-Z0-9\.]+);' }
    | ForEach-Object { $Matches[2] }
    | Sort-Object -Unique

    $matchingPackages = @{}
    foreach($packageName in $PackageVersions.Keys) {
        if($AdditionalPackages -contains $packageName) {
            $matchingPackages[$packageName] = $PackageVersions[$packageName]
            continue;
        }

        foreach ($namespace in $usedNamespaces) {
            if ($namespace.StartsWith($packageName)) {
                $matchingPackages[$packageName] = $PackageVersions[$packageName]
                break;
            }
        }
    }

    [xml]$proj = Get-Content -Path $projFile
    $itemGroup = $proj.Project.ItemGroup | Where-Object { $_.PackageReference }
    if(!$itemGroup) {
        $itemGroup = $proj.CreateElement("ItemGroup")
        $proj.Project.AppendChild($itemGroup) | Out-Null
    }
    foreach ($packageName in $matchingPackages.Keys | Sort-Object) {
        Write-Host "Adding package $packageName version $($matchingPackages[$packageName]) to $projFile..." -ForegroundColor Cyan
        $packageReference = $proj.CreateElement("PackageReference")
        $packageReference.SetAttribute("Include", $packageName)
        $itemGroup.AppendChild($packageReference) | Out-Null
    }
    $proj.Save($projFile)
}

function AddPackageReferences {
    param(
        [string]$Path,
        [hashtable]$PackageVersions,
        [string[]]$Packages
    )
    Write-Host "Adding package references to $Path..." -ForegroundColor Cyan
    $projFile = Get-ChildItem -Path $Path -Filter "*.csproj" | Select-Object -ExpandProperty FullName -First 1
    if(!$projFile) {
        Write-Host "No project file found in $Path." -ForegroundColor Yellow
        return;
    }

    [xml]$proj = Get-Content -Path $projFile
    $itemGroup = $proj.Project.ItemGroup | Where-Object { $_.PackageReference }
    if(!$itemGroup) {
        $itemGroup = $proj.CreateElement("ItemGroup")
        $proj.Project.AppendChild($itemGroup) | Out-Null
    }
    $existingReferences = $itemGroup.PackageReference | ForEach-Object { $_.Include }

    $packagesToAdd = @{}
    foreach($packageName in $Packages) {
        if($existingReferences -notcontains $packageName) {
            $packagesToAdd[$packageName] = $PackageVersions[$packageName]
        }
    }

    foreach ($packageName in $packagesToAdd.Keys | Sort-Object) {
        Write-Host "Adding package $packageName version $($packagesToAdd[$packageName]) to $projFile..." -ForegroundColor Cyan
        $packageReference = $proj.CreateElement("PackageReference")
        $packageReference.SetAttribute("Include", $packageName)
        $itemGroup.AppendChild($packageReference) | Out-Null
    }
    $proj.Save($projFile)
}

function MigrateArea {
    param(
        [string]$areaName,
        [string]$azureCliPath,
        [string]$azureCorePath,
        [string]$azureTestsCorePath,
        [hashtable]$packageVersions
    )

    $areaDirectoryName = $areaName.ToLower()
    Write-Host "Migrating area $areaName..." -ForegroundColor Yellow

    $sourcePath = "$workPath\src\areas\$areaName"
    if(!(Test-Path $sourcePath)) {
        Write-Host "Source path $sourcePath does not exist." -ForegroundColor Yellow
        return;
    }

    $areaProjectName = "AzureMcp.$areaName"

    CreateArea -areaName $areaDirectoryName `
        -cliProjectPath $azureCliPath `
        -areaProjectName $areaProjectName `
        -libDependencies $azureCorePath `
        -testDependencies $azureTestsCorePath

    $targetPath = "$RepositoryRoot\areas\$areaDirectoryName\src\AzureMcp.$areaName"
    Write-Host "Migrating source files from $sourcePath to $targetPath" -ForegroundColor Cyan
    Move-Item -Path "$sourcePath\*" -Destination $targetPath -Force
    UpdatePackageReferences -Path $targetPath -PackageVersions $packageVersions

    $sourcePath = "$workPath\tests\Areas\$areaName\UnitTests"
    if(Test-Path $sourcePath) {
        $targetPath = "$RepositoryRoot\areas\$areaDirectoryName\tests\AzureMcp.$areaName.UnitTests"
        Write-Host "Migrating unit test files from $sourcePath to $targetPath" -ForegroundColor Cyan
        Move-Item -Path "$sourcePath\*" -Destination $targetPath -Force
    }

    $sourcePath = "$workPath\tests\Areas\$areaName\LiveTests"
    if(Test-Path $sourcePath) {
        # Has live tests
        $targetPath = "$RepositoryRoot\areas\$areaDirectoryName\tests\AzureMcp.$areaName.LiveTests"
        Write-Host "Migrating live test from $sourcePath to $targetPath" -ForegroundColor Cyan
        Move-Item -Path "$sourcePath\*" -Destination $targetPath -Force

        $bicepFile = "$workPath\infra\services\$areaDirectoryName.bicep"
        $targetPath = "$RepositoryRoot\areas\$areaDirectoryName\tests\test-resources.bicep"
        if(Test-Path $bicepFile) {
            Write-Host "Migrating Bicep file from $bicepFile to $targetPath" -ForegroundColor Cyan
            Move-Item -Path $bicepFile -Destination $targetPath -Force
        } else {
            Write-Host "Copying default bicep template to $targetPath" -ForegroundColor Cyan
            Copy-Item -Path "$workPath\infra\core\test-resources.bicep" -Destination $targetPath -Force
        }

        $postScriptFile = "$workPath\infra\services\$areaDirectoryName-post.ps1"
        $targetPath = "$RepositoryRoot\areas\$areaDirectoryName\tests\test-resources-post.ps1"
        if(Test-Path $postScriptFile) {
            Write-Host "Migrating post script file from $postScriptFile to $targetPath" -ForegroundColor Cyan
            Move-Item -Path $postScriptFile -Destination $targetPath -Force
        } else {
            AddTestPostScript -path $targetPath
        }
    }
}

function ResetAndInitialize {
    git reset
    git add .\convert-repo.ps1
    git restore .
    git clean -xdf

    # Move all existing source into a temporary directory ".work"
    if(Test-Path $workPath) {
        Write-Host "Temporary work directory already exists at $workPath. Removing it..." -ForegroundColor Yellow
        Remove-Item $workPath -Recurse -Force
    }

    New-Item -ItemType Directory -Path $workPath | Out-Null

    Write-Host "Moving existing files to temporary work directory..." -ForegroundColor Cyan
    Move-Item -Path "$RepositoryRoot\src" -Destination $workPath -Force
    Move-Item -Path "$RepositoryRoot\tests" -Destination $workPath -Force
    Move-Item -Path "$RepositoryRoot\infra" -Destination $workPath -Force
    Move-Item -Path "$RepositoryRoot\*.sln" -Destination $workPath -Force
}

function UpdateDirectoryBuildProps {
    $dirPropsPath = "$RepositoryRoot/Directory.Build.props"
    [xml]$proj = Get-Content "$workPath/src/AzureMcp.csproj"
    $version = $proj.Project.PropertyGroup.Version | Select-Object -First 1

    [xml]$dirsProps = Get-Content -Path $dirPropsPath -Raw
    $propertyGroup = $dirsProps.Project.PropertyGroup | Select-Object -First 1

    $node = $propertyGroup.PrependChild($dirsProps.CreateElement("Version"))
    $node.InnerText = $version

    $node = $propertyGroup.AppendChild($dirsProps.CreateElement("IsPackable"))
    $node.InnerText = 'False'

    $node = $propertyGroup.AppendChild($dirsProps.CreateComment(''))
    $node.InnerText = ' Suppress SYSLIB0020 for generated System.Text.Json code that uses obsolete IgnoreNullValues '

    $node = $propertyGroup.AppendChild($dirsProps.CreateElement("NoWarn"))
    $node.InnerText = '$(NoWarn);SYSLIB0020'

    $dirsProps.Save($dirPropsPath)
}

function AddProjectsToSolution {
    $projects = Get-ChildItem -Path $RepositoryRoot -Filter "*.csproj" -Recurse
    #dotnet sln add $projects --in-root
    foreach ($project in $projects) {
        dotnet sln add $project.FullName | Out-Host
    }
}

function UpdateCodeFiles {
    $excludeNamespaces = @("AzureMcp")

    $namespaceChanges = @{}
    $fileNamespaces = @{}

    $projects = Get-ChildItem -Path $RepositoryRoot -Filter "*.csproj" -Recurse
        | Where-Object { $_.FullName -like '*\core\*' -or $_.FullName -like '*\areas\*' }

    foreach ($project in $projects) {
        $projectDirectory = Split-Path -Path $project.FullName -Parent
        $codeFiles = Get-ChildItem -Path $projectDirectory -Recurse -Include '*.cs'
        $rootNamespace = $project.Name.Replace('.csproj', '')

        if($codeFiles.Count -eq 0) {
            Write-Host "No code files found in $projectDirectory." -ForegroundColor Yellow
            Remove-Item $projectDirectory -Recurse -Force
            $projects = $projects | Where-Object { $_ -ne $project }
            continue;
        }

        foreach ($file in $codeFiles) {
            if ($file.DirectoryName -eq $projectDirectory) {
                # If the file is in the root of the project, use the root namespace directly
                $newNamespace = $rootNamespace
            } else {
                # Otherwise, calculate the relative namespace based on the project directory
                $relativeNamespace = $file.DirectoryName.Substring($projectDirectory.Length + 1).Replace('\', '.')
                $newNamespace = "$rootNamespace.$relativeNamespace"
            }

            $content = Get-Content -Path $file.FullName -Raw
            $newContent = $content
            # Replace core namespaces (AzureMcp.Commands -> AzureMcp.Core.Commands, etc.)
            if ($newContent -match '\n( *)namespace\s+([\w\.]+)') {
                $currentNamespace = $Matches[2]

                if ($currentNamespace -ne $newNamespace -and $currentNamespace -notin $excludeNamespaces) {
                    $existingChange = $namespaceChanges[$currentNamespace]
                    if($existingChange -and $existingChange -ne $newNamespace) {
                        Write-Host "Namespace $currentNamespace already mapped to $existingChange. Overriding with $newNamespace." -ForegroundColor Yellow
                    }

                    $namespaceChanges[$currentNamespace] = $newNamespace
                    $fileNamespaces[$file.FullName] = $newNamespace
                }
            }

            $newContent = $newContent.TrimEnd(@("`n", "`r"))
            if ($newContent -ne $content) {
                Set-Content -Path $file.FullName -Value $newContent
            }
        }
    }

    $sortedKeys = $namespaceChanges.Keys | Sort-Object { $_.Length } -Descending
    foreach ($current in $sortedKeys) {
        $new = $namespaceChanges[$current]
        Write-Host "$current -> $new" -ForegroundColor Cyan
    }

    # apply namespace changes to all code files
    foreach ($project in $projects) {
        $projectDirectory = Split-Path -Path $project.FullName -Parent
        $codeFiles = Get-ChildItem -Path $projectDirectory -Recurse -Include '*.cs'

        foreach ($file in $codeFiles) {
            $content = Get-Content -Path $file.FullName -Raw
            $newContent = $content
            foreach ($oldNamespace in $sortedKeys) {
                $newNamespace = $namespaceChanges[$oldNamespace]
                $newContent = $newContent.Replace("$oldNamespace", "$newNamespace")
            }
            # if the file references IAreaSetup, it should have a using statement for AzureMcp.Core.Areas
            if ($file.Name -ne 'IAreaSetup.cs' -and $newContent -match 'IAreaSetup') {
                $usingStatement = "using AzureMcp.Core.Areas;"

                if (-not $newContent.Contains($usingStatement)) {
                    #Add a using statement before the namespace declaration
                    $newContent = $newContent -replace '\n( *namespace )', "$usingStatement`n`n`$1"
                }
            }

            # Remove xunit Trait attributes
            $newContent = $newContent -replace '\[Trait\(.*?\]\s*\n*', ''

            if($project.Name -like '*Tests*' -and $newContent -notlike '*using AzureMcp.Tests;*') {
                $newContent = $newContent -replace 'using Xunit;', "using AzureMcp.Tests;`nusing Xunit;"
            }

            if($project.Name -like '*LiveTests*' -and $newContent -notlike '*using AzureMcp.Tests.Client;*') {
                $newContent = $newContent -replace 'using Xunit;', "using AzureMcp.Tests.Client;`nusing Xunit;"
            }

            $newNamespace = $fileNamespaces[$file.FullName]
            if($newNamespace) {
                $newContent = $newContent -replace '(\n *)namespace +([\w\.]+)', "`$1namespace $newNamespace"
            }

            $newContent = $newContent.TrimEnd(@("`n", "`r"))
            if ($newContent -ne $content) {
                Set-Content -Path $file.FullName -Value $newContent
            }
        }
    }
}

function AddTestPostScript([string]$Path) {
    $directory = Split-Path -Path $Path -Parent
    $content = Get-Content "$workPath/infra/core/test-resources-post.ps1" -Raw

    $relativePath = Resolve-Path -Path "$RepositoryRoot/eng" -Relative -RelativeBasePath $directory

    $content = $content.Replace('../../eng', $relativePath.Replace('\', '/'))

    $content | Set-Content -Path $Path

    Write-Host "Test post script created at $Path" -ForegroundColor Green
}

function RemoveEmptyDirectories {
    while($true)
    {
        $emptyDirectories = Get-ChildItem -Path $RepositoryRoot -Directory -Recurse | Where-Object { !(Get-ChildItem $_) }
        $emptyDirectories | Remove-Item
        if($emptyDirectories.Count -eq 0) {
            break;
        }
    }
}

# Setup script for an Area based repository structure
# Creates directory structure and C# projects as defined in README.md

Write-Host "Setting up area based repository structure..." -ForegroundColor Green
# Set repository root to current directory

Push-Location $RepositoryRoot
try {
    $packagesPropsPath = "$RepositoryRoot\Directory.Packages.props"
    $cliDirectory = "$RepositoryRoot\core\src\AzureMcp.Cli"
    $coreDirectory = "$RepositoryRoot\core\src\AzureMcp.Core"

    ResetAndInitialize
    UpdateDirectoryBuildProps

    Write-Host "Creating solution file at $slnPath..." -ForegroundColor Cyan
    dotnet new sln -n "AzureMcp" -o $RepositoryRoot

    $packageVersions = @{}
    [xml]$packagesProps = Get-Content -Path $packagesPropsPath -Raw
    $packagesProps.Project.ItemGroup.PackageVersion | ForEach-Object { $packageVersions[$_.Include] = $_.Version }

    $areas = Get-ChildItem -Path "$workPath/src/areas" -Directory
    | Select-Object -ExpandProperty Name
    | Where-Object { $_ -notin @('Group', 'Server', 'Subscription', 'Tools') }

    # Create directory structure
    Write-Host "Creating Core..." -ForegroundColor Yellow

    $azureCorePath = CreateLibraryProject -Path $coreDirectory -MapEmbeddedResources
    $azureCliPath = CreateCliProject -Path $cliDirectory -projectDependencies $azureCorePath
    Copy-Item -Path "$workPath\src\GlobalUsings.cs" -Destination $cliDirectory -Force

    # add the wildcard areas reference to the cli project
    $cliProjFile = Get-ChildItem -Path $azureCliPath -Filter "*.csproj" | Select-Object -ExpandProperty FullName -First 1
    [xml]$proj = Get-Content -Path $cliProjFile
    $reference = $proj.CreateElement("ProjectReference")
    $reference.SetAttribute("Include", "..\..\..\areas\*\src\**\AzureMcp.*.csproj")
    $proj.Project.ItemGroup.AppendChild($reference) | Out-Null
    $proj.Save($cliProjFile)

    $azureTestsCorePath = CreateLibraryProject -Path "$RepositoryRoot\core\tests\AzureMcp.Tests" -projectDependencies $azureCorePath -IsAotCompatible $false
    CreateTestProject -Path "$RepositoryRoot\core\tests\AzureMcp.Core.UnitTests" -projectDependencies $azureTestsCorePath, $azureCliPath
    CreateTestProject -Path "$RepositoryRoot\core\tests\AzureMcp.Core.LiveTests" -projectDependencies $azureTestsCorePath, $azureCliPath

    Write-Host "Core created successfully!" -ForegroundColor Green

    Write-Host "Creating areas..." -ForegroundColor Yellow

    foreach ($area in $areas) {
        MigrateArea `
            -AreaName $area `
            -azureCliPath $azureCliPath `
            -azureCorePath $azureCorePath `
            -azureTestsCorePath $azureTestsCorePath `
            -PackageVersions $packageVersions
    }

    RemoveEmptyDirectories

    # All remaining files are "Core" files
    $sourcePath = "$workPath\src"
    $targetPath = "$RepositoryRoot\core\src\AzureMcp.Core"
    Write-Host "Moving core source files from $sourcePath to $targetPath" -ForegroundColor Cyan
    Remove-Item -Path "$sourcePath\*.*proj" -Force -ErrorAction SilentlyContinue
    Move-Item -Path "$sourcePath\*" -Destination $targetPath -Force

    $sourcePath = "$workPath\tests"
    $targetPath = "$RepositoryRoot\core\tests\AzureMcp.Core.UnitTests"
    Write-Host "Moving core test files from $sourcePath to $targetPath" -ForegroundColor Cyan
    # move only files that end with "Tests.cs" to the target folder, keeping the directory structure
    Get-ChildItem -Path $sourcePath -Recurse -Filter "*Tests.cs" | ForEach-Object {
        $relativePath = $_.FullName.Substring($sourcePath.Length + 1)
        $targetFilePath = Join-Path -Path $targetPath -ChildPath $relativePath
        $targetDir = Split-Path -Path $targetFilePath -Parent
        if(!(Test-Path -Path $targetDir)) {
            New-Item -ItemType Directory -Path $targetDir | Out-Null
        }
        Move-Item -Path $_.FullName -Destination $targetFilePath -Force
    }

    $sourcePath = "$workPath\tests"
    $targetPath = "$RepositoryRoot\core\tests\AzureMcp.Tests"
    Write-Host "Moving remaining test helper files from $sourcePath to $targetPath" -ForegroundColor Cyan
    Remove-Item -Path "$sourcePath\*.*proj" -Force -ErrorAction SilentlyContinue
    Move-Item -Path "$sourcePath\*" -Destination $targetPath -Force

    ### Manual adjustments
    New-Item -Path "$RepositoryRoot\areas\monitor\tests\samples" -ItemType Directory -Force | Out-Null
    Copy-Item -Path "$workPath\infra\samples\*" -Destination "$RepositoryRoot\areas\monitor\tests\samples" -Force

    New-Item -Path "$RepositoryRoot\areas\search\tests\samples" -ItemType Directory -Force | Out-Null
    Copy-Item -Path "$workPath\infra\samples\*" -Destination "$RepositoryRoot\areas\search\tests\samples" -Force

    Move-Item -Path "$coreDirectory\Program.cs" -Destination $cliDirectory -Force
    Move-Item -Path "core\tests\AzureMcp.Tests\Areas\Server\UnitTests\CommandFactoryHelpers.cs" `
       -Destination "core\tests\AzureMcp.Core.UnitTests\Areas\Server" -Force

    AddTestPostScript -Path "$RepositoryRoot\core\tests\test-resources-post.ps1"
    Copy-Item -Path "$workPath\infra\core\test-resources.bicep" -Destination "$RepositoryRoot\core\tests" -Force
    Copy-Item -Path "$workPath\infra\core\test-resources.bicep" -Destination "$RepositoryRoot\areas\marketplace\tests" -Force

    RemoveEmptyDirectories

    New-Item 'core\tests\AzureMcp.Core.UnitTests\Areas\Server' -ItemType Directory -Force | Out-Null
    New-Item 'core\tests\AzureMcp.Core.UnitTests\Areas\Subscription' -ItemType Directory -Force | Out-Null

    Move-Item -Path "core\tests\AzureMcp.Tests\Areas\Server\UnitTests\*" -Destination "core\tests\AzureMcp.Core.UnitTests\Areas\Server" -Force
    Move-Item -Path "core\tests\AzureMcp.Tests\Areas\Subscription\UnitTests\*" -Destination "core\tests\AzureMcp.Core.UnitTests\Areas\Subscription" -Force
    Move-Item -Path "core\tests\AzureMcp.Core.UnitTests\Areas\Server\UnitTests\*" -Destination "core\tests\AzureMcp.Core.UnitTests\Areas\Server" -Force
    Move-Item -Path "core\tests\AzureMcp.Core.UnitTests\Areas\Subscription\UnitTests\*" -Destination "core\tests\AzureMcp.Core.UnitTests\Areas\Subscription" -Force

    New-Item 'core\tests\AzureMcp.Core.LiveTests\Areas\Server' -ItemType Directory -Force | Out-Null
    Move-Item -Path "core\tests\AzureMcp.Core.UnitTests\Areas\Server\LiveTests\*" -Destination "core\tests\AzureMcp.Core.LiveTests\Areas\Server" -Force

    New-Item 'core\tests\AzureMcp.Core.LiveTests\Services\Azure\Authentication' -ItemType Directory -Force | Out-Null
    Move-Item -Path "core\tests\AzureMcp.Core.UnitTests\Services\Azure\Authentication\AuthenticationIntegrationTests.cs" -Destination "core\tests\AzureMcp.Core.LiveTests\Services\Azure\Authentication\" -Force

    Move-Item -Path "core\tests\AzureMcp.Core.UnitTests\Client\*" -Destination "core\tests\AzureMcp.Core.LiveTests\" -Force
    Move-Item -Path "core\tests\AzureMcp.Core.LiveTests\MockClientTests.cs" -Destination "core\tests\AzureMcp.Core.UnitTests\Client" -Force

    AddPackageReferences -Path "areas\monitor\tests\AzureMcp.Monitor.LiveTests" -PackageVersions $packageVersions -Packages @('Azure.Monitor.Ingestion')

    UpdatePackageReferences -Path $cliDirectory -PackageVersions $packageVersions
    UpdatePackageReferences -Path $coreDirectory -PackageVersions $packageVersions -AdditionalPackages @('OpenTelemetry.Exporter.OpenTelemetryProtocol', 'Azure.Monitor.OpenTelemetry.AspNetCore', 'System.Linq.AsyncEnumerable', 'Newtonsoft.Json')
    AddPackageReferences -Path $azureTestsCorePath -PackageVersions $packageVersions -Packages @('ModelContextProtocol', 'Microsoft.NET.Test.Sdk', 'System.IdentityModel.Tokens.Jwt', 'xunit.v3.assert', 'xunit.v3.extensibility.core')

    $content = Get-Content -Path $azureCorePath -Raw
    $content = $content -replace '</IsAotCompatible>', "</IsAotCompatible>`n    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>"
    Set-Content -Path $azureCorePath -Value $content

    UpdateCodeFiles

    RemoveEmptyDirectories

    AddProjectsToSolution

    Write-Host "Azure areas created successfully!" -ForegroundColor Green
}
finally {
    Pop-Location
}
