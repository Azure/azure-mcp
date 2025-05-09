param(
    [string] $ResourceGroupName,
    [string] $BaseName
)

$ErrorActionPreference = "Stop"

. "$PSScriptRoot/../../eng/common/scripts/common.ps1"

# Get current subscription ID
$subscriptionId = (Get-AzContext).Subscription.Id

$apiVersion = '2020-06-30'
$token = Get-AzAccessToken -ResourceUrl https://search.azure.com -AsSecureString | Select-Object -ExpandProperty Token
$uri = "https://$BaseName.search.windows.net"

$indexDefinition = @{
  name = "products"
  fields = @(
    @{ name = "chunk_id"; type = "Edm.String"; searchable = $true; retrievable = $true; stored = $true; sortable = $true; key = $true; analyzer = "keyword" }
    @{ name = "parent_id"; type = "Edm.String"; filterable = $true; retrievable = $true; stored = $true }
    @{ name = "chunk"; type = "Edm.String"; searchable = $true; retrievable = $true; stored = $true }
    @{ name = "title"; type = "Edm.String"; searchable = $true; retrievable = $true; stored = $true }
    @{ name = "header_1"; type = "Edm.String"; searchable = $true; retrievable = $true; stored = $true }
    @{ name = "header_2"; type = "Edm.String"; searchable = $true; retrievable = $true; stored = $true }
    @{ name = "header_3"; type = "Edm.String"; searchable = $true; retrievable = $true; stored = $true }
    @{ name = "text_vector"; type = "Collection(Edm.Single)"; searchable = $true; retrievable = $true; stored = $true; dimensions = 1536; vectorSearchProfile = "products-azureOpenAi-text-profile" }
    @{ name = "category"; type = "Edm.String"; searchable = $true; filterable = $true; retrievable = $true; stored = $true; facetable = $true }
  )
}

$dataSourceDefinition = @{
  name = "products-datasource"
  type = "azureblob"
  credentials = @{ connectionString = "ResourceId=/subscriptions/$subscriptionId/resourceGroups/$ResourceGroupName/providers/Microsoft.Storage/storageAccounts/$BaseName;" }
  container = @{ name = "searchdocs" }
}

$skillsetDefinition = @{
  name = "products-skillset"
  description = "Skillset to chunk documents and generate embeddings"
  skills = @(
    @{
        '@odata.type' = "#Microsoft.Skills.Text.SplitSkill"
        name = "#1"
        description = "Split skill to chunk documents"
        context = "/document"
        defaultLanguageCode = "en"
        textSplitMode = "pages"
        maximumPageLength = 2000
        pageOverlapLength = 500
        maximumPagesToTake = 0
        unit = "characters"
        inputs = @(
            @{
                name = "text"
                source = "/document/content"
                inputs = @()
            }
        )
        outputs = @(
            @{
                name = "textItems"
                targetName = "pages"
            }
        )
    }
    @{
        '@odata.type' = "#Microsoft.Skills.Text.AzureOpenAIEmbeddingSkill"
        name = "#2"
        context = "/document/pages/*"
        resourceUri = "https://$BaseName-ai.openai.azure.com"
        apiKey = "<redacted>"
        deploymentId = "embedding-model"
        dimensions = 1536
        modelName = "text-embedding-3-small"
        inputs = @(
            @{
                name = "text"
                source = "/document/pages/*"
                inputs = @()
            }
        )
        outputs = @(
            @{
                name = "embedding"
                targetName = "text_vector"
            }
        )
    }  )
  indexProjections = @{
    selectors = @(
      @{
        targetIndexName = "products"
        parentKeyFieldName = "parent_id"
        sourceContext = "/document/pages/*"
        mappings = @(
          @{ name = "text_vector"; source = "/document/pages/*/text_vector" }
          @{ name = "chunk"; source = "/document/pages/*" }
          @{ name = "title"; source = "/document/title" }
          @{ name = "header_1"; source = "/document/sections/h1" }
          @{ name = "header_2"; source = "/document/sections/h2" }
          @{ name = "header_3"; source = "/document/sections/h3" }
          @{ name = "category"; source = "/document/category" }
        )
      }
    )
    parameters = @{
      projectionMode = "skipIndexingParentDocuments"
    }
  }
}

$indexerDefinition = @{
  name = "products-indexer"
  dataSourceName = "products-datasource"
  skillsetName = "products-skillset"
  targetIndexName = "products"
  parameters = @{
    configuration = @{
      dataToExtract = "contentAndMetadata"
      parsingMode = "markdown"
      markdownHeaderDepth = "h3"
      markdownParsingSubmode = "oneToMany"
    }
  }
  fieldMappings = @(
    @{
      sourceFieldName = "metadata_storage_name"
      targetFieldName = "title"
    }
  )
  outputFieldMappings = @()
}

# Create the index
Invoke-RestMethod `
    -Method 'PUT' `
    -Uri "$uri/indexes/$($indexDefinition['name'])?api-version=$apiVersion" `
    -Authentication Bearer `
    -Token $token `
    -ContentType 'application/json' `
    -Body (ConvertTo-Json $indexDefinition -Depth 10)

# Create the datasource
Invoke-RestMethod `
    -Method 'PUT' `
    -Uri "$uri/datasources/$($dataSourceDefinition.name)?api-version=$apiVersion" `
    -Authentication Bearer `
    -Token $token `
    -ContentType 'application/json' `
    -Body (ConvertTo-Json $dataSourceDefinition -Depth 10)

# Create the skillset
Invoke-RestMethod `
    -Method 'PUT' `
    -Uri "$uri/skillsets/$($skillsetDefinition.name)?api-version=$apiVersion" `
    -Authentication Bearer `
    -Token $token `
    -ContentType 'application/json' `
    -Body (ConvertTo-Json $skillsetDefinition -Depth 10)

# Create the indexer
Invoke-RestMethod `
    -Method 'PUT' `
    -Uri "$uri/indexers/$($indexerDefinition.name)?api-version=$apiVersion" `
    -Authentication Bearer `
    -Token $token `
    -ContentType 'application/json' `
    -Body (ConvertTo-Json $indexerDefinition -Depth 10)

# Upload sample files
$context = New-AzStorageContext -StorageAccountName $BaseName -UseConnectedAccount
$container = 'searchdocs'
$categories = @('A', 'B', 'C')
Write-Host "Uploading sample files to blob storage: $BaseName/$container" -ForegroundColor Yellow
foreach ($file in Get-ChildItem -Path "$PSScriptRoot/../samples" -Filter '*.md') {
    $category = $categories | Get-Random
    Write-Host "  $($file.Name)`: { category: $category }" -ForegroundColor Yellow
    Set-AzStorageBlobContent -File $file.FullName -Container $container -Blob $file.Name -Metadata @{ category = $category } -Context $context -Force -ProgressAction SilentlyContinue
}
