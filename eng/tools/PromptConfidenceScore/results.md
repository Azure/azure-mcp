# Tool Selection Analysis Setup

**Setup completed:** 2025-08-03 18:32:40  
**Tool count:** 95  
**Database setup time:** 54.8259349s  

---

# Tool Selection Analysis Results

**Analysis Date:** 2025-08-03 18:32:40  
**Tool count:** 95  

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
- [Test 10: azmcp-search-list](#test-10)
- [Test 11: azmcp-search-list](#test-11)
- [Test 12: azmcp-search-list](#test-12)
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
- [Test 48: azmcp-postgres-server-config](#test-48)
- [Test 49: azmcp-postgres-server-list](#test-49)
- [Test 50: azmcp-postgres-server-list](#test-50)
- [Test 51: azmcp-postgres-server-list](#test-51)
- [Test 52: azmcp-postgres-server-param](#test-52)
- [Test 53: azmcp-postgres-server-setparam](#test-53)
- [Test 54: azmcp-postgres-table-list](#test-54)
- [Test 55: azmcp-postgres-table-list](#test-55)
- [Test 56: azmcp-postgres-table-schema](#test-56)
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
- [Test 92: azmcp-tool-list](#test-92)
- [Test 93: azmcp-tool-list](#test-93)
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
- [Test 120: azmcp-redis-cache-list](#test-120)
- [Test 121: azmcp-redis-cache-list](#test-121)
- [Test 122: azmcp-redis-cache-list](#test-122)
- [Test 123: azmcp-redis-cache-list-accesspolicy](#test-123)
- [Test 124: azmcp-redis-cache-list-accesspolicy](#test-124)
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
- [Test 143: azmcp-sql-firewall-rule-list](#test-143)
- [Test 144: azmcp-sql-firewall-rule-list](#test-144)
- [Test 145: azmcp-sql-firewall-rule-list](#test-145)
- [Test 146: azmcp-sql-server-entra-admin-list](#test-146)
- [Test 147: azmcp-sql-server-entra-admin-list](#test-147)
- [Test 148: azmcp-sql-server-entra-admin-list](#test-148)
- [Test 149: azmcp-storage-account-list](#test-149)
- [Test 150: azmcp-storage-account-list](#test-150)
- [Test 151: azmcp-storage-account-list](#test-151)
- [Test 152: azmcp-storage-blob-batch-set-tier](#test-152)
- [Test 153: azmcp-storage-blob-batch-set-tier](#test-153)
- [Test 154: azmcp-storage-blob-container-details](#test-154)
- [Test 155: azmcp-storage-blob-container-list](#test-155)
- [Test 156: azmcp-storage-blob-container-list](#test-156)
- [Test 157: azmcp-storage-blob-list](#test-157)
- [Test 158: azmcp-storage-blob-list](#test-158)
- [Test 159: azmcp-storage-datalake-directory-create](#test-159)
- [Test 160: azmcp-storage-datalake-file-system-list-paths](#test-160)
- [Test 161: azmcp-storage-datalake-file-system-list-paths](#test-161)
- [Test 162: azmcp-storage-table-list](#test-162)
- [Test 163: azmcp-storage-table-list](#test-163)
- [Test 164: azmcp-subscription-list](#test-164)
- [Test 165: azmcp-subscription-list](#test-165)
- [Test 166: azmcp-subscription-list](#test-166)
- [Test 167: azmcp-subscription-list](#test-167)
- [Test 168: azmcp-azureterraformbestpractices-get](#test-168)
- [Test 169: azmcp-azureterraformbestpractices-get](#test-169)
- [Test 170: azmcp-workbooks-create](#test-170)
- [Test 171: azmcp-workbooks-delete](#test-171)
- [Test 172: azmcp-workbooks-list](#test-172)
- [Test 173: azmcp-workbooks-list](#test-173)
- [Test 174: azmcp-workbooks-show](#test-174)
- [Test 175: azmcp-workbooks-show](#test-175)
- [Test 176: azmcp-workbooks-update](#test-176)
- [Test 177: azmcp-bicepschema-get](#test-177)

---

## Test 1

**Expected Tool:** `azmcp-foundry-models-deploy`  
**Prompt:** Deploy a GPT4o instance on my resource \<resource-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.308128 | `azmcp-foundry-models-deploy` | ✅ **EXPECTED** |
| 2 | 0.275714 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.219783 | `azmcp-grafana-list` | ❌ |
| 4 | 0.219213 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 5 | 0.217469 | `azmcp-loadtesting-test-create` | ❌ |
| 6 | 0.217420 | `azmcp-loadtesting-testrun-create` | ❌ |
| 7 | 0.216052 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.214845 | `azmcp-postgres-server-setparam` | ❌ |
| 9 | 0.211918 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.210869 | `azmcp-bestpractices-get` | ❌ |

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
| 5 | 0.369270 | `azmcp-search-index-list` | ❌ |
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
| 5 | 0.319403 | `azmcp-search-index-list` | ❌ |
| 6 | 0.286814 | `azmcp-grafana-list` | ❌ |
| 7 | 0.265906 | `azmcp-extension-azd` | ❌ |
| 8 | 0.259989 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.254926 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.250392 | `azmcp-redis-cluster-list` | ❌ |

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
| 5 | 0.326896 | `azmcp-search-index-list` | ❌ |
| 6 | 0.298648 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.285399 | `azmcp-postgres-table-list` | ❌ |
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
| 3 | 0.356898 | `azmcp-foundry-models-deployments-list` | ❌ |
| 4 | 0.309590 | `azmcp-search-service-list` | ❌ |
| 5 | 0.277360 | `azmcp-search-index-list` | ❌ |
| 6 | 0.244697 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.233089 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 8 | 0.231148 | `azmcp-grafana-list` | ❌ |
| 9 | 0.216807 | `azmcp-extension-azd` | ❌ |
| 10 | 0.206602 | `azmcp-search-index-describe` | ❌ |

---

## Test 6

**Expected Tool:** `azmcp-search-index-describe`  
**Prompt:** Show me the details of the index \<index-name> in Cognitive Search service \<service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628497 | `azmcp-search-index-list` | ❌ |
| 2 | 0.593595 | `azmcp-search-index-describe` | ✅ **EXPECTED** |
| 3 | 0.477262 | `azmcp-search-index-query` | ❌ |
| 4 | 0.428217 | `azmcp-search-service-list` | ❌ |
| 5 | 0.380565 | `azmcp-aks-cluster-get` | ❌ |
| 6 | 0.372843 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.369164 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.340990 | `azmcp-kusto-table-schema` | ❌ |
| 9 | 0.333862 | `azmcp-sql-db-show` | ❌ |
| 10 | 0.332732 | `azmcp-monitor-table-list` | ❌ |

---

## Test 7

**Expected Tool:** `azmcp-search-index-list`  
**Prompt:** List all indexes in the Cognitive Search service \<service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.781483 | `azmcp-search-index-list` | ✅ **EXPECTED** |
| 2 | 0.553511 | `azmcp-search-service-list` | ❌ |
| 3 | 0.533132 | `azmcp-search-index-describe` | ❌ |
| 4 | 0.477187 | `azmcp-search-index-query` | ❌ |
| 5 | 0.438121 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.409910 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.406388 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.403857 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.383672 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.383130 | `azmcp-kusto-database-list` | ❌ |

---

## Test 8

**Expected Tool:** `azmcp-search-index-list`  
**Prompt:** Show me the indexes in the Cognitive Search service \<service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.732841 | `azmcp-search-index-list` | ✅ **EXPECTED** |
| 2 | 0.520895 | `azmcp-search-index-describe` | ❌ |
| 3 | 0.493534 | `azmcp-search-service-list` | ❌ |
| 4 | 0.455153 | `azmcp-search-index-query` | ❌ |
| 5 | 0.398801 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.379225 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.366094 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.359049 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.354062 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.345573 | `azmcp-foundry-models-list` | ❌ |

---

## Test 9

**Expected Tool:** `azmcp-search-index-query`  
**Prompt:** Search for instances of \<search_term> in the index \<index-name> in Cognitive Search service \<service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.555552 | `azmcp-search-index-list` | ❌ |
| 2 | 0.525951 | `azmcp-search-index-query` | ✅ **EXPECTED** |
| 3 | 0.497337 | `azmcp-search-index-describe` | ❌ |
| 4 | 0.458458 | `azmcp-search-service-list` | ❌ |
| 5 | 0.343163 | `azmcp-kusto-query` | ❌ |
| 6 | 0.321981 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 7 | 0.319294 | `azmcp-monitor-workspace-log-query` | ❌ |
| 8 | 0.301036 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.285725 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.281294 | `azmcp-monitor-metrics-definitions` | ❌ |

---

## Test 10

**Expected Tool:** `azmcp-search-list`  
**Prompt:** List all Cognitive Search services in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.745450 | `azmcp-search-service-list` | ❌ |
| 2 | 0.571448 | `azmcp-search-index-list` | ❌ |
| 3 | 0.500455 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.494272 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.493011 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.492228 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.486066 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.482464 | `azmcp-grafana-list` | ❌ |
| 9 | 0.477471 | `azmcp-subscription-list` | ❌ |
| 10 | 0.470384 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 11

**Expected Tool:** `azmcp-search-list`  
**Prompt:** Show me the Cognitive Search services in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.644213 | `azmcp-search-service-list` | ❌ |
| 2 | 0.494869 | `azmcp-search-index-list` | ❌ |
| 3 | 0.425939 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.412158 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.408456 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.400229 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.399822 | `azmcp-grafana-list` | ❌ |
| 8 | 0.397883 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.393708 | `azmcp-subscription-list` | ❌ |
| 10 | 0.390559 | `azmcp-foundry-models-list` | ❌ |

---

## Test 12

**Expected Tool:** `azmcp-search-list`  
**Prompt:** Show me my Cognitive Search services  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.482308 | `azmcp-search-service-list` | ❌ |
| 2 | 0.458985 | `azmcp-search-index-list` | ❌ |
| 3 | 0.344699 | `azmcp-foundry-models-deployments-list` | ❌ |
| 4 | 0.344264 | `azmcp-search-index-describe` | ❌ |
| 5 | 0.336013 | `azmcp-search-index-query` | ❌ |
| 6 | 0.322540 | `azmcp-foundry-models-list` | ❌ |
| 7 | 0.290214 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.283366 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.281134 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.278531 | `azmcp-redis-cache-list` | ❌ |

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
| 7 | 0.443594 | `azmcp-appconfig-kv-show` | ❌ |
| 8 | 0.442214 | `azmcp-grafana-list` | ❌ |
| 9 | 0.441656 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.429305 | `azmcp-search-service-list` | ❌ |

---

## Test 14

**Expected Tool:** `azmcp-appconfig-account-list`  
**Prompt:** Show me the App Configuration stores in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.634978 | `azmcp-appconfig-account-list` | ✅ **EXPECTED** |
| 2 | 0.533437 | `azmcp-appconfig-kv-list` | ❌ |
| 3 | 0.442245 | `azmcp-appconfig-kv-show` | ❌ |
| 4 | 0.385527 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.372455 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.368731 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.359567 | `azmcp-postgres-server-config` | ❌ |
| 8 | 0.356514 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.354747 | `azmcp-appconfig-kv-delete` | ❌ |
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
| 3 | 0.432001 | `azmcp-appconfig-kv-show` | ❌ |
| 4 | 0.364584 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.355916 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.348661 | `azmcp-appconfig-kv-delete` | ❌ |
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
| 3 | 0.475281 | `azmcp-appconfig-kv-set` | ❌ |
| 4 | 0.444881 | `azmcp-appconfig-kv-unlock` | ❌ |
| 5 | 0.443998 | `azmcp-appconfig-kv-lock` | ❌ |
| 6 | 0.413401 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.392016 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.264810 | `azmcp-workbooks-delete` | ❌ |
| 9 | 0.261982 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.248752 | `azmcp-keyvault-key-list` | ❌ |

---

## Test 17

**Expected Tool:** `azmcp-appconfig-kv-list`  
**Prompt:** List all key-value settings in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.730852 | `azmcp-appconfig-kv-list` | ✅ **EXPECTED** |
| 2 | 0.610828 | `azmcp-appconfig-kv-show` | ❌ |
| 3 | 0.564221 | `azmcp-appconfig-kv-set` | ❌ |
| 4 | 0.557810 | `azmcp-appconfig-account-list` | ❌ |
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
| 2 | 0.623796 | `azmcp-appconfig-kv-show` | ❌ |
| 3 | 0.553238 | `azmcp-appconfig-kv-set` | ❌ |
| 4 | 0.522426 | `azmcp-appconfig-account-list` | ❌ |
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
| 4 | 0.496036 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.440566 | `azmcp-appconfig-kv-show` | ❌ |
| 6 | 0.431516 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.373656 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.251259 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.238565 | `azmcp-keyvault-secret-create` | ❌ |
| 10 | 0.238242 | `azmcp-postgres-server-setparam` | ❌ |

---

## Test 20

**Expected Tool:** `azmcp-appconfig-kv-set`  
**Prompt:** Set the key <key_name> in App Configuration store <app_config_store_name> to \<value>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.652015 | `azmcp-appconfig-kv-set` | ✅ **EXPECTED** |
| 2 | 0.530004 | `azmcp-appconfig-kv-lock` | ❌ |
| 3 | 0.515422 | `azmcp-appconfig-kv-list` | ❌ |
| 4 | 0.512120 | `azmcp-appconfig-kv-show` | ❌ |
| 5 | 0.509346 | `azmcp-appconfig-kv-unlock` | ❌ |
| 6 | 0.508058 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.374966 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.345756 | `azmcp-postgres-server-setparam` | ❌ |
| 9 | 0.318751 | `azmcp-keyvault-secret-create` | ❌ |
| 10 | 0.308207 | `azmcp-keyvault-key-create` | ❌ |

---

## Test 21

**Expected Tool:** `azmcp-appconfig-kv-show`  
**Prompt:** Show the content for the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.603216 | `azmcp-appconfig-kv-list` | ❌ |
| 2 | 0.564320 | `azmcp-appconfig-kv-show` | ✅ **EXPECTED** |
| 3 | 0.451371 | `azmcp-appconfig-kv-set` | ❌ |
| 4 | 0.441713 | `azmcp-appconfig-kv-delete` | ❌ |
| 5 | 0.437432 | `azmcp-appconfig-account-list` | ❌ |
| 6 | 0.433846 | `azmcp-appconfig-kv-lock` | ❌ |
| 7 | 0.427588 | `azmcp-appconfig-kv-unlock` | ❌ |
| 8 | 0.301859 | `azmcp-keyvault-key-list` | ❌ |
| 9 | 0.291447 | `azmcp-postgres-server-config` | ❌ |
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
| 4 | 0.478090 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.476496 | `azmcp-appconfig-kv-delete` | ❌ |
| 6 | 0.451670 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.409406 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.267852 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.259561 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.252825 | `azmcp-keyvault-secret-create` | ❌ |

---

## Test 23

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** Create a Storage account with name <storage_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.494049 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.455881 | `azmcp-storage-account-list` | ❌ |
| 3 | 0.429618 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.426605 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.425930 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.403904 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.386699 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.374496 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.359998 | `azmcp-appconfig-kv-set` | ❌ |
| 10 | 0.337708 | `azmcp-storage-datalake-directory-create` | ❌ |

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
| 5 | 0.499251 | `azmcp-kusto-cluster-list` | ❌ |
| 6 | 0.496186 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.484074 | `azmcp-monitor-workspace-list` | ❌ |
| 8 | 0.482576 | `azmcp-grafana-list` | ❌ |
| 9 | 0.477475 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.473774 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 25

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** Show me the details of the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591950 | `azmcp-storage-blob-container-details` | ❌ |
| 2 | 0.577867 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.517881 | `azmcp-storage-blob-list` | ❌ |
| 4 | 0.516957 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.509806 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.441096 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.433899 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.417590 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.402357 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 10 | 0.371852 | `azmcp-sql-db-show` | ❌ |

---

## Test 26

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** List all cosmosdb accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.818357 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.668480 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.628122 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.615268 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.588682 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.587691 | `azmcp-subscription-list` | ❌ |
| 7 | 0.557870 | `azmcp-search-service-list` | ❌ |
| 8 | 0.528963 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.517902 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.516914 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 27

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** Show me my cosmosdb accounts  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.665447 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.605357 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.571613 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.467671 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.465987 | `azmcp-storage-blob-container-list` | ❌ |
| 6 | 0.457790 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 7 | 0.452019 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.436283 | `azmcp-subscription-list` | ❌ |
| 9 | 0.406730 | `azmcp-storage-blob-list` | ❌ |
| 10 | 0.392611 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

---

## Test 28

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** Show me the cosmosdb accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.752494 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.605125 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.566249 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.558106 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.546327 | `azmcp-subscription-list` | ❌ |
| 6 | 0.535227 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.513709 | `azmcp-search-service-list` | ❌ |
| 8 | 0.488006 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.475144 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.466414 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 29

**Expected Tool:** `azmcp-cosmos-database-container-item-query`  
**Prompt:** Show me the items that contain the word <search_term> in the container <container_name> in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.605253 | `azmcp-cosmos-database-container-list` | ❌ |
| 2 | 0.582607 | `azmcp-cosmos-database-container-item-query` | ✅ **EXPECTED** |
| 3 | 0.477874 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.471366 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.447757 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.420011 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.419721 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.398979 | `azmcp-search-service-list` | ❌ |
| 9 | 0.395587 | `azmcp-search-index-list` | ❌ |
| 10 | 0.386406 | `azmcp-kusto-query` | ❌ |

---

## Test 30

**Expected Tool:** `azmcp-cosmos-database-container-list`  
**Prompt:** List all the containers in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.852832 | `azmcp-cosmos-database-container-list` | ✅ **EXPECTED** |
| 2 | 0.693815 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.681044 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.630659 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.573211 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.568698 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 7 | 0.535260 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.502504 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.460959 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.453217 | `azmcp-kusto-database-list` | ❌ |

---

## Test 31

**Expected Tool:** `azmcp-cosmos-database-container-list`  
**Prompt:** Show me the containers in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.789399 | `azmcp-cosmos-database-container-list` | ✅ **EXPECTED** |
| 2 | 0.621310 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.614219 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.564031 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 5 | 0.562070 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.493294 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.485422 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.471023 | `azmcp-storage-table-list` | ❌ |
| 9 | 0.405645 | `azmcp-kusto-table-list` | ❌ |
| 10 | 0.403820 | `azmcp-kusto-database-list` | ❌ |

---

## Test 32

**Expected Tool:** `azmcp-cosmos-database-list`  
**Prompt:** List all the databases in the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.815683 | `azmcp-cosmos-database-list` | ✅ **EXPECTED** |
| 2 | 0.668515 | `azmcp-cosmos-account-list` | ❌ |
| 3 | 0.665298 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.576126 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.555134 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.548066 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.526389 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 8 | 0.526046 | `azmcp-redis-cluster-database-list` | ❌ |
| 9 | 0.514861 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.501477 | `azmcp-postgres-database-list` | ❌ |

---

## Test 33

**Expected Tool:** `azmcp-cosmos-database-list`  
**Prompt:** Show me the databases in the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.749370 | `azmcp-cosmos-database-list` | ✅ **EXPECTED** |
| 2 | 0.624759 | `azmcp-cosmos-database-container-list` | ❌ |
| 3 | 0.614572 | `azmcp-cosmos-account-list` | ❌ |
| 4 | 0.529965 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.512038 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 6 | 0.505363 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.498207 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.497414 | `azmcp-redis-cluster-database-list` | ❌ |
| 9 | 0.471814 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.447875 | `azmcp-postgres-database-list` | ❌ |

---

## Test 34

**Expected Tool:** `azmcp-kusto-cluster-get`  
**Prompt:** Show me the details of the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.500112 | `azmcp-kusto-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.464532 | `azmcp-aks-cluster-get` | ❌ |
| 3 | 0.457669 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.416762 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.364174 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.363004 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.344871 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.340890 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.333864 | `azmcp-kusto-query` | ❌ |
| 10 | 0.332639 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 35

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** List all Data Explorer clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651218 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.644037 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.540703 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.535930 | `azmcp-aks-cluster-list` | ❌ |
| 5 | 0.509396 | `azmcp-grafana-list` | ❌ |
| 6 | 0.505912 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.492107 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.487882 | `azmcp-search-service-list` | ❌ |
| 9 | 0.487583 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.480592 | `azmcp-kusto-cluster-get` | ❌ |

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
| 4 | 0.351881 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.338123 | `azmcp-aks-cluster-list` | ❌ |
| 6 | 0.334967 | `azmcp-kusto-cluster-get` | ❌ |
| 7 | 0.314729 | `azmcp-aks-cluster-get` | ❌ |
| 8 | 0.303083 | `azmcp-grafana-list` | ❌ |
| 9 | 0.292838 | `azmcp-redis-cache-list` | ❌ |
| 10 | 0.285121 | `azmcp-kusto-query` | ❌ |

---

## Test 37

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** Show me the Data Explorer clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584053 | `azmcp-redis-cluster-list` | ❌ |
| 2 | 0.549797 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 3 | 0.471096 | `azmcp-aks-cluster-list` | ❌ |
| 4 | 0.462944 | `azmcp-grafana-list` | ❌ |
| 5 | 0.460716 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.455251 | `azmcp-kusto-database-list` | ❌ |
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
| 2 | 0.606872 | `azmcp-kusto-database-list` | ✅ **EXPECTED** |
| 3 | 0.553218 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.549673 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.473076 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.461496 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.459180 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.434386 | `azmcp-postgres-table-list` | ❌ |
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
| 2 | 0.555945 | `azmcp-kusto-database-list` | ✅ **EXPECTED** |
| 3 | 0.497144 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.486732 | `azmcp-postgres-database-list` | ❌ |
| 5 | 0.438890 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.427251 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.422588 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.383664 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.368071 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.362905 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 40

**Expected Tool:** `azmcp-kusto-query`  
**Prompt:** Show me all items that contain the word <search_term> in the Data Explorer table <table_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.384447 | `azmcp-kusto-query` | ✅ **EXPECTED** |
| 2 | 0.367607 | `azmcp-kusto-sample` | ❌ |
| 3 | 0.349147 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.345798 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.333120 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.320384 | `azmcp-kusto-table-schema` | ❌ |
| 7 | 0.319112 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.314961 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.307962 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.304329 | `azmcp-kusto-database-list` | ❌ |

---

## Test 41

**Expected Tool:** `azmcp-kusto-sample`  
**Prompt:** Show me a data sample from the Data Explorer table <table_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.549157 | `azmcp-kusto-sample` | ✅ **EXPECTED** |
| 2 | 0.423565 | `azmcp-kusto-table-schema` | ❌ |
| 3 | 0.393953 | `azmcp-kusto-table-list` | ❌ |
| 4 | 0.377056 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.364611 | `azmcp-postgres-table-schema` | ❌ |
| 6 | 0.361845 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.343671 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.341674 | `azmcp-monitor-table-list` | ❌ |
| 9 | 0.333761 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.329933 | `azmcp-storage-table-list` | ❌ |

---

## Test 42

**Expected Tool:** `azmcp-kusto-table-list`  
**Prompt:** List all tables in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.590089 | `azmcp-kusto-table-list` | ✅ **EXPECTED** |
| 2 | 0.585272 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.550082 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.520793 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.518223 | `azmcp-kusto-database-list` | ❌ |
| 6 | 0.517060 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.475411 | `azmcp-postgres-database-list` | ❌ |
| 8 | 0.464400 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.462090 | `azmcp-kusto-table-schema` | ❌ |
| 10 | 0.436512 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 43

**Expected Tool:** `azmcp-kusto-table-list`  
**Prompt:** Show me the tables in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.548875 | `azmcp-kusto-table-list` | ✅ **EXPECTED** |
| 2 | 0.523519 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.494108 | `azmcp-redis-cluster-database-list` | ❌ |
| 4 | 0.490717 | `azmcp-monitor-table-list` | ❌ |
| 5 | 0.475656 | `azmcp-kusto-table-schema` | ❌ |
| 6 | 0.472689 | `azmcp-kusto-database-list` | ❌ |
| 7 | 0.466302 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.437647 | `azmcp-kusto-sample` | ❌ |
| 9 | 0.431964 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.421413 | `azmcp-postgres-database-list` | ❌ |

---

## Test 44

**Expected Tool:** `azmcp-kusto-table-schema`  
**Prompt:** Show me the schema for table <table_name> in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591993 | `azmcp-kusto-table-schema` | ✅ **EXPECTED** |
| 2 | 0.564028 | `azmcp-postgres-table-schema` | ❌ |
| 3 | 0.440615 | `azmcp-kusto-sample` | ❌ |
| 4 | 0.440349 | `azmcp-kusto-table-list` | ❌ |
| 5 | 0.413451 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.398469 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.387404 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.366035 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.365567 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.357502 | `azmcp-storage-table-list` | ❌ |

---

## Test 45

**Expected Tool:** `azmcp-postgres-database-list`  
**Prompt:** List all PostgreSQL databases in server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.816469 | `azmcp-postgres-database-list` | ✅ **EXPECTED** |
| 2 | 0.654921 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.619269 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.533816 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.485674 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.459027 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.454519 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.445558 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.429604 | `azmcp-postgres-table-schema` | ❌ |
| 10 | 0.421371 | `azmcp-postgres-database-query` | ❌ |

---

## Test 46

**Expected Tool:** `azmcp-postgres-database-list`  
**Prompt:** Show me the PostgreSQL databases in server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.754126 | `azmcp-postgres-database-list` | ✅ **EXPECTED** |
| 2 | 0.593434 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.589631 | `azmcp-postgres-table-list` | ❌ |
| 4 | 0.546389 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.499088 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.440842 | `azmcp-postgres-table-schema` | ❌ |
| 7 | 0.438619 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.432230 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.418061 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.414815 | `azmcp-postgres-server-setparam` | ❌ |

---

## Test 47

**Expected Tool:** `azmcp-postgres-database-query`  
**Prompt:** Show me all items that contain the word \<search_term> in the PostgreSQL database \<database> in server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.532560 | `azmcp-postgres-database-list` | ❌ |
| 2 | 0.492666 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.463655 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.402465 | `azmcp-postgres-database-query` | ✅ **EXPECTED** |
| 5 | 0.396439 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.395057 | `azmcp-postgres-server-param` | ❌ |
| 7 | 0.372073 | `azmcp-postgres-table-schema` | ❌ |
| 8 | 0.343297 | `azmcp-postgres-server-setparam` | ❌ |
| 9 | 0.287221 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.266480 | `azmcp-redis-cluster-database-list` | ❌ |

---

## Test 48

**Expected Tool:** `azmcp-postgres-server-config`  
**Prompt:** Show me the configuration of PostgreSQL server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.750976 | `azmcp-postgres-server-config` | ✅ **EXPECTED** |
| 2 | 0.598166 | `azmcp-postgres-server-param` | ❌ |
| 3 | 0.547341 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.531523 | `azmcp-postgres-server-setparam` | ❌ |
| 5 | 0.521654 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.478715 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.440180 | `azmcp-postgres-table-schema` | ❌ |
| 8 | 0.397389 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.270614 | `azmcp-appconfig-kv-list` | ❌ |
| 10 | 0.270407 | `azmcp-sql-db-list` | ❌ |

---

## Test 49

**Expected Tool:** `azmcp-postgres-server-list`  
**Prompt:** List all PostgreSQL servers in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.900022 | `azmcp-postgres-server-list` | ✅ **EXPECTED** |
| 2 | 0.640733 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.565858 | `azmcp-postgres-table-list` | ❌ |
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
| 1 | 0.674326 | `azmcp-postgres-server-list` | ✅ **EXPECTED** |
| 2 | 0.607062 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.576348 | `azmcp-postgres-server-config` | ❌ |
| 4 | 0.522892 | `azmcp-postgres-table-list` | ❌ |
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
| 4 | 0.514376 | `azmcp-postgres-table-list` | ❌ |
| 5 | 0.505869 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.452608 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.444127 | `azmcp-grafana-list` | ❌ |
| 8 | 0.414695 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.411590 | `azmcp-search-service-list` | ❌ |
| 10 | 0.410719 | `azmcp-postgres-database-query` | ❌ |

---

## Test 52

**Expected Tool:** `azmcp-postgres-server-param`  
**Prompt:** Show me if the parameter my PostgreSQL server \<server> has replication enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.606947 | `azmcp-postgres-server-param` | ✅ **EXPECTED** |
| 2 | 0.551493 | `azmcp-postgres-server-config` | ❌ |
| 3 | 0.501505 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.494033 | `azmcp-postgres-server-setparam` | ❌ |
| 5 | 0.460945 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.372778 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.357826 | `azmcp-postgres-table-schema` | ❌ |
| 8 | 0.318972 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.234381 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.220293 | `azmcp-sql-server-entra-admin-list` | ❌ |

---

## Test 53

**Expected Tool:** `azmcp-postgres-server-setparam`  
**Prompt:** Enable replication for my PostgreSQL server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.491897 | `azmcp-postgres-server-config` | ❌ |
| 2 | 0.476641 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.469764 | `azmcp-postgres-server-setparam` | ✅ **EXPECTED** |
| 4 | 0.452312 | `azmcp-postgres-database-list` | ❌ |
| 5 | 0.449297 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.370281 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.356733 | `azmcp-postgres-database-query` | ❌ |
| 8 | 0.337386 | `azmcp-postgres-table-schema` | ❌ |
| 9 | 0.194806 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.185002 | `azmcp-sql-server-firewall-rule-list` | ❌ |

---

## Test 54

**Expected Tool:** `azmcp-postgres-table-list`  
**Prompt:** List all tables in the PostgreSQL database \<database> in server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.791779 | `azmcp-postgres-table-list` | ✅ **EXPECTED** |
| 2 | 0.754026 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.573262 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.531222 | `azmcp-postgres-table-schema` | ❌ |
| 5 | 0.495695 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.442736 | `azmcp-postgres-database-query` | ❌ |
| 7 | 0.426275 | `azmcp-postgres-server-param` | ❌ |
| 8 | 0.424484 | `azmcp-kusto-table-list` | ❌ |
| 9 | 0.417698 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.395380 | `azmcp-redis-cluster-database-list` | ❌ |

---

## Test 55

**Expected Tool:** `azmcp-postgres-table-list`  
**Prompt:** Show me the tables in the PostgreSQL database \<database> in server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.735464 | `azmcp-postgres-table-list` | ✅ **EXPECTED** |
| 2 | 0.691921 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.564458 | `azmcp-postgres-table-schema` | ❌ |
| 4 | 0.547103 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.517195 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.459817 | `azmcp-postgres-database-query` | ❌ |
| 7 | 0.444724 | `azmcp-postgres-server-param` | ❌ |
| 8 | 0.378427 | `azmcp-monitor-table-list` | ❌ |
| 9 | 0.376444 | `azmcp-kusto-table-list` | ❌ |
| 10 | 0.371245 | `azmcp-redis-cluster-database-list` | ❌ |

---

## Test 56

**Expected Tool:** `azmcp-postgres-table-schema`  
**Prompt:** Show me the schema of table \<table> in the PostgreSQL database \<database> in server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.726767 | `azmcp-postgres-table-schema` | ✅ **EXPECTED** |
| 2 | 0.590449 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.556402 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.502823 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.479091 | `azmcp-kusto-table-schema` | ❌ |
| 6 | 0.444017 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.441402 | `azmcp-postgres-server-param` | ❌ |
| 8 | 0.415760 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.352868 | `azmcp-postgres-server-setparam` | ❌ |
| 10 | 0.343014 | `azmcp-monitor-table-list` | ❌ |

---

## Test 57

**Expected Tool:** `azmcp-extension-azd`  
**Prompt:** Create a To-Do list web application that uses NodeJS and MongoDB  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.241366 | `azmcp-extension-az` | ❌ |
| 2 | 0.186055 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 3 | 0.185423 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 4 | 0.181543 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.177946 | `azmcp-cosmos-database-list` | ❌ |
| 6 | 0.173269 | `azmcp-extension-azd` | ✅ **EXPECTED** |
| 7 | 0.165750 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.151256 | `azmcp-loadtesting-testresource-create` | ❌ |
| 9 | 0.151015 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.148838 | `azmcp-bestpractices-get` | ❌ |

---

## Test 58

**Expected Tool:** `azmcp-extension-azd`  
**Prompt:** Deploy my web application to Azure App Service  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.437357 | `azmcp-foundry-models-deploy` | ❌ |
| 2 | 0.364145 | `azmcp-extension-azd` | ✅ **EXPECTED** |
| 3 | 0.361106 | `azmcp-foundry-models-deployments-list` | ❌ |
| 4 | 0.356425 | `azmcp-extension-az` | ❌ |
| 5 | 0.340372 | `azmcp-bestpractices-get` | ❌ |
| 6 | 0.319982 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 7 | 0.309187 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.297374 | `azmcp-workbooks-delete` | ❌ |
| 9 | 0.289972 | `azmcp-search-index-list` | ❌ |
| 10 | 0.276977 | `azmcp-keyvault-certificate-create` | ❌ |

---

## Test 59

**Expected Tool:** `azmcp-keyvault-certificate-create`  
**Prompt:** Create a new certificate called <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.740315 | `azmcp-keyvault-certificate-create` | ✅ **EXPECTED** |
| 2 | 0.595631 | `azmcp-keyvault-key-create` | ❌ |
| 3 | 0.590990 | `azmcp-keyvault-secret-create` | ❌ |
| 4 | 0.575960 | `azmcp-keyvault-certificate-list` | ❌ |
| 5 | 0.543244 | `azmcp-keyvault-certificate-get` | ❌ |
| 6 | 0.434682 | `azmcp-keyvault-key-list` | ❌ |
| 7 | 0.414022 | `azmcp-keyvault-secret-list` | ❌ |
| 8 | 0.353599 | `azmcp-appconfig-kv-set` | ❌ |
| 9 | 0.310260 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.300980 | `azmcp-storage-datalake-directory-create` | ❌ |

---

## Test 60

**Expected Tool:** `azmcp-keyvault-certificate-get`  
**Prompt:** Show me the certificate <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628358 | `azmcp-keyvault-certificate-get` | ✅ **EXPECTED** |
| 2 | 0.624457 | `azmcp-keyvault-certificate-list` | ❌ |
| 3 | 0.564954 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.493432 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.475385 | `azmcp-keyvault-secret-list` | ❌ |
| 6 | 0.423750 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.418533 | `azmcp-keyvault-secret-create` | ❌ |
| 8 | 0.402194 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.346167 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.337241 | `azmcp-storage-blob-container-list` | ❌ |

---

## Test 61

**Expected Tool:** `azmcp-keyvault-certificate-get`  
**Prompt:** Show me the details of the certificate <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.662791 | `azmcp-keyvault-certificate-get` | ✅ **EXPECTED** |
| 2 | 0.606608 | `azmcp-keyvault-certificate-list` | ❌ |
| 3 | 0.535164 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.499345 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.482446 | `azmcp-keyvault-secret-list` | ❌ |
| 6 | 0.423224 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.415834 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.411956 | `azmcp-keyvault-secret-create` | ❌ |
| 9 | 0.373686 | `azmcp-kusto-cluster-get` | ❌ |
| 10 | 0.365440 | `azmcp-sql-db-show` | ❌ |

---

## Test 62

**Expected Tool:** `azmcp-keyvault-certificate-list`  
**Prompt:** List all certificates in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.762014 | `azmcp-keyvault-certificate-list` | ✅ **EXPECTED** |
| 2 | 0.637437 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.608799 | `azmcp-keyvault-secret-list` | ❌ |
| 4 | 0.566714 | `azmcp-keyvault-certificate-get` | ❌ |
| 5 | 0.539560 | `azmcp-keyvault-certificate-create` | ❌ |
| 6 | 0.478099 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.453226 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.446830 | `azmcp-storage-blob-container-list` | ❌ |
| 9 | 0.440946 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.431201 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 63

**Expected Tool:** `azmcp-keyvault-certificate-list`  
**Prompt:** Show me the certificates in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.660576 | `azmcp-keyvault-certificate-list` | ✅ **EXPECTED** |
| 2 | 0.570514 | `azmcp-keyvault-certificate-get` | ❌ |
| 3 | 0.540050 | `azmcp-keyvault-key-list` | ❌ |
| 4 | 0.516722 | `azmcp-keyvault-secret-list` | ❌ |
| 5 | 0.509058 | `azmcp-keyvault-certificate-create` | ❌ |
| 6 | 0.420506 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.396005 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.389990 | `azmcp-keyvault-secret-create` | ❌ |
| 9 | 0.387755 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.385226 | `azmcp-appconfig-kv-show` | ❌ |

---

## Test 64

**Expected Tool:** `azmcp-keyvault-key-create`  
**Prompt:** Create a new key called <key_name> with the RSA type in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.676112 | `azmcp-keyvault-key-create` | ✅ **EXPECTED** |
| 2 | 0.569892 | `azmcp-keyvault-secret-create` | ❌ |
| 3 | 0.555826 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.465742 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.417395 | `azmcp-keyvault-certificate-list` | ❌ |
| 6 | 0.413161 | `azmcp-keyvault-secret-list` | ❌ |
| 7 | 0.389641 | `azmcp-keyvault-certificate-get` | ❌ |
| 8 | 0.380330 | `azmcp-appconfig-kv-set` | ❌ |
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
| 4 | 0.498767 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.477363 | `azmcp-storage-blob-container-list` | ❌ |
| 6 | 0.473916 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.469953 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.468044 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.467247 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.460777 | `azmcp-storage-blob-list` | ❌ |

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
| 4 | 0.479883 | `azmcp-keyvault-certificate-get` | ❌ |
| 5 | 0.462250 | `azmcp-keyvault-key-create` | ❌ |
| 6 | 0.429323 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.421475 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.416773 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.412552 | `azmcp-keyvault-certificate-create` | ❌ |
| 10 | 0.397021 | `azmcp-storage-blob-container-list` | ❌ |

---

## Test 67

**Expected Tool:** `azmcp-keyvault-secret-create`  
**Prompt:** Create a new secret called <secret_name> with value <secret_value> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.768041 | `azmcp-keyvault-secret-create` | ✅ **EXPECTED** |
| 2 | 0.613360 | `azmcp-keyvault-key-create` | ❌ |
| 3 | 0.572330 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.516457 | `azmcp-keyvault-secret-list` | ❌ |
| 5 | 0.477410 | `azmcp-appconfig-kv-set` | ❌ |
| 6 | 0.417525 | `azmcp-keyvault-key-list` | ❌ |
| 7 | 0.384262 | `azmcp-keyvault-certificate-list` | ❌ |
| 8 | 0.373932 | `azmcp-appconfig-kv-lock` | ❌ |
| 9 | 0.370067 | `azmcp-keyvault-certificate-get` | ❌ |
| 10 | 0.345957 | `azmcp-appconfig-kv-show` | ❌ |

---

## Test 68

**Expected Tool:** `azmcp-keyvault-secret-list`  
**Prompt:** List all secrets in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.747347 | `azmcp-keyvault-secret-list` | ✅ **EXPECTED** |
| 2 | 0.617196 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.569983 | `azmcp-keyvault-certificate-list` | ❌ |
| 4 | 0.518899 | `azmcp-keyvault-secret-create` | ❌ |
| 5 | 0.455598 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.441225 | `azmcp-storage-blob-container-list` | ❌ |
| 7 | 0.433244 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.422515 | `azmcp-storage-account-list` | ❌ |
| 9 | 0.422170 | `azmcp-storage-blob-list` | ❌ |
| 10 | 0.417989 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 69

**Expected Tool:** `azmcp-keyvault-secret-list`  
**Prompt:** Show me the secrets in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.615400 | `azmcp-keyvault-secret-list` | ✅ **EXPECTED** |
| 2 | 0.520654 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.501880 | `azmcp-keyvault-secret-create` | ❌ |
| 4 | 0.467743 | `azmcp-keyvault-certificate-list` | ❌ |
| 5 | 0.456490 | `azmcp-keyvault-certificate-get` | ❌ |
| 6 | 0.419988 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.411733 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.385852 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.381576 | `azmcp-keyvault-certificate-create` | ❌ |
| 10 | 0.373816 | `azmcp-storage-blob-container-list` | ❌ |

---

## Test 70

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Get the configuration of AKS cluster \<cluster-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661499 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.612367 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.514301 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.462699 | `azmcp-loadtesting-test-get` | ❌ |
| 5 | 0.429147 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.409196 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.399021 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.392207 | `azmcp-appconfig-account-list` | ❌ |
| 9 | 0.390986 | `azmcp-appconfig-kv-list` | ❌ |
| 10 | 0.376565 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 71

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Show me the details of AKS cluster \<cluster-name> in resource group \<resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.667563 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.591201 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.536064 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.471916 | `azmcp-sql-db-show` | ❌ |
| 5 | 0.451026 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.414641 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.390622 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.386161 | `azmcp-group-list` | ❌ |
| 9 | 0.381715 | `azmcp-monitor-metrics-definitions` | ❌ |
| 10 | 0.376377 | `azmcp-sql-elastic-pool-list` | ❌ |

---

## Test 72

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Show me the network configuration for AKS cluster \<cluster-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.574422 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.567978 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.419585 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.348636 | `azmcp-kusto-cluster-list` | ❌ |
| 5 | 0.345602 | `azmcp-loadtesting-test-get` | ❌ |
| 6 | 0.342157 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.337221 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.324897 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.318292 | `azmcp-appconfig-kv-list` | ❌ |
| 10 | 0.303860 | `azmcp-loadtesting-test-create` | ❌ |

---

## Test 73

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** What are the details of my AKS cluster \<cluster-name> in \<resource-group>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.660401 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.577972 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.536521 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.422628 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.414527 | `azmcp-sql-db-show` | ❌ |
| 6 | 0.376489 | `azmcp-kusto-cluster-list` | ❌ |
| 7 | 0.376162 | `azmcp-monitor-metrics-query` | ❌ |
| 8 | 0.370427 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.364498 | `azmcp-monitor-metrics-definitions` | ❌ |
| 10 | 0.361905 | `azmcp-sql-elastic-pool-list` | ❌ |

---

## Test 74

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** List all AKS clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.800922 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.690255 | `azmcp-kusto-cluster-list` | ❌ |
| 3 | 0.599940 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.560818 | `azmcp-aks-cluster-get` | ❌ |
| 5 | 0.549327 | `azmcp-search-service-list` | ❌ |
| 6 | 0.543684 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.515922 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.503430 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.502401 | `azmcp-subscription-list` | ❌ |
| 10 | 0.498121 | `azmcp-group-list` | ❌ |

---

## Test 75

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** Show me my Azure Kubernetes Service clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.607789 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.536440 | `azmcp-aks-cluster-get` | ❌ |
| 3 | 0.492910 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.446270 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.409808 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.403403 | `azmcp-kusto-database-list` | ❌ |
| 7 | 0.388143 | `azmcp-search-service-list` | ❌ |
| 8 | 0.383346 | `azmcp-search-index-list` | ❌ |
| 9 | 0.371535 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.363804 | `azmcp-subscription-list` | ❌ |

---

## Test 76

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** What AKS clusters do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.623631 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.530010 | `azmcp-aks-cluster-get` | ❌ |
| 3 | 0.449602 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.416564 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.378826 | `azmcp-monitor-workspace-list` | ❌ |
| 6 | 0.347244 | `azmcp-kusto-cluster-get` | ❌ |
| 7 | 0.342303 | `azmcp-extension-az` | ❌ |
| 8 | 0.337217 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.328074 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.325876 | `azmcp-extension-azd` | ❌ |

---

## Test 77

**Expected Tool:** `azmcp-loadtesting-test-create`  
**Prompt:** Create a basic URL test using the following endpoint URL \<test-url> that runs for 30 minutes with 45 virtual users. The test name is \<sample-name> with the test id \<test-id> and the load testing resource is \<load-test-resource> in the resource group \<resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.586388 | `azmcp-loadtesting-test-create` | ✅ **EXPECTED** |
| 2 | 0.536845 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.493962 | `azmcp-loadtesting-testrun-create` | ❌ |
| 4 | 0.417668 | `azmcp-loadtesting-testresource-list` | ❌ |
| 5 | 0.396484 | `azmcp-loadtesting-test-get` | ❌ |
| 6 | 0.391957 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.345777 | `azmcp-monitor-resource-log-query` | ❌ |
| 8 | 0.337312 | `azmcp-loadtesting-testrun-update` | ❌ |
| 9 | 0.333853 | `azmcp-loadtesting-testrun-list` | ❌ |
| 10 | 0.326264 | `azmcp-monitor-workspace-log-query` | ❌ |

---

## Test 78

**Expected Tool:** `azmcp-loadtesting-test-get`  
**Prompt:** Get the load test with id \<test-id> in the load test resource \<test-resource> in resource group \<resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.655521 | `azmcp-loadtesting-test-get` | ✅ **EXPECTED** |
| 2 | 0.622512 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.583866 | `azmcp-loadtesting-testresource-create` | ❌ |
| 4 | 0.549634 | `azmcp-loadtesting-testrun-get` | ❌ |
| 5 | 0.485418 | `azmcp-loadtesting-testrun-list` | ❌ |
| 6 | 0.471668 | `azmcp-loadtesting-testrun-create` | ❌ |
| 7 | 0.449643 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.414468 | `azmcp-group-list` | ❌ |
| 9 | 0.414187 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.384833 | `azmcp-loadtesting-testrun-update` | ❌ |

---

## Test 79

**Expected Tool:** `azmcp-loadtesting-testresource-create`  
**Prompt:** Create a load test resource \<load-test-resource-name> in the resource group \<resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713657 | `azmcp-loadtesting-testresource-create` | ✅ **EXPECTED** |
| 2 | 0.588614 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.523077 | `azmcp-loadtesting-test-create` | ❌ |
| 4 | 0.473618 | `azmcp-loadtesting-testrun-create` | ❌ |
| 5 | 0.446452 | `azmcp-loadtesting-test-get` | ❌ |
| 6 | 0.435910 | `azmcp-workbooks-create` | ❌ |
| 7 | 0.415901 | `azmcp-group-list` | ❌ |
| 8 | 0.393483 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.377932 | `azmcp-loadtesting-testrun-get` | ❌ |
| 10 | 0.364280 | `azmcp-workbooks-list` | ❌ |

---

## Test 80

**Expected Tool:** `azmcp-loadtesting-testresource-list`  
**Prompt:** List all load testing resources in the resource group \<resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.733890 | `azmcp-loadtesting-testresource-list` | ✅ **EXPECTED** |
| 2 | 0.585965 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.571444 | `azmcp-group-list` | ❌ |
| 4 | 0.555049 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.516843 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.515846 | `azmcp-workbooks-list` | ❌ |
| 7 | 0.513345 | `azmcp-loadtesting-test-get` | ❌ |
| 8 | 0.510045 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.490092 | `azmcp-grafana-list` | ❌ |
| 10 | 0.486535 | `azmcp-loadtesting-testrun-list` | ❌ |

---

## Test 81

**Expected Tool:** `azmcp-loadtesting-testrun-create`  
**Prompt:** Create a test run using the id \<testrun-id> for test \<test-id> in the load testing resource \<load-testing-resource> in resource group \<resource-group>. Use the name of test run \<display-name> and description as \<description>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.620740 | `azmcp-loadtesting-testrun-create` | ✅ **EXPECTED** |
| 2 | 0.618720 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.545731 | `azmcp-loadtesting-test-create` | ❌ |
| 4 | 0.519195 | `azmcp-loadtesting-testrun-update` | ❌ |
| 5 | 0.484350 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.467913 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.431314 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.411962 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.379914 | `azmcp-workbooks-create` | ❌ |
| 10 | 0.328393 | `azmcp-keyvault-key-create` | ❌ |

---

## Test 82

**Expected Tool:** `azmcp-loadtesting-testrun-get`  
**Prompt:** Get the load test run with id \<testrun-id> in the load test resource \<test-resource> in resource group \<resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.637202 | `azmcp-loadtesting-test-get` | ❌ |
| 2 | 0.610428 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.592690 | `azmcp-loadtesting-testrun-get` | ✅ **EXPECTED** |
| 4 | 0.569263 | `azmcp-loadtesting-testresource-create` | ❌ |
| 5 | 0.544878 | `azmcp-loadtesting-testrun-create` | ❌ |
| 6 | 0.520871 | `azmcp-loadtesting-testrun-list` | ❌ |
| 7 | 0.447895 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.436988 | `azmcp-loadtesting-testrun-update` | ❌ |
| 9 | 0.405420 | `azmcp-group-list` | ❌ |
| 10 | 0.404533 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 83

**Expected Tool:** `azmcp-loadtesting-testrun-list`  
**Prompt:** Get all the load test runs for the test with id \<test-id> in the load test resource \<test-resource> in resource group \<resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633538 | `azmcp-loadtesting-testresource-list` | ❌ |
| 2 | 0.614820 | `azmcp-loadtesting-test-get` | ❌ |
| 3 | 0.577554 | `azmcp-loadtesting-testrun-list` | ✅ **EXPECTED** |
| 4 | 0.573905 | `azmcp-loadtesting-testrun-get` | ❌ |
| 5 | 0.552015 | `azmcp-loadtesting-testresource-create` | ❌ |
| 6 | 0.486601 | `azmcp-loadtesting-testrun-create` | ❌ |
| 7 | 0.450765 | `azmcp-group-list` | ❌ |
| 8 | 0.426757 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.424219 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.412646 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 84

**Expected Tool:** `azmcp-loadtesting-testrun-update`  
**Prompt:** Update a test run display name as \<display-name> for the id \<testrun-id> for test \<test-id> in the load testing resource \<load-testing-resource> in resource group \<resource-group>.  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.663619 | `azmcp-loadtesting-testrun-update` | ✅ **EXPECTED** |
| 2 | 0.506785 | `azmcp-loadtesting-testrun-create` | ❌ |
| 3 | 0.457126 | `azmcp-loadtesting-testrun-get` | ❌ |
| 4 | 0.439819 | `azmcp-loadtesting-test-get` | ❌ |
| 5 | 0.432666 | `azmcp-loadtesting-testresource-create` | ❌ |
| 6 | 0.399846 | `azmcp-loadtesting-test-create` | ❌ |
| 7 | 0.397389 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.390496 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.316181 | `azmcp-workbooks-update` | ❌ |
| 10 | 0.290657 | `azmcp-workbooks-create` | ❌ |

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
| 7 | 0.492210 | `azmcp-subscription-list` | ❌ |
| 8 | 0.491655 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.489846 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.488589 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 86

**Expected Tool:** `azmcp-marketplace-product-get`  
**Prompt:** Get details about marketplace product <product_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.528228 | `azmcp-marketplace-product-get` | ✅ **EXPECTED** |
| 2 | 0.365508 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.354541 | `azmcp-servicebus-topic-details` | ❌ |
| 4 | 0.345344 | `azmcp-servicebus-queue-details` | ❌ |
| 5 | 0.324750 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.322443 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.310098 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.302363 | `azmcp-aks-cluster-get` | ❌ |
| 9 | 0.300057 | `azmcp-search-index-describe` | ❌ |
| 10 | 0.289354 | `azmcp-workbooks-show` | ❌ |

---

## Test 87

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure code generation best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.653427 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.623315 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.415425 | `azmcp-extension-az` | ❌ |
| 4 | 0.363348 | `azmcp-bicepschema-get` | ❌ |
| 5 | 0.351200 | `azmcp-extension-azd` | ❌ |
| 6 | 0.322197 | `azmcp-extension-azqr` | ❌ |
| 7 | 0.291934 | `azmcp-foundry-models-list` | ❌ |
| 8 | 0.287049 | `azmcp-loadtesting-test-create` | ❌ |
| 9 | 0.285183 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.283944 | `azmcp-marketplace-product-get` | ❌ |

---

## Test 88

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure deployment best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.660594 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.591127 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.466164 | `azmcp-extension-az` | ❌ |
| 4 | 0.438759 | `azmcp-foundry-models-deployments-list` | ❌ |
| 5 | 0.372306 | `azmcp-extension-azd` | ❌ |
| 6 | 0.336929 | `azmcp-bicepschema-get` | ❌ |
| 7 | 0.326174 | `azmcp-marketplace-product-get` | ❌ |
| 8 | 0.316784 | `azmcp-loadtesting-test-get` | ❌ |
| 9 | 0.312595 | `azmcp-foundry-models-deploy` | ❌ |
| 10 | 0.307635 | `azmcp-extension-azqr` | ❌ |

---

## Test 89

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure Functions code generation best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.588698 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.572749 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.403032 | `azmcp-extension-az` | ❌ |
| 4 | 0.355589 | `azmcp-bicepschema-get` | ❌ |
| 5 | 0.334592 | `azmcp-extension-azd` | ❌ |
| 6 | 0.317025 | `azmcp-foundry-models-list` | ❌ |
| 7 | 0.309045 | `azmcp-foundry-models-deployments-list` | ❌ |
| 8 | 0.292709 | `azmcp-extension-azqr` | ❌ |
| 9 | 0.276526 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.274942 | `azmcp-foundry-models-deploy` | ❌ |

---

## Test 90

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure Functions deployment best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.564559 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.510864 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.447455 | `azmcp-extension-az` | ❌ |
| 4 | 0.447227 | `azmcp-foundry-models-deployments-list` | ❌ |
| 5 | 0.350882 | `azmcp-extension-azd` | ❌ |
| 6 | 0.340473 | `azmcp-foundry-models-deploy` | ❌ |
| 7 | 0.336125 | `azmcp-bicepschema-get` | ❌ |
| 8 | 0.317270 | `azmcp-foundry-models-list` | ❌ |
| 9 | 0.296958 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.285117 | `azmcp-marketplace-product-get` | ❌ |

---

## Test 91

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Fetch the latest Azure Static Web Apps best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.592255 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.547375 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.388440 | `azmcp-extension-az` | ❌ |
| 4 | 0.345585 | `azmcp-extension-azd` | ❌ |
| 5 | 0.330483 | `azmcp-bicepschema-get` | ❌ |
| 6 | 0.301120 | `azmcp-foundry-models-deployments-list` | ❌ |
| 7 | 0.275756 | `azmcp-foundry-models-list` | ❌ |
| 8 | 0.268843 | `azmcp-extension-azqr` | ❌ |
| 9 | 0.265709 | `azmcp-marketplace-product-get` | ❌ |
| 10 | 0.257837 | `azmcp-aks-cluster-get` | ❌ |

---

## Test 92

**Expected Tool:** `azmcp-tool-list`  
**Prompt:** List all available tools in the Azure MCP server  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.455117 | `azmcp-extension-az` | ❌ |
| 2 | 0.430235 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.402330 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 4 | 0.398599 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 5 | 0.395140 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.392853 | `azmcp-extension-azd` | ❌ |
| 7 | 0.390696 | `azmcp-keyvault-certificate-list` | ❌ |
| 8 | 0.389970 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.389641 | `azmcp-subscription-list` | ❌ |
| 10 | 0.389260 | `azmcp-redis-cache-list` | ❌ |

---

## Test 93

**Expected Tool:** `azmcp-tool-list`  
**Prompt:** Show me the available tools in the Azure MCP server  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.477557 | `azmcp-extension-az` | ❌ |
| 2 | 0.401787 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.373806 | `azmcp-extension-azd` | ❌ |
| 4 | 0.357752 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 5 | 0.345389 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 6 | 0.328647 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 7 | 0.325168 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.324715 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.315361 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.314650 | `azmcp-subscription-list` | ❌ |

---

## Test 94

**Expected Tool:** `azmcp-monitor-healthmodels-entity-gethealth`  
**Prompt:** Show me the health status of entity <entity_id> in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.479740 | `azmcp-monitor-healthmodels-entity-gethealth` | ✅ **EXPECTED** |
| 2 | 0.472094 | `azmcp-monitor-workspace-list` | ❌ |
| 3 | 0.468204 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.464012 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.413357 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.410207 | `azmcp-monitor-resource-log-query` | ❌ |
| 7 | 0.380121 | `azmcp-grafana-list` | ❌ |
| 8 | 0.339333 | `azmcp-aks-cluster-get` | ❌ |
| 9 | 0.337603 | `azmcp-loadtesting-testrun-get` | ❌ |
| 10 | 0.316587 | `azmcp-workbooks-show` | ❌ |

---

## Test 95

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** Get metric definitions for <resource_type> <resource_name> from the namespace  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.601035 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 2 | 0.433928 | `azmcp-monitor-metrics-query` | ❌ |
| 3 | 0.332356 | `azmcp-monitor-table-type-list` | ❌ |
| 4 | 0.331706 | `azmcp-servicebus-topic-details` | ❌ |
| 5 | 0.319457 | `azmcp-search-index-describe` | ❌ |
| 6 | 0.319115 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 7 | 0.317584 | `azmcp-servicebus-queue-details` | ❌ |
| 8 | 0.304735 | `azmcp-grafana-list` | ❌ |
| 9 | 0.302254 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 10 | 0.302221 | `azmcp-storage-blob-container-details` | ❌ |

---

## Test 96

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** Show me all available metrics and their definitions for storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.559546 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.542755 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 3 | 0.542736 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.541840 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.535998 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.527261 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.459788 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.447432 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 9 | 0.432639 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.414439 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 97

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** What metric definitions are available for the Application Insights resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.526496 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 2 | 0.408026 | `azmcp-monitor-metrics-query` | ❌ |
| 3 | 0.370848 | `azmcp-monitor-table-type-list` | ❌ |
| 4 | 0.342946 | `azmcp-monitor-resource-log-query` | ❌ |
| 5 | 0.329534 | `azmcp-loadtesting-testresource-list` | ❌ |
| 6 | 0.325097 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.313858 | `azmcp-search-index-list` | ❌ |
| 8 | 0.308315 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.302823 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.301966 | `azmcp-workbooks-show` | ❌ |

---

## Test 98

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Analyze the performance trends and response times for Application Insights resource <resource_name> over the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.453881 | `azmcp-monitor-resource-log-query` | ❌ |
| 2 | 0.439684 | `azmcp-loadtesting-testrun-get` | ❌ |
| 3 | 0.434042 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 4 | 0.404582 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.362649 | `azmcp-monitor-metrics-definitions` | ❌ |
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
| 1 | 0.451288 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.411471 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.396652 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.356326 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.342298 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 6 | 0.341525 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.338108 | `azmcp-search-index-list` | ❌ |
| 8 | 0.326899 | `azmcp-loadtesting-testresource-list` | ❌ |
| 9 | 0.302312 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.292483 | `azmcp-search-service-list` | ❌ |

---

## Test 100

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Get the <aggregation_type> <metric_name> metric for <resource_type> <resource_name> over the last <time_period> with intervals  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.415471 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.397693 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.306210 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.279473 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.275289 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.269434 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.269301 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 8 | 0.259129 | `azmcp-grafana-list` | ❌ |
| 9 | 0.249833 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.248770 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 101

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Investigate error rates and failed requests for Application Insights resource <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.420194 | `azmcp-monitor-resource-log-query` | ❌ |
| 2 | 0.384781 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 3 | 0.368342 | `azmcp-loadtesting-testrun-get` | ❌ |
| 4 | 0.354940 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.325741 | `azmcp-monitor-metrics-definitions` | ❌ |
| 6 | 0.316302 | `azmcp-loadtesting-testresource-list` | ❌ |
| 7 | 0.299707 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.293311 | `azmcp-loadtesting-testresource-create` | ❌ |
| 9 | 0.292603 | `azmcp-search-index-list` | ❌ |
| 10 | 0.283523 | `azmcp-extension-azqr` | ❌ |

---

## Test 102

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Query the <metric_name> metric for <resource_type> <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.477344 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.429670 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.385940 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.362063 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.298357 | `azmcp-search-index-query` | ❌ |
| 6 | 0.293060 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.288148 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 8 | 0.280325 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 9 | 0.272349 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.269058 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 103

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** What's the request per second rate for my Application Insights resource <resource_name> over the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.351252 | `azmcp-monitor-resource-log-query` | ❌ |
| 2 | 0.346123 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 3 | 0.341334 | `azmcp-monitor-workspace-log-query` | ❌ |
| 4 | 0.331215 | `azmcp-loadtesting-testresource-list` | ❌ |
| 5 | 0.327098 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.319343 | `azmcp-loadtesting-testresource-create` | ❌ |
| 7 | 0.314311 | `azmcp-monitor-metrics-definitions` | ❌ |
| 8 | 0.278490 | `azmcp-workbooks-show` | ❌ |
| 9 | 0.277129 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.269556 | `azmcp-search-index-list` | ❌ |

---

## Test 104

**Expected Tool:** `azmcp-monitor-resource-log-query`  
**Prompt:** Show me the logs for the past hour for the resource <resource_name> in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584906 | `azmcp-monitor-workspace-log-query` | ❌ |
| 2 | 0.582691 | `azmcp-monitor-resource-log-query` | ✅ **EXPECTED** |
| 3 | 0.443468 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.442971 | `azmcp-monitor-table-list` | ❌ |
| 5 | 0.416553 | `azmcp-monitor-metrics-query` | ❌ |
| 6 | 0.392377 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.390022 | `azmcp-grafana-list` | ❌ |
| 8 | 0.358681 | `azmcp-monitor-metrics-definitions` | ❌ |
| 9 | 0.355135 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 10 | 0.341502 | `azmcp-search-index-list` | ❌ |

---

## Test 105

**Expected Tool:** `azmcp-monitor-table-list`  
**Prompt:** List all tables in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.851114 | `azmcp-monitor-table-list` | ✅ **EXPECTED** |
| 2 | 0.725716 | `azmcp-monitor-table-type-list` | ❌ |
| 3 | 0.620481 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.586647 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.512074 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.502162 | `azmcp-grafana-list` | ❌ |
| 7 | 0.488586 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.436231 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.434982 | `azmcp-search-index-list` | ❌ |
| 10 | 0.420389 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 106

**Expected Tool:** `azmcp-monitor-table-list`  
**Prompt:** Show me the tables in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.798459 | `azmcp-monitor-table-list` | ✅ **EXPECTED** |
| 2 | 0.701122 | `azmcp-monitor-table-type-list` | ❌ |
| 3 | 0.599917 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.532887 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.487237 | `azmcp-grafana-list` | ❌ |
| 6 | 0.468496 | `azmcp-kusto-table-list` | ❌ |
| 7 | 0.441635 | `azmcp-monitor-workspace-log-query` | ❌ |
| 8 | 0.427433 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.424176 | `azmcp-kusto-table-schema` | ❌ |
| 10 | 0.414189 | `azmcp-monitor-resource-log-query` | ❌ |

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
| 4 | 0.525468 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.477280 | `azmcp-grafana-list` | ❌ |
| 6 | 0.442184 | `azmcp-kusto-table-list` | ❌ |
| 7 | 0.428810 | `azmcp-kusto-table-schema` | ❌ |
| 8 | 0.418519 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.401662 | `azmcp-kusto-sample` | ❌ |
| 10 | 0.394213 | `azmcp-monitor-workspace-log-query` | ❌ |

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
| 4 | 0.502460 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.475734 | `azmcp-grafana-list` | ❌ |
| 6 | 0.437988 | `azmcp-kusto-table-schema` | ❌ |
| 7 | 0.417759 | `azmcp-kusto-table-list` | ❌ |
| 8 | 0.416739 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.411568 | `azmcp-kusto-sample` | ❌ |
| 10 | 0.381376 | `azmcp-monitor-resource-log-query` | ❌ |

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
| 7 | 0.517493 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.513548 | `azmcp-aks-cluster-list` | ❌ |
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
| 8 | 0.384281 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.383433 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.379597 | `azmcp-storage-table-list` | ❌ |

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
| 9 | 0.428945 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.427132 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 112

**Expected Tool:** `azmcp-monitor-workspace-log-query`  
**Prompt:** Show me the logs for the past hour in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.581662 | `azmcp-monitor-workspace-log-query` | ✅ **EXPECTED** |
| 2 | 0.500599 | `azmcp-monitor-resource-log-query` | ❌ |
| 3 | 0.485984 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.483323 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.427241 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.365704 | `azmcp-grafana-list` | ❌ |
| 7 | 0.339692 | `azmcp-search-index-list` | ❌ |
| 8 | 0.328415 | `azmcp-monitor-metrics-query` | ❌ |
| 9 | 0.318833 | `azmcp-workbooks-delete` | ❌ |
| 10 | 0.309810 | `azmcp-loadtesting-testrun-get` | ❌ |

---

## Test 113

**Expected Tool:** `azmcp-datadog-monitoredresources-list`  
**Prompt:** List all monitored resources in the Datadog resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.666628 | `azmcp-datadog-monitoredresources-list` | ✅ **EXPECTED** |
| 2 | 0.434813 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.409198 | `azmcp-monitor-metrics-definitions` | ❌ |
| 4 | 0.408658 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.401731 | `azmcp-grafana-list` | ❌ |
| 6 | 0.381429 | `azmcp-monitor-metrics-query` | ❌ |
| 7 | 0.369805 | `azmcp-redis-cluster-database-list` | ❌ |
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
| 3 | 0.385569 | `azmcp-monitor-metrics-definitions` | ❌ |
| 4 | 0.378703 | `azmcp-monitor-metrics-query` | ❌ |
| 5 | 0.374071 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.371017 | `azmcp-grafana-list` | ❌ |
| 7 | 0.343214 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.342468 | `azmcp-redis-cluster-database-list` | ❌ |
| 9 | 0.319895 | `azmcp-workbooks-list` | ❌ |
| 10 | 0.306863 | `azmcp-monitor-resource-log-query` | ❌ |

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
| 4 | 0.427495 | `azmcp-search-service-list` | ❌ |
| 5 | 0.426190 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 6 | 0.423237 | `azmcp-subscription-list` | ❌ |
| 7 | 0.388980 | `azmcp-monitor-workspace-list` | ❌ |
| 8 | 0.365968 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.359574 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.354341 | `azmcp-redis-cache-list` | ❌ |

---

## Test 116

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Provide compliance recommendations for my current Azure subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.527053 | `azmcp-bestpractices-get` | ❌ |
| 2 | 0.487764 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.473977 | `azmcp-extension-az` | ❌ |
| 4 | 0.462791 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 5 | 0.382455 | `azmcp-search-service-list` | ❌ |
| 6 | 0.375754 | `azmcp-subscription-list` | ❌ |
| 7 | 0.338365 | `azmcp-marketplace-product-get` | ❌ |
| 8 | 0.333631 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.331088 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 10 | 0.316628 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 117

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Scan my Azure subscription for compliance recommendations  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.516925 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 2 | 0.514791 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.490409 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 4 | 0.472526 | `azmcp-extension-az` | ❌ |
| 5 | 0.450091 | `azmcp-search-service-list` | ❌ |
| 6 | 0.423512 | `azmcp-subscription-list` | ❌ |
| 7 | 0.398621 | `azmcp-monitor-workspace-list` | ❌ |
| 8 | 0.389830 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.371590 | `azmcp-redis-cluster-list` | ❌ |
| 10 | 0.369448 | `azmcp-redis-cache-list` | ❌ |

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
| 4 | 0.483125 | `azmcp-subscription-list` | ❌ |
| 5 | 0.478700 | `azmcp-grafana-list` | ❌ |
| 6 | 0.474796 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.471364 | `azmcp-cosmos-account-list` | ❌ |
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
| 1 | 0.609704 | `azmcp-role-assignment-list` | ✅ **EXPECTED** |
| 2 | 0.456956 | `azmcp-grafana-list` | ❌ |
| 3 | 0.436747 | `azmcp-subscription-list` | ❌ |
| 4 | 0.435642 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.435287 | `azmcp-search-service-list` | ❌ |
| 6 | 0.435155 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.428663 | `azmcp-group-list` | ❌ |
| 8 | 0.428370 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.420804 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.410380 | `azmcp-redis-cache-accesspolicy-list` | ❌ |

---

## Test 120

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
| 9 | 0.431968 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.431715 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 121

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
| 7 | 0.262105 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.257555 | `azmcp-appconfig-account-list` | ❌ |
| 9 | 0.252070 | `azmcp-grafana-list` | ❌ |
| 10 | 0.246445 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 122

**Expected Tool:** `azmcp-redis-cache-list`  
**Prompt:** Show me the Redis Caches in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.692209 | `azmcp-redis-cache-list` | ✅ **EXPECTED** |
| 2 | 0.595721 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.461603 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 4 | 0.434924 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.427325 | `azmcp-grafana-list` | ❌ |
| 6 | 0.399303 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.383383 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.382294 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.368549 | `azmcp-search-service-list` | ❌ |
| 10 | 0.361735 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 123

**Expected Tool:** `azmcp-redis-cache-list-accesspolicy`  
**Prompt:** List all access policies in the Redis Cache <cache_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.757057 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 2 | 0.565047 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.445073 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.377563 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.312428 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.307404 | `azmcp-keyvault-secret-list` | ❌ |
| 7 | 0.303736 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.303531 | `azmcp-appconfig-kv-list` | ❌ |
| 9 | 0.300024 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.298380 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 124

**Expected Tool:** `azmcp-redis-cache-list-accesspolicy`  
**Prompt:** Show me the access policies in the Redis Cache <cache_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713839 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 2 | 0.523153 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.412377 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.338859 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.300045 | `azmcp-bestpractices-get` | ❌ |
| 6 | 0.293441 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.286321 | `azmcp-appconfig-kv-list` | ❌ |
| 8 | 0.285163 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.258045 | `azmcp-appconfig-account-list` | ❌ |
| 10 | 0.257151 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 125

**Expected Tool:** `azmcp-redis-cluster-database-list`  
**Prompt:** List all databases in the Redis Cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.752920 | `azmcp-redis-cluster-database-list` | ✅ **EXPECTED** |
| 2 | 0.603780 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.592889 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.548268 | `azmcp-postgres-database-list` | ❌ |
| 5 | 0.538403 | `azmcp-cosmos-database-list` | ❌ |
| 6 | 0.471359 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.460313 | `azmcp-kusto-table-list` | ❌ |
| 8 | 0.458244 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.449548 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.419697 | `azmcp-postgres-table-list` | ❌ |

---

## Test 126

**Expected Tool:** `azmcp-redis-cluster-database-list`  
**Prompt:** Show me the databases in the Redis Cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.721506 | `azmcp-redis-cluster-database-list` | ✅ **EXPECTED** |
| 2 | 0.562860 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.537698 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.481618 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.480274 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.434940 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.419032 | `azmcp-kusto-table-list` | ❌ |
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
| 5 | 0.569125 | `azmcp-aks-cluster-list` | ❌ |
| 6 | 0.554298 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.522070 | `azmcp-kusto-database-list` | ❌ |
| 8 | 0.503279 | `azmcp-grafana-list` | ❌ |
| 9 | 0.467957 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.463770 | `azmcp-search-service-list` | ❌ |

---

## Test 128

**Expected Tool:** `azmcp-redis-cluster-list`  
**Prompt:** Show me my Redis Clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591593 | `azmcp-redis-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.514374 | `azmcp-redis-cluster-database-list` | ❌ |
| 3 | 0.467519 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.403281 | `azmcp-kusto-cluster-list` | ❌ |
| 5 | 0.385069 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 6 | 0.367907 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.329389 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.318440 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.301322 | `azmcp-aks-cluster-get` | ❌ |
| 10 | 0.297699 | `azmcp-kusto-cluster-get` | ❌ |

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
| 6 | 0.491245 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.456252 | `azmcp-grafana-list` | ❌ |
| 8 | 0.435203 | `azmcp-kusto-cluster-get` | ❌ |
| 9 | 0.434680 | `azmcp-kusto-database-list` | ❌ |
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
| 9 | 0.345595 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.343018 | `azmcp-sql-elastic-pool-list` | ❌ |

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
| 1 | 0.651319 | `azmcp-servicebus-queue-details` | ✅ **EXPECTED** |
| 2 | 0.459209 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.434862 | `azmcp-servicebus-topic-details` | ❌ |
| 4 | 0.375437 | `azmcp-aks-cluster-get` | ❌ |
| 5 | 0.338738 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.337239 | `azmcp-sql-db-show` | ❌ |
| 7 | 0.335606 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.330561 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.330090 | `azmcp-search-index-list` | ❌ |
| 10 | 0.308567 | `azmcp-redis-cache-list` | ❌ |

---

## Test 134

**Expected Tool:** `azmcp-servicebus-topic-details`  
**Prompt:** Show me the details of service bus <service_bus_name> topic <topic_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.612731 | `azmcp-servicebus-topic-details` | ✅ **EXPECTED** |
| 2 | 0.574218 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.500092 | `azmcp-servicebus-queue-details` | ❌ |
| 4 | 0.361384 | `azmcp-aks-cluster-get` | ❌ |
| 5 | 0.348999 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.347044 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.340036 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.324869 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.323227 | `azmcp-search-index-list` | ❌ |
| 10 | 0.317549 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 135

**Expected Tool:** `azmcp-servicebus-topic-subscription-details`  
**Prompt:** Show me the details of service bus <service_bus_name> subscription <subscription_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.629474 | `azmcp-servicebus-topic-subscription-details` | ✅ **EXPECTED** |
| 2 | 0.506577 | `azmcp-servicebus-queue-details` | ❌ |
| 3 | 0.486684 | `azmcp-servicebus-topic-details` | ❌ |
| 4 | 0.449818 | `azmcp-search-service-list` | ❌ |
| 5 | 0.435097 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.429458 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.421009 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.409798 | `azmcp-aks-cluster-list` | ❌ |
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
| 3 | 0.602889 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.529998 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.482736 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.474927 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.466130 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.464525 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.459406 | `azmcp-kusto-table-list` | ❌ |
| 10 | 0.457219 | `azmcp-postgres-server-list` | ❌ |

---

## Test 137

**Expected Tool:** `azmcp-sql-db-list`  
**Prompt:** Show me all the databases configuration details in the Azure SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609394 | `azmcp-sql-db-list` | ✅ **EXPECTED** |
| 2 | 0.524351 | `azmcp-sql-db-show` | ❌ |
| 3 | 0.471862 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.461750 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.458660 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.454368 | `azmcp-sql-elastic-pool-list` | ❌ |
| 7 | 0.394395 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.387690 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.387411 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.380452 | `azmcp-appconfig-account-list` | ❌ |

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
| 5 | 0.374152 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 6 | 0.371766 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.354111 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 8 | 0.348227 | `azmcp-sql-elastic-pool-list` | ❌ |
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
| 8 | 0.349152 | `azmcp-kusto-table-schema` | ❌ |
| 9 | 0.343370 | `azmcp-postgres-table-list` | ❌ |
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
| 3 | 0.434570 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.431871 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 5 | 0.431174 | `azmcp-cosmos-database-list` | ❌ |
| 6 | 0.416273 | `azmcp-monitor-table-list` | ❌ |
| 7 | 0.414738 | `azmcp-postgres-database-list` | ❌ |
| 8 | 0.412096 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 9 | 0.409078 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.408053 | `azmcp-storage-table-list` | ❌ |

---

## Test 141

**Expected Tool:** `azmcp-sql-elastic-pool-list`  
**Prompt:** Show me the elastic pools configured for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.616579 | `azmcp-sql-elastic-pool-list` | ✅ **EXPECTED** |
| 2 | 0.457163 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.385834 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 4 | 0.378556 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.357705 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 6 | 0.357019 | `azmcp-postgres-server-config` | ❌ |
| 7 | 0.354094 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.335615 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.324219 | `azmcp-search-index-list` | ❌ |
| 10 | 0.323509 | `azmcp-monitor-table-type-list` | ❌ |

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
| 4 | 0.344799 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.316044 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 6 | 0.311302 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.308077 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.307724 | `azmcp-storage-table-list` | ❌ |
| 9 | 0.306214 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.298933 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 143

**Expected Tool:** `azmcp-sql-firewall-rule-list`  
**Prompt:** List all firewall rules for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.732317 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 2 | 0.397092 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.385148 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.359228 | `azmcp-sql-db-list` | ❌ |
| 5 | 0.347004 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.327808 | `azmcp-postgres-database-list` | ❌ |
| 7 | 0.317728 | `azmcp-search-index-list` | ❌ |
| 8 | 0.304958 | `azmcp-keyvault-secret-list` | ❌ |
| 9 | 0.304175 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.301667 | `azmcp-postgres-table-list` | ❌ |

---

## Test 144

**Expected Tool:** `azmcp-sql-firewall-rule-list`  
**Prompt:** Show me the firewall rules for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.631522 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 2 | 0.321400 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.312057 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.290274 | `azmcp-extension-az` | ❌ |
| 5 | 0.290213 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.287719 | `azmcp-postgres-server-param` | ❌ |
| 7 | 0.276173 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.272567 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.272059 | `azmcp-sql-db-show` | ❌ |
| 10 | 0.255335 | `azmcp-bestpractices-get` | ❌ |

---

## Test 145

**Expected Tool:** `azmcp-sql-firewall-rule-list`  
**Prompt:** What firewall rules are configured for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633663 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 2 | 0.311867 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.299474 | `azmcp-extension-az` | ❌ |
| 4 | 0.277628 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.262028 | `azmcp-sql-db-list` | ❌ |
| 6 | 0.261404 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.261123 | `azmcp-postgres-server-param` | ❌ |
| 8 | 0.258402 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.247516 | `azmcp-bestpractices-get` | ❌ |
| 10 | 0.227217 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 146

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** List Microsoft Entra ID administrators for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.788356 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.407501 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 3 | 0.376055 | `azmcp-sql-db-list` | ❌ |
| 4 | 0.365636 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.328968 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.328737 | `azmcp-role-assignment-list` | ❌ |
| 7 | 0.312627 | `azmcp-postgres-database-list` | ❌ |
| 8 | 0.280450 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.279198 | `azmcp-sql-db-show` | ❌ |
| 10 | 0.277773 | `azmcp-storage-table-list` | ❌ |

---

## Test 147

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** Show me the Entra ID administrators configured for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.718251 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.315966 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.311085 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.309133 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 5 | 0.303560 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.268897 | `azmcp-sql-elastic-pool-list` | ❌ |
| 7 | 0.266264 | `azmcp-postgres-server-param` | ❌ |
| 8 | 0.250838 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.249616 | `azmcp-postgres-database-list` | ❌ |
| 10 | 0.228064 | `azmcp-role-assignment-list` | ❌ |

---

## Test 148

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** What Microsoft Entra ID administrators are set up for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651306 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.253610 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.244772 | `azmcp-extension-az` | ❌ |
| 4 | 0.229560 | `azmcp-sql-elastic-pool-list` | ❌ |
| 5 | 0.228161 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 6 | 0.217698 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.205654 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.199907 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.191356 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.189581 | `azmcp-storage-table-list` | ❌ |

---

## Test 149

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** List all storage accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.790279 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.652745 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.641184 | `azmcp-cosmos-account-list` | ❌ |
| 4 | 0.628090 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.562982 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.554773 | `azmcp-subscription-list` | ❌ |
| 7 | 0.539741 | `azmcp-search-service-list` | ❌ |
| 8 | 0.531542 | `azmcp-appconfig-account-list` | ❌ |
| 9 | 0.497662 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.496435 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 150

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** Show me my storage accounts  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.574615 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.560516 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.505688 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.483330 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.456588 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.431657 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.405218 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 8 | 0.402931 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.393156 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.375146 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 151

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** Show me the storage accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.701826 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.595460 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.583848 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.574454 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.504700 | `azmcp-subscription-list` | ❌ |
| 6 | 0.502734 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.490259 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.480775 | `azmcp-search-service-list` | ❌ |
| 9 | 0.472516 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.437717 | `azmcp-redis-cache-list` | ❌ |

---

## Test 152

**Expected Tool:** `azmcp-storage-blob-batch-set-tier`  
**Prompt:** Set access tier to Cool for multiple blobs in the container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.710392 | `azmcp-storage-blob-batch-set-tier` | ✅ **EXPECTED** |
| 2 | 0.485345 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.460510 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.449409 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.382202 | `azmcp-cosmos-database-container-list` | ❌ |
| 6 | 0.327160 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.325885 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.325396 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 9 | 0.305955 | `azmcp-appconfig-kv-unlock` | ❌ |
| 10 | 0.304977 | `azmcp-appconfig-kv-lock` | ❌ |

---

## Test 153

**Expected Tool:** `azmcp-storage-blob-batch-set-tier`  
**Prompt:** Change the access tier to Archive for blobs file1.txt and file2.txt in the container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.632605 | `azmcp-storage-blob-batch-set-tier` | ✅ **EXPECTED** |
| 2 | 0.444632 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.429745 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.421357 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.350730 | `azmcp-cosmos-database-container-list` | ❌ |
| 6 | 0.347694 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.335961 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.303923 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 9 | 0.292426 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.292070 | `azmcp-appconfig-kv-lock` | ❌ |

---

## Test 154

**Expected Tool:** `azmcp-storage-blob-container-details`  
**Prompt:** Show me the properties of the storage container files in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.678011 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.678008 | `azmcp-storage-blob-container-details` | ✅ **EXPECTED** |
| 3 | 0.611693 | `azmcp-storage-blob-list` | ❌ |
| 4 | 0.537937 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.530648 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.515548 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.452341 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 8 | 0.437454 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.432791 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.407465 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 155

**Expected Tool:** `azmcp-storage-blob-container-list`  
**Prompt:** List all blob containers in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.764995 | `azmcp-storage-blob-container-list` | ✅ **EXPECTED** |
| 2 | 0.743500 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.629987 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.557011 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.554192 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.540541 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.468593 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.460731 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.418697 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 10 | 0.414549 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

---

## Test 156

**Expected Tool:** `azmcp-storage-blob-container-list`  
**Prompt:** Show me the blob containers in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.697693 | `azmcp-storage-blob-container-list` | ✅ **EXPECTED** |
| 2 | 0.673822 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.578331 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.539113 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.505990 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.504109 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.447566 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.410884 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.392129 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 10 | 0.380842 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

---

## Test 157

**Expected Tool:** `azmcp-storage-blob-list`  
**Prompt:** List all blobs in the blob container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.795420 | `azmcp-storage-blob-list` | ✅ **EXPECTED** |
| 2 | 0.715105 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.596810 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.585629 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.535735 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.524098 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.449480 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.448011 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 9 | 0.429272 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 10 | 0.423178 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 158

**Expected Tool:** `azmcp-storage-blob-list`  
**Prompt:** Show me the blobs in the blob container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.701477 | `azmcp-storage-blob-list` | ✅ **EXPECTED** |
| 2 | 0.649625 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.555682 | `azmcp-storage-blob-container-details` | ❌ |
| 4 | 0.550249 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.476647 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.447605 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.418165 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 8 | 0.413225 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 9 | 0.401214 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.383449 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

---

## Test 159

**Expected Tool:** `azmcp-storage-datalake-directory-create`  
**Prompt:** Create a new directory at the path <directory_path> in Data Lake in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661244 | `azmcp-storage-datalake-directory-create` | ✅ **EXPECTED** |
| 2 | 0.487554 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 3 | 0.384985 | `azmcp-keyvault-secret-create` | ❌ |
| 4 | 0.373153 | `azmcp-keyvault-certificate-create` | ❌ |
| 5 | 0.372044 | `azmcp-storage-blob-container-list` | ❌ |
| 6 | 0.366135 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.332103 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.317865 | `azmcp-storage-blob-list` | ❌ |
| 9 | 0.315631 | `azmcp-loadtesting-testresource-create` | ❌ |
| 10 | 0.310939 | `azmcp-loadtesting-test-create` | ❌ |

---

## Test 160

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** List all paths in the Data Lake file system <file_system_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.860109 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.493101 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.486377 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.478279 | `azmcp-storage-datalake-directory-create` | ❌ |
| 5 | 0.476282 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.461277 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.423757 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.414341 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.402734 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.401558 | `azmcp-monitor-table-list` | ❌ |

---

## Test 161

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** Show me the paths in the Data Lake file system <file_system_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.804437 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.464603 | `azmcp-storage-datalake-directory-create` | ❌ |
| 3 | 0.438075 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.436104 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.413277 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.396638 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.368149 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.353149 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.351701 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.344977 | `azmcp-monitor-table-type-list` | ❌ |

---

## Test 162

**Expected Tool:** `azmcp-storage-table-list`  
**Prompt:** List all tables in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.790094 | `azmcp-storage-table-list` | ✅ **EXPECTED** |
| 2 | 0.620113 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.584417 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.561794 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.553627 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.513277 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.511143 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.504759 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.496198 | `azmcp-kusto-table-list` | ❌ |
| 10 | 0.492250 | `azmcp-postgres-table-list` | ❌ |

---

## Test 163

**Expected Tool:** `azmcp-storage-table-list`  
**Prompt:** Show me the tables in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.746243 | `azmcp-storage-table-list` | ✅ **EXPECTED** |
| 2 | 0.600426 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.532359 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.528309 | `azmcp-monitor-table-list` | ❌ |
| 5 | 0.524642 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.490488 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.489228 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.472357 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.467736 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.463396 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 164

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** List all subscriptions for my account  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.576055 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.512963 | `azmcp-cosmos-account-list` | ❌ |
| 3 | 0.489578 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.473852 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.471653 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.470819 | `azmcp-search-service-list` | ❌ |
| 7 | 0.450973 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.445724 | `azmcp-grafana-list` | ❌ |
| 9 | 0.436338 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.431337 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 165

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
| 7 | 0.315604 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.308874 | `azmcp-appconfig-account-list` | ❌ |
| 9 | 0.303528 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.297209 | `azmcp-group-list` | ❌ |

---

## Test 166

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** What is my current subscription?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.319958 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.298352 | `azmcp-marketplace-product-get` | ❌ |
| 3 | 0.286711 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.285063 | `azmcp-search-service-list` | ❌ |
| 5 | 0.282645 | `azmcp-grafana-list` | ❌ |
| 6 | 0.279702 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.278798 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.256358 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.252504 | `azmcp-loadtesting-testresource-list` | ❌ |
| 10 | 0.250314 | `azmcp-servicebus-topic-subscription-details` | ❌ |

---

## Test 167

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
| 7 | 0.304965 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.303629 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 9 | 0.294080 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.291826 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 168

**Expected Tool:** `azmcp-azureterraformbestpractices-get`  
**Prompt:** Fetch the Azure Terraform best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.719984 | `azmcp-azureterraformbestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.658343 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.459270 | `azmcp-extension-az` | ❌ |
| 4 | 0.354838 | `azmcp-bicepschema-get` | ❌ |
| 5 | 0.331791 | `azmcp-extension-azd` | ❌ |
| 6 | 0.309265 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.302784 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.295828 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.295781 | `azmcp-extension-azqr` | ❌ |
| 10 | 0.293925 | `azmcp-foundry-models-list` | ❌ |

---

## Test 169

**Expected Tool:** `azmcp-azureterraformbestpractices-get`  
**Prompt:** Show me the Azure Terraform best practices and generate code sample to get a secret from Azure Key Vault  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.596296 | `azmcp-azureterraformbestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.551618 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.439871 | `azmcp-keyvault-secret-list` | ❌ |
| 4 | 0.438956 | `azmcp-keyvault-secret-create` | ❌ |
| 5 | 0.428895 | `azmcp-keyvault-certificate-get` | ❌ |
| 6 | 0.406432 | `azmcp-extension-az` | ❌ |
| 7 | 0.389450 | `azmcp-keyvault-key-list` | ❌ |
| 8 | 0.381307 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.378624 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.374825 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 170

**Expected Tool:** `azmcp-workbooks-create`  
**Prompt:** Create a new workbook named <workbook_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.553572 | `azmcp-workbooks-create` | ✅ **EXPECTED** |
| 2 | 0.433162 | `azmcp-workbooks-update` | ❌ |
| 3 | 0.365578 | `azmcp-workbooks-delete` | ❌ |
| 4 | 0.361215 | `azmcp-workbooks-show` | ❌ |
| 5 | 0.328113 | `azmcp-workbooks-list` | ❌ |
| 6 | 0.240128 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.217233 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.214818 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.188137 | `azmcp-loadtesting-testresource-create` | ❌ |
| 10 | 0.172751 | `azmcp-monitor-table-list` | ❌ |

---

## Test 171

**Expected Tool:** `azmcp-workbooks-delete`  
**Prompt:** Delete the workbook with resource ID <workbook_resource_id>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.624673 | `azmcp-workbooks-delete` | ✅ **EXPECTED** |
| 2 | 0.518630 | `azmcp-workbooks-show` | ❌ |
| 3 | 0.429901 | `azmcp-workbooks-create` | ❌ |
| 4 | 0.425569 | `azmcp-workbooks-list` | ❌ |
| 5 | 0.390355 | `azmcp-workbooks-update` | ❌ |
| 6 | 0.273939 | `azmcp-grafana-list` | ❌ |
| 7 | 0.198585 | `azmcp-appconfig-kv-delete` | ❌ |
| 8 | 0.186811 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.186572 | `azmcp-monitor-workspace-log-query` | ❌ |
| 10 | 0.157219 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 172

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
| 6 | 0.488599 | `azmcp-group-list` | ❌ |
| 7 | 0.454315 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.454210 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.416566 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.413409 | `azmcp-sql-db-list` | ❌ |

---

## Test 173

**Expected Tool:** `azmcp-workbooks-list`  
**Prompt:** What workbooks do I have in resource group <resource_group_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.708612 | `azmcp-workbooks-list` | ✅ **EXPECTED** |
| 2 | 0.566593 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.539957 | `azmcp-workbooks-show` | ❌ |
| 4 | 0.488377 | `azmcp-workbooks-delete` | ❌ |
| 5 | 0.472378 | `azmcp-grafana-list` | ❌ |
| 6 | 0.428025 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.423028 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.421646 | `azmcp-group-list` | ❌ |
| 9 | 0.392371 | `azmcp-loadtesting-testresource-list` | ❌ |
| 10 | 0.371128 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 174

**Expected Tool:** `azmcp-workbooks-show`  
**Prompt:** Get information about the workbook with resource ID <workbook_resource_id>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.697539 | `azmcp-workbooks-show` | ✅ **EXPECTED** |
| 2 | 0.503906 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.494708 | `azmcp-workbooks-list` | ❌ |
| 4 | 0.457252 | `azmcp-workbooks-delete` | ❌ |
| 5 | 0.419105 | `azmcp-workbooks-update` | ❌ |
| 6 | 0.353546 | `azmcp-grafana-list` | ❌ |
| 7 | 0.235477 | `azmcp-marketplace-product-get` | ❌ |
| 8 | 0.234855 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.227558 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.226385 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 175

**Expected Tool:** `azmcp-workbooks-show`  
**Prompt:** Show me the workbook with display name <workbook_display_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.469476 | `azmcp-workbooks-show` | ✅ **EXPECTED** |
| 2 | 0.453028 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.437638 | `azmcp-workbooks-update` | ❌ |
| 4 | 0.424338 | `azmcp-workbooks-list` | ❌ |
| 5 | 0.371623 | `azmcp-workbooks-delete` | ❌ |
| 6 | 0.292898 | `azmcp-grafana-list` | ❌ |
| 7 | 0.266530 | `azmcp-monitor-table-list` | ❌ |
| 8 | 0.239907 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.227383 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.198220 | `azmcp-search-index-list` | ❌ |

---

## Test 176

**Expected Tool:** `azmcp-workbooks-update`  
**Prompt:** Update the workbook <workbook_resource_id> with a new text step  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.469915 | `azmcp-workbooks-update` | ✅ **EXPECTED** |
| 2 | 0.384366 | `azmcp-workbooks-create` | ❌ |
| 3 | 0.362354 | `azmcp-workbooks-show` | ❌ |
| 4 | 0.351698 | `azmcp-workbooks-delete` | ❌ |
| 5 | 0.283187 | `azmcp-loadtesting-testrun-update` | ❌ |
| 6 | 0.262873 | `azmcp-workbooks-list` | ❌ |
| 7 | 0.170118 | `azmcp-grafana-list` | ❌ |
| 8 | 0.144812 | `azmcp-extension-az` | ❌ |
| 9 | 0.139542 | `azmcp-appconfig-kv-set` | ❌ |
| 10 | 0.138420 | `azmcp-loadtesting-testrun-create` | ❌ |

---

## Test 177

**Expected Tool:** `azmcp-bicepschema-get`  
**Prompt:** How can I use Bicep to create an Azure OpenAI service?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.432409 | `azmcp-bicepschema-get` | ✅ **EXPECTED** |
| 2 | 0.401162 | `azmcp-extension-az` | ❌ |
| 3 | 0.400985 | `azmcp-foundry-models-deploy` | ❌ |
| 4 | 0.394677 | `azmcp-bestpractices-get` | ❌ |
| 5 | 0.375156 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 6 | 0.345030 | `azmcp-search-service-list` | ❌ |
| 7 | 0.342237 | `azmcp-foundry-models-deployments-list` | ❌ |
| 8 | 0.341619 | `azmcp-search-index-list` | ❌ |
| 9 | 0.331160 | `azmcp-search-index-query` | ❌ |
| 10 | 0.320686 | `azmcp-extension-azd` | ❌ |

---

## Summary

**Total Prompts Tested:** 177  
**Execution Time:** 95.4774860s  

### Success Rate Metrics

**Top Choice Success:** 79.7% (141/177 tests)  
**High Confidence (≥0.5):** 79.7% (141/177 tests)  
**Top Choice + High Confidence:** 72.3% (128/177 tests)  

### Success Rate Analysis

🟠 **Fair** - The tool selection system needs significant improvement.

