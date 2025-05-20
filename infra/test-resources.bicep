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

var deploymentName = deployment().name

module storage 'services/storage.bicep' = {
  name: '${deploymentName}-storage'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module cosmos 'services/cosmos.bicep' = {
  name: '${deploymentName}-cosmos'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module appConfiguration 'services/appConfiguration.bicep' = {
  name: '${deploymentName}-appConfiguration'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module monitoring 'services/monitoring.bicep' = {
  name: '${deploymentName}-monitoring'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module keyvault 'services/keyvault.bicep' = {
  name: '${deploymentName}-keyvault'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module servicebus 'services/servicebus.bicep' = {
  name: '${deploymentName}-servicebus'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module kusto 'services/kusto.bicep' = {
  name: '${deploymentName}-kusto'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}
