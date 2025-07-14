
param(
    [string]$os,
    $PackageArguments
)

# Run the npm packaging step
Invoke-Expression "npm run ci-package -- $PackageArguments"
