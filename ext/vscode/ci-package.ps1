
param(
    [string]$os,
    $PackageArguments
)

# Run the npm packaging step
Invoke-Expression "npm run ci-package -- $PackageArguments"
Remove-Item -Recurse -Force node_modules
