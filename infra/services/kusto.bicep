targetScope = 'resourceGroup'

@minLength(3)
@maxLength(50)
@description('The base resource name.')
param baseName string = resourceGroup().name

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@description('The tenant ID to which the application and resources belong.')
param tenantId string = '72f988bf-86f1-41af-91ab-2d7cd011db47'

@description('The client OID to grant access to test resources.')
param testApplicationOid string

@description('The type of the test application principal. Lease empty for user.')
param testApplicationType string = ''

var shouldCreateRoleAssignments = testApplicationType == 'App'

// Owner role definition ID for role assignment
var ownerRoleDefinitionId = 'ea037b3f-7b9c-48b6-820f-8f0d04de3690'

resource kustoCluster 'Microsoft.Kusto/clusters@2024-04-13' = {
  name: baseName
  location: location
  sku: {
    name: 'Standard_E2ads_v5'
    tier: 'Standard'
    capacity: 2
  }
  identity: {
    type: 'SystemAssigned'
  }

  resource kustoDatabase 'databases' = {
    location: location
    name: 'ToDoLists'
    kind: 'ReadWrite'
  }

  resource kustoPrincipals 'principalAssignments' = {
    name: guid(kustoCluster.id, testApplicationOid)
    properties: {
      principalId: testApplicationOid
      principalType: 'App'
      role: 'AllDatabasesAdmin'
      tenantId: tenantId
    }
  }
}

// Role assignment for Owner role at resource group scope
resource ownerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(resourceGroup().id, testApplicationOid, ownerRoleDefinitionId)
  properties: {
    principalId: testApplicationOid
    principalType: 'App'
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', ownerRoleDefinitionId)
  }
}
