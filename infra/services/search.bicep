// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

@description('The base resource name for the Search service and index.')
param baseName string
@description('The location for the Search service.')
param location string
@description('The tenant ID for RBAC assignments.')
param tenantId string
@description('The object ID to grant access to the Search resource.')
param testApplicationOid string

resource search 'Microsoft.Search/searchServices@2023-10-01-preview' = {
  name: baseName
  location: location
  sku: {
    name: 'basic'
  }
  properties: {
    hostingMode: 'default'
    partitionCount: 1
    replicaCount: 1
    publicNetworkAccess: 'enabled'
  }
}

resource index 'Microsoft.Search/searchServices/indexes@2023-10-01-preview' = {
  name: '${search.name}/${baseName}'
  properties: {
    fields: [
      {
        name: 'id'
        type: 'Edm.String'
        key: true
        searchable: false
        filterable: false
        sortable: false
        facetable: false
        retrievable: true
      }
      {
        name: 'content'
        type: 'Edm.String'
        key: false
        searchable: true
        filterable: false
        sortable: false
        facetable: false
        retrievable: true
      }
    ]
  }
  dependsOn: [
    search
  ]
}

resource rbac 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(search.id, testApplicationOid, 'search-contributor')
  scope: search
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '7ca78c08-252a-4471-8644-bb5ff32d4ba0') // Search Service Contributor
    principalId: testApplicationOid
    principalType: 'User'
  }
}
