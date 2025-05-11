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

resource cache 'Microsoft.Cache/redis@2025-04-01' = {
  location: location
  name: '${baseName}_cache'
  properties: {
    enableNonSslPort: false
    minimumTlsVersion: '1.2'
    sku: {
      capacity: 0
      family: 'C'
      name: 'Basic'
    }
    redisConfiguration: {
      'aad-enabled': 'false'
    }
  }
}

resource cluster 'Microsoft.Cache/redisEnterprise@2025-05-01-preview' = {
  location: location
  name: '${baseName}_cluster'
  kind: 'v2'
  sku: {
    name: 'Balanced_B0'
  }
  identity: {
    type: 'None'
  }
  properties: {
    minimumTlsVersion: '1.2'
  }
}
