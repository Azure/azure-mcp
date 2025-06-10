targetScope = 'resourceGroup'

@maxLength(8)
@description('A unique prefix for the resource names. This will be empty in pipeline deployments.')
param uniquePrefix string

@description('The location of the resource. By default, this is the same as the resource group.')
param location string

var prefix = empty(uniquePrefix) ? 'azmcp' : 'azmcp${uniquePrefix}'

// Azure OpenAI resource
resource openai 'Microsoft.CognitiveServices/accounts@2023-05-01' = {
  name:  toLower('${prefix}-test')
  location: location
  kind: 'OpenAI'
  sku: {
    name: 'S0'
  }
  properties: {
    customSubDomainName:  toLower('${prefix}-test')
    publicNetworkAccess: 'Enabled'
  }
}

// Deployment of the text-embedding-3-small model
resource openaiDeployment 'Microsoft.CognitiveServices/accounts/deployments@2023-05-01' = {
  parent: openai
  name: 'embedding-model'
  sku: {
    name: 'Standard'
    capacity: 100 // This is the Tokens Per Minute (TPM) capacity for the model
  }
  properties: {
    model: {
      format: 'OpenAI'
      name: 'text-embedding-3-small'
    }
  }
}

// Managed identity to place on the search service
resource searchServiceIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: '${prefix}-search-service-identity'
  location: location
}

// Role assignments:
// Identity           | Resource        | Role
// -------------------------------------------------------------------------------
// search service     | openai account  | Cognitive Services OpenAI Contributor

// Cognitive Services OpenAI Contributor role definition
resource openaiContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // Cognitive Services OpenAI Contributor role
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#cognitive-services-openai-contributor
  name: 'a001fd3d-188f-4b5d-821b-7da978bf7442'
}

// Assign Cognitive Services OpenAI Contributor role to the search resource's identity
resource search_openAi_roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(openaiContributorRoleDefinition.id, searchServiceIdentity.id, openai.id)
  scope: openai
  properties: {
    principalId: searchServiceIdentity.properties.principalId
    roleDefinitionId: openaiContributorRoleDefinition.id
  }
}
