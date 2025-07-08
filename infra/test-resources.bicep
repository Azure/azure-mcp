targetScope = 'resourceGroup'

@minLength(5)
@maxLength(20)
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

var staticSuffix = toLower(substring(subscription().subscriptionId, 0, 4))
var staticBaseName = 'mcp${staticSuffix}'
var staticResourceGroupName = 'mcp-static-${staticSuffix}'

// Please keep this module list alphabetical

module appConfiguration 'services/appConfig.bicep' = if (empty(areas) || contains(areas, 'AppConfig')) {
  name: '${deploymentName}-appConfig'
  params: {
    baseName: baseName
    location: location
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

module cosmos 'services/cosmos.bicep' = if (empty(areas) || contains(areas, 'Cosmos')) {
  name: '${deploymentName}-cosmos'
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

module grafana 'services/grafana.bicep' = if (empty(areas) || contains(areas, 'Grafana')) {
  name: '${deploymentName}-grafana'
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

module kusto 'services/kusto.bicep' = if (empty(areas) || contains(areas, 'Kusto')) {
  name: '${deploymentName}-kusto'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module monitoring 'services/monitor.bicep' = if (empty(areas) || contains(areas, 'Monitor')) {
  name: '${deploymentName}-monitor'
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

module aiSearch 'services/search.bicep' = if (empty(areas) || contains(areas, 'Search')) {
  name: '${deploymentName}-search'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
    staticBaseName: staticBaseName
    staticResourceGroupName: staticResourceGroupName
  }
}


module servicebus 'services/servicebus.bicep' = if (empty(areas) || contains(areas, 'ServiceBus')) {
  name: '${deploymentName}-servicebus'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module sql 'services/sql.bicep' = if (empty(areas) || contains(areas, 'Sql')) {
  name: '${deploymentName}-sql'
  params: {
    baseName: baseName
    location: 'westus2'
    testApplicationOid: testApplicationOid
  }
}

module storage 'services/storage.bicep' = if (empty(areas) || contains(areas, 'Storage')) {
  name: '${deploymentName}-storage'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}
