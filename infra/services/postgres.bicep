targetScope = 'resourceGroup'

param testApplicationOid string
param tenantId string = '72f988bf-86f1-41af-91ab-2d7cd011db47'
param location string = resourceGroup().location
param baseName string = resourceGroup().name
param serverEdition string = 'GeneralPurpose'
param skuSizeGB int = 128
param dbInstanceType string = 'Standard_D4ds_v4'
param haMode string = 'Disabled'
param version string = '16'
param virtualNetworkExternalId string = ''
param subnetName string = ''
param privateDnsZoneArmResourceId string = ''

resource postgres_server 'Microsoft.DBforPostgreSQL/flexibleServers@2024-08-01' = {
  name: baseName
  location: location
  sku: {
    name: dbInstanceType
    tier: serverEdition
  }
  properties: {
    version: version
    authConfig: {
      activeDirectoryAuth: 'Enabled'
      passwordAuth: 'Disabled'
      tenantId: tenantId
    }
    network: {
      delegatedSubnetResourceId: (empty(virtualNetworkExternalId) ? json('null') : json('\'${virtualNetworkExternalId}/subnets/${subnetName}\''))
      privateDnsZoneArmResourceId: (empty(virtualNetworkExternalId) ? json('null') : privateDnsZoneArmResourceId)
    }
    highAvailability: {
      mode: haMode
    }
    storage: {
      storageSizeGB: skuSizeGB
    }
    backup: {
      backupRetentionDays: 7
      geoRedundantBackup: 'Disabled'
    }
  }
}

// resource blobContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
//   scope: subscription()
//   // This is the role to allow backup vault to access PostgreSQL Flexible Server Resource APIs for Long Term Retention Backup.
//   // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#databases
//   name: 'c088a766-074b-43ba-90d4-1fb21feae531'
// }

// resource appBlobRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' =  {
//   name: guid(blobContributorRoleDefinition.id, testApplicationOid, postgres_server.id)
//   scope: postgres_server
//   properties:{
//     principalId: testApplicationOid
//     roleDefinitionId: blobContributorRoleDefinition.id
//     description: 'Blob Contributor for testApplicationOid'
//   }
// }
