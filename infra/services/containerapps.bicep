targetScope = 'resourceGroup'

@minLength(3)
@maxLength(17)
@description('The base resource name. Container Apps environments have specific length restrictions.')
param baseName string = resourceGroup().name

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@description('The client OID to grant access to test resources.')
param testApplicationOid string

// Container Apps Environment
resource containerAppEnvironment 'Microsoft.App/managedEnvironments@2023-05-01' = {
  name: '${baseName}-env'
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalyticsWorkspace.properties.customerId
        sharedKey: logAnalyticsWorkspace.listKeys().primarySharedKey
      }
    }
  }
}

// Log Analytics Workspace for Container Apps Environment
resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: '${baseName}-logs'
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
  }
}

// Test Container App
resource testContainerApp 'Microsoft.App/containerApps@2023-05-01' = {
  name: '${baseName}-testapp'
  location: location
  properties: {
    managedEnvironmentId: containerAppEnvironment.id
    configuration: {
      ingress: {
        external: true
        targetPort: 80
        allowInsecure: false
        traffic: [
          {
            latestRevision: true
            weight: 100
          }
        ]
      }
    }
    template: {
      containers: [
        {
          name: 'test-container'
          image: 'mcr.microsoft.com/k8se/quickstart:latest'
          resources: {
            cpu: json('0.25')
            memory: '0.5Gi'
          }
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 3
      }
    }
  }
}

// Container Apps Contributor role assignment for test application
resource containerAppsContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // Container Apps Contributor role
  // See https://learn.microsoft.com/azure/role-based-access-control/built-in-roles
  name: 'ad2dd5fb-cd4b-4fd4-a9b6-4fed3630980a'
}

resource appContainerAppsRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(containerAppsContributorRoleDefinition.id, testApplicationOid, resourceGroup().id)
  scope: resourceGroup()
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: containerAppsContributorRoleDefinition.id
    description: 'Container Apps Contributor for testApplicationOid'
  }
}

// Reader role assignment for broader access
resource readerRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: 'acdd72a7-3385-48ef-bd42-f606fba81ae7'
}

resource appReaderRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(readerRoleDefinition.id, testApplicationOid, resourceGroup().id)
  scope: resourceGroup()
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: readerRoleDefinition.id
    description: 'Reader for testApplicationOid'
  }
}

// Outputs for test consumption
output containerAppEnvironmentName string = containerAppEnvironment.name
output testContainerAppName string = testContainerApp.name
output logAnalyticsWorkspaceName string = logAnalyticsWorkspace.name