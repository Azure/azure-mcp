# Authentication and Security for Azure MCP Server

This document provides comprehensive guidance for Azure MCP Server authentication and related security considerations in enterprise environments. While the core focus is authentication, it also covers network and security configurations that commonly affect authentication in enterprise scenarios.

## Authentication Fundamentals

Azure MCP Server uses the Azure Identity SDK's [`DefaultAzureCredential`](https://learn.microsoft.com/dotnet/azure/sdk/authentication/credential-chains?tabs=dac) for authentication, which tries credentials in the following order:

1. **Environment Variables** (`EnvironmentCredential`) - Perfect for CI/CD pipelines
2. **Shared Token Cache** (`SharedTokenCacheCredential`) - Uses cached tokens from other tools
3. **Visual Studio** (`VisualStudioCredential`) - Uses your Visual Studio credentials
4. **Azure CLI** (`AzureCliCredential`) - Uses your existing Azure CLI login
5. **Azure PowerShell** (`AzurePowerShellCredential`) - Uses your Az PowerShell login
6. **Azure Developer CLI** (`AzureDeveloperCliCredential`) - Uses your azd login
7. **Interactive Browser** (`InteractiveBrowserCredential`) - Falls back to browser-based login if needed

For production scenarios, additional credentials can be enabled:
- **Managed Identity** (`ManagedIdentityCredential`) - For Azure-hosted applications
- **Workload Identity** (`WorkloadIdentityCredential`) - For Kubernetes workloads

## Authentication Scenarios in Enterprise Environments

### Authentication with Protected Resources (Local Auth Disabled)

Many organizations disable local authentication methods (access keys, SAS tokens) on their Azure resources for security compliance. This affects resources like:

- Azure Storage accounts
- Azure Cosmos DB
- Azure Key Vault (in some configurations)
- Azure Service Bus
- Azure Event Hubs

#### What You Need to Know

When local authentication is disabled, Azure MCP Server must use **Azure Entra ID (formerly Azure AD)** authentication exclusively. This requires:

1. **Proper RBAC permissions** on the target resources
2. **Network connectivity** to Azure Entra ID endpoints
3. **Valid Azure Entra ID credentials** (not personal Microsoft accounts)

#### Working with Your Resource Administrator

**Information to Provide to Your Admin:**

1. **Service Principal Requirements:**
   ```
   Application Name: Azure MCP Server Access
   Required Permissions: 
   - Resource-specific data plane roles (e.g., Storage Blob Data Reader)
   - Subscription/Resource Group reader permissions (for discovery)
   ```

2. **Network Requirements:**
   ```
   Required Endpoints:
   - login.microsoftonline.com (Azure AD authentication)
   - management.azure.com (Azure Resource Manager)
   - Resource-specific endpoints (e.g., *.blob.core.windows.net)
   ```

3. **RBAC Role Assignments Needed:**
   ```
   Scope: Subscription, Resource Group, or specific Resource
   Principal: Your user account or service principal
   Roles: Service-specific data plane roles
   ```

**Questions to Ask Your Admin:**

- Is local authentication disabled on the target resources?
- What RBAC roles are available for data plane access?
- Are there any Conditional Access policies that might block authentication?
- Is there a preferred authentication method (user vs. service principal)?
- Are there network restrictions (private endpoints, firewall rules)?

### Authentication Through Network Restrictions

Organizations often implement network restrictions that can affect Azure MCP Server's ability to authenticate and access resources. While these are network security configurations, they directly impact authentication flows.

#### Common Network Restrictions

1. **Corporate Firewalls**
   - Outbound HTTPS filtering
   - Proxy server requirements
   - Limited allowed domains

2. **Azure Firewall Rules**
   - IP address restrictions on Azure resources
   - Virtual network integration requirements
   - Private endpoint configurations

3. **Conditional Access Policies**
   - Device compliance requirements
   - Location-based restrictions
   - Multi-factor authentication requirements

#### Firewall Configuration Requirements

**Essential Endpoints for Authentication:**
```
login.microsoftonline.com:443
login.windows.net:443
management.azure.com:443
graph.microsoft.com:443
```

**Resource-Specific Endpoints:**
```
Storage: *.blob.core.windows.net:443, *.table.core.windows.net:443
Key Vault: *.vault.azure.net:443
Cosmos DB: *.documents.azure.com:443
Service Bus: *.servicebus.windows.net:443
```

**Working with Network Administrators:**

1. **Request Firewall Rules:**
   ```
   Source: Your IP address or network range
   Destination: Azure service endpoints (see above)
   Protocol: HTTPS (TCP/443)
   ```

2. **Proxy Configuration:**
   ```bash
   # Set proxy environment variables if required
   export HTTP_PROXY=http://proxy.company.com:8080
   export HTTPS_PROXY=http://proxy.company.com:8080
   export NO_PROXY=localhost,127.0.0.1
   ```

3. **Corporate Certificate Trust:**
   - Ensure corporate CA certificates are trusted
   - May require certificate bundle updates

### Authentication with Private Endpoints

When Azure resources are configured with private endpoints, authentication flows require additional network configuration to reach the authentication endpoints.

#### DNS Configuration

Private endpoints require proper DNS resolution:

```bash
# Verify DNS resolution for private endpoints
nslookup mystorageaccount.blob.core.windows.net
# Should resolve to private IP (10.x.x.x range)
```

#### Network Connectivity

Ensure your development environment can reach the private endpoint:

1. **VPN Connection** to the corporate network
2. **ExpressRoute** connectivity
3. **Point-to-site VPN** configuration
4. **Bastion Host** or jump server access

#### Working with Network Administrators

**Information to Provide:**

1. **Resource Details:**
   ```
   Resource Name: mystorageaccount
   Private Endpoint Name: mystorageaccount-pe
   Required DNS Zone: privatelink.blob.core.windows.net
   ```

2. **Network Requirements:**
   ```
   Source Network: Developer workstation network
   Destination Network: Private endpoint subnet
   Ports: TCP/443 for HTTPS
   ```

### Service Principal Authentication in Restricted Environments

For automated scenarios or when interactive authentication isn't possible due to security policies:

#### Creating Service Principal

Work with your Azure administrator to create a service principal:

```bash
# Admin runs this command
az ad sp create-for-rbac --name "azure-mcp-server" --role "Reader" --scopes "/subscriptions/{subscription-id}"
```

#### Configuration

Set environment variables for service principal authentication:

```bash
export AZURE_CLIENT_ID="your-client-id"
export AZURE_CLIENT_SECRET="your-client-secret"
export AZURE_TENANT_ID="your-tenant-id"
```

#### Security Best Practices

1. **Least Privilege:** Only assign necessary permissions
2. **Secret Rotation:** Regularly rotate client secrets
3. **Certificate Authentication:** Prefer certificates over secrets when possible
4. **Monitoring:** Enable audit logging for service principal usage

### Authentication with Conditional Access Policies

Organizations may enforce Conditional Access policies that affect authentication flows:

#### Common Policy Requirements

1. **Device Compliance**
   - Device must be Azure AD joined
   - Device must meet compliance policies
   - Intune enrollment may be required

2. **Multi-Factor Authentication (MFA)**
   - Additional authentication factors required
   - May not work with non-interactive scenarios

3. **Location Restrictions**
   - Authentication only allowed from specific IP ranges
   - VPN connection may be required

#### Working with Identity Administrators

**Questions to Ask:**

- Are there Conditional Access policies affecting my authentication?
- Is my device compliant with organizational policies?
- Can I get an exception for development scenarios?
- Are there specific authentication methods I should use?

### Troubleshooting Authentication in Enterprise Environments

#### Diagnostic Commands

1. **Test Authentication:**
   ```bash
   az login --tenant your-tenant-id
   az account show
   ```

2. **Test Resource Access:**
   ```bash
   # Test storage access
   az storage blob list --account-name mystorageaccount --container-name mycontainer --auth-mode login
   ```

3. **Network Connectivity:**
   ```bash
   # Test endpoint connectivity
   curl -I https://login.microsoftonline.com
   telnet mystorageaccount.blob.core.windows.net 443
   ```

#### Common Error Patterns

1. **DNS Resolution Failures:**
   ```
   Error: getaddrinfo ENOTFOUND mystorageaccount.privatelink.blob.core.windows.net
   Solution: Configure DNS for private endpoints
   ```

2. **Certificate Trust Issues:**
   ```
   Error: UNABLE_TO_VERIFY_LEAF_SIGNATURE
   Solution: Install corporate CA certificates
   ```

3. **Firewall Blocks:**
   ```
   Error: connect ETIMEDOUT
   Solution: Configure firewall rules for Azure endpoints
   ```

## Authentication Configuration by Environment

### Development Environment

For local development with minimal restrictions:

```bash
# Use Azure CLI authentication
az login
# Verify access
az account show
```

### CI/CD Pipelines

For automated builds and deployments:

```bash
# Use service principal
export AZURE_CLIENT_ID="pipeline-sp-client-id"
export AZURE_CLIENT_SECRET="pipeline-sp-secret"
export AZURE_TENANT_ID="your-tenant-id"
```

### Production Environments

For applications running in Azure:

```bash
# Enable managed identity
export AZURE_MCP_INCLUDE_PRODUCTION_CREDENTIALS=true
```

For more details on these authentication methods, see:
- [Managed Identity documentation](https://learn.microsoft.com/azure/active-directory/managed-identities-azure-resources/)
- [Workload Identity Federation documentation](https://learn.microsoft.com/azure/active-directory/workload-identities/workload-identity-federation)

## Getting Help

When working with administrators, provide:

1. **Specific Error Messages:** Include full error text and stack traces
2. **Resource Details:** Names, regions, and configuration details
3. **Network Context:** Where you're connecting from and network setup
4. **Authentication Method:** Which credential type you're trying to use
5. **Logs:** Relevant log entries showing the authentication attempt

For additional support, see the [Troubleshooting Guide](https://github.com/Azure/azure-mcp/blob/main/TROUBLESHOOTING.md).

If you need further assistance, you can request help by [filing a GitHub issue](https://github.com/Azure/azure-mcp/issues/new) in this repository.