targetScope = 'resourceGroup'

@minLength(3)
@maxLength(17)
@description('The base resource name. Service names have specific length restrictions.')
param baseName string = resourceGroup().name

@description('The client OID to grant access to test resources.')
param testApplicationOid string = deployer().objectId

// The test infrastructure will only provide baseName and testApplicationOid.
// Any additional parameters are for local deployments only and require default values.

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

// Create a simple storage account for testing ResourceHealth
resource testStorageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: take('${baseName}rh${uniqueString(resourceGroup().id)}', 24)
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    allowBlobPublicAccess: false
    minimumTlsVersion: 'TLS1_2'
    supportsHttpsTrafficOnly: true
  }
}

// Create a simple virtual machine for testing ResourceHealth (Basic SKU for cost efficiency)
resource testVirtualNetwork 'Microsoft.Network/virtualNetworks@2023-06-01' = {
  name: '${baseName}-rh-vnet'
  location: location
  properties: {
    addressSpace: {
      addressPrefixes: ['10.0.0.0/16']
    }
    subnets: [
      {
        name: 'default'
        properties: {
          addressPrefix: '10.0.0.0/24'
        }
      }
    ]
  }
}

resource testNetworkInterface 'Microsoft.Network/networkInterfaces@2023-06-01' = {
  name: '${baseName}-rh-nic'
  location: location
  properties: {
    ipConfigurations: [
      {
        name: 'ipconfig1'
        properties: {
          privateIPAllocationMethod: 'Dynamic'
          subnet: {
            id: testVirtualNetwork.properties.subnets[0].id
          }
        }
      }
    ]
  }
}

resource testVirtualMachine 'Microsoft.Compute/virtualMachines@2023-07-01' = {
  name: '${baseName}-rh-vm'
  location: location
  properties: {
    hardwareProfile: {
      vmSize: 'Standard_B1s' // Smallest/cheapest VM size
    }
    osProfile: {
      computerName: '${baseName}rhvm'
      adminUsername: 'azureadmin'
      adminPassword: 'P@ssw0rd123!' // Simple password for test VM
      linuxConfiguration: {
        disablePasswordAuthentication: false
      }
    }
    storageProfile: {
      imageReference: {
        publisher: 'Canonical'
        offer: '0001-com-ubuntu-server-jammy'
        sku: '22_04-lts-gen2'
        version: 'latest'
      }
      osDisk: {
        createOption: 'FromImage'
        managedDisk: {
          storageAccountType: 'Standard_LRS'
        }
      }
    }
    networkProfile: {
      networkInterfaces: [
        {
          id: testNetworkInterface.id
        }
      ]
    }
  }
}

// Role assignment for test application - Reader role includes ResourceHealth permissions
resource readerRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: 'acdd72a7-3385-48ef-bd42-f606fba81ae7' // Reader role
}

resource testApplicationReaderRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(readerRoleDefinition.id, testApplicationOid, resourceGroup().id)
  scope: resourceGroup()
  properties: {
    roleDefinitionId: readerRoleDefinition.id
    principalId: testApplicationOid
    principalType: 'ServicePrincipal'
  }
}

// Outputs for test consumption
output testStorageAccountName string = testStorageAccount.name
output testStorageAccountResourceId string = testStorageAccount.id
output testVirtualMachineName string = testVirtualMachine.name
output testVirtualMachineResourceId string = testVirtualMachine.id
output resourceGroupName string = resourceGroup().name
