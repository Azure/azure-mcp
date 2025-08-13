# Tool Selection Analysis Setup

**Setup completed:** 2025-08-13 11:59:10  
**Tool count:** 105  
**Database setup time:** 6.5478472s  

---

# Tool Selection Analysis Results

**Analysis Date:** 2025-08-13 11:59:10  
**Tool count:** 105  

## Table of Contents

- [Test 1: azmcp-foundry-models-deploy](#test-1)
- [Test 2: azmcp-foundry-models-deployments-list](#test-2)
- [Test 3: azmcp-foundry-models-deployments-list](#test-3)
- [Test 4: azmcp-foundry-models-list](#test-4)
- [Test 5: azmcp-foundry-models-list](#test-5)
- [Test 6: azmcp-search-index-describe](#test-6)
- [Test 7: azmcp-search-index-list](#test-7)
- [Test 8: azmcp-search-index-list](#test-8)
- [Test 9: azmcp-search-index-query](#test-9)
- [Test 10: azmcp-search-service-list](#test-10)
- [Test 11: azmcp-search-service-list](#test-11)
- [Test 12: azmcp-search-service-list](#test-12)
- [Test 13: azmcp-appconfig-account-list](#test-13)
- [Test 14: azmcp-appconfig-account-list](#test-14)
- [Test 15: azmcp-appconfig-account-list](#test-15)
- [Test 16: azmcp-appconfig-kv-delete](#test-16)
- [Test 17: azmcp-appconfig-kv-list](#test-17)
- [Test 18: azmcp-appconfig-kv-list](#test-18)
- [Test 19: azmcp-appconfig-kv-lock](#test-19)
- [Test 20: azmcp-appconfig-kv-set](#test-20)
- [Test 21: azmcp-appconfig-kv-show](#test-21)
- [Test 22: azmcp-appconfig-kv-unlock](#test-22)
- [Test 23: azmcp-extension-az](#test-23)
- [Test 24: azmcp-extension-az](#test-24)
- [Test 25: azmcp-extension-az](#test-25)
- [Test 26: azmcp-acr-registry-list](#test-26)
- [Test 27: azmcp-acr-registry-list](#test-27)
- [Test 28: azmcp-acr-registry-list](#test-28)
- [Test 29: azmcp-acr-registry-list](#test-29)
- [Test 30: azmcp-acr-registry-list](#test-30)
- [Test 31: azmcp-cosmos-account-list](#test-31)
- [Test 32: azmcp-cosmos-account-list](#test-32)
- [Test 33: azmcp-cosmos-account-list](#test-33)
- [Test 34: azmcp-cosmos-database-container-item-query](#test-34)
- [Test 35: azmcp-cosmos-database-container-list](#test-35)
- [Test 36: azmcp-cosmos-database-container-list](#test-36)
- [Test 37: azmcp-cosmos-database-list](#test-37)
- [Test 38: azmcp-cosmos-database-list](#test-38)
- [Test 39: azmcp-kusto-cluster-get](#test-39)
- [Test 40: azmcp-kusto-cluster-list](#test-40)
- [Test 41: azmcp-kusto-cluster-list](#test-41)
- [Test 42: azmcp-kusto-cluster-list](#test-42)
- [Test 43: azmcp-kusto-database-list](#test-43)
- [Test 44: azmcp-kusto-database-list](#test-44)
- [Test 45: azmcp-kusto-query](#test-45)
- [Test 46: azmcp-kusto-sample](#test-46)
- [Test 47: azmcp-kusto-table-list](#test-47)
- [Test 48: azmcp-kusto-table-list](#test-48)
- [Test 49: azmcp-kusto-table-schema](#test-49)
- [Test 50: azmcp-postgres-database-list](#test-50)
- [Test 51: azmcp-postgres-database-list](#test-51)
- [Test 52: azmcp-postgres-database-query](#test-52)
- [Test 53: azmcp-postgres-server-config-get](#test-53)
- [Test 54: azmcp-postgres-server-list](#test-54)
- [Test 55: azmcp-postgres-server-list](#test-55)
- [Test 56: azmcp-postgres-server-list](#test-56)
- [Test 57: azmcp-postgres-server-param](#test-57)
- [Test 58: azmcp-postgres-server-param-set](#test-58)
- [Test 59: azmcp-postgres-table-list](#test-59)
- [Test 60: azmcp-postgres-table-list](#test-60)
- [Test 61: azmcp-postgres-table-schema-get](#test-61)
- [Test 62: azmcp-extension-azd](#test-62)
- [Test 63: azmcp-extension-azd](#test-63)
- [Test 64: azmcp-functionapp-list](#test-64)
- [Test 65: azmcp-functionapp-list](#test-65)
- [Test 66: azmcp-functionapp-list](#test-66)
- [Test 67: azmcp-keyvault-certificate-create](#test-67)
- [Test 68: azmcp-keyvault-certificate-get](#test-68)
- [Test 69: azmcp-keyvault-certificate-get](#test-69)
- [Test 70: azmcp-keyvault-certificate-list](#test-70)
- [Test 71: azmcp-keyvault-certificate-list](#test-71)
- [Test 72: azmcp-keyvault-key-create](#test-72)
- [Test 73: azmcp-keyvault-key-list](#test-73)
- [Test 74: azmcp-keyvault-key-list](#test-74)
- [Test 75: azmcp-keyvault-secret-create](#test-75)
- [Test 76: azmcp-keyvault-secret-list](#test-76)
- [Test 77: azmcp-keyvault-secret-list](#test-77)
- [Test 78: azmcp-aks-cluster-get](#test-78)
- [Test 79: azmcp-aks-cluster-get](#test-79)
- [Test 80: azmcp-aks-cluster-get](#test-80)
- [Test 81: azmcp-aks-cluster-get](#test-81)
- [Test 82: azmcp-aks-cluster-list](#test-82)
- [Test 83: azmcp-aks-cluster-list](#test-83)
- [Test 84: azmcp-aks-cluster-list](#test-84)
- [Test 85: azmcp-loadtesting-test-create](#test-85)
- [Test 86: azmcp-loadtesting-test-get](#test-86)
- [Test 87: azmcp-loadtesting-testresource-create](#test-87)
- [Test 88: azmcp-loadtesting-testresource-list](#test-88)
- [Test 89: azmcp-loadtesting-testrun-create](#test-89)
- [Test 90: azmcp-loadtesting-testrun-get](#test-90)
- [Test 91: azmcp-loadtesting-testrun-list](#test-91)
- [Test 92: azmcp-loadtesting-testrun-update](#test-92)
- [Test 93: azmcp-grafana-list](#test-93)
- [Test 94: azmcp-marketplace-product-get](#test-94)
- [Test 95: azmcp-bestpractices-get](#test-95)
- [Test 96: azmcp-bestpractices-get](#test-96)
- [Test 97: azmcp-bestpractices-get](#test-97)
- [Test 98: azmcp-bestpractices-get](#test-98)
- [Test 99: azmcp-bestpractices-get](#test-99)
- [Test 100: azmcp-bestpractices-get](#test-100)
- [Test 101: azmcp-bestpractices-get](#test-101)
- [Test 102: azmcp-bestpractices-get](#test-102)
- [Test 103: azmcp-bestpractices-get](#test-103)
- [Test 104: azmcp-bestpractices-get](#test-104)
- [Test 105: azmcp-monitor-healthmodels-entity-gethealth](#test-105)
- [Test 106: azmcp-monitor-metrics-definitions](#test-106)
- [Test 107: azmcp-monitor-metrics-definitions](#test-107)
- [Test 108: azmcp-monitor-metrics-definitions](#test-108)
- [Test 109: azmcp-monitor-metrics-query](#test-109)
- [Test 110: azmcp-monitor-metrics-query](#test-110)
- [Test 111: azmcp-monitor-metrics-query](#test-111)
- [Test 112: azmcp-monitor-metrics-query](#test-112)
- [Test 113: azmcp-monitor-metrics-query](#test-113)
- [Test 114: azmcp-monitor-metrics-query](#test-114)
- [Test 115: azmcp-monitor-resource-log-query](#test-115)
- [Test 116: azmcp-monitor-table-list](#test-116)
- [Test 117: azmcp-monitor-table-list](#test-117)
- [Test 118: azmcp-monitor-table-type-list](#test-118)
- [Test 119: azmcp-monitor-table-type-list](#test-119)
- [Test 120: azmcp-monitor-workspace-list](#test-120)
- [Test 121: azmcp-monitor-workspace-list](#test-121)
- [Test 122: azmcp-monitor-workspace-list](#test-122)
- [Test 123: azmcp-monitor-workspace-log-query](#test-123)
- [Test 124: azmcp-datadog-monitoredresources-list](#test-124)
- [Test 125: azmcp-datadog-monitoredresources-list](#test-125)
- [Test 126: azmcp-extension-azqr](#test-126)
- [Test 127: azmcp-extension-azqr](#test-127)
- [Test 128: azmcp-extension-azqr](#test-128)
- [Test 129: azmcp-role-assignment-list](#test-129)
- [Test 130: azmcp-role-assignment-list](#test-130)
- [Test 131: azmcp-redis-cache-accesspolicy-list](#test-131)
- [Test 132: azmcp-redis-cache-accesspolicy-list](#test-132)
- [Test 133: azmcp-redis-cache-list](#test-133)
- [Test 134: azmcp-redis-cache-list](#test-134)
- [Test 135: azmcp-redis-cache-list](#test-135)
- [Test 136: azmcp-redis-cluster-database-list](#test-136)
- [Test 137: azmcp-redis-cluster-database-list](#test-137)
- [Test 138: azmcp-redis-cluster-list](#test-138)
- [Test 139: azmcp-redis-cluster-list](#test-139)
- [Test 140: azmcp-redis-cluster-list](#test-140)
- [Test 141: azmcp-group-list](#test-141)
- [Test 142: azmcp-group-list](#test-142)
- [Test 143: azmcp-group-list](#test-143)
- [Test 144: azmcp-servicebus-queue-details](#test-144)
- [Test 145: azmcp-servicebus-topic-details](#test-145)
- [Test 146: azmcp-servicebus-topic-subscription-details](#test-146)
- [Test 147: azmcp-sql-db-list](#test-147)
- [Test 148: azmcp-sql-db-list](#test-148)
- [Test 149: azmcp-sql-db-show](#test-149)
- [Test 150: azmcp-sql-db-show](#test-150)
- [Test 151: azmcp-sql-elastic-pool-list](#test-151)
- [Test 152: azmcp-sql-elastic-pool-list](#test-152)
- [Test 153: azmcp-sql-elastic-pool-list](#test-153)
- [Test 154: azmcp-sql-server-entra-admin-list](#test-154)
- [Test 155: azmcp-sql-server-entra-admin-list](#test-155)
- [Test 156: azmcp-sql-server-entra-admin-list](#test-156)
- [Test 157: azmcp-sql-server-firewall-rule-list](#test-157)
- [Test 158: azmcp-sql-server-firewall-rule-list](#test-158)
- [Test 159: azmcp-sql-server-firewall-rule-list](#test-159)
- [Test 160: azmcp-storage-account-create](#test-160)
- [Test 161: azmcp-storage-account-create](#test-161)
- [Test 162: azmcp-storage-account-create](#test-162)
- [Test 163: azmcp-storage-account-list](#test-163)
- [Test 164: azmcp-storage-account-list](#test-164)
- [Test 165: azmcp-storage-account-list](#test-165)
- [Test 166: azmcp-storage-blob-batch-set-tier](#test-166)
- [Test 167: azmcp-storage-blob-batch-set-tier](#test-167)
- [Test 168: azmcp-storage-blob-container-create](#test-168)
- [Test 169: azmcp-storage-blob-container-create](#test-169)
- [Test 170: azmcp-storage-blob-container-create](#test-170)
- [Test 171: azmcp-storage-blob-container-details](#test-171)
- [Test 172: azmcp-storage-blob-container-list](#test-172)
- [Test 173: azmcp-storage-blob-container-list](#test-173)
- [Test 174: azmcp-storage-blob-details](#test-174)
- [Test 175: azmcp-storage-blob-details](#test-175)
- [Test 176: azmcp-storage-blob-list](#test-176)
- [Test 177: azmcp-storage-blob-list](#test-177)
- [Test 178: azmcp-storage-datalake-directory-create](#test-178)
- [Test 179: azmcp-storage-datalake-file-system-list-paths](#test-179)
- [Test 180: azmcp-storage-datalake-file-system-list-paths](#test-180)
- [Test 181: azmcp-storage-datalake-file-system-list-paths](#test-181)
- [Test 182: azmcp-storage-queue-message-send](#test-182)
- [Test 183: azmcp-storage-queue-message-send](#test-183)
- [Test 184: azmcp-storage-queue-message-send](#test-184)
- [Test 185: azmcp-storage-share-file-list](#test-185)
- [Test 186: azmcp-storage-share-file-list](#test-186)
- [Test 187: azmcp-storage-share-file-list](#test-187)
- [Test 188: azmcp-storage-table-list](#test-188)
- [Test 189: azmcp-storage-table-list](#test-189)
- [Test 190: azmcp-subscription-list](#test-190)
- [Test 191: azmcp-subscription-list](#test-191)
- [Test 192: azmcp-subscription-list](#test-192)
- [Test 193: azmcp-subscription-list](#test-193)
- [Test 194: azmcp-azureterraformbestpractices-get](#test-194)
- [Test 195: azmcp-azureterraformbestpractices-get](#test-195)
- [Test 196: azmcp-virtualdesktop-hostpool-list](#test-196)
- [Test 197: azmcp-virtualdesktop-hostpool-sessionhost-list](#test-197)
- [Test 198: azmcp-virtualdesktop-hostpool-sessionhost-usersession-list](#test-198)
- [Test 199: azmcp-workbooks-create](#test-199)
- [Test 200: azmcp-workbooks-delete](#test-200)
- [Test 201: azmcp-workbooks-list](#test-201)
- [Test 202: azmcp-workbooks-list](#test-202)
- [Test 203: azmcp-workbooks-show](#test-203)
- [Test 204: azmcp-workbooks-show](#test-204)
- [Test 205: azmcp-workbooks-update](#test-205)
- [Test 206: azmcp-bicepschema-get](#test-206)

---

## Test 1

**Expected Tool:** `azmcp-foundry-models-deploy`  
**Prompt:** Deploy a GPT4o instance on my resource <resource-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.313400 | `azmcp-foundry-models-deploy` | ✅ **EXPECTED** |
| 2 | 0.269534 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.222504 | `azmcp-grafana-list` | ❌ |
| 4 | 0.222210 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.221635 | `azmcp-workbooks-create` | ❌ |
| 6 | 0.219081 | `azmcp-storage-account-create` | ❌ |
| 7 | 0.218848 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 8 | 0.215293 | `azmcp-loadtesting-testrun-create` | ❌ |
| 9 | 0.215098 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.208401 | `azmcp-loadtesting-test-create` | ❌ |
| 11 | 0.206600 | `azmcp-bestpractices-get` | ❌ |
| 12 | 0.204774 | `azmcp-loadtesting-test-get` | ❌ |
| 13 | 0.204420 | `azmcp-postgres-server-param-set` | ❌ |
| 14 | 0.204301 | `azmcp-extension-azqr` | ❌ |
| 15 | 0.200613 | `azmcp-loadtesting-testresource-list` | ❌ |
| 16 | 0.195615 | `azmcp-workbooks-list` | ❌ |
| 17 | 0.190106 | `azmcp-redis-cluster-list` | ❌ |
| 18 | 0.189255 | `azmcp-postgres-server-param-get` | ❌ |
| 19 | 0.185323 | `azmcp-workbooks-delete` | ❌ |
| 20 | 0.172651 | `azmcp-storage-table-list` | ❌ |

---

## Test 2

**Expected Tool:** `azmcp-foundry-models-deployments-list`  
**Prompt:** List all AI Foundry model deployments  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.559508 | `azmcp-foundry-models-deployments-list` | ✅ **EXPECTED** |
| 2 | 0.549636 | `azmcp-foundry-models-list` | ❌ |
| 3 | 0.533239 | `azmcp-foundry-models-deploy` | ❌ |
| 4 | 0.404693 | `azmcp-search-service-list` | ❌ |
| 5 | 0.387176 | `azmcp-search-index-list` | ❌ |
| 6 | 0.334867 | `azmcp-grafana-list` | ❌ |
| 7 | 0.318854 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.312247 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.306266 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 10 | 0.302262 | `azmcp-monitor-table-type-list` | ❌ |
| 11 | 0.301302 | `azmcp-redis-cluster-list` | ❌ |
| 12 | 0.295178 | `azmcp-functionapp-list` | ❌ |
| 13 | 0.289448 | `azmcp-monitor-workspace-list` | ❌ |
| 14 | 0.288248 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.287662 | `azmcp-appconfig-account-list` | ❌ |
| 16 | 0.284874 | `azmcp-monitor-table-list` | ❌ |
| 17 | 0.277091 | `azmcp-extension-azd` | ❌ |
| 18 | 0.273892 | `azmcp-subscription-list` | ❌ |
| 19 | 0.273875 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 20 | 0.266663 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |

---

## Test 3

**Expected Tool:** `azmcp-foundry-models-deployments-list`  
**Prompt:** Show me all AI Foundry model deployments  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.518221 | `azmcp-foundry-models-list` | ❌ |
| 2 | 0.503424 | `azmcp-foundry-models-deploy` | ❌ |
| 3 | 0.488885 | `azmcp-foundry-models-deployments-list` | ✅ **EXPECTED** |
| 4 | 0.360908 | `azmcp-search-service-list` | ❌ |
| 5 | 0.337032 | `azmcp-search-index-list` | ❌ |
| 6 | 0.286814 | `azmcp-grafana-list` | ❌ |
| 7 | 0.265906 | `azmcp-extension-azd` | ❌ |
| 8 | 0.259989 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.254926 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.250392 | `azmcp-redis-cluster-list` | ❌ |
| 11 | 0.243193 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 12 | 0.243133 | `azmcp-monitor-table-type-list` | ❌ |
| 13 | 0.242798 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 14 | 0.238612 | `azmcp-search-index-describe` | ❌ |
| 15 | 0.235765 | `azmcp-loadtesting-testresource-list` | ❌ |
| 16 | 0.234075 | `azmcp-redis-cache-list` | ❌ |
| 17 | 0.232469 | `azmcp-monitor-workspace-list` | ❌ |
| 18 | 0.231690 | `azmcp-bestpractices-get` | ❌ |
| 19 | 0.231556 | `azmcp-monitor-table-list` | ❌ |
| 20 | 0.227845 | `azmcp-subscription-list` | ❌ |

---

## Test 4

**Expected Tool:** `azmcp-foundry-models-list`  
**Prompt:** List all AI Foundry models  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.560022 | `azmcp-foundry-models-list` | ✅ **EXPECTED** |
| 2 | 0.401146 | `azmcp-foundry-models-deploy` | ❌ |
| 3 | 0.355031 | `azmcp-search-service-list` | ❌ |
| 4 | 0.346909 | `azmcp-foundry-models-deployments-list` | ❌ |
| 5 | 0.337429 | `azmcp-search-index-list` | ❌ |
| 6 | 0.298648 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.285437 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.277883 | `azmcp-grafana-list` | ❌ |
| 9 | 0.273026 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.252297 | `azmcp-postgres-database-list` | ❌ |
| 11 | 0.248620 | `azmcp-redis-cache-list` | ❌ |
| 12 | 0.247709 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.245288 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 14 | 0.245005 | `azmcp-loadtesting-testrun-list` | ❌ |
| 15 | 0.243429 | `azmcp-postgres-server-list` | ❌ |
| 16 | 0.242198 | `azmcp-redis-cluster-list` | ❌ |
| 17 | 0.240216 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 18 | 0.226117 | `azmcp-keyvault-certificate-list` | ❌ |
| 19 | 0.225465 | `azmcp-keyvault-key-list` | ❌ |
| 20 | 0.222970 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 5

**Expected Tool:** `azmcp-foundry-models-list`  
**Prompt:** Show me the available AI Foundry models  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.574818 | `azmcp-foundry-models-list` | ✅ **EXPECTED** |
| 2 | 0.430513 | `azmcp-foundry-models-deploy` | ❌ |
| 3 | 0.356899 | `azmcp-foundry-models-deployments-list` | ❌ |
| 4 | 0.309590 | `azmcp-search-service-list` | ❌ |
| 5 | 0.287991 | `azmcp-search-index-list` | ❌ |
| 6 | 0.244697 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.233051 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 8 | 0.231148 | `azmcp-grafana-list` | ❌ |
| 9 | 0.216807 | `azmcp-extension-azd` | ❌ |
| 10 | 0.212188 | `azmcp-search-index-describe` | ❌ |
| 11 | 0.203040 | `azmcp-loadtesting-testresource-list` | ❌ |
| 12 | 0.203036 | `azmcp-search-index-query` | ❌ |
| 13 | 0.202283 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 14 | 0.200059 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.199386 | `azmcp-redis-cluster-list` | ❌ |
| 16 | 0.198127 | `azmcp-bestpractices-get` | ❌ |
| 17 | 0.194339 | `azmcp-loadtesting-testresource-create` | ❌ |
| 18 | 0.191616 | `azmcp-redis-cache-list` | ❌ |
| 19 | 0.177862 | `azmcp-monitor-table-list` | ❌ |
| 20 | 0.171268 | `azmcp-subscription-list` | ❌ |

---

## Test 6

**Expected Tool:** `azmcp-search-index-describe`  
**Prompt:** Show me the details of the index <index-name> in Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.618178 | `azmcp-search-index-list` | ❌ |
| 2 | 0.597678 | `azmcp-search-index-describe` | ✅ **EXPECTED** |
| 3 | 0.465274 | `azmcp-search-index-query` | ❌ |
| 4 | 0.436730 | `azmcp-search-service-list` | ❌ |
| 5 | 0.393556 | `azmcp-aks-cluster-get` | ❌ |
| 6 | 0.389306 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.358315 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.356252 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.330038 | `azmcp-kusto-table-schema` | ❌ |
| 10 | 0.327156 | `azmcp-workbooks-show` | ❌ |
| 11 | 0.326847 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.326590 | `azmcp-storage-blob-container-details` | ❌ |
| 13 | 0.325015 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.322423 | `azmcp-monitor-table-list` | ❌ |
| 15 | 0.321928 | `azmcp-redis-cache-list` | ❌ |
| 16 | 0.320890 | `azmcp-foundry-models-deployments-list` | ❌ |
| 17 | 0.316039 | `azmcp-appconfig-kv-show` | ❌ |
| 18 | 0.313076 | `azmcp-keyvault-certificate-get` | ❌ |
| 19 | 0.312237 | `azmcp-cosmos-database-list` | ❌ |
| 20 | 0.312080 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 7

**Expected Tool:** `azmcp-search-index-list`  
**Prompt:** List all indexes in the Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.796644 | `azmcp-search-index-list` | ✅ **EXPECTED** |
| 2 | 0.561102 | `azmcp-search-service-list` | ❌ |
| 3 | 0.518943 | `azmcp-search-index-describe` | ❌ |
| 4 | 0.455689 | `azmcp-search-index-query` | ❌ |
| 5 | 0.439452 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.416404 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.409307 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.406485 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.392400 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.382877 | `azmcp-keyvault-key-list` | ❌ |
| 11 | 0.378750 | `azmcp-kusto-database-list` | ❌ |
| 12 | 0.378297 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.375372 | `azmcp-foundry-models-deployments-list` | ❌ |
| 14 | 0.369526 | `azmcp-keyvault-certificate-list` | ❌ |
| 15 | 0.368845 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.367388 | `azmcp-redis-cache-list` | ❌ |
| 17 | 0.362619 | `azmcp-keyvault-secret-list` | ❌ |
| 18 | 0.361922 | `azmcp-foundry-models-list` | ❌ |
| 19 | 0.360722 | `azmcp-redis-cluster-list` | ❌ |
| 20 | 0.349633 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 8

**Expected Tool:** `azmcp-search-index-list`  
**Prompt:** Show me the indexes in the Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.750042 | `azmcp-search-index-list` | ✅ **EXPECTED** |
| 2 | 0.512453 | `azmcp-search-index-describe` | ❌ |
| 3 | 0.497599 | `azmcp-search-service-list` | ❌ |
| 4 | 0.443812 | `azmcp-search-index-query` | ❌ |
| 5 | 0.401807 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.382692 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.372639 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.370330 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.353839 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.351788 | `azmcp-foundry-models-deployments-list` | ❌ |
| 11 | 0.350043 | `azmcp-kusto-database-list` | ❌ |
| 12 | 0.347566 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.341728 | `azmcp-foundry-models-list` | ❌ |
| 14 | 0.332227 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.331271 | `azmcp-keyvault-key-list` | ❌ |
| 16 | 0.330437 | `azmcp-kusto-table-list` | ❌ |
| 17 | 0.328039 | `azmcp-redis-cluster-list` | ❌ |
| 18 | 0.324069 | `azmcp-redis-cache-list` | ❌ |
| 19 | 0.323041 | `azmcp-cosmos-database-container-list` | ❌ |
| 20 | 0.313024 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 9

**Expected Tool:** `azmcp-search-index-query`  
**Prompt:** Search for instances of <search_term> in the index <index-name> in Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.552944 | `azmcp-search-index-list` | ❌ |
| 2 | 0.522558 | `azmcp-search-index-query` | ✅ **EXPECTED** |
| 3 | 0.492637 | `azmcp-search-index-describe` | ❌ |
| 4 | 0.463356 | `azmcp-search-service-list` | ❌ |
| 5 | 0.327095 | `azmcp-kusto-query` | ❌ |
| 6 | 0.322009 | `azmcp-monitor-workspace-log-query` | ❌ |
| 7 | 0.311044 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 8 | 0.298026 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.288242 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.283532 | `azmcp-foundry-models-list` | ❌ |
| 11 | 0.269913 | `azmcp-monitor-table-list` | ❌ |
| 12 | 0.269646 | `azmcp-monitor-metrics-definitions` | ❌ |
| 13 | 0.254226 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.248402 | `azmcp-monitor-table-type-list` | ❌ |
| 15 | 0.244844 | `azmcp-kusto-sample` | ❌ |
| 16 | 0.243995 | `azmcp-monitor-workspace-list` | ❌ |
| 17 | 0.243984 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.235536 | `azmcp-cosmos-database-container-list` | ❌ |
| 19 | 0.232713 | `azmcp-loadtesting-testrun-get` | ❌ |
| 20 | 0.229137 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 10

**Expected Tool:** `azmcp-search-service-list`  
**Prompt:** List all Cognitive Search services in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.745450 | `azmcp-search-service-list` | ✅ **EXPECTED** |
| 2 | 0.608251 | `azmcp-search-index-list` | ❌ |
| 3 | 0.500455 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.494272 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.493011 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.492228 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.486066 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.482464 | `azmcp-grafana-list` | ❌ |
| 9 | 0.477471 | `azmcp-subscription-list` | ❌ |
| 10 | 0.470278 | `azmcp-kusto-cluster-list` | ❌ |
| 11 | 0.467863 | `azmcp-functionapp-list` | ❌ |
| 12 | 0.454460 | `azmcp-foundry-models-deployments-list` | ❌ |
| 13 | 0.451956 | `azmcp-aks-cluster-list` | ❌ |
| 14 | 0.441517 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.427817 | `azmcp-group-list` | ❌ |
| 16 | 0.417472 | `azmcp-appconfig-account-list` | ❌ |
| 17 | 0.414984 | `azmcp-foundry-models-list` | ❌ |
| 18 | 0.409911 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.408684 | `azmcp-loadtesting-testresource-list` | ❌ |
| 20 | 0.405265 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |

---

## Test 11

**Expected Tool:** `azmcp-search-service-list`  
**Prompt:** Show me the Cognitive Search services in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.644213 | `azmcp-search-service-list` | ✅ **EXPECTED** |
| 2 | 0.525315 | `azmcp-search-index-list` | ❌ |
| 3 | 0.425939 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.412158 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.408456 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.400229 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.399822 | `azmcp-grafana-list` | ❌ |
| 8 | 0.397883 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.393708 | `azmcp-subscription-list` | ❌ |
| 10 | 0.390559 | `azmcp-foundry-models-list` | ❌ |
| 11 | 0.384559 | `azmcp-postgres-server-list` | ❌ |
| 12 | 0.382307 | `azmcp-functionapp-list` | ❌ |
| 13 | 0.376950 | `azmcp-search-index-describe` | ❌ |
| 14 | 0.375985 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.367365 | `azmcp-search-index-query` | ❌ |
| 16 | 0.363247 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.362317 | `azmcp-marketplace-product-get` | ❌ |
| 18 | 0.360230 | `azmcp-loadtesting-testresource-list` | ❌ |
| 19 | 0.356378 | `azmcp-storage-account-list` | ❌ |
| 20 | 0.348165 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 12

**Expected Tool:** `azmcp-search-service-list`  
**Prompt:** Show me my Cognitive Search services  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.488099 | `azmcp-search-index-list` | ❌ |
| 2 | 0.482308 | `azmcp-search-service-list` | ✅ **EXPECTED** |
| 3 | 0.351773 | `azmcp-search-index-describe` | ❌ |
| 4 | 0.344699 | `azmcp-foundry-models-deployments-list` | ❌ |
| 5 | 0.329777 | `azmcp-search-index-query` | ❌ |
| 6 | 0.322540 | `azmcp-foundry-models-list` | ❌ |
| 7 | 0.290214 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.283366 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.281134 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.278531 | `azmcp-redis-cache-list` | ❌ |
| 11 | 0.276660 | `azmcp-extension-az` | ❌ |
| 12 | 0.274081 | `azmcp-monitor-table-type-list` | ❌ |
| 13 | 0.272173 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.266987 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.264394 | `azmcp-grafana-list` | ❌ |
| 16 | 0.264162 | `azmcp-postgres-server-list` | ❌ |
| 17 | 0.261073 | `azmcp-aks-cluster-list` | ❌ |
| 18 | 0.254044 | `azmcp-aks-cluster-get` | ❌ |
| 19 | 0.250037 | `azmcp-cosmos-database-container-list` | ❌ |
| 20 | 0.246849 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 13

**Expected Tool:** `azmcp-appconfig-account-list`  
**Prompt:** List all App Configuration stores in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.786360 | `azmcp-appconfig-account-list` | ✅ **EXPECTED** |
| 2 | 0.635561 | `azmcp-appconfig-kv-list` | ❌ |
| 3 | 0.492146 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.491380 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.473554 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.460703 | `azmcp-functionapp-list` | ❌ |
| 7 | 0.458475 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.442214 | `azmcp-grafana-list` | ❌ |
| 9 | 0.441656 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.429305 | `azmcp-search-service-list` | ❌ |
| 11 | 0.427658 | `azmcp-subscription-list` | ❌ |
| 12 | 0.427460 | `azmcp-appconfig-kv-show` | ❌ |
| 13 | 0.420766 | `azmcp-kusto-cluster-list` | ❌ |
| 14 | 0.408599 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.404636 | `azmcp-storage-table-list` | ❌ |
| 16 | 0.398242 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.387414 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.387179 | `azmcp-appconfig-kv-delete` | ❌ |
| 19 | 0.385938 | `azmcp-sql-db-list` | ❌ |
| 20 | 0.370646 | `azmcp-postgres-server-config-get` | ❌ |

---

## Test 14

**Expected Tool:** `azmcp-appconfig-account-list`  
**Prompt:** Show me the App Configuration stores in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.634978 | `azmcp-appconfig-account-list` | ✅ **EXPECTED** |
| 2 | 0.533437 | `azmcp-appconfig-kv-list` | ❌ |
| 3 | 0.425610 | `azmcp-appconfig-kv-show` | ❌ |
| 4 | 0.372456 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.368731 | `azmcp-redis-cache-list` | ❌ |
| 6 | 0.368236 | `azmcp-functionapp-list` | ❌ |
| 7 | 0.359567 | `azmcp-postgres-server-config-get` | ❌ |
| 8 | 0.356514 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.354747 | `azmcp-appconfig-kv-delete` | ❌ |
| 10 | 0.348603 | `azmcp-appconfig-kv-set` | ❌ |
| 11 | 0.341263 | `azmcp-grafana-list` | ❌ |
| 12 | 0.331211 | `azmcp-storage-account-list` | ❌ |
| 13 | 0.325885 | `azmcp-subscription-list` | ❌ |
| 14 | 0.321968 | `azmcp-appconfig-kv-unlock` | ❌ |
| 15 | 0.320538 | `azmcp-marketplace-product-get` | ❌ |
| 16 | 0.317667 | `azmcp-search-service-list` | ❌ |
| 17 | 0.316132 | `azmcp-appconfig-kv-lock` | ❌ |
| 18 | 0.296589 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.292788 | `azmcp-monitor-workspace-list` | ❌ |
| 20 | 0.288739 | `azmcp-servicebus-topic-subscription-details` | ❌ |

---

## Test 15

**Expected Tool:** `azmcp-appconfig-account-list`  
**Prompt:** Show me my App Configuration stores  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.565428 | `azmcp-appconfig-account-list` | ✅ **EXPECTED** |
| 2 | 0.564674 | `azmcp-appconfig-kv-list` | ❌ |
| 3 | 0.414544 | `azmcp-appconfig-kv-show` | ❌ |
| 4 | 0.355873 | `azmcp-postgres-server-config-get` | ❌ |
| 5 | 0.348545 | `azmcp-appconfig-kv-delete` | ❌ |
| 6 | 0.327084 | `azmcp-appconfig-kv-set` | ❌ |
| 7 | 0.308019 | `azmcp-appconfig-kv-unlock` | ❌ |
| 8 | 0.302319 | `azmcp-appconfig-kv-lock` | ❌ |
| 9 | 0.244060 | `azmcp-loadtesting-testrun-list` | ❌ |
| 10 | 0.237718 | `azmcp-loadtesting-test-get` | ❌ |
| 11 | 0.233464 | `azmcp-postgres-server-list` | ❌ |
| 12 | 0.231722 | `azmcp-redis-cache-list` | ❌ |
| 13 | 0.230153 | `azmcp-storage-blob-container-list` | ❌ |
| 14 | 0.223159 | `azmcp-bestpractices-get` | ❌ |
| 15 | 0.221553 | `azmcp-postgres-database-list` | ❌ |
| 16 | 0.216154 | `azmcp-redis-cluster-list` | ❌ |
| 17 | 0.214241 | `azmcp-storage-account-list` | ❌ |
| 18 | 0.209914 | `azmcp-sql-db-list` | ❌ |
| 19 | 0.208521 | `azmcp-storage-blob-container-details` | ❌ |
| 20 | 0.204215 | `azmcp-postgres-server-param-get` | ❌ |

---

## Test 16

**Expected Tool:** `azmcp-appconfig-kv-delete`  
**Prompt:** Delete the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.618277 | `azmcp-appconfig-kv-delete` | ✅ **EXPECTED** |
| 2 | 0.486631 | `azmcp-appconfig-kv-list` | ❌ |
| 3 | 0.444881 | `azmcp-appconfig-kv-unlock` | ❌ |
| 4 | 0.443998 | `azmcp-appconfig-kv-lock` | ❌ |
| 5 | 0.424344 | `azmcp-appconfig-kv-set` | ❌ |
| 6 | 0.399569 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.392016 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.264810 | `azmcp-workbooks-delete` | ❌ |
| 9 | 0.262111 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.248919 | `azmcp-keyvault-key-list` | ❌ |
| 11 | 0.240483 | `azmcp-keyvault-secret-create` | ❌ |
| 12 | 0.194831 | `azmcp-postgres-server-config-get` | ❌ |
| 13 | 0.175522 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.173143 | `azmcp-postgres-server-param-set` | ❌ |
| 15 | 0.145096 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 16 | 0.141099 | `azmcp-postgres-server-param-get` | ❌ |
| 17 | 0.137771 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 18 | 0.135822 | `azmcp-redis-cache-list` | ❌ |
| 19 | 0.131936 | `azmcp-sql-db-list` | ❌ |
| 20 | 0.131748 | `azmcp-storage-table-list` | ❌ |

---

## Test 17

**Expected Tool:** `azmcp-appconfig-kv-list`  
**Prompt:** List all key-value settings in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.730852 | `azmcp-appconfig-kv-list` | ✅ **EXPECTED** |
| 2 | 0.595054 | `azmcp-appconfig-kv-show` | ❌ |
| 3 | 0.557810 | `azmcp-appconfig-account-list` | ❌ |
| 4 | 0.530884 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.482784 | `azmcp-appconfig-kv-unlock` | ❌ |
| 6 | 0.464635 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.438315 | `azmcp-appconfig-kv-lock` | ❌ |
| 8 | 0.377534 | `azmcp-postgres-server-config-get` | ❌ |
| 9 | 0.374473 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.338195 | `azmcp-keyvault-secret-list` | ❌ |
| 11 | 0.329461 | `azmcp-loadtesting-testrun-list` | ❌ |
| 12 | 0.296084 | `azmcp-postgres-table-list` | ❌ |
| 13 | 0.292091 | `azmcp-redis-cache-list` | ❌ |
| 14 | 0.279679 | `azmcp-storage-table-list` | ❌ |
| 15 | 0.275400 | `azmcp-storage-blob-container-list` | ❌ |
| 16 | 0.267026 | `azmcp-postgres-database-list` | ❌ |
| 17 | 0.264791 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 18 | 0.263496 | `azmcp-monitor-table-list` | ❌ |
| 19 | 0.258800 | `azmcp-subscription-list` | ❌ |
| 20 | 0.257911 | `azmcp-sql-db-list` | ❌ |

---

## Test 18

**Expected Tool:** `azmcp-appconfig-kv-list`  
**Prompt:** Show me the key-value settings in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.682275 | `azmcp-appconfig-kv-list` | ✅ **EXPECTED** |
| 2 | 0.606545 | `azmcp-appconfig-kv-show` | ❌ |
| 3 | 0.522426 | `azmcp-appconfig-account-list` | ❌ |
| 4 | 0.512945 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.490384 | `azmcp-appconfig-kv-unlock` | ❌ |
| 6 | 0.468503 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.458896 | `azmcp-appconfig-kv-lock` | ❌ |
| 8 | 0.370520 | `azmcp-postgres-server-config-get` | ❌ |
| 9 | 0.316879 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.294889 | `azmcp-keyvault-key-list` | ❌ |
| 11 | 0.282379 | `azmcp-loadtesting-testrun-list` | ❌ |
| 12 | 0.258688 | `azmcp-postgres-server-param-get` | ❌ |
| 13 | 0.248138 | `azmcp-storage-blob-container-details` | ❌ |
| 14 | 0.247879 | `azmcp-storage-blob-details` | ❌ |
| 15 | 0.243655 | `azmcp-postgres-server-param-set` | ❌ |
| 16 | 0.236373 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 17 | 0.233375 | `azmcp-redis-cache-list` | ❌ |
| 18 | 0.228684 | `azmcp-storage-blob-container-list` | ❌ |
| 19 | 0.225853 | `azmcp-storage-table-list` | ❌ |
| 20 | 0.216213 | `azmcp-sql-db-list` | ❌ |

---

## Test 19

**Expected Tool:** `azmcp-appconfig-kv-lock`  
**Prompt:** Lock the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.646614 | `azmcp-appconfig-kv-lock` | ✅ **EXPECTED** |
| 2 | 0.518065 | `azmcp-appconfig-kv-unlock` | ❌ |
| 3 | 0.508804 | `azmcp-appconfig-kv-list` | ❌ |
| 4 | 0.445551 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.431516 | `azmcp-appconfig-kv-delete` | ❌ |
| 6 | 0.423650 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.373656 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.251279 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.238544 | `azmcp-keyvault-secret-create` | ❌ |
| 10 | 0.238242 | `azmcp-postgres-server-param-set` | ❌ |
| 11 | 0.211331 | `azmcp-postgres-server-config-get` | ❌ |
| 12 | 0.208190 | `azmcp-keyvault-key-list` | ❌ |
| 13 | 0.191570 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.160992 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 15 | 0.154529 | `azmcp-postgres-server-param-get` | ❌ |
| 16 | 0.144326 | `azmcp-servicebus-queue-details` | ❌ |
| 17 | 0.135403 | `azmcp-storage-blob-container-details` | ❌ |
| 18 | 0.123426 | `azmcp-search-index-describe` | ❌ |
| 19 | 0.116471 | `azmcp-postgres-table-schema-get` | ❌ |
| 20 | 0.112348 | `azmcp-storage-table-list` | ❌ |

---

## Test 20

**Expected Tool:** `azmcp-appconfig-kv-set`  
**Prompt:** Set the key <key_name> in App Configuration store <app_config_store_name> to <value>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609635 | `azmcp-appconfig-kv-set` | ✅ **EXPECTED** |
| 2 | 0.541850 | `azmcp-appconfig-kv-lock` | ❌ |
| 3 | 0.518499 | `azmcp-appconfig-kv-list` | ❌ |
| 4 | 0.511219 | `azmcp-appconfig-kv-unlock` | ❌ |
| 5 | 0.507170 | `azmcp-appconfig-kv-show` | ❌ |
| 6 | 0.505571 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.377919 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.346927 | `azmcp-postgres-server-param-set` | ❌ |
| 9 | 0.311433 | `azmcp-keyvault-secret-create` | ❌ |
| 10 | 0.305932 | `azmcp-keyvault-key-create` | ❌ |
| 11 | 0.221138 | `azmcp-loadtesting-test-create` | ❌ |
| 12 | 0.208947 | `azmcp-postgres-server-config-get` | ❌ |
| 13 | 0.206836 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.201343 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 15 | 0.167006 | `azmcp-postgres-server-param-get` | ❌ |
| 16 | 0.136975 | `azmcp-storage-queue-message-send` | ❌ |
| 17 | 0.124191 | `azmcp-servicebus-queue-details` | ❌ |
| 18 | 0.123491 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.122352 | `azmcp-search-index-describe` | ❌ |
| 20 | 0.120302 | `azmcp-workbooks-update` | ❌ |

---

## Test 21

**Expected Tool:** `azmcp-appconfig-kv-show`  
**Prompt:** Show the content for the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.602751 | `azmcp-appconfig-kv-list` | ❌ |
| 2 | 0.561137 | `azmcp-appconfig-kv-show` | ✅ **EXPECTED** |
| 3 | 0.448466 | `azmcp-appconfig-kv-set` | ❌ |
| 4 | 0.441433 | `azmcp-appconfig-kv-delete` | ❌ |
| 5 | 0.437109 | `azmcp-appconfig-account-list` | ❌ |
| 6 | 0.433639 | `azmcp-appconfig-kv-lock` | ❌ |
| 7 | 0.427332 | `azmcp-appconfig-kv-unlock` | ❌ |
| 8 | 0.302187 | `azmcp-keyvault-key-list` | ❌ |
| 9 | 0.290688 | `azmcp-postgres-server-config-get` | ❌ |
| 10 | 0.276655 | `azmcp-loadtesting-test-get` | ❌ |
| 11 | 0.260322 | `azmcp-keyvault-secret-list` | ❌ |
| 12 | 0.221914 | `azmcp-storage-blob-container-details` | ❌ |
| 13 | 0.217367 | `azmcp-postgres-server-param-get` | ❌ |
| 14 | 0.206517 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.205610 | `azmcp-storage-table-list` | ❌ |
| 16 | 0.205499 | `azmcp-storage-blob-container-list` | ❌ |
| 17 | 0.193531 | `azmcp-storage-blob-list` | ❌ |
| 18 | 0.191836 | `azmcp-storage-blob-details` | ❌ |
| 19 | 0.185937 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 20 | 0.173991 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 22

**Expected Tool:** `azmcp-appconfig-kv-unlock`  
**Prompt:** Unlock the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.604237 | `azmcp-appconfig-kv-unlock` | ✅ **EXPECTED** |
| 2 | 0.552616 | `azmcp-appconfig-kv-lock` | ❌ |
| 3 | 0.541339 | `azmcp-appconfig-kv-list` | ❌ |
| 4 | 0.476628 | `azmcp-appconfig-kv-delete` | ❌ |
| 5 | 0.436779 | `azmcp-appconfig-kv-show` | ❌ |
| 6 | 0.426780 | `azmcp-appconfig-kv-set` | ❌ |
| 7 | 0.410349 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.267811 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.259507 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.253236 | `azmcp-keyvault-secret-create` | ❌ |
| 11 | 0.225157 | `azmcp-postgres-server-config-get` | ❌ |
| 12 | 0.185633 | `azmcp-postgres-server-param-set` | ❌ |
| 13 | 0.180737 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.159638 | `azmcp-postgres-server-param-get` | ❌ |
| 15 | 0.148680 | `azmcp-storage-blob-container-details` | ❌ |
| 16 | 0.146721 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 17 | 0.144171 | `azmcp-servicebus-queue-details` | ❌ |
| 18 | 0.133890 | `azmcp-search-index-describe` | ❌ |
| 19 | 0.132621 | `azmcp-workbooks-delete` | ❌ |
| 20 | 0.131001 | `azmcp-storage-table-list` | ❌ |

---

## Test 23

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** Create a Storage account with name <storage_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628750 | `azmcp-storage-account-create` | ❌ |
| 2 | 0.472375 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.444809 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.429618 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.403075 | `azmcp-keyvault-secret-create` | ❌ |
| 6 | 0.396132 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.386811 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.376271 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.373307 | `azmcp-keyvault-certificate-create` | ❌ |
| 10 | 0.352805 | `azmcp-appconfig-kv-set` | ❌ |
| 11 | 0.337708 | `azmcp-storage-datalake-directory-create` | ❌ |
| 12 | 0.333768 | `azmcp-storage-blob-container-create` | ❌ |
| 13 | 0.329961 | `azmcp-loadtesting-testresource-create` | ❌ |
| 14 | 0.327875 | `azmcp-workbooks-create` | ❌ |
| 15 | 0.325736 | `azmcp-loadtesting-test-create` | ❌ |
| 16 | 0.318516 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.303766 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.303151 | `azmcp-appconfig-kv-lock` | ❌ |
| 19 | 0.300375 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 20 | 0.276344 | `azmcp-appconfig-kv-show` | ❌ |

---

## Test 24

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** List all virtual machines in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.577373 | `azmcp-search-service-list` | ❌ |
| 2 | 0.531767 | `azmcp-subscription-list` | ❌ |
| 3 | 0.530948 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.500615 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.499209 | `azmcp-kusto-cluster-list` | ❌ |
| 6 | 0.496186 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.491235 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 8 | 0.484074 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.482576 | `azmcp-grafana-list` | ❌ |
| 10 | 0.478198 | `azmcp-aks-cluster-list` | ❌ |
| 11 | 0.473774 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.473712 | `azmcp-functionapp-list` | ❌ |
| 13 | 0.468411 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 14 | 0.454007 | `azmcp-group-list` | ❌ |
| 15 | 0.453243 | `azmcp-storage-account-list` | ❌ |
| 16 | 0.435259 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 17 | 0.430100 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.411045 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.407261 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.385092 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 25

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** Show me the details of the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.565873 | `azmcp-storage-blob-container-details` | ❌ |
| 2 | 0.559925 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.514061 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.509806 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.495892 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.476522 | `azmcp-storage-account-create` | ❌ |
| 7 | 0.434946 | `azmcp-storage-blob-details` | ❌ |
| 8 | 0.433899 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.433255 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.417590 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.371852 | `azmcp-sql-db-show` | ❌ |
| 12 | 0.367600 | `azmcp-aks-cluster-get` | ❌ |
| 13 | 0.360310 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.347986 | `azmcp-subscription-list` | ❌ |
| 15 | 0.347005 | `azmcp-loadtesting-testrun-get` | ❌ |
| 16 | 0.346336 | `azmcp-monitor-resource-log-query` | ❌ |
| 17 | 0.337702 | `azmcp-kusto-cluster-get` | ❌ |
| 18 | 0.326950 | `azmcp-keyvault-key-list` | ❌ |
| 19 | 0.325659 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.323187 | `azmcp-functionapp-list` | ❌ |

---

## Test 26

**Expected Tool:** `azmcp-acr-registry-list`  
**Prompt:** List all Azure Container Registries in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.743568 | `azmcp-acr-registry-list` | ✅ **EXPECTED** |
| 2 | 0.528620 | `azmcp-search-service-list` | ❌ |
| 3 | 0.527822 | `azmcp-aks-cluster-list` | ❌ |
| 4 | 0.525768 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.515937 | `azmcp-subscription-list` | ❌ |
| 6 | 0.514293 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.509386 | `azmcp-monitor-workspace-list` | ❌ |
| 8 | 0.502925 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.500856 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.490776 | `azmcp-appconfig-account-list` | ❌ |
| 11 | 0.483500 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.482499 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.482236 | `azmcp-redis-cluster-list` | ❌ |
| 14 | 0.481761 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.480869 | `azmcp-group-list` | ❌ |
| 16 | 0.473592 | `azmcp-functionapp-list` | ❌ |
| 17 | 0.469722 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.460523 | `azmcp-sql-db-list` | ❌ |
| 19 | 0.460343 | `azmcp-cosmos-database-list` | ❌ |
| 20 | 0.454507 | `azmcp-storage-blob-list` | ❌ |

---

## Test 27

**Expected Tool:** `azmcp-acr-registry-list`  
**Prompt:** Show me my Azure Container Registries  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.586014 | `azmcp-acr-registry-list` | ✅ **EXPECTED** |
| 2 | 0.457032 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.415552 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.376444 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.362031 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.356398 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.353379 | `azmcp-subscription-list` | ❌ |
| 8 | 0.349526 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.349291 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.344071 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.339252 | `azmcp-appconfig-account-list` | ❌ |
| 12 | 0.338380 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.336892 | `azmcp-keyvault-certificate-list` | ❌ |
| 14 | 0.334637 | `azmcp-extension-az` | ❌ |
| 15 | 0.333732 | `azmcp-monitor-workspace-list` | ❌ |
| 16 | 0.332742 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.328377 | `azmcp-redis-cache-list` | ❌ |
| 18 | 0.328159 | `azmcp-storage-account-list` | ❌ |
| 19 | 0.327853 | `azmcp-redis-cluster-list` | ❌ |
| 20 | 0.320584 | `azmcp-keyvault-secret-list` | ❌ |

---

## Test 28

**Expected Tool:** `azmcp-acr-registry-list`  
**Prompt:** Show me the container registries in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.637130 | `azmcp-acr-registry-list` | ✅ **EXPECTED** |
| 2 | 0.474000 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.471804 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.464679 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.463742 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.463435 | `azmcp-search-service-list` | ❌ |
| 7 | 0.452849 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.451253 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.443939 | `azmcp-appconfig-account-list` | ❌ |
| 10 | 0.440464 | `azmcp-subscription-list` | ❌ |
| 11 | 0.435835 | `azmcp-grafana-list` | ❌ |
| 12 | 0.432598 | `azmcp-storage-account-list` | ❌ |
| 13 | 0.431745 | `azmcp-cosmos-database-container-list` | ❌ |
| 14 | 0.431172 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.430308 | `azmcp-cosmos-account-list` | ❌ |
| 16 | 0.409093 | `azmcp-storage-table-list` | ❌ |
| 17 | 0.404664 | `azmcp-group-list` | ❌ |
| 18 | 0.394148 | `azmcp-kusto-database-list` | ❌ |
| 19 | 0.393235 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.381972 | `azmcp-storage-blob-list` | ❌ |

---

## Test 29

**Expected Tool:** `azmcp-acr-registry-list`  
**Prompt:** List container registries in resource group <resource_group_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633938 | `azmcp-acr-registry-list` | ✅ **EXPECTED** |
| 2 | 0.454929 | `azmcp-group-list` | ❌ |
| 3 | 0.453845 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 4 | 0.452130 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.446008 | `azmcp-cosmos-database-container-list` | ❌ |
| 6 | 0.428000 | `azmcp-workbooks-list` | ❌ |
| 7 | 0.411316 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.409133 | `azmcp-sql-db-list` | ❌ |
| 9 | 0.392315 | `azmcp-storage-blob-list` | ❌ |
| 10 | 0.388773 | `azmcp-redis-cache-list` | ❌ |
| 11 | 0.372510 | `azmcp-sql-elastic-pool-list` | ❌ |
| 12 | 0.370359 | `azmcp-redis-cluster-database-list` | ❌ |
| 13 | 0.366688 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 14 | 0.366482 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.356044 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.354145 | `azmcp-cosmos-database-list` | ❌ |
| 17 | 0.352336 | `azmcp-loadtesting-testresource-list` | ❌ |
| 18 | 0.352165 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.348151 | `azmcp-monitor-metrics-definitions` | ❌ |
| 20 | 0.342481 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 30

**Expected Tool:** `azmcp-acr-registry-list`  
**Prompt:** Show me the container registries in resource group <resource_group_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.639391 | `azmcp-acr-registry-list` | ✅ **EXPECTED** |
| 2 | 0.449441 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 3 | 0.445741 | `azmcp-group-list` | ❌ |
| 4 | 0.440715 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.416353 | `azmcp-cosmos-database-container-list` | ❌ |
| 6 | 0.413975 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.400209 | `azmcp-workbooks-list` | ❌ |
| 8 | 0.378353 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.373837 | `azmcp-sql-elastic-pool-list` | ❌ |
| 10 | 0.371878 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 11 | 0.368887 | `azmcp-storage-blob-list` | ❌ |
| 12 | 0.367734 | `azmcp-redis-cache-list` | ❌ |
| 13 | 0.358684 | `azmcp-monitor-workspace-list` | ❌ |
| 14 | 0.354807 | `azmcp-loadtesting-testresource-list` | ❌ |
| 15 | 0.351411 | `azmcp-cosmos-database-list` | ❌ |
| 16 | 0.344037 | `azmcp-kusto-cluster-list` | ❌ |
| 17 | 0.343802 | `azmcp-aks-cluster-list` | ❌ |
| 18 | 0.341763 | `azmcp-kusto-database-list` | ❌ |
| 19 | 0.341019 | `azmcp-monitor-metrics-definitions` | ❌ |
| 20 | 0.339742 | `azmcp-storage-table-list` | ❌ |

---

## Test 31

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** List all cosmosdb accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.818357 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.668480 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.615268 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.606695 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.588682 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.587691 | `azmcp-subscription-list` | ❌ |
| 7 | 0.557870 | `azmcp-search-service-list` | ❌ |
| 8 | 0.530755 | `azmcp-storage-blob-container-list` | ❌ |
| 9 | 0.528963 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.516811 | `azmcp-kusto-cluster-list` | ❌ |
| 11 | 0.514716 | `azmcp-functionapp-list` | ❌ |
| 12 | 0.502428 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.502199 | `azmcp-redis-cluster-list` | ❌ |
| 14 | 0.499161 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.497679 | `azmcp-appconfig-account-list` | ❌ |
| 16 | 0.486978 | `azmcp-group-list` | ❌ |
| 17 | 0.483046 | `azmcp-grafana-list` | ❌ |
| 18 | 0.474934 | `azmcp-postgres-server-list` | ❌ |
| 19 | 0.473458 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.472743 | `azmcp-storage-blob-list` | ❌ |

---

## Test 32

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** Show me my cosmosdb accounts  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.665447 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.605357 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.571613 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.473546 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.467671 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.443458 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.436283 | `azmcp-subscription-list` | ❌ |
| 8 | 0.431496 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 9 | 0.407809 | `azmcp-storage-blob-list` | ❌ |
| 10 | 0.390141 | `azmcp-kusto-database-list` | ❌ |
| 11 | 0.386108 | `azmcp-monitor-workspace-list` | ❌ |
| 12 | 0.383985 | `azmcp-appconfig-account-list` | ❌ |
| 13 | 0.381323 | `azmcp-sql-db-list` | ❌ |
| 14 | 0.379496 | `azmcp-appconfig-kv-show` | ❌ |
| 15 | 0.373667 | `azmcp-redis-cluster-list` | ❌ |
| 16 | 0.361373 | `azmcp-search-service-list` | ❌ |
| 17 | 0.359737 | `azmcp-search-index-list` | ❌ |
| 18 | 0.358441 | `azmcp-keyvault-key-list` | ❌ |
| 19 | 0.356034 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.353926 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 33

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** Show me the cosmosdb accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.752494 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.605125 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.566249 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.548174 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.546327 | `azmcp-subscription-list` | ❌ |
| 6 | 0.535227 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.513709 | `azmcp-search-service-list` | ❌ |
| 8 | 0.488006 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.483799 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.466414 | `azmcp-redis-cluster-list` | ❌ |
| 11 | 0.462400 | `azmcp-functionapp-list` | ❌ |
| 12 | 0.457584 | `azmcp-appconfig-account-list` | ❌ |
| 13 | 0.456219 | `azmcp-redis-cache-list` | ❌ |
| 14 | 0.454916 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.453626 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.441136 | `azmcp-grafana-list` | ❌ |
| 17 | 0.438277 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 18 | 0.433094 | `azmcp-postgres-server-list` | ❌ |
| 19 | 0.429996 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.426516 | `azmcp-sql-db-list` | ❌ |

---

## Test 34

**Expected Tool:** `azmcp-cosmos-database-container-item-query`  
**Prompt:** Show me the items that contain the word <search_term> in the container <container_name> in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.605253 | `azmcp-cosmos-database-container-list` | ❌ |
| 2 | 0.566854 | `azmcp-cosmos-database-container-item-query` | ✅ **EXPECTED** |
| 3 | 0.477874 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.469244 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.447757 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.417506 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.408739 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.398979 | `azmcp-search-service-list` | ❌ |
| 9 | 0.386227 | `azmcp-search-index-list` | ❌ |
| 10 | 0.384335 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.378151 | `azmcp-kusto-query` | ❌ |
| 12 | 0.351331 | `azmcp-kusto-table-list` | ❌ |
| 13 | 0.340982 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.335256 | `azmcp-sql-db-list` | ❌ |
| 15 | 0.334389 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.334381 | `azmcp-storage-blob-details` | ❌ |
| 17 | 0.331041 | `azmcp-kusto-sample` | ❌ |
| 18 | 0.326439 | `azmcp-monitor-resource-log-query` | ❌ |
| 19 | 0.302962 | `azmcp-appconfig-kv-show` | ❌ |
| 20 | 0.300284 | `azmcp-kusto-table-schema` | ❌ |

---

## Test 35

**Expected Tool:** `azmcp-cosmos-database-container-list`  
**Prompt:** List all the containers in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.852975 | `azmcp-cosmos-database-container-list` | ✅ **EXPECTED** |
| 2 | 0.690357 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.681132 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.630560 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.561393 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.535130 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.527734 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 8 | 0.473366 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.449121 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.441240 | `azmcp-storage-account-list` | ❌ |
| 11 | 0.439658 | `azmcp-sql-db-list` | ❌ |
| 12 | 0.427646 | `azmcp-kusto-table-list` | ❌ |
| 13 | 0.424542 | `azmcp-redis-cluster-database-list` | ❌ |
| 14 | 0.411456 | `azmcp-monitor-table-list` | ❌ |
| 15 | 0.392785 | `azmcp-postgres-database-list` | ❌ |
| 16 | 0.379324 | `azmcp-storage-blob-details` | ❌ |
| 17 | 0.378102 | `azmcp-keyvault-certificate-list` | ❌ |
| 18 | 0.372280 | `azmcp-kusto-cluster-list` | ❌ |
| 19 | 0.368267 | `azmcp-keyvault-key-list` | ❌ |
| 20 | 0.362881 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 36

**Expected Tool:** `azmcp-cosmos-database-container-list`  
**Prompt:** Show me the containers in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.789395 | `azmcp-cosmos-database-container-list` | ✅ **EXPECTED** |
| 2 | 0.614220 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.611374 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.562062 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.521532 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 6 | 0.474816 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.471019 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.449542 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.398064 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.397755 | `azmcp-sql-db-list` | ❌ |
| 11 | 0.395513 | `azmcp-kusto-table-list` | ❌ |
| 12 | 0.386806 | `azmcp-redis-cluster-database-list` | ❌ |
| 13 | 0.372499 | `azmcp-storage-blob-details` | ❌ |
| 14 | 0.362377 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.345665 | `azmcp-sql-db-show` | ❌ |
| 16 | 0.342544 | `azmcp-monitor-table-list` | ❌ |
| 17 | 0.319535 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.318540 | `azmcp-appconfig-kv-show` | ❌ |
| 19 | 0.314917 | `azmcp-kusto-table-schema` | ❌ |
| 20 | 0.311105 | `azmcp-acr-registry-list` | ❌ |

---

## Test 37

**Expected Tool:** `azmcp-cosmos-database-list`  
**Prompt:** List all the databases in the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.815683 | `azmcp-cosmos-database-list` | ✅ **EXPECTED** |
| 2 | 0.668515 | `azmcp-cosmos-account-list` | ❌ |
| 3 | 0.665298 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.571319 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.555134 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.548066 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.526046 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.501477 | `azmcp-postgres-database-list` | ❌ |
| 9 | 0.500364 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.471453 | `azmcp-kusto-table-list` | ❌ |
| 11 | 0.459194 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 12 | 0.455974 | `azmcp-storage-account-list` | ❌ |
| 13 | 0.450854 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.439548 | `azmcp-storage-blob-list` | ❌ |
| 15 | 0.405939 | `azmcp-keyvault-key-list` | ❌ |
| 16 | 0.401638 | `azmcp-subscription-list` | ❌ |
| 17 | 0.397642 | `azmcp-keyvault-certificate-list` | ❌ |
| 18 | 0.396808 | `azmcp-search-index-list` | ❌ |
| 19 | 0.389032 | `azmcp-keyvault-secret-list` | ❌ |
| 20 | 0.384106 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 38

**Expected Tool:** `azmcp-cosmos-database-list`  
**Prompt:** Show me the databases in the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.749370 | `azmcp-cosmos-database-list` | ✅ **EXPECTED** |
| 2 | 0.624759 | `azmcp-cosmos-database-container-list` | ❌ |
| 3 | 0.614572 | `azmcp-cosmos-account-list` | ❌ |
| 4 | 0.524837 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.505363 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.498206 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.497414 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.456313 | `azmcp-storage-blob-container-list` | ❌ |
| 9 | 0.449759 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.447875 | `azmcp-postgres-database-list` | ❌ |
| 11 | 0.437993 | `azmcp-kusto-table-list` | ❌ |
| 12 | 0.399935 | `azmcp-storage-account-list` | ❌ |
| 13 | 0.396280 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.384707 | `azmcp-storage-blob-list` | ❌ |
| 15 | 0.361429 | `azmcp-monitor-table-type-list` | ❌ |
| 16 | 0.361170 | `azmcp-sql-db-show` | ❌ |
| 17 | 0.344554 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.339401 | `azmcp-kusto-cluster-list` | ❌ |
| 19 | 0.335899 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.334745 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 39

**Expected Tool:** `azmcp-kusto-cluster-get`  
**Prompt:** Show me the details of the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.482148 | `azmcp-kusto-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.464523 | `azmcp-aks-cluster-get` | ❌ |
| 3 | 0.457669 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.416762 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.364174 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.362595 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.344871 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.344590 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.332631 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.326472 | `azmcp-redis-cache-list` | ❌ |
| 11 | 0.318754 | `azmcp-kusto-query` | ❌ |
| 12 | 0.314617 | `azmcp-kusto-table-schema` | ❌ |
| 13 | 0.304033 | `azmcp-storage-blob-container-details` | ❌ |
| 14 | 0.301024 | `azmcp-grafana-list` | ❌ |
| 15 | 0.300008 | `azmcp-kusto-table-list` | ❌ |
| 16 | 0.284474 | `azmcp-servicebus-queue-details` | ❌ |
| 17 | 0.269678 | `azmcp-sql-db-list` | ❌ |
| 18 | 0.249991 | `azmcp-storage-blob-details` | ❌ |
| 19 | 0.249854 | `azmcp-monitor-workspace-list` | ❌ |
| 20 | 0.247942 | `azmcp-sql-elastic-pool-list` | ❌ |

---

## Test 40

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** List all Data Explorer clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651159 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.644037 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.549093 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.535902 | `azmcp-aks-cluster-list` | ❌ |
| 5 | 0.509396 | `azmcp-grafana-list` | ❌ |
| 6 | 0.505912 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.492107 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.487882 | `azmcp-search-service-list` | ❌ |
| 9 | 0.487583 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.486159 | `azmcp-kusto-cluster-get` | ❌ |
| 11 | 0.460255 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.458754 | `azmcp-redis-cluster-database-list` | ❌ |
| 13 | 0.451500 | `azmcp-kusto-table-list` | ❌ |
| 14 | 0.428236 | `azmcp-storage-table-list` | ❌ |
| 15 | 0.427759 | `azmcp-subscription-list` | ❌ |
| 16 | 0.411791 | `azmcp-group-list` | ❌ |
| 17 | 0.407907 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.404889 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.402579 | `azmcp-storage-account-list` | ❌ |
| 20 | 0.395458 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 41

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** Show me my Data Explorer clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.437363 | `azmcp-redis-cluster-list` | ❌ |
| 2 | 0.391087 | `azmcp-redis-cluster-database-list` | ❌ |
| 3 | 0.386054 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 4 | 0.359551 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.341784 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.337747 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.314734 | `azmcp-aks-cluster-get` | ❌ |
| 8 | 0.303083 | `azmcp-grafana-list` | ❌ |
| 9 | 0.292838 | `azmcp-redis-cache-list` | ❌ |
| 10 | 0.287768 | `azmcp-kusto-sample` | ❌ |
| 11 | 0.285603 | `azmcp-kusto-query` | ❌ |
| 12 | 0.283331 | `azmcp-kusto-table-list` | ❌ |
| 13 | 0.270931 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.264112 | `azmcp-monitor-table-type-list` | ❌ |
| 15 | 0.264035 | `azmcp-monitor-workspace-list` | ❌ |
| 16 | 0.261960 | `azmcp-postgres-server-list` | ❌ |
| 17 | 0.257166 | `azmcp-cosmos-database-list` | ❌ |
| 18 | 0.255960 | `azmcp-postgres-database-list` | ❌ |
| 19 | 0.240044 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 20 | 0.232059 | `azmcp-storage-table-list` | ❌ |

---

## Test 42

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** Show me the Data Explorer clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584053 | `azmcp-redis-cluster-list` | ❌ |
| 2 | 0.549726 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 3 | 0.470903 | `azmcp-aks-cluster-list` | ❌ |
| 4 | 0.469570 | `azmcp-kusto-cluster-get` | ❌ |
| 5 | 0.464294 | `azmcp-kusto-database-list` | ❌ |
| 6 | 0.462945 | `azmcp-grafana-list` | ❌ |
| 7 | 0.446124 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.440326 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.432048 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.421307 | `azmcp-search-service-list` | ❌ |
| 11 | 0.396253 | `azmcp-redis-cluster-database-list` | ❌ |
| 12 | 0.392541 | `azmcp-kusto-table-list` | ❌ |
| 13 | 0.386776 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.377490 | `azmcp-kusto-query` | ❌ |
| 15 | 0.371088 | `azmcp-subscription-list` | ❌ |
| 16 | 0.366318 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.365972 | `azmcp-storage-table-list` | ❌ |
| 18 | 0.352162 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.347845 | `azmcp-aks-cluster-get` | ❌ |
| 20 | 0.334862 | `azmcp-storage-account-list` | ❌ |

---

## Test 43

**Expected Tool:** `azmcp-kusto-database-list`  
**Prompt:** List all databases in the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628129 | `azmcp-redis-cluster-database-list` | ❌ |
| 2 | 0.610646 | `azmcp-kusto-database-list` | ✅ **EXPECTED** |
| 3 | 0.553218 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.549673 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.474354 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.461496 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.459180 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.434330 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.431606 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.404095 | `azmcp-monitor-table-list` | ❌ |
| 11 | 0.396060 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.379966 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.375535 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.363663 | `azmcp-postgres-server-list` | ❌ |
| 15 | 0.357739 | `azmcp-monitor-table-type-list` | ❌ |
| 16 | 0.349789 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.340735 | `azmcp-redis-cache-list` | ❌ |
| 18 | 0.334270 | `azmcp-grafana-list` | ❌ |
| 19 | 0.320757 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.318850 | `azmcp-kusto-query` | ❌ |

---

## Test 44

**Expected Tool:** `azmcp-kusto-database-list`  
**Prompt:** Show me the databases in the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.597975 | `azmcp-redis-cluster-database-list` | ❌ |
| 2 | 0.558503 | `azmcp-kusto-database-list` | ✅ **EXPECTED** |
| 3 | 0.497144 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.486732 | `azmcp-postgres-database-list` | ❌ |
| 5 | 0.440064 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.427251 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.422588 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.383612 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.368013 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.362905 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.359378 | `azmcp-monitor-table-list` | ❌ |
| 12 | 0.338777 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.336400 | `azmcp-monitor-table-type-list` | ❌ |
| 14 | 0.336104 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.334803 | `azmcp-kusto-table-schema` | ❌ |
| 16 | 0.330351 | `azmcp-postgres-server-list` | ❌ |
| 17 | 0.313195 | `azmcp-redis-cache-list` | ❌ |
| 18 | 0.310346 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.309809 | `azmcp-kusto-sample` | ❌ |
| 20 | 0.305756 | `azmcp-kusto-query` | ❌ |

---

## Test 45

**Expected Tool:** `azmcp-kusto-query`  
**Prompt:** Show me all items that contain the word <search_term> in the Data Explorer table <table_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.381352 | `azmcp-kusto-query` | ✅ **EXPECTED** |
| 2 | 0.363252 | `azmcp-kusto-sample` | ❌ |
| 3 | 0.349147 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.345799 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.334762 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.319112 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.318883 | `azmcp-kusto-table-schema` | ❌ |
| 8 | 0.314961 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.308113 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.304014 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 11 | 0.302894 | `azmcp-postgres-table-list` | ❌ |
| 12 | 0.300427 | `azmcp-search-service-list` | ❌ |
| 13 | 0.300418 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.295070 | `azmcp-search-index-list` | ❌ |
| 15 | 0.292089 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.279867 | `azmcp-monitor-resource-log-query` | ❌ |
| 17 | 0.264026 | `azmcp-grafana-list` | ❌ |
| 18 | 0.263085 | `azmcp-kusto-cluster-get` | ❌ |
| 19 | 0.257738 | `azmcp-postgres-database-list` | ❌ |
| 20 | 0.257542 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 46

**Expected Tool:** `azmcp-kusto-sample`  
**Prompt:** Show me a data sample from the Data Explorer table <table_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.537154 | `azmcp-kusto-sample` | ✅ **EXPECTED** |
| 2 | 0.419463 | `azmcp-kusto-table-schema` | ❌ |
| 3 | 0.391423 | `azmcp-kusto-table-list` | ❌ |
| 4 | 0.377056 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.364611 | `azmcp-postgres-table-schema-get` | ❌ |
| 6 | 0.361845 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.343671 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.341674 | `azmcp-monitor-table-list` | ❌ |
| 9 | 0.337281 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.329933 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.319239 | `azmcp-kusto-query` | ❌ |
| 12 | 0.318189 | `azmcp-postgres-table-list` | ❌ |
| 13 | 0.310196 | `azmcp-kusto-cluster-get` | ❌ |
| 14 | 0.285956 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.267689 | `azmcp-aks-cluster-get` | ❌ |
| 16 | 0.257683 | `azmcp-monitor-resource-log-query` | ❌ |
| 17 | 0.254555 | `azmcp-postgres-database-list` | ❌ |
| 18 | 0.249214 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.243553 | `azmcp-postgres-server-list` | ❌ |
| 20 | 0.240744 | `azmcp-grafana-list` | ❌ |

---

## Test 47

**Expected Tool:** `azmcp-kusto-table-list`  
**Prompt:** List all tables in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591668 | `azmcp-kusto-table-list` | ✅ **EXPECTED** |
| 2 | 0.585237 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.550007 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.521516 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.520802 | `azmcp-redis-cluster-database-list` | ❌ |
| 6 | 0.517077 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.475496 | `azmcp-postgres-database-list` | ❌ |
| 8 | 0.464341 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.449656 | `azmcp-kusto-table-schema` | ❌ |
| 10 | 0.436518 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.429278 | `azmcp-redis-cluster-list` | ❌ |
| 12 | 0.412275 | `azmcp-kusto-sample` | ❌ |
| 13 | 0.410385 | `azmcp-kusto-cluster-list` | ❌ |
| 14 | 0.384895 | `azmcp-postgres-table-schema-get` | ❌ |
| 15 | 0.380671 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.361884 | `azmcp-sql-db-list` | ❌ |
| 17 | 0.349204 | `azmcp-postgres-server-list` | ❌ |
| 18 | 0.337427 | `azmcp-kusto-query` | ❌ |
| 19 | 0.329892 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.329669 | `azmcp-grafana-list` | ❌ |

---

## Test 48

**Expected Tool:** `azmcp-kusto-table-list`  
**Prompt:** Show me the tables in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.549885 | `azmcp-kusto-table-list` | ✅ **EXPECTED** |
| 2 | 0.523432 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.494108 | `azmcp-redis-cluster-database-list` | ❌ |
| 4 | 0.490717 | `azmcp-monitor-table-list` | ❌ |
| 5 | 0.475412 | `azmcp-kusto-database-list` | ❌ |
| 6 | 0.466302 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.466212 | `azmcp-kusto-table-schema` | ❌ |
| 8 | 0.431964 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.425623 | `azmcp-kusto-sample` | ❌ |
| 10 | 0.421413 | `azmcp-postgres-database-list` | ❌ |
| 11 | 0.403445 | `azmcp-redis-cluster-list` | ❌ |
| 12 | 0.402646 | `azmcp-postgres-table-schema-get` | ❌ |
| 13 | 0.391081 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.367163 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.348891 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.335264 | `azmcp-sql-db-list` | ❌ |
| 17 | 0.330383 | `azmcp-kusto-query` | ❌ |
| 18 | 0.326690 | `azmcp-postgres-server-list` | ❌ |
| 19 | 0.314766 | `azmcp-kusto-cluster-get` | ❌ |
| 20 | 0.300035 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 49

**Expected Tool:** `azmcp-kusto-table-schema`  
**Prompt:** Show me the schema for table <table_name> in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.588175 | `azmcp-kusto-table-schema` | ✅ **EXPECTED** |
| 2 | 0.564349 | `azmcp-postgres-table-schema-get` | ❌ |
| 3 | 0.437383 | `azmcp-kusto-table-list` | ❌ |
| 4 | 0.432525 | `azmcp-kusto-sample` | ❌ |
| 5 | 0.413648 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.398505 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.387630 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.366309 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.365927 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.357515 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.345139 | `azmcp-redis-cluster-list` | ❌ |
| 12 | 0.314500 | `azmcp-kusto-cluster-get` | ❌ |
| 13 | 0.309072 | `azmcp-postgres-database-list` | ❌ |
| 14 | 0.308459 | `azmcp-sql-db-show` | ❌ |
| 15 | 0.298184 | `azmcp-kusto-query` | ❌ |
| 16 | 0.294717 | `azmcp-cosmos-database-list` | ❌ |
| 17 | 0.282579 | `azmcp-kusto-cluster-list` | ❌ |
| 18 | 0.275699 | `azmcp-cosmos-database-container-list` | ❌ |
| 19 | 0.273917 | `azmcp-aks-cluster-get` | ❌ |
| 20 | 0.273511 | `azmcp-sql-db-list` | ❌ |

---

## Test 50

**Expected Tool:** `azmcp-postgres-database-list`  
**Prompt:** List all PostgreSQL databases in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.815617 | `azmcp-postgres-database-list` | ✅ **EXPECTED** |
| 2 | 0.644014 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.622790 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.542685 | `azmcp-postgres-server-config-get` | ❌ |
| 5 | 0.490904 | `azmcp-postgres-server-param-get` | ❌ |
| 6 | 0.453436 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.444410 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.435828 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.418348 | `azmcp-postgres-database-query` | ❌ |
| 10 | 0.414679 | `azmcp-postgres-server-param-set` | ❌ |
| 11 | 0.413602 | `azmcp-postgres-table-schema-get` | ❌ |
| 12 | 0.407877 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.319946 | `azmcp-kusto-table-list` | ❌ |
| 14 | 0.293787 | `azmcp-cosmos-database-container-list` | ❌ |
| 15 | 0.292441 | `azmcp-cosmos-account-list` | ❌ |
| 16 | 0.289334 | `azmcp-grafana-list` | ❌ |
| 17 | 0.252461 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.249494 | `azmcp-kusto-cluster-list` | ❌ |
| 19 | 0.245456 | `azmcp-group-list` | ❌ |
| 20 | 0.243466 | `azmcp-acr-registry-list` | ❌ |

---

## Test 51

**Expected Tool:** `azmcp-postgres-database-list`  
**Prompt:** Show me the PostgreSQL databases in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.760033 | `azmcp-postgres-database-list` | ✅ **EXPECTED** |
| 2 | 0.589783 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.585891 | `azmcp-postgres-table-list` | ❌ |
| 4 | 0.552660 | `azmcp-postgres-server-config-get` | ❌ |
| 5 | 0.495629 | `azmcp-postgres-server-param-get` | ❌ |
| 6 | 0.433860 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.430589 | `azmcp-postgres-table-schema-get` | ❌ |
| 8 | 0.426839 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.416937 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.412972 | `azmcp-postgres-server-param-set` | ❌ |
| 11 | 0.385475 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.365997 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.281529 | `azmcp-kusto-table-list` | ❌ |
| 14 | 0.261442 | `azmcp-cosmos-database-container-list` | ❌ |
| 15 | 0.257971 | `azmcp-grafana-list` | ❌ |
| 16 | 0.247726 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.235403 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.228013 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.222503 | `azmcp-kusto-table-schema` | ❌ |
| 20 | 0.212647 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 52

**Expected Tool:** `azmcp-postgres-database-query`  
**Prompt:** Show me all items that contain the word <search_term> in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.546211 | `azmcp-postgres-database-list` | ❌ |
| 2 | 0.503267 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.466599 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.415817 | `azmcp-postgres-database-query` | ✅ **EXPECTED** |
| 5 | 0.403969 | `azmcp-postgres-server-param-get` | ❌ |
| 6 | 0.403924 | `azmcp-postgres-server-config-get` | ❌ |
| 7 | 0.380446 | `azmcp-postgres-table-schema-get` | ❌ |
| 8 | 0.354323 | `azmcp-postgres-server-param-set` | ❌ |
| 9 | 0.301808 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.277622 | `azmcp-sql-db-show` | ❌ |
| 11 | 0.264914 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 12 | 0.262356 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.262160 | `azmcp-kusto-query` | ❌ |
| 14 | 0.254174 | `azmcp-kusto-table-list` | ❌ |
| 15 | 0.248628 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.244295 | `azmcp-kusto-database-list` | ❌ |
| 17 | 0.236363 | `azmcp-grafana-list` | ❌ |
| 18 | 0.218677 | `azmcp-kusto-table-schema` | ❌ |
| 19 | 0.217855 | `azmcp-kusto-sample` | ❌ |
| 20 | 0.189002 | `azmcp-foundry-models-list` | ❌ |

---

## Test 53

**Expected Tool:** `azmcp-postgres-server-config-get`  
**Prompt:** Show me the configuration of PostgreSQL server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.756593 | `azmcp-postgres-server-config-get` | ✅ **EXPECTED** |
| 2 | 0.599471 | `azmcp-postgres-server-param-get` | ❌ |
| 3 | 0.535229 | `azmcp-postgres-server-param-set` | ❌ |
| 4 | 0.535049 | `azmcp-postgres-database-list` | ❌ |
| 5 | 0.518574 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.463172 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.431476 | `azmcp-postgres-table-schema-get` | ❌ |
| 8 | 0.394675 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.269224 | `azmcp-appconfig-kv-list` | ❌ |
| 10 | 0.269018 | `azmcp-sql-db-list` | ❌ |
| 11 | 0.261733 | `azmcp-sql-db-show` | ❌ |
| 12 | 0.235724 | `azmcp-loadtesting-testrun-list` | ❌ |
| 13 | 0.222849 | `azmcp-appconfig-account-list` | ❌ |
| 14 | 0.222599 | `azmcp-loadtesting-test-get` | ❌ |
| 15 | 0.208314 | `azmcp-appconfig-kv-show` | ❌ |
| 16 | 0.174936 | `azmcp-aks-cluster-get` | ❌ |
| 17 | 0.168322 | `azmcp-kusto-table-schema` | ❌ |
| 18 | 0.160792 | `azmcp-grafana-list` | ❌ |
| 19 | 0.156689 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.154250 | `azmcp-appconfig-kv-unlock` | ❌ |

---

## Test 54

**Expected Tool:** `azmcp-postgres-server-list`  
**Prompt:** List all PostgreSQL servers in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.900023 | `azmcp-postgres-server-list` | ✅ **EXPECTED** |
| 2 | 0.640733 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.565914 | `azmcp-postgres-table-list` | ❌ |
| 4 | 0.538997 | `azmcp-postgres-server-config-get` | ❌ |
| 5 | 0.507621 | `azmcp-postgres-server-param-get` | ❌ |
| 6 | 0.483663 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.472458 | `azmcp-grafana-list` | ❌ |
| 8 | 0.453788 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.446509 | `azmcp-redis-cache-list` | ❌ |
| 10 | 0.430475 | `azmcp-search-service-list` | ❌ |
| 11 | 0.406887 | `azmcp-monitor-workspace-list` | ❌ |
| 12 | 0.406617 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.401097 | `azmcp-sql-db-list` | ❌ |
| 14 | 0.399172 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.397428 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.389191 | `azmcp-appconfig-account-list` | ❌ |
| 17 | 0.373699 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.365995 | `azmcp-group-list` | ❌ |
| 19 | 0.352057 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.346981 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 55

**Expected Tool:** `azmcp-postgres-server-list`  
**Prompt:** Show me my PostgreSQL servers  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.674327 | `azmcp-postgres-server-list` | ✅ **EXPECTED** |
| 2 | 0.607062 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.576349 | `azmcp-postgres-server-config-get` | ❌ |
| 4 | 0.522996 | `azmcp-postgres-table-list` | ❌ |
| 5 | 0.506171 | `azmcp-postgres-server-param-get` | ❌ |
| 6 | 0.409406 | `azmcp-postgres-database-query` | ❌ |
| 7 | 0.400088 | `azmcp-postgres-server-param-set` | ❌ |
| 8 | 0.372955 | `azmcp-postgres-table-schema-get` | ❌ |
| 9 | 0.318087 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.305392 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 11 | 0.274763 | `azmcp-grafana-list` | ❌ |
| 12 | 0.260533 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.253264 | `azmcp-kusto-database-list` | ❌ |
| 14 | 0.245020 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.241748 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.239500 | `azmcp-appconfig-account-list` | ❌ |
| 17 | 0.229842 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.227547 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.219274 | `azmcp-cosmos-database-container-list` | ❌ |
| 20 | 0.218819 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 56

**Expected Tool:** `azmcp-postgres-server-list`  
**Prompt:** Show me the PostgreSQL servers in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.832155 | `azmcp-postgres-server-list` | ✅ **EXPECTED** |
| 2 | 0.579232 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.531804 | `azmcp-postgres-server-config-get` | ❌ |
| 4 | 0.514445 | `azmcp-postgres-table-list` | ❌ |
| 5 | 0.505869 | `azmcp-postgres-server-param-get` | ❌ |
| 6 | 0.452608 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.444127 | `azmcp-grafana-list` | ❌ |
| 8 | 0.414695 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.411590 | `azmcp-search-service-list` | ❌ |
| 10 | 0.410719 | `azmcp-postgres-database-query` | ❌ |
| 11 | 0.403470 | `azmcp-kusto-cluster-list` | ❌ |
| 12 | 0.399866 | `azmcp-postgres-table-schema-get` | ❌ |
| 13 | 0.376954 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.362650 | `azmcp-kusto-database-list` | ❌ |
| 15 | 0.362557 | `azmcp-appconfig-account-list` | ❌ |
| 16 | 0.360509 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.358409 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.334225 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.325681 | `azmcp-group-list` | ❌ |
| 20 | 0.311792 | `azmcp-marketplace-product-get` | ❌ |

---

## Test 57

**Expected Tool:** `azmcp-postgres-server-param`  
**Prompt:** Show me if the parameter my PostgreSQL server <server> has replication enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.594762 | `azmcp-postgres-server-param-get` | ❌ |
| 2 | 0.539670 | `azmcp-postgres-server-config-get` | ❌ |
| 3 | 0.489708 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.480763 | `azmcp-postgres-server-param-set` | ❌ |
| 5 | 0.451871 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.357567 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.330790 | `azmcp-postgres-table-schema-get` | ❌ |
| 8 | 0.305277 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.228058 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.207596 | `azmcp-sql-db-list` | ❌ |
| 11 | 0.185330 | `azmcp-appconfig-kv-list` | ❌ |
| 12 | 0.174099 | `azmcp-grafana-list` | ❌ |
| 13 | 0.169204 | `azmcp-appconfig-account-list` | ❌ |
| 14 | 0.158164 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.155817 | `azmcp-appconfig-kv-show` | ❌ |
| 16 | 0.145392 | `azmcp-loadtesting-testrun-list` | ❌ |
| 17 | 0.145073 | `azmcp-kusto-database-list` | ❌ |
| 18 | 0.142405 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.140200 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.138704 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 58

**Expected Tool:** `azmcp-postgres-server-param-set`  
**Prompt:** Enable replication for my PostgreSQL server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.488474 | `azmcp-postgres-server-config-get` | ❌ |
| 2 | 0.469794 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.464562 | `azmcp-postgres-server-param-set` | ✅ **EXPECTED** |
| 4 | 0.447011 | `azmcp-postgres-server-param-get` | ❌ |
| 5 | 0.440760 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.354049 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.341624 | `azmcp-postgres-database-query` | ❌ |
| 8 | 0.317484 | `azmcp-postgres-table-schema-get` | ❌ |
| 9 | 0.184982 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.176538 | `azmcp-sql-db-list` | ❌ |
| 11 | 0.133385 | `azmcp-kusto-sample` | ❌ |
| 12 | 0.127120 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.123491 | `azmcp-kusto-table-schema` | ❌ |
| 14 | 0.118089 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.114978 | `azmcp-kusto-cluster-get` | ❌ |
| 16 | 0.113841 | `azmcp-grafana-list` | ❌ |
| 17 | 0.108485 | `azmcp-kusto-table-list` | ❌ |
| 18 | 0.102847 | `azmcp-extension-azqr` | ❌ |
| 19 | 0.102298 | `azmcp-loadtesting-testrun-list` | ❌ |
| 20 | 0.102139 | `azmcp-appconfig-kv-list` | ❌ |

---

## Test 59

**Expected Tool:** `azmcp-postgres-table-list`  
**Prompt:** List all tables in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.789883 | `azmcp-postgres-table-list` | ✅ **EXPECTED** |
| 2 | 0.750580 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.574930 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.519820 | `azmcp-postgres-table-schema-get` | ❌ |
| 5 | 0.501400 | `azmcp-postgres-server-config-get` | ❌ |
| 6 | 0.449190 | `azmcp-postgres-database-query` | ❌ |
| 7 | 0.432813 | `azmcp-kusto-table-list` | ❌ |
| 8 | 0.430171 | `azmcp-postgres-server-param-get` | ❌ |
| 9 | 0.394396 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.386992 | `azmcp-redis-cluster-database-list` | ❌ |
| 11 | 0.380821 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.373673 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.352211 | `azmcp-kusto-database-list` | ❌ |
| 14 | 0.308203 | `azmcp-kusto-table-schema` | ❌ |
| 15 | 0.299785 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.257808 | `azmcp-grafana-list` | ❌ |
| 17 | 0.256245 | `azmcp-kusto-sample` | ❌ |
| 18 | 0.249162 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.236931 | `azmcp-appconfig-kv-list` | ❌ |
| 20 | 0.229856 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 60

**Expected Tool:** `azmcp-postgres-table-list`  
**Prompt:** Show me the tables in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.736083 | `azmcp-postgres-table-list` | ✅ **EXPECTED** |
| 2 | 0.690112 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.558357 | `azmcp-postgres-table-schema-get` | ❌ |
| 4 | 0.543331 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.521570 | `azmcp-postgres-server-config-get` | ❌ |
| 6 | 0.464929 | `azmcp-postgres-database-query` | ❌ |
| 7 | 0.447184 | `azmcp-postgres-server-param-get` | ❌ |
| 8 | 0.390392 | `azmcp-kusto-table-list` | ❌ |
| 9 | 0.371151 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.371036 | `azmcp-postgres-server-param-set` | ❌ |
| 11 | 0.360749 | `azmcp-monitor-table-list` | ❌ |
| 12 | 0.334843 | `azmcp-kusto-table-schema` | ❌ |
| 13 | 0.315781 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.307262 | `azmcp-kusto-database-list` | ❌ |
| 15 | 0.272906 | `azmcp-kusto-sample` | ❌ |
| 16 | 0.266178 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.243542 | `azmcp-grafana-list` | ❌ |
| 18 | 0.207521 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.205697 | `azmcp-appconfig-kv-list` | ❌ |
| 20 | 0.202860 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 61

**Expected Tool:** `azmcp-postgres-table-schema-get`  
**Prompt:** Show me the schema of table <table> in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.714877 | `azmcp-postgres-table-schema-get` | ✅ **EXPECTED** |
| 2 | 0.597846 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.574230 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.508082 | `azmcp-postgres-server-config-get` | ❌ |
| 5 | 0.475665 | `azmcp-kusto-table-schema` | ❌ |
| 6 | 0.443816 | `azmcp-postgres-server-param-get` | ❌ |
| 7 | 0.442553 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.427530 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.362687 | `azmcp-postgres-server-param-set` | ❌ |
| 10 | 0.336037 | `azmcp-sql-db-show` | ❌ |
| 11 | 0.322766 | `azmcp-kusto-table-list` | ❌ |
| 12 | 0.312465 | `azmcp-monitor-table-list` | ❌ |
| 13 | 0.303748 | `azmcp-kusto-sample` | ❌ |
| 14 | 0.253353 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.239225 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.212206 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.201673 | `azmcp-grafana-list` | ❌ |
| 18 | 0.185124 | `azmcp-appconfig-kv-list` | ❌ |
| 19 | 0.183782 | `azmcp-bicepschema-get` | ❌ |
| 20 | 0.167077 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 62

**Expected Tool:** `azmcp-extension-azd`  
**Prompt:** Create a To-Do list web application that uses NodeJS and MongoDB  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.241366 | `azmcp-extension-az` | ❌ |
| 2 | 0.196585 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 3 | 0.185408 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 4 | 0.181543 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.177946 | `azmcp-cosmos-database-list` | ❌ |
| 6 | 0.173269 | `azmcp-extension-azd` | ✅ **EXPECTED** |
| 7 | 0.165761 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.151402 | `azmcp-loadtesting-testresource-create` | ❌ |
| 9 | 0.151015 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.148838 | `azmcp-bestpractices-get` | ❌ |
| 11 | 0.148122 | `azmcp-postgres-database-list` | ❌ |
| 12 | 0.145985 | `azmcp-storage-share-file-list` | ❌ |
| 13 | 0.145027 | `azmcp-redis-cluster-list` | ❌ |
| 14 | 0.143077 | `azmcp-loadtesting-test-create` | ❌ |
| 15 | 0.142503 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.138516 | `azmcp-storage-blob-container-create` | ❌ |
| 17 | 0.137936 | `azmcp-postgres-database-query` | ❌ |
| 18 | 0.129433 | `azmcp-sql-db-list` | ❌ |
| 19 | 0.126407 | `azmcp-storage-blob-list` | ❌ |
| 20 | 0.126141 | `azmcp-postgres-server-list` | ❌ |

---

## Test 63

**Expected Tool:** `azmcp-extension-azd`  
**Prompt:** Deploy my web application to Azure App Service  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.437357 | `azmcp-foundry-models-deploy` | ❌ |
| 2 | 0.364145 | `azmcp-extension-azd` | ✅ **EXPECTED** |
| 3 | 0.361106 | `azmcp-foundry-models-deployments-list` | ❌ |
| 4 | 0.356426 | `azmcp-extension-az` | ❌ |
| 5 | 0.345434 | `azmcp-functionapp-list` | ❌ |
| 6 | 0.340372 | `azmcp-bestpractices-get` | ❌ |
| 7 | 0.320091 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 8 | 0.307826 | `azmcp-loadtesting-test-create` | ❌ |
| 9 | 0.299738 | `azmcp-search-index-list` | ❌ |
| 10 | 0.297374 | `azmcp-workbooks-delete` | ❌ |
| 11 | 0.276267 | `azmcp-keyvault-certificate-create` | ❌ |
| 12 | 0.273452 | `azmcp-search-service-list` | ❌ |
| 13 | 0.259689 | `azmcp-extension-azqr` | ❌ |
| 14 | 0.250828 | `azmcp-storage-queue-message-send` | ❌ |
| 15 | 0.244902 | `azmcp-sql-db-list` | ❌ |
| 16 | 0.239027 | `azmcp-storage-account-create` | ❌ |
| 17 | 0.236376 | `azmcp-sql-db-show` | ❌ |
| 18 | 0.227749 | `azmcp-search-index-query` | ❌ |
| 19 | 0.225631 | `azmcp-subscription-list` | ❌ |
| 20 | 0.223004 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 64

**Expected Tool:** `azmcp-functionapp-list`  
**Prompt:** List all function apps in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.782285 | `azmcp-functionapp-list` | ✅ **EXPECTED** |
| 2 | 0.547255 | `azmcp-search-service-list` | ❌ |
| 3 | 0.516618 | `azmcp-cosmos-account-list` | ❌ |
| 4 | 0.516217 | `azmcp-appconfig-account-list` | ❌ |
| 5 | 0.489404 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.485259 | `azmcp-subscription-list` | ❌ |
| 7 | 0.474299 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.465575 | `azmcp-group-list` | ❌ |
| 9 | 0.464534 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.455718 | `azmcp-aks-cluster-list` | ❌ |
| 11 | 0.455388 | `azmcp-postgres-server-list` | ❌ |
| 12 | 0.451429 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.445099 | `azmcp-redis-cache-list` | ❌ |
| 14 | 0.442614 | `azmcp-redis-cluster-list` | ❌ |
| 15 | 0.432144 | `azmcp-grafana-list` | ❌ |
| 16 | 0.414796 | `azmcp-foundry-models-deployments-list` | ❌ |
| 17 | 0.411904 | `azmcp-sql-db-list` | ❌ |
| 18 | 0.411748 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.411426 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.406081 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 65

**Expected Tool:** `azmcp-functionapp-list`  
**Prompt:** Show me my Azure function apps  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.610253 | `azmcp-functionapp-list` | ✅ **EXPECTED** |
| 2 | 0.385832 | `azmcp-foundry-models-deployments-list` | ❌ |
| 3 | 0.374655 | `azmcp-appconfig-account-list` | ❌ |
| 4 | 0.372790 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.369681 | `azmcp-subscription-list` | ❌ |
| 6 | 0.368018 | `azmcp-extension-az` | ❌ |
| 7 | 0.359788 | `azmcp-search-service-list` | ❌ |
| 8 | 0.358879 | `azmcp-bestpractices-get` | ❌ |
| 9 | 0.353279 | `azmcp-extension-azd` | ❌ |
| 10 | 0.345104 | `azmcp-storage-blob-container-list` | ❌ |
| 11 | 0.341159 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.339976 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 13 | 0.336259 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 14 | 0.334019 | `azmcp-role-assignment-list` | ❌ |
| 15 | 0.333136 | `azmcp-sql-db-list` | ❌ |
| 16 | 0.329221 | `azmcp-storage-account-list` | ❌ |
| 17 | 0.327870 | `azmcp-monitor-workspace-list` | ❌ |
| 18 | 0.322615 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.318217 | `azmcp-search-index-list` | ❌ |
| 20 | 0.301794 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 66

**Expected Tool:** `azmcp-functionapp-list`  
**Prompt:** What function apps do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.440788 | `azmcp-functionapp-list` | ✅ **EXPECTED** |
| 2 | 0.256927 | `azmcp-extension-az` | ❌ |
| 3 | 0.249658 | `azmcp-appconfig-account-list` | ❌ |
| 4 | 0.244782 | `azmcp-appconfig-kv-list` | ❌ |
| 5 | 0.239514 | `azmcp-foundry-models-deployments-list` | ❌ |
| 6 | 0.235352 | `azmcp-extension-azd` | ❌ |
| 7 | 0.208396 | `azmcp-foundry-models-list` | ❌ |
| 8 | 0.201390 | `azmcp-bestpractices-get` | ❌ |
| 9 | 0.200777 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.195857 | `azmcp-role-assignment-list` | ❌ |
| 11 | 0.190894 | `azmcp-appconfig-kv-show` | ❌ |
| 12 | 0.189381 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 13 | 0.185142 | `azmcp-monitor-resource-log-query` | ❌ |
| 14 | 0.184120 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.182124 | `azmcp-storage-table-list` | ❌ |
| 16 | 0.181525 | `azmcp-storage-share-file-list` | ❌ |
| 17 | 0.179181 | `azmcp-storage-blob-details` | ❌ |
| 18 | 0.175281 | `azmcp-subscription-list` | ❌ |
| 19 | 0.172273 | `azmcp-workbooks-list` | ❌ |
| 20 | 0.172081 | `azmcp-storage-account-list` | ❌ |

---

## Test 67

**Expected Tool:** `azmcp-keyvault-certificate-create`  
**Prompt:** Create a new certificate called <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.740547 | `azmcp-keyvault-certificate-create` | ✅ **EXPECTED** |
| 2 | 0.595850 | `azmcp-keyvault-key-create` | ❌ |
| 3 | 0.590531 | `azmcp-keyvault-secret-create` | ❌ |
| 4 | 0.575960 | `azmcp-keyvault-certificate-list` | ❌ |
| 5 | 0.543043 | `azmcp-keyvault-certificate-get` | ❌ |
| 6 | 0.434841 | `azmcp-keyvault-key-list` | ❌ |
| 7 | 0.414022 | `azmcp-keyvault-secret-list` | ❌ |
| 8 | 0.394768 | `azmcp-storage-account-create` | ❌ |
| 9 | 0.330026 | `azmcp-appconfig-kv-set` | ❌ |
| 10 | 0.308667 | `azmcp-loadtesting-test-create` | ❌ |
| 11 | 0.300980 | `azmcp-storage-datalake-directory-create` | ❌ |
| 12 | 0.294330 | `azmcp-loadtesting-testresource-create` | ❌ |
| 13 | 0.285184 | `azmcp-workbooks-create` | ❌ |
| 14 | 0.235260 | `azmcp-storage-blob-container-list` | ❌ |
| 15 | 0.233821 | `azmcp-storage-table-list` | ❌ |
| 16 | 0.227111 | `azmcp-storage-account-list` | ❌ |
| 17 | 0.219479 | `azmcp-subscription-list` | ❌ |
| 18 | 0.210729 | `azmcp-search-service-list` | ❌ |
| 19 | 0.208862 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 20 | 0.206476 | `azmcp-monitor-metrics-query` | ❌ |

---

## Test 68

**Expected Tool:** `azmcp-keyvault-certificate-get`  
**Prompt:** Show me the certificate <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.627979 | `azmcp-keyvault-certificate-get` | ✅ **EXPECTED** |
| 2 | 0.624457 | `azmcp-keyvault-certificate-list` | ❌ |
| 3 | 0.564636 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.493612 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.475385 | `azmcp-keyvault-secret-list` | ❌ |
| 6 | 0.423725 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.418861 | `azmcp-keyvault-secret-create` | ❌ |
| 8 | 0.390699 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.346167 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.331147 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.317250 | `azmcp-storage-blob-container-list` | ❌ |
| 12 | 0.317177 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.293933 | `azmcp-storage-account-list` | ❌ |
| 14 | 0.293421 | `azmcp-subscription-list` | ❌ |
| 15 | 0.288844 | `azmcp-storage-blob-list` | ❌ |
| 16 | 0.276581 | `azmcp-role-assignment-list` | ❌ |
| 17 | 0.273746 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 18 | 0.269735 | `azmcp-sql-db-show` | ❌ |
| 19 | 0.266867 | `azmcp-search-service-list` | ❌ |
| 20 | 0.263867 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

---

## Test 69

**Expected Tool:** `azmcp-keyvault-certificate-get`  
**Prompt:** Show me the details of the certificate <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.662324 | `azmcp-keyvault-certificate-get` | ✅ **EXPECTED** |
| 2 | 0.606534 | `azmcp-keyvault-certificate-list` | ❌ |
| 3 | 0.535270 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.499422 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.482380 | `azmcp-keyvault-secret-list` | ❌ |
| 6 | 0.415718 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.412434 | `azmcp-keyvault-secret-create` | ❌ |
| 8 | 0.411136 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.365386 | `azmcp-sql-db-show` | ❌ |
| 10 | 0.363228 | `azmcp-aks-cluster-get` | ❌ |
| 11 | 0.354726 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.332921 | `azmcp-storage-blob-container-details` | ❌ |
| 13 | 0.316364 | `azmcp-storage-blob-container-list` | ❌ |
| 14 | 0.315096 | `azmcp-storage-table-list` | ❌ |
| 15 | 0.305778 | `azmcp-subscription-list` | ❌ |
| 16 | 0.301699 | `azmcp-servicebus-queue-details` | ❌ |
| 17 | 0.295608 | `azmcp-storage-account-list` | ❌ |
| 18 | 0.290918 | `azmcp-storage-blob-list` | ❌ |
| 19 | 0.289520 | `azmcp-role-assignment-list` | ❌ |
| 20 | 0.283646 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 70

**Expected Tool:** `azmcp-keyvault-certificate-list`  
**Prompt:** List all certificates in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.762015 | `azmcp-keyvault-certificate-list` | ✅ **EXPECTED** |
| 2 | 0.637525 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.608799 | `azmcp-keyvault-secret-list` | ❌ |
| 4 | 0.566460 | `azmcp-keyvault-certificate-get` | ❌ |
| 5 | 0.538639 | `azmcp-keyvault-certificate-create` | ❌ |
| 6 | 0.478100 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.453226 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.440871 | `azmcp-storage-blob-container-list` | ❌ |
| 9 | 0.431201 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.429531 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.425440 | `azmcp-storage-account-list` | ❌ |
| 12 | 0.424367 | `azmcp-keyvault-key-create` | ❌ |
| 13 | 0.417908 | `azmcp-storage-blob-list` | ❌ |
| 14 | 0.408492 | `azmcp-keyvault-secret-create` | ❌ |
| 15 | 0.408042 | `azmcp-subscription-list` | ❌ |
| 16 | 0.373773 | `azmcp-search-index-list` | ❌ |
| 17 | 0.368412 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 18 | 0.358938 | `azmcp-role-assignment-list` | ❌ |
| 19 | 0.357371 | `azmcp-search-service-list` | ❌ |
| 20 | 0.340553 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |

---

## Test 71

**Expected Tool:** `azmcp-keyvault-certificate-list`  
**Prompt:** Show me the certificates in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.660576 | `azmcp-keyvault-certificate-list` | ✅ **EXPECTED** |
| 2 | 0.570282 | `azmcp-keyvault-certificate-get` | ❌ |
| 3 | 0.540172 | `azmcp-keyvault-key-list` | ❌ |
| 4 | 0.516722 | `azmcp-keyvault-secret-list` | ❌ |
| 5 | 0.508048 | `azmcp-keyvault-certificate-create` | ❌ |
| 6 | 0.420506 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.396048 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.390169 | `azmcp-keyvault-secret-create` | ❌ |
| 9 | 0.382983 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.382082 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.381033 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.372424 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.362782 | `azmcp-subscription-list` | ❌ |
| 14 | 0.361859 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.351372 | `azmcp-storage-blob-list` | ❌ |
| 16 | 0.323177 | `azmcp-role-assignment-list` | ❌ |
| 17 | 0.317459 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 18 | 0.317235 | `azmcp-search-index-list` | ❌ |
| 19 | 0.300672 | `azmcp-search-service-list` | ❌ |
| 20 | 0.296774 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

---

## Test 72

**Expected Tool:** `azmcp-keyvault-key-create`  
**Prompt:** Create a new key called <key_name> with the RSA type in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.676456 | `azmcp-keyvault-key-create` | ✅ **EXPECTED** |
| 2 | 0.569344 | `azmcp-keyvault-secret-create` | ❌ |
| 3 | 0.555807 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.465925 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.420699 | `azmcp-storage-account-create` | ❌ |
| 6 | 0.417391 | `azmcp-keyvault-certificate-list` | ❌ |
| 7 | 0.413149 | `azmcp-keyvault-secret-list` | ❌ |
| 8 | 0.397094 | `azmcp-appconfig-kv-set` | ❌ |
| 9 | 0.389754 | `azmcp-keyvault-certificate-get` | ❌ |
| 10 | 0.340703 | `azmcp-appconfig-kv-lock` | ❌ |
| 11 | 0.307097 | `azmcp-appconfig-kv-unlock` | ❌ |
| 12 | 0.287108 | `azmcp-storage-datalake-directory-create` | ❌ |
| 13 | 0.265912 | `azmcp-storage-account-list` | ❌ |
| 14 | 0.261795 | `azmcp-workbooks-create` | ❌ |
| 15 | 0.261088 | `azmcp-storage-blob-container-list` | ❌ |
| 16 | 0.252096 | `azmcp-storage-table-list` | ❌ |
| 17 | 0.235616 | `azmcp-storage-blob-list` | ❌ |
| 18 | 0.223521 | `azmcp-storage-queue-message-send` | ❌ |
| 19 | 0.215763 | `azmcp-subscription-list` | ❌ |
| 20 | 0.215338 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 73

**Expected Tool:** `azmcp-keyvault-key-list`  
**Prompt:** List all keys in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.737160 | `azmcp-keyvault-key-list` | ✅ **EXPECTED** |
| 2 | 0.650155 | `azmcp-keyvault-secret-list` | ❌ |
| 3 | 0.631528 | `azmcp-keyvault-certificate-list` | ❌ |
| 4 | 0.498767 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.473916 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.473155 | `azmcp-storage-blob-container-list` | ❌ |
| 7 | 0.468044 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.467321 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.461462 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.455805 | `azmcp-keyvault-certificate-get` | ❌ |
| 11 | 0.455016 | `azmcp-storage-blob-list` | ❌ |
| 12 | 0.443785 | `azmcp-cosmos-database-container-list` | ❌ |
| 13 | 0.439167 | `azmcp-appconfig-kv-list` | ❌ |
| 14 | 0.428290 | `azmcp-keyvault-secret-create` | ❌ |
| 15 | 0.426909 | `azmcp-subscription-list` | ❌ |
| 16 | 0.402742 | `azmcp-search-index-list` | ❌ |
| 17 | 0.378003 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 18 | 0.376452 | `azmcp-search-service-list` | ❌ |
| 19 | 0.360638 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 20 | 0.354970 | `azmcp-monitor-table-list` | ❌ |

---

## Test 74

**Expected Tool:** `azmcp-keyvault-key-list`  
**Prompt:** Show me the keys in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609587 | `azmcp-keyvault-key-list` | ✅ **EXPECTED** |
| 2 | 0.535381 | `azmcp-keyvault-secret-list` | ❌ |
| 3 | 0.520010 | `azmcp-keyvault-certificate-list` | ❌ |
| 4 | 0.479818 | `azmcp-keyvault-certificate-get` | ❌ |
| 5 | 0.462248 | `azmcp-keyvault-key-create` | ❌ |
| 6 | 0.429515 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.421475 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.411560 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.405205 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.390487 | `azmcp-storage-blob-container-list` | ❌ |
| 11 | 0.375139 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.373589 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.368554 | `azmcp-storage-account-list` | ❌ |
| 14 | 0.360209 | `azmcp-storage-blob-list` | ❌ |
| 15 | 0.353390 | `azmcp-subscription-list` | ❌ |
| 16 | 0.323360 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 17 | 0.320761 | `azmcp-search-index-list` | ❌ |
| 18 | 0.312486 | `azmcp-storage-blob-container-details` | ❌ |
| 19 | 0.307809 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 20 | 0.306567 | `azmcp-role-assignment-list` | ❌ |

---

## Test 75

**Expected Tool:** `azmcp-keyvault-secret-create`  
**Prompt:** Create a new secret called <secret_name> with value <secret_value> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.767701 | `azmcp-keyvault-secret-create` | ✅ **EXPECTED** |
| 2 | 0.613549 | `azmcp-keyvault-key-create` | ❌ |
| 3 | 0.572065 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.516457 | `azmcp-keyvault-secret-list` | ❌ |
| 5 | 0.461437 | `azmcp-appconfig-kv-set` | ❌ |
| 6 | 0.428374 | `azmcp-storage-account-create` | ❌ |
| 7 | 0.417672 | `azmcp-keyvault-key-list` | ❌ |
| 8 | 0.384262 | `azmcp-keyvault-certificate-list` | ❌ |
| 9 | 0.373932 | `azmcp-appconfig-kv-lock` | ❌ |
| 10 | 0.369946 | `azmcp-keyvault-certificate-get` | ❌ |
| 11 | 0.342157 | `azmcp-appconfig-kv-show` | ❌ |
| 12 | 0.321535 | `azmcp-storage-datalake-directory-create` | ❌ |
| 13 | 0.287066 | `azmcp-workbooks-create` | ❌ |
| 14 | 0.275431 | `azmcp-storage-queue-message-send` | ❌ |
| 15 | 0.246820 | `azmcp-storage-blob-container-list` | ❌ |
| 16 | 0.246578 | `azmcp-storage-account-list` | ❌ |
| 17 | 0.236457 | `azmcp-storage-table-list` | ❌ |
| 18 | 0.222749 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 19 | 0.209815 | `azmcp-subscription-list` | ❌ |
| 20 | 0.209458 | `azmcp-storage-blob-list` | ❌ |

---

## Test 76

**Expected Tool:** `azmcp-keyvault-secret-list`  
**Prompt:** List all secrets in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.747343 | `azmcp-keyvault-secret-list` | ✅ **EXPECTED** |
| 2 | 0.617221 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.569911 | `azmcp-keyvault-certificate-list` | ❌ |
| 4 | 0.519133 | `azmcp-keyvault-secret-create` | ❌ |
| 5 | 0.455500 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.433610 | `azmcp-storage-blob-container-list` | ❌ |
| 7 | 0.433185 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.417973 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.415723 | `azmcp-storage-blob-list` | ❌ |
| 10 | 0.414310 | `azmcp-keyvault-certificate-get` | ❌ |
| 11 | 0.414302 | `azmcp-storage-account-list` | ❌ |
| 12 | 0.410496 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.409840 | `azmcp-keyvault-key-create` | ❌ |
| 14 | 0.391082 | `azmcp-subscription-list` | ❌ |
| 15 | 0.391046 | `azmcp-keyvault-certificate-create` | ❌ |
| 16 | 0.355446 | `azmcp-search-index-list` | ❌ |
| 17 | 0.347416 | `azmcp-search-service-list` | ❌ |
| 18 | 0.342445 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 19 | 0.340997 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 20 | 0.331203 | `azmcp-role-assignment-list` | ❌ |

---

## Test 77

**Expected Tool:** `azmcp-keyvault-secret-list`  
**Prompt:** Show me the secrets in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.615400 | `azmcp-keyvault-secret-list` | ✅ **EXPECTED** |
| 2 | 0.520758 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.502403 | `azmcp-keyvault-secret-create` | ❌ |
| 4 | 0.467743 | `azmcp-keyvault-certificate-list` | ❌ |
| 5 | 0.456355 | `azmcp-keyvault-certificate-get` | ❌ |
| 6 | 0.411630 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.410957 | `azmcp-appconfig-kv-show` | ❌ |
| 8 | 0.385852 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.380560 | `azmcp-keyvault-certificate-create` | ❌ |
| 10 | 0.370457 | `azmcp-storage-blob-container-list` | ❌ |
| 11 | 0.354945 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.345256 | `azmcp-subscription-list` | ❌ |
| 13 | 0.344682 | `azmcp-storage-blob-list` | ❌ |
| 14 | 0.344339 | `azmcp-storage-table-list` | ❌ |
| 15 | 0.341754 | `azmcp-storage-blob-container-details` | ❌ |
| 16 | 0.336532 | `azmcp-storage-account-list` | ❌ |
| 17 | 0.301358 | `azmcp-storage-blob-details` | ❌ |
| 18 | 0.299295 | `azmcp-search-index-list` | ❌ |
| 19 | 0.296151 | `azmcp-search-service-list` | ❌ |
| 20 | 0.296014 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

---

## Test 78

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Get the configuration of AKS cluster <cluster-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.660869 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.611633 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.463682 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.456804 | `azmcp-loadtesting-test-get` | ❌ |
| 5 | 0.430975 | `azmcp-postgres-server-config-get` | ❌ |
| 6 | 0.391924 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.390959 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.390819 | `azmcp-appconfig-kv-list` | ❌ |
| 9 | 0.390072 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.367841 | `azmcp-redis-cluster-list` | ❌ |
| 11 | 0.350240 | `azmcp-sql-db-show` | ❌ |
| 12 | 0.349742 | `azmcp-keyvault-certificate-get` | ❌ |
| 13 | 0.349205 | `azmcp-loadtesting-test-create` | ❌ |
| 14 | 0.339882 | `azmcp-sql-db-list` | ❌ |
| 15 | 0.337661 | `azmcp-sql-elastic-pool-list` | ❌ |
| 16 | 0.325605 | `azmcp-storage-blob-details` | ❌ |
| 17 | 0.314217 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 18 | 0.312179 | `azmcp-storage-blob-container-details` | ❌ |
| 19 | 0.310082 | `azmcp-monitor-workspace-list` | ❌ |
| 20 | 0.301918 | `azmcp-monitor-metrics-query` | ❌ |

---

## Test 79

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Show me the details of AKS cluster <cluster-name> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.666849 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.589272 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.508226 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.461466 | `azmcp-sql-db-show` | ❌ |
| 5 | 0.448796 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.396409 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.384610 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.371570 | `azmcp-group-list` | ❌ |
| 9 | 0.365232 | `azmcp-sql-elastic-pool-list` | ❌ |
| 10 | 0.362332 | `azmcp-sql-db-list` | ❌ |
| 11 | 0.356690 | `azmcp-loadtesting-test-get` | ❌ |
| 12 | 0.356313 | `azmcp-monitor-metrics-definitions` | ❌ |
| 13 | 0.352350 | `azmcp-monitor-metrics-query` | ❌ |
| 14 | 0.350559 | `azmcp-workbooks-list` | ❌ |
| 15 | 0.347735 | `azmcp-acr-registry-list` | ❌ |
| 16 | 0.345523 | `azmcp-redis-cache-list` | ❌ |
| 17 | 0.338773 | `azmcp-redis-cluster-database-list` | ❌ |
| 18 | 0.336540 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.332717 | `azmcp-keyvault-certificate-get` | ❌ |
| 20 | 0.321723 | `azmcp-storage-account-create` | ❌ |

---

## Test 80

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Show me the network configuration for AKS cluster <cluster-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.567273 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.563366 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.368584 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.340349 | `azmcp-loadtesting-test-get` | ❌ |
| 5 | 0.340225 | `azmcp-kusto-cluster-list` | ❌ |
| 6 | 0.334923 | `azmcp-appconfig-account-list` | ❌ |
| 7 | 0.334860 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.314513 | `azmcp-appconfig-kv-list` | ❌ |
| 9 | 0.309738 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.296592 | `azmcp-postgres-server-config-get` | ❌ |
| 11 | 0.295183 | `azmcp-loadtesting-test-create` | ❌ |
| 12 | 0.290244 | `azmcp-keyvault-certificate-list` | ❌ |
| 13 | 0.275751 | `azmcp-sql-db-show` | ❌ |
| 14 | 0.273195 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.267611 | `azmcp-sql-elastic-pool-list` | ❌ |
| 16 | 0.265086 | `azmcp-sql-db-list` | ❌ |
| 17 | 0.262012 | `azmcp-role-assignment-list` | ❌ |
| 18 | 0.258610 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.257686 | `azmcp-subscription-list` | ❌ |
| 20 | 0.253017 | `azmcp-monitor-metrics-query` | ❌ |

---

## Test 81

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** What are the details of my AKS cluster <cluster-name> in <resource-group>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661426 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.578911 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.503925 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.418518 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.417836 | `azmcp-sql-db-show` | ❌ |
| 6 | 0.372748 | `azmcp-kusto-cluster-list` | ❌ |
| 7 | 0.364343 | `azmcp-monitor-metrics-query` | ❌ |
| 8 | 0.360459 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.357011 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.353268 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 11 | 0.346769 | `azmcp-monitor-metrics-definitions` | ❌ |
| 12 | 0.345652 | `azmcp-sql-db-list` | ❌ |
| 13 | 0.344520 | `azmcp-extension-az` | ❌ |
| 14 | 0.343973 | `azmcp-storage-blob-container-details` | ❌ |
| 15 | 0.342851 | `azmcp-functionapp-list` | ❌ |
| 16 | 0.342039 | `azmcp-storage-account-create` | ❌ |
| 17 | 0.327724 | `azmcp-extension-azqr` | ❌ |
| 18 | 0.327314 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.323813 | `azmcp-workbooks-show` | ❌ |
| 20 | 0.323300 | `azmcp-servicebus-queue-details` | ❌ |

---

## Test 82

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** List all AKS clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.801334 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.690159 | `azmcp-kusto-cluster-list` | ❌ |
| 3 | 0.599940 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.560861 | `azmcp-aks-cluster-get` | ❌ |
| 5 | 0.549327 | `azmcp-search-service-list` | ❌ |
| 6 | 0.543684 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.515922 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.509202 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.505947 | `azmcp-functionapp-list` | ❌ |
| 10 | 0.502401 | `azmcp-subscription-list` | ❌ |
| 11 | 0.498121 | `azmcp-group-list` | ❌ |
| 12 | 0.495977 | `azmcp-postgres-server-list` | ❌ |
| 13 | 0.486531 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 14 | 0.486142 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.483741 | `azmcp-storage-account-list` | ❌ |
| 16 | 0.483592 | `azmcp-kusto-cluster-get` | ❌ |
| 17 | 0.482355 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.481469 | `azmcp-grafana-list` | ❌ |
| 19 | 0.445271 | `azmcp-storage-table-list` | ❌ |
| 20 | 0.443082 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |

---

## Test 83

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** Show me my Azure Kubernetes Service clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.608332 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.536412 | `azmcp-aks-cluster-get` | ❌ |
| 3 | 0.492765 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.446270 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.409711 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.408385 | `azmcp-kusto-database-list` | ❌ |
| 7 | 0.388143 | `azmcp-search-service-list` | ❌ |
| 8 | 0.387737 | `azmcp-search-index-list` | ❌ |
| 9 | 0.371535 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.363804 | `azmcp-subscription-list` | ❌ |
| 11 | 0.362727 | `azmcp-acr-registry-list` | ❌ |
| 12 | 0.360053 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.359675 | `azmcp-storage-blob-container-list` | ❌ |
| 14 | 0.355864 | `azmcp-keyvault-secret-list` | ❌ |
| 15 | 0.354776 | `azmcp-keyvault-key-list` | ❌ |
| 16 | 0.351900 | `azmcp-extension-az` | ❌ |
| 17 | 0.349439 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 18 | 0.348854 | `azmcp-postgres-server-list` | ❌ |
| 19 | 0.343363 | `azmcp-redis-cluster-database-list` | ❌ |
| 20 | 0.338716 | `azmcp-sql-db-list` | ❌ |

---

## Test 84

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** What AKS clusters do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.624210 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.530023 | `azmcp-aks-cluster-get` | ❌ |
| 3 | 0.449526 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.416564 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.378826 | `azmcp-monitor-workspace-list` | ❌ |
| 6 | 0.345241 | `azmcp-kusto-cluster-get` | ❌ |
| 7 | 0.342303 | `azmcp-extension-az` | ❌ |
| 8 | 0.341581 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.335417 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 10 | 0.334494 | `azmcp-acr-registry-list` | ❌ |
| 11 | 0.328074 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.325876 | `azmcp-extension-azd` | ❌ |
| 13 | 0.322075 | `azmcp-sql-elastic-pool-list` | ❌ |
| 14 | 0.317568 | `azmcp-keyvault-key-list` | ❌ |
| 15 | 0.313457 | `azmcp-storage-blob-container-list` | ❌ |
| 16 | 0.313058 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 17 | 0.312354 | `azmcp-subscription-list` | ❌ |
| 18 | 0.311888 | `azmcp-monitor-table-type-list` | ❌ |
| 19 | 0.305255 | `azmcp-redis-cluster-database-list` | ❌ |
| 20 | 0.302663 | `azmcp-storage-account-list` | ❌ |

---

## Test 85

**Expected Tool:** `azmcp-loadtesting-test-create`  
**Prompt:** Create a basic URL test using the following endpoint URL <test-url> that runs for 30 minutes with 45 virtual users. The test name is <sample-name> with the test id <test-id> and the load testing resource is <load-test-resource> in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.585388 | `azmcp-loadtesting-test-create` | ✅ **EXPECTED** |
| 2 | 0.531213 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.508690 | `azmcp-loadtesting-testrun-create` | ❌ |
| 4 | 0.413857 | `azmcp-loadtesting-testresource-list` | ❌ |
| 5 | 0.402698 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.399602 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.346526 | `azmcp-loadtesting-testrun-update` | ❌ |
| 8 | 0.342853 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.336804 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.323398 | `azmcp-monitor-workspace-log-query` | ❌ |
| 11 | 0.322205 | `azmcp-storage-account-create` | ❌ |
| 12 | 0.310144 | `azmcp-workbooks-create` | ❌ |
| 13 | 0.309940 | `azmcp-keyvault-certificate-create` | ❌ |
| 14 | 0.299491 | `azmcp-keyvault-key-create` | ❌ |
| 15 | 0.280483 | `azmcp-storage-queue-message-send` | ❌ |
| 16 | 0.273813 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 17 | 0.263809 | `azmcp-monitor-metrics-query` | ❌ |
| 18 | 0.255511 | `azmcp-search-service-list` | ❌ |
| 19 | 0.251708 | `azmcp-workbooks-delete` | ❌ |
| 20 | 0.250085 | `azmcp-storage-datalake-directory-create` | ❌ |

---

## Test 86

**Expected Tool:** `azmcp-loadtesting-test-get`  
**Prompt:** Get the load test with id <test-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.643912 | `azmcp-loadtesting-test-get` | ✅ **EXPECTED** |
| 2 | 0.608881 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.574308 | `azmcp-loadtesting-testresource-create` | ❌ |
| 4 | 0.540975 | `azmcp-loadtesting-testrun-get` | ❌ |
| 5 | 0.473733 | `azmcp-loadtesting-testrun-list` | ❌ |
| 6 | 0.473323 | `azmcp-loadtesting-testrun-create` | ❌ |
| 7 | 0.436995 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.407086 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.397437 | `azmcp-group-list` | ❌ |
| 10 | 0.373229 | `azmcp-loadtesting-testrun-update` | ❌ |
| 11 | 0.370024 | `azmcp-workbooks-show` | ❌ |
| 12 | 0.365569 | `azmcp-workbooks-list` | ❌ |
| 13 | 0.360438 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 14 | 0.329344 | `azmcp-sql-db-show` | ❌ |
| 15 | 0.298792 | `azmcp-monitor-workspace-log-query` | ❌ |
| 16 | 0.296766 | `azmcp-workbooks-delete` | ❌ |
| 17 | 0.289872 | `azmcp-sql-db-list` | ❌ |
| 18 | 0.288769 | `azmcp-redis-cluster-list` | ❌ |
| 19 | 0.285310 | `azmcp-monitor-metrics-query` | ❌ |
| 20 | 0.283492 | `azmcp-redis-cache-list` | ❌ |

---

## Test 87

**Expected Tool:** `azmcp-loadtesting-testresource-create`  
**Prompt:** Create a load test resource <load-test-resource-name> in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.717465 | `azmcp-loadtesting-testresource-create` | ✅ **EXPECTED** |
| 2 | 0.596828 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.514437 | `azmcp-loadtesting-test-create` | ❌ |
| 4 | 0.476662 | `azmcp-loadtesting-testrun-create` | ❌ |
| 5 | 0.447548 | `azmcp-loadtesting-test-get` | ❌ |
| 6 | 0.442167 | `azmcp-workbooks-create` | ❌ |
| 7 | 0.416885 | `azmcp-group-list` | ❌ |
| 8 | 0.394670 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.376849 | `azmcp-storage-account-create` | ❌ |
| 10 | 0.375890 | `azmcp-loadtesting-testrun-get` | ❌ |
| 11 | 0.369409 | `azmcp-workbooks-list` | ❌ |
| 12 | 0.350916 | `azmcp-loadtesting-testrun-update` | ❌ |
| 13 | 0.342213 | `azmcp-redis-cluster-list` | ❌ |
| 14 | 0.341251 | `azmcp-grafana-list` | ❌ |
| 15 | 0.335696 | `azmcp-redis-cache-list` | ❌ |
| 16 | 0.328684 | `azmcp-monitor-resource-log-query` | ❌ |
| 17 | 0.298329 | `azmcp-monitor-metrics-query` | ❌ |
| 18 | 0.298311 | `azmcp-monitor-workspace-list` | ❌ |
| 19 | 0.294933 | `azmcp-sql-db-show` | ❌ |
| 20 | 0.294626 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 88

**Expected Tool:** `azmcp-loadtesting-testresource-list`  
**Prompt:** List all load testing resources in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.738027 | `azmcp-loadtesting-testresource-list` | ✅ **EXPECTED** |
| 2 | 0.591733 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.577408 | `azmcp-group-list` | ❌ |
| 4 | 0.565391 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.526662 | `azmcp-workbooks-list` | ❌ |
| 6 | 0.515624 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.512935 | `azmcp-loadtesting-test-get` | ❌ |
| 8 | 0.511607 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.488178 | `azmcp-loadtesting-testrun-list` | ❌ |
| 10 | 0.487330 | `azmcp-grafana-list` | ❌ |
| 11 | 0.470899 | `azmcp-acr-registry-list` | ❌ |
| 12 | 0.467689 | `azmcp-loadtesting-testrun-get` | ❌ |
| 13 | 0.454667 | `azmcp-search-service-list` | ❌ |
| 14 | 0.452190 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.438057 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.437276 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 17 | 0.426880 | `azmcp-sql-db-list` | ❌ |
| 18 | 0.411694 | `azmcp-sql-elastic-pool-list` | ❌ |
| 19 | 0.408978 | `azmcp-storage-account-list` | ❌ |
| 20 | 0.403921 | `azmcp-subscription-list` | ❌ |

---

## Test 89

**Expected Tool:** `azmcp-loadtesting-testrun-create`  
**Prompt:** Create a test run using the id <testrun-id> for test <test-id> in the load testing resource <load-testing-resource> in resource group <resource-group>. Use the name of test run <display-name> and description as <description>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.621803 | `azmcp-loadtesting-testrun-create` | ✅ **EXPECTED** |
| 2 | 0.592789 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.540789 | `azmcp-loadtesting-test-create` | ❌ |
| 4 | 0.530882 | `azmcp-loadtesting-testrun-update` | ❌ |
| 5 | 0.489907 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.472404 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.413872 | `azmcp-loadtesting-testrun-list` | ❌ |
| 8 | 0.411627 | `azmcp-loadtesting-testresource-list` | ❌ |
| 9 | 0.402120 | `azmcp-workbooks-create` | ❌ |
| 10 | 0.354629 | `azmcp-storage-account-create` | ❌ |
| 11 | 0.331022 | `azmcp-keyvault-key-create` | ❌ |
| 12 | 0.325463 | `azmcp-keyvault-secret-create` | ❌ |
| 13 | 0.314636 | `azmcp-storage-datalake-directory-create` | ❌ |
| 14 | 0.309076 | `azmcp-monitor-resource-log-query` | ❌ |
| 15 | 0.272151 | `azmcp-sql-db-show` | ❌ |
| 16 | 0.260678 | `azmcp-storage-queue-message-send` | ❌ |
| 17 | 0.250958 | `azmcp-monitor-workspace-log-query` | ❌ |
| 18 | 0.249643 | `azmcp-workbooks-show` | ❌ |
| 19 | 0.243950 | `azmcp-monitor-metrics-query` | ❌ |
| 20 | 0.242768 | `azmcp-sql-elastic-pool-list` | ❌ |

---

## Test 90

**Expected Tool:** `azmcp-loadtesting-testrun-get`  
**Prompt:** Get the load test run with id <testrun-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.626771 | `azmcp-loadtesting-test-get` | ❌ |
| 2 | 0.603105 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.572847 | `azmcp-loadtesting-testrun-get` | ✅ **EXPECTED** |
| 4 | 0.561975 | `azmcp-loadtesting-testresource-create` | ❌ |
| 5 | 0.535319 | `azmcp-loadtesting-testrun-create` | ❌ |
| 6 | 0.499462 | `azmcp-loadtesting-testrun-list` | ❌ |
| 7 | 0.434245 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.415506 | `azmcp-loadtesting-testrun-update` | ❌ |
| 9 | 0.397863 | `azmcp-group-list` | ❌ |
| 10 | 0.397352 | `azmcp-monitor-resource-log-query` | ❌ |
| 11 | 0.369901 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 12 | 0.356170 | `azmcp-workbooks-list` | ❌ |
| 13 | 0.352827 | `azmcp-workbooks-show` | ❌ |
| 14 | 0.328783 | `azmcp-sql-db-show` | ❌ |
| 15 | 0.293659 | `azmcp-monitor-workspace-log-query` | ❌ |
| 16 | 0.287017 | `azmcp-sql-db-list` | ❌ |
| 17 | 0.285105 | `azmcp-redis-cluster-list` | ❌ |
| 18 | 0.284660 | `azmcp-monitor-metrics-query` | ❌ |
| 19 | 0.281108 | `azmcp-sql-elastic-pool-list` | ❌ |
| 20 | 0.279852 | `azmcp-redis-cache-list` | ❌ |

---

## Test 91

**Expected Tool:** `azmcp-loadtesting-testrun-list`  
**Prompt:** Get all the load test runs for the test with id <test-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.615911 | `azmcp-loadtesting-testresource-list` | ❌ |
| 2 | 0.607863 | `azmcp-loadtesting-test-get` | ❌ |
| 3 | 0.573152 | `azmcp-loadtesting-testrun-get` | ❌ |
| 4 | 0.568973 | `azmcp-loadtesting-testrun-list` | ✅ **EXPECTED** |
| 5 | 0.535001 | `azmcp-loadtesting-testresource-create` | ❌ |
| 6 | 0.492773 | `azmcp-loadtesting-testrun-create` | ❌ |
| 7 | 0.432166 | `azmcp-group-list` | ❌ |
| 8 | 0.417937 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.406523 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.395574 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 11 | 0.392132 | `azmcp-loadtesting-testrun-update` | ❌ |
| 12 | 0.391165 | `azmcp-workbooks-list` | ❌ |
| 13 | 0.340552 | `azmcp-workbooks-show` | ❌ |
| 14 | 0.329477 | `azmcp-sql-db-list` | ❌ |
| 15 | 0.328008 | `azmcp-redis-cluster-list` | ❌ |
| 16 | 0.326463 | `azmcp-sql-elastic-pool-list` | ❌ |
| 17 | 0.323924 | `azmcp-redis-cache-list` | ❌ |
| 18 | 0.316894 | `azmcp-sql-db-show` | ❌ |
| 19 | 0.307218 | `azmcp-monitor-metrics-query` | ❌ |
| 20 | 0.304573 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 92

**Expected Tool:** `azmcp-loadtesting-testrun-update`  
**Prompt:** Update a test run display name as <display-name> for the id <testrun-id> for test <test-id> in the load testing resource <load-testing-resource> in resource group <resource-group>.  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.659812 | `azmcp-loadtesting-testrun-update` | ✅ **EXPECTED** |
| 2 | 0.509199 | `azmcp-loadtesting-testrun-create` | ❌ |
| 3 | 0.455629 | `azmcp-loadtesting-testrun-get` | ❌ |
| 4 | 0.446611 | `azmcp-loadtesting-test-get` | ❌ |
| 5 | 0.422061 | `azmcp-loadtesting-testresource-create` | ❌ |
| 6 | 0.399536 | `azmcp-loadtesting-test-create` | ❌ |
| 7 | 0.384654 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.383635 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.320124 | `azmcp-workbooks-update` | ❌ |
| 10 | 0.300023 | `azmcp-workbooks-create` | ❌ |
| 11 | 0.268172 | `azmcp-workbooks-show` | ❌ |
| 12 | 0.267137 | `azmcp-appconfig-kv-set` | ❌ |
| 13 | 0.259606 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 14 | 0.256023 | `azmcp-appconfig-kv-unlock` | ❌ |
| 15 | 0.251946 | `azmcp-monitor-resource-log-query` | ❌ |
| 16 | 0.237372 | `azmcp-workbooks-delete` | ❌ |
| 17 | 0.232572 | `azmcp-sql-db-show` | ❌ |
| 18 | 0.227194 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 19 | 0.226593 | `azmcp-role-assignment-list` | ❌ |
| 20 | 0.223727 | `azmcp-monitor-metrics-query` | ❌ |

---

## Test 93

**Expected Tool:** `azmcp-grafana-list`  
**Prompt:** List all Azure Managed Grafana in one subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.578892 | `azmcp-grafana-list` | ✅ **EXPECTED** |
| 2 | 0.544665 | `azmcp-search-service-list` | ❌ |
| 3 | 0.513028 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.505732 | `azmcp-kusto-cluster-list` | ❌ |
| 5 | 0.498240 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 6 | 0.497343 | `azmcp-functionapp-list` | ❌ |
| 7 | 0.493645 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.492724 | `azmcp-postgres-server-list` | ❌ |
| 9 | 0.492210 | `azmcp-subscription-list` | ❌ |
| 10 | 0.492151 | `azmcp-aks-cluster-list` | ❌ |
| 11 | 0.489846 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.482789 | `azmcp-redis-cache-list` | ❌ |
| 13 | 0.452635 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 14 | 0.441315 | `azmcp-group-list` | ❌ |
| 15 | 0.440392 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.438275 | `azmcp-storage-account-list` | ❌ |
| 17 | 0.431917 | `azmcp-storage-table-list` | ❌ |
| 18 | 0.422236 | `azmcp-acr-registry-list` | ❌ |
| 19 | 0.417927 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.417527 | `azmcp-workbooks-list` | ❌ |

---

## Test 94

**Expected Tool:** `azmcp-marketplace-product-get`  
**Prompt:** Get details about marketplace product <product_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.528196 | `azmcp-marketplace-product-get` | ✅ **EXPECTED** |
| 2 | 0.353256 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.330903 | `azmcp-servicebus-queue-details` | ❌ |
| 4 | 0.323704 | `azmcp-servicebus-topic-details` | ❌ |
| 5 | 0.322443 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.302335 | `azmcp-aks-cluster-get` | ❌ |
| 7 | 0.295818 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.289354 | `azmcp-workbooks-show` | ❌ |
| 9 | 0.276826 | `azmcp-kusto-cluster-get` | ❌ |
| 10 | 0.274403 | `azmcp-redis-cache-list` | ❌ |
| 11 | 0.269243 | `azmcp-sql-db-show` | ❌ |
| 12 | 0.266271 | `azmcp-foundry-models-list` | ❌ |
| 13 | 0.264527 | `azmcp-search-index-describe` | ❌ |
| 14 | 0.252041 | `azmcp-loadtesting-test-get` | ❌ |
| 15 | 0.248779 | `azmcp-grafana-list` | ❌ |
| 16 | 0.245820 | `azmcp-appconfig-kv-show` | ❌ |
| 17 | 0.236343 | `azmcp-redis-cluster-list` | ❌ |
| 18 | 0.235780 | `azmcp-loadtesting-testrun-list` | ❌ |
| 19 | 0.225855 | `azmcp-storage-blob-details` | ❌ |
| 20 | 0.225581 | `azmcp-keyvault-certificate-get` | ❌ |

---

## Test 95

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure code generation best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.649047 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.612444 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.435166 | `azmcp-extension-az` | ❌ |
| 4 | 0.372867 | `azmcp-extension-azd` | ❌ |
| 5 | 0.345046 | `azmcp-bicepschema-get` | ❌ |
| 6 | 0.331277 | `azmcp-extension-azqr` | ❌ |
| 7 | 0.295713 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.284326 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.270143 | `azmcp-foundry-models-list` | ❌ |
| 10 | 0.270057 | `azmcp-functionapp-list` | ❌ |
| 11 | 0.259833 | `azmcp-subscription-list` | ❌ |
| 12 | 0.258775 | `azmcp-search-service-list` | ❌ |
| 13 | 0.251882 | `azmcp-storage-queue-message-send` | ❌ |
| 14 | 0.251118 | `azmcp-storage-blob-details` | ❌ |
| 15 | 0.244143 | `azmcp-storage-account-create` | ❌ |
| 16 | 0.234254 | `azmcp-workbooks-show` | ❌ |
| 17 | 0.231474 | `azmcp-storage-blob-container-create` | ❌ |
| 18 | 0.231059 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.229140 | `azmcp-monitor-resource-log-query` | ❌ |
| 20 | 0.223545 | `azmcp-search-index-list` | ❌ |

---

## Test 96

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure deployment best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633068 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.543354 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.452068 | `azmcp-extension-az` | ❌ |
| 4 | 0.424017 | `azmcp-foundry-models-deployments-list` | ❌ |
| 5 | 0.366111 | `azmcp-extension-azd` | ❌ |
| 6 | 0.314578 | `azmcp-foundry-models-deploy` | ❌ |
| 7 | 0.292632 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.291654 | `azmcp-functionapp-list` | ❌ |
| 9 | 0.291319 | `azmcp-bicepschema-get` | ❌ |
| 10 | 0.289889 | `azmcp-marketplace-product-get` | ❌ |
| 11 | 0.279585 | `azmcp-subscription-list` | ❌ |
| 12 | 0.277791 | `azmcp-search-service-list` | ❌ |
| 13 | 0.267567 | `azmcp-storage-blob-details` | ❌ |
| 14 | 0.257356 | `azmcp-workbooks-delete` | ❌ |
| 15 | 0.252257 | `azmcp-storage-queue-message-send` | ❌ |
| 16 | 0.249532 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 17 | 0.246865 | `azmcp-sql-db-show` | ❌ |
| 18 | 0.243530 | `azmcp-role-assignment-list` | ❌ |
| 19 | 0.242677 | `azmcp-monitor-metrics-query` | ❌ |
| 20 | 0.241105 | `azmcp-workbooks-show` | ❌ |

---

## Test 97

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.671381 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.575533 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.455995 | `azmcp-extension-az` | ❌ |
| 4 | 0.362465 | `azmcp-extension-azd` | ❌ |
| 5 | 0.319861 | `azmcp-bicepschema-get` | ❌ |
| 6 | 0.313408 | `azmcp-foundry-models-deployments-list` | ❌ |
| 7 | 0.306411 | `azmcp-marketplace-product-get` | ❌ |
| 8 | 0.302595 | `azmcp-extension-azqr` | ❌ |
| 9 | 0.301857 | `azmcp-subscription-list` | ❌ |
| 10 | 0.299916 | `azmcp-keyvault-certificate-get` | ❌ |
| 11 | 0.297266 | `azmcp-loadtesting-test-create` | ❌ |
| 12 | 0.293300 | `azmcp-storage-blob-details` | ❌ |
| 13 | 0.287118 | `azmcp-search-service-list` | ❌ |
| 14 | 0.275399 | `azmcp-workbooks-delete` | ❌ |
| 15 | 0.272158 | `azmcp-monitor-metrics-query` | ❌ |
| 16 | 0.269047 | `azmcp-workbooks-show` | ❌ |
| 17 | 0.265500 | `azmcp-sql-db-show` | ❌ |
| 18 | 0.264648 | `azmcp-monitor-resource-log-query` | ❌ |
| 19 | 0.262767 | `azmcp-storage-queue-message-send` | ❌ |
| 20 | 0.259082 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 98

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure Functions code generation best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.576108 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.553918 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.416803 | `azmcp-extension-az` | ❌ |
| 4 | 0.393445 | `azmcp-functionapp-list` | ❌ |
| 5 | 0.348603 | `azmcp-extension-azd` | ❌ |
| 6 | 0.333044 | `azmcp-bicepschema-get` | ❌ |
| 7 | 0.299738 | `azmcp-extension-azqr` | ❌ |
| 8 | 0.295196 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.290919 | `azmcp-foundry-models-list` | ❌ |
| 10 | 0.288946 | `azmcp-loadtesting-test-create` | ❌ |
| 11 | 0.241692 | `azmcp-storage-blob-details` | ❌ |
| 12 | 0.240062 | `azmcp-storage-queue-message-send` | ❌ |
| 13 | 0.219838 | `azmcp-storage-blob-container-create` | ❌ |
| 14 | 0.219298 | `azmcp-subscription-list` | ❌ |
| 15 | 0.212761 | `azmcp-search-service-list` | ❌ |
| 16 | 0.211461 | `azmcp-monitor-resource-log-query` | ❌ |
| 17 | 0.201024 | `azmcp-storage-account-create` | ❌ |
| 18 | 0.199219 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.197780 | `azmcp-monitor-table-type-list` | ❌ |
| 20 | 0.193696 | `azmcp-storage-blob-batch-set-tier` | ❌ |

---

## Test 99

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure Functions deployment best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.553170 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.487743 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.439182 | `azmcp-foundry-models-deployments-list` | ❌ |
| 4 | 0.431008 | `azmcp-extension-az` | ❌ |
| 5 | 0.424080 | `azmcp-functionapp-list` | ❌ |
| 6 | 0.349387 | `azmcp-extension-azd` | ❌ |
| 7 | 0.343156 | `azmcp-foundry-models-deploy` | ❌ |
| 8 | 0.298877 | `azmcp-bicepschema-get` | ❌ |
| 9 | 0.287346 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.274006 | `azmcp-foundry-models-list` | ❌ |
| 11 | 0.256807 | `azmcp-storage-queue-message-send` | ❌ |
| 12 | 0.254398 | `azmcp-storage-blob-details` | ❌ |
| 13 | 0.244786 | `azmcp-search-service-list` | ❌ |
| 14 | 0.241860 | `azmcp-subscription-list` | ❌ |
| 15 | 0.235261 | `azmcp-workbooks-delete` | ❌ |
| 16 | 0.220954 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 17 | 0.220367 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 18 | 0.218912 | `azmcp-sql-db-show` | ❌ |
| 19 | 0.216915 | `azmcp-monitor-resource-log-query` | ❌ |
| 20 | 0.216136 | `azmcp-workbooks-show` | ❌ |

---

## Test 100

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure Functions best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.586538 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.521087 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.444295 | `azmcp-extension-az` | ❌ |
| 4 | 0.433145 | `azmcp-functionapp-list` | ❌ |
| 5 | 0.348542 | `azmcp-extension-azd` | ❌ |
| 6 | 0.333475 | `azmcp-foundry-models-deployments-list` | ❌ |
| 7 | 0.308337 | `azmcp-bicepschema-get` | ❌ |
| 8 | 0.296226 | `azmcp-foundry-models-list` | ❌ |
| 9 | 0.289805 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.274389 | `azmcp-storage-queue-message-send` | ❌ |
| 11 | 0.269853 | `azmcp-storage-blob-details` | ❌ |
| 12 | 0.269353 | `azmcp-extension-azqr` | ❌ |
| 13 | 0.247460 | `azmcp-subscription-list` | ❌ |
| 14 | 0.246976 | `azmcp-monitor-resource-log-query` | ❌ |
| 15 | 0.246705 | `azmcp-search-service-list` | ❌ |
| 16 | 0.241153 | `azmcp-workbooks-delete` | ❌ |
| 17 | 0.231022 | `azmcp-workbooks-show` | ❌ |
| 18 | 0.229466 | `azmcp-monitor-metrics-query` | ❌ |
| 19 | 0.227274 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 20 | 0.222940 | `azmcp-monitor-table-type-list` | ❌ |

---

## Test 101

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure Static Web Apps best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.577758 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.526365 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.405993 | `azmcp-extension-az` | ❌ |
| 4 | 0.355985 | `azmcp-extension-azd` | ❌ |
| 5 | 0.317236 | `azmcp-functionapp-list` | ❌ |
| 6 | 0.280644 | `azmcp-bicepschema-get` | ❌ |
| 7 | 0.274640 | `azmcp-foundry-models-deployments-list` | ❌ |
| 8 | 0.268529 | `azmcp-extension-azqr` | ❌ |
| 9 | 0.256951 | `azmcp-storage-blob-details` | ❌ |
| 10 | 0.251701 | `azmcp-loadtesting-test-create` | ❌ |
| 11 | 0.240282 | `azmcp-storage-account-create` | ❌ |
| 12 | 0.237212 | `azmcp-aks-cluster-get` | ❌ |
| 13 | 0.223337 | `azmcp-search-service-list` | ❌ |
| 14 | 0.221706 | `azmcp-storage-blob-container-create` | ❌ |
| 15 | 0.219454 | `azmcp-workbooks-delete` | ❌ |
| 16 | 0.216432 | `azmcp-monitor-table-type-list` | ❌ |
| 17 | 0.215376 | `azmcp-monitor-resource-log-query` | ❌ |
| 18 | 0.213802 | `azmcp-subscription-list` | ❌ |
| 19 | 0.213641 | `azmcp-workbooks-show` | ❌ |
| 20 | 0.207237 | `azmcp-storage-queue-message-send` | ❌ |

---

## Test 102

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** What are azure function best practices?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.553494 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.487728 | `azmcp-extension-az` | ❌ |
| 3 | 0.478537 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 4 | 0.394061 | `azmcp-functionapp-list` | ❌ |
| 5 | 0.323967 | `azmcp-extension-azd` | ❌ |
| 6 | 0.280178 | `azmcp-storage-queue-message-send` | ❌ |
| 7 | 0.275139 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.265313 | `azmcp-foundry-models-deploy` | ❌ |
| 9 | 0.262013 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.248119 | `azmcp-monitor-resource-log-query` | ❌ |
| 11 | 0.248003 | `azmcp-workbooks-delete` | ❌ |
| 12 | 0.245105 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 13 | 0.243935 | `azmcp-storage-blob-details` | ❌ |
| 14 | 0.242179 | `azmcp-extension-azqr` | ❌ |
| 15 | 0.216440 | `azmcp-storage-account-create` | ❌ |
| 16 | 0.214156 | `azmcp-monitor-workspace-log-query` | ❌ |
| 17 | 0.208456 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 18 | 0.207847 | `azmcp-monitor-metrics-query` | ❌ |
| 19 | 0.203317 | `azmcp-subscription-list` | ❌ |
| 20 | 0.202520 | `azmcp-storage-table-list` | ❌ |

---

## Test 103

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Create the plan for creating a simple HTTP-triggered function app in javascript that returns a random compliment from a predefined list in a JSON response. And deploy it to azure eventually. But don't create any code until I confirm.  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.406619 | `azmcp-extension-az` | ❌ |
| 2 | 0.360692 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.336439 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 4 | 0.319970 | `azmcp-loadtesting-test-create` | ❌ |
| 5 | 0.289903 | `azmcp-functionapp-list` | ❌ |
| 6 | 0.270072 | `azmcp-extension-azd` | ❌ |
| 7 | 0.264547 | `azmcp-foundry-models-deployments-list` | ❌ |
| 8 | 0.259734 | `azmcp-foundry-models-deploy` | ❌ |
| 9 | 0.254585 | `azmcp-loadtesting-testresource-create` | ❌ |
| 10 | 0.236666 | `azmcp-extension-azqr` | ❌ |
| 11 | 0.218912 | `azmcp-workbooks-create` | ❌ |
| 12 | 0.213599 | `azmcp-storage-blob-details` | ❌ |
| 13 | 0.201280 | `azmcp-storage-blob-container-create` | ❌ |
| 14 | 0.200212 | `azmcp-storage-account-create` | ❌ |
| 15 | 0.190533 | `azmcp-storage-queue-message-send` | ❌ |
| 16 | 0.174633 | `azmcp-subscription-list` | ❌ |
| 17 | 0.174479 | `azmcp-workbooks-delete` | ❌ |
| 18 | 0.171522 | `azmcp-workbooks-show` | ❌ |
| 19 | 0.168175 | `azmcp-storage-table-list` | ❌ |
| 20 | 0.167769 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 104

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Create the plan for creating a to-do list app. And deploy it to azure as a container app. But don't create any code until I confirm.  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.408474 | `azmcp-extension-az` | ❌ |
| 2 | 0.367219 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.333124 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 4 | 0.304256 | `azmcp-extension-azd` | ❌ |
| 5 | 0.300092 | `azmcp-loadtesting-test-create` | ❌ |
| 6 | 0.266937 | `azmcp-foundry-models-deploy` | ❌ |
| 7 | 0.250404 | `azmcp-loadtesting-testresource-create` | ❌ |
| 8 | 0.243202 | `azmcp-functionapp-list` | ❌ |
| 9 | 0.230519 | `azmcp-storage-blob-container-create` | ❌ |
| 10 | 0.230263 | `azmcp-foundry-models-deployments-list` | ❌ |
| 11 | 0.228431 | `azmcp-storage-blob-container-list` | ❌ |
| 12 | 0.226128 | `azmcp-storage-account-create` | ❌ |
| 13 | 0.221563 | `azmcp-cosmos-database-container-list` | ❌ |
| 14 | 0.218293 | `azmcp-storage-blob-list` | ❌ |
| 15 | 0.209213 | `azmcp-workbooks-create` | ❌ |
| 16 | 0.207259 | `azmcp-storage-table-list` | ❌ |
| 17 | 0.195211 | `azmcp-storage-blob-details` | ❌ |
| 18 | 0.185994 | `azmcp-workbooks-delete` | ❌ |
| 19 | 0.183850 | `azmcp-storage-queue-message-send` | ❌ |
| 20 | 0.182206 | `azmcp-storage-blob-container-details` | ❌ |

---

## Test 105

**Expected Tool:** `azmcp-monitor-healthmodels-entity-gethealth`  
**Prompt:** Show me the health status of entity <entity_id> in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.477138 | `azmcp-monitor-healthmodels-entity-gethealth` | ✅ **EXPECTED** |
| 2 | 0.472094 | `azmcp-monitor-workspace-list` | ❌ |
| 3 | 0.468204 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.464012 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.413357 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.404140 | `azmcp-monitor-resource-log-query` | ❌ |
| 7 | 0.380121 | `azmcp-grafana-list` | ❌ |
| 8 | 0.339320 | `azmcp-aks-cluster-get` | ❌ |
| 9 | 0.337603 | `azmcp-loadtesting-testrun-get` | ❌ |
| 10 | 0.316587 | `azmcp-workbooks-show` | ❌ |
| 11 | 0.297666 | `azmcp-aks-cluster-list` | ❌ |
| 12 | 0.292690 | `azmcp-workbooks-delete` | ❌ |
| 13 | 0.291667 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 14 | 0.291114 | `azmcp-search-index-list` | ❌ |
| 15 | 0.285842 | `azmcp-search-service-list` | ❌ |
| 16 | 0.279273 | `azmcp-kusto-query` | ❌ |
| 17 | 0.276713 | `azmcp-loadtesting-test-get` | ❌ |
| 18 | 0.269774 | `azmcp-kusto-cluster-get` | ❌ |
| 19 | 0.268052 | `azmcp-monitor-metrics-definitions` | ❌ |
| 20 | 0.266585 | `azmcp-kusto-table-schema` | ❌ |

---

## Test 106

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** Get metric definitions for <resource_type> <resource_name> from the namespace  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.587636 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 2 | 0.442663 | `azmcp-monitor-metrics-query` | ❌ |
| 3 | 0.332356 | `azmcp-monitor-table-type-list` | ❌ |
| 4 | 0.315310 | `azmcp-servicebus-topic-details` | ❌ |
| 5 | 0.311108 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 6 | 0.305434 | `azmcp-servicebus-queue-details` | ❌ |
| 7 | 0.304735 | `azmcp-grafana-list` | ❌ |
| 8 | 0.303145 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.293189 | `azmcp-search-index-describe` | ❌ |
| 10 | 0.281090 | `azmcp-workbooks-show` | ❌ |
| 11 | 0.277663 | `azmcp-loadtesting-test-get` | ❌ |
| 12 | 0.277566 | `azmcp-kusto-table-schema` | ❌ |
| 13 | 0.272252 | `azmcp-storage-blob-container-details` | ❌ |
| 14 | 0.269984 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 15 | 0.268667 | `azmcp-redis-cache-list` | ❌ |
| 16 | 0.263236 | `azmcp-redis-cluster-list` | ❌ |
| 17 | 0.249144 | `azmcp-aks-cluster-get` | ❌ |
| 18 | 0.248987 | `azmcp-bicepschema-get` | ❌ |
| 19 | 0.234617 | `azmcp-loadtesting-testresource-list` | ❌ |
| 20 | 0.227542 | `azmcp-appconfig-kv-list` | ❌ |

---

## Test 107

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** Show me all available metrics and their definitions for storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.556726 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.542805 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.541200 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.539767 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.524761 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 6 | 0.519808 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.459829 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.459179 | `azmcp-storage-blob-details` | ❌ |
| 9 | 0.431109 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.414488 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.406424 | `azmcp-storage-account-create` | ❌ |
| 12 | 0.397526 | `azmcp-appconfig-kv-list` | ❌ |
| 13 | 0.391340 | `azmcp-monitor-table-type-list` | ❌ |
| 14 | 0.390422 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.383411 | `azmcp-subscription-list` | ❌ |
| 16 | 0.383256 | `azmcp-monitor-metrics-query` | ❌ |
| 17 | 0.378259 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.359476 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.357481 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.343994 | `azmcp-keyvault-secret-list` | ❌ |

---

## Test 108

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** What metric definitions are available for the Application Insights resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.520426 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 2 | 0.419054 | `azmcp-monitor-metrics-query` | ❌ |
| 3 | 0.370848 | `azmcp-monitor-table-type-list` | ❌ |
| 4 | 0.337952 | `azmcp-monitor-resource-log-query` | ❌ |
| 5 | 0.329534 | `azmcp-loadtesting-testresource-list` | ❌ |
| 6 | 0.323861 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.308315 | `azmcp-monitor-workspace-log-query` | ❌ |
| 8 | 0.303296 | `azmcp-search-index-list` | ❌ |
| 9 | 0.302823 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.301966 | `azmcp-workbooks-show` | ❌ |
| 11 | 0.299167 | `azmcp-loadtesting-testrun-get` | ❌ |
| 12 | 0.287635 | `azmcp-storage-blob-details` | ❌ |
| 13 | 0.286226 | `azmcp-loadtesting-testresource-create` | ❌ |
| 14 | 0.286161 | `azmcp-loadtesting-test-get` | ❌ |
| 15 | 0.284437 | `azmcp-grafana-list` | ❌ |
| 16 | 0.283538 | `azmcp-search-index-describe` | ❌ |
| 17 | 0.279929 | `azmcp-foundry-models-deployments-list` | ❌ |
| 18 | 0.278426 | `azmcp-loadtesting-test-create` | ❌ |
| 19 | 0.262578 | `azmcp-workbooks-list` | ❌ |
| 20 | 0.259886 | `azmcp-extension-az` | ❌ |

---

## Test 109

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Analyze the performance trends and response times for Application Insights resource <resource_name> over the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.445163 | `azmcp-monitor-resource-log-query` | ❌ |
| 2 | 0.439700 | `azmcp-loadtesting-testrun-get` | ❌ |
| 3 | 0.434275 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 4 | 0.404591 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.343154 | `azmcp-monitor-metrics-definitions` | ❌ |
| 6 | 0.340662 | `azmcp-loadtesting-testrun-list` | ❌ |
| 7 | 0.339810 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.329502 | `azmcp-loadtesting-testresource-create` | ❌ |
| 9 | 0.328483 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.326814 | `azmcp-workbooks-show` | ❌ |
| 11 | 0.326124 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 12 | 0.314691 | `azmcp-extension-azqr` | ❌ |
| 13 | 0.308096 | `azmcp-functionapp-list` | ❌ |
| 14 | 0.295919 | `azmcp-grafana-list` | ❌ |
| 15 | 0.291438 | `azmcp-search-index-list` | ❌ |
| 16 | 0.289471 | `azmcp-workbooks-delete` | ❌ |
| 17 | 0.286230 | `azmcp-storage-blob-details` | ❌ |
| 18 | 0.281007 | `azmcp-sql-db-show` | ❌ |
| 19 | 0.279714 | `azmcp-search-service-list` | ❌ |
| 20 | 0.274474 | `azmcp-sql-db-list` | ❌ |

---

## Test 110

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Check the availability metrics for my Application Insights resource <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.450181 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.396415 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.389485 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.355916 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.341430 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.339070 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.327025 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.311412 | `azmcp-search-index-list` | ❌ |
| 9 | 0.302435 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.295374 | `azmcp-functionapp-list` | ❌ |
| 11 | 0.292136 | `azmcp-search-service-list` | ❌ |
| 12 | 0.288272 | `azmcp-loadtesting-testresource-create` | ❌ |
| 13 | 0.286047 | `azmcp-monitor-table-type-list` | ❌ |
| 14 | 0.285861 | `azmcp-grafana-list` | ❌ |
| 15 | 0.284986 | `azmcp-workbooks-show` | ❌ |
| 16 | 0.281286 | `azmcp-foundry-models-deployments-list` | ❌ |
| 17 | 0.278615 | `azmcp-storage-blob-details` | ❌ |
| 18 | 0.274721 | `azmcp-aks-cluster-get` | ❌ |
| 19 | 0.264947 | `azmcp-workbooks-list` | ❌ |
| 20 | 0.264882 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 111

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Get the <aggregation_type> <metric_name> metric for <resource_type> <resource_name> over the last <time_period> with intervals  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.422560 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.388536 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.300080 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.279662 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.275431 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.267284 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.259173 | `azmcp-grafana-list` | ❌ |
| 8 | 0.249937 | `azmcp-loadtesting-test-get` | ❌ |
| 9 | 0.249769 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.249463 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 11 | 0.248805 | `azmcp-loadtesting-testresource-list` | ❌ |
| 12 | 0.245754 | `azmcp-workbooks-show` | ❌ |
| 13 | 0.244722 | `azmcp-loadtesting-testrun-get` | ❌ |
| 14 | 0.243711 | `azmcp-storage-blob-details` | ❌ |
| 15 | 0.240416 | `azmcp-workbooks-list` | ❌ |
| 16 | 0.235682 | `azmcp-kusto-table-schema` | ❌ |
| 17 | 0.224363 | `azmcp-loadtesting-testrun-list` | ❌ |
| 18 | 0.219652 | `azmcp-storage-blob-container-create` | ❌ |
| 19 | 0.214399 | `azmcp-postgres-server-param-get` | ❌ |
| 20 | 0.213324 | `azmcp-aks-cluster-get` | ❌ |

---

## Test 112

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Investigate error rates and failed requests for Application Insights resource <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.413826 | `azmcp-monitor-resource-log-query` | ❌ |
| 2 | 0.383333 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 3 | 0.368342 | `azmcp-loadtesting-testrun-get` | ❌ |
| 4 | 0.354940 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.316302 | `azmcp-loadtesting-testresource-list` | ❌ |
| 6 | 0.314773 | `azmcp-monitor-metrics-definitions` | ❌ |
| 7 | 0.295669 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.293304 | `azmcp-loadtesting-testresource-create` | ❌ |
| 9 | 0.284397 | `azmcp-functionapp-list` | ❌ |
| 10 | 0.283523 | `azmcp-extension-azqr` | ❌ |
| 11 | 0.280087 | `azmcp-search-index-list` | ❌ |
| 12 | 0.274550 | `azmcp-loadtesting-test-get` | ❌ |
| 13 | 0.273673 | `azmcp-aks-cluster-get` | ❌ |
| 14 | 0.272721 | `azmcp-search-service-list` | ❌ |
| 15 | 0.272330 | `azmcp-foundry-models-deployments-list` | ❌ |
| 16 | 0.271333 | `azmcp-workbooks-show` | ❌ |
| 17 | 0.259245 | `azmcp-monitor-table-type-list` | ❌ |
| 18 | 0.247777 | `azmcp-redis-cache-list` | ❌ |
| 19 | 0.246410 | `azmcp-redis-cluster-list` | ❌ |
| 20 | 0.244617 | `azmcp-workbooks-delete` | ❌ |

---

## Test 113

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Query the <metric_name> metric for <resource_type> <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.489043 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.413331 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.374190 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.362158 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.293283 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.280946 | `azmcp-search-index-query` | ❌ |
| 7 | 0.272538 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.267066 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.264361 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 10 | 0.262118 | `azmcp-grafana-list` | ❌ |
| 11 | 0.256899 | `azmcp-loadtesting-testrun-list` | ❌ |
| 12 | 0.252156 | `azmcp-servicebus-queue-details` | ❌ |
| 13 | 0.250777 | `azmcp-postgres-server-param-get` | ❌ |
| 14 | 0.246358 | `azmcp-loadtesting-test-get` | ❌ |
| 15 | 0.244316 | `azmcp-storage-blob-details` | ❌ |
| 16 | 0.244288 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 17 | 0.239256 | `azmcp-kusto-query` | ❌ |
| 18 | 0.235840 | `azmcp-loadtesting-testresource-list` | ❌ |
| 19 | 0.232092 | `azmcp-workbooks-show` | ❌ |
| 20 | 0.231796 | `azmcp-storage-blob-container-details` | ❌ |

---

## Test 114

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** What's the request per second rate for my Application Insights resource <resource_name> over the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.348971 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.348732 | `azmcp-monitor-resource-log-query` | ❌ |
| 3 | 0.341334 | `azmcp-monitor-workspace-log-query` | ❌ |
| 4 | 0.331215 | `azmcp-loadtesting-testresource-list` | ❌ |
| 5 | 0.327098 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.319338 | `azmcp-loadtesting-testresource-create` | ❌ |
| 7 | 0.307343 | `azmcp-monitor-metrics-definitions` | ❌ |
| 8 | 0.278491 | `azmcp-workbooks-show` | ❌ |
| 9 | 0.277129 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.272243 | `azmcp-functionapp-list` | ❌ |
| 11 | 0.266918 | `azmcp-search-index-list` | ❌ |
| 12 | 0.262525 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 13 | 0.260709 | `azmcp-foundry-models-deployments-list` | ❌ |
| 14 | 0.258891 | `azmcp-extension-azqr` | ❌ |
| 15 | 0.257533 | `azmcp-marketplace-product-get` | ❌ |
| 16 | 0.254630 | `azmcp-search-service-list` | ❌ |
| 17 | 0.246652 | `azmcp-storage-queue-message-send` | ❌ |
| 18 | 0.234473 | `azmcp-search-index-query` | ❌ |
| 19 | 0.229355 | `azmcp-redis-cache-list` | ❌ |
| 20 | 0.223896 | `azmcp-workbooks-list` | ❌ |

---

## Test 115

**Expected Tool:** `azmcp-monitor-resource-log-query`  
**Prompt:** Show me the logs for the past hour for the resource <resource_name> in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584906 | `azmcp-monitor-workspace-log-query` | ❌ |
| 2 | 0.577600 | `azmcp-monitor-resource-log-query` | ✅ **EXPECTED** |
| 3 | 0.443468 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.442971 | `azmcp-monitor-table-list` | ❌ |
| 5 | 0.412622 | `azmcp-monitor-metrics-query` | ❌ |
| 6 | 0.392377 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.390022 | `azmcp-grafana-list` | ❌ |
| 8 | 0.352661 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.342350 | `azmcp-monitor-metrics-definitions` | ❌ |
| 10 | 0.333531 | `azmcp-workbooks-list` | ❌ |
| 11 | 0.331818 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 12 | 0.326390 | `azmcp-workbooks-delete` | ❌ |
| 13 | 0.320807 | `azmcp-loadtesting-testrun-get` | ❌ |
| 14 | 0.319835 | `azmcp-search-index-list` | ❌ |
| 15 | 0.307859 | `azmcp-aks-cluster-get` | ❌ |
| 16 | 0.302745 | `azmcp-loadtesting-testresource-list` | ❌ |
| 17 | 0.299952 | `azmcp-loadtesting-test-get` | ❌ |
| 18 | 0.296809 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.296311 | `azmcp-loadtesting-testrun-list` | ❌ |
| 20 | 0.291854 | `azmcp-kusto-query` | ❌ |

---

## Test 116

**Expected Tool:** `azmcp-monitor-table-list`  
**Prompt:** List all tables in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.851075 | `azmcp-monitor-table-list` | ✅ **EXPECTED** |
| 2 | 0.725738 | `azmcp-monitor-table-type-list` | ❌ |
| 3 | 0.620445 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.586691 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.511123 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.502075 | `azmcp-grafana-list` | ❌ |
| 7 | 0.488557 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.436216 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.420394 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.419859 | `azmcp-kusto-database-list` | ❌ |
| 11 | 0.409449 | `azmcp-monitor-resource-log-query` | ❌ |
| 12 | 0.405953 | `azmcp-search-index-list` | ❌ |
| 13 | 0.400092 | `azmcp-workbooks-list` | ❌ |
| 14 | 0.397408 | `azmcp-kusto-table-schema` | ❌ |
| 15 | 0.378748 | `azmcp-sql-db-list` | ❌ |
| 16 | 0.374930 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.366099 | `azmcp-kusto-sample` | ❌ |
| 18 | 0.365781 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.365442 | `azmcp-kusto-cluster-list` | ❌ |
| 20 | 0.357966 | `azmcp-keyvault-key-list` | ❌ |

---

## Test 117

**Expected Tool:** `azmcp-monitor-table-list`  
**Prompt:** Show me the tables in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.798460 | `azmcp-monitor-table-list` | ✅ **EXPECTED** |
| 2 | 0.701122 | `azmcp-monitor-table-type-list` | ❌ |
| 3 | 0.599917 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.532887 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.487237 | `azmcp-grafana-list` | ❌ |
| 6 | 0.466630 | `azmcp-kusto-table-list` | ❌ |
| 7 | 0.441635 | `azmcp-monitor-workspace-log-query` | ❌ |
| 8 | 0.427408 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.413450 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.411590 | `azmcp-kusto-table-schema` | ❌ |
| 11 | 0.376474 | `azmcp-kusto-sample` | ❌ |
| 12 | 0.376338 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.373298 | `azmcp-workbooks-list` | ❌ |
| 14 | 0.370624 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.370200 | `azmcp-search-index-list` | ❌ |
| 16 | 0.347853 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.339950 | `azmcp-sql-db-list` | ❌ |
| 18 | 0.332244 | `azmcp-kusto-cluster-list` | ❌ |
| 19 | 0.331919 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.320651 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 118

**Expected Tool:** `azmcp-monitor-table-type-list`  
**Prompt:** List all available table types in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.881524 | `azmcp-monitor-table-type-list` | ✅ **EXPECTED** |
| 2 | 0.765702 | `azmcp-monitor-table-list` | ❌ |
| 3 | 0.569921 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.525469 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.477280 | `azmcp-grafana-list` | ❌ |
| 6 | 0.447435 | `azmcp-kusto-table-list` | ❌ |
| 7 | 0.418517 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.416351 | `azmcp-kusto-table-schema` | ❌ |
| 9 | 0.394213 | `azmcp-monitor-workspace-log-query` | ❌ |
| 10 | 0.380581 | `azmcp-kusto-sample` | ❌ |
| 11 | 0.371871 | `azmcp-monitor-resource-log-query` | ❌ |
| 12 | 0.369889 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.367798 | `azmcp-workbooks-list` | ❌ |
| 14 | 0.361820 | `azmcp-kusto-database-list` | ❌ |
| 15 | 0.356649 | `azmcp-search-index-list` | ❌ |
| 16 | 0.354706 | `azmcp-kusto-cluster-list` | ❌ |
| 17 | 0.346304 | `azmcp-foundry-models-list` | ❌ |
| 18 | 0.341608 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.339421 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 20 | 0.326479 | `azmcp-monitor-metrics-definitions` | ❌ |

---

## Test 119

**Expected Tool:** `azmcp-monitor-table-type-list`  
**Prompt:** Show me the available table types in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.843138 | `azmcp-monitor-table-type-list` | ✅ **EXPECTED** |
| 2 | 0.736837 | `azmcp-monitor-table-list` | ❌ |
| 3 | 0.576731 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.502460 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.475734 | `azmcp-grafana-list` | ❌ |
| 6 | 0.427934 | `azmcp-kusto-table-schema` | ❌ |
| 7 | 0.421484 | `azmcp-kusto-table-list` | ❌ |
| 8 | 0.416739 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.391308 | `azmcp-kusto-sample` | ❌ |
| 10 | 0.384124 | `azmcp-monitor-resource-log-query` | ❌ |
| 11 | 0.372991 | `azmcp-postgres-table-list` | ❌ |
| 12 | 0.352574 | `azmcp-workbooks-list` | ❌ |
| 13 | 0.348357 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.344675 | `azmcp-search-index-list` | ❌ |
| 15 | 0.340942 | `azmcp-postgres-table-schema-get` | ❌ |
| 16 | 0.340101 | `azmcp-foundry-models-list` | ❌ |
| 17 | 0.339760 | `azmcp-kusto-cluster-list` | ❌ |
| 18 | 0.338446 | `azmcp-kusto-database-list` | ❌ |
| 19 | 0.329195 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.316152 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 120

**Expected Tool:** `azmcp-monitor-workspace-list`  
**Prompt:** List all Log Analytics workspaces in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.813902 | `azmcp-monitor-workspace-list` | ✅ **EXPECTED** |
| 2 | 0.680201 | `azmcp-grafana-list` | ❌ |
| 3 | 0.660135 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.588276 | `azmcp-search-service-list` | ❌ |
| 5 | 0.583213 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.530333 | `azmcp-kusto-cluster-list` | ❌ |
| 7 | 0.517493 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.513671 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.502425 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.500768 | `azmcp-workbooks-list` | ❌ |
| 11 | 0.494595 | `azmcp-group-list` | ❌ |
| 12 | 0.493730 | `azmcp-subscription-list` | ❌ |
| 13 | 0.487800 | `azmcp-functionapp-list` | ❌ |
| 14 | 0.487565 | `azmcp-storage-table-list` | ❌ |
| 15 | 0.471758 | `azmcp-redis-cluster-list` | ❌ |
| 16 | 0.470266 | `azmcp-postgres-server-list` | ❌ |
| 17 | 0.467655 | `azmcp-appconfig-account-list` | ❌ |
| 18 | 0.466748 | `azmcp-acr-registry-list` | ❌ |
| 19 | 0.448201 | `azmcp-kusto-database-list` | ❌ |
| 20 | 0.444214 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 121

**Expected Tool:** `azmcp-monitor-workspace-list`  
**Prompt:** Show me my Log Analytics workspaces  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.656194 | `azmcp-monitor-workspace-list` | ✅ **EXPECTED** |
| 2 | 0.585436 | `azmcp-monitor-table-list` | ❌ |
| 3 | 0.531083 | `azmcp-monitor-table-type-list` | ❌ |
| 4 | 0.518254 | `azmcp-grafana-list` | ❌ |
| 5 | 0.462960 | `azmcp-monitor-workspace-log-query` | ❌ |
| 6 | 0.398741 | `azmcp-search-service-list` | ❌ |
| 7 | 0.386422 | `azmcp-workbooks-list` | ❌ |
| 8 | 0.384081 | `azmcp-search-index-list` | ❌ |
| 9 | 0.383245 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.381606 | `azmcp-monitor-resource-log-query` | ❌ |
| 11 | 0.379597 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.376990 | `azmcp-storage-blob-container-list` | ❌ |
| 13 | 0.373786 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.357901 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.354276 | `azmcp-cosmos-database-list` | ❌ |
| 16 | 0.352809 | `azmcp-acr-registry-list` | ❌ |
| 17 | 0.350239 | `azmcp-loadtesting-testresource-list` | ❌ |
| 18 | 0.344457 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.337974 | `azmcp-foundry-models-deployments-list` | ❌ |
| 20 | 0.331405 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 122

**Expected Tool:** `azmcp-monitor-workspace-list`  
**Prompt:** Show me the Log Analytics workspaces in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.732962 | `azmcp-monitor-workspace-list` | ✅ **EXPECTED** |
| 2 | 0.601481 | `azmcp-grafana-list` | ❌ |
| 3 | 0.580261 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.521316 | `azmcp-monitor-table-type-list` | ❌ |
| 5 | 0.500498 | `azmcp-search-service-list` | ❌ |
| 6 | 0.449975 | `azmcp-monitor-workspace-log-query` | ❌ |
| 7 | 0.439215 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.435475 | `azmcp-workbooks-list` | ❌ |
| 9 | 0.428945 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.427025 | `azmcp-aks-cluster-list` | ❌ |
| 11 | 0.422707 | `azmcp-subscription-list` | ❌ |
| 12 | 0.422379 | `azmcp-loadtesting-testresource-list` | ❌ |
| 13 | 0.418520 | `azmcp-storage-account-list` | ❌ |
| 14 | 0.413155 | `azmcp-storage-table-list` | ❌ |
| 15 | 0.411648 | `azmcp-acr-registry-list` | ❌ |
| 16 | 0.404177 | `azmcp-group-list` | ❌ |
| 17 | 0.402600 | `azmcp-redis-cluster-list` | ❌ |
| 18 | 0.395576 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.390483 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.379433 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 123

**Expected Tool:** `azmcp-monitor-workspace-log-query`  
**Prompt:** Show me the logs for the past hour in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.581663 | `azmcp-monitor-workspace-log-query` | ✅ **EXPECTED** |
| 2 | 0.492885 | `azmcp-monitor-resource-log-query` | ❌ |
| 3 | 0.485984 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.483323 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.427241 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.365704 | `azmcp-grafana-list` | ❌ |
| 7 | 0.325566 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 8 | 0.324938 | `azmcp-monitor-metrics-query` | ❌ |
| 9 | 0.322001 | `azmcp-search-index-list` | ❌ |
| 10 | 0.318833 | `azmcp-workbooks-delete` | ❌ |
| 11 | 0.309810 | `azmcp-loadtesting-testrun-get` | ❌ |
| 12 | 0.300988 | `azmcp-search-service-list` | ❌ |
| 13 | 0.292300 | `azmcp-extension-az` | ❌ |
| 14 | 0.291669 | `azmcp-kusto-query` | ❌ |
| 15 | 0.288369 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.287253 | `azmcp-aks-cluster-get` | ❌ |
| 17 | 0.284162 | `azmcp-loadtesting-testrun-list` | ❌ |
| 18 | 0.276315 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.270069 | `azmcp-kusto-sample` | ❌ |
| 20 | 0.267852 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 124

**Expected Tool:** `azmcp-datadog-monitoredresources-list`  
**Prompt:** List all monitored resources in the Datadog resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.668989 | `azmcp-datadog-monitoredresources-list` | ✅ **EXPECTED** |
| 2 | 0.434813 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.408658 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.401731 | `azmcp-grafana-list` | ❌ |
| 5 | 0.386195 | `azmcp-monitor-metrics-definitions` | ❌ |
| 6 | 0.369805 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.367875 | `azmcp-monitor-metrics-query` | ❌ |
| 8 | 0.364360 | `azmcp-workbooks-list` | ❌ |
| 9 | 0.355415 | `azmcp-loadtesting-testresource-list` | ❌ |
| 10 | 0.345409 | `azmcp-postgres-database-list` | ❌ |
| 11 | 0.345298 | `azmcp-group-list` | ❌ |
| 12 | 0.330769 | `azmcp-postgres-table-list` | ❌ |
| 13 | 0.327205 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.318192 | `azmcp-sql-db-list` | ❌ |
| 15 | 0.310492 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 16 | 0.304321 | `azmcp-monitor-table-type-list` | ❌ |
| 17 | 0.304097 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.296544 | `azmcp-cosmos-database-container-list` | ❌ |
| 19 | 0.294543 | `azmcp-kusto-database-list` | ❌ |
| 20 | 0.283467 | `azmcp-loadtesting-testrun-list` | ❌ |

---

## Test 125

**Expected Tool:** `azmcp-datadog-monitoredresources-list`  
**Prompt:** Show me the monitored resources in the Datadog resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.624135 | `azmcp-datadog-monitoredresources-list` | ✅ **EXPECTED** |
| 2 | 0.393227 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.374071 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.371017 | `azmcp-grafana-list` | ❌ |
| 5 | 0.364986 | `azmcp-monitor-metrics-query` | ❌ |
| 6 | 0.363038 | `azmcp-monitor-metrics-definitions` | ❌ |
| 7 | 0.343214 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.342468 | `azmcp-redis-cluster-database-list` | ❌ |
| 9 | 0.319895 | `azmcp-workbooks-list` | ❌ |
| 10 | 0.300073 | `azmcp-monitor-resource-log-query` | ❌ |
| 11 | 0.285253 | `azmcp-group-list` | ❌ |
| 12 | 0.276990 | `azmcp-sql-db-list` | ❌ |
| 13 | 0.274464 | `azmcp-loadtesting-testrun-get` | ❌ |
| 14 | 0.272698 | `azmcp-postgres-database-list` | ❌ |
| 15 | 0.271423 | `azmcp-postgres-server-list` | ❌ |
| 16 | 0.270840 | `azmcp-loadtesting-testrun-list` | ❌ |
| 17 | 0.269734 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 18 | 0.264788 | `azmcp-cosmos-database-list` | ❌ |
| 19 | 0.260738 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.256947 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 126

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Check my Azure subscription for any compliance issues or recommendations  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.476826 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 2 | 0.459005 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.442625 | `azmcp-extension-az` | ❌ |
| 4 | 0.427495 | `azmcp-search-service-list` | ❌ |
| 5 | 0.426262 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 6 | 0.423237 | `azmcp-subscription-list` | ❌ |
| 7 | 0.388980 | `azmcp-monitor-workspace-list` | ❌ |
| 8 | 0.366572 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.359574 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.355872 | `azmcp-functionapp-list` | ❌ |
| 11 | 0.354341 | `azmcp-redis-cache-list` | ❌ |
| 12 | 0.351428 | `azmcp-redis-cluster-list` | ❌ |
| 13 | 0.346128 | `azmcp-aks-cluster-list` | ❌ |
| 14 | 0.340681 | `azmcp-acr-registry-list` | ❌ |
| 15 | 0.338176 | `azmcp-grafana-list` | ❌ |
| 16 | 0.332107 | `azmcp-storage-account-list` | ❌ |
| 17 | 0.321228 | `azmcp-storage-table-list` | ❌ |
| 18 | 0.319314 | `azmcp-monitor-resource-log-query` | ❌ |
| 19 | 0.297488 | `azmcp-role-assignment-list` | ❌ |
| 20 | 0.295734 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 127

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Provide compliance recommendations for my current Azure subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.527082 | `azmcp-bestpractices-get` | ❌ |
| 2 | 0.487897 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.474017 | `azmcp-extension-az` | ❌ |
| 4 | 0.462743 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 5 | 0.382470 | `azmcp-search-service-list` | ❌ |
| 6 | 0.375770 | `azmcp-subscription-list` | ❌ |
| 7 | 0.338370 | `azmcp-marketplace-product-get` | ❌ |
| 8 | 0.333625 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.330816 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 10 | 0.316612 | `azmcp-redis-cluster-list` | ❌ |
| 11 | 0.310888 | `azmcp-redis-cache-list` | ❌ |
| 12 | 0.310637 | `azmcp-functionapp-list` | ❌ |
| 13 | 0.308147 | `azmcp-acr-registry-list` | ❌ |
| 14 | 0.302488 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.302114 | `azmcp-appconfig-account-list` | ❌ |
| 16 | 0.286650 | `azmcp-role-assignment-list` | ❌ |
| 17 | 0.282297 | `azmcp-sql-db-show` | ❌ |
| 18 | 0.278733 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.277213 | `azmcp-storage-account-list` | ❌ |
| 20 | 0.273089 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |

---

## Test 128

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Scan my Azure subscription for compliance recommendations  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.516925 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 2 | 0.514791 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.490423 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 4 | 0.472526 | `azmcp-extension-az` | ❌ |
| 5 | 0.450091 | `azmcp-search-service-list` | ❌ |
| 6 | 0.423512 | `azmcp-subscription-list` | ❌ |
| 7 | 0.398621 | `azmcp-monitor-workspace-list` | ❌ |
| 8 | 0.391331 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.371590 | `azmcp-redis-cluster-list` | ❌ |
| 10 | 0.369448 | `azmcp-redis-cache-list` | ❌ |
| 11 | 0.365011 | `azmcp-functionapp-list` | ❌ |
| 12 | 0.359041 | `azmcp-acr-registry-list` | ❌ |
| 13 | 0.357309 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.355604 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.348565 | `azmcp-grafana-list` | ❌ |
| 16 | 0.339864 | `azmcp-role-assignment-list` | ❌ |
| 17 | 0.339515 | `azmcp-storage-account-list` | ❌ |
| 18 | 0.322264 | `azmcp-search-index-list` | ❌ |
| 19 | 0.317812 | `azmcp-monitor-resource-log-query` | ❌ |
| 20 | 0.316995 | `azmcp-storage-table-list` | ❌ |

---

## Test 129

**Expected Tool:** `azmcp-role-assignment-list`  
**Prompt:** List all available role assignments in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.645259 | `azmcp-role-assignment-list` | ✅ **EXPECTED** |
| 2 | 0.487393 | `azmcp-search-service-list` | ❌ |
| 3 | 0.483988 | `azmcp-group-list` | ❌ |
| 4 | 0.483125 | `azmcp-subscription-list` | ❌ |
| 5 | 0.478700 | `azmcp-grafana-list` | ❌ |
| 6 | 0.474796 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.471364 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.460029 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.452819 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.448138 | `azmcp-storage-account-list` | ❌ |
| 11 | 0.446242 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 12 | 0.441516 | `azmcp-functionapp-list` | ❌ |
| 13 | 0.430677 | `azmcp-kusto-cluster-list` | ❌ |
| 14 | 0.427950 | `azmcp-workbooks-list` | ❌ |
| 15 | 0.425029 | `azmcp-postgres-server-list` | ❌ |
| 16 | 0.403172 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.397565 | `azmcp-appconfig-account-list` | ❌ |
| 18 | 0.397229 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.374732 | `azmcp-loadtesting-testresource-list` | ❌ |
| 20 | 0.365611 | `azmcp-acr-registry-list` | ❌ |

---

## Test 130

**Expected Tool:** `azmcp-role-assignment-list`  
**Prompt:** Show me the available role assignments in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609705 | `azmcp-role-assignment-list` | ✅ **EXPECTED** |
| 2 | 0.456956 | `azmcp-grafana-list` | ❌ |
| 3 | 0.436747 | `azmcp-subscription-list` | ❌ |
| 4 | 0.435642 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.435287 | `azmcp-search-service-list` | ❌ |
| 6 | 0.435155 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.428663 | `azmcp-group-list` | ❌ |
| 8 | 0.428370 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.420804 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.410242 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 11 | 0.395445 | `azmcp-workbooks-list` | ❌ |
| 12 | 0.390936 | `azmcp-storage-account-list` | ❌ |
| 13 | 0.388350 | `azmcp-functionapp-list` | ❌ |
| 14 | 0.388322 | `azmcp-postgres-server-list` | ❌ |
| 15 | 0.386801 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.383475 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.373204 | `azmcp-appconfig-account-list` | ❌ |
| 18 | 0.368511 | `azmcp-loadtesting-testresource-list` | ❌ |
| 19 | 0.353628 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.351829 | `azmcp-marketplace-product-get` | ❌ |

---

## Test 131

**Expected Tool:** `azmcp-redis-cache-accesspolicy-list`  
**Prompt:** List all access policies in the Redis Cache <cache_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.757041 | `azmcp-redis-cache-accesspolicy-list` | ✅ **EXPECTED** |
| 2 | 0.565047 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.445073 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.377563 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.312428 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.307404 | `azmcp-keyvault-secret-list` | ❌ |
| 7 | 0.303736 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.303531 | `azmcp-appconfig-kv-list` | ❌ |
| 9 | 0.300150 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.300024 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.298380 | `azmcp-keyvault-certificate-list` | ❌ |
| 12 | 0.296709 | `azmcp-keyvault-key-list` | ❌ |
| 13 | 0.292082 | `azmcp-bestpractices-get` | ❌ |
| 14 | 0.284898 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.284304 | `azmcp-grafana-list` | ❌ |
| 16 | 0.284115 | `azmcp-loadtesting-testrun-list` | ❌ |
| 17 | 0.283583 | `azmcp-storage-blob-container-list` | ❌ |
| 18 | 0.281283 | `azmcp-storage-blob-container-details` | ❌ |
| 19 | 0.277696 | `azmcp-subscription-list` | ❌ |
| 20 | 0.274897 | `azmcp-role-assignment-list` | ❌ |

---

## Test 132

**Expected Tool:** `azmcp-redis-cache-accesspolicy-list`  
**Prompt:** Show me the access policies in the Redis Cache <cache_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713842 | `azmcp-redis-cache-accesspolicy-list` | ✅ **EXPECTED** |
| 2 | 0.523153 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.412377 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.338859 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.300045 | `azmcp-bestpractices-get` | ❌ |
| 6 | 0.288868 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.286321 | `azmcp-appconfig-kv-list` | ❌ |
| 8 | 0.280245 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.258045 | `azmcp-appconfig-account-list` | ❌ |
| 10 | 0.257151 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.253484 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.253228 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 13 | 0.253169 | `azmcp-loadtesting-testrun-list` | ❌ |
| 14 | 0.249917 | `azmcp-extension-az` | ❌ |
| 15 | 0.247853 | `azmcp-keyvault-secret-list` | ❌ |
| 16 | 0.246871 | `azmcp-grafana-list` | ❌ |
| 17 | 0.241891 | `azmcp-role-assignment-list` | ❌ |
| 18 | 0.230775 | `azmcp-storage-blob-container-list` | ❌ |
| 19 | 0.230761 | `azmcp-subscription-list` | ❌ |
| 20 | 0.230692 | `azmcp-storage-account-list` | ❌ |

---

## Test 133

**Expected Tool:** `azmcp-redis-cache-list`  
**Prompt:** List all Redis Caches in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.764063 | `azmcp-redis-cache-list` | ✅ **EXPECTED** |
| 2 | 0.653924 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.501837 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 4 | 0.495048 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.472307 | `azmcp-grafana-list` | ❌ |
| 6 | 0.466173 | `azmcp-kusto-cluster-list` | ❌ |
| 7 | 0.464785 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.433313 | `azmcp-search-service-list` | ❌ |
| 9 | 0.431968 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.431715 | `azmcp-appconfig-account-list` | ❌ |
| 11 | 0.423145 | `azmcp-subscription-list` | ❌ |
| 12 | 0.396295 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.393716 | `azmcp-storage-account-list` | ❌ |
| 14 | 0.381343 | `azmcp-kusto-database-list` | ❌ |
| 15 | 0.380661 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.373395 | `azmcp-group-list` | ❌ |
| 17 | 0.373274 | `azmcp-storage-table-list` | ❌ |
| 18 | 0.368849 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.361464 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.354948 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 134

**Expected Tool:** `azmcp-redis-cache-list`  
**Prompt:** Show me my Redis Caches  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.537885 | `azmcp-redis-cache-list` | ✅ **EXPECTED** |
| 2 | 0.450385 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 3 | 0.441104 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.401235 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.283598 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.265858 | `azmcp-appconfig-kv-list` | ❌ |
| 7 | 0.262106 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.257556 | `azmcp-appconfig-account-list` | ❌ |
| 9 | 0.252070 | `azmcp-grafana-list` | ❌ |
| 10 | 0.246445 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.236096 | `azmcp-postgres-table-list` | ❌ |
| 12 | 0.233781 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.231390 | `azmcp-loadtesting-testrun-list` | ❌ |
| 14 | 0.225621 | `azmcp-postgres-server-config-get` | ❌ |
| 15 | 0.225079 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.224946 | `azmcp-storage-blob-container-list` | ❌ |
| 17 | 0.218007 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.213788 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.211175 | `azmcp-extension-az` | ❌ |
| 20 | 0.210121 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 135

**Expected Tool:** `azmcp-redis-cache-list`  
**Prompt:** Show me the Redis Caches in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.692210 | `azmcp-redis-cache-list` | ✅ **EXPECTED** |
| 2 | 0.595721 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.461557 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 4 | 0.434924 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.427325 | `azmcp-grafana-list` | ❌ |
| 6 | 0.399303 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.383383 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.382324 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.368549 | `azmcp-search-service-list` | ❌ |
| 10 | 0.361735 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.353487 | `azmcp-subscription-list` | ❌ |
| 12 | 0.340764 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.327206 | `azmcp-loadtesting-testresource-list` | ❌ |
| 14 | 0.320519 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.315691 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.310840 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.306356 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.304064 | `azmcp-group-list` | ❌ |
| 19 | 0.303249 | `azmcp-storage-table-list` | ❌ |
| 20 | 0.298785 | `azmcp-kusto-database-list` | ❌ |

---

## Test 136

**Expected Tool:** `azmcp-redis-cluster-database-list`  
**Prompt:** List all databases in the Redis Cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.752919 | `azmcp-redis-cluster-database-list` | ✅ **EXPECTED** |
| 2 | 0.603780 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.594994 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.548268 | `azmcp-postgres-database-list` | ❌ |
| 5 | 0.538403 | `azmcp-cosmos-database-list` | ❌ |
| 6 | 0.471359 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.458238 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.456133 | `azmcp-kusto-table-list` | ❌ |
| 9 | 0.449548 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.419621 | `azmcp-postgres-table-list` | ❌ |
| 11 | 0.385544 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.379937 | `azmcp-postgres-server-list` | ❌ |
| 13 | 0.376130 | `azmcp-aks-cluster-list` | ❌ |
| 14 | 0.366236 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.355786 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 16 | 0.352225 | `azmcp-storage-table-list` | ❌ |
| 17 | 0.328140 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.325668 | `azmcp-monitor-table-list` | ❌ |
| 19 | 0.324867 | `azmcp-grafana-list` | ❌ |
| 20 | 0.306241 | `azmcp-keyvault-key-list` | ❌ |

---

## Test 137

**Expected Tool:** `azmcp-redis-cluster-database-list`  
**Prompt:** Show me the databases in the Redis Cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.721506 | `azmcp-redis-cluster-database-list` | ✅ **EXPECTED** |
| 2 | 0.562860 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.537788 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.481618 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.480274 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.434940 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.414701 | `azmcp-kusto-table-list` | ❌ |
| 8 | 0.408379 | `azmcp-sql-db-list` | ❌ |
| 9 | 0.397269 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.351025 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.349880 | `azmcp-postgres-table-list` | ❌ |
| 12 | 0.343244 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 13 | 0.337272 | `azmcp-postgres-server-list` | ❌ |
| 14 | 0.325031 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.318982 | `azmcp-cosmos-account-list` | ❌ |
| 16 | 0.306736 | `azmcp-storage-table-list` | ❌ |
| 17 | 0.302228 | `azmcp-kusto-sample` | ❌ |
| 18 | 0.294393 | `azmcp-kusto-table-schema` | ❌ |
| 19 | 0.292180 | `azmcp-grafana-list` | ❌ |
| 20 | 0.288275 | `azmcp-sql-db-show` | ❌ |

---

## Test 138

**Expected Tool:** `azmcp-redis-cluster-list`  
**Prompt:** List all Redis Clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.812960 | `azmcp-redis-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.679020 | `azmcp-kusto-cluster-list` | ❌ |
| 3 | 0.672104 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.588847 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.569230 | `azmcp-aks-cluster-list` | ❌ |
| 6 | 0.554298 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.527406 | `azmcp-kusto-database-list` | ❌ |
| 8 | 0.503279 | `azmcp-grafana-list` | ❌ |
| 9 | 0.467957 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.463770 | `azmcp-search-service-list` | ❌ |
| 11 | 0.457600 | `azmcp-kusto-cluster-get` | ❌ |
| 12 | 0.455613 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.445496 | `azmcp-group-list` | ❌ |
| 14 | 0.445406 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.442786 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 16 | 0.439308 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 17 | 0.436494 | `azmcp-subscription-list` | ❌ |
| 18 | 0.422129 | `azmcp-storage-account-list` | ❌ |
| 19 | 0.419137 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.419093 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 139

**Expected Tool:** `azmcp-redis-cluster-list`  
**Prompt:** Show me my Redis Clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591593 | `azmcp-redis-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.514375 | `azmcp-redis-cluster-database-list` | ❌ |
| 3 | 0.467519 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.403254 | `azmcp-kusto-cluster-list` | ❌ |
| 5 | 0.385013 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 6 | 0.367843 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.329389 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.322157 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.305874 | `azmcp-kusto-cluster-get` | ❌ |
| 10 | 0.301294 | `azmcp-aks-cluster-get` | ❌ |
| 11 | 0.295045 | `azmcp-grafana-list` | ❌ |
| 12 | 0.291684 | `azmcp-postgres-database-list` | ❌ |
| 13 | 0.272504 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.260993 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.259662 | `azmcp-postgres-server-config-get` | ❌ |
| 16 | 0.256969 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.253862 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.248657 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.246052 | `azmcp-monitor-workspace-list` | ❌ |
| 20 | 0.238292 | `azmcp-storage-blob-container-list` | ❌ |

---

## Test 140

**Expected Tool:** `azmcp-redis-cluster-list`  
**Prompt:** Show me the Redis Clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.744239 | `azmcp-redis-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.607511 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.580855 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.518857 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.494170 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.491235 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.456252 | `azmcp-grafana-list` | ❌ |
| 8 | 0.446568 | `azmcp-kusto-cluster-get` | ❌ |
| 9 | 0.440660 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.400163 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 11 | 0.394530 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.394483 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.393490 | `azmcp-search-service-list` | ❌ |
| 14 | 0.389814 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.372221 | `azmcp-group-list` | ❌ |
| 16 | 0.368888 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.367043 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 18 | 0.363787 | `azmcp-subscription-list` | ❌ |
| 19 | 0.357211 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.342348 | `azmcp-storage-account-list` | ❌ |

---

## Test 141

**Expected Tool:** `azmcp-group-list`  
**Prompt:** List all resource groups in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.755935 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.566552 | `azmcp-workbooks-list` | ❌ |
| 3 | 0.552487 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 4 | 0.545480 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.542878 | `azmcp-grafana-list` | ❌ |
| 6 | 0.530600 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.524770 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.524265 | `azmcp-search-service-list` | ❌ |
| 9 | 0.518520 | `azmcp-acr-registry-list` | ❌ |
| 10 | 0.517060 | `azmcp-loadtesting-testresource-list` | ❌ |
| 11 | 0.500858 | `azmcp-monitor-workspace-list` | ❌ |
| 12 | 0.486716 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.485276 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 14 | 0.479545 | `azmcp-subscription-list` | ❌ |
| 15 | 0.477393 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.460870 | `azmcp-postgres-server-list` | ❌ |
| 17 | 0.460518 | `azmcp-functionapp-list` | ❌ |
| 18 | 0.460013 | `azmcp-storage-account-list` | ❌ |
| 19 | 0.455194 | `azmcp-sql-db-list` | ❌ |
| 20 | 0.418099 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 142

**Expected Tool:** `azmcp-group-list`  
**Prompt:** Show me my resource groups  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.529504 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.463493 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 3 | 0.453960 | `azmcp-workbooks-list` | ❌ |
| 4 | 0.429014 | `azmcp-loadtesting-testresource-list` | ❌ |
| 5 | 0.426935 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.407817 | `azmcp-grafana-list` | ❌ |
| 7 | 0.391278 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.383058 | `azmcp-acr-registry-list` | ❌ |
| 9 | 0.366273 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.360229 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 11 | 0.345595 | `azmcp-redis-cluster-database-list` | ❌ |
| 12 | 0.343018 | `azmcp-sql-elastic-pool-list` | ❌ |
| 13 | 0.337270 | `azmcp-monitor-metrics-definitions` | ❌ |
| 14 | 0.335296 | `azmcp-sql-db-show` | ❌ |
| 15 | 0.328413 | `azmcp-loadtesting-testresource-create` | ❌ |
| 16 | 0.326427 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.325292 | `azmcp-kusto-cluster-list` | ❌ |
| 18 | 0.324355 | `azmcp-role-assignment-list` | ❌ |
| 19 | 0.323258 | `azmcp-extension-azqr` | ❌ |
| 20 | 0.322223 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 143

**Expected Tool:** `azmcp-group-list`  
**Prompt:** Show me the resource groups in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.665771 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.532436 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 3 | 0.523088 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.522911 | `azmcp-workbooks-list` | ❌ |
| 5 | 0.518543 | `azmcp-loadtesting-testresource-list` | ❌ |
| 6 | 0.515905 | `azmcp-grafana-list` | ❌ |
| 7 | 0.492945 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.487780 | `azmcp-acr-registry-list` | ❌ |
| 9 | 0.475313 | `azmcp-search-service-list` | ❌ |
| 10 | 0.470627 | `azmcp-kusto-cluster-list` | ❌ |
| 11 | 0.460412 | `azmcp-monitor-workspace-list` | ❌ |
| 12 | 0.451840 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 13 | 0.437648 | `azmcp-aks-cluster-list` | ❌ |
| 14 | 0.435360 | `azmcp-subscription-list` | ❌ |
| 15 | 0.432994 | `azmcp-cosmos-account-list` | ❌ |
| 16 | 0.423232 | `azmcp-postgres-server-list` | ❌ |
| 17 | 0.419100 | `azmcp-sql-db-show` | ❌ |
| 18 | 0.416138 | `azmcp-sql-db-list` | ❌ |
| 19 | 0.403277 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.388880 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 144

**Expected Tool:** `azmcp-servicebus-queue-details`  
**Prompt:** Show me the details of service bus <service_bus_name> queue <queue_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.642881 | `azmcp-servicebus-queue-details` | ✅ **EXPECTED** |
| 2 | 0.460932 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.400870 | `azmcp-servicebus-topic-details` | ❌ |
| 4 | 0.376496 | `azmcp-storage-queue-message-send` | ❌ |
| 5 | 0.375386 | `azmcp-aks-cluster-get` | ❌ |
| 6 | 0.338738 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.337239 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.323046 | `azmcp-kusto-cluster-get` | ❌ |
| 9 | 0.316350 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.310924 | `azmcp-search-index-list` | ❌ |
| 11 | 0.308567 | `azmcp-redis-cache-list` | ❌ |
| 12 | 0.296681 | `azmcp-aks-cluster-list` | ❌ |
| 13 | 0.291678 | `azmcp-redis-cluster-list` | ❌ |
| 14 | 0.278897 | `azmcp-functionapp-list` | ❌ |
| 15 | 0.278565 | `azmcp-storage-table-list` | ❌ |
| 16 | 0.278097 | `azmcp-marketplace-product-get` | ❌ |
| 17 | 0.271662 | `azmcp-loadtesting-test-get` | ❌ |
| 18 | 0.266686 | `azmcp-appconfig-kv-show` | ❌ |
| 19 | 0.258399 | `azmcp-keyvault-certificate-get` | ❌ |
| 20 | 0.255819 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 145

**Expected Tool:** `azmcp-servicebus-topic-details`  
**Prompt:** Show me the details of service bus <service_bus_name> topic <topic_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591649 | `azmcp-servicebus-topic-details` | ✅ **EXPECTED** |
| 2 | 0.571861 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.483956 | `azmcp-servicebus-queue-details` | ❌ |
| 4 | 0.361354 | `azmcp-aks-cluster-get` | ❌ |
| 5 | 0.347044 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.340036 | `azmcp-sql-db-show` | ❌ |
| 7 | 0.335558 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.324869 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.317702 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.315561 | `azmcp-redis-cluster-list` | ❌ |
| 11 | 0.306601 | `azmcp-search-index-list` | ❌ |
| 12 | 0.303980 | `azmcp-search-service-list` | ❌ |
| 13 | 0.303663 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.297323 | `azmcp-grafana-list` | ❌ |
| 15 | 0.295351 | `azmcp-functionapp-list` | ❌ |
| 16 | 0.294385 | `azmcp-marketplace-product-get` | ❌ |
| 17 | 0.290551 | `azmcp-monitor-workspace-list` | ❌ |
| 18 | 0.278717 | `azmcp-kusto-table-schema` | ❌ |
| 19 | 0.278644 | `azmcp-loadtesting-test-get` | ❌ |
| 20 | 0.275724 | `azmcp-loadtesting-testrun-list` | ❌ |

---

## Test 146

**Expected Tool:** `azmcp-servicebus-topic-subscription-details`  
**Prompt:** Show me the details of service bus <service_bus_name> subscription <subscription_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633187 | `azmcp-servicebus-topic-subscription-details` | ✅ **EXPECTED** |
| 2 | 0.494530 | `azmcp-servicebus-queue-details` | ❌ |
| 3 | 0.457036 | `azmcp-servicebus-topic-details` | ❌ |
| 4 | 0.449818 | `azmcp-search-service-list` | ❌ |
| 5 | 0.429458 | `azmcp-redis-cache-list` | ❌ |
| 6 | 0.426573 | `azmcp-kusto-cluster-get` | ❌ |
| 7 | 0.421009 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.409757 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.406289 | `azmcp-functionapp-list` | ❌ |
| 10 | 0.404739 | `azmcp-redis-cluster-list` | ❌ |
| 11 | 0.396057 | `azmcp-marketplace-product-get` | ❌ |
| 12 | 0.395176 | `azmcp-grafana-list` | ❌ |
| 13 | 0.388049 | `azmcp-postgres-server-list` | ❌ |
| 14 | 0.385222 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.374364 | `azmcp-subscription-list` | ❌ |
| 16 | 0.369986 | `azmcp-appconfig-account-list` | ❌ |
| 17 | 0.368411 | `azmcp-aks-cluster-get` | ❌ |
| 18 | 0.368202 | `azmcp-kusto-cluster-list` | ❌ |
| 19 | 0.367649 | `azmcp-group-list` | ❌ |
| 20 | 0.358070 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 147

**Expected Tool:** `azmcp-sql-db-list`  
**Prompt:** List all databases in the Azure SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.643186 | `azmcp-sql-db-list` | ✅ **EXPECTED** |
| 2 | 0.609178 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.602890 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.527896 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.482736 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.474927 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.466130 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.464525 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.457314 | `azmcp-kusto-table-list` | ❌ |
| 10 | 0.457219 | `azmcp-postgres-server-list` | ❌ |
| 11 | 0.456149 | `azmcp-monitor-table-list` | ❌ |
| 12 | 0.443648 | `azmcp-postgres-table-list` | ❌ |
| 13 | 0.441355 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.440528 | `azmcp-cosmos-database-container-list` | ❌ |
| 15 | 0.420915 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 16 | 0.400489 | `azmcp-keyvault-certificate-list` | ❌ |
| 17 | 0.395236 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.394543 | `azmcp-keyvault-secret-list` | ❌ |
| 19 | 0.382910 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.367491 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 148

**Expected Tool:** `azmcp-sql-db-list`  
**Prompt:** Show me all the databases configuration details in the Azure SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609322 | `azmcp-sql-db-list` | ✅ **EXPECTED** |
| 2 | 0.524274 | `azmcp-sql-db-show` | ❌ |
| 3 | 0.471862 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.461650 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.458742 | `azmcp-postgres-server-config-get` | ❌ |
| 6 | 0.454316 | `azmcp-sql-elastic-pool-list` | ❌ |
| 7 | 0.394366 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.387645 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.387445 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.380428 | `azmcp-appconfig-account-list` | ❌ |
| 11 | 0.357046 | `azmcp-aks-cluster-list` | ❌ |
| 12 | 0.356790 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 13 | 0.350224 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.349880 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.349685 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 16 | 0.347075 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.345262 | `azmcp-loadtesting-test-get` | ❌ |
| 18 | 0.342792 | `azmcp-appconfig-kv-list` | ❌ |
| 19 | 0.342284 | `azmcp-aks-cluster-get` | ❌ |
| 20 | 0.341681 | `azmcp-kusto-table-list` | ❌ |

---

## Test 149

**Expected Tool:** `azmcp-sql-db-show`  
**Prompt:** Get the configuration details for the SQL database <database_name> on server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.593150 | `azmcp-postgres-server-config-get` | ❌ |
| 2 | 0.528136 | `azmcp-sql-db-show` | ✅ **EXPECTED** |
| 3 | 0.465693 | `azmcp-sql-db-list` | ❌ |
| 4 | 0.446682 | `azmcp-postgres-server-param-get` | ❌ |
| 5 | 0.374073 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 6 | 0.371766 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.353995 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 8 | 0.348228 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.341701 | `azmcp-postgres-database-list` | ❌ |
| 10 | 0.341203 | `azmcp-postgres-table-schema-get` | ❌ |
| 11 | 0.325945 | `azmcp-kusto-table-schema` | ❌ |
| 12 | 0.320054 | `azmcp-aks-cluster-get` | ❌ |
| 13 | 0.301561 | `azmcp-postgres-server-param-set` | ❌ |
| 14 | 0.297839 | `azmcp-appconfig-kv-show` | ❌ |
| 15 | 0.294987 | `azmcp-appconfig-kv-list` | ❌ |
| 16 | 0.273566 | `azmcp-kusto-cluster-get` | ❌ |
| 17 | 0.273315 | `azmcp-cosmos-database-list` | ❌ |
| 18 | 0.263979 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.260930 | `azmcp-loadtesting-testrun-list` | ❌ |
| 20 | 0.253680 | `azmcp-kusto-table-list` | ❌ |

---

## Test 150

**Expected Tool:** `azmcp-sql-db-show`  
**Prompt:** Show me the details of SQL database <database_name> in server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.530095 | `azmcp-sql-db-show` | ✅ **EXPECTED** |
| 2 | 0.440073 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.421862 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.375668 | `azmcp-postgres-server-config-get` | ❌ |
| 5 | 0.361500 | `azmcp-redis-cluster-database-list` | ❌ |
| 6 | 0.357119 | `azmcp-postgres-server-param-get` | ❌ |
| 7 | 0.351744 | `azmcp-postgres-table-schema-get` | ❌ |
| 8 | 0.344694 | `azmcp-kusto-table-schema` | ❌ |
| 9 | 0.343310 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.339765 | `azmcp-postgres-server-list` | ❌ |
| 11 | 0.337996 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.328612 | `azmcp-sql-elastic-pool-list` | ❌ |
| 13 | 0.323587 | `azmcp-kusto-table-list` | ❌ |
| 14 | 0.300133 | `azmcp-cosmos-database-container-list` | ❌ |
| 15 | 0.299814 | `azmcp-aks-cluster-get` | ❌ |
| 16 | 0.296827 | `azmcp-kusto-database-list` | ❌ |
| 17 | 0.294910 | `azmcp-loadtesting-testrun-get` | ❌ |
| 18 | 0.285843 | `azmcp-kusto-cluster-get` | ❌ |
| 19 | 0.261790 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 20 | 0.252492 | `azmcp-kusto-sample` | ❌ |

---

## Test 151

**Expected Tool:** `azmcp-sql-elastic-pool-list`  
**Prompt:** List all elastic pools in SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.686435 | `azmcp-sql-elastic-pool-list` | ✅ **EXPECTED** |
| 2 | 0.502376 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.458252 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 4 | 0.434570 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.431788 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 6 | 0.431174 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.416273 | `azmcp-monitor-table-list` | ❌ |
| 8 | 0.414738 | `azmcp-postgres-database-list` | ❌ |
| 9 | 0.412061 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 10 | 0.409078 | `azmcp-monitor-table-type-list` | ❌ |
| 11 | 0.408080 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 12 | 0.394337 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.370652 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.363533 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.357347 | `azmcp-kusto-table-list` | ❌ |
| 16 | 0.351757 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.351647 | `azmcp-cosmos-database-container-list` | ❌ |
| 18 | 0.349759 | `azmcp-keyvault-key-list` | ❌ |
| 19 | 0.348568 | `azmcp-keyvault-secret-list` | ❌ |
| 20 | 0.331834 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 152

**Expected Tool:** `azmcp-sql-elastic-pool-list`  
**Prompt:** Show me the elastic pools configured for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.616579 | `azmcp-sql-elastic-pool-list` | ✅ **EXPECTED** |
| 2 | 0.457163 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.389020 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 4 | 0.385736 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 5 | 0.378556 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.357655 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 7 | 0.357019 | `azmcp-postgres-server-config-get` | ❌ |
| 8 | 0.354094 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.343841 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 10 | 0.335733 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 11 | 0.335615 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.319686 | `azmcp-aks-cluster-list` | ❌ |
| 13 | 0.304600 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.304317 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.298907 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.298264 | `azmcp-acr-registry-list` | ❌ |
| 17 | 0.297857 | `azmcp-aks-cluster-get` | ❌ |
| 18 | 0.293905 | `azmcp-cosmos-database-container-list` | ❌ |
| 19 | 0.277055 | `azmcp-loadtesting-test-get` | ❌ |
| 20 | 0.274001 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 153

**Expected Tool:** `azmcp-sql-elastic-pool-list`  
**Prompt:** What elastic pools are available in my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.602478 | `azmcp-sql-elastic-pool-list` | ✅ **EXPECTED** |
| 2 | 0.397670 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.378527 | `azmcp-monitor-table-type-list` | ❌ |
| 4 | 0.367673 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 5 | 0.344799 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.322365 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 7 | 0.315966 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 8 | 0.311302 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.308077 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.307724 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.298933 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.292558 | `azmcp-kusto-cluster-list` | ❌ |
| 13 | 0.284157 | `azmcp-kusto-database-list` | ❌ |
| 14 | 0.281680 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.259325 | `azmcp-loadtesting-testresource-list` | ❌ |
| 16 | 0.256675 | `azmcp-acr-registry-list` | ❌ |
| 17 | 0.252920 | `azmcp-foundry-models-deployments-list` | ❌ |
| 18 | 0.249936 | `azmcp-extension-az` | ❌ |
| 19 | 0.247097 | `azmcp-grafana-list` | ❌ |
| 20 | 0.240422 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 154

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** List Microsoft Entra ID administrators for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.788366 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.407432 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 3 | 0.376055 | `azmcp-sql-db-list` | ❌ |
| 4 | 0.365636 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.328968 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.328737 | `azmcp-role-assignment-list` | ❌ |
| 7 | 0.312627 | `azmcp-postgres-database-list` | ❌ |
| 8 | 0.280941 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 9 | 0.280450 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.279198 | `azmcp-sql-db-show` | ❌ |
| 11 | 0.277773 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.258095 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.249396 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 14 | 0.249153 | `azmcp-kusto-database-list` | ❌ |
| 15 | 0.246563 | `azmcp-keyvault-secret-list` | ❌ |
| 16 | 0.245267 | `azmcp-group-list` | ❌ |
| 17 | 0.238368 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.233337 | `azmcp-cosmos-database-container-list` | ❌ |
| 19 | 0.232633 | `azmcp-loadtesting-testrun-list` | ❌ |
| 20 | 0.227804 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 155

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** Show me the Entra ID administrators configured for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.718172 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.315966 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.311085 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.309059 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 5 | 0.303560 | `azmcp-postgres-server-config-get` | ❌ |
| 6 | 0.268897 | `azmcp-sql-elastic-pool-list` | ❌ |
| 7 | 0.266264 | `azmcp-postgres-server-param-get` | ❌ |
| 8 | 0.250838 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.249616 | `azmcp-postgres-database-list` | ❌ |
| 10 | 0.228064 | `azmcp-role-assignment-list` | ❌ |
| 11 | 0.214529 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.197679 | `azmcp-cosmos-database-container-list` | ❌ |
| 13 | 0.194313 | `azmcp-appconfig-account-list` | ❌ |
| 14 | 0.193050 | `azmcp-kusto-database-list` | ❌ |
| 15 | 0.191538 | `azmcp-appconfig-kv-list` | ❌ |
| 16 | 0.188120 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.186088 | `azmcp-loadtesting-testrun-list` | ❌ |
| 18 | 0.182322 | `azmcp-extension-az` | ❌ |
| 19 | 0.174217 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.172277 | `azmcp-kusto-table-schema` | ❌ |

---

## Test 156

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** What Microsoft Entra ID administrators are set up for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651257 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.253610 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.244772 | `azmcp-extension-az` | ❌ |
| 4 | 0.229560 | `azmcp-sql-elastic-pool-list` | ❌ |
| 5 | 0.228093 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 6 | 0.217698 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.205654 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.198194 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.189581 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.188753 | `azmcp-postgres-server-param-get` | ❌ |
| 11 | 0.182452 | `azmcp-postgres-server-config-get` | ❌ |
| 12 | 0.169345 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.165162 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 14 | 0.164022 | `azmcp-extension-azd` | ❌ |
| 15 | 0.163349 | `azmcp-bestpractices-get` | ❌ |
| 16 | 0.150803 | `azmcp-kusto-table-list` | ❌ |
| 17 | 0.150232 | `azmcp-cosmos-database-list` | ❌ |
| 18 | 0.148605 | `azmcp-loadtesting-testrun-update` | ❌ |
| 19 | 0.147481 | `azmcp-loadtesting-testresource-list` | ❌ |
| 20 | 0.144075 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 157

**Expected Tool:** `azmcp-sql-server-firewall-rule-list`  
**Prompt:** List all firewall rules for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.732275 | `azmcp-sql-server-firewall-rule-list` | ✅ **EXPECTED** |
| 2 | 0.397036 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.385148 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.359228 | `azmcp-sql-db-list` | ❌ |
| 5 | 0.347004 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.327808 | `azmcp-postgres-database-list` | ❌ |
| 7 | 0.304958 | `azmcp-keyvault-secret-list` | ❌ |
| 8 | 0.304175 | `azmcp-monitor-table-list` | ❌ |
| 9 | 0.301711 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.299205 | `azmcp-postgres-server-config-get` | ❌ |
| 11 | 0.298226 | `azmcp-sql-db-show` | ❌ |
| 12 | 0.278127 | `azmcp-functionapp-list` | ❌ |
| 13 | 0.278098 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.277675 | `azmcp-keyvault-key-list` | ❌ |
| 15 | 0.276828 | `azmcp-keyvault-certificate-list` | ❌ |
| 16 | 0.270667 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.263181 | `azmcp-kusto-table-list` | ❌ |
| 18 | 0.263086 | `azmcp-bestpractices-get` | ❌ |
| 19 | 0.259932 | `azmcp-extension-az` | ❌ |
| 20 | 0.253852 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 158

**Expected Tool:** `azmcp-sql-server-firewall-rule-list`  
**Prompt:** Show me the firewall rules for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.631499 | `azmcp-sql-server-firewall-rule-list` | ✅ **EXPECTED** |
| 2 | 0.321356 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.312035 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.290374 | `azmcp-extension-az` | ❌ |
| 5 | 0.290235 | `azmcp-postgres-server-config-get` | ❌ |
| 6 | 0.287747 | `azmcp-postgres-server-param-get` | ❌ |
| 7 | 0.276175 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.272586 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.272053 | `azmcp-sql-db-show` | ❌ |
| 10 | 0.255371 | `azmcp-bestpractices-get` | ❌ |
| 11 | 0.228878 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 12 | 0.226778 | `azmcp-monitor-table-list` | ❌ |
| 13 | 0.225372 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 14 | 0.208267 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 15 | 0.206761 | `azmcp-keyvault-secret-list` | ❌ |
| 16 | 0.206114 | `azmcp-kusto-table-list` | ❌ |
| 17 | 0.197711 | `azmcp-kusto-sample` | ❌ |
| 18 | 0.189871 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.189786 | `azmcp-cosmos-database-list` | ❌ |
| 20 | 0.188646 | `azmcp-kusto-query` | ❌ |

---

## Test 159

**Expected Tool:** `azmcp-sql-server-firewall-rule-list`  
**Prompt:** What firewall rules are configured for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633622 | `azmcp-sql-server-firewall-rule-list` | ✅ **EXPECTED** |
| 2 | 0.311820 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.299474 | `azmcp-extension-az` | ❌ |
| 4 | 0.277628 | `azmcp-postgres-server-config-get` | ❌ |
| 5 | 0.262028 | `azmcp-sql-db-list` | ❌ |
| 6 | 0.261404 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.261123 | `azmcp-postgres-server-param-get` | ❌ |
| 8 | 0.258402 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.247516 | `azmcp-bestpractices-get` | ❌ |
| 10 | 0.223532 | `azmcp-postgres-server-param-set` | ❌ |
| 11 | 0.220723 | `azmcp-sql-db-show` | ❌ |
| 12 | 0.205223 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 13 | 0.200326 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 14 | 0.186128 | `azmcp-loadtesting-test-get` | ❌ |
| 15 | 0.185360 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 16 | 0.167153 | `azmcp-keyvault-secret-list` | ❌ |
| 17 | 0.163295 | `azmcp-functionapp-list` | ❌ |
| 18 | 0.162568 | `azmcp-kusto-query` | ❌ |
| 19 | 0.161583 | `azmcp-appconfig-kv-list` | ❌ |
| 20 | 0.161301 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 160

**Expected Tool:** `azmcp-storage-account-create`  
**Prompt:** Create a new storage account called testaccount123 in East US region  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.529799 | `azmcp-storage-account-create` | ✅ **EXPECTED** |
| 2 | 0.412893 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.412624 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.391586 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.374006 | `azmcp-loadtesting-test-create` | ❌ |
| 6 | 0.355113 | `azmcp-loadtesting-testresource-create` | ❌ |
| 7 | 0.346555 | `azmcp-storage-blob-list` | ❌ |
| 8 | 0.343651 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.325925 | `azmcp-keyvault-secret-create` | ❌ |
| 10 | 0.323501 | `azmcp-appconfig-kv-set` | ❌ |
| 11 | 0.315272 | `azmcp-keyvault-key-create` | ❌ |
| 12 | 0.311744 | `azmcp-storage-blob-container-create` | ❌ |
| 13 | 0.308283 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 14 | 0.303692 | `azmcp-keyvault-certificate-create` | ❌ |
| 15 | 0.298887 | `azmcp-storage-datalake-directory-create` | ❌ |
| 16 | 0.297236 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.292627 | `azmcp-storage-blob-details` | ❌ |
| 18 | 0.289742 | `azmcp-appconfig-kv-show` | ❌ |
| 19 | 0.277805 | `azmcp-cosmos-database-container-list` | ❌ |
| 20 | 0.264217 | `azmcp-appconfig-kv-lock` | ❌ |

---

## Test 161

**Expected Tool:** `azmcp-storage-account-create`  
**Prompt:** Create a storage account with premium performance and LRS replication  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.492373 | `azmcp-storage-account-create` | ✅ **EXPECTED** |
| 2 | 0.404063 | `azmcp-storage-account-list` | ❌ |
| 3 | 0.369322 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.361412 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.359300 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 6 | 0.355743 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.344391 | `azmcp-loadtesting-testresource-create` | ❌ |
| 8 | 0.329099 | `azmcp-loadtesting-test-create` | ❌ |
| 9 | 0.327972 | `azmcp-storage-blob-list` | ❌ |
| 10 | 0.310332 | `azmcp-workbooks-create` | ❌ |
| 11 | 0.309596 | `azmcp-monitor-resource-log-query` | ❌ |
| 12 | 0.309352 | `azmcp-storage-blob-container-create` | ❌ |
| 13 | 0.302787 | `azmcp-extension-az` | ❌ |
| 14 | 0.284886 | `azmcp-bestpractices-get` | ❌ |
| 15 | 0.284385 | `azmcp-cosmos-account-list` | ❌ |
| 16 | 0.281142 | `azmcp-appconfig-kv-lock` | ❌ |
| 17 | 0.279141 | `azmcp-keyvault-certificate-create` | ❌ |
| 18 | 0.278882 | `azmcp-keyvault-key-create` | ❌ |
| 19 | 0.272299 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 20 | 0.264381 | `azmcp-extension-azqr` | ❌ |

---

## Test 162

**Expected Tool:** `azmcp-storage-account-create`  
**Prompt:** Create a new storage account with Data Lake Storage Gen2 enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628313 | `azmcp-storage-account-create` | ✅ **EXPECTED** |
| 2 | 0.444359 | `azmcp-storage-datalake-directory-create` | ❌ |
| 3 | 0.426606 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.424991 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.392966 | `azmcp-storage-blob-container-create` | ❌ |
| 6 | 0.389091 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.386262 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.384073 | `azmcp-loadtesting-testresource-create` | ❌ |
| 9 | 0.380638 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.380537 | `azmcp-keyvault-key-create` | ❌ |
| 11 | 0.372115 | `azmcp-storage-blob-list` | ❌ |
| 12 | 0.371756 | `azmcp-keyvault-certificate-create` | ❌ |
| 13 | 0.363721 | `azmcp-workbooks-create` | ❌ |
| 14 | 0.359330 | `azmcp-keyvault-secret-create` | ❌ |
| 15 | 0.356635 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 16 | 0.309241 | `azmcp-appconfig-kv-set` | ❌ |
| 17 | 0.302875 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.296585 | `azmcp-cosmos-database-container-list` | ❌ |
| 19 | 0.289633 | `azmcp-cosmos-database-list` | ❌ |
| 20 | 0.284126 | `azmcp-extension-az` | ❌ |

---

## Test 163

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** List all storage accounts in my subscription including their location and SKU  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.699451 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.581393 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.540735 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.536909 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.501088 | `azmcp-subscription-list` | ❌ |
| 6 | 0.496742 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.493246 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.471507 | `azmcp-search-service-list` | ❌ |
| 9 | 0.459793 | `azmcp-functionapp-list` | ❌ |
| 10 | 0.458793 | `azmcp-monitor-workspace-list` | ❌ |
| 11 | 0.454195 | `azmcp-acr-registry-list` | ❌ |
| 12 | 0.448591 | `azmcp-storage-blob-container-details` | ❌ |
| 13 | 0.447964 | `azmcp-aks-cluster-list` | ❌ |
| 14 | 0.445545 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.441838 | `azmcp-redis-cluster-list` | ❌ |
| 16 | 0.432560 | `azmcp-kusto-cluster-list` | ❌ |
| 17 | 0.416387 | `azmcp-group-list` | ❌ |
| 18 | 0.412679 | `azmcp-grafana-list` | ❌ |
| 19 | 0.393518 | `azmcp-cosmos-database-list` | ❌ |
| 20 | 0.389849 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 164

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** Show me my storage accounts with whether hierarchical namespace (HNS) is enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.575082 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.498860 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.450677 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.424921 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.421642 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.419265 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.411558 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 8 | 0.375553 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.367906 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.362578 | `azmcp-storage-account-create` | ❌ |
| 11 | 0.347173 | `azmcp-appconfig-account-list` | ❌ |
| 12 | 0.346039 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.342056 | `azmcp-subscription-list` | ❌ |
| 14 | 0.341752 | `azmcp-storage-blob-details` | ❌ |
| 15 | 0.335306 | `azmcp-appconfig-kv-show` | ❌ |
| 16 | 0.330385 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.327902 | `azmcp-functionapp-list` | ❌ |
| 18 | 0.322272 | `azmcp-keyvault-key-list` | ❌ |
| 19 | 0.312384 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.299108 | `azmcp-keyvault-secret-list` | ❌ |

---

## Test 165

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** Show me the storage accounts in my subscription and include HTTPS-only and public blob access settings  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.610561 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.501033 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.499153 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.485850 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.473598 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.453933 | `azmcp-subscription-list` | ❌ |
| 7 | 0.431468 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.424322 | `azmcp-storage-blob-details` | ❌ |
| 9 | 0.418264 | `azmcp-search-service-list` | ❌ |
| 10 | 0.415080 | `azmcp-appconfig-account-list` | ❌ |
| 11 | 0.401678 | `azmcp-storage-account-create` | ❌ |
| 12 | 0.383734 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 13 | 0.383337 | `azmcp-functionapp-list` | ❌ |
| 14 | 0.382328 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.376660 | `azmcp-appconfig-kv-show` | ❌ |
| 16 | 0.359998 | `azmcp-cosmos-database-list` | ❌ |
| 17 | 0.359053 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.353273 | `azmcp-cosmos-database-container-list` | ❌ |
| 19 | 0.342616 | `azmcp-bestpractices-get` | ❌ |
| 20 | 0.341045 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 166

**Expected Tool:** `azmcp-storage-blob-batch-set-tier`  
**Prompt:** Set access tier to Cool for multiple blobs in the container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.696993 | `azmcp-storage-blob-batch-set-tier` | ✅ **EXPECTED** |
| 2 | 0.492966 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.462118 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.455287 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.419266 | `azmcp-storage-blob-container-create` | ❌ |
| 6 | 0.409148 | `azmcp-storage-blob-details` | ❌ |
| 7 | 0.382121 | `azmcp-cosmos-database-container-list` | ❌ |
| 8 | 0.373709 | `azmcp-storage-account-create` | ❌ |
| 9 | 0.365263 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.327050 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.308915 | `azmcp-appconfig-kv-set` | ❌ |
| 12 | 0.305879 | `azmcp-appconfig-kv-unlock` | ❌ |
| 13 | 0.305416 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 14 | 0.304870 | `azmcp-appconfig-kv-lock` | ❌ |
| 15 | 0.292635 | `azmcp-storage-queue-message-send` | ❌ |
| 16 | 0.284246 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.276161 | `azmcp-appconfig-kv-show` | ❌ |
| 18 | 0.257792 | `azmcp-extension-az` | ❌ |
| 19 | 0.230100 | `azmcp-keyvault-secret-create` | ❌ |
| 20 | 0.229809 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 167

**Expected Tool:** `azmcp-storage-blob-batch-set-tier`  
**Prompt:** Change the access tier to Archive for blobs file1.txt and file2.txt in the container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.616237 | `azmcp-storage-blob-batch-set-tier` | ✅ **EXPECTED** |
| 2 | 0.441478 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.425383 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.410237 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.361953 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.359545 | `azmcp-storage-account-create` | ❌ |
| 7 | 0.350832 | `azmcp-storage-blob-details` | ❌ |
| 8 | 0.350589 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.349075 | `azmcp-storage-blob-container-create` | ❌ |
| 10 | 0.347648 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.297814 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 12 | 0.292106 | `azmcp-appconfig-kv-lock` | ❌ |
| 13 | 0.280472 | `azmcp-extension-az` | ❌ |
| 14 | 0.276293 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.276128 | `azmcp-appconfig-kv-unlock` | ❌ |
| 16 | 0.265543 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 17 | 0.249577 | `azmcp-appconfig-kv-set` | ❌ |
| 18 | 0.239475 | `azmcp-bestpractices-get` | ❌ |
| 19 | 0.238078 | `azmcp-cosmos-database-list` | ❌ |
| 20 | 0.237216 | `azmcp-appconfig-kv-show` | ❌ |

---

## Test 168

**Expected Tool:** `azmcp-storage-blob-container-create`  
**Prompt:** Create the storage container mycontainer in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.586723 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.535980 | `azmcp-storage-account-create` | ❌ |
| 3 | 0.506905 | `azmcp-storage-blob-container-details` | ❌ |
| 4 | 0.498724 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.464848 | `azmcp-cosmos-database-container-list` | ❌ |
| 6 | 0.442551 | `azmcp-storage-blob-container-create` | ✅ **EXPECTED** |
| 7 | 0.397262 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.396023 | `azmcp-storage-account-list` | ❌ |
| 9 | 0.363780 | `azmcp-storage-blob-details` | ❌ |
| 10 | 0.342627 | `azmcp-keyvault-secret-create` | ❌ |
| 11 | 0.341219 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 12 | 0.334239 | `azmcp-keyvault-key-create` | ❌ |
| 13 | 0.333864 | `azmcp-appconfig-kv-set` | ❌ |
| 14 | 0.332880 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 15 | 0.323747 | `azmcp-storage-datalake-directory-create` | ❌ |
| 16 | 0.320668 | `azmcp-keyvault-certificate-create` | ❌ |
| 17 | 0.300712 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.288125 | `azmcp-appconfig-kv-lock` | ❌ |
| 19 | 0.280228 | `azmcp-appconfig-kv-show` | ❌ |
| 20 | 0.278995 | `azmcp-loadtesting-testresource-create` | ❌ |

---

## Test 169

**Expected Tool:** `azmcp-storage-blob-container-create`  
**Prompt:** Create the container using blob public access in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.572307 | `azmcp-storage-blob-container-create` | ✅ **EXPECTED** |
| 2 | 0.529860 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.528538 | `azmcp-storage-blob-list` | ❌ |
| 4 | 0.524401 | `azmcp-storage-account-create` | ❌ |
| 5 | 0.498758 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.454786 | `azmcp-storage-blob-details` | ❌ |
| 7 | 0.425854 | `azmcp-cosmos-database-container-list` | ❌ |
| 8 | 0.417384 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 9 | 0.383216 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.367078 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.308107 | `azmcp-keyvault-secret-create` | ❌ |
| 12 | 0.302725 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 13 | 0.299737 | `azmcp-keyvault-key-create` | ❌ |
| 14 | 0.293702 | `azmcp-keyvault-certificate-create` | ❌ |
| 15 | 0.289619 | `azmcp-workbooks-create` | ❌ |
| 16 | 0.282349 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.277475 | `azmcp-appconfig-kv-set` | ❌ |
| 18 | 0.273619 | `azmcp-loadtesting-testresource-create` | ❌ |
| 19 | 0.267195 | `azmcp-appconfig-kv-lock` | ❌ |
| 20 | 0.256710 | `azmcp-loadtesting-test-create` | ❌ |

---

## Test 170

**Expected Tool:** `azmcp-storage-blob-container-create`  
**Prompt:** Create a new blob container named documents with container public access in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.507833 | `azmcp-storage-blob-container-create` | ✅ **EXPECTED** |
| 2 | 0.492810 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.492008 | `azmcp-storage-blob-list` | ❌ |
| 4 | 0.490274 | `azmcp-storage-account-create` | ❌ |
| 5 | 0.461479 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.451961 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.411617 | `azmcp-storage-blob-details` | ❌ |
| 8 | 0.383494 | `azmcp-storage-table-list` | ❌ |
| 9 | 0.381329 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 10 | 0.373517 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 11 | 0.363629 | `azmcp-storage-account-list` | ❌ |
| 12 | 0.335160 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.334420 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.299368 | `azmcp-keyvault-certificate-create` | ❌ |
| 15 | 0.294659 | `azmcp-workbooks-create` | ❌ |
| 16 | 0.294123 | `azmcp-keyvault-secret-create` | ❌ |
| 17 | 0.286954 | `azmcp-keyvault-key-create` | ❌ |
| 18 | 0.278916 | `azmcp-appconfig-kv-set` | ❌ |
| 19 | 0.262154 | `azmcp-appconfig-kv-lock` | ❌ |
| 20 | 0.257249 | `azmcp-loadtesting-testresource-create` | ❌ |

---

## Test 171

**Expected Tool:** `azmcp-storage-blob-container-details`  
**Prompt:** Show me the properties of the storage container files in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.667718 | `azmcp-storage-blob-container-details` | ✅ **EXPECTED** |
| 2 | 0.666091 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.600582 | `azmcp-storage-blob-list` | ❌ |
| 4 | 0.562628 | `azmcp-storage-blob-details` | ❌ |
| 5 | 0.537937 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.525001 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.515548 | `azmcp-cosmos-database-container-list` | ❌ |
| 8 | 0.456572 | `azmcp-storage-account-create` | ❌ |
| 9 | 0.435234 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.432791 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.410040 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 12 | 0.380620 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.378780 | `azmcp-monitor-resource-log-query` | ❌ |
| 14 | 0.367178 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 15 | 0.366819 | `azmcp-storage-blob-container-create` | ❌ |
| 16 | 0.356007 | `azmcp-appconfig-kv-list` | ❌ |
| 17 | 0.345628 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.335616 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.324014 | `azmcp-keyvault-secret-list` | ❌ |
| 20 | 0.323854 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 172

**Expected Tool:** `azmcp-storage-blob-container-list`  
**Prompt:** List all blob containers in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.755983 | `azmcp-storage-blob-container-list` | ✅ **EXPECTED** |
| 2 | 0.727277 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.629987 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.558951 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.540541 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.519003 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.505735 | `azmcp-storage-blob-details` | ❌ |
| 8 | 0.468593 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.460731 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.416998 | `azmcp-storage-blob-container-create` | ❌ |
| 11 | 0.413843 | `azmcp-storage-account-create` | ❌ |
| 12 | 0.381682 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 13 | 0.376933 | `azmcp-keyvault-key-list` | ❌ |
| 14 | 0.367042 | `azmcp-keyvault-certificate-list` | ❌ |
| 15 | 0.360451 | `azmcp-keyvault-secret-list` | ❌ |
| 16 | 0.351913 | `azmcp-subscription-list` | ❌ |
| 17 | 0.347599 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.339362 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.337882 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.332655 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 173

**Expected Tool:** `azmcp-storage-blob-container-list`  
**Prompt:** Show me the blob containers in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.692491 | `azmcp-storage-blob-container-list` | ✅ **EXPECTED** |
| 2 | 0.661978 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.578331 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.525905 | `azmcp-storage-blob-details` | ❌ |
| 5 | 0.520200 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.514682 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.505990 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.447566 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.423899 | `azmcp-storage-blob-container-create` | ❌ |
| 10 | 0.417130 | `azmcp-storage-account-create` | ❌ |
| 11 | 0.410884 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.359283 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 13 | 0.343809 | `azmcp-appconfig-kv-show` | ❌ |
| 14 | 0.337936 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 15 | 0.335904 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 16 | 0.331725 | `azmcp-acr-registry-list` | ❌ |
| 17 | 0.325814 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.318193 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.311328 | `azmcp-keyvault-certificate-list` | ❌ |
| 20 | 0.311152 | `azmcp-keyvault-secret-list` | ❌ |

---

## Test 174

**Expected Tool:** `azmcp-storage-blob-details`  
**Prompt:** Show me the properties for blob <blob_name> in container <container_name> in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.658235 | `azmcp-storage-blob-details` | ✅ **EXPECTED** |
| 2 | 0.636267 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.610028 | `azmcp-storage-blob-container-details` | ❌ |
| 4 | 0.580237 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.500990 | `azmcp-cosmos-database-container-list` | ❌ |
| 6 | 0.443899 | `azmcp-storage-blob-container-create` | ❌ |
| 7 | 0.443515 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.428375 | `azmcp-storage-account-list` | ❌ |
| 9 | 0.398507 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.387446 | `azmcp-storage-account-create` | ❌ |
| 11 | 0.374386 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 12 | 0.357758 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 13 | 0.355804 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.337388 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.316771 | `azmcp-monitor-resource-log-query` | ❌ |
| 16 | 0.296874 | `azmcp-appconfig-kv-list` | ❌ |
| 17 | 0.295298 | `azmcp-appconfig-kv-lock` | ❌ |
| 18 | 0.286744 | `azmcp-keyvault-key-list` | ❌ |
| 19 | 0.281357 | `azmcp-aks-cluster-get` | ❌ |
| 20 | 0.275667 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 175

**Expected Tool:** `azmcp-storage-blob-details`  
**Prompt:** Get the details about blob <blob_name> in the container <container_name> in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.636162 | `azmcp-storage-blob-container-details` | ❌ |
| 2 | 0.626619 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.625695 | `azmcp-storage-blob-details` | ✅ **EXPECTED** |
| 4 | 0.567166 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.472775 | `azmcp-cosmos-database-container-list` | ❌ |
| 6 | 0.441969 | `azmcp-storage-blob-container-create` | ❌ |
| 7 | 0.423044 | `azmcp-storage-account-create` | ❌ |
| 8 | 0.419302 | `azmcp-storage-account-list` | ❌ |
| 9 | 0.409580 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.372648 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 11 | 0.368006 | `azmcp-aks-cluster-get` | ❌ |
| 12 | 0.367356 | `azmcp-appconfig-kv-show` | ❌ |
| 13 | 0.363222 | `azmcp-workbooks-show` | ❌ |
| 14 | 0.355347 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 15 | 0.347279 | `azmcp-kusto-cluster-get` | ❌ |
| 16 | 0.332842 | `azmcp-keyvault-certificate-get` | ❌ |
| 17 | 0.327805 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.306354 | `azmcp-loadtesting-test-get` | ❌ |
| 19 | 0.290539 | `azmcp-cosmos-database-list` | ❌ |
| 20 | 0.288214 | `azmcp-appconfig-kv-lock` | ❌ |

---

## Test 176

**Expected Tool:** `azmcp-storage-blob-list`  
**Prompt:** List all blobs in the blob container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.781334 | `azmcp-storage-blob-list` | ✅ **EXPECTED** |
| 2 | 0.698225 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.596811 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.539646 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.539217 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.524098 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.520550 | `azmcp-storage-blob-details` | ❌ |
| 8 | 0.449480 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.423179 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.422976 | `azmcp-storage-blob-container-create` | ❌ |
| 11 | 0.417271 | `azmcp-storage-account-create` | ❌ |
| 12 | 0.405584 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 13 | 0.389021 | `azmcp-keyvault-key-list` | ❌ |
| 14 | 0.381823 | `azmcp-keyvault-secret-list` | ❌ |
| 15 | 0.378366 | `azmcp-keyvault-certificate-list` | ❌ |
| 16 | 0.368241 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 17 | 0.363818 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 18 | 0.331606 | `azmcp-functionapp-list` | ❌ |
| 19 | 0.319109 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.318972 | `azmcp-appconfig-kv-list` | ❌ |

---

## Test 177

**Expected Tool:** `azmcp-storage-blob-list`  
**Prompt:** Show me the blobs in the blob container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.699726 | `azmcp-storage-blob-list` | ✅ **EXPECTED** |
| 2 | 0.642459 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.550249 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.549408 | `azmcp-storage-blob-details` | ❌ |
| 5 | 0.547345 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.476647 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.459208 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.451296 | `azmcp-storage-blob-container-create` | ❌ |
| 9 | 0.401214 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.395385 | `azmcp-storage-account-create` | ❌ |
| 11 | 0.384647 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 12 | 0.382161 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 13 | 0.364907 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.351493 | `azmcp-appconfig-kv-show` | ❌ |
| 15 | 0.343120 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 16 | 0.309456 | `azmcp-keyvault-key-list` | ❌ |
| 17 | 0.305836 | `azmcp-keyvault-secret-list` | ❌ |
| 18 | 0.298533 | `azmcp-keyvault-certificate-list` | ❌ |
| 19 | 0.296009 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.289538 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 178

**Expected Tool:** `azmcp-storage-datalake-directory-create`  
**Prompt:** Create a new directory at the path <directory_path> in Data Lake in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661238 | `azmcp-storage-datalake-directory-create` | ✅ **EXPECTED** |
| 2 | 0.498492 | `azmcp-storage-account-create` | ❌ |
| 3 | 0.466092 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 4 | 0.383890 | `azmcp-keyvault-secret-create` | ❌ |
| 5 | 0.372591 | `azmcp-keyvault-certificate-create` | ❌ |
| 6 | 0.366022 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.359267 | `azmcp-storage-blob-container-list` | ❌ |
| 8 | 0.332097 | `azmcp-storage-table-list` | ❌ |
| 9 | 0.315606 | `azmcp-loadtesting-testresource-create` | ❌ |
| 10 | 0.308195 | `azmcp-loadtesting-test-create` | ❌ |
| 11 | 0.304725 | `azmcp-storage-account-list` | ❌ |
| 12 | 0.301584 | `azmcp-storage-blob-list` | ❌ |
| 13 | 0.301136 | `azmcp-appconfig-kv-set` | ❌ |
| 14 | 0.297395 | `azmcp-workbooks-create` | ❌ |
| 15 | 0.282353 | `azmcp-storage-blob-container-details` | ❌ |
| 16 | 0.261764 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.260048 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 18 | 0.248249 | `azmcp-cosmos-database-list` | ❌ |
| 19 | 0.247315 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 20 | 0.241156 | `azmcp-appconfig-kv-lock` | ❌ |

---

## Test 179

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** List all paths in the Data Lake file system <file_system_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.771997 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.493098 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.481725 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.478289 | `azmcp-storage-datalake-directory-create` | ❌ |
| 5 | 0.466614 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.462487 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.423761 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.421423 | `azmcp-storage-share-file-list` | ❌ |
| 9 | 0.414332 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.402737 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.401558 | `azmcp-monitor-table-list` | ❌ |
| 12 | 0.394205 | `azmcp-keyvault-key-list` | ❌ |
| 13 | 0.382355 | `azmcp-monitor-table-type-list` | ❌ |
| 14 | 0.368150 | `azmcp-keyvault-certificate-list` | ❌ |
| 15 | 0.366988 | `azmcp-keyvault-secret-list` | ❌ |
| 16 | 0.364623 | `azmcp-monitor-workspace-list` | ❌ |
| 17 | 0.363017 | `azmcp-functionapp-list` | ❌ |
| 18 | 0.339876 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.330988 | `azmcp-kusto-table-list` | ❌ |
| 20 | 0.328432 | `azmcp-kusto-database-list` | ❌ |

---

## Test 180

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** Show me the paths in the Data Lake file system <file_system_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.745256 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.464603 | `azmcp-storage-datalake-directory-create` | ❌ |
| 3 | 0.438075 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.433533 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.407149 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.403132 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.382229 | `azmcp-storage-share-file-list` | ❌ |
| 8 | 0.368149 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.353149 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.351701 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.344977 | `azmcp-monitor-table-type-list` | ❌ |
| 12 | 0.343677 | `azmcp-monitor-resource-log-query` | ❌ |
| 13 | 0.343639 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.324283 | `azmcp-keyvault-key-list` | ❌ |
| 15 | 0.304975 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 16 | 0.304166 | `azmcp-keyvault-secret-list` | ❌ |
| 17 | 0.300686 | `azmcp-functionapp-list` | ❌ |
| 18 | 0.291196 | `azmcp-keyvault-certificate-list` | ❌ |
| 19 | 0.283128 | `azmcp-kusto-table-list` | ❌ |
| 20 | 0.276389 | `azmcp-appconfig-kv-show` | ❌ |

---

## Test 181

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** Recursively list all paths in the Data Lake file system <file_system_name> in the storage account <account_name> filtered by <filter_path>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.786333 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.464962 | `azmcp-storage-share-file-list` | ❌ |
| 3 | 0.434881 | `azmcp-storage-datalake-directory-create` | ❌ |
| 4 | 0.403314 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.396753 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.390408 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.375971 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.343509 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.338093 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.336798 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.333351 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.325706 | `azmcp-monitor-table-list` | ❌ |
| 13 | 0.324384 | `azmcp-functionapp-list` | ❌ |
| 14 | 0.312871 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 15 | 0.306982 | `azmcp-storage-blob-container-details` | ❌ |
| 16 | 0.306205 | `azmcp-keyvault-key-list` | ❌ |
| 17 | 0.296976 | `azmcp-keyvault-certificate-list` | ❌ |
| 18 | 0.290391 | `azmcp-keyvault-secret-list` | ❌ |
| 19 | 0.287924 | `azmcp-kusto-table-list` | ❌ |
| 20 | 0.284314 | `azmcp-kusto-database-list` | ❌ |

---

## Test 182

**Expected Tool:** `azmcp-storage-queue-message-send`  
**Prompt:** Send a message "Hello, World!" to the queue <queue_name> in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.539274 | `azmcp-storage-queue-message-send` | ✅ **EXPECTED** |
| 2 | 0.416281 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.402428 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.397625 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.379570 | `azmcp-storage-account-create` | ❌ |
| 6 | 0.378065 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.354106 | `azmcp-servicebus-queue-details` | ❌ |
| 8 | 0.326593 | `azmcp-appconfig-kv-set` | ❌ |
| 9 | 0.322902 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.315197 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.314590 | `azmcp-appconfig-kv-show` | ❌ |
| 12 | 0.311620 | `azmcp-cosmos-database-container-list` | ❌ |
| 13 | 0.308183 | `azmcp-monitor-resource-log-query` | ❌ |
| 14 | 0.304110 | `azmcp-appconfig-kv-lock` | ❌ |
| 15 | 0.289924 | `azmcp-storage-blob-container-details` | ❌ |
| 16 | 0.281854 | `azmcp-cosmos-database-list` | ❌ |
| 17 | 0.269343 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 18 | 0.265023 | `azmcp-appconfig-kv-unlock` | ❌ |
| 19 | 0.257569 | `azmcp-keyvault-secret-create` | ❌ |
| 20 | 0.247435 | `azmcp-extension-az` | ❌ |

---

## Test 183

**Expected Tool:** `azmcp-storage-queue-message-send`  
**Prompt:** Send a message with TTL of 3600 seconds to the queue <queue_name> in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.627403 | `azmcp-storage-queue-message-send` | ✅ **EXPECTED** |
| 2 | 0.391804 | `azmcp-servicebus-queue-details` | ❌ |
| 3 | 0.382446 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.364926 | `azmcp-storage-account-create` | ❌ |
| 5 | 0.361205 | `azmcp-storage-blob-container-list` | ❌ |
| 6 | 0.351951 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.334221 | `azmcp-storage-blob-list` | ❌ |
| 8 | 0.333301 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 9 | 0.331278 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.305837 | `azmcp-appconfig-kv-set` | ❌ |
| 11 | 0.300628 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 12 | 0.296619 | `azmcp-storage-blob-container-details` | ❌ |
| 13 | 0.291760 | `azmcp-appconfig-kv-lock` | ❌ |
| 14 | 0.277939 | `azmcp-cosmos-database-container-list` | ❌ |
| 15 | 0.274957 | `azmcp-appconfig-kv-show` | ❌ |
| 16 | 0.270281 | `azmcp-keyvault-secret-create` | ❌ |
| 17 | 0.264516 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.253023 | `azmcp-appconfig-kv-unlock` | ❌ |
| 19 | 0.250190 | `azmcp-keyvault-key-create` | ❌ |
| 20 | 0.232608 | `azmcp-extension-az` | ❌ |

---

## Test 184

**Expected Tool:** `azmcp-storage-queue-message-send`  
**Prompt:** Add a message to the queue <queue_name> in storage account <account_name> with visibility timeout of 30 seconds  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.593042 | `azmcp-storage-queue-message-send` | ✅ **EXPECTED** |
| 2 | 0.374054 | `azmcp-servicebus-queue-details` | ❌ |
| 3 | 0.370618 | `azmcp-storage-account-create` | ❌ |
| 4 | 0.340740 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.336156 | `azmcp-storage-blob-container-list` | ❌ |
| 6 | 0.334611 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.315059 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.304272 | `azmcp-storage-blob-list` | ❌ |
| 9 | 0.298459 | `azmcp-appconfig-kv-lock` | ❌ |
| 10 | 0.295351 | `azmcp-keyvault-secret-create` | ❌ |
| 11 | 0.283393 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 12 | 0.281054 | `azmcp-storage-blob-container-details` | ❌ |
| 13 | 0.276347 | `azmcp-appconfig-kv-show` | ❌ |
| 14 | 0.269006 | `azmcp-monitor-resource-log-query` | ❌ |
| 15 | 0.267398 | `azmcp-keyvault-key-create` | ❌ |
| 16 | 0.259428 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.255833 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 18 | 0.252116 | `azmcp-appconfig-kv-unlock` | ❌ |
| 19 | 0.243694 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.237321 | `azmcp-keyvault-certificate-create` | ❌ |

---

## Test 185

**Expected Tool:** `azmcp-storage-share-file-list`  
**Prompt:** List all files and directories in the File Share <share_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633600 | `azmcp-storage-share-file-list` | ✅ **EXPECTED** |
| 2 | 0.566864 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.548129 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.522013 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.521134 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.502204 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 7 | 0.428574 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.424113 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.421484 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.403858 | `azmcp-keyvault-key-list` | ❌ |
| 11 | 0.403490 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.403299 | `azmcp-keyvault-secret-list` | ❌ |
| 13 | 0.388314 | `azmcp-keyvault-certificate-list` | ❌ |
| 14 | 0.383106 | `azmcp-storage-account-create` | ❌ |
| 15 | 0.369482 | `azmcp-subscription-list` | ❌ |
| 16 | 0.368242 | `azmcp-monitor-table-list` | ❌ |
| 17 | 0.357727 | `azmcp-functionapp-list` | ❌ |
| 18 | 0.353989 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.335195 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.331873 | `azmcp-appconfig-kv-list` | ❌ |

---

## Test 186

**Expected Tool:** `azmcp-storage-share-file-list`  
**Prompt:** Show me the files in the File Share <share_name> directory <directory_path> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.542448 | `azmcp-storage-share-file-list` | ✅ **EXPECTED** |
| 2 | 0.499705 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 3 | 0.482291 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.467153 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.438000 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.435773 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.406137 | `azmcp-storage-datalake-directory-create` | ❌ |
| 8 | 0.380528 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.363337 | `azmcp-storage-account-create` | ❌ |
| 10 | 0.358592 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.354819 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.340054 | `azmcp-monitor-resource-log-query` | ❌ |
| 13 | 0.337301 | `azmcp-appconfig-kv-show` | ❌ |
| 14 | 0.326024 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.324795 | `azmcp-keyvault-secret-list` | ❌ |
| 16 | 0.318234 | `azmcp-keyvault-key-list` | ❌ |
| 17 | 0.312911 | `azmcp-appconfig-account-list` | ❌ |
| 18 | 0.307229 | `azmcp-keyvault-certificate-list` | ❌ |
| 19 | 0.303683 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 20 | 0.295763 | `azmcp-extension-azqr` | ❌ |

---

## Test 187

**Expected Tool:** `azmcp-storage-share-file-list`  
**Prompt:** List files with prefix 'report' in the File Share <share_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.589189 | `azmcp-storage-share-file-list` | ✅ **EXPECTED** |
| 2 | 0.455145 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.452776 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.447178 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 5 | 0.436979 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.420943 | `azmcp-extension-azqr` | ❌ |
| 7 | 0.418634 | `azmcp-storage-blob-list` | ❌ |
| 8 | 0.376409 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.373343 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.365714 | `azmcp-workbooks-list` | ❌ |
| 11 | 0.358021 | `azmcp-storage-blob-container-details` | ❌ |
| 12 | 0.341499 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.340247 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.337124 | `azmcp-cosmos-database-container-list` | ❌ |
| 15 | 0.334752 | `azmcp-keyvault-certificate-list` | ❌ |
| 16 | 0.321923 | `azmcp-keyvault-secret-list` | ❌ |
| 17 | 0.316894 | `azmcp-functionapp-list` | ❌ |
| 18 | 0.316020 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.315230 | `azmcp-keyvault-key-list` | ❌ |
| 20 | 0.302649 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 188

**Expected Tool:** `azmcp-storage-table-list`  
**Prompt:** List all tables in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.790094 | `azmcp-storage-table-list` | ✅ **EXPECTED** |
| 2 | 0.612805 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.584417 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.559592 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.540808 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.513277 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.511143 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.504758 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.492182 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.485750 | `azmcp-kusto-table-list` | ❌ |
| 11 | 0.479637 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.424688 | `azmcp-storage-blob-container-details` | ❌ |
| 13 | 0.401483 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.398832 | `azmcp-keyvault-key-list` | ❌ |
| 15 | 0.398805 | `azmcp-sql-db-list` | ❌ |
| 16 | 0.397545 | `azmcp-kusto-database-list` | ❌ |
| 17 | 0.370923 | `azmcp-keyvault-certificate-list` | ❌ |
| 18 | 0.357974 | `azmcp-keyvault-secret-list` | ❌ |
| 19 | 0.355473 | `azmcp-kusto-table-schema` | ❌ |
| 20 | 0.343908 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 189

**Expected Tool:** `azmcp-storage-table-list`  
**Prompt:** Show me the tables in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.746243 | `azmcp-storage-table-list` | ✅ **EXPECTED** |
| 2 | 0.596267 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.534397 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.528309 | `azmcp-monitor-table-list` | ❌ |
| 5 | 0.515698 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.490488 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.489228 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.472357 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.463396 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.448611 | `azmcp-storage-blob-container-details` | ❌ |
| 11 | 0.447390 | `azmcp-kusto-table-list` | ❌ |
| 12 | 0.420569 | `azmcp-postgres-table-list` | ❌ |
| 13 | 0.401869 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.400969 | `azmcp-monitor-resource-log-query` | ❌ |
| 15 | 0.378049 | `azmcp-keyvault-key-list` | ❌ |
| 16 | 0.372688 | `azmcp-kusto-table-schema` | ❌ |
| 17 | 0.360976 | `azmcp-kusto-database-list` | ❌ |
| 18 | 0.360366 | `azmcp-appconfig-kv-show` | ❌ |
| 19 | 0.353251 | `azmcp-kusto-sample` | ❌ |
| 20 | 0.349575 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 190

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** List all subscriptions for my account  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.576055 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.512964 | `azmcp-cosmos-account-list` | ❌ |
| 3 | 0.473852 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.471653 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.470819 | `azmcp-search-service-list` | ❌ |
| 6 | 0.463550 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.450973 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.445724 | `azmcp-grafana-list` | ❌ |
| 9 | 0.436338 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.431346 | `azmcp-kusto-cluster-list` | ❌ |
| 11 | 0.430280 | `azmcp-group-list` | ❌ |
| 12 | 0.406935 | `azmcp-appconfig-account-list` | ❌ |
| 13 | 0.394924 | `azmcp-aks-cluster-list` | ❌ |
| 14 | 0.393796 | `azmcp-functionapp-list` | ❌ |
| 15 | 0.388737 | `azmcp-monitor-workspace-list` | ❌ |
| 16 | 0.366860 | `azmcp-loadtesting-testresource-list` | ❌ |
| 17 | 0.364799 | `azmcp-storage-blob-container-list` | ❌ |
| 18 | 0.354353 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.345154 | `azmcp-cosmos-database-list` | ❌ |
| 20 | 0.344901 | `azmcp-servicebus-topic-subscription-details` | ❌ |

---

## Test 191

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** Show me my subscriptions  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.405723 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.381238 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.351864 | `azmcp-grafana-list` | ❌ |
| 4 | 0.350951 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.344860 | `azmcp-search-service-list` | ❌ |
| 6 | 0.341813 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.315659 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.308874 | `azmcp-appconfig-account-list` | ❌ |
| 9 | 0.303528 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.297209 | `azmcp-group-list` | ❌ |
| 11 | 0.296282 | `azmcp-monitor-workspace-list` | ❌ |
| 12 | 0.285434 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 13 | 0.275417 | `azmcp-loadtesting-testresource-list` | ❌ |
| 14 | 0.274740 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.274307 | `azmcp-storage-account-list` | ❌ |
| 16 | 0.272700 | `azmcp-marketplace-product-get` | ❌ |
| 17 | 0.258133 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.256330 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.255193 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.230467 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 192

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** What is my current subscription?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.319958 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.298281 | `azmcp-marketplace-product-get` | ❌ |
| 3 | 0.286711 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.285063 | `azmcp-search-service-list` | ❌ |
| 5 | 0.282645 | `azmcp-grafana-list` | ❌ |
| 6 | 0.279702 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.278798 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.256383 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.254815 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 10 | 0.252504 | `azmcp-loadtesting-testresource-list` | ❌ |
| 11 | 0.233143 | `azmcp-monitor-workspace-list` | ❌ |
| 12 | 0.230571 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.230324 | `azmcp-kusto-cluster-get` | ❌ |
| 14 | 0.222799 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.218788 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.216460 | `azmcp-group-list` | ❌ |
| 17 | 0.210573 | `azmcp-storage-account-list` | ❌ |
| 18 | 0.199227 | `azmcp-functionapp-list` | ❌ |
| 19 | 0.185187 | `azmcp-storage-table-list` | ❌ |
| 20 | 0.172388 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 193

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** What subscriptions do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.403229 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.354504 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.342318 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.340339 | `azmcp-grafana-list` | ❌ |
| 5 | 0.336798 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.332531 | `azmcp-search-service-list` | ❌ |
| 7 | 0.304995 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.300478 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 9 | 0.294080 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.291826 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.285657 | `azmcp-marketplace-product-get` | ❌ |
| 12 | 0.282326 | `azmcp-loadtesting-testresource-list` | ❌ |
| 13 | 0.281294 | `azmcp-appconfig-account-list` | ❌ |
| 14 | 0.269869 | `azmcp-group-list` | ❌ |
| 15 | 0.258450 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.257746 | `azmcp-functionapp-list` | ❌ |
| 17 | 0.254491 | `azmcp-storage-account-list` | ❌ |
| 18 | 0.235569 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.228817 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.223476 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 194

**Expected Tool:** `azmcp-azureterraformbestpractices-get`  
**Prompt:** Fetch the Azure Terraform best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.720000 | `azmcp-azureterraformbestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.658343 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.459270 | `azmcp-extension-az` | ❌ |
| 4 | 0.354838 | `azmcp-bicepschema-get` | ❌ |
| 5 | 0.331791 | `azmcp-extension-azd` | ❌ |
| 6 | 0.309265 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.295828 | `azmcp-foundry-models-deployments-list` | ❌ |
| 8 | 0.295781 | `azmcp-extension-azqr` | ❌ |
| 9 | 0.295591 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 10 | 0.293925 | `azmcp-foundry-models-list` | ❌ |
| 11 | 0.291466 | `azmcp-subscription-list` | ❌ |
| 12 | 0.272676 | `azmcp-storage-blob-details` | ❌ |
| 13 | 0.269162 | `azmcp-workbooks-show` | ❌ |
| 14 | 0.267814 | `azmcp-redis-cluster-list` | ❌ |
| 15 | 0.267273 | `azmcp-search-service-list` | ❌ |
| 16 | 0.265091 | `azmcp-workbooks-delete` | ❌ |
| 17 | 0.264875 | `azmcp-redis-cache-list` | ❌ |
| 18 | 0.263617 | `azmcp-monitor-metrics-query` | ❌ |
| 19 | 0.262869 | `azmcp-monitor-resource-log-query` | ❌ |
| 20 | 0.262662 | `azmcp-redis-cache-accesspolicy-list` | ❌ |

---

## Test 195

**Expected Tool:** `azmcp-azureterraformbestpractices-get`  
**Prompt:** Show me the Azure Terraform best practices and generate code sample to get a secret from Azure Key Vault  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.596411 | `azmcp-azureterraformbestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.551618 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.439871 | `azmcp-keyvault-secret-list` | ❌ |
| 4 | 0.439536 | `azmcp-keyvault-secret-create` | ❌ |
| 5 | 0.428888 | `azmcp-keyvault-certificate-get` | ❌ |
| 6 | 0.406432 | `azmcp-extension-az` | ❌ |
| 7 | 0.389424 | `azmcp-keyvault-key-list` | ❌ |
| 8 | 0.380133 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.378655 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.374825 | `azmcp-keyvault-certificate-list` | ❌ |
| 11 | 0.274768 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 12 | 0.274538 | `azmcp-subscription-list` | ❌ |
| 13 | 0.264788 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.264572 | `azmcp-monitor-resource-log-query` | ❌ |
| 15 | 0.253309 | `azmcp-sql-db-show` | ❌ |
| 16 | 0.250062 | `azmcp-storage-table-list` | ❌ |
| 17 | 0.249924 | `azmcp-search-service-list` | ❌ |
| 18 | 0.243929 | `azmcp-storage-blob-container-list` | ❌ |
| 19 | 0.240313 | `azmcp-redis-cache-list` | ❌ |
| 20 | 0.238639 | `azmcp-storage-account-list` | ❌ |

---

## Test 196

**Expected Tool:** `azmcp-virtualdesktop-hostpool-list`  
**Prompt:** List all host pools in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713612 | `azmcp-virtualdesktop-hostpool-list` | ✅ **EXPECTED** |
| 2 | 0.658080 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 3 | 0.566564 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.557529 | `azmcp-search-service-list` | ❌ |
| 5 | 0.538464 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 6 | 0.536542 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.528358 | `azmcp-sql-elastic-pool-list` | ❌ |
| 8 | 0.527948 | `azmcp-postgres-server-list` | ❌ |
| 9 | 0.525912 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.506608 | `azmcp-redis-cache-list` | ❌ |
| 11 | 0.505116 | `azmcp-subscription-list` | ❌ |
| 12 | 0.496297 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.495490 | `azmcp-grafana-list` | ❌ |
| 14 | 0.492515 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.476718 | `azmcp-group-list` | ❌ |
| 16 | 0.474981 | `azmcp-functionapp-list` | ❌ |
| 17 | 0.460388 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.459250 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.456279 | `azmcp-kusto-database-list` | ❌ |
| 20 | 0.431372 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 197

**Expected Tool:** `azmcp-virtualdesktop-hostpool-sessionhost-list`  
**Prompt:** List all session hosts in host pool <hostpool_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.736121 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ✅ **EXPECTED** |
| 2 | 0.714170 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 3 | 0.590258 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 4 | 0.397910 | `azmcp-sql-elastic-pool-list` | ❌ |
| 5 | 0.364696 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.337530 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.335295 | `azmcp-monitor-workspace-list` | ❌ |
| 8 | 0.333524 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.332928 | `azmcp-keyvault-secret-list` | ❌ |
| 10 | 0.330744 | `azmcp-aks-cluster-list` | ❌ |
| 11 | 0.329287 | `azmcp-search-service-list` | ❌ |
| 12 | 0.328664 | `azmcp-keyvault-key-list` | ❌ |
| 13 | 0.321841 | `azmcp-subscription-list` | ❌ |
| 14 | 0.319935 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.312156 | `azmcp-keyvault-certificate-list` | ❌ |
| 16 | 0.311262 | `azmcp-grafana-list` | ❌ |
| 17 | 0.302706 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.291590 | `azmcp-loadtesting-testrun-list` | ❌ |
| 19 | 0.291489 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.289841 | `azmcp-functionapp-list` | ❌ |

---

## Test 198

**Expected Tool:** `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list`  
**Prompt:** List all user sessions on session host <sessionhost_name> in host pool <hostpool_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.812057 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ✅ **EXPECTED** |
| 2 | 0.666731 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 3 | 0.513535 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 4 | 0.336385 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.329666 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.324603 | `azmcp-subscription-list` | ❌ |
| 7 | 0.316500 | `azmcp-loadtesting-testrun-list` | ❌ |
| 8 | 0.316295 | `azmcp-postgres-server-list` | ❌ |
| 9 | 0.305300 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.305072 | `azmcp-aks-cluster-list` | ❌ |
| 11 | 0.304414 | `azmcp-workbooks-list` | ❌ |
| 12 | 0.299973 | `azmcp-keyvault-secret-list` | ❌ |
| 13 | 0.297624 | `azmcp-search-service-list` | ❌ |
| 14 | 0.295899 | `azmcp-grafana-list` | ❌ |
| 15 | 0.278813 | `azmcp-cosmos-account-list` | ❌ |
| 16 | 0.278018 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.276597 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.276398 | `azmcp-kusto-cluster-list` | ❌ |
| 19 | 0.272231 | `azmcp-loadtesting-testrun-get` | ❌ |
| 20 | 0.271027 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 199

**Expected Tool:** `azmcp-workbooks-create`  
**Prompt:** Create a new workbook named <workbook_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.552212 | `azmcp-workbooks-create` | ✅ **EXPECTED** |
| 2 | 0.433162 | `azmcp-workbooks-update` | ❌ |
| 3 | 0.365579 | `azmcp-workbooks-delete` | ❌ |
| 4 | 0.361215 | `azmcp-workbooks-show` | ❌ |
| 5 | 0.328113 | `azmcp-workbooks-list` | ❌ |
| 6 | 0.239813 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.217244 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.213942 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.195157 | `azmcp-storage-account-create` | ❌ |
| 10 | 0.188280 | `azmcp-loadtesting-testresource-create` | ❌ |
| 11 | 0.172751 | `azmcp-monitor-table-list` | ❌ |
| 12 | 0.169440 | `azmcp-grafana-list` | ❌ |
| 13 | 0.148897 | `azmcp-loadtesting-test-create` | ❌ |
| 14 | 0.147365 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.142879 | `azmcp-storage-datalake-directory-create` | ❌ |
| 16 | 0.138518 | `azmcp-monitor-table-type-list` | ❌ |
| 17 | 0.130524 | `azmcp-loadtesting-testrun-create` | ❌ |
| 18 | 0.116803 | `azmcp-loadtesting-testrun-update` | ❌ |
| 19 | 0.111941 | `azmcp-extension-azqr` | ❌ |
| 20 | 0.108926 | `azmcp-appconfig-kv-set` | ❌ |

---

## Test 200

**Expected Tool:** `azmcp-workbooks-delete`  
**Prompt:** Delete the workbook with resource ID <workbook_resource_id>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.624673 | `azmcp-workbooks-delete` | ✅ **EXPECTED** |
| 2 | 0.518630 | `azmcp-workbooks-show` | ❌ |
| 3 | 0.432454 | `azmcp-workbooks-create` | ❌ |
| 4 | 0.425569 | `azmcp-workbooks-list` | ❌ |
| 5 | 0.390355 | `azmcp-workbooks-update` | ❌ |
| 6 | 0.273939 | `azmcp-grafana-list` | ❌ |
| 7 | 0.198585 | `azmcp-appconfig-kv-delete` | ❌ |
| 8 | 0.193231 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.186572 | `azmcp-monitor-workspace-log-query` | ❌ |
| 10 | 0.157219 | `azmcp-monitor-workspace-list` | ❌ |
| 11 | 0.148882 | `azmcp-extension-azqr` | ❌ |
| 12 | 0.146426 | `azmcp-redis-cache-list` | ❌ |
| 13 | 0.145141 | `azmcp-loadtesting-testresource-list` | ❌ |
| 14 | 0.134979 | `azmcp-loadtesting-testrun-update` | ❌ |
| 15 | 0.134817 | `azmcp-redis-cluster-list` | ❌ |
| 16 | 0.132287 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.131813 | `azmcp-group-list` | ❌ |
| 18 | 0.126273 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 19 | 0.122641 | `azmcp-marketplace-product-get` | ❌ |
| 20 | 0.120291 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 201

**Expected Tool:** `azmcp-workbooks-list`  
**Prompt:** List all workbooks in my resource group <resource_group_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.772431 | `azmcp-workbooks-list` | ✅ **EXPECTED** |
| 2 | 0.562485 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.532565 | `azmcp-workbooks-show` | ❌ |
| 4 | 0.516739 | `azmcp-grafana-list` | ❌ |
| 5 | 0.490216 | `azmcp-workbooks-delete` | ❌ |
| 6 | 0.488600 | `azmcp-group-list` | ❌ |
| 7 | 0.459883 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.454210 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.416566 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.413409 | `azmcp-sql-db-list` | ❌ |
| 11 | 0.405963 | `azmcp-loadtesting-testresource-list` | ❌ |
| 12 | 0.405064 | `azmcp-redis-cluster-list` | ❌ |
| 13 | 0.387238 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 14 | 0.382616 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.366266 | `azmcp-functionapp-list` | ❌ |
| 16 | 0.362740 | `azmcp-acr-registry-list` | ❌ |
| 17 | 0.357955 | `azmcp-monitor-metrics-definitions` | ❌ |
| 18 | 0.352940 | `azmcp-cosmos-database-list` | ❌ |
| 19 | 0.349674 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.332561 | `azmcp-kusto-database-list` | ❌ |

---

## Test 202

**Expected Tool:** `azmcp-workbooks-list`  
**Prompt:** What workbooks do I have in resource group <resource_group_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.708612 | `azmcp-workbooks-list` | ✅ **EXPECTED** |
| 2 | 0.570259 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.539957 | `azmcp-workbooks-show` | ❌ |
| 4 | 0.488377 | `azmcp-workbooks-delete` | ❌ |
| 5 | 0.472378 | `azmcp-grafana-list` | ❌ |
| 6 | 0.428025 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.425274 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.421646 | `azmcp-group-list` | ❌ |
| 9 | 0.392371 | `azmcp-loadtesting-testresource-list` | ❌ |
| 10 | 0.371128 | `azmcp-redis-cluster-list` | ❌ |
| 11 | 0.363744 | `azmcp-sql-db-list` | ❌ |
| 12 | 0.362606 | `azmcp-monitor-table-list` | ❌ |
| 13 | 0.358317 | `azmcp-monitor-table-type-list` | ❌ |
| 14 | 0.354258 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 15 | 0.338334 | `azmcp-acr-registry-list` | ❌ |
| 16 | 0.334580 | `azmcp-extension-azqr` | ❌ |
| 17 | 0.327418 | `azmcp-monitor-metrics-definitions` | ❌ |
| 18 | 0.322280 | `azmcp-functionapp-list` | ❌ |
| 19 | 0.313199 | `azmcp-extension-az` | ❌ |
| 20 | 0.299828 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 203

**Expected Tool:** `azmcp-workbooks-show`  
**Prompt:** Get information about the workbook with resource ID <workbook_resource_id>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.697539 | `azmcp-workbooks-show` | ✅ **EXPECTED** |
| 2 | 0.498390 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.494708 | `azmcp-workbooks-list` | ❌ |
| 4 | 0.457252 | `azmcp-workbooks-delete` | ❌ |
| 5 | 0.419105 | `azmcp-workbooks-update` | ❌ |
| 6 | 0.353546 | `azmcp-grafana-list` | ❌ |
| 7 | 0.238836 | `azmcp-monitor-resource-log-query` | ❌ |
| 8 | 0.235453 | `azmcp-marketplace-product-get` | ❌ |
| 9 | 0.227558 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.226385 | `azmcp-loadtesting-test-get` | ❌ |
| 11 | 0.223667 | `azmcp-servicebus-topic-details` | ❌ |
| 12 | 0.219675 | `azmcp-storage-blob-container-details` | ❌ |
| 13 | 0.218999 | `azmcp-loadtesting-testresource-list` | ❌ |
| 14 | 0.210588 | `azmcp-monitor-metrics-definitions` | ❌ |
| 15 | 0.208401 | `azmcp-storage-blob-details` | ❌ |
| 16 | 0.207388 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.195751 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 18 | 0.195373 | `azmcp-group-list` | ❌ |
| 19 | 0.194010 | `azmcp-loadtesting-testrun-get` | ❌ |
| 20 | 0.189657 | `azmcp-extension-azqr` | ❌ |

---

## Test 204

**Expected Tool:** `azmcp-workbooks-show`  
**Prompt:** Show me the workbook with display name <workbook_display_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.469476 | `azmcp-workbooks-show` | ✅ **EXPECTED** |
| 2 | 0.455158 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.437638 | `azmcp-workbooks-update` | ❌ |
| 4 | 0.424338 | `azmcp-workbooks-list` | ❌ |
| 5 | 0.371623 | `azmcp-workbooks-delete` | ❌ |
| 6 | 0.292898 | `azmcp-grafana-list` | ❌ |
| 7 | 0.266530 | `azmcp-monitor-table-list` | ❌ |
| 8 | 0.239907 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.227383 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.176481 | `azmcp-role-assignment-list` | ❌ |
| 11 | 0.175814 | `azmcp-appconfig-kv-show` | ❌ |
| 12 | 0.174513 | `azmcp-loadtesting-testrun-update` | ❌ |
| 13 | 0.174123 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.165774 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.154760 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.149678 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.146054 | `azmcp-kusto-table-schema` | ❌ |
| 18 | 0.143754 | `azmcp-loadtesting-testrun-get` | ❌ |
| 19 | 0.141559 | `azmcp-foundry-models-list` | ❌ |
| 20 | 0.138897 | `azmcp-loadtesting-testrun-list` | ❌ |

---

## Test 205

**Expected Tool:** `azmcp-workbooks-update`  
**Prompt:** Update the workbook <workbook_resource_id> with a new text step  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.469915 | `azmcp-workbooks-update` | ✅ **EXPECTED** |
| 2 | 0.382651 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.362354 | `azmcp-workbooks-show` | ❌ |
| 4 | 0.351698 | `azmcp-workbooks-delete` | ❌ |
| 5 | 0.276727 | `azmcp-loadtesting-testrun-update` | ❌ |
| 6 | 0.262873 | `azmcp-workbooks-list` | ❌ |
| 7 | 0.170118 | `azmcp-grafana-list` | ❌ |
| 8 | 0.145788 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 9 | 0.144812 | `azmcp-extension-az` | ❌ |
| 10 | 0.142186 | `azmcp-loadtesting-testrun-create` | ❌ |
| 11 | 0.138354 | `azmcp-appconfig-kv-set` | ❌ |
| 12 | 0.136366 | `azmcp-loadtesting-testresource-create` | ❌ |
| 13 | 0.131007 | `azmcp-postgres-database-query` | ❌ |
| 14 | 0.129973 | `azmcp-postgres-server-param-set` | ❌ |
| 15 | 0.124925 | `azmcp-appconfig-kv-unlock` | ❌ |
| 16 | 0.121289 | `azmcp-monitor-workspace-log-query` | ❌ |
| 17 | 0.115996 | `azmcp-appconfig-kv-lock` | ❌ |
| 18 | 0.105705 | `azmcp-extension-azqr` | ❌ |
| 19 | 0.100393 | `azmcp-monitor-resource-log-query` | ❌ |
| 20 | 0.098382 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 206

**Expected Tool:** `azmcp-bicepschema-get`  
**Prompt:** How can I use Bicep to create an Azure OpenAI service?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.432409 | `azmcp-bicepschema-get` | ✅ **EXPECTED** |
| 2 | 0.401162 | `azmcp-extension-az` | ❌ |
| 3 | 0.400985 | `azmcp-foundry-models-deploy` | ❌ |
| 4 | 0.394677 | `azmcp-bestpractices-get` | ❌ |
| 5 | 0.375221 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 6 | 0.363171 | `azmcp-search-index-list` | ❌ |
| 7 | 0.345030 | `azmcp-search-service-list` | ❌ |
| 8 | 0.342237 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.335700 | `azmcp-search-index-query` | ❌ |
| 10 | 0.320686 | `azmcp-extension-azd` | ❌ |
| 11 | 0.303518 | `azmcp-search-index-describe` | ❌ |
| 12 | 0.300402 | `azmcp-loadtesting-testresource-create` | ❌ |
| 13 | 0.300217 | `azmcp-loadtesting-test-create` | ❌ |
| 14 | 0.293981 | `azmcp-extension-azqr` | ❌ |
| 15 | 0.286560 | `azmcp-storage-account-create` | ❌ |
| 16 | 0.280207 | `azmcp-workbooks-delete` | ❌ |
| 17 | 0.268139 | `azmcp-workbooks-create` | ❌ |
| 18 | 0.263814 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 19 | 0.252310 | `azmcp-monitor-resource-log-query` | ❌ |
| 20 | 0.248290 | `azmcp-storage-blob-details` | ❌ |

---

## Summary

**Total Prompts Tested:** 206  
**Execution Time:** 33.1829746s  

### Success Rate Metrics

**Top Choice Success:** 85.4% (176/206 tests)  

#### Confidence Level Distribution

**💪 Very High Confidence (≥0.8):** 6.3% (13/206 tests)  
**🎯 High Confidence (≥0.7):** 28.6% (59/206 tests)  
**✅ Good Confidence (≥0.6):** 59.7% (123/206 tests)  
**👍 Fair Confidence (≥0.5):** 83.5% (172/206 tests)  
**👌 Acceptable Confidence (≥0.4):** 93.2% (192/206 tests)  
**❌ Low Confidence (<0.4):** 6.8% (14/206 tests)  

#### Top Choice + Confidence Combinations

**💪 Top Choice + Very High Confidence (≥0.8):** 6.3% (13/206 tests)  
**🎯 Top Choice + High Confidence (≥0.7):** 28.6% (59/206 tests)  
**✅ Top Choice + Good Confidence (≥0.6):** 58.7% (121/206 tests)  
**👍 Top Choice + Fair Confidence (≥0.5):** 77.2% (159/206 tests)  
**👌 Top Choice + Acceptable Confidence (≥0.4):** 83.5% (172/206 tests)  

### Success Rate Analysis

🟡 **Good** - The tool selection system is performing adequately but has room for improvement.

⚠️ **Recommendation:** Tool descriptions need improvement to better match user intent (targets: ≥0.6 good, ≥0.7 high).

