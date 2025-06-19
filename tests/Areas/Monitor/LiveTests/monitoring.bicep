// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

targetScope = 'resourceGroup'

@minLength(4)
@maxLength(63)
@description('The base resource name.')
param baseName string

@description('The location of the resource. By default, this is the same as the resource group.')
param location string

@description('The tenant ID to which the application and resources belong.')
param tenantId string

@description('The client OID to grant access to test resources.')
param testApplicationOid string

resource workspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: baseName
  location: location
  properties: {
    retentionInDays: 30
    sku: {
      name: 'PerGB2018'
    }
    features: {
      searchVersion: 1
      workspaceCapping: 'Off'
    }
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}
