# Tool Selection Analysis Setup

**Setup completed:** 2025-07-21 00:25:57  
**Tool count:** 85  
**Database setup time:** 1.5386687s  

---

# Tool Selection Analysis Results

**Analysis Date:** 2025-07-21 00:25:57  
**Tool count:** 85  

## Table of Contents

- [Test 1: azmcp-foundry-models-list](#test-1)
- [Test 2: azmcp-foundry-models-list](#test-2)
- [Test 3: azmcp-foundry-models-deploy](#test-3)
- [Test 4: azmcp-foundry-models-deployments-list](#test-4)
- [Test 5: azmcp-foundry-models-deployments-list](#test-5)
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
- [Test 59: azmcp-keyvault-key-create](#test-59)
- [Test 60: azmcp-keyvault-key-get](#test-60)
- [Test 61: azmcp-keyvault-key-list](#test-61)
- [Test 62: azmcp-keyvault-key-list](#test-62)
- [Test 63: azmcp-keyvault-secret-get](#test-63)
- [Test 64: azmcp-aks-cluster-list](#test-64)
- [Test 65: azmcp-aks-cluster-list](#test-65)
- [Test 66: azmcp-aks-cluster-list](#test-66)
- [Test 67: azmcp-loadtesting-test-create](#test-67)
- [Test 68: azmcp-loadtesting-test-get](#test-68)
- [Test 69: azmcp-loadtesting-testresource-create](#test-69)
- [Test 70: azmcp-loadtesting-testresource-list](#test-70)
- [Test 71: azmcp-loadtesting-testrun-create](#test-71)
- [Test 72: azmcp-loadtesting-testrun-get](#test-72)
- [Test 73: azmcp-loadtesting-testrun-list](#test-73)
- [Test 74: azmcp-loadtesting-testrun-update](#test-74)
- [Test 75: azmcp-grafana-list](#test-75)
- [Test 76: azmcp-marketplace-product-get](#test-76)
- [Test 77: azmcp-bestpractices-azurefunctions-get-code-generation](#test-77)
- [Test 78: azmcp-bestpractices-azurefunctions-get-deployment](#test-78)
- [Test 79: azmcp-bestpractices-general-get](#test-79)
- [Test 80: azmcp-bestpractices-general-get](#test-80)
- [Test 81: azmcp-tool-list](#test-81)
- [Test 82: azmcp-tool-list](#test-82)
- [Test 83: azmcp-monitor-healthmodels-entity-gethealth](#test-83)
- [Test 84: azmcp-monitor-metrics-definitions](#test-84)
- [Test 85: azmcp-monitor-metrics-definitions](#test-85)
- [Test 86: azmcp-monitor-metrics-definitions](#test-86)
- [Test 87: azmcp-monitor-metrics-query](#test-87)
- [Test 88: azmcp-monitor-metrics-query](#test-88)
- [Test 89: azmcp-monitor-metrics-query](#test-89)
- [Test 90: azmcp-monitor-metrics-query](#test-90)
- [Test 91: azmcp-monitor-metrics-query](#test-91)
- [Test 92: azmcp-monitor-metrics-query](#test-92)
- [Test 93: azmcp-monitor-resource-log-query](#test-93)
- [Test 94: azmcp-monitor-table-list](#test-94)
- [Test 95: azmcp-monitor-table-list](#test-95)
- [Test 96: azmcp-monitor-table-type-list](#test-96)
- [Test 97: azmcp-monitor-table-type-list](#test-97)
- [Test 98: azmcp-monitor-workspace-list](#test-98)
- [Test 99: azmcp-monitor-workspace-list](#test-99)
- [Test 100: azmcp-monitor-workspace-list](#test-100)
- [Test 101: azmcp-monitor-workspace-log-query](#test-101)
- [Test 102: azmcp-datadog-monitoredresources-list](#test-102)
- [Test 103: azmcp-datadog-monitoredresources-list](#test-103)
- [Test 104: azmcp-extension-azqr](#test-104)
- [Test 105: azmcp-extension-azqr](#test-105)
- [Test 106: azmcp-extension-azqr](#test-106)
- [Test 107: azmcp-role-assignment-list](#test-107)
- [Test 108: azmcp-role-assignment-list](#test-108)
- [Test 109: azmcp-redis-cache-list](#test-109)
- [Test 110: azmcp-redis-cache-list](#test-110)
- [Test 111: azmcp-redis-cache-list](#test-111)
- [Test 112: azmcp-redis-cache-list-accesspolicy](#test-112)
- [Test 113: azmcp-redis-cache-list-accesspolicy](#test-113)
- [Test 114: azmcp-redis-cluster-database-list](#test-114)
- [Test 115: azmcp-redis-cluster-database-list](#test-115)
- [Test 116: azmcp-redis-cluster-list](#test-116)
- [Test 117: azmcp-redis-cluster-list](#test-117)
- [Test 118: azmcp-redis-cluster-list](#test-118)
- [Test 119: azmcp-group-list](#test-119)
- [Test 120: azmcp-group-list](#test-120)
- [Test 121: azmcp-group-list](#test-121)
- [Test 122: azmcp-servicebus-queue-details](#test-122)
- [Test 123: azmcp-servicebus-queue-peek](#test-123)
- [Test 124: azmcp-servicebus-topic-details](#test-124)
- [Test 125: azmcp-servicebus-topic-subscription-details](#test-125)
- [Test 126: azmcp-servicebus-topic-subscription-peek](#test-126)
- [Test 127: azmcp-sql-db-show](#test-127)
- [Test 128: azmcp-sql-db-show](#test-128)
- [Test 129: azmcp-sql-elastic-pool-list](#test-129)
- [Test 130: azmcp-sql-elastic-pool-list](#test-130)
- [Test 131: azmcp-sql-elastic-pool-list](#test-131)
- [Test 132: azmcp-sql-firewall-rule-list](#test-132)
- [Test 133: azmcp-sql-firewall-rule-list](#test-133)
- [Test 134: azmcp-sql-firewall-rule-list](#test-134)
- [Test 135: azmcp-sql-server-entra-admin-list](#test-135)
- [Test 136: azmcp-sql-server-entra-admin-list](#test-136)
- [Test 137: azmcp-sql-server-entra-admin-list](#test-137)
- [Test 138: azmcp-storage-account-list](#test-138)
- [Test 139: azmcp-storage-account-list](#test-139)
- [Test 140: azmcp-storage-account-list](#test-140)
- [Test 141: azmcp-storage-blob-container-details](#test-141)
- [Test 142: azmcp-storage-blob-container-list](#test-142)
- [Test 143: azmcp-storage-blob-container-list](#test-143)
- [Test 144: azmcp-storage-blob-list](#test-144)
- [Test 145: azmcp-storage-blob-list](#test-145)
- [Test 146: azmcp-storage-datalake-file-system-list-paths](#test-146)
- [Test 147: azmcp-storage-datalake-file-system-list-paths](#test-147)
- [Test 148: azmcp-storage-table-list](#test-148)
- [Test 149: azmcp-storage-table-list](#test-149)
- [Test 150: azmcp-subscription-list](#test-150)
- [Test 151: azmcp-subscription-list](#test-151)
- [Test 152: azmcp-subscription-list](#test-152)
- [Test 153: azmcp-subscription-list](#test-153)
- [Test 154: azmcp-azureterraformbestpractices-get](#test-154)
- [Test 155: azmcp-azureterraformbestpractices-get](#test-155)
- [Test 156: azmcp-bicepschema-get](#test-156)

---

## Test 1

**Expected Tool:** `azmcp-foundry-models-list`  
**Prompt:** List all AI Foundry models  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.560022 | `azmcp-foundry-models-list` | ✅ **EXPECTED** |
| 2 | 0.401146 | `azmcp-foundry-models-deploy` | ❌ |
| 3 | 0.355031 | `azmcp-search-service-list` | ❌ |
| 4 | 0.346909 | `azmcp-foundry-models-deployments-list` | ❌ |
| 5 | 0.326271 | `azmcp-search-index-list` | ❌ |
| 6 | 0.298648 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.285437 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.277883 | `azmcp-grafana-list` | ❌ |
| 9 | 0.273026 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.252297 | `azmcp-postgres-database-list` | ❌ |

---

## Test 2

**Expected Tool:** `azmcp-foundry-models-list`  
**Prompt:** Show me the available AI Foundry models  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.574818 | `azmcp-foundry-models-list` | ✅ **EXPECTED** |
| 2 | 0.430513 | `azmcp-foundry-models-deploy` | ❌ |
| 3 | 0.356898 | `azmcp-foundry-models-deployments-list` | ❌ |
| 4 | 0.309590 | `azmcp-search-service-list` | ❌ |
| 5 | 0.276470 | `azmcp-search-index-list` | ❌ |
| 6 | 0.244697 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.237143 | `azmcp-bestpractices-general-get` | ❌ |
| 8 | 0.233079 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 9 | 0.231148 | `azmcp-grafana-list` | ❌ |
| 10 | 0.221283 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |

---

## Test 3

**Expected Tool:** `azmcp-foundry-models-deploy`  
**Prompt:** Deploy a GPT4o instance on my resource \<resource-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.308128 | `azmcp-foundry-models-deploy` | ✅ **EXPECTED** |
| 2 | 0.275714 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.219783 | `azmcp-grafana-list` | ❌ |
| 4 | 0.217469 | `azmcp-loadtesting-test-create` | ❌ |
| 5 | 0.217420 | `azmcp-loadtesting-testrun-create` | ❌ |
| 6 | 0.216052 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.214845 | `azmcp-postgres-server-setparam` | ❌ |
| 8 | 0.211918 | `azmcp-loadtesting-test-get` | ❌ |
| 9 | 0.209806 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.207390 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |

---

## Test 4

**Expected Tool:** `azmcp-foundry-models-deployments-list`  
**Prompt:** List all AI Foundry model deployments  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.559508 | `azmcp-foundry-models-deployments-list` | ✅ **EXPECTED** |
| 2 | 0.549636 | `azmcp-foundry-models-list` | ❌ |
| 3 | 0.533239 | `azmcp-foundry-models-deploy` | ❌ |
| 4 | 0.404693 | `azmcp-search-service-list` | ❌ |
| 5 | 0.369235 | `azmcp-search-index-list` | ❌ |
| 6 | 0.334867 | `azmcp-grafana-list` | ❌ |
| 7 | 0.318854 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.314552 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 9 | 0.312247 | `azmcp-loadtesting-testrun-list` | ❌ |
| 10 | 0.302262 | `azmcp-monitor-table-type-list` | ❌ |

---

## Test 5

**Expected Tool:** `azmcp-foundry-models-deployments-list`  
**Prompt:** Show me all AI Foundry model deployments  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.518221 | `azmcp-foundry-models-list` | ❌ |
| 2 | 0.503424 | `azmcp-foundry-models-deploy` | ❌ |
| 3 | 0.488885 | `azmcp-foundry-models-deployments-list` | ✅ **EXPECTED** |
| 4 | 0.360908 | `azmcp-search-service-list` | ❌ |
| 5 | 0.318904 | `azmcp-search-index-list` | ❌ |
| 6 | 0.286814 | `azmcp-grafana-list` | ❌ |
| 7 | 0.280357 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 8 | 0.265906 | `azmcp-extension-azd` | ❌ |
| 9 | 0.261716 | `azmcp-bestpractices-general-get` | ❌ |
| 10 | 0.259989 | `azmcp-loadtesting-testrun-list` | ❌ |

---

## Test 6

**Expected Tool:** `azmcp-search-index-describe`  
**Prompt:** Show me the details of the index \<index-name> in Cognitive Search service \<service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.629324 | `azmcp-search-index-list` | ❌ |
| 2 | 0.593595 | `azmcp-search-index-describe` | ✅ **EXPECTED** |
| 3 | 0.477262 | `azmcp-search-index-query` | ❌ |
| 4 | 0.428217 | `azmcp-search-service-list` | ❌ |
| 5 | 0.372843 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.369164 | `azmcp-kusto-cluster-get` | ❌ |
| 7 | 0.340990 | `azmcp-kusto-table-schema` | ❌ |
| 8 | 0.333862 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.332732 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.325259 | `azmcp-storage-blob-container-details` | ❌ |

---

## Test 7

**Expected Tool:** `azmcp-search-index-list`  
**Prompt:** List all indexes in the Cognitive Search service \<service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.782674 | `azmcp-search-index-list` | ✅ **EXPECTED** |
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
| 1 | 0.734068 | `azmcp-search-index-list` | ✅ **EXPECTED** |
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
| 1 | 0.555113 | `azmcp-search-index-list` | ❌ |
| 2 | 0.525962 | `azmcp-search-index-query` | ✅ **EXPECTED** |
| 3 | 0.497425 | `azmcp-search-index-describe` | ❌ |
| 4 | 0.458539 | `azmcp-search-service-list` | ❌ |
| 5 | 0.343131 | `azmcp-kusto-query` | ❌ |
| 6 | 0.321890 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 7 | 0.319206 | `azmcp-monitor-workspace-log-query` | ❌ |
| 8 | 0.300864 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.285734 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.281280 | `azmcp-monitor-metrics-definitions` | ❌ |

---

## Test 10

**Expected Tool:** `azmcp-search-list`  
**Prompt:** List all Cognitive Search services in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.745450 | `azmcp-search-service-list` | ❌ |
| 2 | 0.572614 | `azmcp-search-index-list` | ❌ |
| 3 | 0.500455 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.494272 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.493011 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.492228 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.486066 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.482464 | `azmcp-grafana-list` | ❌ |
| 9 | 0.477406 | `azmcp-subscription-list` | ❌ |
| 10 | 0.470384 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 11

**Expected Tool:** `azmcp-search-list`  
**Prompt:** Show me the Cognitive Search services in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.644213 | `azmcp-search-service-list` | ❌ |
| 2 | 0.495110 | `azmcp-search-index-list` | ❌ |
| 3 | 0.425939 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.412158 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.408456 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.400229 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.399822 | `azmcp-grafana-list` | ❌ |
| 8 | 0.397883 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.393679 | `azmcp-subscription-list` | ❌ |
| 10 | 0.390559 | `azmcp-foundry-models-list` | ❌ |

---

## Test 12

**Expected Tool:** `azmcp-search-list`  
**Prompt:** Show me my Cognitive Search services  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.482308 | `azmcp-search-service-list` | ❌ |
| 2 | 0.459155 | `azmcp-search-index-list` | ❌ |
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
| 4 | 0.385267 | `azmcp-appconfig-kv-set` | ❌ |
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
| 4 | 0.364317 | `azmcp-appconfig-kv-set` | ❌ |
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
| 3 | 0.475144 | `azmcp-appconfig-kv-set` | ❌ |
| 4 | 0.444881 | `azmcp-appconfig-kv-unlock` | ❌ |
| 5 | 0.443998 | `azmcp-appconfig-kv-lock` | ❌ |
| 6 | 0.413401 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.392016 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.239861 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.237936 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.230918 | `azmcp-keyvault-key-get` | ❌ |

---

## Test 17

**Expected Tool:** `azmcp-appconfig-kv-list`  
**Prompt:** List all key-value settings in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.730852 | `azmcp-appconfig-kv-list` | ✅ **EXPECTED** |
| 2 | 0.610828 | `azmcp-appconfig-kv-show` | ❌ |
| 3 | 0.564147 | `azmcp-appconfig-kv-set` | ❌ |
| 4 | 0.557810 | `azmcp-appconfig-account-list` | ❌ |
| 5 | 0.482784 | `azmcp-appconfig-kv-unlock` | ❌ |
| 6 | 0.464635 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.438315 | `azmcp-appconfig-kv-lock` | ❌ |
| 8 | 0.377534 | `azmcp-postgres-server-config` | ❌ |
| 9 | 0.358408 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.329461 | `azmcp-loadtesting-testrun-list` | ❌ |

---

## Test 18

**Expected Tool:** `azmcp-appconfig-kv-list`  
**Prompt:** Show me the key-value settings in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.682275 | `azmcp-appconfig-kv-list` | ✅ **EXPECTED** |
| 2 | 0.623796 | `azmcp-appconfig-kv-show` | ❌ |
| 3 | 0.553241 | `azmcp-appconfig-kv-set` | ❌ |
| 4 | 0.522426 | `azmcp-appconfig-account-list` | ❌ |
| 5 | 0.490384 | `azmcp-appconfig-kv-unlock` | ❌ |
| 6 | 0.468503 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.458896 | `azmcp-appconfig-kv-lock` | ❌ |
| 8 | 0.370520 | `azmcp-postgres-server-config` | ❌ |
| 9 | 0.316879 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.290220 | `azmcp-keyvault-key-list` | ❌ |

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
| 4 | 0.496084 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.440566 | `azmcp-appconfig-kv-show` | ❌ |
| 6 | 0.431516 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.373656 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.238242 | `azmcp-postgres-server-setparam` | ❌ |
| 9 | 0.226824 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.221519 | `azmcp-keyvault-key-get` | ❌ |

---

## Test 20

**Expected Tool:** `azmcp-appconfig-kv-set`  
**Prompt:** Set the key <key_name> in App Configuration store <app_config_store_name> to \<value>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.652311 | `azmcp-appconfig-kv-set` | ✅ **EXPECTED** |
| 2 | 0.530109 | `azmcp-appconfig-kv-lock` | ❌ |
| 3 | 0.515498 | `azmcp-appconfig-kv-list` | ❌ |
| 4 | 0.512161 | `azmcp-appconfig-kv-show` | ❌ |
| 5 | 0.509476 | `azmcp-appconfig-kv-unlock` | ❌ |
| 6 | 0.508094 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.375031 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.345915 | `azmcp-postgres-server-setparam` | ❌ |
| 9 | 0.271083 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.232410 | `azmcp-keyvault-key-get` | ❌ |

---

## Test 21

**Expected Tool:** `azmcp-appconfig-kv-show`  
**Prompt:** Show the content for the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.602729 | `azmcp-appconfig-kv-list` | ❌ |
| 2 | 0.564168 | `azmcp-appconfig-kv-show` | ✅ **EXPECTED** |
| 3 | 0.451247 | `azmcp-appconfig-kv-set` | ❌ |
| 4 | 0.441580 | `azmcp-appconfig-kv-delete` | ❌ |
| 5 | 0.436893 | `azmcp-appconfig-account-list` | ❌ |
| 6 | 0.433828 | `azmcp-appconfig-kv-lock` | ❌ |
| 7 | 0.427548 | `azmcp-appconfig-kv-unlock` | ❌ |
| 8 | 0.302884 | `azmcp-keyvault-key-get` | ❌ |
| 9 | 0.293028 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.291120 | `azmcp-postgres-server-config` | ❌ |

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
| 4 | 0.478038 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.476496 | `azmcp-appconfig-kv-delete` | ❌ |
| 6 | 0.451670 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.409406 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.264131 | `azmcp-keyvault-key-get` | ❌ |
| 9 | 0.246827 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.240025 | `azmcp-keyvault-key-create` | ❌ |

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
| 5 | 0.426045 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.360273 | `azmcp-appconfig-kv-set` | ❌ |
| 7 | 0.335372 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.329895 | `azmcp-loadtesting-testresource-create` | ❌ |
| 9 | 0.327983 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.318516 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 24

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** Show me the details of the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591903 | `azmcp-storage-blob-container-details` | ❌ |
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

## Test 25

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** List all virtual machines in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.577373 | `azmcp-search-service-list` | ❌ |
| 2 | 0.531748 | `azmcp-subscription-list` | ❌ |
| 3 | 0.530948 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.501504 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.500615 | `azmcp-redis-cache-list` | ❌ |
| 6 | 0.499251 | `azmcp-kusto-cluster-list` | ❌ |
| 7 | 0.496186 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.484074 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.482576 | `azmcp-grafana-list` | ❌ |
| 10 | 0.478070 | `azmcp-aks-cluster-list` | ❌ |

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
| 6 | 0.587625 | `azmcp-subscription-list` | ❌ |
| 7 | 0.557870 | `azmcp-search-service-list` | ❌ |
| 8 | 0.528963 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.517902 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.516914 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 27

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** Show me the cosmosdb accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.752494 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.605125 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.566249 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.558106 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.546289 | `azmcp-subscription-list` | ❌ |
| 6 | 0.535227 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.513709 | `azmcp-search-service-list` | ❌ |
| 8 | 0.488006 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.475144 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.466414 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 28

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
| 8 | 0.436267 | `azmcp-subscription-list` | ❌ |
| 9 | 0.406730 | `azmcp-storage-blob-list` | ❌ |
| 10 | 0.392611 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

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
| 7 | 0.419841 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.398979 | `azmcp-search-service-list` | ❌ |
| 9 | 0.395755 | `azmcp-search-index-list` | ❌ |
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
| 8 | 0.502610 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.460959 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.453217 | `azmcp-kusto-database-list` | ❌ |

---

## Test 31

**Expected Tool:** `azmcp-cosmos-database-container-list`  
**Prompt:** Show me the containers in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.789395 | `azmcp-cosmos-database-container-list` | ✅ **EXPECTED** |
| 2 | 0.621396 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.614220 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.564030 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 5 | 0.562062 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.493335 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.485678 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.471018 | `azmcp-storage-table-list` | ❌ |
| 9 | 0.405675 | `azmcp-kusto-table-list` | ❌ |
| 10 | 0.403817 | `azmcp-kusto-database-list` | ❌ |

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
| 6 | 0.526389 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 7 | 0.526046 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.514861 | `azmcp-storage-blob-container-list` | ❌ |
| 9 | 0.501477 | `azmcp-postgres-database-list` | ❌ |
| 10 | 0.481728 | `azmcp-kusto-table-list` | ❌ |

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
| 7 | 0.497414 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.471814 | `azmcp-storage-blob-container-list` | ❌ |
| 9 | 0.447875 | `azmcp-postgres-database-list` | ❌ |
| 10 | 0.446971 | `azmcp-kusto-table-list` | ❌ |

---

## Test 34

**Expected Tool:** `azmcp-kusto-cluster-get`  
**Prompt:** Show me the details of the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.500112 | `azmcp-kusto-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.457669 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.416762 | `azmcp-redis-cluster-database-list` | ❌ |
| 4 | 0.364174 | `azmcp-loadtesting-testrun-get` | ❌ |
| 5 | 0.363689 | `azmcp-aks-cluster-list` | ❌ |
| 6 | 0.344871 | `azmcp-sql-db-show` | ❌ |
| 7 | 0.340890 | `azmcp-kusto-database-list` | ❌ |
| 8 | 0.333864 | `azmcp-kusto-query` | ❌ |
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
| 3 | 0.540703 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.535989 | `azmcp-aks-cluster-list` | ❌ |
| 5 | 0.509396 | `azmcp-grafana-list` | ❌ |
| 6 | 0.505912 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.492107 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.487882 | `azmcp-search-service-list` | ❌ |
| 9 | 0.487583 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.480592 | `azmcp-kusto-cluster-get` | ❌ |

---

## Test 36

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** Show me the Data Explorer clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584053 | `azmcp-redis-cluster-list` | ❌ |
| 2 | 0.549797 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 3 | 0.471271 | `azmcp-aks-cluster-list` | ❌ |
| 4 | 0.462944 | `azmcp-grafana-list` | ❌ |
| 5 | 0.460716 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.455251 | `azmcp-kusto-database-list` | ❌ |
| 7 | 0.446124 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.440326 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.432048 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.421307 | `azmcp-search-service-list` | ❌ |

---

## Test 37

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** Show me my Data Explorer clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.437363 | `azmcp-redis-cluster-list` | ❌ |
| 2 | 0.391087 | `azmcp-redis-cluster-database-list` | ❌ |
| 3 | 0.386126 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 4 | 0.351881 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.338720 | `azmcp-aks-cluster-list` | ❌ |
| 6 | 0.334967 | `azmcp-kusto-cluster-get` | ❌ |
| 7 | 0.303083 | `azmcp-grafana-list` | ❌ |
| 8 | 0.292838 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.285121 | `azmcp-kusto-query` | ❌ |
| 10 | 0.284806 | `azmcp-kusto-sample` | ❌ |

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
| 6 | 0.459180 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.434330 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.431669 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.404095 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.396060 | `azmcp-cosmos-database-container-list` | ❌ |

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
| 7 | 0.383664 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.368013 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.362905 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.359378 | `azmcp-monitor-table-list` | ❌ |

---

## Test 40

**Expected Tool:** `azmcp-kusto-query`  
**Prompt:** Show me all items that contain the word <search_term> in the Data Explorer table <table_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.384419 | `azmcp-kusto-query` | ✅ **EXPECTED** |
| 2 | 0.367816 | `azmcp-kusto-sample` | ❌ |
| 3 | 0.349312 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.345704 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.333207 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.320602 | `azmcp-kusto-table-schema` | ❌ |
| 7 | 0.319066 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.315123 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.307915 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.304308 | `azmcp-kusto-database-list` | ❌ |

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
| 1 | 0.590004 | `azmcp-kusto-table-list` | ✅ **EXPECTED** |
| 2 | 0.585237 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.550007 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.520802 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.518157 | `azmcp-kusto-database-list` | ❌ |
| 6 | 0.517077 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.475496 | `azmcp-postgres-database-list` | ❌ |
| 8 | 0.464341 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.462052 | `azmcp-kusto-table-schema` | ❌ |
| 10 | 0.436518 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 43

**Expected Tool:** `azmcp-kusto-table-list`  
**Prompt:** Show me the tables in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.548875 | `azmcp-kusto-table-list` | ✅ **EXPECTED** |
| 2 | 0.523432 | `azmcp-postgres-table-list` | ❌ |
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
| 1 | 0.591726 | `azmcp-kusto-table-schema` | ✅ **EXPECTED** |
| 2 | 0.563806 | `azmcp-postgres-table-schema` | ❌ |
| 3 | 0.440427 | `azmcp-kusto-table-list` | ❌ |
| 4 | 0.439943 | `azmcp-kusto-sample` | ❌ |
| 5 | 0.413540 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.399748 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.387211 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.366376 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.365795 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.358008 | `azmcp-storage-table-list` | ❌ |

---

## Test 45

**Expected Tool:** `azmcp-postgres-database-list`  
**Prompt:** List all PostgreSQL databases in server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.816469 | `azmcp-postgres-database-list` | ✅ **EXPECTED** |
| 2 | 0.654926 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.619269 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.533816 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.485674 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.459027 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.445558 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.429604 | `azmcp-postgres-table-schema` | ❌ |
| 9 | 0.421371 | `azmcp-postgres-database-query` | ❌ |
| 10 | 0.418768 | `azmcp-kusto-database-list` | ❌ |

---

## Test 46

**Expected Tool:** `azmcp-postgres-database-list`  
**Prompt:** Show me the PostgreSQL databases in server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.754126 | `azmcp-postgres-database-list` | ✅ **EXPECTED** |
| 2 | 0.593434 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.589659 | `azmcp-postgres-table-list` | ❌ |
| 4 | 0.546389 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.499088 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.440842 | `azmcp-postgres-table-schema` | ❌ |
| 7 | 0.438619 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.432230 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.414815 | `azmcp-postgres-server-setparam` | ❌ |
| 10 | 0.391820 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 47

**Expected Tool:** `azmcp-postgres-database-query`  
**Prompt:** Show me all items that contain the word \<search_term> in the PostgreSQL database \<database> in server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.532637 | `azmcp-postgres-database-list` | ❌ |
| 2 | 0.492741 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.463628 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.402461 | `azmcp-postgres-database-query` | ✅ **EXPECTED** |
| 5 | 0.396499 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.395130 | `azmcp-postgres-server-param` | ❌ |
| 7 | 0.372172 | `azmcp-postgres-table-schema` | ❌ |
| 8 | 0.343323 | `azmcp-postgres-server-setparam` | ❌ |
| 9 | 0.266621 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.265996 | `azmcp-search-service-list` | ❌ |

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
| 6 | 0.478818 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.440180 | `azmcp-postgres-table-schema` | ❌ |
| 8 | 0.397389 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.270614 | `azmcp-appconfig-kv-list` | ❌ |
| 10 | 0.253907 | `azmcp-sql-db-show` | ❌ |

---

## Test 49

**Expected Tool:** `azmcp-postgres-server-list`  
**Prompt:** List all PostgreSQL servers in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.900022 | `azmcp-postgres-server-list` | ✅ **EXPECTED** |
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

## Test 51

**Expected Tool:** `azmcp-postgres-server-list`  
**Prompt:** Show me my PostgreSQL servers  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.674326 | `azmcp-postgres-server-list` | ✅ **EXPECTED** |
| 2 | 0.607062 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.576348 | `azmcp-postgres-server-config` | ❌ |
| 4 | 0.522996 | `azmcp-postgres-table-list` | ❌ |
| 5 | 0.506171 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.409406 | `azmcp-postgres-database-query` | ❌ |
| 7 | 0.400088 | `azmcp-postgres-server-setparam` | ❌ |
| 8 | 0.372955 | `azmcp-postgres-table-schema` | ❌ |
| 9 | 0.318087 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.305360 | `azmcp-sql-server-entra-admin-list` | ❌ |

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
| 6 | 0.372846 | `azmcp-postgres-table-list` | ❌ |
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
| 6 | 0.370342 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.356733 | `azmcp-postgres-database-query` | ❌ |
| 8 | 0.337386 | `azmcp-postgres-table-schema` | ❌ |
| 9 | 0.194806 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.185013 | `azmcp-sql-server-firewall-rule-list` | ❌ |

---

## Test 54

**Expected Tool:** `azmcp-postgres-table-list`  
**Prompt:** List all tables in the PostgreSQL database \<database> in server \<server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.791736 | `azmcp-postgres-table-list` | ✅ **EXPECTED** |
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
| 1 | 0.735442 | `azmcp-postgres-table-list` | ✅ **EXPECTED** |
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
| 2 | 0.590459 | `azmcp-postgres-table-list` | ❌ |
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
| 2 | 0.227013 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 3 | 0.198599 | `azmcp-bestpractices-general-get` | ❌ |
| 4 | 0.187987 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 5 | 0.186055 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 6 | 0.185433 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 7 | 0.181543 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.177946 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.173269 | `azmcp-extension-azd` | ✅ **EXPECTED** |
| 10 | 0.165761 | `azmcp-postgres-table-list` | ❌ |

---

## Test 58

**Expected Tool:** `azmcp-extension-azd`  
**Prompt:** Deploy my web application to Azure App Service  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.437357 | `azmcp-foundry-models-deploy` | ❌ |
| 2 | 0.417642 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 3 | 0.364145 | `azmcp-extension-azd` | ✅ **EXPECTED** |
| 4 | 0.361106 | `azmcp-foundry-models-deployments-list` | ❌ |
| 5 | 0.356425 | `azmcp-extension-az` | ❌ |
| 6 | 0.339542 | `azmcp-bestpractices-general-get` | ❌ |
| 7 | 0.323086 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 8 | 0.320093 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 9 | 0.309187 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.289883 | `azmcp-search-index-list` | ❌ |

---

## Test 59

**Expected Tool:** `azmcp-keyvault-key-create`  
**Prompt:** Create a new key called <key_name> with the RSA type in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.648759 | `azmcp-keyvault-key-create` | ✅ **EXPECTED** |
| 2 | 0.412544 | `azmcp-keyvault-key-get` | ❌ |
| 3 | 0.410878 | `azmcp-keyvault-key-list` | ❌ |
| 4 | 0.380603 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.341740 | `azmcp-keyvault-secret-get` | ❌ |
| 6 | 0.340729 | `azmcp-appconfig-kv-lock` | ❌ |
| 7 | 0.307182 | `azmcp-appconfig-kv-unlock` | ❌ |
| 8 | 0.296381 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.276727 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.269809 | `azmcp-loadtesting-testresource-create` | ❌ |

---

## Test 60

**Expected Tool:** `azmcp-keyvault-key-get`  
**Prompt:** Show me the details of key <key_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.557249 | `azmcp-keyvault-key-get` | ✅ **EXPECTED** |
| 2 | 0.484165 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.470273 | `azmcp-appconfig-kv-show` | ❌ |
| 4 | 0.440633 | `azmcp-keyvault-secret-get` | ❌ |
| 5 | 0.413044 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.401255 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.369598 | `azmcp-servicebus-queue-details` | ❌ |
| 8 | 0.364420 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.362573 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.362505 | `azmcp-appconfig-kv-unlock` | ❌ |

---

## Test 61

**Expected Tool:** `azmcp-keyvault-key-list`  
**Prompt:** List all keys in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.677012 | `azmcp-keyvault-key-list` | ✅ **EXPECTED** |
| 2 | 0.498767 | `azmcp-cosmos-account-list` | ❌ |
| 3 | 0.482027 | `azmcp-keyvault-key-get` | ❌ |
| 4 | 0.477363 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.473916 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.469953 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.468044 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.460777 | `azmcp-storage-blob-list` | ❌ |
| 9 | 0.443785 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.441458 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

---

## Test 62

**Expected Tool:** `azmcp-keyvault-key-list`  
**Prompt:** Show me the keys in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.561723 | `azmcp-keyvault-key-list` | ✅ **EXPECTED** |
| 2 | 0.501727 | `azmcp-keyvault-key-get` | ❌ |
| 3 | 0.448815 | `azmcp-keyvault-secret-get` | ❌ |
| 4 | 0.421475 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.416773 | `azmcp-appconfig-kv-show` | ❌ |
| 6 | 0.398848 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.397021 | `azmcp-storage-blob-container-list` | ❌ |
| 8 | 0.375139 | `azmcp-storage-table-list` | ❌ |
| 9 | 0.373589 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.372229 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 63

**Expected Tool:** `azmcp-keyvault-secret-get`  
**Prompt:** Show me the details about the secret <secret_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.556862 | `azmcp-keyvault-secret-get` | ✅ **EXPECTED** |
| 2 | 0.502965 | `azmcp-keyvault-key-get` | ❌ |
| 3 | 0.453983 | `azmcp-appconfig-kv-show` | ❌ |
| 4 | 0.450924 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.412807 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.372112 | `azmcp-servicebus-queue-details` | ❌ |
| 7 | 0.366282 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.361982 | `azmcp-storage-blob-container-list` | ❌ |
| 9 | 0.359760 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.354767 | `azmcp-keyvault-key-create` | ❌ |

---

## Test 64

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** List all AKS clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.800736 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.690255 | `azmcp-kusto-cluster-list` | ❌ |
| 3 | 0.599940 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.549327 | `azmcp-search-service-list` | ❌ |
| 5 | 0.543684 | `azmcp-monitor-workspace-list` | ❌ |
| 6 | 0.515922 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.503430 | `azmcp-kusto-database-list` | ❌ |
| 8 | 0.502344 | `azmcp-subscription-list` | ❌ |
| 9 | 0.498121 | `azmcp-group-list` | ❌ |
| 10 | 0.497456 | `azmcp-keyvault-key-list` | ❌ |

---

## Test 65

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** Show me my Azure Kubernetes Service clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.608231 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.492910 | `azmcp-kusto-cluster-list` | ❌ |
| 3 | 0.446270 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.409808 | `azmcp-kusto-cluster-get` | ❌ |
| 5 | 0.403403 | `azmcp-kusto-database-list` | ❌ |
| 6 | 0.388143 | `azmcp-search-service-list` | ❌ |
| 7 | 0.383463 | `azmcp-search-index-list` | ❌ |
| 8 | 0.371535 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.363769 | `azmcp-subscription-list` | ❌ |
| 10 | 0.360053 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 66

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** What AKS clusters do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.624092 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.449602 | `azmcp-kusto-cluster-list` | ❌ |
| 3 | 0.416564 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.378826 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.347244 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.342303 | `azmcp-extension-az` | ❌ |
| 7 | 0.337217 | `azmcp-kusto-database-list` | ❌ |
| 8 | 0.328074 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.325876 | `azmcp-extension-azd` | ❌ |
| 10 | 0.322075 | `azmcp-sql-elastic-pool-list` | ❌ |

---

## Test 67

**Expected Tool:** `azmcp-loadtesting-test-create`  
**Prompt:** Create a basic URL test using the following endpoint URL \<test-url> that runs for 30 minutes with 45 virtual users. The test name is \<sample-name> with the test id \<test-id> and the load testing resource is \<load-test-resource> in the resource group \<resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.586388 | `azmcp-loadtesting-test-create` | ✅ **EXPECTED** |
| 2 | 0.536845 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.493962 | `azmcp-loadtesting-testrun-create` | ❌ |
| 4 | 0.417682 | `azmcp-loadtesting-testresource-list` | ❌ |
| 5 | 0.396484 | `azmcp-loadtesting-test-get` | ❌ |
| 6 | 0.391957 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.345596 | `azmcp-monitor-resource-log-query` | ❌ |
| 8 | 0.337312 | `azmcp-loadtesting-testrun-update` | ❌ |
| 9 | 0.333853 | `azmcp-loadtesting-testrun-list` | ❌ |
| 10 | 0.326264 | `azmcp-monitor-workspace-log-query` | ❌ |

---

## Test 68

**Expected Tool:** `azmcp-loadtesting-test-get`  
**Prompt:** Get the load test with id \<test-id> in the load test resource \<test-resource> in resource group \<resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.655553 | `azmcp-loadtesting-test-get` | ✅ **EXPECTED** |
| 2 | 0.622547 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.583945 | `azmcp-loadtesting-testresource-create` | ❌ |
| 4 | 0.549618 | `azmcp-loadtesting-testrun-get` | ❌ |
| 5 | 0.485335 | `azmcp-loadtesting-testrun-list` | ❌ |
| 6 | 0.471605 | `azmcp-loadtesting-testrun-create` | ❌ |
| 7 | 0.449553 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.414465 | `azmcp-group-list` | ❌ |
| 9 | 0.414372 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.384696 | `azmcp-loadtesting-testrun-update` | ❌ |

---

## Test 69

**Expected Tool:** `azmcp-loadtesting-testresource-create`  
**Prompt:** Create a load test resource \<load-test-resource-name> in the resource group \<resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713657 | `azmcp-loadtesting-testresource-create` | ✅ **EXPECTED** |
| 2 | 0.588519 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.523077 | `azmcp-loadtesting-test-create` | ❌ |
| 4 | 0.473618 | `azmcp-loadtesting-testrun-create` | ❌ |
| 5 | 0.446452 | `azmcp-loadtesting-test-get` | ❌ |
| 6 | 0.415901 | `azmcp-group-list` | ❌ |
| 7 | 0.393483 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.377932 | `azmcp-loadtesting-testrun-get` | ❌ |
| 9 | 0.359373 | `azmcp-loadtesting-testrun-update` | ❌ |
| 10 | 0.349129 | `azmcp-extension-azqr` | ❌ |

---

## Test 70

**Expected Tool:** `azmcp-loadtesting-testresource-list`  
**Prompt:** List all load testing resources in the resource group \<resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.733863 | `azmcp-loadtesting-testresource-list` | ✅ **EXPECTED** |
| 2 | 0.585965 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.571444 | `azmcp-group-list` | ❌ |
| 4 | 0.555049 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.516843 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.513345 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.510045 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.490092 | `azmcp-grafana-list` | ❌ |
| 9 | 0.486535 | `azmcp-loadtesting-testrun-list` | ❌ |
| 10 | 0.463772 | `azmcp-loadtesting-testrun-get` | ❌ |

---

## Test 71

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
| 7 | 0.431280 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.411962 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.314820 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.301313 | `azmcp-keyvault-key-create` | ❌ |

---

## Test 72

**Expected Tool:** `azmcp-loadtesting-testrun-get`  
**Prompt:** Get the load test run with id \<testrun-id> in the load test resource \<test-resource> in resource group \<resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.636591 | `azmcp-loadtesting-test-get` | ❌ |
| 2 | 0.610981 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.591836 | `azmcp-loadtesting-testrun-get` | ✅ **EXPECTED** |
| 4 | 0.569352 | `azmcp-loadtesting-testresource-create` | ❌ |
| 5 | 0.543493 | `azmcp-loadtesting-testrun-create` | ❌ |
| 6 | 0.520235 | `azmcp-loadtesting-testrun-list` | ❌ |
| 7 | 0.446714 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.436125 | `azmcp-loadtesting-testrun-update` | ❌ |
| 9 | 0.406005 | `azmcp-group-list` | ❌ |
| 10 | 0.403708 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 73

**Expected Tool:** `azmcp-loadtesting-testrun-list`  
**Prompt:** Get all the load test runs for the test with id \<test-id> in the load test resource \<test-resource> in resource group \<resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633477 | `azmcp-loadtesting-testresource-list` | ❌ |
| 2 | 0.614820 | `azmcp-loadtesting-test-get` | ❌ |
| 3 | 0.577554 | `azmcp-loadtesting-testrun-list` | ✅ **EXPECTED** |
| 4 | 0.573905 | `azmcp-loadtesting-testrun-get` | ❌ |
| 5 | 0.552015 | `azmcp-loadtesting-testresource-create` | ❌ |
| 6 | 0.486601 | `azmcp-loadtesting-testrun-create` | ❌ |
| 7 | 0.450765 | `azmcp-group-list` | ❌ |
| 8 | 0.426869 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.424219 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.412646 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 74

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
| 7 | 0.397342 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.390496 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.273690 | `azmcp-monitor-metrics-definitions` | ❌ |
| 10 | 0.266009 | `azmcp-appconfig-kv-set` | ❌ |

---

## Test 75

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
| 7 | 0.492159 | `azmcp-subscription-list` | ❌ |
| 8 | 0.491813 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.489846 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.488589 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 76

**Expected Tool:** `azmcp-marketplace-product-get`  
**Prompt:** Get details about marketplace product <product_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.506463 | `azmcp-marketplace-product-get` | ✅ **EXPECTED** |
| 2 | 0.365508 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.354541 | `azmcp-servicebus-topic-details` | ❌ |
| 4 | 0.345344 | `azmcp-servicebus-queue-details` | ❌ |
| 5 | 0.324643 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.322443 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.310098 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.300057 | `azmcp-search-index-describe` | ❌ |
| 9 | 0.274403 | `azmcp-redis-cache-list` | ❌ |
| 10 | 0.269243 | `azmcp-sql-db-show` | ❌ |

---

## Test 77

**Expected Tool:** `azmcp-bestpractices-azurefunctions-get-code-generation`  
**Prompt:** Fetch the latest Azure Functions code generation best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.742212 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ✅ **EXPECTED** |
| 2 | 0.691732 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 3 | 0.577785 | `azmcp-bestpractices-general-get` | ❌ |
| 4 | 0.572787 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 5 | 0.403032 | `azmcp-extension-az` | ❌ |
| 6 | 0.355589 | `azmcp-bicepschema-get` | ❌ |
| 7 | 0.334592 | `azmcp-extension-azd` | ❌ |
| 8 | 0.317025 | `azmcp-foundry-models-list` | ❌ |
| 9 | 0.309045 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.292709 | `azmcp-extension-azqr` | ❌ |

---

## Test 78

**Expected Tool:** `azmcp-bestpractices-azurefunctions-get-deployment`  
**Prompt:** Fetch the latest Azure Functions deployment best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.680320 | `azmcp-bestpractices-azurefunctions-get-deployment` | ✅ **EXPECTED** |
| 2 | 0.634653 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 3 | 0.516088 | `azmcp-bestpractices-general-get` | ❌ |
| 4 | 0.510942 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 5 | 0.447455 | `azmcp-extension-az` | ❌ |
| 6 | 0.447227 | `azmcp-foundry-models-deployments-list` | ❌ |
| 7 | 0.350882 | `azmcp-extension-azd` | ❌ |
| 8 | 0.340473 | `azmcp-foundry-models-deploy` | ❌ |
| 9 | 0.336125 | `azmcp-bicepschema-get` | ❌ |
| 10 | 0.317270 | `azmcp-foundry-models-list` | ❌ |

---

## Test 79

**Expected Tool:** `azmcp-bestpractices-general-get`  
**Prompt:** Fetch the latest Azure best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.614150 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 2 | 0.594866 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 3 | 0.588861 | `azmcp-bestpractices-general-get` | ✅ **EXPECTED** |
| 4 | 0.550059 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 5 | 0.457161 | `azmcp-extension-az` | ❌ |
| 6 | 0.361659 | `azmcp-bicepschema-get` | ❌ |
| 7 | 0.355660 | `azmcp-extension-azd` | ❌ |
| 8 | 0.323934 | `azmcp-extension-azqr` | ❌ |
| 9 | 0.320757 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.319319 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 80

**Expected Tool:** `azmcp-bestpractices-general-get`  
**Prompt:** Fetch the latest Azure best practices and generate code sample to get a secret from Azure Key Vault  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.582025 | `azmcp-bestpractices-general-get` | ✅ **EXPECTED** |
| 2 | 0.578038 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 3 | 0.543679 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 4 | 0.539501 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 5 | 0.507338 | `azmcp-keyvault-secret-get` | ❌ |
| 6 | 0.458599 | `azmcp-keyvault-key-get` | ❌ |
| 7 | 0.396898 | `azmcp-extension-az` | ❌ |
| 8 | 0.378531 | `azmcp-keyvault-key-list` | ❌ |
| 9 | 0.362147 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.351192 | `azmcp-bicepschema-get` | ❌ |

---

## Test 81

**Expected Tool:** `azmcp-tool-list`  
**Prompt:** List all available tools in the Azure MCP server  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.455117 | `azmcp-extension-az` | ❌ |
| 2 | 0.417931 | `azmcp-bestpractices-general-get` | ❌ |
| 3 | 0.402330 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 4 | 0.398584 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 5 | 0.395140 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.394487 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 7 | 0.392853 | `azmcp-extension-azd` | ❌ |
| 8 | 0.389970 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.389750 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 10 | 0.389676 | `azmcp-subscription-list` | ❌ |

---

## Test 82

**Expected Tool:** `azmcp-tool-list`  
**Prompt:** Show me the available tools in the Azure MCP server  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.477557 | `azmcp-extension-az` | ❌ |
| 2 | 0.391250 | `azmcp-bestpractices-general-get` | ❌ |
| 3 | 0.373806 | `azmcp-extension-azd` | ❌ |
| 4 | 0.360883 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 5 | 0.358140 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 6 | 0.357808 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 7 | 0.345389 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.328647 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 9 | 0.325168 | `azmcp-redis-cluster-list` | ❌ |
| 10 | 0.324715 | `azmcp-redis-cache-list` | ❌ |

---

## Test 83

**Expected Tool:** `azmcp-monitor-healthmodels-entity-gethealth`  
**Prompt:** Show me the health status of entity <entity_id> in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.479793 | `azmcp-monitor-healthmodels-entity-gethealth` | ✅ **EXPECTED** |
| 2 | 0.472088 | `azmcp-monitor-workspace-list` | ❌ |
| 3 | 0.468228 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.464025 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.413404 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.410030 | `azmcp-monitor-resource-log-query` | ❌ |
| 7 | 0.380242 | `azmcp-grafana-list` | ❌ |
| 8 | 0.337636 | `azmcp-loadtesting-testrun-get` | ❌ |
| 9 | 0.298841 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.297099 | `azmcp-search-index-list` | ❌ |

---

## Test 84

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** What metric definitions are available for the Application Insights resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.526496 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 2 | 0.408026 | `azmcp-monitor-metrics-query` | ❌ |
| 3 | 0.370848 | `azmcp-monitor-table-type-list` | ❌ |
| 4 | 0.343171 | `azmcp-monitor-resource-log-query` | ❌ |
| 5 | 0.329707 | `azmcp-loadtesting-testresource-list` | ❌ |
| 6 | 0.325097 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.314407 | `azmcp-search-index-list` | ❌ |
| 8 | 0.308315 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.302823 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.299167 | `azmcp-loadtesting-testrun-get` | ❌ |

---

## Test 85

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** Show me all available metrics and their definitions for storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.559626 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.542805 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.542692 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 4 | 0.541810 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.535987 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.527337 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.459829 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.447474 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 9 | 0.432758 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.414488 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 86

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
| 10 | 0.302184 | `azmcp-storage-blob-container-details` | ❌ |

---

## Test 87

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Query the <metric_name> metric for <resource_type> <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.477344 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.429670 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.385767 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.362063 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.298357 | `azmcp-search-index-query` | ❌ |
| 6 | 0.293060 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.288148 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 8 | 0.280325 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 9 | 0.272349 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.269058 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 88

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** What's the request per second rate for my Application Insights resource <resource_name> over the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.351379 | `azmcp-monitor-resource-log-query` | ❌ |
| 2 | 0.346123 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 3 | 0.341334 | `azmcp-monitor-workspace-log-query` | ❌ |
| 4 | 0.331324 | `azmcp-loadtesting-testresource-list` | ❌ |
| 5 | 0.327098 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.319343 | `azmcp-loadtesting-testresource-create` | ❌ |
| 7 | 0.314311 | `azmcp-monitor-metrics-definitions` | ❌ |
| 8 | 0.277129 | `azmcp-loadtesting-test-get` | ❌ |
| 9 | 0.269927 | `azmcp-search-index-list` | ❌ |
| 10 | 0.265893 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 89

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Investigate error rates and failed requests for Application Insights resource <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.420524 | `azmcp-monitor-resource-log-query` | ❌ |
| 2 | 0.384781 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 3 | 0.368342 | `azmcp-loadtesting-testrun-get` | ❌ |
| 4 | 0.354940 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.325741 | `azmcp-monitor-metrics-definitions` | ❌ |
| 6 | 0.316415 | `azmcp-loadtesting-testresource-list` | ❌ |
| 7 | 0.299707 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.293311 | `azmcp-loadtesting-testresource-create` | ❌ |
| 9 | 0.292956 | `azmcp-search-index-list` | ❌ |
| 10 | 0.283523 | `azmcp-extension-azqr` | ❌ |

---

## Test 90

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Analyze the performance trends and response times for Application Insights resource <resource_name> over the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.453970 | `azmcp-monitor-resource-log-query` | ❌ |
| 2 | 0.439684 | `azmcp-loadtesting-testrun-get` | ❌ |
| 3 | 0.434042 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 4 | 0.404582 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.362649 | `azmcp-monitor-metrics-definitions` | ❌ |
| 6 | 0.340642 | `azmcp-loadtesting-testrun-list` | ❌ |
| 7 | 0.339887 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.332075 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.329460 | `azmcp-loadtesting-testresource-create` | ❌ |
| 10 | 0.328475 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 91

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Check the availability metrics for my Application Insights resource <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.451288 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.411471 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.396598 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.356326 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.342298 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 6 | 0.341525 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.338294 | `azmcp-search-index-list` | ❌ |
| 8 | 0.326989 | `azmcp-loadtesting-testresource-list` | ❌ |
| 9 | 0.302312 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.292483 | `azmcp-search-service-list` | ❌ |

---

## Test 92

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Get the <aggregation_type> <metric_name> metric for <resource_type> <resource_name> over the last <time_period> with intervals  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.415400 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.397591 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.306473 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.279638 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.275443 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.269404 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.269323 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 8 | 0.259193 | `azmcp-grafana-list` | ❌ |
| 9 | 0.249889 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.248741 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 93

**Expected Tool:** `azmcp-monitor-resource-log-query`  
**Prompt:** Show me the logs for the past hour for the resource <resource_name> in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584906 | `azmcp-monitor-workspace-log-query` | ❌ |
| 2 | 0.582530 | `azmcp-monitor-resource-log-query` | ✅ **EXPECTED** |
| 3 | 0.443468 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.442971 | `azmcp-monitor-table-list` | ❌ |
| 5 | 0.416553 | `azmcp-monitor-metrics-query` | ❌ |
| 6 | 0.392377 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.390022 | `azmcp-grafana-list` | ❌ |
| 8 | 0.358681 | `azmcp-monitor-metrics-definitions` | ❌ |
| 9 | 0.355135 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 10 | 0.341795 | `azmcp-search-index-list` | ❌ |

---

## Test 94

**Expected Tool:** `azmcp-monitor-table-list`  
**Prompt:** List all tables in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.851075 | `azmcp-monitor-table-list` | ✅ **EXPECTED** |
| 2 | 0.725738 | `azmcp-monitor-table-type-list` | ❌ |
| 3 | 0.620445 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.586691 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.512135 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.502075 | `azmcp-grafana-list` | ❌ |
| 7 | 0.488557 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.436216 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.435085 | `azmcp-search-index-list` | ❌ |
| 10 | 0.420394 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 95

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
| 8 | 0.427408 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.424176 | `azmcp-kusto-table-schema` | ❌ |
| 10 | 0.414014 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 96

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
| 8 | 0.418517 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.401662 | `azmcp-kusto-sample` | ❌ |
| 10 | 0.394213 | `azmcp-monitor-workspace-log-query` | ❌ |

---

## Test 97

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
| 10 | 0.381136 | `azmcp-monitor-resource-log-query` | ❌ |

---

## Test 98

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
| 8 | 0.513651 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.508179 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.494595 | `azmcp-group-list` | ❌ |

---

## Test 99

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
| 8 | 0.428945 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.427608 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.422729 | `azmcp-subscription-list` | ❌ |

---

## Test 100

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
| 7 | 0.384248 | `azmcp-aks-cluster-list` | ❌ |
| 8 | 0.383935 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.379597 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.376438 | `azmcp-search-index-list` | ❌ |

---

## Test 101

**Expected Tool:** `azmcp-monitor-workspace-log-query`  
**Prompt:** Show me the logs for the past hour in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.581662 | `azmcp-monitor-workspace-log-query` | ✅ **EXPECTED** |
| 2 | 0.500368 | `azmcp-monitor-resource-log-query` | ❌ |
| 3 | 0.485984 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.483323 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.427241 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.365704 | `azmcp-grafana-list` | ❌ |
| 7 | 0.339654 | `azmcp-search-index-list` | ❌ |
| 8 | 0.328415 | `azmcp-monitor-metrics-query` | ❌ |
| 9 | 0.309810 | `azmcp-loadtesting-testrun-get` | ❌ |
| 10 | 0.307363 | `azmcp-kusto-query` | ❌ |

---

## Test 102

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
| 8 | 0.355436 | `azmcp-loadtesting-testresource-list` | ❌ |
| 9 | 0.345409 | `azmcp-postgres-database-list` | ❌ |
| 10 | 0.345298 | `azmcp-group-list` | ❌ |

---

## Test 103

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
| 9 | 0.306947 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.294064 | `azmcp-search-index-list` | ❌ |

---

## Test 104

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Scan my Azure subscription for compliance recommendations  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.516925 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 2 | 0.490438 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.485070 | `azmcp-bestpractices-general-get` | ❌ |
| 4 | 0.472526 | `azmcp-extension-az` | ❌ |
| 5 | 0.456290 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 6 | 0.450091 | `azmcp-search-service-list` | ❌ |
| 7 | 0.435754 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 8 | 0.423459 | `azmcp-subscription-list` | ❌ |
| 9 | 0.398621 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.389830 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 105

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Check my Azure subscription for any compliance issues or recommendations  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.476826 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 2 | 0.442625 | `azmcp-extension-az` | ❌ |
| 3 | 0.427495 | `azmcp-search-service-list` | ❌ |
| 4 | 0.426311 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 5 | 0.423208 | `azmcp-subscription-list` | ❌ |
| 6 | 0.417430 | `azmcp-bestpractices-general-get` | ❌ |
| 7 | 0.401651 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 8 | 0.388980 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.379525 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 10 | 0.365968 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 106

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Provide compliance recommendations for my current Azure subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.487939 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 2 | 0.479258 | `azmcp-bestpractices-general-get` | ❌ |
| 3 | 0.474017 | `azmcp-extension-az` | ❌ |
| 4 | 0.462743 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 5 | 0.453120 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 6 | 0.436332 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 7 | 0.382470 | `azmcp-search-service-list` | ❌ |
| 8 | 0.375772 | `azmcp-subscription-list` | ❌ |
| 9 | 0.335126 | `azmcp-marketplace-product-get` | ❌ |
| 10 | 0.333625 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 107

**Expected Tool:** `azmcp-role-assignment-list`  
**Prompt:** List all available role assignments in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.645259 | `azmcp-role-assignment-list` | ✅ **EXPECTED** |
| 2 | 0.487393 | `azmcp-search-service-list` | ❌ |
| 3 | 0.483988 | `azmcp-group-list` | ❌ |
| 4 | 0.483107 | `azmcp-subscription-list` | ❌ |
| 5 | 0.478700 | `azmcp-grafana-list` | ❌ |
| 6 | 0.474796 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.471364 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.460029 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.457712 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.452819 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 108

**Expected Tool:** `azmcp-role-assignment-list`  
**Prompt:** Show me the available role assignments in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609704 | `azmcp-role-assignment-list` | ✅ **EXPECTED** |
| 2 | 0.456956 | `azmcp-grafana-list` | ❌ |
| 3 | 0.436780 | `azmcp-subscription-list` | ❌ |
| 4 | 0.435642 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.435287 | `azmcp-search-service-list` | ❌ |
| 6 | 0.435155 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.428663 | `azmcp-group-list` | ❌ |
| 8 | 0.428370 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.420804 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.410380 | `azmcp-redis-cache-accesspolicy-list` | ❌ |

---

## Test 109

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

## Test 110

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

## Test 111

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

## Test 112

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
| 6 | 0.303736 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.303531 | `azmcp-appconfig-kv-list` | ❌ |
| 8 | 0.300024 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.297310 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.287235 | `azmcp-search-index-list` | ❌ |

---

## Test 113

**Expected Tool:** `azmcp-redis-cache-list-accesspolicy`  
**Prompt:** Show me the access policies in the Redis Cache <cache_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713839 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 2 | 0.523153 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.412377 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.338859 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.293447 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.286321 | `azmcp-appconfig-kv-list` | ❌ |
| 7 | 0.285163 | `azmcp-appconfig-kv-show` | ❌ |
| 8 | 0.277662 | `azmcp-bestpractices-general-get` | ❌ |
| 9 | 0.258045 | `azmcp-appconfig-account-list` | ❌ |
| 10 | 0.257151 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 114

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
| 9 | 0.419621 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.385544 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 115

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
| 8 | 0.397285 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.351025 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.349880 | `azmcp-postgres-table-list` | ❌ |

---

## Test 116

**Expected Tool:** `azmcp-redis-cluster-list`  
**Prompt:** List all Redis Clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.812960 | `azmcp-redis-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.679028 | `azmcp-kusto-cluster-list` | ❌ |
| 3 | 0.672104 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.588847 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.569405 | `azmcp-aks-cluster-list` | ❌ |
| 6 | 0.554298 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.522070 | `azmcp-kusto-database-list` | ❌ |
| 8 | 0.503279 | `azmcp-grafana-list` | ❌ |
| 9 | 0.467957 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.463770 | `azmcp-search-service-list` | ❌ |

---

## Test 117

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
| 6 | 0.491894 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.456252 | `azmcp-grafana-list` | ❌ |
| 8 | 0.435203 | `azmcp-kusto-cluster-get` | ❌ |
| 9 | 0.434680 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.400256 | `azmcp-redis-cache-accesspolicy-list` | ❌ |

---

## Test 118

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
| 6 | 0.368828 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.329389 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.318440 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.297699 | `azmcp-kusto-cluster-get` | ❌ |
| 10 | 0.295045 | `azmcp-grafana-list` | ❌ |

---

## Test 119

**Expected Tool:** `azmcp-group-list`  
**Prompt:** List all resource groups in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.755935 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.545480 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.542878 | `azmcp-grafana-list` | ❌ |
| 4 | 0.542393 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.530600 | `azmcp-redis-cache-list` | ❌ |
| 6 | 0.524796 | `azmcp-kusto-cluster-list` | ❌ |
| 7 | 0.524265 | `azmcp-search-service-list` | ❌ |
| 8 | 0.516885 | `azmcp-loadtesting-testresource-list` | ❌ |
| 9 | 0.500858 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.488187 | `azmcp-storage-account-list` | ❌ |

---

## Test 120

**Expected Tool:** `azmcp-group-list`  
**Prompt:** Show me the resource groups in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.665771 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.526530 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 3 | 0.523088 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.518359 | `azmcp-loadtesting-testresource-list` | ❌ |
| 5 | 0.515905 | `azmcp-grafana-list` | ❌ |
| 6 | 0.492945 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.475313 | `azmcp-search-service-list` | ❌ |
| 8 | 0.470658 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.460412 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.437750 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 121

**Expected Tool:** `azmcp-group-list`  
**Prompt:** Show me my resource groups  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.529504 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.460557 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 3 | 0.428814 | `azmcp-loadtesting-testresource-list` | ❌ |
| 4 | 0.426935 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.407817 | `azmcp-grafana-list` | ❌ |
| 6 | 0.391278 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.345595 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.343018 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.335296 | `azmcp-sql-db-show` | ❌ |
| 10 | 0.332765 | `azmcp-monitor-metrics-definitions` | ❌ |

---

## Test 122

**Expected Tool:** `azmcp-servicebus-queue-details`  
**Prompt:** Show me the details of service bus <service_bus_name> queue <queue_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651319 | `azmcp-servicebus-queue-details` | ✅ **EXPECTED** |
| 2 | 0.459209 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.434862 | `azmcp-servicebus-topic-details` | ❌ |
| 4 | 0.338738 | `azmcp-loadtesting-testrun-get` | ❌ |
| 5 | 0.337239 | `azmcp-sql-db-show` | ❌ |
| 6 | 0.335606 | `azmcp-kusto-cluster-get` | ❌ |
| 7 | 0.331575 | `azmcp-search-index-list` | ❌ |
| 8 | 0.330476 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.308567 | `azmcp-redis-cache-list` | ❌ |
| 10 | 0.303021 | `azmcp-search-index-describe` | ❌ |

---

## Test 123

**Expected Tool:** `azmcp-servicebus-queue-peek`  
**Prompt:** Show me the latest message in service bus <service_bus_name> queue <queue_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.495806 | `azmcp-servicebus-queue-details` | ❌ |
| 2 | 0.349757 | `azmcp-servicebus-topic-details` | ❌ |
| 3 | 0.331172 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 4 | 0.280616 | `azmcp-search-index-query` | ❌ |
| 5 | 0.276468 | `azmcp-search-index-list` | ❌ |
| 6 | 0.256551 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.254126 | `azmcp-search-service-list` | ❌ |
| 8 | 0.247380 | `azmcp-keyvault-secret-get` | ❌ |
| 9 | 0.244089 | `azmcp-loadtesting-testrun-get` | ❌ |
| 10 | 0.240455 | `azmcp-storage-table-list` | ❌ |

---

## Test 124

**Expected Tool:** `azmcp-servicebus-topic-details`  
**Prompt:** Show me the details of service bus <service_bus_name> topic <topic_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.612731 | `azmcp-servicebus-topic-details` | ✅ **EXPECTED** |
| 2 | 0.574218 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.500092 | `azmcp-servicebus-queue-details` | ❌ |
| 4 | 0.348999 | `azmcp-kusto-cluster-get` | ❌ |
| 5 | 0.347044 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.340036 | `azmcp-sql-db-show` | ❌ |
| 7 | 0.324869 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.324414 | `azmcp-search-index-list` | ❌ |
| 9 | 0.319203 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.315561 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 125

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
| 8 | 0.411048 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.404739 | `azmcp-redis-cluster-list` | ❌ |
| 10 | 0.403551 | `azmcp-keyvault-key-get` | ❌ |

---

## Test 126

**Expected Tool:** `azmcp-servicebus-topic-subscription-peek`  
**Prompt:** Show me the latest message in service bus <service_bus_name> subscription <subscription_name> for the topic <topic_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.506945 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 2 | 0.469808 | `azmcp-servicebus-topic-details` | ❌ |
| 3 | 0.380440 | `azmcp-servicebus-queue-details` | ❌ |
| 4 | 0.366643 | `azmcp-search-service-list` | ❌ |
| 5 | 0.328619 | `azmcp-monitor-workspace-list` | ❌ |
| 6 | 0.316407 | `azmcp-keyvault-secret-get` | ❌ |
| 7 | 0.311159 | `azmcp-subscription-list` | ❌ |
| 8 | 0.309131 | `azmcp-postgres-server-list` | ❌ |
| 9 | 0.302125 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.301192 | `azmcp-redis-cache-list` | ❌ |

---

## Test 127

**Expected Tool:** `azmcp-sql-db-show`  
**Prompt:** Show me the details of SQL database <database_name> in server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.530095 | `azmcp-sql-db-show` | ✅ **EXPECTED** |
| 2 | 0.421862 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.375668 | `azmcp-postgres-server-config` | ❌ |
| 4 | 0.361500 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.357119 | `azmcp-postgres-server-param` | ❌ |
| 6 | 0.351744 | `azmcp-postgres-table-schema` | ❌ |
| 7 | 0.349152 | `azmcp-kusto-table-schema` | ❌ |
| 8 | 0.343310 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.339765 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.337996 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 128

**Expected Tool:** `azmcp-sql-db-show`  
**Prompt:** Get the configuration details for the SQL database <database_name> on server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.593150 | `azmcp-postgres-server-config` | ❌ |
| 2 | 0.528136 | `azmcp-sql-db-show` | ✅ **EXPECTED** |
| 3 | 0.446682 | `azmcp-postgres-server-param` | ❌ |
| 4 | 0.374073 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 5 | 0.371766 | `azmcp-loadtesting-test-get` | ❌ |
| 6 | 0.354111 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 7 | 0.348227 | `azmcp-sql-elastic-pool-list` | ❌ |
| 8 | 0.341701 | `azmcp-postgres-database-list` | ❌ |
| 9 | 0.341203 | `azmcp-postgres-table-schema` | ❌ |
| 10 | 0.330440 | `azmcp-kusto-table-schema` | ❌ |

---

## Test 129

**Expected Tool:** `azmcp-sql-elastic-pool-list`  
**Prompt:** List all elastic pools in SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.686435 | `azmcp-sql-elastic-pool-list` | ✅ **EXPECTED** |
| 2 | 0.434570 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.431871 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 4 | 0.431174 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.416273 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.414738 | `azmcp-postgres-database-list` | ❌ |
| 7 | 0.412061 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 8 | 0.409078 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.408053 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.401435 | `azmcp-search-index-list` | ❌ |

---

## Test 130

**Expected Tool:** `azmcp-sql-elastic-pool-list`  
**Prompt:** Show me the elastic pools configured for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.616579 | `azmcp-sql-elastic-pool-list` | ✅ **EXPECTED** |
| 2 | 0.385834 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.378556 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.357655 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 5 | 0.357019 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.354094 | `azmcp-sql-db-show` | ❌ |
| 7 | 0.335615 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.324569 | `azmcp-search-index-list` | ❌ |
| 9 | 0.323509 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.319977 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 131

**Expected Tool:** `azmcp-sql-elastic-pool-list`  
**Prompt:** What elastic pools are available in my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.602478 | `azmcp-sql-elastic-pool-list` | ✅ **EXPECTED** |
| 2 | 0.378527 | `azmcp-monitor-table-type-list` | ❌ |
| 3 | 0.344799 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.316044 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 5 | 0.311302 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.308077 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.307724 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.306214 | `azmcp-monitor-table-list` | ❌ |
| 9 | 0.298933 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.295933 | `azmcp-search-index-list` | ❌ |

---

## Test 132

**Expected Tool:** `azmcp-sql-firewall-rule-list`  
**Prompt:** List all firewall rules for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.732275 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 2 | 0.397092 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.385148 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.347004 | `azmcp-sql-elastic-pool-list` | ❌ |
| 5 | 0.327808 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.318412 | `azmcp-search-index-list` | ❌ |
| 7 | 0.304175 | `azmcp-monitor-table-list` | ❌ |
| 8 | 0.301711 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.299205 | `azmcp-postgres-server-config` | ❌ |
| 10 | 0.298226 | `azmcp-sql-db-show` | ❌ |

---

## Test 133

**Expected Tool:** `azmcp-sql-firewall-rule-list`  
**Prompt:** Show me the firewall rules for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.631499 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 2 | 0.321414 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.312035 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.290374 | `azmcp-extension-az` | ❌ |
| 5 | 0.290235 | `azmcp-postgres-server-config` | ❌ |
| 6 | 0.287747 | `azmcp-postgres-server-param` | ❌ |
| 7 | 0.272586 | `azmcp-sql-elastic-pool-list` | ❌ |
| 8 | 0.272053 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.252680 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.244086 | `azmcp-search-index-list` | ❌ |

---

## Test 134

**Expected Tool:** `azmcp-sql-firewall-rule-list`  
**Prompt:** What firewall rules are configured for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633622 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 2 | 0.311867 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.299474 | `azmcp-extension-az` | ❌ |
| 4 | 0.277628 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.261404 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.261123 | `azmcp-postgres-server-param` | ❌ |
| 7 | 0.258402 | `azmcp-sql-elastic-pool-list` | ❌ |
| 8 | 0.227217 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 9 | 0.223532 | `azmcp-postgres-server-setparam` | ❌ |
| 10 | 0.220809 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |

---

## Test 135

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** List Microsoft Entra ID administrators for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.788356 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.407432 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 3 | 0.365636 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.328968 | `azmcp-sql-elastic-pool-list` | ❌ |
| 5 | 0.328737 | `azmcp-role-assignment-list` | ❌ |
| 6 | 0.312627 | `azmcp-postgres-database-list` | ❌ |
| 7 | 0.280450 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.279198 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.277773 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.276401 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 136

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** Show me the Entra ID administrators configured for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.718251 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.311085 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.309059 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 4 | 0.303560 | `azmcp-postgres-server-config` | ❌ |
| 5 | 0.268897 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.266264 | `azmcp-postgres-server-param` | ❌ |
| 7 | 0.250838 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.249616 | `azmcp-postgres-database-list` | ❌ |
| 9 | 0.228064 | `azmcp-role-assignment-list` | ❌ |
| 10 | 0.214529 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 137

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** What Microsoft Entra ID administrators are set up for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651416 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.245296 | `azmcp-extension-az` | ❌ |
| 3 | 0.230451 | `azmcp-sql-elastic-pool-list` | ❌ |
| 4 | 0.228627 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 5 | 0.218292 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.205556 | `azmcp-sql-db-show` | ❌ |
| 7 | 0.200200 | `azmcp-monitor-resource-log-query` | ❌ |
| 8 | 0.191563 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 9 | 0.190179 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.189283 | `azmcp-postgres-server-param` | ❌ |

---

## Test 138

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
| 6 | 0.554722 | `azmcp-subscription-list` | ❌ |
| 7 | 0.539741 | `azmcp-search-service-list` | ❌ |
| 8 | 0.531542 | `azmcp-appconfig-account-list` | ❌ |
| 9 | 0.526979 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.497662 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 139

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** Show me the storage accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.701826 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.595460 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.583848 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.574454 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.504685 | `azmcp-subscription-list` | ❌ |
| 6 | 0.502734 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.490259 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.480775 | `azmcp-search-service-list` | ❌ |
| 9 | 0.472516 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.455386 | `azmcp-keyvault-key-list` | ❌ |

---

## Test 140

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
| 6 | 0.431671 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.405218 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 8 | 0.402931 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.393156 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.375146 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 141

**Expected Tool:** `azmcp-storage-blob-container-details`  
**Prompt:** Show me the properties of the storage container files in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.678061 | `azmcp-storage-blob-container-details` | ✅ **EXPECTED** |
| 2 | 0.678011 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.611693 | `azmcp-storage-blob-list` | ❌ |
| 4 | 0.537937 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.530648 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.515548 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.452341 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 8 | 0.437454 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.432791 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.407465 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 142

**Expected Tool:** `azmcp-storage-blob-container-list`  
**Prompt:** List all blob containers in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.764995 | `azmcp-storage-blob-container-list` | ✅ **EXPECTED** |
| 2 | 0.743500 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.629987 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.557097 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.554192 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.540541 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.468593 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.460731 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.414549 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 10 | 0.383247 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 143

**Expected Tool:** `azmcp-storage-blob-container-list`  
**Prompt:** Show me the blob containers in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.697693 | `azmcp-storage-blob-container-list` | ✅ **EXPECTED** |
| 2 | 0.673822 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.578331 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.539233 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.505990 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.504109 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.447566 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.410884 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.380842 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 10 | 0.377462 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 144

**Expected Tool:** `azmcp-storage-blob-list`  
**Prompt:** List all blobs in the blob container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.795420 | `azmcp-storage-blob-list` | ✅ **EXPECTED** |
| 2 | 0.715105 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.596810 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.585823 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.535735 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.524098 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.449480 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.429272 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 9 | 0.423178 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.414716 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 145

**Expected Tool:** `azmcp-storage-blob-list`  
**Prompt:** Show me the blobs in the blob container <container_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.701477 | `azmcp-storage-blob-list` | ✅ **EXPECTED** |
| 2 | 0.649625 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.555885 | `azmcp-storage-blob-container-details` | ❌ |
| 4 | 0.550249 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.476647 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.447605 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.413225 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 8 | 0.401214 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.383449 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 10 | 0.364907 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 146

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** List all paths in the Data Lake file system <file_system_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.860114 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.493098 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.486390 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.476297 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.461279 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.423761 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.414332 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.402737 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.401558 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.382355 | `azmcp-monitor-table-type-list` | ❌ |

---

## Test 147

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** Show me the paths in the Data Lake file system <file_system_name> in the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.804437 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.438075 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.436104 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.413277 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.396638 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.368149 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.353149 | `azmcp-cosmos-database-container-list` | ❌ |
| 8 | 0.351701 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.344977 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.344192 | `azmcp-storage-blob-container-details` | ❌ |

---

## Test 148

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
| 10 | 0.492182 | `azmcp-postgres-table-list` | ❌ |

---

## Test 149

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
| 9 | 0.467811 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.463396 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 150

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** List all subscriptions for my account  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.575972 | `azmcp-subscription-list` | ✅ **EXPECTED** |
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

## Test 151

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** What subscriptions do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.403195 | `azmcp-subscription-list` | ✅ **EXPECTED** |
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

## Test 152

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** Show me my subscriptions  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.405703 | `azmcp-subscription-list` | ✅ **EXPECTED** |
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

## Test 153

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** What is my current subscription?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.319927 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.286711 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.285063 | `azmcp-search-service-list` | ❌ |
| 4 | 0.282645 | `azmcp-grafana-list` | ❌ |
| 5 | 0.279702 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.278798 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.272849 | `azmcp-marketplace-product-get` | ❌ |
| 8 | 0.256358 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.252448 | `azmcp-loadtesting-testresource-list` | ❌ |
| 10 | 0.250314 | `azmcp-servicebus-topic-subscription-details` | ❌ |

---

## Test 154

**Expected Tool:** `azmcp-azureterraformbestpractices-get`  
**Prompt:** Fetch the Azure Terraform best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.719967 | `azmcp-azureterraformbestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.565726 | `azmcp-bestpractices-general-get` | ❌ |
| 3 | 0.562657 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 4 | 0.541160 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 5 | 0.459270 | `azmcp-extension-az` | ❌ |
| 6 | 0.354838 | `azmcp-bicepschema-get` | ❌ |
| 7 | 0.331791 | `azmcp-extension-azd` | ❌ |
| 8 | 0.312030 | `azmcp-marketplace-product-get` | ❌ |
| 9 | 0.309265 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.302784 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 155

**Expected Tool:** `azmcp-azureterraformbestpractices-get`  
**Prompt:** Show me the Azure Terraform best practices and generate code sample to get a secret from Azure Key Vault  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.596382 | `azmcp-azureterraformbestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.529100 | `azmcp-bestpractices-general-get` | ❌ |
| 3 | 0.517083 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 4 | 0.488468 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 5 | 0.465277 | `azmcp-keyvault-secret-get` | ❌ |
| 6 | 0.420419 | `azmcp-keyvault-key-get` | ❌ |
| 7 | 0.406432 | `azmcp-extension-az` | ❌ |
| 8 | 0.362845 | `azmcp-keyvault-key-list` | ❌ |
| 9 | 0.345627 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.324534 | `azmcp-appconfig-kv-show` | ❌ |

---

## Test 156

**Expected Tool:** `azmcp-bicepschema-get`  
**Prompt:** How can I use Bicep to create an Azure OpenAI service?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.432409 | `azmcp-bicepschema-get` | ✅ **EXPECTED** |
| 2 | 0.401162 | `azmcp-extension-az` | ❌ |
| 3 | 0.400985 | `azmcp-foundry-models-deploy` | ❌ |
| 4 | 0.375228 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 5 | 0.374477 | `azmcp-bestpractices-general-get` | ❌ |
| 6 | 0.362766 | `azmcp-bestpractices-azurefunctions-get-code-generation` | ❌ |
| 7 | 0.353206 | `azmcp-bestpractices-azurefunctions-get-deployment` | ❌ |
| 8 | 0.345030 | `azmcp-search-service-list` | ❌ |
| 9 | 0.342237 | `azmcp-foundry-models-deployments-list` | ❌ |
| 10 | 0.341213 | `azmcp-search-index-list` | ❌ |

---

## Summary

**Total Prompts Tested:** 156  
**Execution Time:** 31.4684098s  

### Success Rate Metrics

**Top Choice Success:** 75.6% (118/156 tests)  
**High Confidence (≥0.5):** 76.9% (120/156 tests)  
**Top Choice + High Confidence:** 68.6% (107/156 tests)  

### Success Rate Analysis

🟠 **Fair** - The tool selection system needs significant improvement.

