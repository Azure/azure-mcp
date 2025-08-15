# Build Artifact Flow

- Each step of the build flow is implemented in PowerShell or Azure Pipeline Yaml.
- The PowerShell scripts will accept input and output paths, with defaults set to flow from stage to stage
- In Azure pipelines, the outputs from each stage will be uploaded to the build for use across later jobs and stages
- In each Azure Pipeline job, the required artifacts are downloaded and their paths are passed to the appropriate stage script.

## Build
- Implemented in `eng/scripts/Build-Code.ps1`
- Used by `eng/pipelines/templates/jobs/build.yml`
- Produces artifacts in `.work/build` (overridden with `-OutputPath`)
- Uses [.NET runtime identifiers](https://learn.microsoft.com/dotnet/core/rid-catalog) for OS and Architecture names
  - win, linux, osx
  - x64, arm64
- Produces a folder per platform, per cli:
  ```
  .work/
    build/
      azure/
        win-x64/
        osx-arm64/
      azure-native/
      fabric/
  ```
- Outputs are not signed or packaged

## Test
- Implemented in `eng/scripts/Test-Code.ps1`
- Used by `eng/pipelines/templates/jobs/build.yml` and `live-test.yml`
- Tests from source and does not consume Build stage artifacts

## Sign
- Implemented in `eng/pipelines/templates/jobs/sign-and-pack.yml`
- There are no powershell scripts for local reproduction
- Replaces binaries in-place, so has no effect on input -> output paths

## Package
- Implemented in format specific scripts like:
  - `eng/scripts/Pack-Modules.ps1`  (npm)
  - `eng/scripts/Build-Docker.ps1` (docker)
- Used by `eng/pipelines/templates/jobs/sign-and-pack.yml`
- Consumes Build stage artifacts from `.work/build` (overridden with `-ArtifactsPath`)
- Produces artifacts in `.work/package` (overridden with `-OutputPath`)
- Produces a folder per format, per cli:
  ```
  .work/
    package/
      azure/
        npm/
          win-x64/
        vsix/
      azure-native/
      fabric/
  ```

## Release
- Implemented in `eng/pipelines/templates/jobs/release.yml`
- There are no powershell scripts for local reproduction
- Consumes Package stage artifacts
- Publishes them to external feeds and registries
