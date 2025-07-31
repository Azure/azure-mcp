// Live test runs require a resource file, so we use an empty one here.
targetScope = 'resourceGroup'

@minLength(3)
@maxLength(24)
@description('The base resource name.')
param baseName string

@description('The client OID to grant access to test resources.')
param testApplicationOid string = deployer().objectId

var location string = resourceGroup().location
var tenantId string = subscription().tenantId

// Add any additional resources and role assignments needed for live tests here.


// Outputs will be available in test-resources-post.ps1
output location string = location

// Their keys will be uppercase
// $DeploymentOutputs.LOCATION

// Log Analytics workspace specifically for Application Insights
resource workspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: '${baseName}-ai'
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

// Application Insights component
resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: baseName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    Request_Source: 'rest'
    WorkspaceResourceId: workspace.id
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

// Standard availability test for https://doestexist.co/
resource availabilityTest 'Microsoft.Insights/webtests@2022-06-15' = {
  name: '${baseName}-availability-test'
  location: location
  tags: {
    'hidden-link:${applicationInsights.id}': 'Resource'
  }
  properties: {
    Name: '${baseName}-availability-test'
    Description: null
    Enabled: true
    Frequency: 300
    Timeout: 120
    Locations: [
      {
        Id: 'us-ca-sjc-azr'
      }
    ]
    Configuration: null
    Kind: 'standard'
    RetryEnabled: false
    ValidationRules: {
      ContentValidation: null
      ExpectedHttpStatusCode: '200'
      IgnoreHttpStatusCode: false
      SSLCheck: false
      SSLCertRemainingLifetimeCheck: null
    }
    Request: {
      ParseDependentRequests: false
      RequestUrl: 'https://doestexist.co/'
      Headers: null
      HttpVerb: 'GET'
      RequestBody: null
    }
  }
}

// Standard availability test for https://bing.com/
resource availabilityTestSucceeding 'Microsoft.Insights/webtests@2022-06-15' = {
  name: '${baseName}-availability-test-succeeding'
  location: location
  tags: {
    'hidden-link:${applicationInsights.id}': 'Resource'
  }
  properties: {
    Name: '${baseName}-availability-test-succeeding'
    Description: null
    Enabled: true
    Frequency: 300
    Timeout: 120
    Locations: [
      {
        Id: 'us-ca-sjc-azr'
      }
    ]
    Configuration: null
    Kind: 'standard'
    RetryEnabled: false
    ValidationRules: {
      ContentValidation: null
      ExpectedHttpStatusCode: '200'
      IgnoreHttpStatusCode: false
      SSLCheck: false
      SSLCertRemainingLifetimeCheck: null
    }
    Request: {
      ParseDependentRequests: false
      RequestUrl: 'https://bing.com/'
      Headers: null
      HttpVerb: 'GET'
      RequestBody: null
    }
  }
}


// Monitoring Reader role definition
resource monitoringReaderRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Monitoring Reader role
  // Can read all monitoring data
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#monitoring-reader
  name: '43d0d8ad-25c7-4714-9337-8ba259a9fe05'
}

// Role assignment for test application
resource appInsightsRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (!empty(testApplicationOid)) {
  name: guid(monitoringReaderRoleDefinition.id, testApplicationOid, applicationInsights.id)
  scope: applicationInsights
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: monitoringReaderRoleDefinition.id
    description: 'Monitoring Reader for testApplicationOid'
  }
}

// Log Analytics Contributor role for the workspace
resource logAnalyticsContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // This is the Log Analytics Contributor role
  // Can read all monitoring data and edit monitoring settings
  // See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#log-analytics-contributor
  name: '92aaf0da-9dab-42b6-94a3-d43ce8d16293'
}

// Role assignment for workspace
resource workspaceRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (!empty(testApplicationOid)) {
  name: guid(logAnalyticsContributorRoleDefinition.id, testApplicationOid, workspace.id)
  scope: workspace
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: logAnalyticsContributorRoleDefinition.id
    description: 'Log Analytics Contributor for testApplicationOid'
  }
}
