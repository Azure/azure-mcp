param(
    [string]$Path = "$PSScriptRoot../../vsix"
)

$originalLocation = Get-Location
try {
    Set-Location $Path
    $extensions = Get-ChildItem -Filter *.vsix -Recurse -File
    foreach ($extension in $extensions) {
        Write-Host "Generating signing manifest for $extension"
        $manifestName = "$($extension.BaseName).manifest"
        $signatureName = "$($extension.BaseName).signature.p7s"

        npm exec --yes @vscode/vsce@latest -- generate-manifest --packagePath "$($extension.FullName)" -o $manifestName | Write-Host
        if ($LASTEXITCODE) {
            Write-Host "Failed to generate signing manifest for $extension"
            exit $LASTEXITCODE
        }

        Copy-Item $manifestName $signatureName
    }
} finally {
    Set-Location $originalLocation
}