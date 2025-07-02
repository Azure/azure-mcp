targetScope = 'resourceGroup'

@minLength(5)
@maxLength(24)
@description('The base resource name.')
param baseName string = resourceGroup().name

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@description('The tenant ID to which the application and resources belong.')
param tenantId string = '72f988bf-86f1-41af-91ab-2d7cd011db47'

@description('The client OID to grant access to test resources.')
param testApplicationOid string

@description('The names of areas to deploy.  An area should deploy if this list is empty, contains "Common", or contains the area name.')
param areas string[] = []

var deploymentName = deployment().name

module storage 'services/storage.bicep' = if (empty(areas) || contains(areas, 'Storage')) {
  name: '${deploymentName}-storage'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module cosmos 'services/cosmos.bicep' = if (empty(areas) || contains(areas, 'Cosmos')) {
  name: '${deploymentName}-cosmos'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module appConfiguration 'services/appConfiguration.bicep' = if (empty(areas) || contains(areas, 'AppConfiguration')) {
  name: '${deploymentName}-appConfiguration'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module monitoring 'services/monitoring.bicep' = if (empty(areas) || contains(areas, 'Monitoring')) {
  name: '${deploymentName}-monitoring'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module keyvault 'services/keyvault.bicep' = if (empty(areas) || contains(areas, 'KeyVault')) {
  name: '${deploymentName}-keyvault'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module servicebus 'services/servicebus.bicep' = if (empty(areas) || contains(areas, 'Servicebus')) {
  name: '${deploymentName}-servicebus'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module redis 'services/redis.bicep' = if (empty(areas) || contains(areas, 'Redis')) {
  name: '${deploymentName}-redis'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module kusto 'services/kusto.bicep' = if (empty(areas) || contains(areas, 'Kusto')) {
  name: '${deploymentName}-kusto'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module foundry 'services/foundry.bicep' = if (contains(areas, 'Foundry')) {
  name: '${deploymentName}-foundry'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

// This module is conditionally deployed only for the specific tenant ID.
module azureIsv 'services/azureIsv.bicep' = if ((empty(areas) || contains(areas, 'AzureIsv')) && tenantId == '888d76fa-54b2-4ced-8ee5-aac1585adee7') {
  name: '${deploymentName}-azureIsv'
  params: {
    baseName: baseName
    location: 'westus2'
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module authorization 'services/authorization.bicep' = if (empty(areas) || contains(areas, 'Authorization')) {
  name: '${deploymentName}-authorization'
  params: {
    testApplicationOid: testApplicationOid
  }
}
