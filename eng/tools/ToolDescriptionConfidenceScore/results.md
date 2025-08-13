# Tool Selection Analysis Setup

**Setup completed:** 2025-08-12 11:57:02  
**Tool count:** 100  
**Database setup time:** 2.6328087s  

---

# Tool Selection Analysis Results

**Analysis Date:** 2025-08-12 11:57:02  
**Tool count:** 100  

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
- [Test 26: azmcp-cosmos-account-list](#test-26)
- [Test 27: azmcp-cosmos-account-list](#test-27)
- [Test 28: azmcp-cosmos-account-list](#test-28)
- [Test 29: azmcp-cosmos-database-container-item-query](#test-29)
- [Test 30: azmcp-cosmos-database-container-list](#test-30)
- [Test 31: azmcp-cosmos-database-container-list](#test-31)
- [Test 32: azmcp-cosmos-database-list](#test-32)
- [Test 33: azmcp-cosmos-database-list](#test-33)
- [Test 34: azmcp-kusto-cluster-get](#test-34)
- [Test 35: azmcp-kusto-cluster-list](#test-35)
- [Test 36: azmcp-kusto-cluster-list](#test-36)
- [Test 37: azmcp-kusto-cluster-list](#test-37)
- [Test 38: azmcp-kusto-database-list](#test-38)
- [Test 39: azmcp-kusto-database-list](#test-39)
- [Test 40: azmcp-kusto-query](#test-40)
- [Test 41: azmcp-kusto-sample](#test-41)
- [Test 42: azmcp-kusto-table-list](#test-42)
- [Test 43: azmcp-kusto-table-list](#test-43)
- [Test 44: azmcp-kusto-table-schema](#test-44)
- [Test 45: azmcp-postgres-database-list](#test-45)
- [Test 46: azmcp-postgres-database-list](#test-46)
- [Test 47: azmcp-postgres-database-query](#test-47)
- [Test 48: azmcp-postgres-server-config-get](#test-48)
- [Test 49: azmcp-postgres-server-list](#test-49)
- [Test 50: azmcp-postgres-server-list](#test-50)
- [Test 51: azmcp-postgres-server-list](#test-51)
- [Test 52: azmcp-postgres-server-param](#test-52)
- [Test 53: azmcp-postgres-server-param-set](#test-53)
- [Test 54: azmcp-postgres-table-list](#test-54)
- [Test 55: azmcp-postgres-table-list](#test-55)
- [Test 56: azmcp-postgres-table-schema-get](#test-56)
- [Test 57: azmcp-extension-azd](#test-57)
- [Test 58: azmcp-extension-azd](#test-58)
- [Test 59: azmcp-keyvault-certificate-create](#test-59)
- [Test 60: azmcp-keyvault-certificate-get](#test-60)
- [Test 61: azmcp-keyvault-certificate-get](#test-61)
- [Test 62: azmcp-keyvault-certificate-list](#test-62)
- [Test 63: azmcp-keyvault-certificate-list](#test-63)
- [Test 64: azmcp-keyvault-key-create](#test-64)
- [Test 65: azmcp-keyvault-key-list](#test-65)
- [Test 66: azmcp-keyvault-key-list](#test-66)
- [Test 67: azmcp-keyvault-secret-create](#test-67)
- [Test 68: azmcp-keyvault-secret-list](#test-68)
- [Test 69: azmcp-keyvault-secret-list](#test-69)
- [Test 70: azmcp-aks-cluster-get](#test-70)
- [Test 71: azmcp-aks-cluster-get](#test-71)
- [Test 72: azmcp-aks-cluster-get](#test-72)
- [Test 73: azmcp-aks-cluster-get](#test-73)
- [Test 74: azmcp-aks-cluster-list](#test-74)
- [Test 75: azmcp-aks-cluster-list](#test-75)
- [Test 76: azmcp-aks-cluster-list](#test-76)
- [Test 77: azmcp-loadtesting-test-create](#test-77)
- [Test 78: azmcp-loadtesting-test-get](#test-78)
- [Test 79: azmcp-loadtesting-testresource-create](#test-79)
- [Test 80: azmcp-loadtesting-testresource-list](#test-80)
- [Test 81: azmcp-loadtesting-testrun-create](#test-81)
- [Test 82: azmcp-loadtesting-testrun-get](#test-82)
- [Test 83: azmcp-loadtesting-testrun-list](#test-83)
- [Test 84: azmcp-loadtesting-testrun-update](#test-84)
- [Test 85: azmcp-grafana-list](#test-85)
- [Test 86: azmcp-marketplace-product-get](#test-86)
- [Test 87: azmcp-bestpractices-get](#test-87)
- [Test 88: azmcp-bestpractices-get](#test-88)
- [Test 89: azmcp-bestpractices-get](#test-89)
- [Test 90: azmcp-bestpractices-get](#test-90)
- [Test 91: azmcp-bestpractices-get](#test-91)
- [Test 92: azmcp-bestpractices-get](#test-92)
- [Test 93: azmcp-bestpractices-get](#test-93)
- [Test 94: azmcp-monitor-healthmodels-entity-gethealth](#test-94)
- [Test 95: azmcp-monitor-metrics-definitions](#test-95)
- [Test 96: azmcp-monitor-metrics-definitions](#test-96)
- [Test 97: azmcp-monitor-metrics-definitions](#test-97)
- [Test 98: azmcp-monitor-metrics-query](#test-98)
- [Test 99: azmcp-monitor-metrics-query](#test-99)
- [Test 100: azmcp-monitor-metrics-query](#test-100)
- [Test 101: azmcp-monitor-metrics-query](#test-101)
- [Test 102: azmcp-monitor-metrics-query](#test-102)
- [Test 103: azmcp-monitor-metrics-query](#test-103)
- [Test 104: azmcp-monitor-resource-log-query](#test-104)
- [Test 105: azmcp-monitor-table-list](#test-105)
- [Test 106: azmcp-monitor-table-list](#test-106)
- [Test 107: azmcp-monitor-table-type-list](#test-107)
- [Test 108: azmcp-monitor-table-type-list](#test-108)
- [Test 109: azmcp-monitor-workspace-list](#test-109)
- [Test 110: azmcp-monitor-workspace-list](#test-110)
- [Test 111: azmcp-monitor-workspace-list](#test-111)
- [Test 112: azmcp-monitor-workspace-log-query](#test-112)
- [Test 113: azmcp-datadog-monitoredresources-list](#test-113)
- [Test 114: azmcp-datadog-monitoredresources-list](#test-114)
- [Test 115: azmcp-extension-azqr](#test-115)
- [Test 116: azmcp-extension-azqr](#test-116)
- [Test 117: azmcp-extension-azqr](#test-117)
- [Test 118: azmcp-role-assignment-list](#test-118)
- [Test 119: azmcp-role-assignment-list](#test-119)
- [Test 120: azmcp-redis-cache-accesspolicy-list](#test-120)
- [Test 121: azmcp-redis-cache-accesspolicy-list](#test-121)
- [Test 122: azmcp-redis-cache-list](#test-122)
- [Test 123: azmcp-redis-cache-list](#test-123)
- [Test 124: azmcp-redis-cache-list](#test-124)
- [Test 125: azmcp-redis-cluster-database-list](#test-125)
- [Test 126: azmcp-redis-cluster-database-list](#test-126)
- [Test 127: azmcp-redis-cluster-list](#test-127)
- [Test 128: azmcp-redis-cluster-list](#test-128)
- [Test 129: azmcp-redis-cluster-list](#test-129)
- [Test 130: azmcp-group-list](#test-130)
- [Test 131: azmcp-group-list](#test-131)
- [Test 132: azmcp-group-list](#test-132)
- [Test 133: azmcp-servicebus-queue-details](#test-133)
- [Test 134: azmcp-servicebus-topic-details](#test-134)
- [Test 135: azmcp-servicebus-topic-subscription-details](#test-135)
- [Test 136: azmcp-sql-db-list](#test-136)
- [Test 137: azmcp-sql-db-list](#test-137)
- [Test 138: azmcp-sql-db-show](#test-138)
- [Test 139: azmcp-sql-db-show](#test-139)
- [Test 140: azmcp-sql-elastic-pool-list](#test-140)
- [Test 141: azmcp-sql-elastic-pool-list](#test-141)
- [Test 142: azmcp-sql-elastic-pool-list](#test-142)
- [Test 143: azmcp-sql-server-entra-admin-list](#test-143)
- [Test 144: azmcp-sql-server-entra-admin-list](#test-144)
- [Test 145: azmcp-sql-server-entra-admin-list](#test-145)
- [Test 146: azmcp-sql-server-firewall-rule-list](#test-146)
- [Test 147: azmcp-sql-server-firewall-rule-list](#test-147)
- [Test 148: azmcp-sql-server-firewall-rule-list](#test-148)
- [Test 149: azmcp-storage-account-create](#test-149)
- [Test 150: azmcp-storage-account-create](#test-150)
- [Test 151: azmcp-storage-account-create](#test-151)
- [Test 152: azmcp-storage-account-list](#test-152)
- [Test 153: azmcp-storage-account-list](#test-153)
- [Test 154: azmcp-storage-account-list](#test-154)
- [Test 155: azmcp-storage-blob-batch-set-tier](#test-155)
- [Test 156: azmcp-storage-blob-batch-set-tier](#test-156)
- [Test 157: azmcp-storage-blob-container-create](#test-157)
- [Test 158: azmcp-storage-blob-container-create](#test-158)
- [Test 159: azmcp-storage-blob-container-create](#test-159)
- [Test 160: azmcp-storage-blob-container-details](#test-160)
- [Test 161: azmcp-storage-blob-container-list](#test-161)
- [Test 162: azmcp-storage-blob-container-list](#test-162)
- [Test 163: azmcp-storage-blob-details](#test-163)
- [Test 164: azmcp-storage-blob-details](#test-164)
- [Test 165: azmcp-storage-blob-list](#test-165)
- [Test 166: azmcp-storage-blob-list](#test-166)
- [Test 167: azmcp-storage-datalake-directory-create](#test-167)
- [Test 168: azmcp-storage-datalake-file-system-list-paths](#test-168)
- [Test 169: azmcp-storage-datalake-file-system-list-paths](#test-169)
- [Test 170: azmcp-storage-datalake-file-system-list-paths](#test-170)
- [Test 171: azmcp-storage-queue-message-send](#test-171)
- [Test 172: azmcp-storage-queue-message-send](#test-172)
- [Test 173: azmcp-storage-queue-message-send](#test-173)
- [Test 174: azmcp-storage-share-file-list](#test-174)
- [Test 175: azmcp-storage-share-file-list](#test-175)
- [Test 176: azmcp-storage-share-file-list](#test-176)
- [Test 177: azmcp-storage-table-list](#test-177)
- [Test 178: azmcp-storage-table-list](#test-178)
- [Test 179: azmcp-subscription-list](#test-179)
- [Test 180: azmcp-subscription-list](#test-180)
- [Test 181: azmcp-subscription-list](#test-181)
- [Test 182: azmcp-subscription-list](#test-182)
- [Test 183: azmcp-azureterraformbestpractices-get](#test-183)
- [Test 184: azmcp-azureterraformbestpractices-get](#test-184)
- [Test 185: azmcp-virtualdesktop-hostpool-list](#test-185)
- [Test 186: azmcp-virtualdesktop-hostpool-sessionhost-list](#test-186)
- [Test 187: azmcp-virtualdesktop-hostpool-sessionhost-usersession-list](#test-187)
- [Test 188: azmcp-workbooks-create](#test-188)
- [Test 189: azmcp-workbooks-delete](#test-189)
- [Test 190: azmcp-workbooks-list](#test-190)
- [Test 191: azmcp-workbooks-list](#test-191)
- [Test 192: azmcp-workbooks-show](#test-192)
- [Test 193: azmcp-workbooks-show](#test-193)
- [Test 194: azmcp-workbooks-update](#test-194)
- [Test 195: azmcp-bicepschema-get](#test-195)

---

## Test 1

**Expected Tool:** `azmcp-foundry-models-deploy`  
**Prompt:** Deploy a GPT4o instance on my resource <resource-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.313400 | `azmcp-foundry-models-deploy` | ✅ **EXPECTED** |
| 2 | 0.269513 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.222504 | `azmcp-grafana-list` | ❌ |
| 4 | 0.219588 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.218848 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 6 | 0.218252 | `azmcp-workbooks-create` | ❌ |
| 7 | 0.215098 | `azmcp-monitor-resource-log-query` | ❌ |
| 8 | 0.211671 | `azmcp-loadtesting-testrun-create` | ❌ |
| 9 | 0.207673 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.206600 | `azmcp-bestpractices-get` | ❌ |

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
| 9 | 0.302262 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.301302 | `azmcp-redis-cluster-list` | ❌ |

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
| 5 | 0.337040 | `azmcp-search-index-list` | ❌ |
| 6 | 0.286814 | `azmcp-grafana-list` | ❌ |
| 7 | 0.281406 | `azmcp-cloudarchitect-design` | ❌ |
| 8 | 0.265906 | `azmcp-extension-azd` | ❌ |
| 9 | 0.259989 | `azmcp-loadtesting-testrun-list` | ❌ |
| 10 | 0.254926 | `azmcp-postgres-server-list` | ❌ |

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
| 5 | 0.337382 | `azmcp-search-index-list` | ❌ |
| 6 | 0.298648 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.285437 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.277883 | `azmcp-grafana-list` | ❌ |
| 9 | 0.273026 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.252297 | `azmcp-postgres-database-list` | ❌ |

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
| 5 | 0.287983 | `azmcp-search-index-list` | ❌ |
| 6 | 0.267262 | `azmcp-cloudarchitect-design` | ❌ |
| 7 | 0.244697 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.233079 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 9 | 0.231148 | `azmcp-grafana-list` | ❌ |
| 10 | 0.216807 | `azmcp-extension-azd` | ❌ |

---

## Test 6

**Expected Tool:** `azmcp-search-index-describe`  
**Prompt:** Show me the details of the index <index-name> in Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.618172 | `azmcp-search-index-list` | ❌ |
| 2 | 0.597678 | `azmcp-search-index-describe` | ✅ **EXPECTED** |
| 3 | 0.465274 | `azmcp-search-index-query` | ❌ |
| 4 | 0.436730 | `azmcp-search-service-list` | ❌ |
| 5 | 0.393556 | `azmcp-aks-cluster-get` | ❌ |
| 6 | 0.389306 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.358315 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.356252 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.330038 | `azmcp-kusto-table-schema` | ❌ |
| 10 | 0.327156 | `azmcp-workbooks-show` | ❌ |

---

## Test 7

**Expected Tool:** `azmcp-search-index-list`  
**Prompt:** List all indexes in the Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.796567 | `azmcp-search-index-list` | ✅ **EXPECTED** |
| 2 | 0.561102 | `azmcp-search-service-list` | ❌ |
| 3 | 0.518943 | `azmcp-search-index-describe` | ❌ |
| 4 | 0.455689 | `azmcp-search-index-query` | ❌ |
| 5 | 0.439452 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.416403 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.409325 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.406485 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.392617 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.382791 | `azmcp-keyvault-key-list` | ❌ |

---

## Test 8

**Expected Tool:** `azmcp-search-index-list`  
**Prompt:** Show me the indexes in the Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.750014 | `azmcp-search-index-list` | ✅ **EXPECTED** |
| 2 | 0.512453 | `azmcp-search-index-describe` | ❌ |
| 3 | 0.497599 | `azmcp-search-service-list` | ❌ |
| 4 | 0.443812 | `azmcp-search-index-query` | ❌ |
| 5 | 0.401807 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.382692 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.372670 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.370328 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.354034 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.351788 | `azmcp-foundry-models-deployments-list` | ❌ |

---

## Test 9

**Expected Tool:** `azmcp-search-index-query`  
**Prompt:** Search for instances of <search_term> in the index <index-name> in Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.552955 | `azmcp-search-index-list` | ❌ |
| 2 | 0.522558 | `azmcp-search-index-query` | ✅ **EXPECTED** |
| 3 | 0.492637 | `azmcp-search-index-describe` | ❌ |
| 4 | 0.463356 | `azmcp-search-service-list` | ❌ |
| 5 | 0.328835 | `azmcp-kusto-query` | ❌ |
| 6 | 0.322009 | `azmcp-monitor-workspace-log-query` | ❌ |
| 7 | 0.311044 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 8 | 0.298026 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.288242 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.283532 | `azmcp-foundry-models-list` | ❌ |

---

## Test 10

**Expected Tool:** `azmcp-search-service-list`  
**Prompt:** List all Cognitive Search services in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.745450 | `azmcp-search-service-list` | ✅ **EXPECTED** |
| 2 | 0.608202 | `azmcp-search-index-list` | ❌ |
| 3 | 0.500455 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.494272 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.493011 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.492239 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.486066 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.482464 | `azmcp-grafana-list` | ❌ |
| 9 | 0.477570 | `azmcp-subscription-list` | ❌ |
| 10 | 0.470384 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 11

**Expected Tool:** `azmcp-search-service-list`  
**Prompt:** Show me the Cognitive Search services in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.644213 | `azmcp-search-service-list` | ✅ **EXPECTED** |
| 2 | 0.525325 | `azmcp-search-index-list` | ❌ |
| 3 | 0.425939 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.412173 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.408456 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.400229 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.399822 | `azmcp-grafana-list` | ❌ |
| 8 | 0.397883 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.393795 | `azmcp-subscription-list` | ❌ |
| 10 | 0.390559 | `azmcp-foundry-models-list` | ❌ |

---

## Test 12

**Expected Tool:** `azmcp-search-service-list`  
**Prompt:** Show me my Cognitive Search services  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.488103 | `azmcp-search-index-list` | ❌ |
| 2 | 0.482308 | `azmcp-search-service-list` | ✅ **EXPECTED** |
| 3 | 0.351773 | `azmcp-search-index-describe` | ❌ |
| 4 | 0.344699 | `azmcp-foundry-models-deployments-list` | ❌ |
| 5 | 0.329777 | `azmcp-search-index-query` | ❌ |
| 6 | 0.322540 | `azmcp-foundry-models-list` | ❌ |
| 7 | 0.316685 | `azmcp-cloudarchitect-design` | ❌ |
| 8 | 0.290209 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.283366 | `azmcp-redis-cluster-list` | ❌ |
| 10 | 0.281134 | `azmcp-monitor-workspace-list` | ❌ |

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
| 6 | 0.459339 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.442214 | `azmcp-grafana-list` | ❌ |
| 8 | 0.441691 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.429305 | `azmcp-search-service-list` | ❌ |
| 10 | 0.427591 | `azmcp-subscription-list` | ❌ |

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
| 6 | 0.359567 | `azmcp-postgres-server-config` | ❌ |
| 7 | 0.356514 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.354747 | `azmcp-appconfig-kv-delete` | ❌ |
| 9 | 0.348603 | `azmcp-appconfig-kv-set` | ❌ |
| 10 | 0.341263 | `azmcp-grafana-list` | ❌ |

---

## Test 15

**Expected Tool:** `azmcp-appconfig-account-list`  
**Prompt:** Show me my App Configuration stores  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.565435 | `azmcp-appconfig-account-list` | ✅ **EXPECTED** |
| 2 | 0.564705 | `azmcp-appconfig-kv-list` | ❌ |
| 3 | 0.414689 | `azmcp-appconfig-kv-show` | ❌ |
| 4 | 0.355916 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.348661 | `azmcp-appconfig-kv-delete` | ❌ |
| 6 | 0.327234 | `azmcp-appconfig-kv-set` | ❌ |
| 7 | 0.308131 | `azmcp-appconfig-kv-unlock` | ❌ |
| 8 | 0.302405 | `azmcp-appconfig-kv-lock` | ❌ |
| 9 | 0.244080 | `azmcp-loadtesting-testrun-list` | ❌ |
| 10 | 0.237881 | `azmcp-loadtesting-test-get` | ❌ |

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
| 9 | 0.262124 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.248752 | `azmcp-keyvault-key-list` | ❌ |

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
| 8 | 0.377534 | `azmcp-postgres-server-config` | ❌ |
| 9 | 0.374460 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.338195 | `azmcp-keyvault-secret-list` | ❌ |

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
| 8 | 0.370520 | `azmcp-postgres-server-config` | ❌ |
| 9 | 0.316879 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.294807 | `azmcp-keyvault-key-list` | ❌ |

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
| 8 | 0.251272 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.238544 | `azmcp-keyvault-secret-create` | ❌ |
| 10 | 0.238242 | `azmcp-postgres-server-setparam` | ❌ |

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
| 8 | 0.346927 | `azmcp-postgres-server-setparam` | ❌ |
| 9 | 0.311433 | `azmcp-keyvault-secret-create` | ❌ |
| 10 | 0.305956 | `azmcp-keyvault-key-create` | ❌ |

---

## Test 21

**Expected Tool:** `azmcp-appconfig-kv-show`  
**Prompt:** Show the content for the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.603216 | `azmcp-appconfig-kv-list` | ❌ |
| 2 | 0.561508 | `azmcp-appconfig-kv-show` | ✅ **EXPECTED** |
| 3 | 0.448912 | `azmcp-appconfig-kv-set` | ❌ |
| 4 | 0.441713 | `azmcp-appconfig-kv-delete` | ❌ |
| 5 | 0.437432 | `azmcp-appconfig-account-list` | ❌ |
| 6 | 0.433846 | `azmcp-appconfig-kv-lock` | ❌ |
| 7 | 0.427588 | `azmcp-appconfig-kv-unlock` | ❌ |
| 8 | 0.301859 | `azmcp-keyvault-key-list` | ❌ |
| 9 | 0.291448 | `azmcp-postgres-server-config` | ❌ |
| 10 | 0.276985 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 22

**Expected Tool:** `azmcp-appconfig-kv-unlock`  
**Prompt:** Unlock the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.603597 | `azmcp-appconfig-kv-unlock` | ✅ **EXPECTED** |
| 2 | 0.552244 | `azmcp-appconfig-kv-lock` | ❌ |
| 3 | 0.541557 | `azmcp-appconfig-kv-list` | ❌ |
| 4 | 0.476496 | `azmcp-appconfig-kv-delete` | ❌ |
| 5 | 0.435759 | `azmcp-appconfig-kv-show` | ❌ |
| 6 | 0.425488 | `azmcp-appconfig-kv-set` | ❌ |
| 7 | 0.409406 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.268059 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.259561 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.252818 | `azmcp-keyvault-secret-create` | ❌ |

---

## Test 23

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** Create a Storage account with name <storage_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.472322 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.455881 | `azmcp-storage-account-list` | ❌ |
| 3 | 0.429692 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.403075 | `azmcp-keyvault-secret-create` | ❌ |
| 5 | 0.396132 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.386753 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.376319 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.374470 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.352805 | `azmcp-appconfig-kv-set` | ❌ |
| 10 | 0.337708 | `azmcp-storage-datalake-directory-create` | ❌ |

---

## Test 24

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** List all virtual machines in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.577373 | `azmcp-search-service-list` | ❌ |
| 2 | 0.531648 | `azmcp-subscription-list` | ❌ |
| 3 | 0.530948 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.500615 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.499251 | `azmcp-kusto-cluster-list` | ❌ |
| 6 | 0.496186 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.491307 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 8 | 0.484074 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.482576 | `azmcp-grafana-list` | ❌ |
| 10 | 0.477657 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 25

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** Show me the details of the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.565925 | `azmcp-storage-blob-container-details` | ❌ |
| 2 | 0.559878 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.516957 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.509799 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.495892 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.433767 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.433255 | `azmcp-appconfig-kv-show` | ❌ |
| 8 | 0.417590 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.371852 | `azmcp-sql-db-show` | ❌ |
| 10 | 0.367600 | `azmcp-aks-cluster-get` | ❌ |

---

## Test 26

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** List all cosmosdb accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.818327 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.668478 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.628122 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.615268 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.588622 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.587706 | `azmcp-subscription-list` | ❌ |
| 7 | 0.557870 | `azmcp-search-service-list` | ❌ |
| 8 | 0.530615 | `azmcp-storage-blob-container-list` | ❌ |
| 9 | 0.528963 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.516914 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 27

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** Show me my cosmosdb accounts  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.665401 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.605355 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.571613 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.473443 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.467675 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.452019 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.436362 | `azmcp-subscription-list` | ❌ |
| 8 | 0.431496 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 9 | 0.407809 | `azmcp-storage-blob-list` | ❌ |
| 10 | 0.390248 | `azmcp-kusto-database-list` | ❌ |

---

## Test 28

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** Show me the cosmosdb accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.752468 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.605120 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.566249 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.558106 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.546326 | `azmcp-subscription-list` | ❌ |
| 6 | 0.535145 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.513709 | `azmcp-search-service-list` | ❌ |
| 8 | 0.488006 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.483663 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.466414 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 29

**Expected Tool:** `azmcp-cosmos-database-container-item-query`  
**Prompt:** Show me the items that contain the word <search_term> in the container <container_name> in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.605253 | `azmcp-cosmos-database-container-list` | ❌ |
| 2 | 0.566854 | `azmcp-cosmos-database-container-item-query` | ✅ **EXPECTED** |
| 3 | 0.477866 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.469254 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.447732 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.417506 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.408746 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.398979 | `azmcp-search-service-list` | ❌ |
| 9 | 0.386170 | `azmcp-search-index-list` | ❌ |
| 10 | 0.384430 | `azmcp-storage-table-list` | ❌ |

---

## Test 30

**Expected Tool:** `azmcp-cosmos-database-container-list`  
**Prompt:** List all the containers in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.852832 | `azmcp-cosmos-database-container-list` | ✅ **EXPECTED** |
| 2 | 0.690115 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.681040 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.630611 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.561245 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.535277 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.527459 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 8 | 0.473467 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.460959 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.449012 | `azmcp-kusto-database-list` | ❌ |

---

## Test 31

**Expected Tool:** `azmcp-cosmos-database-container-list`  
**Prompt:** Show me the containers in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.789395 | `azmcp-cosmos-database-container-list` | ✅ **EXPECTED** |
| 2 | 0.614213 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.611343 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.562020 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.521532 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 6 | 0.474816 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.471009 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.449542 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.398139 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.397755 | `azmcp-sql-db-list` | ❌ |

---

## Test 32

**Expected Tool:** `azmcp-cosmos-database-list`  
**Prompt:** List all the databases in the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.815680 | `azmcp-cosmos-database-list` | ✅ **EXPECTED** |
| 2 | 0.668494 | `azmcp-cosmos-account-list` | ❌ |
| 3 | 0.665298 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.571422 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.555237 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.548066 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.526046 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.501477 | `azmcp-postgres-database-list` | ❌ |
| 9 | 0.500269 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.471381 | `azmcp-kusto-table-list` | ❌ |

---

## Test 33

**Expected Tool:** `azmcp-cosmos-database-list`  
**Prompt:** Show me the databases in the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.749363 | `azmcp-cosmos-database-list` | ✅ **EXPECTED** |
| 2 | 0.624759 | `azmcp-cosmos-database-container-list` | ❌ |
| 3 | 0.614558 | `azmcp-cosmos-account-list` | ❌ |
| 4 | 0.524965 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.505419 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.498206 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.497414 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.456237 | `azmcp-storage-blob-container-list` | ❌ |
| 9 | 0.449759 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.447875 | `azmcp-postgres-database-list` | ❌ |

---

## Test 34

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
| 6 | 0.362958 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.344871 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.344591 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.332639 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.326472 | `azmcp-redis-cache-list` | ❌ |

---

## Test 35

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** List all Data Explorer clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651218 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.644037 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.549121 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.536049 | `azmcp-aks-cluster-list` | ❌ |
| 5 | 0.509396 | `azmcp-grafana-list` | ❌ |
| 6 | 0.505912 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.492107 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.487882 | `azmcp-search-service-list` | ❌ |
| 9 | 0.487583 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.486159 | `azmcp-kusto-cluster-get` | ❌ |

---

## Test 36

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** Show me my Data Explorer clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.437363 | `azmcp-redis-cluster-list` | ❌ |
| 2 | 0.391087 | `azmcp-redis-cluster-database-list` | ❌ |
| 3 | 0.386126 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 4 | 0.359576 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.341784 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.338217 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.314734 | `azmcp-aks-cluster-get` | ❌ |
| 8 | 0.303083 | `azmcp-grafana-list` | ❌ |
| 9 | 0.292838 | `azmcp-redis-cache-list` | ❌ |
| 10 | 0.288982 | `azmcp-kusto-sample` | ❌ |

---

## Test 37

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** Show me the Data Explorer clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584053 | `azmcp-redis-cluster-list` | ❌ |
| 2 | 0.549797 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 3 | 0.471120 | `azmcp-aks-cluster-list` | ❌ |
| 4 | 0.469570 | `azmcp-kusto-cluster-get` | ❌ |
| 5 | 0.464345 | `azmcp-kusto-database-list` | ❌ |
| 6 | 0.462945 | `azmcp-grafana-list` | ❌ |
| 7 | 0.446124 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.440326 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.432048 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.421307 | `azmcp-search-service-list` | ❌ |

---

## Test 38

**Expected Tool:** `azmcp-kusto-database-list`  
**Prompt:** List all databases in the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628129 | `azmcp-redis-cluster-database-list` | ❌ |
| 2 | 0.610653 | `azmcp-kusto-database-list` | ✅ **EXPECTED** |
| 3 | 0.553218 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.549673 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.474361 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.461496 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.459180 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.434330 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.431669 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.404095 | `azmcp-monitor-table-list` | ❌ |

---

## Test 39

**Expected Tool:** `azmcp-kusto-database-list`  
**Prompt:** Show me the databases in the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.597975 | `azmcp-redis-cluster-database-list` | ❌ |
| 2 | 0.558546 | `azmcp-kusto-database-list` | ✅ **EXPECTED** |
| 3 | 0.497143 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.486732 | `azmcp-postgres-database-list` | ❌ |
| 5 | 0.440079 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.427251 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.422588 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.383664 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.368013 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.362905 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 40

**Expected Tool:** `azmcp-kusto-query`  
**Prompt:** Show me all items that contain the word <search_term> in the Data Explorer table <table_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.382652 | `azmcp-kusto-query` | ✅ **EXPECTED** |
| 2 | 0.363120 | `azmcp-kusto-sample` | ❌ |
| 3 | 0.349147 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.345799 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.334797 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.319112 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.318883 | `azmcp-kusto-table-schema` | ❌ |
| 8 | 0.314961 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.308081 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.304014 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 41

**Expected Tool:** `azmcp-kusto-sample`  
**Prompt:** Show me a data sample from the Data Explorer table <table_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.538483 | `azmcp-kusto-sample` | ✅ **EXPECTED** |
| 2 | 0.419463 | `azmcp-kusto-table-schema` | ❌ |
| 3 | 0.391474 | `azmcp-kusto-table-list` | ❌ |
| 4 | 0.377056 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.364611 | `azmcp-postgres-table-schema` | ❌ |
| 6 | 0.361845 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.343671 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.341674 | `azmcp-monitor-table-list` | ❌ |
| 9 | 0.337275 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.329965 | `azmcp-storage-table-list` | ❌ |

---

## Test 42

**Expected Tool:** `azmcp-kusto-table-list`  
**Prompt:** List all tables in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591648 | `azmcp-kusto-table-list` | ✅ **EXPECTED** |
| 2 | 0.585237 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.550007 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.521534 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.520802 | `azmcp-redis-cluster-database-list` | ❌ |
| 6 | 0.517106 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.475496 | `azmcp-postgres-database-list` | ❌ |
| 8 | 0.464341 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.449656 | `azmcp-kusto-table-schema` | ❌ |
| 10 | 0.436518 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 43

**Expected Tool:** `azmcp-kusto-table-list`  
**Prompt:** Show me the tables in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.550024 | `azmcp-kusto-table-list` | ✅ **EXPECTED** |
| 2 | 0.523609 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.494337 | `azmcp-redis-cluster-database-list` | ❌ |
| 4 | 0.490672 | `azmcp-monitor-table-list` | ❌ |
| 5 | 0.475601 | `azmcp-kusto-database-list` | ❌ |
| 6 | 0.466379 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.466155 | `azmcp-kusto-table-schema` | ❌ |
| 8 | 0.431882 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.421626 | `azmcp-postgres-database-list` | ❌ |
| 10 | 0.421505 | `azmcp-kusto-sample` | ❌ |

---

## Test 44

**Expected Tool:** `azmcp-kusto-table-schema`  
**Prompt:** Show me the schema for table <table_name> in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.587791 | `azmcp-kusto-table-schema` | ✅ **EXPECTED** |
| 2 | 0.564230 | `azmcp-postgres-table-schema` | ❌ |
| 3 | 0.437235 | `azmcp-kusto-table-list` | ❌ |
| 4 | 0.426960 | `azmcp-kusto-sample` | ❌ |
| 5 | 0.413584 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.397432 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.387828 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.366193 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.365463 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.357480 | `azmcp-storage-table-list` | ❌ |

---

## Test 45

**Expected Tool:** `azmcp-postgres-database-list`  
**Prompt:** List all PostgreSQL databases in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.815617 | `azmcp-postgres-database-list` | ✅ **EXPECTED** |
| 2 | 0.644014 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.622790 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.542685 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.490904 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.453436 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.444410 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.435823 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.418348 | `azmcp-postgres-database-query` | ❌ |
| 10 | 0.414679 | `azmcp-postgres-server-setparam` | ❌ |

---

## Test 46

**Expected Tool:** `azmcp-postgres-database-list`  
**Prompt:** Show me the PostgreSQL databases in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.759980 | `azmcp-postgres-database-list` | ✅ **EXPECTED** |
| 2 | 0.589707 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.585885 | `azmcp-postgres-table-list` | ❌ |
| 4 | 0.552632 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.495607 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.433900 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.430634 | `azmcp-postgres-table-schema` | ❌ |
| 8 | 0.426802 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.416926 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.412941 | `azmcp-postgres-server-setparam` | ❌ |

---

## Test 47

**Expected Tool:** `azmcp-postgres-database-query`  
**Prompt:** Show me all items that contain the word <search_term> in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.546211 | `azmcp-postgres-database-list` | ❌ |
| 2 | 0.503267 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.466599 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.415817 | `azmcp-postgres-database-query` | ✅ **EXPECTED** |
| 5 | 0.403969 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.403924 | `azmcp-postgres-server-config` | ❌ |
| 7 | 0.380446 | `azmcp-postgres-table-schema` | ❌ |
| 8 | 0.354323 | `azmcp-postgres-server-setparam` | ❌ |
| 9 | 0.301808 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.277622 | `azmcp-sql-db-show` | ❌ |

---

## Test 48

**Expected Tool:** `azmcp-postgres-server-config-get`  
**Prompt:** Show me the configuration of PostgreSQL server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.756593 | `azmcp-postgres-server-config` | ❌ |
| 2 | 0.599471 | `azmcp-postgres-server-param` | ❌ |
| 3 | 0.535229 | `azmcp-postgres-server-setparam` | ❌ |
| 4 | 0.535049 | `azmcp-postgres-database-list` | ❌ |
| 5 | 0.518574 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.463172 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.431476 | `azmcp-postgres-table-schema` | ❌ |
| 8 | 0.394675 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.269224 | `azmcp-appconfig-kv-list` | ❌ |
| 10 | 0.269018 | `azmcp-sql-db-list` | ❌ |

---

## Test 49

**Expected Tool:** `azmcp-postgres-server-list`  
**Prompt:** List all PostgreSQL servers in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.900023 | `azmcp-postgres-server-list` | ✅ **EXPECTED** |
| 2 | 0.640733 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.565914 | `azmcp-postgres-table-list` | ❌ |
| 4 | 0.538997 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.507621 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.483663 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.472458 | `azmcp-grafana-list` | ❌ |
| 8 | 0.453841 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.446509 | `azmcp-redis-cache-list` | ❌ |
| 10 | 0.430475 | `azmcp-search-service-list` | ❌ |

---

## Test 50

**Expected Tool:** `azmcp-postgres-server-list`  
**Prompt:** Show me my PostgreSQL servers  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.674327 | `azmcp-postgres-server-list` | ✅ **EXPECTED** |
| 2 | 0.607062 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.576349 | `azmcp-postgres-server-config` | ❌ |
| 4 | 0.522996 | `azmcp-postgres-table-list` | ❌ |
| 5 | 0.506171 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.409406 | `azmcp-postgres-database-query` | ❌ |
| 7 | 0.400088 | `azmcp-postgres-server-setparam` | ❌ |
| 8 | 0.372955 | `azmcp-postgres-table-schema` | ❌ |
| 9 | 0.318087 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.305360 | `azmcp-sql-server-entra-admin-list` | ❌ |

---

## Test 51

**Expected Tool:** `azmcp-postgres-server-list`  
**Prompt:** Show me the PostgreSQL servers in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.832155 | `azmcp-postgres-server-list` | ✅ **EXPECTED** |
| 2 | 0.579232 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.531804 | `azmcp-postgres-server-config` | ❌ |
| 4 | 0.514445 | `azmcp-postgres-table-list` | ❌ |
| 5 | 0.505869 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.452608 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.444127 | `azmcp-grafana-list` | ❌ |
| 8 | 0.414695 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.411590 | `azmcp-search-service-list` | ❌ |
| 10 | 0.410719 | `azmcp-postgres-database-query` | ❌ |

---

## Test 52

**Expected Tool:** `azmcp-postgres-server-param`  
**Prompt:** Show me if the parameter my PostgreSQL server <server> has replication enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.594753 | `azmcp-postgres-server-param` | ✅ **EXPECTED** |
| 2 | 0.539671 | `azmcp-postgres-server-config` | ❌ |
| 3 | 0.489693 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.480826 | `azmcp-postgres-server-setparam` | ❌ |
| 5 | 0.451871 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.357606 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.330875 | `azmcp-postgres-table-schema` | ❌ |
| 8 | 0.305351 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.227987 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.207560 | `azmcp-sql-db-list` | ❌ |

---

## Test 53

**Expected Tool:** `azmcp-postgres-server-param-set`  
**Prompt:** Enable replication for my PostgreSQL server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.488474 | `azmcp-postgres-server-config` | ❌ |
| 2 | 0.469794 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.464562 | `azmcp-postgres-server-setparam` | ❌ |
| 4 | 0.447011 | `azmcp-postgres-server-param` | ❌ |
| 5 | 0.440760 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.354049 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.341624 | `azmcp-postgres-database-query` | ❌ |
| 8 | 0.317484 | `azmcp-postgres-table-schema` | ❌ |
| 9 | 0.184982 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.176538 | `azmcp-sql-db-list` | ❌ |

---

## Test 54

**Expected Tool:** `azmcp-postgres-table-list`  
**Prompt:** List all tables in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.789883 | `azmcp-postgres-table-list` | ✅ **EXPECTED** |
| 2 | 0.750580 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.574930 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.519820 | `azmcp-postgres-table-schema` | ❌ |
| 5 | 0.501400 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.449190 | `azmcp-postgres-database-query` | ❌ |
| 7 | 0.432750 | `azmcp-kusto-table-list` | ❌ |
| 8 | 0.430171 | `azmcp-postgres-server-param` | ❌ |
| 9 | 0.394396 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.386992 | `azmcp-redis-cluster-database-list` | ❌ |

---

## Test 55

**Expected Tool:** `azmcp-postgres-table-list`  
**Prompt:** Show me the tables in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.736083 | `azmcp-postgres-table-list` | ✅ **EXPECTED** |
| 2 | 0.690112 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.558357 | `azmcp-postgres-table-schema` | ❌ |
| 4 | 0.543331 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.521570 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.464929 | `azmcp-postgres-database-query` | ❌ |
| 7 | 0.447184 | `azmcp-postgres-server-param` | ❌ |
| 8 | 0.390339 | `azmcp-kusto-table-list` | ❌ |
| 9 | 0.371151 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.371036 | `azmcp-postgres-server-setparam` | ❌ |

---

## Test 56

**Expected Tool:** `azmcp-postgres-table-schema-get`  
**Prompt:** Show me the schema of table <table> in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.714877 | `azmcp-postgres-table-schema` | ❌ |
| 2 | 0.597846 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.574230 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.508082 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.475665 | `azmcp-kusto-table-schema` | ❌ |
| 6 | 0.443816 | `azmcp-postgres-server-param` | ❌ |
| 7 | 0.442553 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.427530 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.362687 | `azmcp-postgres-server-setparam` | ❌ |
| 10 | 0.336037 | `azmcp-sql-db-show` | ❌ |

---

## Test 57

**Expected Tool:** `azmcp-extension-azd`  
**Prompt:** Create a To-Do list web application that uses NodeJS and MongoDB  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.241366 | `azmcp-extension-az` | ❌ |
| 2 | 0.205325 | `azmcp-cloudarchitect-design` | ❌ |
| 3 | 0.196585 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 4 | 0.185433 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 5 | 0.181543 | `azmcp-redis-cluster-database-list` | ❌ |
| 6 | 0.177948 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.173269 | `azmcp-extension-azd` | ✅ **EXPECTED** |
| 8 | 0.165761 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.151256 | `azmcp-loadtesting-testresource-create` | ❌ |
| 10 | 0.151015 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 58

**Expected Tool:** `azmcp-extension-azd`  
**Prompt:** Deploy my web application to Azure App Service  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.437357 | `azmcp-foundry-models-deploy` | ❌ |
| 2 | 0.407179 | `azmcp-cloudarchitect-design` | ❌ |
| 3 | 0.364145 | `azmcp-extension-azd` | ✅ **EXPECTED** |
| 4 | 0.361106 | `azmcp-foundry-models-deployments-list` | ❌ |
| 5 | 0.356426 | `azmcp-extension-az` | ❌ |
| 6 | 0.340372 | `azmcp-bestpractices-get` | ❌ |
| 7 | 0.320093 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 8 | 0.309187 | `azmcp-loadtesting-test-create` | ❌ |
| 9 | 0.299828 | `azmcp-search-index-list` | ❌ |
| 10 | 0.297374 | `azmcp-workbooks-delete` | ❌ |

---

## Test 59

**Expected Tool:** `azmcp-keyvault-certificate-create`  
**Prompt:** Create a new certificate called <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.740223 | `azmcp-keyvault-certificate-create` | ✅ **EXPECTED** |
| 2 | 0.595781 | `azmcp-keyvault-key-create` | ❌ |
| 3 | 0.590531 | `azmcp-keyvault-secret-create` | ❌ |
| 4 | 0.575960 | `azmcp-keyvault-certificate-list` | ❌ |
| 5 | 0.543043 | `azmcp-keyvault-certificate-get` | ❌ |
| 6 | 0.434682 | `azmcp-keyvault-key-list` | ❌ |
| 7 | 0.414022 | `azmcp-keyvault-secret-list` | ❌ |
| 8 | 0.330026 | `azmcp-appconfig-kv-set` | ❌ |
| 9 | 0.310260 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.300980 | `azmcp-storage-datalake-directory-create` | ❌ |

---

## Test 60

**Expected Tool:** `azmcp-keyvault-certificate-get`  
**Prompt:** Show me the certificate <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628044 | `azmcp-keyvault-certificate-get` | ✅ **EXPECTED** |
| 2 | 0.624536 | `azmcp-keyvault-certificate-list` | ❌ |
| 3 | 0.564868 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.493572 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.475461 | `azmcp-keyvault-secret-list` | ❌ |
| 6 | 0.423669 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.418826 | `azmcp-keyvault-secret-create` | ❌ |
| 8 | 0.390684 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.346150 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.331207 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 61

**Expected Tool:** `azmcp-keyvault-certificate-get`  
**Prompt:** Show me the details of the certificate <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.662324 | `azmcp-keyvault-certificate-get` | ✅ **EXPECTED** |
| 2 | 0.606534 | `azmcp-keyvault-certificate-list` | ❌ |
| 3 | 0.535051 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.499272 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.482380 | `azmcp-keyvault-secret-list` | ❌ |
| 6 | 0.415716 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.412434 | `azmcp-keyvault-secret-create` | ❌ |
| 8 | 0.411136 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.365386 | `azmcp-sql-db-show` | ❌ |
| 10 | 0.363228 | `azmcp-aks-cluster-get` | ❌ |

---

## Test 62

**Expected Tool:** `azmcp-keyvault-certificate-list`  
**Prompt:** List all certificates in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.762015 | `azmcp-keyvault-certificate-list` | ✅ **EXPECTED** |
| 2 | 0.637437 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.608799 | `azmcp-keyvault-secret-list` | ❌ |
| 4 | 0.566460 | `azmcp-keyvault-certificate-get` | ❌ |
| 5 | 0.539516 | `azmcp-keyvault-certificate-create` | ❌ |
| 6 | 0.478022 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.453228 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.440946 | `azmcp-storage-account-list` | ❌ |
| 9 | 0.440771 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.431201 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 63

**Expected Tool:** `azmcp-keyvault-certificate-list`  
**Prompt:** Show me the certificates in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.660576 | `azmcp-keyvault-certificate-list` | ✅ **EXPECTED** |
| 2 | 0.570282 | `azmcp-keyvault-certificate-get` | ❌ |
| 3 | 0.540050 | `azmcp-keyvault-key-list` | ❌ |
| 4 | 0.516722 | `azmcp-keyvault-secret-list` | ❌ |
| 5 | 0.509020 | `azmcp-keyvault-certificate-create` | ❌ |
| 6 | 0.420427 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.396010 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.390169 | `azmcp-keyvault-secret-create` | ❌ |
| 9 | 0.382908 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.382081 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 64

**Expected Tool:** `azmcp-keyvault-key-create`  
**Prompt:** Create a new key called <key_name> with the RSA type in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.676328 | `azmcp-keyvault-key-create` | ✅ **EXPECTED** |
| 2 | 0.569250 | `azmcp-keyvault-secret-create` | ❌ |
| 3 | 0.555747 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.465742 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.417395 | `azmcp-keyvault-certificate-list` | ❌ |
| 6 | 0.413161 | `azmcp-keyvault-secret-list` | ❌ |
| 7 | 0.397141 | `azmcp-appconfig-kv-set` | ❌ |
| 8 | 0.389769 | `azmcp-keyvault-certificate-get` | ❌ |
| 9 | 0.340767 | `azmcp-appconfig-kv-lock` | ❌ |
| 10 | 0.307167 | `azmcp-appconfig-kv-unlock` | ❌ |

---

## Test 65

**Expected Tool:** `azmcp-keyvault-key-list`  
**Prompt:** List all keys in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.737135 | `azmcp-keyvault-key-list` | ✅ **EXPECTED** |
| 2 | 0.650155 | `azmcp-keyvault-secret-list` | ❌ |
| 3 | 0.631528 | `azmcp-keyvault-certificate-list` | ❌ |
| 4 | 0.498655 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.473942 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.473051 | `azmcp-storage-blob-container-list` | ❌ |
| 7 | 0.469953 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.468048 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.467249 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.455805 | `azmcp-keyvault-certificate-get` | ❌ |

---

## Test 66

**Expected Tool:** `azmcp-keyvault-key-list`  
**Prompt:** Show me the keys in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609392 | `azmcp-keyvault-key-list` | ✅ **EXPECTED** |
| 2 | 0.535381 | `azmcp-keyvault-secret-list` | ❌ |
| 3 | 0.520010 | `azmcp-keyvault-certificate-list` | ❌ |
| 4 | 0.479818 | `azmcp-keyvault-certificate-get` | ❌ |
| 5 | 0.462204 | `azmcp-keyvault-key-create` | ❌ |
| 6 | 0.429515 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.421380 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.412490 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.405205 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.390419 | `azmcp-storage-blob-container-list` | ❌ |

---

## Test 67

**Expected Tool:** `azmcp-keyvault-secret-create`  
**Prompt:** Create a new secret called <secret_name> with value <secret_value> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.767822 | `azmcp-keyvault-secret-create` | ✅ **EXPECTED** |
| 2 | 0.613364 | `azmcp-keyvault-key-create` | ❌ |
| 3 | 0.572303 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.516547 | `azmcp-keyvault-secret-list` | ❌ |
| 5 | 0.461384 | `azmcp-appconfig-kv-set` | ❌ |
| 6 | 0.417424 | `azmcp-keyvault-key-list` | ❌ |
| 7 | 0.384284 | `azmcp-keyvault-certificate-list` | ❌ |
| 8 | 0.373768 | `azmcp-appconfig-kv-lock` | ❌ |
| 9 | 0.369965 | `azmcp-keyvault-certificate-get` | ❌ |
| 10 | 0.342036 | `azmcp-appconfig-kv-show` | ❌ |

---

## Test 68

**Expected Tool:** `azmcp-keyvault-secret-list`  
**Prompt:** List all secrets in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.747343 | `azmcp-keyvault-secret-list` | ✅ **EXPECTED** |
| 2 | 0.617131 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.569911 | `azmcp-keyvault-certificate-list` | ❌ |
| 4 | 0.519133 | `azmcp-keyvault-secret-create` | ❌ |
| 5 | 0.455406 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.433562 | `azmcp-storage-blob-container-list` | ❌ |
| 7 | 0.433188 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.422433 | `azmcp-storage-account-list` | ❌ |
| 9 | 0.417973 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.415723 | `azmcp-storage-blob-list` | ❌ |

---

## Test 69

**Expected Tool:** `azmcp-keyvault-secret-list`  
**Prompt:** Show me the secrets in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.615400 | `azmcp-keyvault-secret-list` | ✅ **EXPECTED** |
| 2 | 0.520654 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.502403 | `azmcp-keyvault-secret-create` | ❌ |
| 4 | 0.467743 | `azmcp-keyvault-certificate-list` | ❌ |
| 5 | 0.456355 | `azmcp-keyvault-certificate-get` | ❌ |
| 6 | 0.411586 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.410957 | `azmcp-appconfig-kv-show` | ❌ |
| 8 | 0.385770 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.381522 | `azmcp-keyvault-certificate-create` | ❌ |
| 10 | 0.370430 | `azmcp-storage-blob-container-list` | ❌ |

---

## Test 70

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Get the configuration of AKS cluster <cluster-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.660869 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.611431 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.463682 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.456804 | `azmcp-loadtesting-test-get` | ❌ |
| 5 | 0.430975 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.391924 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.390959 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.390819 | `azmcp-appconfig-kv-list` | ❌ |
| 9 | 0.390141 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.367841 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 71

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Show me the details of AKS cluster <cluster-name> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.666849 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.589101 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.508226 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.461466 | `azmcp-sql-db-show` | ❌ |
| 5 | 0.448796 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.397366 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.384654 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.371570 | `azmcp-group-list` | ❌ |
| 9 | 0.365232 | `azmcp-sql-elastic-pool-list` | ❌ |
| 10 | 0.362332 | `azmcp-sql-db-list` | ❌ |

---

## Test 72

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Show me the network configuration for AKS cluster <cluster-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.567273 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.563029 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.368584 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.340349 | `azmcp-loadtesting-test-get` | ❌ |
| 5 | 0.340293 | `azmcp-kusto-cluster-list` | ❌ |
| 6 | 0.334923 | `azmcp-appconfig-account-list` | ❌ |
| 7 | 0.334860 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.314513 | `azmcp-appconfig-kv-list` | ❌ |
| 9 | 0.309738 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.296592 | `azmcp-postgres-server-config` | ❌ |

---

## Test 73

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** What are the details of my AKS cluster <cluster-name> in <resource-group>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661426 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.578662 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.503925 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.418518 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.417836 | `azmcp-sql-db-show` | ❌ |
| 6 | 0.372812 | `azmcp-kusto-cluster-list` | ❌ |
| 7 | 0.364343 | `azmcp-monitor-metrics-query` | ❌ |
| 8 | 0.360459 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.357011 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.354061 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 74

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** List all AKS clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.801067 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.690255 | `azmcp-kusto-cluster-list` | ❌ |
| 3 | 0.599940 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.560861 | `azmcp-aks-cluster-get` | ❌ |
| 5 | 0.549327 | `azmcp-search-service-list` | ❌ |
| 6 | 0.543684 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.515877 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.509175 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.502307 | `azmcp-subscription-list` | ❌ |
| 10 | 0.498121 | `azmcp-group-list` | ❌ |

---

## Test 75

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** Show me my Azure Kubernetes Service clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.608056 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.536412 | `azmcp-aks-cluster-get` | ❌ |
| 3 | 0.492910 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.446270 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.409711 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.408417 | `azmcp-kusto-database-list` | ❌ |
| 7 | 0.388143 | `azmcp-search-service-list` | ❌ |
| 8 | 0.387792 | `azmcp-search-index-list` | ❌ |
| 9 | 0.371535 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.363742 | `azmcp-subscription-list` | ❌ |

---

## Test 76

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** What AKS clusters do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.623896 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.530023 | `azmcp-aks-cluster-get` | ❌ |
| 3 | 0.449602 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.416564 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.378826 | `azmcp-monitor-workspace-list` | ❌ |
| 6 | 0.345241 | `azmcp-kusto-cluster-get` | ❌ |
| 7 | 0.342303 | `azmcp-extension-az` | ❌ |
| 8 | 0.341590 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.335444 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 10 | 0.327993 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 77

**Expected Tool:** `azmcp-loadtesting-test-create`  
**Prompt:** Create a basic URL test using the following endpoint URL <test-url> that runs for 30 minutes with 45 virtual users. The test name is <sample-name> with the test id <test-id> and the load testing resource is <load-test-resource> in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.586859 | `azmcp-loadtesting-test-create` | ✅ **EXPECTED** |
| 2 | 0.531362 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.503553 | `azmcp-loadtesting-testrun-create` | ❌ |
| 4 | 0.413857 | `azmcp-loadtesting-testresource-list` | ❌ |
| 5 | 0.402698 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.399602 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.350088 | `azmcp-loadtesting-testrun-update` | ❌ |
| 8 | 0.342853 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.336804 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.323398 | `azmcp-monitor-workspace-log-query` | ❌ |

---

## Test 78

**Expected Tool:** `azmcp-loadtesting-test-get`  
**Prompt:** Get the load test with id <test-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.643912 | `azmcp-loadtesting-test-get` | ✅ **EXPECTED** |
| 2 | 0.608881 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.574394 | `azmcp-loadtesting-testresource-create` | ❌ |
| 4 | 0.540975 | `azmcp-loadtesting-testrun-get` | ❌ |
| 5 | 0.473733 | `azmcp-loadtesting-testrun-list` | ❌ |
| 6 | 0.467383 | `azmcp-loadtesting-testrun-create` | ❌ |
| 7 | 0.434487 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.407086 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.397437 | `azmcp-group-list` | ❌ |
| 10 | 0.376222 | `azmcp-loadtesting-testrun-update` | ❌ |

---

## Test 79

**Expected Tool:** `azmcp-loadtesting-testresource-create`  
**Prompt:** Create a load test resource <load-test-resource-name> in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.717638 | `azmcp-loadtesting-testresource-create` | ✅ **EXPECTED** |
| 2 | 0.596760 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.515005 | `azmcp-loadtesting-test-create` | ❌ |
| 4 | 0.471911 | `azmcp-loadtesting-testrun-create` | ❌ |
| 5 | 0.447501 | `azmcp-loadtesting-test-get` | ❌ |
| 6 | 0.439299 | `azmcp-workbooks-create` | ❌ |
| 7 | 0.416772 | `azmcp-group-list` | ❌ |
| 8 | 0.396214 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.375845 | `azmcp-loadtesting-testrun-get` | ❌ |
| 10 | 0.369297 | `azmcp-workbooks-list` | ❌ |

---

## Test 80

**Expected Tool:** `azmcp-loadtesting-testresource-list`  
**Prompt:** List all load testing resources in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.738027 | `azmcp-loadtesting-testresource-list` | ✅ **EXPECTED** |
| 2 | 0.591851 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.577408 | `azmcp-group-list` | ❌ |
| 4 | 0.558483 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.526662 | `azmcp-workbooks-list` | ❌ |
| 6 | 0.515624 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.512935 | `azmcp-loadtesting-test-get` | ❌ |
| 8 | 0.511607 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.488178 | `azmcp-loadtesting-testrun-list` | ❌ |
| 10 | 0.487330 | `azmcp-grafana-list` | ❌ |

---

## Test 81

**Expected Tool:** `azmcp-loadtesting-testrun-create`  
**Prompt:** Create a test run using the id <testrun-id> for test <test-id> in the load testing resource <load-testing-resource> in resource group <resource-group>. Use the name of test run <display-name> and description as <description>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.612403 | `azmcp-loadtesting-testrun-create` | ✅ **EXPECTED** |
| 2 | 0.592805 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.540336 | `azmcp-loadtesting-test-create` | ❌ |
| 4 | 0.529011 | `azmcp-loadtesting-testrun-update` | ❌ |
| 5 | 0.489907 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.472404 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.413872 | `azmcp-loadtesting-testrun-list` | ❌ |
| 8 | 0.411627 | `azmcp-loadtesting-testresource-list` | ❌ |
| 9 | 0.393385 | `azmcp-workbooks-create` | ❌ |
| 10 | 0.331071 | `azmcp-keyvault-key-create` | ❌ |

---

## Test 82

**Expected Tool:** `azmcp-loadtesting-testrun-get`  
**Prompt:** Get the load test run with id <testrun-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.626628 | `azmcp-loadtesting-test-get` | ❌ |
| 2 | 0.602886 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.572473 | `azmcp-loadtesting-testrun-get` | ✅ **EXPECTED** |
| 4 | 0.561685 | `azmcp-loadtesting-testresource-create` | ❌ |
| 5 | 0.527221 | `azmcp-loadtesting-testrun-create` | ❌ |
| 6 | 0.499197 | `azmcp-loadtesting-testrun-list` | ❌ |
| 7 | 0.431806 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.416670 | `azmcp-loadtesting-testrun-update` | ❌ |
| 9 | 0.397792 | `azmcp-group-list` | ❌ |
| 10 | 0.397300 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 83

**Expected Tool:** `azmcp-loadtesting-testrun-list`  
**Prompt:** Get all the load test runs for the test with id <test-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.615977 | `azmcp-loadtesting-testresource-list` | ❌ |
| 2 | 0.607935 | `azmcp-loadtesting-test-get` | ❌ |
| 3 | 0.573158 | `azmcp-loadtesting-testrun-get` | ❌ |
| 4 | 0.568920 | `azmcp-loadtesting-testrun-list` | ✅ **EXPECTED** |
| 5 | 0.535207 | `azmcp-loadtesting-testresource-create` | ❌ |
| 6 | 0.484149 | `azmcp-loadtesting-testrun-create` | ❌ |
| 7 | 0.432149 | `azmcp-group-list` | ❌ |
| 8 | 0.418023 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.404583 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.400150 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 84

**Expected Tool:** `azmcp-loadtesting-testrun-update`  
**Prompt:** Update a test run display name as <display-name> for the id <testrun-id> for test <test-id> in the load testing resource <load-testing-resource> in resource group <resource-group>.  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.658869 | `azmcp-loadtesting-testrun-update` | ✅ **EXPECTED** |
| 2 | 0.500735 | `azmcp-loadtesting-testrun-create` | ❌ |
| 3 | 0.455550 | `azmcp-loadtesting-testrun-get` | ❌ |
| 4 | 0.446546 | `azmcp-loadtesting-test-get` | ❌ |
| 5 | 0.421980 | `azmcp-loadtesting-testresource-create` | ❌ |
| 6 | 0.398872 | `azmcp-loadtesting-test-create` | ❌ |
| 7 | 0.384614 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.383546 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.320044 | `azmcp-workbooks-update` | ❌ |
| 10 | 0.290319 | `azmcp-workbooks-create` | ❌ |

---

## Test 85

**Expected Tool:** `azmcp-grafana-list`  
**Prompt:** List all Azure Managed Grafana in one subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.578892 | `azmcp-grafana-list` | ✅ **EXPECTED** |
| 2 | 0.544665 | `azmcp-search-service-list` | ❌ |
| 3 | 0.513028 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.505836 | `azmcp-kusto-cluster-list` | ❌ |
| 5 | 0.493645 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.492724 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.492205 | `azmcp-subscription-list` | ❌ |
| 8 | 0.491740 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.489852 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.488589 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 86

**Expected Tool:** `azmcp-marketplace-product-get`  
**Prompt:** Get details about marketplace product <product_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.528228 | `azmcp-marketplace-product-get` | ✅ **EXPECTED** |
| 2 | 0.353256 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.330935 | `azmcp-servicebus-queue-details` | ❌ |
| 4 | 0.323704 | `azmcp-servicebus-topic-details` | ❌ |
| 5 | 0.322443 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.302335 | `azmcp-aks-cluster-get` | ❌ |
| 7 | 0.295793 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.289354 | `azmcp-workbooks-show` | ❌ |
| 9 | 0.276826 | `azmcp-kusto-cluster-get` | ❌ |
| 10 | 0.274403 | `azmcp-redis-cache-list` | ❌ |

---

## Test 87

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure code generation best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.653427 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.623348 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.424036 | `azmcp-cloudarchitect-design` | ❌ |
| 4 | 0.415425 | `azmcp-extension-az` | ❌ |
| 5 | 0.363348 | `azmcp-bicepschema-get` | ❌ |
| 6 | 0.351200 | `azmcp-extension-azd` | ❌ |
| 7 | 0.322197 | `azmcp-extension-azqr` | ❌ |
| 8 | 0.291934 | `azmcp-foundry-models-list` | ❌ |
| 9 | 0.287049 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.285183 | `azmcp-foundry-models-deployments-list` | ❌ |

---

## Test 88

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure deployment best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.660594 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.591163 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.466164 | `azmcp-extension-az` | ❌ |
| 4 | 0.440897 | `azmcp-cloudarchitect-design` | ❌ |
| 5 | 0.438759 | `azmcp-foundry-models-deployments-list` | ❌ |
| 6 | 0.372306 | `azmcp-extension-azd` | ❌ |
| 7 | 0.336929 | `azmcp-bicepschema-get` | ❌ |
| 8 | 0.326174 | `azmcp-marketplace-product-get` | ❌ |
| 9 | 0.316784 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.312595 | `azmcp-foundry-models-deploy` | ❌ |

---

## Test 89

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.693009 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.614150 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.457161 | `azmcp-extension-az` | ❌ |
| 4 | 0.455197 | `azmcp-cloudarchitect-design` | ❌ |
| 5 | 0.361659 | `azmcp-bicepschema-get` | ❌ |
| 6 | 0.355660 | `azmcp-extension-azd` | ❌ |
| 7 | 0.323934 | `azmcp-extension-azqr` | ❌ |
| 8 | 0.320757 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.319598 | `azmcp-marketplace-product-get` | ❌ |
| 10 | 0.319319 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 90

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure Functions code generation best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.588698 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.572787 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.403032 | `azmcp-extension-az` | ❌ |
| 4 | 0.381688 | `azmcp-cloudarchitect-design` | ❌ |
| 5 | 0.355589 | `azmcp-bicepschema-get` | ❌ |
| 6 | 0.334592 | `azmcp-extension-azd` | ❌ |
| 7 | 0.317025 | `azmcp-foundry-models-list` | ❌ |
| 8 | 0.309045 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.292709 | `azmcp-extension-azqr` | ❌ |
| 10 | 0.276526 | `azmcp-loadtesting-test-create` | ❌ |

---

## Test 91

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure Functions deployment best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.564559 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.510942 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.447455 | `azmcp-extension-az` | ❌ |
| 4 | 0.447227 | `azmcp-foundry-models-deployments-list` | ❌ |
| 5 | 0.395143 | `azmcp-cloudarchitect-design` | ❌ |
| 6 | 0.350882 | `azmcp-extension-azd` | ❌ |
| 7 | 0.340473 | `azmcp-foundry-models-deploy` | ❌ |
| 8 | 0.336125 | `azmcp-bicepschema-get` | ❌ |
| 9 | 0.317270 | `azmcp-foundry-models-list` | ❌ |
| 10 | 0.296958 | `azmcp-loadtesting-test-create` | ❌ |

---

## Test 92

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure Functions best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.604226 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.548048 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.423216 | `azmcp-extension-az` | ❌ |
| 4 | 0.387769 | `azmcp-cloudarchitect-design` | ❌ |
| 5 | 0.349170 | `azmcp-foundry-models-deployments-list` | ❌ |
| 6 | 0.348180 | `azmcp-bicepschema-get` | ❌ |
| 7 | 0.338117 | `azmcp-foundry-models-list` | ❌ |
| 8 | 0.328981 | `azmcp-extension-azd` | ❌ |
| 9 | 0.291251 | `azmcp-marketplace-product-get` | ❌ |
| 10 | 0.283516 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 93

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure Static Web Apps best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.592255 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.547337 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.395019 | `azmcp-cloudarchitect-design` | ❌ |
| 4 | 0.388440 | `azmcp-extension-az` | ❌ |
| 5 | 0.345585 | `azmcp-extension-azd` | ❌ |
| 6 | 0.330483 | `azmcp-bicepschema-get` | ❌ |
| 7 | 0.301120 | `azmcp-foundry-models-deployments-list` | ❌ |
| 8 | 0.275756 | `azmcp-foundry-models-list` | ❌ |
| 9 | 0.268843 | `azmcp-extension-azqr` | ❌ |
| 10 | 0.265709 | `azmcp-marketplace-product-get` | ❌ |

---

## Test 94

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

---

## Test 95

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
| 6 | 0.305464 | `azmcp-servicebus-queue-details` | ❌ |
| 7 | 0.304735 | `azmcp-grafana-list` | ❌ |
| 8 | 0.302254 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.293189 | `azmcp-search-index-describe` | ❌ |
| 10 | 0.281090 | `azmcp-workbooks-show` | ❌ |

---

## Test 96

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** Show me all available metrics and their definitions for storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.556689 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.542903 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.539818 | `azmcp-storage-blob-container-details` | ❌ |
| 4 | 0.535987 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.524761 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 6 | 0.519808 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.459748 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.431109 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.414488 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.397526 | `azmcp-appconfig-kv-list` | ❌ |

---

## Test 97

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
| 6 | 0.325097 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.308315 | `azmcp-monitor-workspace-log-query` | ❌ |
| 8 | 0.303266 | `azmcp-search-index-list` | ❌ |
| 9 | 0.302823 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.301966 | `azmcp-workbooks-show` | ❌ |

---

## Test 98

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Analyze the performance trends and response times for Application Insights resource <resource_name> over the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.445153 | `azmcp-monitor-resource-log-query` | ❌ |
| 2 | 0.439684 | `azmcp-loadtesting-testrun-get` | ❌ |
| 3 | 0.434260 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 4 | 0.404582 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.343141 | `azmcp-monitor-metrics-definitions` | ❌ |
| 6 | 0.340642 | `azmcp-loadtesting-testrun-list` | ❌ |
| 7 | 0.339771 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.332075 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.329460 | `azmcp-loadtesting-testresource-create` | ❌ |
| 10 | 0.328475 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 99

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Check the availability metrics for my Application Insights resource <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.450052 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.396276 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.389662 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.356326 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.342298 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 6 | 0.341525 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.326899 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.311815 | `azmcp-search-index-list` | ❌ |
| 9 | 0.302312 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.292483 | `azmcp-search-service-list` | ❌ |

---

## Test 100

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Get the <aggregation_type> <metric_name> metric for <resource_type> <resource_name> over the last <time_period> with intervals  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.422575 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.388574 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.300039 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.279577 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.275450 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.269366 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.259164 | `azmcp-grafana-list` | ❌ |
| 8 | 0.249899 | `azmcp-loadtesting-test-get` | ❌ |
| 9 | 0.249641 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.249387 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |

---

## Test 101

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
| 7 | 0.299707 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.293311 | `azmcp-loadtesting-testresource-create` | ❌ |
| 9 | 0.283523 | `azmcp-extension-azqr` | ❌ |
| 10 | 0.280135 | `azmcp-search-index-list` | ❌ |

---

## Test 102

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Query the <metric_name> metric for <resource_type> <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.488812 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.413107 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.373954 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.362063 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.293060 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.281012 | `azmcp-search-index-query` | ❌ |
| 7 | 0.272349 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.269058 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.264193 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 10 | 0.261986 | `azmcp-grafana-list` | ❌ |

---

## Test 103

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
| 6 | 0.319343 | `azmcp-loadtesting-testresource-create` | ❌ |
| 7 | 0.307343 | `azmcp-monitor-metrics-definitions` | ❌ |
| 8 | 0.278491 | `azmcp-workbooks-show` | ❌ |
| 9 | 0.277129 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.266949 | `azmcp-search-index-list` | ❌ |

---

## Test 104

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
| 8 | 0.355135 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.342350 | `azmcp-monitor-metrics-definitions` | ❌ |
| 10 | 0.333531 | `azmcp-workbooks-list` | ❌ |

---

## Test 105

**Expected Tool:** `azmcp-monitor-table-list`  
**Prompt:** List all tables in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.851075 | `azmcp-monitor-table-list` | ✅ **EXPECTED** |
| 2 | 0.725738 | `azmcp-monitor-table-type-list` | ❌ |
| 3 | 0.620445 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.586830 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.510990 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.502075 | `azmcp-grafana-list` | ❌ |
| 7 | 0.488557 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.436216 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.420392 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.419935 | `azmcp-kusto-database-list` | ❌ |

---

## Test 106

**Expected Tool:** `azmcp-monitor-table-list`  
**Prompt:** Show me the tables in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.798460 | `azmcp-monitor-table-list` | ✅ **EXPECTED** |
| 2 | 0.701122 | `azmcp-monitor-table-type-list` | ❌ |
| 3 | 0.599917 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.532978 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.487237 | `azmcp-grafana-list` | ❌ |
| 6 | 0.466524 | `azmcp-kusto-table-list` | ❌ |
| 7 | 0.441635 | `azmcp-monitor-workspace-log-query` | ❌ |
| 8 | 0.427408 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.413450 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.411590 | `azmcp-kusto-table-schema` | ❌ |

---

## Test 107

**Expected Tool:** `azmcp-monitor-table-type-list`  
**Prompt:** List all available table types in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.881524 | `azmcp-monitor-table-type-list` | ✅ **EXPECTED** |
| 2 | 0.765702 | `azmcp-monitor-table-list` | ❌ |
| 3 | 0.569921 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.525664 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.477280 | `azmcp-grafana-list` | ❌ |
| 6 | 0.447332 | `azmcp-kusto-table-list` | ❌ |
| 7 | 0.418517 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.416351 | `azmcp-kusto-table-schema` | ❌ |
| 9 | 0.394213 | `azmcp-monitor-workspace-log-query` | ❌ |
| 10 | 0.380989 | `azmcp-kusto-sample` | ❌ |

---

## Test 108

**Expected Tool:** `azmcp-monitor-table-type-list`  
**Prompt:** Show me the available table types in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.843138 | `azmcp-monitor-table-type-list` | ✅ **EXPECTED** |
| 2 | 0.736837 | `azmcp-monitor-table-list` | ❌ |
| 3 | 0.576731 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.502620 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.475734 | `azmcp-grafana-list` | ❌ |
| 6 | 0.427934 | `azmcp-kusto-table-schema` | ❌ |
| 7 | 0.421408 | `azmcp-kusto-table-list` | ❌ |
| 8 | 0.416739 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.391732 | `azmcp-kusto-sample` | ❌ |
| 10 | 0.384124 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 109

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
| 6 | 0.530433 | `azmcp-kusto-cluster-list` | ❌ |
| 7 | 0.517506 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.513663 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.508179 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.500768 | `azmcp-workbooks-list` | ❌ |

---

## Test 110

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
| 8 | 0.384131 | `azmcp-search-index-list` | ❌ |
| 9 | 0.383596 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.381606 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 111

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
| 7 | 0.439297 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.435475 | `azmcp-workbooks-list` | ❌ |
| 9 | 0.428958 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.427183 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 112

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
| 9 | 0.322067 | `azmcp-search-index-list` | ❌ |
| 10 | 0.318833 | `azmcp-workbooks-delete` | ❌ |

---

## Test 113

**Expected Tool:** `azmcp-datadog-monitoredresources-list`  
**Prompt:** List all monitored resources in the Datadog resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.666628 | `azmcp-datadog-monitoredresources-list` | ✅ **EXPECTED** |
| 2 | 0.434813 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.408658 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.401731 | `azmcp-grafana-list` | ❌ |
| 5 | 0.386195 | `azmcp-monitor-metrics-definitions` | ❌ |
| 6 | 0.369805 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.367875 | `azmcp-monitor-metrics-query` | ❌ |
| 8 | 0.364360 | `azmcp-workbooks-list` | ❌ |
| 9 | 0.355415 | `azmcp-loadtesting-testresource-list` | ❌ |
| 10 | 0.345409 | `azmcp-postgres-database-list` | ❌ |

---

## Test 114

**Expected Tool:** `azmcp-datadog-monitoredresources-list`  
**Prompt:** Show me the monitored resources in the Datadog resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.624792 | `azmcp-datadog-monitoredresources-list` | ✅ **EXPECTED** |
| 2 | 0.393227 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.374071 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.371017 | `azmcp-grafana-list` | ❌ |
| 5 | 0.364986 | `azmcp-monitor-metrics-query` | ❌ |
| 6 | 0.363038 | `azmcp-monitor-metrics-definitions` | ❌ |
| 7 | 0.343214 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.342468 | `azmcp-redis-cluster-database-list` | ❌ |
| 9 | 0.319895 | `azmcp-workbooks-list` | ❌ |
| 10 | 0.300073 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 115

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Check my Azure subscription for any compliance issues or recommendations  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.476826 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 2 | 0.459005 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.442625 | `azmcp-extension-az` | ❌ |
| 4 | 0.429452 | `azmcp-cloudarchitect-design` | ❌ |
| 5 | 0.427495 | `azmcp-search-service-list` | ❌ |
| 6 | 0.426311 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 7 | 0.423259 | `azmcp-subscription-list` | ❌ |
| 8 | 0.388980 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.365968 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 10 | 0.359605 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 116

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Provide compliance recommendations for my current Azure subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.527082 | `azmcp-bestpractices-get` | ❌ |
| 2 | 0.489996 | `azmcp-cloudarchitect-design` | ❌ |
| 3 | 0.487939 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 4 | 0.474017 | `azmcp-extension-az` | ❌ |
| 5 | 0.462743 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 6 | 0.382470 | `azmcp-search-service-list` | ❌ |
| 7 | 0.375802 | `azmcp-subscription-list` | ❌ |
| 8 | 0.338388 | `azmcp-marketplace-product-get` | ❌ |
| 9 | 0.333625 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.331080 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 117

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Scan my Azure subscription for compliance recommendations  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.516925 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 2 | 0.514791 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.490438 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 4 | 0.472526 | `azmcp-extension-az` | ❌ |
| 5 | 0.450091 | `azmcp-search-service-list` | ❌ |
| 6 | 0.449857 | `azmcp-cloudarchitect-design` | ❌ |
| 7 | 0.423554 | `azmcp-subscription-list` | ❌ |
| 8 | 0.398621 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.389831 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 10 | 0.371590 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 118

**Expected Tool:** `azmcp-role-assignment-list`  
**Prompt:** List all available role assignments in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.645259 | `azmcp-role-assignment-list` | ✅ **EXPECTED** |
| 2 | 0.487393 | `azmcp-search-service-list` | ❌ |
| 3 | 0.483988 | `azmcp-group-list` | ❌ |
| 4 | 0.483122 | `azmcp-subscription-list` | ❌ |
| 5 | 0.478700 | `azmcp-grafana-list` | ❌ |
| 6 | 0.474796 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.471378 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.460029 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.457712 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.452819 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 119

**Expected Tool:** `azmcp-role-assignment-list`  
**Prompt:** Show me the available role assignments in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609705 | `azmcp-role-assignment-list` | ✅ **EXPECTED** |
| 2 | 0.456956 | `azmcp-grafana-list` | ❌ |
| 3 | 0.436790 | `azmcp-subscription-list` | ❌ |
| 4 | 0.435642 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.435287 | `azmcp-search-service-list` | ❌ |
| 6 | 0.435155 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.428663 | `azmcp-group-list` | ❌ |
| 8 | 0.428370 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.420835 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.410380 | `azmcp-redis-cache-accesspolicy-list` | ❌ |

---

## Test 120

**Expected Tool:** `azmcp-redis-cache-accesspolicy-list`  
**Prompt:** List all access policies in the Redis Cache <cache_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.757057 | `azmcp-redis-cache-accesspolicy-list` | ✅ **EXPECTED** |
| 2 | 0.565047 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.445073 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.377563 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.312374 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.307404 | `azmcp-keyvault-secret-list` | ❌ |
| 7 | 0.303690 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.303531 | `azmcp-appconfig-kv-list` | ❌ |
| 9 | 0.300022 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.298380 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 121

**Expected Tool:** `azmcp-redis-cache-accesspolicy-list`  
**Prompt:** Show me the access policies in the Redis Cache <cache_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713787 | `azmcp-redis-cache-accesspolicy-list` | ✅ **EXPECTED** |
| 2 | 0.523114 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.412352 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.338840 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.300005 | `azmcp-bestpractices-get` | ❌ |
| 6 | 0.288926 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.286297 | `azmcp-appconfig-kv-list` | ❌ |
| 8 | 0.280220 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.258024 | `azmcp-appconfig-account-list` | ❌ |
| 10 | 0.257069 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 122

**Expected Tool:** `azmcp-redis-cache-list`  
**Prompt:** List all Redis Caches in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.764063 | `azmcp-redis-cache-list` | ✅ **EXPECTED** |
| 2 | 0.653924 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.501880 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 4 | 0.495048 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.472307 | `azmcp-grafana-list` | ❌ |
| 6 | 0.466141 | `azmcp-kusto-cluster-list` | ❌ |
| 7 | 0.464785 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.433313 | `azmcp-search-service-list` | ❌ |
| 9 | 0.431972 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.431715 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 123

**Expected Tool:** `azmcp-redis-cache-list`  
**Prompt:** Show me my Redis Caches  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.537885 | `azmcp-redis-cache-list` | ✅ **EXPECTED** |
| 2 | 0.450387 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 3 | 0.441104 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.401235 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.283598 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.265858 | `azmcp-appconfig-kv-list` | ❌ |
| 7 | 0.262106 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.257556 | `azmcp-appconfig-account-list` | ❌ |
| 9 | 0.252070 | `azmcp-grafana-list` | ❌ |
| 10 | 0.246442 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 124

**Expected Tool:** `azmcp-redis-cache-list`  
**Prompt:** Show me the Redis Caches in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.692210 | `azmcp-redis-cache-list` | ✅ **EXPECTED** |
| 2 | 0.595721 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.461603 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 4 | 0.434924 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.427325 | `azmcp-grafana-list` | ❌ |
| 6 | 0.399303 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.383383 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.382294 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.368549 | `azmcp-search-service-list` | ❌ |
| 10 | 0.361743 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 125

**Expected Tool:** `azmcp-redis-cluster-database-list`  
**Prompt:** List all databases in the Redis Cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.752919 | `azmcp-redis-cluster-database-list` | ✅ **EXPECTED** |
| 2 | 0.603780 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.594976 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.548268 | `azmcp-postgres-database-list` | ❌ |
| 5 | 0.538401 | `azmcp-cosmos-database-list` | ❌ |
| 6 | 0.471359 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.458244 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.456164 | `azmcp-kusto-table-list` | ❌ |
| 9 | 0.449548 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.419621 | `azmcp-postgres-table-list` | ❌ |

---

## Test 126

**Expected Tool:** `azmcp-redis-cluster-database-list`  
**Prompt:** Show me the databases in the Redis Cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.721506 | `azmcp-redis-cluster-database-list` | ✅ **EXPECTED** |
| 2 | 0.562860 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.537800 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.481612 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.480274 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.434940 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.414751 | `azmcp-kusto-table-list` | ❌ |
| 8 | 0.408379 | `azmcp-sql-db-list` | ❌ |
| 9 | 0.397285 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.351025 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 127

**Expected Tool:** `azmcp-redis-cluster-list`  
**Prompt:** List all Redis Clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.812960 | `azmcp-redis-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.679028 | `azmcp-kusto-cluster-list` | ❌ |
| 3 | 0.672104 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.588847 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.569222 | `azmcp-aks-cluster-list` | ❌ |
| 6 | 0.554298 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.527424 | `azmcp-kusto-database-list` | ❌ |
| 8 | 0.503279 | `azmcp-grafana-list` | ❌ |
| 9 | 0.467999 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.463770 | `azmcp-search-service-list` | ❌ |

---

## Test 128

**Expected Tool:** `azmcp-redis-cluster-list`  
**Prompt:** Show me my Redis Clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591593 | `azmcp-redis-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.514375 | `azmcp-redis-cluster-database-list` | ❌ |
| 3 | 0.467519 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.403281 | `azmcp-kusto-cluster-list` | ❌ |
| 5 | 0.385069 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 6 | 0.368011 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.329389 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.322188 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.305874 | `azmcp-kusto-cluster-get` | ❌ |
| 10 | 0.301294 | `azmcp-aks-cluster-get` | ❌ |

---

## Test 129

**Expected Tool:** `azmcp-redis-cluster-list`  
**Prompt:** Show me the Redis Clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.744239 | `azmcp-redis-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.607511 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.580866 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.518857 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.494170 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.491262 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.456252 | `azmcp-grafana-list` | ❌ |
| 8 | 0.446568 | `azmcp-kusto-cluster-get` | ❌ |
| 9 | 0.440697 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.400256 | `azmcp-redis-cache-accesspolicy-list` | ❌ |

---

## Test 130

**Expected Tool:** `azmcp-group-list`  
**Prompt:** List all resource groups in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.755935 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.566552 | `azmcp-workbooks-list` | ❌ |
| 3 | 0.545480 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.542878 | `azmcp-grafana-list` | ❌ |
| 5 | 0.542393 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 6 | 0.530600 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.524796 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.524265 | `azmcp-search-service-list` | ❌ |
| 9 | 0.517060 | `azmcp-loadtesting-testresource-list` | ❌ |
| 10 | 0.500858 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 131

**Expected Tool:** `azmcp-group-list`  
**Prompt:** Show me my resource groups  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.529504 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.460557 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 3 | 0.453960 | `azmcp-workbooks-list` | ❌ |
| 4 | 0.429014 | `azmcp-loadtesting-testresource-list` | ❌ |
| 5 | 0.426935 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.407817 | `azmcp-grafana-list` | ❌ |
| 7 | 0.391278 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.366273 | `azmcp-sql-db-list` | ❌ |
| 9 | 0.360235 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 10 | 0.345595 | `azmcp-redis-cluster-database-list` | ❌ |

---

## Test 132

**Expected Tool:** `azmcp-group-list`  
**Prompt:** Show me the resource groups in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.665771 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.526530 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 3 | 0.523088 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.522911 | `azmcp-workbooks-list` | ❌ |
| 5 | 0.518543 | `azmcp-loadtesting-testresource-list` | ❌ |
| 6 | 0.515905 | `azmcp-grafana-list` | ❌ |
| 7 | 0.492945 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.475313 | `azmcp-search-service-list` | ❌ |
| 9 | 0.470658 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.460412 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 133

**Expected Tool:** `azmcp-servicebus-queue-details`  
**Prompt:** Show me the details of service bus <service_bus_name> queue <queue_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.642876 | `azmcp-servicebus-queue-details` | ✅ **EXPECTED** |
| 2 | 0.460932 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.400870 | `azmcp-servicebus-topic-details` | ❌ |
| 4 | 0.375386 | `azmcp-aks-cluster-get` | ❌ |
| 5 | 0.338738 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.337239 | `azmcp-sql-db-show` | ❌ |
| 7 | 0.323046 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.316365 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.310912 | `azmcp-search-index-list` | ❌ |
| 10 | 0.308567 | `azmcp-redis-cache-list` | ❌ |

---

## Test 134

**Expected Tool:** `azmcp-servicebus-topic-details`  
**Prompt:** Show me the details of service bus <service_bus_name> topic <topic_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591687 | `azmcp-servicebus-topic-details` | ✅ **EXPECTED** |
| 2 | 0.571889 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.484072 | `azmcp-servicebus-queue-details` | ❌ |
| 4 | 0.361420 | `azmcp-aks-cluster-get` | ❌ |
| 5 | 0.347045 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.340022 | `azmcp-sql-db-show` | ❌ |
| 7 | 0.335572 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.324892 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.317554 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.315560 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 135

**Expected Tool:** `azmcp-servicebus-topic-subscription-details`  
**Prompt:** Show me the details of service bus <service_bus_name> subscription <subscription_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633187 | `azmcp-servicebus-topic-subscription-details` | ✅ **EXPECTED** |
| 2 | 0.494515 | `azmcp-servicebus-queue-details` | ❌ |
| 3 | 0.457036 | `azmcp-servicebus-topic-details` | ❌ |
| 4 | 0.449818 | `azmcp-search-service-list` | ❌ |
| 5 | 0.429458 | `azmcp-redis-cache-list` | ❌ |
| 6 | 0.426573 | `azmcp-kusto-cluster-get` | ❌ |
| 7 | 0.421009 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.409614 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.404739 | `azmcp-redis-cluster-list` | ❌ |
| 10 | 0.396053 | `azmcp-marketplace-product-get` | ❌ |

---

## Test 136

**Expected Tool:** `azmcp-sql-db-list`  
**Prompt:** List all databases in the Azure SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.643186 | `azmcp-sql-db-list` | ✅ **EXPECTED** |
| 2 | 0.609178 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.602888 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.527972 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.482736 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.474927 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.466220 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.464525 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.457220 | `azmcp-kusto-table-list` | ❌ |
| 10 | 0.457219 | `azmcp-postgres-server-list` | ❌ |

---

## Test 137

**Expected Tool:** `azmcp-sql-db-list`  
**Prompt:** Show me all the databases configuration details in the Azure SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609322 | `azmcp-sql-db-list` | ✅ **EXPECTED** |
| 2 | 0.524274 | `azmcp-sql-db-show` | ❌ |
| 3 | 0.471862 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.461646 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.458742 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.454316 | `azmcp-sql-elastic-pool-list` | ❌ |
| 7 | 0.394366 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.387715 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.387445 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.380428 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 138

**Expected Tool:** `azmcp-sql-db-show`  
**Prompt:** Get the configuration details for the SQL database <database_name> on server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.593150 | `azmcp-postgres-server-config` | ❌ |
| 2 | 0.528136 | `azmcp-sql-db-show` | ✅ **EXPECTED** |
| 3 | 0.465693 | `azmcp-sql-db-list` | ❌ |
| 4 | 0.446682 | `azmcp-postgres-server-param` | ❌ |
| 5 | 0.374073 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 6 | 0.371766 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.354111 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 8 | 0.348228 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.341701 | `azmcp-postgres-database-list` | ❌ |
| 10 | 0.341203 | `azmcp-postgres-table-schema` | ❌ |

---

## Test 139

**Expected Tool:** `azmcp-sql-db-show`  
**Prompt:** Show me the details of SQL database <database_name> in server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.530095 | `azmcp-sql-db-show` | ✅ **EXPECTED** |
| 2 | 0.440073 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.421862 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.375668 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.361500 | `azmcp-redis-cluster-database-list` | ❌ |
| 6 | 0.357119 | `azmcp-postgres-server-param` | ❌ |
| 7 | 0.351744 | `azmcp-postgres-table-schema` | ❌ |
| 8 | 0.344694 | `azmcp-kusto-table-schema` | ❌ |
| 9 | 0.343310 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.339765 | `azmcp-postgres-server-list` | ❌ |

---

## Test 140

**Expected Tool:** `azmcp-sql-elastic-pool-list`  
**Prompt:** List all elastic pools in SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.686435 | `azmcp-sql-elastic-pool-list` | ✅ **EXPECTED** |
| 2 | 0.502376 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.458302 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 4 | 0.434570 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.431871 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 6 | 0.431177 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.416273 | `azmcp-monitor-table-list` | ❌ |
| 8 | 0.414738 | `azmcp-postgres-database-list` | ❌ |
| 9 | 0.412061 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 10 | 0.409078 | `azmcp-monitor-table-type-list` | ❌ |

---

## Test 141

**Expected Tool:** `azmcp-sql-elastic-pool-list`  
**Prompt:** Show me the elastic pools configured for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.616579 | `azmcp-sql-elastic-pool-list` | ✅ **EXPECTED** |
| 2 | 0.457163 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.389072 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 4 | 0.385834 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 5 | 0.378556 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.357655 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 7 | 0.357019 | `azmcp-postgres-server-config` | ❌ |
| 8 | 0.354094 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.343841 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 10 | 0.335733 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |

---

## Test 142

**Expected Tool:** `azmcp-sql-elastic-pool-list`  
**Prompt:** What elastic pools are available in my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.602478 | `azmcp-sql-elastic-pool-list` | ✅ **EXPECTED** |
| 2 | 0.397670 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.378527 | `azmcp-monitor-table-type-list` | ❌ |
| 4 | 0.367742 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 5 | 0.344799 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.322365 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 7 | 0.316044 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 8 | 0.311302 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.308077 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.307794 | `azmcp-storage-table-list` | ❌ |

---

## Test 143

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** List Microsoft Entra ID administrators for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.788356 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.407432 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 3 | 0.376055 | `azmcp-sql-db-list` | ❌ |
| 4 | 0.365636 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.328968 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.328737 | `azmcp-role-assignment-list` | ❌ |
| 7 | 0.312627 | `azmcp-postgres-database-list` | ❌ |
| 8 | 0.280941 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 9 | 0.280452 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.279198 | `azmcp-sql-db-show` | ❌ |

---

## Test 144

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** Show me the Entra ID administrators configured for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.718251 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.315966 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.311085 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.309059 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 5 | 0.303560 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.268897 | `azmcp-sql-elastic-pool-list` | ❌ |
| 7 | 0.266264 | `azmcp-postgres-server-param` | ❌ |
| 8 | 0.250838 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.249616 | `azmcp-postgres-database-list` | ❌ |
| 10 | 0.228064 | `azmcp-role-assignment-list` | ❌ |

---

## Test 145

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** What Microsoft Entra ID administrators are set up for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651306 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.253610 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.249437 | `azmcp-cloudarchitect-design` | ❌ |
| 4 | 0.244772 | `azmcp-extension-az` | ❌ |
| 5 | 0.229560 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.228093 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 7 | 0.217698 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.205654 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.198194 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.189609 | `azmcp-storage-table-list` | ❌ |

---

## Test 146

**Expected Tool:** `azmcp-sql-server-firewall-rule-list`  
**Prompt:** List all firewall rules for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.732275 | `azmcp-sql-server-firewall-rule-list` | ✅ **EXPECTED** |
| 2 | 0.397092 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.385148 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.359228 | `azmcp-sql-db-list` | ❌ |
| 5 | 0.347004 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.327808 | `azmcp-postgres-database-list` | ❌ |
| 7 | 0.304958 | `azmcp-keyvault-secret-list` | ❌ |
| 8 | 0.304175 | `azmcp-monitor-table-list` | ❌ |
| 9 | 0.301711 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.299205 | `azmcp-postgres-server-config` | ❌ |

---

## Test 147

**Expected Tool:** `azmcp-sql-server-firewall-rule-list`  
**Prompt:** Show me the firewall rules for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.631499 | `azmcp-sql-server-firewall-rule-list` | ✅ **EXPECTED** |
| 2 | 0.321414 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.312035 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.290374 | `azmcp-extension-az` | ❌ |
| 5 | 0.290235 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.287747 | `azmcp-postgres-server-param` | ❌ |
| 7 | 0.276175 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.272586 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.272053 | `azmcp-sql-db-show` | ❌ |
| 10 | 0.255371 | `azmcp-bestpractices-get` | ❌ |

---

## Test 148

**Expected Tool:** `azmcp-sql-server-firewall-rule-list`  
**Prompt:** What firewall rules are configured for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633622 | `azmcp-sql-server-firewall-rule-list` | ✅ **EXPECTED** |
| 2 | 0.311867 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.299474 | `azmcp-extension-az` | ❌ |
| 4 | 0.277628 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.262028 | `azmcp-sql-db-list` | ❌ |
| 6 | 0.261404 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.261123 | `azmcp-postgres-server-param` | ❌ |
| 8 | 0.258402 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.247516 | `azmcp-bestpractices-get` | ❌ |
| 10 | 0.223532 | `azmcp-postgres-server-setparam` | ❌ |

---

## Test 149

**Expected Tool:** `azmcp-storage-account-create`  
**Prompt:** Create a new storage account called testaccount123 in East US region  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.412865 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.409148 | `azmcp-storage-account-list` | ❌ |
| 3 | 0.391555 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.374781 | `azmcp-loadtesting-test-create` | ❌ |
| 5 | 0.355049 | `azmcp-loadtesting-testresource-create` | ❌ |
| 6 | 0.346555 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.343750 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.325925 | `azmcp-keyvault-secret-create` | ❌ |
| 9 | 0.323501 | `azmcp-appconfig-kv-set` | ❌ |
| 10 | 0.315246 | `azmcp-keyvault-key-create` | ❌ |

---

## Test 150

**Expected Tool:** `azmcp-storage-account-create`  
**Prompt:** Create a storage account with premium performance and LRS replication  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.380549 | `azmcp-storage-account-list` | ❌ |
| 2 | 0.369325 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.361418 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.359300 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 5 | 0.355733 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.345072 | `azmcp-cloudarchitect-design` | ❌ |
| 7 | 0.344343 | `azmcp-loadtesting-testresource-create` | ❌ |
| 8 | 0.330036 | `azmcp-loadtesting-test-create` | ❌ |
| 9 | 0.327972 | `azmcp-storage-blob-list` | ❌ |
| 10 | 0.309596 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 151

**Expected Tool:** `azmcp-storage-account-create`  
**Prompt:** Create a new storage account with Data Lake Storage Gen2 enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.444359 | `azmcp-storage-datalake-directory-create` | ❌ |
| 2 | 0.426555 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.403466 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.389056 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.386385 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.383927 | `azmcp-loadtesting-testresource-create` | ❌ |
| 7 | 0.382839 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.380532 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.372411 | `azmcp-keyvault-certificate-create` | ❌ |
| 10 | 0.372115 | `azmcp-storage-blob-list` | ❌ |

---

## Test 152

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** List all storage accounts in my subscription including their location and SKU  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.667808 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.581352 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.540653 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.536858 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.501142 | `azmcp-subscription-list` | ❌ |
| 6 | 0.496742 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.493246 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.471507 | `azmcp-search-service-list` | ❌ |
| 9 | 0.458793 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.448575 | `azmcp-storage-blob-container-details` | ❌ |

---

## Test 153

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** Show me my storage accounts with whether hierarchical namespace (HNS) is enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.498854 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.498760 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.450671 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.424921 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.421609 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.419283 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.411558 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 8 | 0.375553 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.367906 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.347173 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 154

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** Show me the storage accounts in my subscription and include HTTPS-only and public blob access settings  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.571763 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.500961 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.499166 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.485850 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.473595 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.454195 | `azmcp-subscription-list` | ❌ |
| 7 | 0.431600 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.418264 | `azmcp-search-service-list` | ❌ |
| 9 | 0.415080 | `azmcp-appconfig-account-list` | ❌ |
| 10 | 0.383734 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

---

## Test 155

**Expected Tool:** `azmcp-storage-blob-batch-set-tier`  
**Prompt:** Set access tier to Cool for multiple blobs in the container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.696980 | `azmcp-storage-blob-batch-set-tier` | ✅ **EXPECTED** |
| 2 | 0.492959 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.462234 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.455203 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.382188 | `azmcp-cosmos-database-container-list` | ❌ |
| 6 | 0.327016 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.325778 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.308936 | `azmcp-appconfig-kv-set` | ❌ |
| 9 | 0.305877 | `azmcp-appconfig-kv-unlock` | ❌ |
| 10 | 0.305470 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 156

**Expected Tool:** `azmcp-storage-blob-batch-set-tier`  
**Prompt:** Change the access tier to Archive for blobs file1.txt and file2.txt in the container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.616173 | `azmcp-storage-blob-batch-set-tier` | ✅ **EXPECTED** |
| 2 | 0.441624 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.425608 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.410228 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.350730 | `azmcp-cosmos-database-container-list` | ❌ |
| 6 | 0.347749 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.335961 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.297862 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 9 | 0.292070 | `azmcp-appconfig-kv-lock` | ❌ |
| 10 | 0.280532 | `azmcp-extension-az` | ❌ |

---

## Test 157

**Expected Tool:** `azmcp-storage-blob-container-create`  
**Prompt:** Create the storage container mycontainer in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.586786 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.506877 | `azmcp-storage-blob-container-details` | ❌ |
| 3 | 0.498724 | `azmcp-storage-blob-list` | ❌ |
| 4 | 0.464848 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.409258 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.397288 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.342627 | `azmcp-keyvault-secret-create` | ❌ |
| 8 | 0.341219 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 9 | 0.334184 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.333864 | `azmcp-appconfig-kv-set` | ❌ |

---

## Test 158

**Expected Tool:** `azmcp-storage-blob-container-create`  
**Prompt:** Create the container using blob public access in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.529936 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.528538 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.498784 | `azmcp-storage-blob-container-details` | ❌ |
| 4 | 0.425854 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.417384 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 6 | 0.367194 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.354831 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.308107 | `azmcp-keyvault-secret-create` | ❌ |
| 9 | 0.302725 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.299793 | `azmcp-cloudarchitect-design` | ❌ |

---

## Test 159

**Expected Tool:** `azmcp-storage-blob-container-create`  
**Prompt:** Create a new blob container named documents with container public access in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.492860 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.492008 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.461518 | `azmcp-storage-blob-container-details` | ❌ |
| 4 | 0.451961 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.383543 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.381329 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 7 | 0.373517 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 8 | 0.341908 | `azmcp-storage-account-list` | ❌ |
| 9 | 0.335109 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.334414 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 160

**Expected Tool:** `azmcp-storage-blob-container-details`  
**Prompt:** Show me the properties of the storage container files in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.667735 | `azmcp-storage-blob-container-details` | ✅ **EXPECTED** |
| 2 | 0.666083 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.600582 | `azmcp-storage-blob-list` | ❌ |
| 4 | 0.538004 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.530648 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.515548 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.435234 | `azmcp-appconfig-kv-show` | ❌ |
| 8 | 0.432714 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.410040 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 10 | 0.380614 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 161

**Expected Tool:** `azmcp-storage-blob-container-list`  
**Prompt:** List all blob containers in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.755949 | `azmcp-storage-blob-container-list` | ✅ **EXPECTED** |
| 2 | 0.727277 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.629987 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.554192 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.540576 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.518991 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.468545 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.460725 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.381682 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 10 | 0.376896 | `azmcp-keyvault-key-list` | ❌ |

---

## Test 162

**Expected Tool:** `azmcp-storage-blob-container-list`  
**Prompt:** Show me the blob containers in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.692472 | `azmcp-storage-blob-container-list` | ✅ **EXPECTED** |
| 2 | 0.661978 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.578331 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.520243 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.506009 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.504109 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.447499 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.410876 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.359283 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 10 | 0.343809 | `azmcp-appconfig-kv-show` | ❌ |

---

## Test 163

**Expected Tool:** `azmcp-storage-blob-details`  
**Prompt:** Show me the properties for blob <blob_name> in container <container_name> in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.636267 | `azmcp-storage-blob-list` | ❌ |
| 2 | 0.610037 | `azmcp-storage-blob-container-details` | ❌ |
| 3 | 0.580250 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.500990 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.443535 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.408447 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.398507 | `azmcp-appconfig-kv-show` | ❌ |
| 8 | 0.374386 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 9 | 0.357758 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.355760 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 164

**Expected Tool:** `azmcp-storage-blob-details`  
**Prompt:** Get the details about blob <blob_name> in the container <container_name> in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.636134 | `azmcp-storage-blob-container-details` | ❌ |
| 2 | 0.626619 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.567171 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.472775 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.409576 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.400790 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.372648 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 8 | 0.368006 | `azmcp-aks-cluster-get` | ❌ |
| 9 | 0.367356 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.363222 | `azmcp-workbooks-show` | ❌ |

---

## Test 165

**Expected Tool:** `azmcp-storage-blob-list`  
**Prompt:** List all blobs in the blob container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.781334 | `azmcp-storage-blob-list` | ✅ **EXPECTED** |
| 2 | 0.698232 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.596811 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.539601 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.535735 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.524125 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.449412 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.423171 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.405584 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 10 | 0.388918 | `azmcp-keyvault-key-list` | ❌ |

---

## Test 166

**Expected Tool:** `azmcp-storage-blob-list`  
**Prompt:** Show me the blobs in the blob container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.699726 | `azmcp-storage-blob-list` | ✅ **EXPECTED** |
| 2 | 0.642497 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.550249 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.547324 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.476634 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.447605 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.401140 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.384647 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 9 | 0.382161 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 10 | 0.364900 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 167

**Expected Tool:** `azmcp-storage-datalake-directory-create`  
**Prompt:** Create a new directory at the path <directory_path> in Data Lake in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661238 | `azmcp-storage-datalake-directory-create` | ✅ **EXPECTED** |
| 2 | 0.466092 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 3 | 0.383890 | `azmcp-keyvault-secret-create` | ❌ |
| 4 | 0.373128 | `azmcp-keyvault-certificate-create` | ❌ |
| 5 | 0.365949 | `azmcp-keyvault-key-create` | ❌ |
| 6 | 0.359237 | `azmcp-storage-blob-container-list` | ❌ |
| 7 | 0.332047 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.315620 | `azmcp-loadtesting-testresource-create` | ❌ |
| 9 | 0.310931 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.309627 | `azmcp-storage-account-list` | ❌ |

---

## Test 168

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** List all paths in the Data Lake file system <file_system_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.771997 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.493285 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.481666 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.478289 | `azmcp-storage-datalake-directory-create` | ❌ |
| 5 | 0.466614 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.461279 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.423691 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.421423 | `azmcp-storage-share-file-list` | ❌ |
| 9 | 0.414331 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.402737 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 169

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** Show me the paths in the Data Lake file system <file_system_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.745256 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.464603 | `azmcp-storage-datalake-directory-create` | ❌ |
| 3 | 0.438209 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.433483 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.407149 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.396638 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.382229 | `azmcp-storage-share-file-list` | ❌ |
| 8 | 0.368074 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.353149 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.351697 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 170

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** Recursively list all paths in the Data Lake file system <file_system_name> in the storage account <account_name> filtered by <filter_path>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.786331 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.464964 | `azmcp-storage-share-file-list` | ❌ |
| 3 | 0.434868 | `azmcp-storage-datalake-directory-create` | ❌ |
| 4 | 0.403370 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.396767 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.390548 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.374712 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.343473 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.338084 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.336821 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 171

**Expected Tool:** `azmcp-storage-queue-message-send`  
**Prompt:** Send a message "Hello, World!" to the queue <queue_name> in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.416351 | `azmcp-storage-table-list` | ❌ |
| 2 | 0.397660 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.387407 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.378132 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.353974 | `azmcp-servicebus-queue-details` | ❌ |
| 6 | 0.326686 | `azmcp-appconfig-kv-set` | ❌ |
| 7 | 0.322915 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 8 | 0.315286 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.314685 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.311737 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 172

**Expected Tool:** `azmcp-storage-queue-message-send`  
**Prompt:** Send a message with TTL of 3600 seconds to the queue <queue_name> in storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.391715 | `azmcp-servicebus-queue-details` | ❌ |
| 2 | 0.382411 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.361177 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.348902 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.334181 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.333270 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 7 | 0.331200 | `azmcp-monitor-resource-log-query` | ❌ |
| 8 | 0.305738 | `azmcp-appconfig-kv-set` | ❌ |
| 9 | 0.300514 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.296655 | `azmcp-storage-blob-container-details` | ❌ |

---

## Test 173

**Expected Tool:** `azmcp-storage-queue-message-send`  
**Prompt:** Add a message to the queue <queue_name> in storage account <account_name> with visibility timeout of 30 seconds  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.374033 | `azmcp-servicebus-queue-details` | ❌ |
| 2 | 0.340743 | `azmcp-appconfig-kv-set` | ❌ |
| 3 | 0.336177 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.334605 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.309956 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.304277 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.298459 | `azmcp-appconfig-kv-lock` | ❌ |
| 8 | 0.295379 | `azmcp-keyvault-secret-create` | ❌ |
| 9 | 0.283395 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 10 | 0.281075 | `azmcp-storage-blob-container-details` | ❌ |

---

## Test 174

**Expected Tool:** `azmcp-storage-share-file-list`  
**Prompt:** List all files and directories in the File Share <share_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633480 | `azmcp-storage-share-file-list` | ✅ **EXPECTED** |
| 2 | 0.566858 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.548329 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.532860 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.521168 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.502198 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 7 | 0.428597 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.424157 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.421564 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.403900 | `azmcp-keyvault-key-list` | ❌ |

---

## Test 175

**Expected Tool:** `azmcp-storage-share-file-list`  
**Prompt:** Show me the files in the File Share <share_name> directory <directory_path> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.542320 | `azmcp-storage-share-file-list` | ✅ **EXPECTED** |
| 2 | 0.499556 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 3 | 0.482415 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.467334 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.441515 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.435735 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.405751 | `azmcp-storage-datalake-directory-create` | ❌ |
| 8 | 0.380627 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.358821 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.354881 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 176

**Expected Tool:** `azmcp-storage-share-file-list`  
**Prompt:** List files with prefix 'report' in the File Share <share_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.588940 | `azmcp-storage-share-file-list` | ✅ **EXPECTED** |
| 2 | 0.456027 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.453668 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.447432 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 5 | 0.444114 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.421309 | `azmcp-extension-azqr` | ❌ |
| 7 | 0.419326 | `azmcp-storage-blob-list` | ❌ |
| 8 | 0.377148 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.373790 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.365964 | `azmcp-workbooks-list` | ❌ |

---

## Test 177

**Expected Tool:** `azmcp-storage-table-list`  
**Prompt:** List all tables in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.790243 | `azmcp-storage-table-list` | ✅ **EXPECTED** |
| 2 | 0.612690 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.584417 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.561794 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.540808 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.513274 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.511143 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.504758 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.492182 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.485641 | `azmcp-kusto-table-list` | ❌ |

---

## Test 178

**Expected Tool:** `azmcp-storage-table-list`  
**Prompt:** Show me the tables in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.746352 | `azmcp-storage-table-list` | ✅ **EXPECTED** |
| 2 | 0.596167 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.532359 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.528309 | `azmcp-monitor-table-list` | ❌ |
| 5 | 0.515698 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.490488 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.489228 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.472352 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.463335 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.448637 | `azmcp-storage-blob-container-details` | ❌ |

---

## Test 179

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** List all subscriptions for my account  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.575969 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.512894 | `azmcp-cosmos-account-list` | ❌ |
| 3 | 0.489578 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.473852 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.471653 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.470819 | `azmcp-search-service-list` | ❌ |
| 7 | 0.450973 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.445724 | `azmcp-grafana-list` | ❌ |
| 9 | 0.436279 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.431337 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 180

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** Show me my subscriptions  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.405684 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.381238 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.351864 | `azmcp-grafana-list` | ❌ |
| 4 | 0.350951 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.344860 | `azmcp-search-service-list` | ❌ |
| 6 | 0.341813 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.315604 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.308874 | `azmcp-appconfig-account-list` | ❌ |
| 9 | 0.303494 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.297209 | `azmcp-group-list` | ❌ |

---

## Test 181

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** What is my current subscription?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.320038 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.298352 | `azmcp-marketplace-product-get` | ❌ |
| 3 | 0.286711 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.285063 | `azmcp-search-service-list` | ❌ |
| 5 | 0.282645 | `azmcp-grafana-list` | ❌ |
| 6 | 0.279702 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.278798 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.256358 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.254815 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 10 | 0.252504 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 182

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** What subscriptions do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.403312 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.354504 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.342318 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.340339 | `azmcp-grafana-list` | ❌ |
| 5 | 0.336798 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.332531 | `azmcp-search-service-list` | ❌ |
| 7 | 0.304965 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.300478 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 9 | 0.294080 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.291833 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 183

**Expected Tool:** `azmcp-azureterraformbestpractices-get`  
**Prompt:** Fetch the Azure Terraform best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.719967 | `azmcp-azureterraformbestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.658343 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.459270 | `azmcp-extension-az` | ❌ |
| 4 | 0.409552 | `azmcp-cloudarchitect-design` | ❌ |
| 5 | 0.354838 | `azmcp-bicepschema-get` | ❌ |
| 6 | 0.331791 | `azmcp-extension-azd` | ❌ |
| 7 | 0.309265 | `azmcp-loadtesting-test-get` | ❌ |
| 8 | 0.302784 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.295828 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.295781 | `azmcp-extension-azqr` | ❌ |

---

## Test 184

**Expected Tool:** `azmcp-azureterraformbestpractices-get`  
**Prompt:** Show me the Azure Terraform best practices and generate code sample to get a secret from Azure Key Vault  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.596382 | `azmcp-azureterraformbestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.551618 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.439871 | `azmcp-keyvault-secret-list` | ❌ |
| 4 | 0.439536 | `azmcp-keyvault-secret-create` | ❌ |
| 5 | 0.428888 | `azmcp-keyvault-certificate-get` | ❌ |
| 6 | 0.406432 | `azmcp-extension-az` | ❌ |
| 7 | 0.389450 | `azmcp-keyvault-key-list` | ❌ |
| 8 | 0.381243 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.378663 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.374825 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 185

**Expected Tool:** `azmcp-virtualdesktop-hostpool-list`  
**Prompt:** List all host pools in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713689 | `azmcp-virtualdesktop-hostpool-list` | ✅ **EXPECTED** |
| 2 | 0.658080 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 3 | 0.566615 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.557529 | `azmcp-search-service-list` | ❌ |
| 5 | 0.538464 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 6 | 0.536542 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.528358 | `azmcp-sql-elastic-pool-list` | ❌ |
| 8 | 0.527948 | `azmcp-postgres-server-list` | ❌ |
| 9 | 0.525796 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.506608 | `azmcp-redis-cache-list` | ❌ |

---

## Test 186

**Expected Tool:** `azmcp-virtualdesktop-hostpool-sessionhost-list`  
**Prompt:** List all session hosts in host pool <hostpool_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.736121 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ✅ **EXPECTED** |
| 2 | 0.714170 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 3 | 0.590273 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 4 | 0.397910 | `azmcp-sql-elastic-pool-list` | ❌ |
| 5 | 0.364696 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.337530 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.335295 | `azmcp-monitor-workspace-list` | ❌ |
| 8 | 0.333517 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.332928 | `azmcp-keyvault-secret-list` | ❌ |
| 10 | 0.330896 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 187

**Expected Tool:** `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list`  
**Prompt:** List all user sessions on session host <sessionhost_name> in host pool <hostpool_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.812057 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ✅ **EXPECTED** |
| 2 | 0.666731 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 3 | 0.513570 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 4 | 0.336385 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.329666 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.324557 | `azmcp-subscription-list` | ❌ |
| 7 | 0.316500 | `azmcp-loadtesting-testrun-list` | ❌ |
| 8 | 0.316295 | `azmcp-postgres-server-list` | ❌ |
| 9 | 0.305300 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.305148 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 188

**Expected Tool:** `azmcp-workbooks-create`  
**Prompt:** Create a new workbook named <workbook_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.553572 | `azmcp-workbooks-create` | ✅ **EXPECTED** |
| 2 | 0.433134 | `azmcp-workbooks-update` | ❌ |
| 3 | 0.365579 | `azmcp-workbooks-delete` | ❌ |
| 4 | 0.361215 | `azmcp-workbooks-show` | ❌ |
| 5 | 0.328113 | `azmcp-workbooks-list` | ❌ |
| 6 | 0.239813 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.217184 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.214789 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.188137 | `azmcp-loadtesting-testresource-create` | ❌ |
| 10 | 0.172751 | `azmcp-monitor-table-list` | ❌ |

---

## Test 189

**Expected Tool:** `azmcp-workbooks-delete`  
**Prompt:** Delete the workbook with resource ID <workbook_resource_id>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.624673 | `azmcp-workbooks-delete` | ✅ **EXPECTED** |
| 2 | 0.518630 | `azmcp-workbooks-show` | ❌ |
| 3 | 0.429901 | `azmcp-workbooks-create` | ❌ |
| 4 | 0.425569 | `azmcp-workbooks-list` | ❌ |
| 5 | 0.390411 | `azmcp-workbooks-update` | ❌ |
| 6 | 0.273939 | `azmcp-grafana-list` | ❌ |
| 7 | 0.198585 | `azmcp-appconfig-kv-delete` | ❌ |
| 8 | 0.193231 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.186572 | `azmcp-monitor-workspace-log-query` | ❌ |
| 10 | 0.157219 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 190

**Expected Tool:** `azmcp-workbooks-list`  
**Prompt:** List all workbooks in my resource group <resource_group_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.772431 | `azmcp-workbooks-list` | ✅ **EXPECTED** |
| 2 | 0.561977 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.532565 | `azmcp-workbooks-show` | ❌ |
| 4 | 0.516739 | `azmcp-grafana-list` | ❌ |
| 5 | 0.490216 | `azmcp-workbooks-delete` | ❌ |
| 6 | 0.488600 | `azmcp-group-list` | ❌ |
| 7 | 0.454315 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.454210 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.416566 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.413409 | `azmcp-sql-db-list` | ❌ |

---

## Test 191

**Expected Tool:** `azmcp-workbooks-list`  
**Prompt:** What workbooks do I have in resource group <resource_group_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.708641 | `azmcp-workbooks-list` | ✅ **EXPECTED** |
| 2 | 0.566545 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.539957 | `azmcp-workbooks-show` | ❌ |
| 4 | 0.488348 | `azmcp-workbooks-delete` | ❌ |
| 5 | 0.472373 | `azmcp-grafana-list` | ❌ |
| 6 | 0.427986 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.422975 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.421634 | `azmcp-group-list` | ❌ |
| 9 | 0.392326 | `azmcp-loadtesting-testresource-list` | ❌ |
| 10 | 0.371124 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 192

**Expected Tool:** `azmcp-workbooks-show`  
**Prompt:** Get information about the workbook with resource ID <workbook_resource_id>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.697539 | `azmcp-workbooks-show` | ✅ **EXPECTED** |
| 2 | 0.503906 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.494708 | `azmcp-workbooks-list` | ❌ |
| 4 | 0.457252 | `azmcp-workbooks-delete` | ❌ |
| 5 | 0.419134 | `azmcp-workbooks-update` | ❌ |
| 6 | 0.353546 | `azmcp-grafana-list` | ❌ |
| 7 | 0.238836 | `azmcp-monitor-resource-log-query` | ❌ |
| 8 | 0.235477 | `azmcp-marketplace-product-get` | ❌ |
| 9 | 0.227558 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.226385 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 193

**Expected Tool:** `azmcp-workbooks-show`  
**Prompt:** Show me the workbook with display name <workbook_display_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.469476 | `azmcp-workbooks-show` | ✅ **EXPECTED** |
| 2 | 0.453028 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.437570 | `azmcp-workbooks-update` | ❌ |
| 4 | 0.424338 | `azmcp-workbooks-list` | ❌ |
| 5 | 0.371623 | `azmcp-workbooks-delete` | ❌ |
| 6 | 0.292898 | `azmcp-grafana-list` | ❌ |
| 7 | 0.266530 | `azmcp-monitor-table-list` | ❌ |
| 8 | 0.239907 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.227383 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.177861 | `azmcp-loadtesting-testrun-update` | ❌ |

---

## Test 194

**Expected Tool:** `azmcp-workbooks-update`  
**Prompt:** Update the workbook <workbook_resource_id> with a new text step  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.469895 | `azmcp-workbooks-update` | ✅ **EXPECTED** |
| 2 | 0.384366 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.362354 | `azmcp-workbooks-show` | ❌ |
| 4 | 0.351698 | `azmcp-workbooks-delete` | ❌ |
| 5 | 0.283187 | `azmcp-loadtesting-testrun-update` | ❌ |
| 6 | 0.262873 | `azmcp-workbooks-list` | ❌ |
| 7 | 0.170118 | `azmcp-grafana-list` | ❌ |
| 8 | 0.145788 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 9 | 0.144812 | `azmcp-extension-az` | ❌ |
| 10 | 0.138420 | `azmcp-loadtesting-testrun-create` | ❌ |

---

## Test 195

**Expected Tool:** `azmcp-bicepschema-get`  
**Prompt:** How can I use Bicep to create an Azure OpenAI service?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.436524 | `azmcp-cloudarchitect-design` | ❌ |
| 2 | 0.432409 | `azmcp-bicepschema-get` | ✅ **EXPECTED** |
| 3 | 0.401162 | `azmcp-extension-az` | ❌ |
| 4 | 0.400985 | `azmcp-foundry-models-deploy` | ❌ |
| 5 | 0.394677 | `azmcp-bestpractices-get` | ❌ |
| 6 | 0.375228 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 7 | 0.363258 | `azmcp-search-index-list` | ❌ |
| 8 | 0.345030 | `azmcp-search-service-list` | ❌ |
| 9 | 0.342237 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.335700 | `azmcp-search-index-query` | ❌ |

---

## Summary

**Total Prompts Tested:** 195  
**Execution Time:** 36.0156997s  

### Success Rate Metrics

**Top Choice Success:** 80.0% (156/195 tests)  
**High Confidence (≥0.5):** 78.5% (153/195 tests)  
**Top Choice + High Confidence:** 72.3% (141/195 tests)  

### Success Rate Analysis

🟠 **Fair** - The tool selection system needs significant improvement.

