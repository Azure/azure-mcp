targetScope = 'resourceGroup'

@minLength(4)
@maxLength(24)
@description('The base resource name.')
param baseName string = resourceGroup().name

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@description('The tenant ID to which the application and resources belong.')
param tenantId string = '72f988bf-86f1-41af-91ab-2d7cd011db47'

@description('The client OID to grant access to test resources.')
param testApplicationOid string

// Reference to storage account - we need this for RBAC assignment
resource storageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' existing = {
  name: baseName
}

// Create searchdocs container in the storage account for document storage
resource searchDocsContainer 'Microsoft.Storage/storageAccounts/blobServices/containers@2022-09-01' = {
  name: '${storageAccount.name}/default/searchdocs'
  properties: {
    publicAccess: 'None'
  }
}

// Azure AI Search service
resource search 'Microsoft.Search/searchServices@2025-02-01-preview' = {
  name: baseName
  location: location
  sku: {
    name: 'basic'
  }
  properties: {
    disableLocalAuth: true
    replicaCount: 1
    partitionCount: 1
    hostingMode: 'default'
    publicNetworkAccess: 'enabled'
  }
  identity: {
    type: 'SystemAssigned'
  }
}

// Azure OpenAI resource
resource openai 'Microsoft.CognitiveServices/accounts@2023-05-01' = {
  name: baseName
  location: location
  kind: 'OpenAI'
  sku: {
    name: 'S0'
  }
  properties: {
    customSubDomainName: toLower(baseName)
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

// Role assignments:
// Identity           | Resource         | Role
// -------------------------------------------------------------------------------
// search service      | openai account  | Cognitive Services OpenAI Contributor
// search service      | storage account | Storage Blob Data Reader
// test application    | openai account  | Cognitive Services OpenAI Contributor
// test application    | search service  | Search Index Data Reader
// test application    | search service  | Search Service Contributor

// Storage Blob Data Reader role definition
resource storageBlobDataReaderRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Storage Blob Data Reader role
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#storage-blob-data-reader
  name: '2a2b9908-6ea1-4ae2-8e65-a410df84e7d1'
}

// Assign Storage Blob Data Reader role for Azure Search service identity on the storage account
resource search_Storage_RoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(storageBlobDataReaderRoleDefinition.id, search.id, storageAccount.id)
  scope: storageAccount
  properties: {
    principalId: search.identity.principalId
    roleDefinitionId: storageBlobDataReaderRoleDefinition.id
  }
}

// Cognitive Services OpenAI Contributor role definition
resource openaiContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // Cognitive Services OpenAI Contributor role
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#cognitive-services-openai-contributor
  name: 'a001fd3d-188f-4b5d-821b-7da978bf7442'
}

// Assign Cognitive Services OpenAI Contributor role to testApplicationOid
resource testApp_openAi_roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(openaiContributorRoleDefinition.id, testApplicationOid, openai.id)
  scope: openai
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: openaiContributorRoleDefinition.id
  }
}

// Assign Cognitive Services OpenAI Contributor role to the search resource's identity
resource search_openAi_roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(openaiContributorRoleDefinition.id, search.id, openai.id)
  scope: openai
  properties: {
    principalId: search.identity.principalId
    roleDefinitionId: openaiContributorRoleDefinition.id
  }
}

// Search Index Data Reader role definition
resource searchIndexDataReaderRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Search Index Data Reader role
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#search-index-data-reader
  name: '1407120a-92aa-4202-b7e9-c0e197c71c8f'
}

// Assign Search Index Data Reader role to testApplicationOid
resource testApp_search_indexDataReaderRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(searchIndexDataReaderRoleDefinition.id, testApplicationOid, search.id)
  scope: search
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: searchIndexDataReaderRoleDefinition.id
  }
}

// Search Service Contributor role definition
resource searchServiceContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Search Service Contributor role
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#search-service-contributor
  name: '7ca78c08-252a-4471-8644-bb5ff32d4ba0'
}

// Assign Search Service Contributor role to testApplicationOid
resource testApp_search_contributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(searchServiceContributorRoleDefinition.id, testApplicationOid, search.id)
  scope: search
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: searchServiceContributorRoleDefinition.id
  }
}
