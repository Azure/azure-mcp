targetScope = 'resourceGroup'

@minLength(3)
@maxLength(24)
@description('The base resource name.')
param baseName string = resourceGroup().name

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@description('The tenant ID to which the application and resources belong.')
param tenantId string = '72f988bf-86f1-41af-91ab-2d7cd011db47'

@description('The client OID to grant access to test resources.')
param testApplicationOid string

resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' = {
  location: location
  name: baseName
  properties: {
    tenantId: subscription().tenantId
    sku: {
      family: 'A'
      name: 'standard'
    }
    enableSoftDelete: true
    softDeleteRetentionInDays: 90
    enableRbacAuthorization: true
  }
}

resource secretsUserRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Key Vault Reader role, which is the minimum role permission we can give.
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#key-vault
  name: '4633458b-17de-408a-b874-0445c86b69e6'
}

resource keyUserRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Key Vault Crypto Officer role.  Allows user to read/write/delete keys.
  name: '14b46e9e-c2b7-41b4-b07b-48a6ebf60603'
}

resource vaultRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' =  {
  name: guid(secretsUserRoleDefinition.id, testApplicationOid, keyVault.id)
  scope: keyVault
  properties:{
    principalId: testApplicationOid
    roleDefinitionId: secretsUserRoleDefinition.id
  }
}

resource vaultRoleAssignment2 'Microsoft.Authorization/roleAssignments@2022-04-01' =  {
  name: guid(keyUserRoleDefinition.id, testApplicationOid, keyVault.id)
  scope: keyVault
  properties:{
    principalId: testApplicationOid
    roleDefinitionId: keyUserRoleDefinition.id
  }
}

resource keyVaultKey 'Microsoft.KeyVault/vaults/keys@2021-11-01-preview' = {
  parent: keyVault
  name: 'foo-bar'
  properties: {
    keySize: 2048
    keyOps: [
      'encrypt'
      'decrypt'
      'sign'
      'verify'
      'wrapKey'
      'unwrapKey'
    ]
    kty: 'RSA'
  }
}

resource secret 'Microsoft.KeyVault/vaults/secrets@2023-07-01' = {
  name: 'foo-bar-secret'
  parent: keyVault
  properties: {
    value: 'foo-bar-value'
  }
}
