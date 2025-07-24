targetScope = 'resourceGroup'

@minLength(3)
@maxLength(20)
@description('The base resource name. Container App names have specific length restrictions.')
param baseName string = resourceGroup().name

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@description('The client OID to grant access to test resources.')
param testApplicationOid string

// Generate shorter names for Container Apps constraints
var shortName = length(baseName) > 15 ? substring(baseName, 0, 15) : baseName

// Container Apps Managed Environment
resource containerAppEnvironment 'Microsoft.App/managedEnvironments@2022-06-01-preview' = {
  name: '${shortName}-env'
  location: location
  sku: {
    name: 'Consumption'
  }
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

// Log Analytics Workspace for Container Apps
resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: '${shortName}-logs'
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
    features: {
      searchVersion: 1
      legacy: 0
      enableLogAccessUsingOnlyResourcePermissions: true
    }
  }
}

// Test Container App
resource testContainerApp 'Microsoft.App/containerApps@2022-06-01-preview' = {
  name: '${shortName}-app'
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
            weight: 100
            latestRevision: true
          }
        ]
      }
    }
    template: {
      containers: [
        {
          image: 'mcr.microsoft.com/k8se/quickstart:latest'
          name: 'simple-hello-world-container'
          resources: {
            cpu: json('.25')
            memory: '.5Gi'
          }
        }
      ]
      scale: {
        minReplicas: 0
        maxReplicas: 1
      }
    }
  }
}

// Role assignments for test application
resource containerAppContributorRole 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: '358470bc-b998-42bd-ab17-a7e34c199c0f' // Container Apps Contributor
}

resource testAppRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(containerAppContributorRole.id, testApplicationOid, resourceGroup().id)
  properties: {
    roleDefinitionId: containerAppContributorRole.id
    principalId: testApplicationOid
    description: 'Container Apps Contributor for test application'
  }
}

// Outputs for test consumption
output containerAppEnvironmentName string = containerAppEnvironment.name
output testContainerAppName string = testContainerApp.name
output logAnalyticsWorkspaceName string = logAnalyticsWorkspace.name
