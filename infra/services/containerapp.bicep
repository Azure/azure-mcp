targetScope = 'resourceGroup'

@minLength(3)
@maxLength(17)
@description('The base resource name. Container Registry names have specific length restrictions.')
param baseName string = resourceGroup().name

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@description('The client OID to grant access to test resources.')
param testApplicationOid string

// Create a test Container Registry for live tests
resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-07-01' = {
  name: '${baseName}cr'
  location: location
  sku: {
    name: 'Basic'
  }
  properties: {
    adminUserEnabled: false
    publicNetworkAccess: 'Enabled'
  }
}

// Role assignment for Contributor access to the test Container Registry
resource contributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Contributor role
  // Grants full access to manage all resources, but does not allow you to assign roles in Azure RBAC
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#contributor
  name: 'b24988ac-6180-42a0-ab88-20f7382dd24c'
}

resource appContainerRegistryRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(contributorRoleDefinition.id, testApplicationOid, containerRegistry.id)
  scope: containerRegistry
  properties: {
    roleDefinitionId: contributorRoleDefinition.id
    principalId: testApplicationOid
    principalType: 'ServicePrincipal'
  }
}

// Outputs for test consumption
output containerRegistryName string = containerRegistry.name
output containerRegistryLoginServer string = containerRegistry.properties.loginServer
