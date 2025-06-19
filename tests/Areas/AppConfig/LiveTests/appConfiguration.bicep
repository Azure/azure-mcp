// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

targetScope = 'resourceGroup'

@minLength(5)
@maxLength(50)
@description('The base resource name.')
param baseName string

@description('The location of the resource. By default, this is the same as the resource group.')
param location string

@description('The tenant ID to which the application and resources belong.')
param tenantId string

@description('The client OID to grant access to test resources.')
param testApplicationOid string

resource appConfig 'Microsoft.AppConfiguration/configurationStores@2022-05-01' = {
  name: baseName
  location: location
  sku: {
    name: 'Standard'
  }
  properties: {
    disableLocalAuth: true
  }
}

resource dataOwnerRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the App Configuration Data Owner role.
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#app-configuration-data-owner
  name: '5ae67dd6-50cb-40e7-96ff-dc2bfa4b606b'
}

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' =  {
  name: guid(dataOwnerRoleDefinition.id, testApplicationOid, appConfig.id)
  scope: appConfig
  properties:{
    principalId: testApplicationOid
    roleDefinitionId: dataOwnerRoleDefinition.id
  }
}
