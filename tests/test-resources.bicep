// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

targetScope = 'resourceGroup'

@minLength(5)
@maxLength(24)
@description('The base resource name.')
param baseName string

@description('The location of the resource. By default, this is the same as the resource group.')
param location string

@description('The tenant ID to which the application and resources belong.')
param tenantId string

@description('The client OID to grant access to test resources.')
param testApplicationOid string

var deploymentName = deployment().name

module storage 'Areas/Storage/LiveTests/storage.bicep' = {
  name: '${deploymentName}-storage'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module cosmos 'Areas/Cosmos/LiveTests/cosmos.bicep' = {
  name: '${deploymentName}-cosmos'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module appConfiguration 'Areas/AppConfig/LiveTests/appConfiguration.bicep' = {
  name: '${deploymentName}-appConfiguration'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module monitoring 'Areas/Monitor/LiveTests/monitoring.bicep' = {
  name: '${deploymentName}-monitoring'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module keyvault 'Areas/KeyVault/LiveTests/keyvault.bicep' = {
  name: '${deploymentName}-keyvault'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module servicebus 'Areas/ServiceBus/LiveTests/servicebus.bicep' = {
  name: '${deploymentName}-servicebus'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module redis 'Areas/Redis/LiveTests/redis.bicep' = {
  name: '${deploymentName}-redis'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module kusto 'Areas/Kusto/LiveTests/kusto.bicep' = {
  name: '${deploymentName}-kusto'
  params: {
    baseName: baseName
    location: location
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

// This module is conditionally deployed only for the specific tenant ID.
module azureIsv 'Areas/AzureIsv/LiveTests/azureIsv.bicep' = if (tenantId == '888d76fa-54b2-4ced-8ee5-aac1585adee7') {
  name: '${deploymentName}-azureIsv'
  params: {
    baseName: baseName
    location: 'westus2'
    tenantId: tenantId
    testApplicationOid: testApplicationOid
  }
}

module authorization 'Areas/Authorization/LiveTests/authorization.bicep' = {
  name: '${deploymentName}-authorization'
  params: {
    testApplicationOid: testApplicationOid
  }
}
