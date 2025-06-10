targetScope = 'resourceGroup'

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@maxLength(8)
@description('A unique prefix for the resource names. This will be empty in pipeline deployments.')
param uniquePrefix string = ''

var deploymentName = deployment().name

module aiSearch 'services/aiSearch-static.bicep' = {
  name: '${deploymentName}-aiSearch'
  params: {
    location: location
    uniquePrefix: uniquePrefix
  }
}
