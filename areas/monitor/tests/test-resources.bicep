targetScope = 'resourceGroup'

@minLength(4)
@maxLength(21)
@description('The base resource name.')
param baseName string = resourceGroup().name

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@description('The tenant ID to which the application and resources belong.')
param tenantId string = '72f988bf-86f1-41af-91ab-2d7cd011db47'

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

// Create a custom table for ingestion testing
resource customTable 'Microsoft.OperationalInsights/workspaces/tables@2022-10-01' = {
  parent: workspace
  name: 'TestLogs_CL'
  properties: {
    totalRetentionInDays: 30
    plan: 'Analytics'
    schema: {
      name: 'TestLogs_CL'
      columns: [
        {
          name: 'TimeGenerated'
          type: 'datetime'
          description: 'The time the log was generated'
        }
        {
          name: 'Message'
          type: 'string'
          description: 'The log message'
        }
        {
          name: 'Level'
          type: 'string'
          description: 'The log level'
        }
        {
          name: 'Source'
          type: 'string'
          description: 'The source of the log'
        }
      ]
    }
  }
}

// Create a Data Collection Rule for ingestion testing
resource dataCollectionRule 'Microsoft.Insights/dataCollectionRules@2023-03-11' = {
  name: 'test-dcr'
  location: location
  properties: {
    destinations: {
      logAnalytics: [
        {
          workspaceResourceId: workspace.id
          name: 'workspace-destination'
        }
      ]
    }
    dataFlows: [
      {
        streams: [
          'Custom-TestLogs'
        ]
        destinations: [
          'workspace-destination'
        ]
        transformKql: 'source'
        outputStream: 'Custom-TestLogs_CL'
      }
    ]
    streamDeclarations: {
      'Custom-TestLogs': {
        columns: [
          {
            name: 'TimeGenerated'
            type: 'datetime'
          }
          {
            name: 'Message'
            type: 'string'
          }
          {
            name: 'Level'
            type: 'string'
          }
          {
            name: 'Source'
            type: 'string'
          }
        ]
      }
    }
  }
}

// Create a storage account to monitor
resource storageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: '${baseName}mon'
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    allowSharedKeyAccess: false
  }

  resource blobServices 'blobServices' = {
    name: 'default'
    resource fooContainer 'containers' = { name: 'foo' }
    resource barContainer 'containers' = { name: 'bar' }
    resource bazContainer 'containers' = { name: 'baz' }
  }

  resource tableServices 'tableServices' = {
    name: 'default'
    resource fooTable 'tables' = { name: 'foo' }
    resource barTable 'tables' = { name: 'bar' }
    resource bazTable 'tables' = { name: 'baz' }
  }
}

// Diagnostic settings for Storage Account (main account level)
resource storageAccountDiagnostics 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = {
  name: 'storage-account-diagnostics'
  scope: storageAccount
  properties: {
    workspaceId: workspace.id
    metrics: [
      {
        category: 'Transaction'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
      {
        category: 'Capacity'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
    ]
  }
}

// Diagnostic settings for Blob Service
resource blobServiceDiagnostics 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = {
  name: 'blob-service-diagnostics'
  scope: storageAccount::blobServices
  properties: {
    workspaceId: workspace.id
    logs: [
      {
        category: 'StorageRead'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
      {
        category: 'StorageWrite'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
      {
        category: 'StorageDelete'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
    ]
    metrics: [
      {
        category: 'Transaction'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
      {
        category: 'Capacity'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
    ]
  }
}

// Diagnostic settings for Table Service
resource tableServiceDiagnostics 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = {
  name: 'table-service-diagnostics'
  scope: storageAccount::tableServices
  properties: {
    workspaceId: workspace.id
    logs: [
      {
        category: 'StorageRead'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
      {
        category: 'StorageWrite'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
      {
        category: 'StorageDelete'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
    ]
    metrics: [
      {
        category: 'Transaction'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
      {
        category: 'Capacity'
        enabled: true
        retentionPolicy: {
          enabled: false
          days: 0
        }
      }
    ]
  }
}

// Role assignment for the test application to access the storage account
resource blobContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Storage Blob Data Contributor role.
  // Read, write, and delete Azure Storage containers and blobs
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#storage
  name: 'ba92f5b4-2d11-453d-a403-e96b0029c9fe'
}

resource appBlobRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' =  {
  name: guid(blobContributorRoleDefinition.id, testApplicationOid, storageAccount.id)
  scope: storageAccount
  properties:{
    principalId: testApplicationOid
    roleDefinitionId: blobContributorRoleDefinition.id
    description: 'Blob Contributor for testApplicationOid'
  }
}

// Role assignment for Monitoring Metrics Publisher to enable ingestion
resource monitoringMetricsPublisherRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Monitoring Metrics Publisher role
  // Enables publishing metrics against Azure resources
  name: '3913510d-42f4-4e42-8a64-420c390055eb'
}

resource appMonitoringMetricsPublisherRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(monitoringMetricsPublisherRoleDefinition.id, testApplicationOid, dataCollectionRule.id)
  scope: dataCollectionRule
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: monitoringMetricsPublisherRoleDefinition.id
    description: 'Monitoring Metrics Publisher for DCR ingestion'
  }
}

// Add Log Analytics Contributor role for the test application on the workspace
resource logAnalyticsContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Log Analytics Contributor role
  name: '92aaf0da-9dab-42b6-94a3-d43ce8d16293'
}

resource appLogAnalyticsContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(logAnalyticsContributorRoleDefinition.id, testApplicationOid, workspace.id)
  scope: workspace
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: logAnalyticsContributorRoleDefinition.id
    description: 'Log Analytics Contributor for workspace access'
  }
}
