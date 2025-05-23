targetScope = 'resourceGroup'

@minLength(3)
@maxLength(50)
@description('The base resource name.')
param baseName string = resourceGroup().name

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@description('The tenant ID to which the application and resources belong.')
param tenantId string = '72f988bf-86f1-41af-91ab-2d7cd011db47'

@description('The client OID to grant access to test resources.')
param testApplicationOid string

var cognitiveServicesContributorRoleId = '25fbc0a9-bd7c-42a3-aa1a-3b75d497ee68' // Cognitive Services Contributor role

resource aiServicesAccount 'Microsoft.CognitiveServices/accounts@2023-10-01-preview' = {
  name: baseName
  location: location
  kind: 'AIServices'
  identity: {
    type: 'SystemAssigned'
  }
  sku: {
    name: 'S0'
  }
  properties: {
    customSubDomainName: baseName
    publicNetworkAccess: 'Enabled'
    disableLocalAuth: true
    dynamicThrottlingEnabled: false
    networkAcls: {
      defaultAction: 'Allow'
    }
    encryption: {
      keySource: 'Microsoft.CognitiveServices'
    }
  }
}

resource contributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(cognitiveServicesContributorRoleId, testApplicationOid, aiServicesAccount.id)
  scope: aiServicesAccount
  properties: {
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', cognitiveServicesContributorRoleId)
    principalId: testApplicationOid
    principalType: 'ServicePrincipal'
  }
}

resource aiProjects 'Microsoft.MachineLearningServices/workspaces@2023-10-01' = {
  name: '${baseName}-ai-projects'
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    friendlyName: '${baseName} AI Projects'
    description: 'Azure AI Projects for Foundry operations'
    storageAccount: '/subscriptions/${subscription().subscriptionId}/resourceGroups/${resourceGroup().name}/providers/Microsoft.Storage/storageAccounts/${baseName}'
    hbiWorkspace: false
    allowPublicAccessWhenBehindVnet: false
    publicNetworkAccess: 'Enabled'
    v1LegacyMode: false
  }
  sku: {
    name: 'Basic'
    tier: 'Basic'
  }
}

resource aiProjectsRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(cognitiveServicesContributorRoleId, testApplicationOid, aiProjects.id)
  scope: aiProjects
  properties: {
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', cognitiveServicesContributorRoleId)
    principalId: testApplicationOid
    principalType: 'ServicePrincipal'
  }
}

resource modelDeployment 'Microsoft.CognitiveServices/accounts/deployments@2023-10-01-preview' = {
  parent: aiServicesAccount
  name: 'gpt-4o'
  sku: {
    name: 'Standard'
    capacity: 1
  }
  properties: {
    model: {
      format: 'OpenAI'
      name: ' gpt-4o'
    }
    scaleSettings: {
      scaleType: 'Standard'
    }
  }
}
