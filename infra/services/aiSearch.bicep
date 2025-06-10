targetScope = 'resourceGroup'

@minLength(4)
@maxLength(24)
@description('The base resource name.')
param baseName string

@description('The location of the resource. By default, this is the same as the resource group.')
param location string

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

resource searchServiceIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' existing = {
  name: 'azmcp-search-service-identity'
  scope: resourceGroup('static-test-resources')
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
    type: 'UserAssigned'
    userAssignedIdentities: {
        '${searchServiceIdentity.id}': {}
    }
  }
}

// Role assignments:
// Identity           | Resource         | Role
// -------------------------------------------------------------------------------
// search service      | storage account | Storage Blob Data Reader
// test application    | search service  | Search Index Data Reader
// test application    | search service  | Search Service Contributor

// Storage Blob Data Reader role definition
resource storageBlobDataReaderRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Storage Blob Data Reader role
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#storage-blob-data-reader
  name: '2a2b9908-6ea1-4ae2-8e65-a410df84e7d1'
}

// Search Index Data Reader role definition
resource searchIndexDataReaderRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Search Index Data Reader role
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#search-index-data-reader
  name: '1407120a-92aa-4202-b7e9-c0e197c71c8f'
}

// Search Service Contributor role definition
resource searchServiceContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Search Service Contributor role
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#search-service-contributor
  name: '7ca78c08-252a-4471-8644-bb5ff32d4ba0'
}


// Assign Storage Blob Data Reader role for Azure Search service identity on the storage account
resource search_Storage_RoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(storageBlobDataReaderRoleDefinition.id, search.id, storageAccount.id)
  scope: storageAccount
  properties: {
    principalId: searchServiceIdentity.properties.principalId
    roleDefinitionId: storageBlobDataReaderRoleDefinition.id
  }
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

// Assign Search Service Contributor role to testApplicationOid
resource testApp_search_contributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(searchServiceContributorRoleDefinition.id, testApplicationOid, search.id)
  scope: search
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: searchServiceContributorRoleDefinition.id
  }
}
