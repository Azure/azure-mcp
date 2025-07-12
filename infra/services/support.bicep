targetScope = 'subscription'

@minLength(3)
@maxLength(17)

@description('The client OID to grant access to test resources.')
param testApplicationOid string

// Support tickets are subscription-level resources that don't require additional infrastructure
// The test application needs Support Request Contributor role to read support tickets
resource supportRequestContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  // This is the Support Request Contributor role
  // Create and manage Support requests
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#support-request-contributor
  name: 'cfd33db0-3dd1-45e3-aa9d-cdbdf3b6f24e'
}

resource supportContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(supportRequestContributorRoleDefinition.id, testApplicationOid, subscription().id)
  properties: {
    roleDefinitionId: supportRequestContributorRoleDefinition.id
    principalId: testApplicationOid
  }
}

// Also assign Reader role for general access to subscription resources
resource readerRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  name: 'acdd72a7-3385-48ef-bd42-f606fba81ae7'
}

resource readerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(readerRoleDefinition.id, testApplicationOid, subscription().id)
  properties: {
    roleDefinitionId: readerRoleDefinition.id
    principalId: testApplicationOid
  }
}

// Outputs for test consumption (no specific resources created for support tickets)
output supportEnabled bool = true
output subscriptionScopeRoleAssigned bool = true
