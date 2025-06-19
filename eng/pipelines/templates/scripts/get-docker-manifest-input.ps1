<#
.SYNOPSIS

.DESCRIPTION

.PARAMETER DockerDeploymentJson
Traditionally should be pulled from a docker deployment yml object via devops expression. Something along the lines of

    $configuration = '${{ convertToJson(parameters.DockerDeployments) }}' -replace '\\', '/'
    get-docker-manifest-input.ps1 -DockerDeploymentJson $configuraiton -ContainerRegistry azsdkengsys -ImageTag 1.0.0-dev.20220407.1

See eng/containers/ci.yml or tools/test-proxy/ci.yml for example configurations.

.PARAMETER ContainerRegistery
The container registry where the tags are stored. Should align with what is passed into `publish-docker-version.yml`

.PARAMETER ImageTag
Docker image tag for current build. Should align with what is passed into `publish-docker-version.yml`.

.PARAMETER DevopsVariable
Used to set an output variable. If not provided, will simply print what it resolves from the deployment json.
#>
param(
  [Parameter(mandatory=$true)]
  [string] $DockerDeploymentJson,
  [Parameter(mandatory=$true)]
  [string] $ContainerRegistry,
  [Parameter(mandatory=$true)]
  [string] $ImageTag
)
$assembledVariable = ""
$configs = $DockerDeploymentJson | ConvertFrom-Json

foreach($config in $configs){
  if(!$config.excludeFromManifest){
    $assembledVariable += "$ContainerRegistry.azurecr.io/$($config.dockerRepo):$ImageTag "
  }
}

if(!$assembledVariable){
  Write-Error "Unable to determine any dependent tags."
  exit(1)
}

return $assembledVariable


