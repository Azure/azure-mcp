param(
    $PackageArguments
)

# Define OS targets and corresponding RIDs
$osList = @(
    @{ Name = "win-x64"; Rid = "win-x64" },
    @{ Name = "linux-x64"; Rid = "linux-x64" },
    @{ Name = "osx-x64"; Rid = "osx-x64" }
)
# Define project and destination base paths
$projectPath = Resolve-Path "../../src"
$dstBase = Join-Path $PSScriptRoot "server"

foreach ($os in $osList) {
    $dstDir = Join-Path $dstBase $($os.Name)
    if (!(Test-Path $dstDir)) {
        New-Item -ItemType Directory -Path $dstDir | Out-Null
    }
    # Publish the .NET server for the target OS
    dotnet publish $projectPath -c Release -r $($os.Rid) --self-contained true -o $dstDir
}

# Run the npm packaging step
Invoke-Expression "npm run ci-package -- $PackageArguments"
