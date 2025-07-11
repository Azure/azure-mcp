targetScope = 'resourceGroup'

@minLength(3)
@maxLength(15)
@description('The base resource name. VM names have specific length restrictions.')
param baseName string = resourceGroup().name

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@description('The client OID to grant access to test resources.')
param testApplicationOid string

@description('Administrator username for the VM.')
param adminUsername string = 'azureuser'

@description('Administrator password for the VM.')
@secure()
param adminPassword string = newGuid()

// Virtual Network
resource vnet 'Microsoft.Network/virtualNetworks@2023-04-01' = {
  name: '${baseName}-vnet'
  location: location
  properties: {
    addressSpace: {
      addressPrefixes: [
        '10.0.0.0/16'
      ]
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

// Network Security Group
resource nsg 'Microsoft.Network/networkSecurityGroups@2023-04-01' = {
  name: '${baseName}-nsg'
  location: location
  properties: {
    securityRules: [
      {
        name: 'AllowSSH'
        properties: {
          protocol: 'Tcp'
          sourcePortRange: '*'
          destinationPortRange: '22'
          sourceAddressPrefix: '*'
          destinationAddressPrefix: '*'
          access: 'Allow'
          priority: 1001
          direction: 'Inbound'
        }
      }
    ]
  }
}

// Public IP
resource publicIp 'Microsoft.Network/publicIPAddresses@2023-04-01' = {
  name: '${baseName}-vm-ip'
  location: location
  properties: {
    publicIPAllocationMethod: 'Dynamic'
  }
}

// Network Interface
resource networkInterface 'Microsoft.Network/networkInterfaces@2023-04-01' = {
  name: '${baseName}-vm-nic'
  location: location
  properties: {
    ipConfigurations: [
      {
        name: 'internal'
        properties: {
          privateIPAllocationMethod: 'Dynamic'
          publicIPAddress: {
            id: publicIp.id
          }
          subnet: {
            id: vnet.properties.subnets[0].id
          }
        }
      }
    ]
    networkSecurityGroup: {
      id: nsg.id
    }
  }
}

// Test Virtual Machine
resource testVm 'Microsoft.Compute/virtualMachines@2023-03-01' = {
  name: '${baseName}-vm'
  location: location
  properties: {
    hardwareProfile: {
      vmSize: 'Standard_B1s'
    }
    osProfile: {
      computerName: '${baseName}vm'
      adminUsername: adminUsername
      adminPassword: adminPassword
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
          id: networkInterface.id
        }
      ]
    }
  }
}

// Role assignment for test application - Virtual Machine Contributor
resource vmContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: '9980e02c-c2be-4d73-94e8-173b1dc7cf3c' // Virtual Machine Contributor
}

resource appVmRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(vmContributorRoleDefinition.id, testApplicationOid, testVm.id)
  scope: testVm
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: vmContributorRoleDefinition.id
    description: 'Virtual Machine Contributor for testApplicationOid'
  }
}

// Reader role for the resource group to list VMs
resource readerRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  name: 'acdd72a7-3385-48ef-bd42-f606fba81ae7' // Reader
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
output vmName string = testVm.name
output vmResourceGroup string = resourceGroup().name
output vmLocation string = location