# Tool Selection Analysis Setup

**Setup completed:** 2025-08-19 14:39:57  
**Tool count:** 118  
**Database setup time:** 11.6864245s  

---

# Tool Selection Analysis Results

**Analysis Date:** 2025-08-19 14:39:57  
**Tool count:** 118  

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
- [Test 31: azmcp-acr-registry-repository-list](#test-31)
- [Test 32: azmcp-acr-registry-repository-list](#test-32)
- [Test 33: azmcp-acr-registry-repository-list](#test-33)
- [Test 34: azmcp-acr-registry-repository-list](#test-34)
- [Test 35: azmcp-cosmos-account-list](#test-35)
- [Test 36: azmcp-cosmos-account-list](#test-36)
- [Test 37: azmcp-cosmos-account-list](#test-37)
- [Test 38: azmcp-cosmos-database-container-item-query](#test-38)
- [Test 39: azmcp-cosmos-database-container-list](#test-39)
- [Test 40: azmcp-cosmos-database-container-list](#test-40)
- [Test 41: azmcp-cosmos-database-list](#test-41)
- [Test 42: azmcp-cosmos-database-list](#test-42)
- [Test 43: azmcp-kusto-cluster-get](#test-43)
- [Test 44: azmcp-kusto-cluster-list](#test-44)
- [Test 45: azmcp-kusto-cluster-list](#test-45)
- [Test 46: azmcp-kusto-cluster-list](#test-46)
- [Test 47: azmcp-kusto-database-list](#test-47)
- [Test 48: azmcp-kusto-database-list](#test-48)
- [Test 49: azmcp-kusto-query](#test-49)
- [Test 50: azmcp-kusto-sample](#test-50)
- [Test 51: azmcp-kusto-table-list](#test-51)
- [Test 52: azmcp-kusto-table-list](#test-52)
- [Test 53: azmcp-kusto-table-schema](#test-53)
- [Test 54: azmcp-postgres-database-list](#test-54)
- [Test 55: azmcp-postgres-database-list](#test-55)
- [Test 56: azmcp-postgres-database-query](#test-56)
- [Test 57: azmcp-postgres-server-config-get](#test-57)
- [Test 58: azmcp-postgres-server-list](#test-58)
- [Test 59: azmcp-postgres-server-list](#test-59)
- [Test 60: azmcp-postgres-server-list](#test-60)
- [Test 61: azmcp-postgres-server-param](#test-61)
- [Test 62: azmcp-postgres-server-param-set](#test-62)
- [Test 63: azmcp-postgres-table-list](#test-63)
- [Test 64: azmcp-postgres-table-list](#test-64)
- [Test 65: azmcp-postgres-table-schema-get](#test-65)
- [Test 66: azmcp-extension-azd](#test-66)
- [Test 67: azmcp-extension-azd](#test-67)
- [Test 68: azmcp-deploy-app-logs-get](#test-68)
- [Test 69: azmcp-deploy-architecture-diagram-generate](#test-69)
- [Test 70: azmcp-deploy-iac-rules-get](#test-70)
- [Test 71: azmcp-deploy-pipeline-guidance-get](#test-71)
- [Test 72: azmcp-deploy-plan-get](#test-72)
- [Test 73: azmcp-functionapp-list](#test-73)
- [Test 74: azmcp-functionapp-list](#test-74)
- [Test 75: azmcp-functionapp-list](#test-75)
- [Test 76: azmcp-keyvault-certificate-create](#test-76)
- [Test 77: azmcp-keyvault-certificate-get](#test-77)
- [Test 78: azmcp-keyvault-certificate-get](#test-78)
- [Test 79: azmcp-keyvault-certificate-import](#test-79)
- [Test 80: azmcp-keyvault-certificate-import](#test-80)
- [Test 81: azmcp-keyvault-certificate-list](#test-81)
- [Test 82: azmcp-keyvault-certificate-list](#test-82)
- [Test 83: azmcp-keyvault-key-create](#test-83)
- [Test 84: azmcp-keyvault-key-list](#test-84)
- [Test 85: azmcp-keyvault-key-list](#test-85)
- [Test 86: azmcp-keyvault-secret-create](#test-86)
- [Test 87: azmcp-keyvault-secret-list](#test-87)
- [Test 88: azmcp-keyvault-secret-list](#test-88)
- [Test 89: azmcp-aks-cluster-get](#test-89)
- [Test 90: azmcp-aks-cluster-get](#test-90)
- [Test 91: azmcp-aks-cluster-get](#test-91)
- [Test 92: azmcp-aks-cluster-get](#test-92)
- [Test 93: azmcp-aks-cluster-list](#test-93)
- [Test 94: azmcp-aks-cluster-list](#test-94)
- [Test 95: azmcp-aks-cluster-list](#test-95)
- [Test 96: azmcp-loadtesting-test-create](#test-96)
- [Test 97: azmcp-loadtesting-test-get](#test-97)
- [Test 98: azmcp-loadtesting-testresource-create](#test-98)
- [Test 99: azmcp-loadtesting-testresource-list](#test-99)
- [Test 100: azmcp-loadtesting-testrun-create](#test-100)
- [Test 101: azmcp-loadtesting-testrun-get](#test-101)
- [Test 102: azmcp-loadtesting-testrun-list](#test-102)
- [Test 103: azmcp-loadtesting-testrun-update](#test-103)
- [Test 104: azmcp-grafana-list](#test-104)
- [Test 105: azmcp-marketplace-product-get](#test-105)
- [Test 106: azmcp-bestpractices-get](#test-106)
- [Test 107: azmcp-bestpractices-get](#test-107)
- [Test 108: azmcp-bestpractices-get](#test-108)
- [Test 109: azmcp-bestpractices-get](#test-109)
- [Test 110: azmcp-bestpractices-get](#test-110)
- [Test 111: azmcp-bestpractices-get](#test-111)
- [Test 112: azmcp-bestpractices-get](#test-112)
- [Test 113: azmcp-bestpractices-get](#test-113)
- [Test 114: azmcp-bestpractices-get](#test-114)
- [Test 115: azmcp-bestpractices-get](#test-115)
- [Test 116: azmcp-monitor-healthmodels-entity-gethealth](#test-116)
- [Test 117: azmcp-monitor-metrics-definitions](#test-117)
- [Test 118: azmcp-monitor-metrics-definitions](#test-118)
- [Test 119: azmcp-monitor-metrics-definitions](#test-119)
- [Test 120: azmcp-monitor-metrics-query](#test-120)
- [Test 121: azmcp-monitor-metrics-query](#test-121)
- [Test 122: azmcp-monitor-metrics-query](#test-122)
- [Test 123: azmcp-monitor-metrics-query](#test-123)
- [Test 124: azmcp-monitor-metrics-query](#test-124)
- [Test 125: azmcp-monitor-metrics-query](#test-125)
- [Test 126: azmcp-monitor-resource-log-query](#test-126)
- [Test 127: azmcp-monitor-table-list](#test-127)
- [Test 128: azmcp-monitor-table-list](#test-128)
- [Test 129: azmcp-monitor-table-type-list](#test-129)
- [Test 130: azmcp-monitor-table-type-list](#test-130)
- [Test 131: azmcp-monitor-workspace-list](#test-131)
- [Test 132: azmcp-monitor-workspace-list](#test-132)
- [Test 133: azmcp-monitor-workspace-list](#test-133)
- [Test 134: azmcp-monitor-workspace-log-query](#test-134)
- [Test 135: azmcp-datadog-monitoredresources-list](#test-135)
- [Test 136: azmcp-datadog-monitoredresources-list](#test-136)
- [Test 137: azmcp-extension-azqr](#test-137)
- [Test 138: azmcp-extension-azqr](#test-138)
- [Test 139: azmcp-extension-azqr](#test-139)
- [Test 140: azmcp-quota-region-availability-list](#test-140)
- [Test 141: azmcp-quota-usage-check](#test-141)
- [Test 142: azmcp-role-assignment-list](#test-142)
- [Test 143: azmcp-role-assignment-list](#test-143)
- [Test 144: azmcp-redis-cache-accesspolicy-list](#test-144)
- [Test 145: azmcp-redis-cache-accesspolicy-list](#test-145)
- [Test 146: azmcp-redis-cache-list](#test-146)
- [Test 147: azmcp-redis-cache-list](#test-147)
- [Test 148: azmcp-redis-cache-list](#test-148)
- [Test 149: azmcp-redis-cluster-database-list](#test-149)
- [Test 150: azmcp-redis-cluster-database-list](#test-150)
- [Test 151: azmcp-redis-cluster-list](#test-151)
- [Test 152: azmcp-redis-cluster-list](#test-152)
- [Test 153: azmcp-redis-cluster-list](#test-153)
- [Test 154: azmcp-group-list](#test-154)
- [Test 155: azmcp-group-list](#test-155)
- [Test 156: azmcp-group-list](#test-156)
- [Test 157: azmcp-resourcehealth-availability-status-get](#test-157)
- [Test 158: azmcp-resourcehealth-availability-status-get](#test-158)
- [Test 159: azmcp-resourcehealth-availability-status-get](#test-159)
- [Test 160: azmcp-resourcehealth-availability-status-list](#test-160)
- [Test 161: azmcp-resourcehealth-availability-status-list](#test-161)
- [Test 162: azmcp-resourcehealth-availability-status-list](#test-162)
- [Test 163: azmcp-servicebus-queue-details](#test-163)
- [Test 164: azmcp-servicebus-topic-details](#test-164)
- [Test 165: azmcp-servicebus-topic-subscription-details](#test-165)
- [Test 166: azmcp-sql-db-list](#test-166)
- [Test 167: azmcp-sql-db-list](#test-167)
- [Test 168: azmcp-sql-db-show](#test-168)
- [Test 169: azmcp-sql-db-show](#test-169)
- [Test 170: azmcp-sql-elastic-pool-list](#test-170)
- [Test 171: azmcp-sql-elastic-pool-list](#test-171)
- [Test 172: azmcp-sql-elastic-pool-list](#test-172)
- [Test 173: azmcp-sql-server-entra-admin-list](#test-173)
- [Test 174: azmcp-sql-server-entra-admin-list](#test-174)
- [Test 175: azmcp-sql-server-entra-admin-list](#test-175)
- [Test 176: azmcp-sql-server-firewall-rule-list](#test-176)
- [Test 177: azmcp-sql-server-firewall-rule-list](#test-177)
- [Test 178: azmcp-sql-server-firewall-rule-list](#test-178)
- [Test 179: azmcp-storage-account-create](#test-179)
- [Test 180: azmcp-storage-account-create](#test-180)
- [Test 181: azmcp-storage-account-create](#test-181)
- [Test 182: azmcp-storage-account-details](#test-182)
- [Test 183: azmcp-storage-account-details](#test-183)
- [Test 184: azmcp-storage-account-list](#test-184)
- [Test 185: azmcp-storage-account-list](#test-185)
- [Test 186: azmcp-storage-account-list](#test-186)
- [Test 187: azmcp-storage-blob-batch-set-tier](#test-187)
- [Test 188: azmcp-storage-blob-batch-set-tier](#test-188)
- [Test 189: azmcp-storage-blob-container-create](#test-189)
- [Test 190: azmcp-storage-blob-container-create](#test-190)
- [Test 191: azmcp-storage-blob-container-create](#test-191)
- [Test 192: azmcp-storage-blob-container-details](#test-192)
- [Test 193: azmcp-storage-blob-container-list](#test-193)
- [Test 194: azmcp-storage-blob-container-list](#test-194)
- [Test 195: azmcp-storage-blob-details](#test-195)
- [Test 196: azmcp-storage-blob-details](#test-196)
- [Test 197: azmcp-storage-blob-list](#test-197)
- [Test 198: azmcp-storage-blob-list](#test-198)
- [Test 199: azmcp-storage-blob-upload](#test-199)
- [Test 200: azmcp-storage-blob-upload](#test-200)
- [Test 201: azmcp-storage-blob-upload](#test-201)
- [Test 202: azmcp-storage-datalake-directory-create](#test-202)
- [Test 203: azmcp-storage-datalake-file-system-list-paths](#test-203)
- [Test 204: azmcp-storage-datalake-file-system-list-paths](#test-204)
- [Test 205: azmcp-storage-datalake-file-system-list-paths](#test-205)
- [Test 206: azmcp-storage-queue-message-send](#test-206)
- [Test 207: azmcp-storage-queue-message-send](#test-207)
- [Test 208: azmcp-storage-queue-message-send](#test-208)
- [Test 209: azmcp-storage-share-file-list](#test-209)
- [Test 210: azmcp-storage-share-file-list](#test-210)
- [Test 211: azmcp-storage-share-file-list](#test-211)
- [Test 212: azmcp-storage-table-list](#test-212)
- [Test 213: azmcp-storage-table-list](#test-213)
- [Test 214: azmcp-subscription-list](#test-214)
- [Test 215: azmcp-subscription-list](#test-215)
- [Test 216: azmcp-subscription-list](#test-216)
- [Test 217: azmcp-subscription-list](#test-217)
- [Test 218: azmcp-azureterraformbestpractices-get](#test-218)
- [Test 219: azmcp-azureterraformbestpractices-get](#test-219)
- [Test 220: azmcp-virtualdesktop-hostpool-list](#test-220)
- [Test 221: azmcp-virtualdesktop-hostpool-sessionhost-list](#test-221)
- [Test 222: azmcp-virtualdesktop-hostpool-sessionhost-usersession-list](#test-222)
- [Test 223: azmcp-workbooks-create](#test-223)
- [Test 224: azmcp-workbooks-delete](#test-224)
- [Test 225: azmcp-workbooks-list](#test-225)
- [Test 226: azmcp-workbooks-list](#test-226)
- [Test 227: azmcp-workbooks-show](#test-227)
- [Test 228: azmcp-workbooks-show](#test-228)
- [Test 229: azmcp-workbooks-update](#test-229)
- [Test 230: azmcp-bicepschema-get](#test-230)

---

## Test 1

**Expected Tool:** `azmcp-foundry-models-deploy`  
**Prompt:** Deploy a GPT4o instance on my resource <resource-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.313400 | `azmcp-foundry-models-deploy` | ✅ **EXPECTED** |
| 2 | 0.274011 | `azmcp-deploy-plan-get` | ❌ |
| 3 | 0.269513 | `azmcp-loadtesting-testresource-create` | ❌ |
| 4 | 0.268967 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 5 | 0.234071 | `azmcp-deploy-iac-rules-get` | ❌ |
| 6 | 0.222504 | `azmcp-grafana-list` | ❌ |
| 7 | 0.222478 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.221635 | `azmcp-workbooks-create` | ❌ |
| 9 | 0.219005 | `azmcp-storage-account-create` | ❌ |
| 10 | 0.218848 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 11 | 0.215293 | `azmcp-loadtesting-testrun-create` | ❌ |
| 12 | 0.215098 | `azmcp-monitor-resource-log-query` | ❌ |
| 13 | 0.208401 | `azmcp-loadtesting-test-create` | ❌ |
| 14 | 0.208124 | `azmcp-quota-region-availability-list` | ❌ |
| 15 | 0.207601 | `azmcp-quota-usage-check` | ❌ |
| 16 | 0.206600 | `azmcp-bestpractices-get` | ❌ |
| 17 | 0.204420 | `azmcp-postgres-server-param-set` | ❌ |
| 18 | 0.195615 | `azmcp-workbooks-list` | ❌ |
| 19 | 0.192420 | `azmcp-monitor-metrics-query` | ❌ |
| 20 | 0.190167 | `azmcp-redis-cluster-list` | ❌ |

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
| 6 | 0.368173 | `azmcp-deploy-plan-get` | ❌ |
| 7 | 0.334867 | `azmcp-grafana-list` | ❌ |
| 8 | 0.318854 | `azmcp-postgres-server-list` | ❌ |
| 9 | 0.312247 | `azmcp-loadtesting-testrun-list` | ❌ |
| 10 | 0.310280 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 11 | 0.306178 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 12 | 0.302262 | `azmcp-monitor-table-type-list` | ❌ |
| 13 | 0.301402 | `azmcp-redis-cluster-list` | ❌ |
| 14 | 0.300018 | `azmcp-deploy-app-logs-get` | ❌ |
| 15 | 0.295411 | `azmcp-functionapp-list` | ❌ |
| 16 | 0.289448 | `azmcp-monitor-workspace-list` | ❌ |
| 17 | 0.288227 | `azmcp-redis-cache-list` | ❌ |
| 18 | 0.285916 | `azmcp-quota-region-availability-list` | ❌ |
| 19 | 0.284874 | `azmcp-monitor-table-list` | ❌ |
| 20 | 0.274109 | `azmcp-subscription-list` | ❌ |

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
| 6 | 0.328814 | `azmcp-deploy-plan-get` | ❌ |
| 7 | 0.305997 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 8 | 0.301514 | `azmcp-deploy-app-logs-get` | ❌ |
| 9 | 0.286814 | `azmcp-grafana-list` | ❌ |
| 10 | 0.272816 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 11 | 0.265906 | `azmcp-extension-azd` | ❌ |
| 12 | 0.259989 | `azmcp-loadtesting-testrun-list` | ❌ |
| 13 | 0.254926 | `azmcp-postgres-server-list` | ❌ |
| 14 | 0.250491 | `azmcp-redis-cluster-list` | ❌ |
| 15 | 0.246893 | `azmcp-quota-region-availability-list` | ❌ |
| 16 | 0.243133 | `azmcp-monitor-table-type-list` | ❌ |
| 17 | 0.238612 | `azmcp-search-index-describe` | ❌ |
| 18 | 0.234081 | `azmcp-redis-cache-list` | ❌ |
| 19 | 0.233993 | `azmcp-quota-usage-check` | ❌ |
| 20 | 0.232469 | `azmcp-monitor-workspace-list` | ❌ |

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
| 11 | 0.248607 | `azmcp-redis-cache-list` | ❌ |
| 12 | 0.247709 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.245193 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 14 | 0.245005 | `azmcp-loadtesting-testrun-list` | ❌ |
| 15 | 0.243429 | `azmcp-postgres-server-list` | ❌ |
| 16 | 0.242253 | `azmcp-redis-cluster-list` | ❌ |
| 17 | 0.240253 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 18 | 0.231110 | `azmcp-monitor-metrics-definitions` | ❌ |
| 19 | 0.229457 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 20 | 0.226117 | `azmcp-keyvault-certificate-list` | ❌ |

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
| 6 | 0.266937 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 7 | 0.245943 | `azmcp-quota-region-availability-list` | ❌ |
| 8 | 0.244697 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.243617 | `azmcp-deploy-plan-get` | ❌ |
| 10 | 0.240256 | `azmcp-monitor-metrics-definitions` | ❌ |
| 11 | 0.237407 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 12 | 0.233079 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 13 | 0.231148 | `azmcp-grafana-list` | ❌ |
| 14 | 0.227966 | `azmcp-deploy-app-logs-get` | ❌ |
| 15 | 0.212188 | `azmcp-search-index-describe` | ❌ |
| 16 | 0.205424 | `azmcp-quota-usage-check` | ❌ |
| 17 | 0.203036 | `azmcp-search-index-query` | ❌ |
| 18 | 0.200059 | `azmcp-monitor-workspace-list` | ❌ |
| 19 | 0.199474 | `azmcp-redis-cluster-list` | ❌ |
| 20 | 0.196419 | `azmcp-resourcehealth-availability-status-list` | ❌ |

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
| 5 | 0.393953 | `azmcp-aks-cluster-get` | ❌ |
| 6 | 0.389306 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.358384 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.356252 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.338129 | `azmcp-storage-account-details` | ❌ |
| 10 | 0.330061 | `azmcp-kusto-table-schema` | ❌ |
| 11 | 0.327156 | `azmcp-workbooks-show` | ❌ |
| 12 | 0.326847 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.326590 | `azmcp-storage-blob-container-details` | ❌ |
| 14 | 0.325015 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.322423 | `azmcp-monitor-table-list` | ❌ |
| 16 | 0.320890 | `azmcp-foundry-models-deployments-list` | ❌ |
| 17 | 0.316039 | `azmcp-appconfig-kv-show` | ❌ |
| 18 | 0.313076 | `azmcp-keyvault-certificate-get` | ❌ |
| 19 | 0.312515 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.312237 | `azmcp-cosmos-database-list` | ❌ |

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
| 10 | 0.382791 | `azmcp-keyvault-key-list` | ❌ |
| 11 | 0.378710 | `azmcp-kusto-database-list` | ❌ |
| 12 | 0.378297 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.375372 | `azmcp-foundry-models-deployments-list` | ❌ |
| 14 | 0.369526 | `azmcp-keyvault-certificate-list` | ❌ |
| 15 | 0.368931 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.367331 | `azmcp-redis-cache-list` | ❌ |
| 17 | 0.362701 | `azmcp-keyvault-secret-list` | ❌ |
| 18 | 0.361922 | `azmcp-foundry-models-list` | ❌ |
| 19 | 0.360852 | `azmcp-redis-cluster-list` | ❌ |
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
| 11 | 0.350008 | `azmcp-kusto-database-list` | ❌ |
| 12 | 0.347566 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.341728 | `azmcp-foundry-models-list` | ❌ |
| 14 | 0.332289 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.331202 | `azmcp-keyvault-key-list` | ❌ |
| 16 | 0.330418 | `azmcp-kusto-table-list` | ❌ |
| 17 | 0.328175 | `azmcp-redis-cluster-list` | ❌ |
| 18 | 0.327223 | `azmcp-monitor-metrics-definitions` | ❌ |
| 19 | 0.324039 | `azmcp-redis-cache-list` | ❌ |
| 20 | 0.323041 | `azmcp-cosmos-database-container-list` | ❌ |

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
| 9 | 0.290809 | `azmcp-monitor-metrics-query` | ❌ |
| 10 | 0.288242 | `azmcp-foundry-models-deployments-list` | ❌ |
| 11 | 0.283532 | `azmcp-foundry-models-list` | ❌ |
| 12 | 0.269913 | `azmcp-monitor-table-list` | ❌ |
| 13 | 0.254226 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.248402 | `azmcp-monitor-table-type-list` | ❌ |
| 15 | 0.244844 | `azmcp-kusto-sample` | ❌ |
| 16 | 0.243984 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.235536 | `azmcp-cosmos-database-container-list` | ❌ |
| 18 | 0.232713 | `azmcp-loadtesting-testrun-get` | ❌ |
| 19 | 0.229137 | `azmcp-cosmos-database-list` | ❌ |
| 20 | 0.228053 | `azmcp-deploy-architecture-diagram-generate` | ❌ |

---

## Test 10

**Expected Tool:** `azmcp-search-service-list`  
**Prompt:** List all Cognitive Search services in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.745450 | `azmcp-search-service-list` | ✅ **EXPECTED** |
| 2 | 0.608251 | `azmcp-search-index-list` | ❌ |
| 3 | 0.500465 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.494272 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.493169 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.492228 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.486066 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.482464 | `azmcp-grafana-list` | ❌ |
| 9 | 0.477637 | `azmcp-subscription-list` | ❌ |
| 10 | 0.470384 | `azmcp-kusto-cluster-list` | ❌ |
| 11 | 0.467613 | `azmcp-functionapp-list` | ❌ |
| 12 | 0.454460 | `azmcp-foundry-models-deployments-list` | ❌ |
| 13 | 0.451893 | `azmcp-aks-cluster-list` | ❌ |
| 14 | 0.441643 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.427817 | `azmcp-group-list` | ❌ |
| 16 | 0.425463 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 17 | 0.418396 | `azmcp-quota-region-availability-list` | ❌ |
| 18 | 0.417472 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.414984 | `azmcp-foundry-models-list` | ❌ |
| 20 | 0.408684 | `azmcp-loadtesting-testresource-list` | ❌ |

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
| 5 | 0.408624 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.400265 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.399822 | `azmcp-grafana-list` | ❌ |
| 8 | 0.397883 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.393900 | `azmcp-subscription-list` | ❌ |
| 10 | 0.390559 | `azmcp-foundry-models-list` | ❌ |
| 11 | 0.384559 | `azmcp-postgres-server-list` | ❌ |
| 12 | 0.382118 | `azmcp-functionapp-list` | ❌ |
| 13 | 0.376962 | `azmcp-quota-region-availability-list` | ❌ |
| 14 | 0.376950 | `azmcp-search-index-describe` | ❌ |
| 15 | 0.376089 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.374692 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 17 | 0.363444 | `azmcp-aks-cluster-list` | ❌ |
| 18 | 0.362366 | `azmcp-marketplace-product-get` | ❌ |
| 19 | 0.360230 | `azmcp-loadtesting-testresource-list` | ❌ |
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
| 5 | 0.336174 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 6 | 0.329777 | `azmcp-search-index-query` | ❌ |
| 7 | 0.322540 | `azmcp-foundry-models-list` | ❌ |
| 8 | 0.290214 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.283512 | `azmcp-redis-cluster-list` | ❌ |
| 10 | 0.281134 | `azmcp-monitor-workspace-list` | ❌ |
| 11 | 0.278539 | `azmcp-redis-cache-list` | ❌ |
| 12 | 0.276660 | `azmcp-extension-az` | ❌ |
| 13 | 0.274081 | `azmcp-monitor-table-type-list` | ❌ |
| 14 | 0.272173 | `azmcp-monitor-table-list` | ❌ |
| 15 | 0.266990 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 16 | 0.266987 | `azmcp-cosmos-database-list` | ❌ |
| 17 | 0.264394 | `azmcp-grafana-list` | ❌ |
| 18 | 0.261383 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.257982 | `azmcp-deploy-app-logs-get` | ❌ |
| 20 | 0.256627 | `azmcp-deploy-plan-get` | ❌ |

---

## Test 13

**Expected Tool:** `azmcp-appconfig-account-list`  
**Prompt:** List all App Configuration stores in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.786360 | `azmcp-appconfig-account-list` | ✅ **EXPECTED** |
| 2 | 0.635561 | `azmcp-appconfig-kv-list` | ❌ |
| 3 | 0.492152 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.491380 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.473643 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.460643 | `azmcp-functionapp-list` | ❌ |
| 7 | 0.458540 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.442214 | `azmcp-grafana-list` | ❌ |
| 9 | 0.441656 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.429305 | `azmcp-search-service-list` | ❌ |
| 11 | 0.427868 | `azmcp-subscription-list` | ❌ |
| 12 | 0.427460 | `azmcp-appconfig-kv-show` | ❌ |
| 13 | 0.420794 | `azmcp-kusto-cluster-list` | ❌ |
| 14 | 0.408599 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.404636 | `azmcp-storage-table-list` | ❌ |
| 16 | 0.398419 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.387471 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.387179 | `azmcp-appconfig-kv-delete` | ❌ |
| 19 | 0.385938 | `azmcp-sql-db-list` | ❌ |
| 20 | 0.380818 | `azmcp-quota-region-availability-list` | ❌ |

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
| 5 | 0.368795 | `azmcp-redis-cache-list` | ❌ |
| 6 | 0.368067 | `azmcp-functionapp-list` | ❌ |
| 7 | 0.359567 | `azmcp-postgres-server-config-get` | ❌ |
| 8 | 0.356598 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.354747 | `azmcp-appconfig-kv-delete` | ❌ |
| 10 | 0.348603 | `azmcp-appconfig-kv-set` | ❌ |
| 11 | 0.341263 | `azmcp-grafana-list` | ❌ |
| 12 | 0.331058 | `azmcp-storage-account-list` | ❌ |
| 13 | 0.326187 | `azmcp-subscription-list` | ❌ |
| 14 | 0.320605 | `azmcp-marketplace-product-get` | ❌ |
| 15 | 0.319835 | `azmcp-appconfig-kv-unlock` | ❌ |
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
| 1 | 0.565435 | `azmcp-appconfig-account-list` | ✅ **EXPECTED** |
| 2 | 0.564705 | `azmcp-appconfig-kv-list` | ❌ |
| 3 | 0.414689 | `azmcp-appconfig-kv-show` | ❌ |
| 4 | 0.355916 | `azmcp-postgres-server-config-get` | ❌ |
| 5 | 0.348661 | `azmcp-appconfig-kv-delete` | ❌ |
| 6 | 0.327234 | `azmcp-appconfig-kv-set` | ❌ |
| 7 | 0.305721 | `azmcp-appconfig-kv-unlock` | ❌ |
| 8 | 0.302405 | `azmcp-appconfig-kv-lock` | ❌ |
| 9 | 0.244080 | `azmcp-loadtesting-testrun-list` | ❌ |
| 10 | 0.237881 | `azmcp-loadtesting-test-get` | ❌ |
| 11 | 0.236404 | `azmcp-deploy-app-logs-get` | ❌ |
| 12 | 0.235204 | `azmcp-storage-account-details` | ❌ |
| 13 | 0.233357 | `azmcp-postgres-server-list` | ❌ |
| 14 | 0.231647 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.230170 | `azmcp-storage-blob-container-list` | ❌ |
| 16 | 0.221405 | `azmcp-postgres-database-list` | ❌ |
| 17 | 0.216170 | `azmcp-redis-cluster-list` | ❌ |
| 18 | 0.214205 | `azmcp-storage-account-list` | ❌ |
| 19 | 0.209941 | `azmcp-sql-db-list` | ❌ |
| 20 | 0.208606 | `azmcp-storage-blob-container-details` | ❌ |

---

## Test 16

**Expected Tool:** `azmcp-appconfig-kv-delete`  
**Prompt:** Delete the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.618277 | `azmcp-appconfig-kv-delete` | ✅ **EXPECTED** |
| 2 | 0.486631 | `azmcp-appconfig-kv-list` | ❌ |
| 3 | 0.443998 | `azmcp-appconfig-kv-lock` | ❌ |
| 4 | 0.443562 | `azmcp-appconfig-kv-unlock` | ❌ |
| 5 | 0.424344 | `azmcp-appconfig-kv-set` | ❌ |
| 6 | 0.399569 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.392016 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.264810 | `azmcp-workbooks-delete` | ❌ |
| 9 | 0.261891 | `azmcp-keyvault-key-create` | ❌ |
| 10 | 0.248752 | `azmcp-keyvault-key-list` | ❌ |
| 11 | 0.240483 | `azmcp-keyvault-secret-create` | ❌ |
| 12 | 0.194831 | `azmcp-postgres-server-config-get` | ❌ |
| 13 | 0.175589 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.173143 | `azmcp-postgres-server-param-set` | ❌ |
| 15 | 0.155805 | `azmcp-storage-account-details` | ❌ |
| 16 | 0.145096 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 17 | 0.141099 | `azmcp-postgres-server-param-get` | ❌ |
| 18 | 0.137771 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 19 | 0.135786 | `azmcp-redis-cache-list` | ❌ |
| 20 | 0.131936 | `azmcp-sql-db-list` | ❌ |

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
| 5 | 0.480701 | `azmcp-appconfig-kv-unlock` | ❌ |
| 6 | 0.464635 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.438315 | `azmcp-appconfig-kv-lock` | ❌ |
| 8 | 0.377534 | `azmcp-postgres-server-config-get` | ❌ |
| 9 | 0.374460 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.338142 | `azmcp-keyvault-secret-list` | ❌ |
| 11 | 0.329461 | `azmcp-loadtesting-testrun-list` | ❌ |
| 12 | 0.296908 | `azmcp-storage-account-details` | ❌ |
| 13 | 0.296084 | `azmcp-postgres-table-list` | ❌ |
| 14 | 0.292047 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.279679 | `azmcp-storage-table-list` | ❌ |
| 16 | 0.275400 | `azmcp-storage-blob-container-list` | ❌ |
| 17 | 0.267026 | `azmcp-postgres-database-list` | ❌ |
| 18 | 0.264833 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 19 | 0.263496 | `azmcp-monitor-table-list` | ❌ |
| 20 | 0.258876 | `azmcp-subscription-list` | ❌ |

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
| 5 | 0.488458 | `azmcp-appconfig-kv-unlock` | ❌ |
| 6 | 0.468503 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.458896 | `azmcp-appconfig-kv-lock` | ❌ |
| 8 | 0.370520 | `azmcp-postgres-server-config-get` | ❌ |
| 9 | 0.316879 | `azmcp-loadtesting-test-get` | ❌ |
| 10 | 0.296442 | `azmcp-storage-account-details` | ❌ |
| 11 | 0.294807 | `azmcp-keyvault-key-list` | ❌ |
| 12 | 0.282379 | `azmcp-loadtesting-testrun-list` | ❌ |
| 13 | 0.258688 | `azmcp-postgres-server-param-get` | ❌ |
| 14 | 0.248138 | `azmcp-storage-blob-container-details` | ❌ |
| 15 | 0.247879 | `azmcp-storage-blob-details` | ❌ |
| 16 | 0.243655 | `azmcp-postgres-server-param-set` | ❌ |
| 17 | 0.236389 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 18 | 0.233360 | `azmcp-redis-cache-list` | ❌ |
| 19 | 0.228684 | `azmcp-storage-blob-container-list` | ❌ |
| 20 | 0.225853 | `azmcp-storage-table-list` | ❌ |

---

## Test 19

**Expected Tool:** `azmcp-appconfig-kv-lock`  
**Prompt:** Lock the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.646614 | `azmcp-appconfig-kv-lock` | ✅ **EXPECTED** |
| 2 | 0.517242 | `azmcp-appconfig-kv-unlock` | ❌ |
| 3 | 0.508804 | `azmcp-appconfig-kv-list` | ❌ |
| 4 | 0.445551 | `azmcp-appconfig-kv-set` | ❌ |
| 5 | 0.431516 | `azmcp-appconfig-kv-delete` | ❌ |
| 6 | 0.423650 | `azmcp-appconfig-kv-show` | ❌ |
| 7 | 0.373656 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.250998 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.238544 | `azmcp-keyvault-secret-create` | ❌ |
| 10 | 0.238242 | `azmcp-postgres-server-param-set` | ❌ |
| 11 | 0.211331 | `azmcp-postgres-server-config-get` | ❌ |
| 12 | 0.208057 | `azmcp-keyvault-key-list` | ❌ |
| 13 | 0.191549 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.160992 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 15 | 0.154529 | `azmcp-postgres-server-param-get` | ❌ |
| 16 | 0.150689 | `azmcp-storage-account-details` | ❌ |
| 17 | 0.144377 | `azmcp-servicebus-queue-details` | ❌ |
| 18 | 0.135403 | `azmcp-storage-blob-container-details` | ❌ |
| 19 | 0.123426 | `azmcp-search-index-describe` | ❌ |
| 20 | 0.116471 | `azmcp-postgres-table-schema-get` | ❌ |

---

## Test 20

**Expected Tool:** `azmcp-appconfig-kv-set`  
**Prompt:** Set the key <key_name> in App Configuration store <app_config_store_name> to <value>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609728 | `azmcp-appconfig-kv-set` | ✅ **EXPECTED** |
| 2 | 0.541978 | `azmcp-appconfig-kv-lock` | ❌ |
| 3 | 0.518617 | `azmcp-appconfig-kv-list` | ❌ |
| 4 | 0.508907 | `azmcp-appconfig-kv-unlock` | ❌ |
| 5 | 0.507353 | `azmcp-appconfig-kv-show` | ❌ |
| 6 | 0.505712 | `azmcp-appconfig-kv-delete` | ❌ |
| 7 | 0.377944 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.346997 | `azmcp-postgres-server-param-set` | ❌ |
| 9 | 0.311477 | `azmcp-keyvault-secret-create` | ❌ |
| 10 | 0.305688 | `azmcp-keyvault-key-create` | ❌ |
| 11 | 0.221137 | `azmcp-loadtesting-test-create` | ❌ |
| 12 | 0.208864 | `azmcp-postgres-server-config-get` | ❌ |
| 13 | 0.206846 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.201346 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 15 | 0.182108 | `azmcp-storage-account-details` | ❌ |
| 16 | 0.167038 | `azmcp-postgres-server-param-get` | ❌ |
| 17 | 0.136989 | `azmcp-storage-queue-message-send` | ❌ |
| 18 | 0.124205 | `azmcp-servicebus-queue-details` | ❌ |
| 19 | 0.123625 | `azmcp-storage-table-list` | ❌ |
| 20 | 0.122351 | `azmcp-search-index-describe` | ❌ |

---

## Test 21

**Expected Tool:** `azmcp-appconfig-kv-show`  
**Prompt:** Show the content for the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.603202 | `azmcp-appconfig-kv-list` | ❌ |
| 2 | 0.561484 | `azmcp-appconfig-kv-show` | ✅ **EXPECTED** |
| 3 | 0.448861 | `azmcp-appconfig-kv-set` | ❌ |
| 4 | 0.441694 | `azmcp-appconfig-kv-delete` | ❌ |
| 5 | 0.437409 | `azmcp-appconfig-account-list` | ❌ |
| 6 | 0.433801 | `azmcp-appconfig-kv-lock` | ❌ |
| 7 | 0.427700 | `azmcp-appconfig-kv-unlock` | ❌ |
| 8 | 0.301844 | `azmcp-keyvault-key-list` | ❌ |
| 9 | 0.291437 | `azmcp-postgres-server-config-get` | ❌ |
| 10 | 0.276983 | `azmcp-loadtesting-test-get` | ❌ |
| 11 | 0.260174 | `azmcp-keyvault-secret-list` | ❌ |
| 12 | 0.239956 | `azmcp-storage-account-details` | ❌ |
| 13 | 0.221821 | `azmcp-storage-blob-container-details` | ❌ |
| 14 | 0.217862 | `azmcp-postgres-server-param-get` | ❌ |
| 15 | 0.206434 | `azmcp-redis-cache-list` | ❌ |
| 16 | 0.205525 | `azmcp-storage-table-list` | ❌ |
| 17 | 0.205373 | `azmcp-storage-blob-container-list` | ❌ |
| 18 | 0.193407 | `azmcp-storage-blob-list` | ❌ |
| 19 | 0.191839 | `azmcp-storage-blob-details` | ❌ |
| 20 | 0.185986 | `azmcp-redis-cache-accesspolicy-list` | ❌ |

---

## Test 22

**Expected Tool:** `azmcp-appconfig-kv-unlock`  
**Prompt:** Unlock the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.602827 | `azmcp-appconfig-kv-unlock` | ✅ **EXPECTED** |
| 2 | 0.552023 | `azmcp-appconfig-kv-lock` | ❌ |
| 3 | 0.541501 | `azmcp-appconfig-kv-list` | ❌ |
| 4 | 0.476313 | `azmcp-appconfig-kv-delete` | ❌ |
| 5 | 0.435689 | `azmcp-appconfig-kv-show` | ❌ |
| 6 | 0.425368 | `azmcp-appconfig-kv-set` | ❌ |
| 7 | 0.409432 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.267591 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.259425 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.252708 | `azmcp-keyvault-secret-create` | ❌ |
| 11 | 0.225419 | `azmcp-postgres-server-config-get` | ❌ |
| 12 | 0.185078 | `azmcp-postgres-server-param-set` | ❌ |
| 13 | 0.179223 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.169387 | `azmcp-storage-account-details` | ❌ |
| 15 | 0.159820 | `azmcp-postgres-server-param-get` | ❌ |
| 16 | 0.148867 | `azmcp-storage-blob-container-details` | ❌ |
| 17 | 0.145471 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 18 | 0.143560 | `azmcp-servicebus-queue-details` | ❌ |
| 19 | 0.132385 | `azmcp-search-index-describe` | ❌ |
| 20 | 0.131103 | `azmcp-workbooks-delete` | ❌ |

---

## Test 23

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** Create a Storage account with name <storage_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628655 | `azmcp-storage-account-create` | ❌ |
| 2 | 0.472375 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.455521 | `azmcp-storage-account-details` | ❌ |
| 4 | 0.444381 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.429618 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.403075 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.396132 | `azmcp-storage-blob-list` | ❌ |
| 8 | 0.387735 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.376271 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.374481 | `azmcp-keyvault-certificate-create` | ❌ |
| 11 | 0.352805 | `azmcp-appconfig-kv-set` | ❌ |
| 12 | 0.337708 | `azmcp-storage-datalake-directory-create` | ❌ |
| 13 | 0.333768 | `azmcp-storage-blob-container-create` | ❌ |
| 14 | 0.329895 | `azmcp-loadtesting-testresource-create` | ❌ |
| 15 | 0.327875 | `azmcp-workbooks-create` | ❌ |
| 16 | 0.325736 | `azmcp-loadtesting-test-create` | ❌ |
| 17 | 0.318516 | `azmcp-cosmos-database-container-list` | ❌ |
| 18 | 0.311829 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 19 | 0.303766 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.303151 | `azmcp-appconfig-kv-lock` | ❌ |

---

## Test 24

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** List all virtual machines in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.577373 | `azmcp-search-service-list` | ❌ |
| 2 | 0.531940 | `azmcp-subscription-list` | ❌ |
| 3 | 0.530948 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.500578 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.499251 | `azmcp-kusto-cluster-list` | ❌ |
| 6 | 0.496288 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.491307 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 8 | 0.484074 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.482576 | `azmcp-grafana-list` | ❌ |
| 10 | 0.477657 | `azmcp-aks-cluster-list` | ❌ |
| 11 | 0.473774 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.473534 | `azmcp-functionapp-list` | ❌ |
| 13 | 0.468411 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 14 | 0.454007 | `azmcp-group-list` | ❌ |
| 15 | 0.453201 | `azmcp-storage-account-list` | ❌ |
| 16 | 0.435557 | `azmcp-quota-region-availability-list` | ❌ |
| 17 | 0.430029 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.411045 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.407255 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.385092 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 25

**Expected Tool:** `azmcp-extension-az`  
**Prompt:** Show me the details of the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.632334 | `azmcp-storage-account-details` | ❌ |
| 2 | 0.565873 | `azmcp-storage-blob-container-details` | ❌ |
| 3 | 0.559925 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.513935 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.509806 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.495892 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.476297 | `azmcp-storage-account-create` | ❌ |
| 8 | 0.434946 | `azmcp-storage-blob-details` | ❌ |
| 9 | 0.433899 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.433255 | `azmcp-appconfig-kv-show` | ❌ |
| 11 | 0.417590 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.377441 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.371852 | `azmcp-sql-db-show` | ❌ |
| 14 | 0.367705 | `azmcp-aks-cluster-get` | ❌ |
| 15 | 0.360310 | `azmcp-cosmos-database-list` | ❌ |
| 16 | 0.347005 | `azmcp-loadtesting-testrun-get` | ❌ |
| 17 | 0.337651 | `azmcp-kusto-cluster-get` | ❌ |
| 18 | 0.326852 | `azmcp-keyvault-key-list` | ❌ |
| 19 | 0.325659 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.323339 | `azmcp-functionapp-list` | ❌ |

---

## Test 26

**Expected Tool:** `azmcp-acr-registry-list`  
**Prompt:** List all Azure Container Registries in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.743533 | `azmcp-acr-registry-list` | ✅ **EXPECTED** |
| 2 | 0.711580 | `azmcp-acr-registry-repository-list` | ❌ |
| 3 | 0.528620 | `azmcp-search-service-list` | ❌ |
| 4 | 0.527457 | `azmcp-aks-cluster-list` | ❌ |
| 5 | 0.525768 | `azmcp-storage-blob-container-list` | ❌ |
| 6 | 0.516076 | `azmcp-subscription-list` | ❌ |
| 7 | 0.514293 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.509386 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.503032 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.500893 | `azmcp-storage-account-list` | ❌ |
| 11 | 0.490776 | `azmcp-appconfig-account-list` | ❌ |
| 12 | 0.483500 | `azmcp-cosmos-database-container-list` | ❌ |
| 13 | 0.482499 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.482333 | `azmcp-redis-cluster-list` | ❌ |
| 15 | 0.481686 | `azmcp-redis-cache-list` | ❌ |
| 16 | 0.480869 | `azmcp-group-list` | ❌ |
| 17 | 0.473576 | `azmcp-functionapp-list` | ❌ |
| 18 | 0.469958 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.462353 | `azmcp-quota-region-availability-list` | ❌ |
| 20 | 0.460523 | `azmcp-sql-db-list` | ❌ |

---

## Test 27

**Expected Tool:** `azmcp-acr-registry-list`  
**Prompt:** Show me my Azure Container Registries  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.585968 | `azmcp-acr-registry-list` | ✅ **EXPECTED** |
| 2 | 0.563636 | `azmcp-acr-registry-repository-list` | ❌ |
| 3 | 0.457032 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.415552 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.376444 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.362031 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.359177 | `azmcp-deploy-app-logs-get` | ❌ |
| 8 | 0.356414 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.353427 | `azmcp-subscription-list` | ❌ |
| 10 | 0.349526 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.349291 | `azmcp-sql-db-list` | ❌ |
| 12 | 0.344750 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.344071 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.339252 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.338380 | `azmcp-storage-table-list` | ❌ |
| 16 | 0.336892 | `azmcp-keyvault-certificate-list` | ❌ |
| 17 | 0.334637 | `azmcp-extension-az` | ❌ |
| 18 | 0.333732 | `azmcp-monitor-workspace-list` | ❌ |
| 19 | 0.328330 | `azmcp-redis-cache-list` | ❌ |
| 20 | 0.328301 | `azmcp-quota-region-availability-list` | ❌ |

---

## Test 28

**Expected Tool:** `azmcp-acr-registry-list`  
**Prompt:** Show me the container registries in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.637158 | `azmcp-acr-registry-list` | ✅ **EXPECTED** |
| 2 | 0.563476 | `azmcp-acr-registry-repository-list` | ❌ |
| 3 | 0.474034 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.471895 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.464679 | `azmcp-storage-blob-container-list` | ❌ |
| 6 | 0.463742 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.463435 | `azmcp-search-service-list` | ❌ |
| 8 | 0.452938 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.451253 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.443939 | `azmcp-appconfig-account-list` | ❌ |
| 11 | 0.440405 | `azmcp-subscription-list` | ❌ |
| 12 | 0.435835 | `azmcp-grafana-list` | ❌ |
| 13 | 0.432469 | `azmcp-storage-account-list` | ❌ |
| 14 | 0.431745 | `azmcp-cosmos-database-container-list` | ❌ |
| 15 | 0.430867 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.430308 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.409093 | `azmcp-storage-table-list` | ❌ |
| 18 | 0.404664 | `azmcp-group-list` | ❌ |
| 19 | 0.398556 | `azmcp-quota-region-availability-list` | ❌ |
| 20 | 0.393994 | `azmcp-kusto-database-list` | ❌ |

---

## Test 29

**Expected Tool:** `azmcp-acr-registry-list`  
**Prompt:** List container registries in resource group <resource_group_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.654318 | `azmcp-acr-registry-repository-list` | ❌ |
| 2 | 0.633923 | `azmcp-acr-registry-list` | ✅ **EXPECTED** |
| 3 | 0.454929 | `azmcp-group-list` | ❌ |
| 4 | 0.454003 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.452130 | `azmcp-storage-blob-container-list` | ❌ |
| 6 | 0.446008 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.428000 | `azmcp-workbooks-list` | ❌ |
| 8 | 0.423541 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 9 | 0.411388 | `azmcp-redis-cluster-list` | ❌ |
| 10 | 0.409133 | `azmcp-sql-db-list` | ❌ |
| 11 | 0.392315 | `azmcp-storage-blob-list` | ❌ |
| 12 | 0.388706 | `azmcp-redis-cache-list` | ❌ |
| 13 | 0.372510 | `azmcp-sql-elastic-pool-list` | ❌ |
| 14 | 0.370359 | `azmcp-redis-cluster-database-list` | ❌ |
| 15 | 0.366704 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 16 | 0.356119 | `azmcp-kusto-cluster-list` | ❌ |
| 17 | 0.354145 | `azmcp-cosmos-database-list` | ❌ |
| 18 | 0.352336 | `azmcp-loadtesting-testresource-list` | ❌ |
| 19 | 0.351949 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.342481 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 30

**Expected Tool:** `azmcp-acr-registry-list`  
**Prompt:** Show me the container registries in resource group <resource_group_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.639357 | `azmcp-acr-registry-list` | ✅ **EXPECTED** |
| 2 | 0.637972 | `azmcp-acr-registry-repository-list` | ❌ |
| 3 | 0.449649 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 4 | 0.445741 | `azmcp-group-list` | ❌ |
| 5 | 0.440715 | `azmcp-storage-blob-container-list` | ❌ |
| 6 | 0.416353 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.413975 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.406554 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 9 | 0.400209 | `azmcp-workbooks-list` | ❌ |
| 10 | 0.378424 | `azmcp-redis-cluster-list` | ❌ |
| 11 | 0.373837 | `azmcp-sql-elastic-pool-list` | ❌ |
| 12 | 0.371881 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 13 | 0.368887 | `azmcp-storage-blob-list` | ❌ |
| 14 | 0.367683 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.358684 | `azmcp-monitor-workspace-list` | ❌ |
| 16 | 0.354807 | `azmcp-loadtesting-testresource-list` | ❌ |
| 17 | 0.351411 | `azmcp-cosmos-database-list` | ❌ |
| 18 | 0.344148 | `azmcp-kusto-cluster-list` | ❌ |
| 19 | 0.343572 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.341747 | `azmcp-kusto-database-list` | ❌ |

---

## Test 31

**Expected Tool:** `azmcp-acr-registry-repository-list`  
**Prompt:** List all container registry repositories in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.626482 | `azmcp-acr-registry-repository-list` | ✅ **EXPECTED** |
| 2 | 0.617537 | `azmcp-acr-registry-list` | ❌ |
| 3 | 0.510437 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.499632 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.495567 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.492604 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.480676 | `azmcp-search-service-list` | ❌ |
| 8 | 0.475629 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.461777 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.461369 | `azmcp-grafana-list` | ❌ |
| 11 | 0.461173 | `azmcp-storage-account-list` | ❌ |
| 12 | 0.456838 | `azmcp-appconfig-account-list` | ❌ |
| 13 | 0.449239 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.448228 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.439900 | `azmcp-subscription-list` | ❌ |
| 16 | 0.438219 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.434157 | `azmcp-storage-blob-list` | ❌ |
| 18 | 0.430939 | `azmcp-group-list` | ❌ |
| 19 | 0.423301 | `azmcp-storage-table-list` | ❌ |
| 20 | 0.414318 | `azmcp-kusto-database-list` | ❌ |

---

## Test 32

**Expected Tool:** `azmcp-acr-registry-repository-list`  
**Prompt:** Show me my container registry repositories  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.546315 | `azmcp-acr-registry-repository-list` | ✅ **EXPECTED** |
| 2 | 0.469279 | `azmcp-acr-registry-list` | ❌ |
| 3 | 0.453559 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.407963 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.387043 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.384158 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.308627 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.308226 | `azmcp-storage-blob-container-create` | ❌ |
| 9 | 0.302507 | `azmcp-redis-cache-list` | ❌ |
| 10 | 0.294662 | `azmcp-storage-blob-details` | ❌ |
| 11 | 0.293367 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.292562 | `azmcp-storage-account-list` | ❌ |
| 13 | 0.292080 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 14 | 0.290126 | `azmcp-redis-cluster-list` | ❌ |
| 15 | 0.283646 | `azmcp-appconfig-account-list` | ❌ |
| 16 | 0.283356 | `azmcp-kusto-database-list` | ❌ |
| 17 | 0.282569 | `azmcp-sql-db-list` | ❌ |
| 18 | 0.276445 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.273109 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.269609 | `azmcp-keyvault-secret-list` | ❌ |

---

## Test 33

**Expected Tool:** `azmcp-acr-registry-repository-list`  
**Prompt:** List repositories in the container registry <registry_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.674296 | `azmcp-acr-registry-repository-list` | ✅ **EXPECTED** |
| 2 | 0.541785 | `azmcp-acr-registry-list` | ❌ |
| 3 | 0.456225 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.433927 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.408817 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.359617 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.355320 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.351007 | `azmcp-redis-cluster-database-list` | ❌ |
| 9 | 0.347437 | `azmcp-postgres-database-list` | ❌ |
| 10 | 0.347141 | `azmcp-kusto-database-list` | ❌ |
| 11 | 0.346850 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.340933 | `azmcp-storage-blob-container-details` | ❌ |
| 13 | 0.340065 | `azmcp-redis-cluster-list` | ❌ |
| 14 | 0.338495 | `azmcp-keyvault-secret-list` | ❌ |
| 15 | 0.337543 | `azmcp-keyvault-certificate-list` | ❌ |
| 16 | 0.332856 | `azmcp-keyvault-key-list` | ❌ |
| 17 | 0.332785 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.332704 | `azmcp-sql-db-list` | ❌ |
| 19 | 0.332572 | `azmcp-monitor-workspace-list` | ❌ |
| 20 | 0.330046 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 34

**Expected Tool:** `azmcp-acr-registry-repository-list`  
**Prompt:** Show me the repositories in the container registry <registry_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.600780 | `azmcp-acr-registry-repository-list` | ✅ **EXPECTED** |
| 2 | 0.501852 | `azmcp-acr-registry-list` | ❌ |
| 3 | 0.430025 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.418623 | `azmcp-cosmos-database-container-list` | ❌ |
| 5 | 0.368174 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.341494 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.333318 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.325373 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.324160 | `azmcp-redis-cluster-list` | ❌ |
| 10 | 0.318739 | `azmcp-kusto-database-list` | ❌ |
| 11 | 0.316614 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 12 | 0.315414 | `azmcp-redis-cluster-database-list` | ❌ |
| 13 | 0.314960 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.311692 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.306052 | `azmcp-sql-db-list` | ❌ |
| 16 | 0.304725 | `azmcp-keyvault-certificate-list` | ❌ |
| 17 | 0.303931 | `azmcp-kusto-cluster-list` | ❌ |
| 18 | 0.300101 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.299629 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.295411 | `azmcp-postgres-database-list` | ❌ |

---

## Test 35

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** List all cosmosdb accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.818357 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.668480 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.615268 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.606794 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.588682 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.587866 | `azmcp-subscription-list` | ❌ |
| 7 | 0.557870 | `azmcp-search-service-list` | ❌ |
| 8 | 0.530755 | `azmcp-storage-blob-container-list` | ❌ |
| 9 | 0.528963 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.516914 | `azmcp-kusto-cluster-list` | ❌ |
| 11 | 0.514581 | `azmcp-functionapp-list` | ❌ |
| 12 | 0.502382 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.502346 | `azmcp-redis-cluster-list` | ❌ |
| 14 | 0.499086 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.497679 | `azmcp-appconfig-account-list` | ❌ |
| 16 | 0.486978 | `azmcp-group-list` | ❌ |
| 17 | 0.483046 | `azmcp-grafana-list` | ❌ |
| 18 | 0.474934 | `azmcp-postgres-server-list` | ❌ |
| 19 | 0.473625 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.472743 | `azmcp-storage-blob-list` | ❌ |

---

## Test 36

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** Show me my cosmosdb accounts  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.665401 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.605336 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.571592 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.473500 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.467599 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.443487 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.443362 | `azmcp-storage-account-details` | ❌ |
| 8 | 0.436329 | `azmcp-subscription-list` | ❌ |
| 9 | 0.431430 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.407798 | `azmcp-storage-blob-list` | ❌ |
| 11 | 0.390183 | `azmcp-kusto-database-list` | ❌ |
| 12 | 0.386011 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.383922 | `azmcp-appconfig-account-list` | ❌ |
| 14 | 0.381261 | `azmcp-sql-db-list` | ❌ |
| 15 | 0.379449 | `azmcp-appconfig-kv-show` | ❌ |
| 16 | 0.373785 | `azmcp-redis-cluster-list` | ❌ |
| 17 | 0.367844 | `azmcp-quota-usage-check` | ❌ |
| 18 | 0.358314 | `azmcp-keyvault-key-list` | ❌ |
| 19 | 0.355807 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.353879 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 37

**Expected Tool:** `azmcp-cosmos-account-list`  
**Prompt:** Show me the cosmosdb accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.752494 | `azmcp-cosmos-account-list` | ✅ **EXPECTED** |
| 2 | 0.605125 | `azmcp-cosmos-database-list` | ❌ |
| 3 | 0.566249 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.548156 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.546402 | `azmcp-subscription-list` | ❌ |
| 6 | 0.535227 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.513709 | `azmcp-search-service-list` | ❌ |
| 8 | 0.488006 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.483799 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.466591 | `azmcp-redis-cluster-list` | ❌ |
| 11 | 0.462156 | `azmcp-functionapp-list` | ❌ |
| 12 | 0.457584 | `azmcp-appconfig-account-list` | ❌ |
| 13 | 0.456185 | `azmcp-redis-cache-list` | ❌ |
| 14 | 0.455017 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.453588 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.443558 | `azmcp-storage-account-details` | ❌ |
| 17 | 0.441136 | `azmcp-grafana-list` | ❌ |
| 18 | 0.438277 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 19 | 0.433094 | `azmcp-postgres-server-list` | ❌ |
| 20 | 0.430336 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 38

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
| 12 | 0.351294 | `azmcp-kusto-table-list` | ❌ |
| 13 | 0.340982 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.335256 | `azmcp-sql-db-list` | ❌ |
| 15 | 0.334399 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.334381 | `azmcp-storage-blob-details` | ❌ |
| 17 | 0.331041 | `azmcp-kusto-sample` | ❌ |
| 18 | 0.326439 | `azmcp-monitor-resource-log-query` | ❌ |
| 19 | 0.308694 | `azmcp-acr-registry-repository-list` | ❌ |
| 20 | 0.302962 | `azmcp-appconfig-kv-show` | ❌ |

---

## Test 39

**Expected Tool:** `azmcp-cosmos-database-container-list`  
**Prompt:** List all the containers in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.852832 | `azmcp-cosmos-database-container-list` | ✅ **EXPECTED** |
| 2 | 0.690158 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.681044 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.630659 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.561245 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.535260 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.527459 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 8 | 0.473516 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.448992 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.441485 | `azmcp-storage-account-list` | ❌ |
| 11 | 0.439770 | `azmcp-sql-db-list` | ❌ |
| 12 | 0.427555 | `azmcp-kusto-table-list` | ❌ |
| 13 | 0.424294 | `azmcp-redis-cluster-database-list` | ❌ |
| 14 | 0.421534 | `azmcp-acr-registry-repository-list` | ❌ |
| 15 | 0.411663 | `azmcp-monitor-table-list` | ❌ |
| 16 | 0.405887 | `azmcp-storage-account-details` | ❌ |
| 17 | 0.392929 | `azmcp-postgres-database-list` | ❌ |
| 18 | 0.378191 | `azmcp-keyvault-certificate-list` | ❌ |
| 19 | 0.372115 | `azmcp-kusto-cluster-list` | ❌ |
| 20 | 0.368473 | `azmcp-keyvault-key-list` | ❌ |

---

## Test 40

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
| 9 | 0.398104 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.397755 | `azmcp-sql-db-list` | ❌ |
| 11 | 0.395453 | `azmcp-kusto-table-list` | ❌ |
| 12 | 0.394078 | `azmcp-storage-account-details` | ❌ |
| 13 | 0.386806 | `azmcp-redis-cluster-database-list` | ❌ |
| 14 | 0.372499 | `azmcp-storage-blob-details` | ❌ |
| 15 | 0.362485 | `azmcp-storage-account-list` | ❌ |
| 16 | 0.355640 | `azmcp-acr-registry-repository-list` | ❌ |
| 17 | 0.345665 | `azmcp-sql-db-show` | ❌ |
| 18 | 0.319603 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.318540 | `azmcp-appconfig-kv-show` | ❌ |
| 20 | 0.314952 | `azmcp-kusto-table-schema` | ❌ |

---

## Test 41

**Expected Tool:** `azmcp-cosmos-database-list`  
**Prompt:** List all the databases in the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.815683 | `azmcp-cosmos-database-list` | ✅ **EXPECTED** |
| 2 | 0.668515 | `azmcp-cosmos-account-list` | ❌ |
| 3 | 0.665298 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.571395 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.555134 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.548066 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.526046 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.501477 | `azmcp-postgres-database-list` | ❌ |
| 9 | 0.500364 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.471358 | `azmcp-kusto-table-list` | ❌ |
| 11 | 0.459194 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 12 | 0.456262 | `azmcp-storage-account-list` | ❌ |
| 13 | 0.450854 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.439548 | `azmcp-storage-blob-list` | ❌ |
| 15 | 0.405825 | `azmcp-keyvault-key-list` | ❌ |
| 16 | 0.401781 | `azmcp-subscription-list` | ❌ |
| 17 | 0.397642 | `azmcp-keyvault-certificate-list` | ❌ |
| 18 | 0.396808 | `azmcp-search-index-list` | ❌ |
| 19 | 0.389204 | `azmcp-keyvault-secret-list` | ❌ |
| 20 | 0.387534 | `azmcp-acr-registry-repository-list` | ❌ |

---

## Test 42

**Expected Tool:** `azmcp-cosmos-database-list`  
**Prompt:** Show me the databases in the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.749370 | `azmcp-cosmos-database-list` | ✅ **EXPECTED** |
| 2 | 0.624759 | `azmcp-cosmos-database-container-list` | ❌ |
| 3 | 0.614572 | `azmcp-cosmos-account-list` | ❌ |
| 4 | 0.524916 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.505363 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.498206 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.497414 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.456313 | `azmcp-storage-blob-container-list` | ❌ |
| 9 | 0.449759 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.447875 | `azmcp-postgres-database-list` | ❌ |
| 11 | 0.437915 | `azmcp-kusto-table-list` | ❌ |
| 12 | 0.400098 | `azmcp-storage-account-list` | ❌ |
| 13 | 0.396280 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.384707 | `azmcp-storage-blob-list` | ❌ |
| 15 | 0.377400 | `azmcp-storage-account-details` | ❌ |
| 16 | 0.361429 | `azmcp-monitor-table-type-list` | ❌ |
| 17 | 0.344442 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.342424 | `azmcp-acr-registry-repository-list` | ❌ |
| 19 | 0.339516 | `azmcp-kusto-cluster-list` | ❌ |
| 20 | 0.335852 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 43

**Expected Tool:** `azmcp-kusto-cluster-get`  
**Prompt:** Show me the details of the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.482059 | `azmcp-kusto-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.464740 | `azmcp-aks-cluster-get` | ❌ |
| 3 | 0.457614 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.416762 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.364174 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.362958 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.344871 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.344639 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.332639 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.326500 | `azmcp-redis-cache-list` | ❌ |
| 11 | 0.318754 | `azmcp-kusto-query` | ❌ |
| 12 | 0.314694 | `azmcp-kusto-table-schema` | ❌ |
| 13 | 0.304033 | `azmcp-storage-blob-container-details` | ❌ |
| 14 | 0.301024 | `azmcp-grafana-list` | ❌ |
| 15 | 0.299931 | `azmcp-kusto-table-list` | ❌ |
| 16 | 0.289673 | `azmcp-storage-account-details` | ❌ |
| 17 | 0.288707 | `azmcp-quota-usage-check` | ❌ |
| 18 | 0.284522 | `azmcp-servicebus-queue-details` | ❌ |
| 19 | 0.269678 | `azmcp-sql-db-list` | ❌ |
| 20 | 0.249991 | `azmcp-storage-blob-details` | ❌ |

---

## Test 44

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** List all Data Explorer clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651218 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.644031 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.548968 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.536049 | `azmcp-aks-cluster-list` | ❌ |
| 5 | 0.509396 | `azmcp-grafana-list` | ❌ |
| 6 | 0.505966 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.492107 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.487882 | `azmcp-search-service-list` | ❌ |
| 9 | 0.487583 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.486395 | `azmcp-kusto-cluster-get` | ❌ |
| 11 | 0.460255 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.458754 | `azmcp-redis-cluster-database-list` | ❌ |
| 13 | 0.451385 | `azmcp-kusto-table-list` | ❌ |
| 14 | 0.428236 | `azmcp-storage-table-list` | ❌ |
| 15 | 0.427976 | `azmcp-subscription-list` | ❌ |
| 16 | 0.411791 | `azmcp-group-list` | ❌ |
| 17 | 0.407832 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.404929 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.402530 | `azmcp-storage-account-list` | ❌ |
| 20 | 0.395458 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 45

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** Show me my Data Explorer clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.437321 | `azmcp-redis-cluster-list` | ❌ |
| 2 | 0.391087 | `azmcp-redis-cluster-database-list` | ❌ |
| 3 | 0.386126 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 4 | 0.359555 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.341628 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.338217 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.314792 | `azmcp-aks-cluster-get` | ❌ |
| 8 | 0.303083 | `azmcp-grafana-list` | ❌ |
| 9 | 0.292903 | `azmcp-redis-cache-list` | ❌ |
| 10 | 0.287768 | `azmcp-kusto-sample` | ❌ |
| 11 | 0.285603 | `azmcp-kusto-query` | ❌ |
| 12 | 0.283296 | `azmcp-kusto-table-list` | ❌ |
| 13 | 0.270931 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.264112 | `azmcp-monitor-table-type-list` | ❌ |
| 15 | 0.264035 | `azmcp-monitor-workspace-list` | ❌ |
| 16 | 0.263226 | `azmcp-quota-usage-check` | ❌ |
| 17 | 0.261960 | `azmcp-postgres-server-list` | ❌ |
| 18 | 0.260320 | `azmcp-deploy-app-logs-get` | ❌ |
| 19 | 0.255960 | `azmcp-postgres-database-list` | ❌ |
| 20 | 0.240130 | `azmcp-redis-cache-accesspolicy-list` | ❌ |

---

## Test 46

**Expected Tool:** `azmcp-kusto-cluster-list`  
**Prompt:** Show me the Data Explorer clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584063 | `azmcp-redis-cluster-list` | ❌ |
| 2 | 0.549797 | `azmcp-kusto-cluster-list` | ✅ **EXPECTED** |
| 3 | 0.471120 | `azmcp-aks-cluster-list` | ❌ |
| 4 | 0.469663 | `azmcp-kusto-cluster-get` | ❌ |
| 5 | 0.464179 | `azmcp-kusto-database-list` | ❌ |
| 6 | 0.462945 | `azmcp-grafana-list` | ❌ |
| 7 | 0.446210 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.440326 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.432048 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.421307 | `azmcp-search-service-list` | ❌ |
| 11 | 0.396253 | `azmcp-redis-cluster-database-list` | ❌ |
| 12 | 0.392421 | `azmcp-kusto-table-list` | ❌ |
| 13 | 0.386776 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.377490 | `azmcp-kusto-query` | ❌ |
| 15 | 0.371308 | `azmcp-subscription-list` | ❌ |
| 16 | 0.368890 | `azmcp-quota-usage-check` | ❌ |
| 17 | 0.366262 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.365972 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.365323 | `azmcp-quota-region-availability-list` | ❌ |
| 20 | 0.347944 | `azmcp-aks-cluster-get` | ❌ |

---

## Test 47

**Expected Tool:** `azmcp-kusto-database-list`  
**Prompt:** List all databases in the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628129 | `azmcp-redis-cluster-database-list` | ❌ |
| 2 | 0.610729 | `azmcp-kusto-database-list` | ✅ **EXPECTED** |
| 3 | 0.553218 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.549673 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.474251 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.461496 | `azmcp-sql-db-list` | ❌ |
| 7 | 0.459182 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.434330 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.431669 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.404095 | `azmcp-monitor-table-list` | ❌ |
| 11 | 0.396060 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.379966 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.375535 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.363663 | `azmcp-postgres-server-list` | ❌ |
| 15 | 0.357739 | `azmcp-monitor-table-type-list` | ❌ |
| 16 | 0.350214 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.340757 | `azmcp-redis-cache-list` | ❌ |
| 18 | 0.334270 | `azmcp-grafana-list` | ❌ |
| 19 | 0.320622 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.318850 | `azmcp-kusto-query` | ❌ |

---

## Test 48

**Expected Tool:** `azmcp-kusto-database-list`  
**Prompt:** Show me the databases in the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.597975 | `azmcp-redis-cluster-database-list` | ❌ |
| 2 | 0.558599 | `azmcp-kusto-database-list` | ✅ **EXPECTED** |
| 3 | 0.497144 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.486732 | `azmcp-postgres-database-list` | ❌ |
| 5 | 0.439954 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.427267 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.422588 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.383664 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.368013 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.362905 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.359378 | `azmcp-monitor-table-list` | ❌ |
| 12 | 0.338777 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.336400 | `azmcp-monitor-table-type-list` | ❌ |
| 14 | 0.336104 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.334851 | `azmcp-kusto-table-schema` | ❌ |
| 16 | 0.330351 | `azmcp-postgres-server-list` | ❌ |
| 17 | 0.313246 | `azmcp-redis-cache-list` | ❌ |
| 18 | 0.310919 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.309809 | `azmcp-kusto-sample` | ❌ |
| 20 | 0.305756 | `azmcp-kusto-query` | ❌ |

---

## Test 49

**Expected Tool:** `azmcp-kusto-query`  
**Prompt:** Show me all items that contain the word <search_term> in the Data Explorer table <table_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.381338 | `azmcp-kusto-query` | ✅ **EXPECTED** |
| 2 | 0.363256 | `azmcp-kusto-sample` | ❌ |
| 3 | 0.349153 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.345777 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.334695 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.319122 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.318989 | `azmcp-kusto-table-schema` | ❌ |
| 8 | 0.314978 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.308146 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.303975 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 11 | 0.302898 | `azmcp-postgres-table-list` | ❌ |
| 12 | 0.300424 | `azmcp-search-service-list` | ❌ |
| 13 | 0.300420 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.295095 | `azmcp-search-index-list` | ❌ |
| 15 | 0.292076 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.279875 | `azmcp-monitor-resource-log-query` | ❌ |
| 17 | 0.264026 | `azmcp-grafana-list` | ❌ |
| 18 | 0.263049 | `azmcp-kusto-cluster-get` | ❌ |
| 19 | 0.257739 | `azmcp-postgres-database-list` | ❌ |
| 20 | 0.257488 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 50

**Expected Tool:** `azmcp-kusto-sample`  
**Prompt:** Show me a data sample from the Data Explorer table <table_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.537154 | `azmcp-kusto-sample` | ✅ **EXPECTED** |
| 2 | 0.419533 | `azmcp-kusto-table-schema` | ❌ |
| 3 | 0.391361 | `azmcp-kusto-table-list` | ❌ |
| 4 | 0.377056 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.364611 | `azmcp-postgres-table-schema-get` | ❌ |
| 6 | 0.361811 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.343671 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.341674 | `azmcp-monitor-table-list` | ❌ |
| 9 | 0.337323 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.329933 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.319239 | `azmcp-kusto-query` | ❌ |
| 12 | 0.318189 | `azmcp-postgres-table-list` | ❌ |
| 13 | 0.310201 | `azmcp-kusto-cluster-get` | ❌ |
| 14 | 0.285941 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.267790 | `azmcp-aks-cluster-get` | ❌ |
| 16 | 0.257683 | `azmcp-monitor-resource-log-query` | ❌ |
| 17 | 0.254555 | `azmcp-postgres-database-list` | ❌ |
| 18 | 0.249424 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.243553 | `azmcp-postgres-server-list` | ❌ |
| 20 | 0.240744 | `azmcp-grafana-list` | ❌ |

---

## Test 51

**Expected Tool:** `azmcp-kusto-table-list`  
**Prompt:** List all tables in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591525 | `azmcp-kusto-table-list` | ✅ **EXPECTED** |
| 2 | 0.585237 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.550007 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.521590 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.520802 | `azmcp-redis-cluster-database-list` | ❌ |
| 6 | 0.517077 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.475496 | `azmcp-postgres-database-list` | ❌ |
| 8 | 0.464341 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.449704 | `azmcp-kusto-table-schema` | ❌ |
| 10 | 0.436518 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.429246 | `azmcp-redis-cluster-list` | ❌ |
| 12 | 0.412275 | `azmcp-kusto-sample` | ❌ |
| 13 | 0.410425 | `azmcp-kusto-cluster-list` | ❌ |
| 14 | 0.384895 | `azmcp-postgres-table-schema-get` | ❌ |
| 15 | 0.380671 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.361884 | `azmcp-sql-db-list` | ❌ |
| 17 | 0.349204 | `azmcp-postgres-server-list` | ❌ |
| 18 | 0.337427 | `azmcp-kusto-query` | ❌ |
| 19 | 0.330068 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.329669 | `azmcp-grafana-list` | ❌ |

---

## Test 52

**Expected Tool:** `azmcp-kusto-table-list`  
**Prompt:** Show me the tables in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.549764 | `azmcp-kusto-table-list` | ✅ **EXPECTED** |
| 2 | 0.523432 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.494108 | `azmcp-redis-cluster-database-list` | ❌ |
| 4 | 0.490717 | `azmcp-monitor-table-list` | ❌ |
| 5 | 0.475510 | `azmcp-kusto-database-list` | ❌ |
| 6 | 0.466302 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.466282 | `azmcp-kusto-table-schema` | ❌ |
| 8 | 0.431964 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.425623 | `azmcp-kusto-sample` | ❌ |
| 10 | 0.421413 | `azmcp-postgres-database-list` | ❌ |
| 11 | 0.403420 | `azmcp-redis-cluster-list` | ❌ |
| 12 | 0.402646 | `azmcp-postgres-table-schema-get` | ❌ |
| 13 | 0.391081 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.367187 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.348891 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.335264 | `azmcp-sql-db-list` | ❌ |
| 17 | 0.330383 | `azmcp-kusto-query` | ❌ |
| 18 | 0.326690 | `azmcp-postgres-server-list` | ❌ |
| 19 | 0.314691 | `azmcp-kusto-cluster-get` | ❌ |
| 20 | 0.300285 | `azmcp-aks-cluster-list` | ❌ |

---

## Test 53

**Expected Tool:** `azmcp-kusto-table-schema`  
**Prompt:** Show me the schema for table <table_name> in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.588179 | `azmcp-kusto-table-schema` | ✅ **EXPECTED** |
| 2 | 0.564311 | `azmcp-postgres-table-schema-get` | ❌ |
| 3 | 0.437392 | `azmcp-kusto-table-list` | ❌ |
| 4 | 0.432585 | `azmcp-kusto-sample` | ❌ |
| 5 | 0.413686 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.398632 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.387660 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.366346 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.366175 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.357533 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.345228 | `azmcp-redis-cluster-list` | ❌ |
| 12 | 0.314616 | `azmcp-kusto-cluster-get` | ❌ |
| 13 | 0.309145 | `azmcp-postgres-database-list` | ❌ |
| 14 | 0.308550 | `azmcp-sql-db-show` | ❌ |
| 15 | 0.298243 | `azmcp-kusto-query` | ❌ |
| 16 | 0.294840 | `azmcp-cosmos-database-list` | ❌ |
| 17 | 0.282712 | `azmcp-kusto-cluster-list` | ❌ |
| 18 | 0.275795 | `azmcp-cosmos-database-container-list` | ❌ |
| 19 | 0.274090 | `azmcp-aks-cluster-get` | ❌ |
| 20 | 0.273625 | `azmcp-sql-db-list` | ❌ |

---

## Test 54

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
| 12 | 0.407935 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.319808 | `azmcp-kusto-table-list` | ❌ |
| 14 | 0.293787 | `azmcp-cosmos-database-container-list` | ❌ |
| 15 | 0.292441 | `azmcp-cosmos-account-list` | ❌ |
| 16 | 0.289334 | `azmcp-grafana-list` | ❌ |
| 17 | 0.252438 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.249563 | `azmcp-kusto-cluster-list` | ❌ |
| 19 | 0.245546 | `azmcp-acr-registry-repository-list` | ❌ |
| 20 | 0.245456 | `azmcp-group-list` | ❌ |

---

## Test 55

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
| 12 | 0.366058 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.281406 | `azmcp-kusto-table-list` | ❌ |
| 14 | 0.261442 | `azmcp-cosmos-database-container-list` | ❌ |
| 15 | 0.257971 | `azmcp-grafana-list` | ❌ |
| 16 | 0.247726 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.235347 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.227995 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.222501 | `azmcp-kusto-table-schema` | ❌ |
| 20 | 0.212647 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 56

**Expected Tool:** `azmcp-postgres-database-query`  
**Prompt:** Show me all items that contain the word <search_term> in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.546227 | `azmcp-postgres-database-list` | ❌ |
| 2 | 0.503300 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.466591 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.415788 | `azmcp-postgres-database-query` | ✅ **EXPECTED** |
| 5 | 0.403981 | `azmcp-postgres-server-param-get` | ❌ |
| 6 | 0.403933 | `azmcp-postgres-server-config-get` | ❌ |
| 7 | 0.380488 | `azmcp-postgres-table-schema-get` | ❌ |
| 8 | 0.354350 | `azmcp-postgres-server-param-set` | ❌ |
| 9 | 0.301804 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.277634 | `azmcp-sql-db-show` | ❌ |
| 11 | 0.264874 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 12 | 0.262312 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.262119 | `azmcp-kusto-query` | ❌ |
| 14 | 0.254096 | `azmcp-kusto-table-list` | ❌ |
| 15 | 0.248603 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.244312 | `azmcp-kusto-database-list` | ❌ |
| 17 | 0.236328 | `azmcp-grafana-list` | ❌ |
| 18 | 0.218730 | `azmcp-kusto-table-schema` | ❌ |
| 19 | 0.217845 | `azmcp-kusto-sample` | ❌ |
| 20 | 0.188965 | `azmcp-foundry-models-list` | ❌ |

---

## Test 57

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
| 16 | 0.175084 | `azmcp-aks-cluster-get` | ❌ |
| 17 | 0.168327 | `azmcp-kusto-table-schema` | ❌ |
| 18 | 0.160792 | `azmcp-grafana-list` | ❌ |
| 19 | 0.156710 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.153895 | `azmcp-appconfig-kv-unlock` | ❌ |

---

## Test 58

**Expected Tool:** `azmcp-postgres-server-list`  
**Prompt:** List all PostgreSQL servers in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.900068 | `azmcp-postgres-server-list` | ✅ **EXPECTED** |
| 2 | 0.640647 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.565870 | `azmcp-postgres-table-list` | ❌ |
| 4 | 0.538897 | `azmcp-postgres-server-config-get` | ❌ |
| 5 | 0.507501 | `azmcp-postgres-server-param-get` | ❌ |
| 6 | 0.483832 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.472522 | `azmcp-grafana-list` | ❌ |
| 8 | 0.453973 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.446690 | `azmcp-redis-cache-list` | ❌ |
| 10 | 0.430611 | `azmcp-search-service-list` | ❌ |
| 11 | 0.406989 | `azmcp-monitor-workspace-list` | ❌ |
| 12 | 0.406729 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.401201 | `azmcp-sql-db-list` | ❌ |
| 14 | 0.399162 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.397400 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.389283 | `azmcp-appconfig-account-list` | ❌ |
| 17 | 0.373731 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.366122 | `azmcp-group-list` | ❌ |
| 19 | 0.352005 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.347083 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 59

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
| 10 | 0.305360 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 11 | 0.274763 | `azmcp-grafana-list` | ❌ |
| 12 | 0.260533 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.253274 | `azmcp-kusto-database-list` | ❌ |
| 14 | 0.245276 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.241835 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.239500 | `azmcp-appconfig-account-list` | ❌ |
| 17 | 0.229741 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.227547 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.219274 | `azmcp-cosmos-database-container-list` | ❌ |
| 20 | 0.218726 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 60

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
| 6 | 0.452670 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.444127 | `azmcp-grafana-list` | ❌ |
| 8 | 0.414788 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.411590 | `azmcp-search-service-list` | ❌ |
| 10 | 0.410719 | `azmcp-postgres-database-query` | ❌ |
| 11 | 0.403538 | `azmcp-kusto-cluster-list` | ❌ |
| 12 | 0.399866 | `azmcp-postgres-table-schema-get` | ❌ |
| 13 | 0.376954 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.362557 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.362513 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.360521 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.358358 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.334101 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.325681 | `azmcp-group-list` | ❌ |
| 20 | 0.311827 | `azmcp-marketplace-product-get` | ❌ |

---

## Test 61

**Expected Tool:** `azmcp-postgres-server-param`  
**Prompt:** Show me if the parameter my PostgreSQL server <server> has replication enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.594753 | `azmcp-postgres-server-param-get` | ❌ |
| 2 | 0.539671 | `azmcp-postgres-server-config-get` | ❌ |
| 3 | 0.489693 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.480826 | `azmcp-postgres-server-param-set` | ❌ |
| 5 | 0.451871 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.357606 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.330875 | `azmcp-postgres-table-schema-get` | ❌ |
| 8 | 0.305351 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.227987 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.207560 | `azmcp-sql-db-list` | ❌ |
| 11 | 0.185273 | `azmcp-appconfig-kv-list` | ❌ |
| 12 | 0.174107 | `azmcp-grafana-list` | ❌ |
| 13 | 0.169190 | `azmcp-appconfig-account-list` | ❌ |
| 14 | 0.158090 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.155785 | `azmcp-appconfig-kv-show` | ❌ |
| 16 | 0.145346 | `azmcp-loadtesting-testrun-list` | ❌ |
| 17 | 0.145022 | `azmcp-kusto-database-list` | ❌ |
| 18 | 0.142387 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.140139 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.138625 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 62

**Expected Tool:** `azmcp-postgres-server-param-set`  
**Prompt:** Enable replication for my PostgreSQL server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.488189 | `azmcp-postgres-server-config-get` | ❌ |
| 2 | 0.470826 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.466350 | `azmcp-postgres-server-param-set` | ✅ **EXPECTED** |
| 4 | 0.447055 | `azmcp-postgres-server-param-get` | ❌ |
| 5 | 0.441897 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.354838 | `azmcp-postgres-table-list` | ❌ |
| 7 | 0.343021 | `azmcp-postgres-database-query` | ❌ |
| 8 | 0.318202 | `azmcp-postgres-table-schema-get` | ❌ |
| 9 | 0.186162 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.176461 | `azmcp-sql-db-list` | ❌ |
| 11 | 0.132947 | `azmcp-kusto-sample` | ❌ |
| 12 | 0.128280 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.124818 | `azmcp-kusto-table-schema` | ❌ |
| 14 | 0.118053 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.117434 | `azmcp-kusto-cluster-get` | ❌ |
| 16 | 0.114191 | `azmcp-grafana-list` | ❌ |
| 17 | 0.112917 | `azmcp-deploy-plan-get` | ❌ |
| 18 | 0.110007 | `azmcp-kusto-table-list` | ❌ |
| 19 | 0.103362 | `azmcp-appconfig-kv-unlock` | ❌ |
| 20 | 0.102853 | `azmcp-extension-azqr` | ❌ |

---

## Test 63

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
| 7 | 0.432682 | `azmcp-kusto-table-list` | ❌ |
| 8 | 0.430171 | `azmcp-postgres-server-param-get` | ❌ |
| 9 | 0.394396 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.386992 | `azmcp-redis-cluster-database-list` | ❌ |
| 11 | 0.380821 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.373673 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.352281 | `azmcp-kusto-database-list` | ❌ |
| 14 | 0.308195 | `azmcp-kusto-table-schema` | ❌ |
| 15 | 0.299785 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.257808 | `azmcp-grafana-list` | ❌ |
| 17 | 0.256245 | `azmcp-kusto-sample` | ❌ |
| 18 | 0.249162 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.236931 | `azmcp-appconfig-kv-list` | ❌ |
| 20 | 0.229889 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 64

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
| 8 | 0.390292 | `azmcp-kusto-table-list` | ❌ |
| 9 | 0.371151 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.371036 | `azmcp-postgres-server-param-set` | ❌ |
| 11 | 0.360749 | `azmcp-monitor-table-list` | ❌ |
| 12 | 0.334871 | `azmcp-kusto-table-schema` | ❌ |
| 13 | 0.315781 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.307353 | `azmcp-kusto-database-list` | ❌ |
| 15 | 0.272906 | `azmcp-kusto-sample` | ❌ |
| 16 | 0.266178 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.243542 | `azmcp-grafana-list` | ❌ |
| 18 | 0.207521 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.205697 | `azmcp-appconfig-kv-list` | ❌ |
| 20 | 0.202950 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 65

**Expected Tool:** `azmcp-postgres-table-schema-get`  
**Prompt:** Show me the schema of table <table> in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.714877 | `azmcp-postgres-table-schema-get` | ✅ **EXPECTED** |
| 2 | 0.597846 | `azmcp-postgres-table-list` | ❌ |
| 3 | 0.574230 | `azmcp-postgres-database-list` | ❌ |
| 4 | 0.508082 | `azmcp-postgres-server-config-get` | ❌ |
| 5 | 0.475642 | `azmcp-kusto-table-schema` | ❌ |
| 6 | 0.443816 | `azmcp-postgres-server-param-get` | ❌ |
| 7 | 0.442553 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.427530 | `azmcp-postgres-database-query` | ❌ |
| 9 | 0.362687 | `azmcp-postgres-server-param-set` | ❌ |
| 10 | 0.336037 | `azmcp-sql-db-show` | ❌ |
| 11 | 0.322720 | `azmcp-kusto-table-list` | ❌ |
| 12 | 0.312465 | `azmcp-monitor-table-list` | ❌ |
| 13 | 0.303748 | `azmcp-kusto-sample` | ❌ |
| 14 | 0.253353 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.239328 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.212206 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.201673 | `azmcp-grafana-list` | ❌ |
| 18 | 0.185124 | `azmcp-appconfig-kv-list` | ❌ |
| 19 | 0.183782 | `azmcp-bicepschema-get` | ❌ |
| 20 | 0.167077 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 66

**Expected Tool:** `azmcp-extension-azd`  
**Prompt:** Create a To-Do list web application that uses NodeJS and MongoDB  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.241366 | `azmcp-extension-az` | ❌ |
| 2 | 0.203706 | `azmcp-deploy-iac-rules-get` | ❌ |
| 3 | 0.200024 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 4 | 0.196585 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 5 | 0.190019 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 6 | 0.187620 | `azmcp-deploy-plan-get` | ❌ |
| 7 | 0.185433 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 8 | 0.181543 | `azmcp-redis-cluster-database-list` | ❌ |
| 9 | 0.177946 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.173269 | `azmcp-extension-azd` | ✅ **EXPECTED** |
| 11 | 0.165761 | `azmcp-postgres-table-list` | ❌ |
| 12 | 0.159142 | `azmcp-deploy-app-logs-get` | ❌ |
| 13 | 0.148122 | `azmcp-postgres-database-list` | ❌ |
| 14 | 0.145985 | `azmcp-storage-share-file-list` | ❌ |
| 15 | 0.145058 | `azmcp-redis-cluster-list` | ❌ |
| 16 | 0.138516 | `azmcp-storage-blob-container-create` | ❌ |
| 17 | 0.137936 | `azmcp-postgres-database-query` | ❌ |
| 18 | 0.129433 | `azmcp-sql-db-list` | ❌ |
| 19 | 0.126407 | `azmcp-storage-blob-list` | ❌ |
| 20 | 0.126141 | `azmcp-postgres-server-list` | ❌ |

---

## Test 67

**Expected Tool:** `azmcp-extension-azd`  
**Prompt:** Deploy my web application to Azure App Service  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.544719 | `azmcp-deploy-plan-get` | ❌ |
| 2 | 0.489853 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 3 | 0.441305 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 4 | 0.437357 | `azmcp-foundry-models-deploy` | ❌ |
| 5 | 0.421067 | `azmcp-deploy-app-logs-get` | ❌ |
| 6 | 0.394023 | `azmcp-deploy-iac-rules-get` | ❌ |
| 7 | 0.364145 | `azmcp-extension-azd` | ✅ **EXPECTED** |
| 8 | 0.361106 | `azmcp-foundry-models-deployments-list` | ❌ |
| 9 | 0.356426 | `azmcp-extension-az` | ❌ |
| 10 | 0.345215 | `azmcp-functionapp-list` | ❌ |
| 11 | 0.308550 | `azmcp-storage-blob-upload` | ❌ |
| 12 | 0.299738 | `azmcp-search-index-list` | ❌ |
| 13 | 0.297374 | `azmcp-workbooks-delete` | ❌ |
| 14 | 0.275067 | `azmcp-quota-usage-check` | ❌ |
| 15 | 0.273452 | `azmcp-search-service-list` | ❌ |
| 16 | 0.250828 | `azmcp-storage-queue-message-send` | ❌ |
| 17 | 0.249133 | `azmcp-storage-account-details` | ❌ |
| 18 | 0.244902 | `azmcp-sql-db-list` | ❌ |
| 19 | 0.239136 | `azmcp-storage-account-create` | ❌ |
| 20 | 0.237105 | `azmcp-resourcehealth-availability-status-get` | ❌ |

---

## Test 68

**Expected Tool:** `azmcp-deploy-app-logs-get`  
**Prompt:** Show me the log of the application deployed by azd  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.686701 | `azmcp-deploy-app-logs-get` | ✅ **EXPECTED** |
| 2 | 0.486709 | `azmcp-extension-azd` | ❌ |
| 3 | 0.471692 | `azmcp-deploy-plan-get` | ❌ |
| 4 | 0.404890 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 5 | 0.392565 | `azmcp-deploy-iac-rules-get` | ❌ |
| 6 | 0.389603 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 7 | 0.341175 | `azmcp-monitor-resource-log-query` | ❌ |
| 8 | 0.340723 | `azmcp-extension-az` | ❌ |
| 9 | 0.334992 | `azmcp-quota-usage-check` | ❌ |
| 10 | 0.328168 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 11 | 0.327028 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 12 | 0.325553 | `azmcp-extension-azqr` | ❌ |
| 13 | 0.307291 | `azmcp-sql-db-show` | ❌ |
| 14 | 0.299854 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 15 | 0.288973 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 16 | 0.284418 | `azmcp-sql-db-list` | ❌ |
| 17 | 0.281706 | `azmcp-monitor-workspace-list` | ❌ |
| 18 | 0.275999 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 19 | 0.275276 | `azmcp-search-index-list` | ❌ |
| 20 | 0.270537 | `azmcp-storage-account-details` | ❌ |

---

## Test 69

**Expected Tool:** `azmcp-deploy-architecture-diagram-generate`  
**Prompt:** Generate the azure architecture diagram for this application  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.680640 | `azmcp-deploy-architecture-diagram-generate` | ✅ **EXPECTED** |
| 2 | 0.562521 | `azmcp-deploy-plan-get` | ❌ |
| 3 | 0.497193 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 4 | 0.435921 | `azmcp-deploy-iac-rules-get` | ❌ |
| 5 | 0.417940 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 6 | 0.393602 | `azmcp-bestpractices-get` | ❌ |
| 7 | 0.371127 | `azmcp-deploy-app-logs-get` | ❌ |
| 8 | 0.343117 | `azmcp-quota-usage-check` | ❌ |
| 9 | 0.323166 | `azmcp-extension-az` | ❌ |
| 10 | 0.322230 | `azmcp-extension-azqr` | ❌ |
| 11 | 0.321444 | `azmcp-extension-azd` | ❌ |
| 12 | 0.263521 | `azmcp-quota-region-availability-list` | ❌ |
| 13 | 0.263337 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 14 | 0.245177 | `azmcp-subscription-list` | ❌ |
| 15 | 0.242778 | `azmcp-storage-account-details` | ❌ |
| 16 | 0.239647 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 17 | 0.235433 | `azmcp-search-index-list` | ❌ |
| 18 | 0.234774 | `azmcp-search-service-list` | ❌ |
| 19 | 0.224830 | `azmcp-workbooks-delete` | ❌ |
| 20 | 0.221064 | `azmcp-role-assignment-list` | ❌ |

---

## Test 70

**Expected Tool:** `azmcp-deploy-iac-rules-get`  
**Prompt:** Show me the rules to generate bicep scripts  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.529092 | `azmcp-deploy-iac-rules-get` | ✅ **EXPECTED** |
| 2 | 0.428630 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.420630 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 4 | 0.415286 | `azmcp-extension-az` | ❌ |
| 5 | 0.404829 | `azmcp-bicepschema-get` | ❌ |
| 6 | 0.341436 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 7 | 0.304788 | `azmcp-deploy-plan-get` | ❌ |
| 8 | 0.266851 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 9 | 0.265367 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 10 | 0.243314 | `azmcp-extension-azd` | ❌ |
| 11 | 0.223979 | `azmcp-extension-azqr` | ❌ |
| 12 | 0.219521 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.201288 | `azmcp-quota-region-availability-list` | ❌ |
| 14 | 0.199541 | `azmcp-storage-share-file-list` | ❌ |
| 15 | 0.196151 | `azmcp-storage-blob-details` | ❌ |
| 16 | 0.188615 | `azmcp-role-assignment-list` | ❌ |
| 17 | 0.183296 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 18 | 0.175772 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.168258 | `azmcp-redis-cache-list` | ❌ |
| 20 | 0.167343 | `azmcp-sql-server-entra-admin-list` | ❌ |

---

## Test 71

**Expected Tool:** `azmcp-deploy-pipeline-guidance-get`  
**Prompt:** How can I create a CI/CD pipeline to deploy this app to Azure?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.638841 | `azmcp-deploy-pipeline-guidance-get` | ✅ **EXPECTED** |
| 2 | 0.499242 | `azmcp-deploy-plan-get` | ❌ |
| 3 | 0.448918 | `azmcp-deploy-iac-rules-get` | ❌ |
| 4 | 0.386921 | `azmcp-extension-az` | ❌ |
| 5 | 0.375202 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 6 | 0.373363 | `azmcp-deploy-app-logs-get` | ❌ |
| 7 | 0.346892 | `azmcp-extension-azd` | ❌ |
| 8 | 0.338440 | `azmcp-foundry-models-deploy` | ❌ |
| 9 | 0.327522 | `azmcp-bestpractices-get` | ❌ |
| 10 | 0.327410 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 11 | 0.230335 | `azmcp-storage-blob-upload` | ❌ |
| 12 | 0.230063 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.214408 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.198364 | `azmcp-storage-queue-message-send` | ❌ |
| 15 | 0.194866 | `azmcp-workbooks-delete` | ❌ |
| 16 | 0.163763 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 17 | 0.163702 | `azmcp-storage-datalake-directory-create` | ❌ |
| 18 | 0.163337 | `azmcp-monitor-resource-log-query` | ❌ |
| 19 | 0.161544 | `azmcp-workbooks-create` | ❌ |
| 20 | 0.160898 | `azmcp-resourcehealth-availability-status-get` | ❌ |

---

## Test 72

**Expected Tool:** `azmcp-deploy-plan-get`  
**Prompt:** Create a plan to deploy this application to azure  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.688055 | `azmcp-deploy-plan-get` | ✅ **EXPECTED** |
| 2 | 0.587903 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 3 | 0.499385 | `azmcp-deploy-iac-rules-get` | ❌ |
| 4 | 0.498575 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 5 | 0.428036 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 6 | 0.421875 | `azmcp-bestpractices-get` | ❌ |
| 7 | 0.416245 | `azmcp-extension-az` | ❌ |
| 8 | 0.413718 | `azmcp-loadtesting-test-create` | ❌ |
| 9 | 0.393518 | `azmcp-deploy-app-logs-get` | ❌ |
| 10 | 0.378091 | `azmcp-extension-azd` | ❌ |
| 11 | 0.312839 | `azmcp-quota-usage-check` | ❌ |
| 12 | 0.279583 | `azmcp-workbooks-delete` | ❌ |
| 13 | 0.277289 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.252696 | `azmcp-workbooks-create` | ❌ |
| 15 | 0.242496 | `azmcp-quota-region-availability-list` | ❌ |
| 16 | 0.242042 | `azmcp-storage-account-details` | ❌ |
| 17 | 0.237090 | `azmcp-storage-blob-upload` | ❌ |
| 18 | 0.229583 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 19 | 0.227009 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 20 | 0.226962 | `azmcp-sql-db-list` | ❌ |

---

## Test 73

**Expected Tool:** `azmcp-functionapp-list`  
**Prompt:** List all function apps in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.782194 | `azmcp-functionapp-list` | ✅ **EXPECTED** |
| 2 | 0.547255 | `azmcp-search-service-list` | ❌ |
| 3 | 0.516618 | `azmcp-cosmos-account-list` | ❌ |
| 4 | 0.516217 | `azmcp-appconfig-account-list` | ❌ |
| 5 | 0.489561 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.485423 | `azmcp-subscription-list` | ❌ |
| 7 | 0.474425 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.465575 | `azmcp-group-list` | ❌ |
| 9 | 0.464534 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.455819 | `azmcp-aks-cluster-list` | ❌ |
| 11 | 0.455388 | `azmcp-postgres-server-list` | ❌ |
| 12 | 0.451429 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.445062 | `azmcp-redis-cache-list` | ❌ |
| 14 | 0.442718 | `azmcp-redis-cluster-list` | ❌ |
| 15 | 0.432144 | `azmcp-grafana-list` | ❌ |
| 16 | 0.431611 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 17 | 0.414796 | `azmcp-foundry-models-deployments-list` | ❌ |
| 18 | 0.411904 | `azmcp-sql-db-list` | ❌ |
| 19 | 0.411581 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.411409 | `azmcp-acr-registry-list` | ❌ |

---

## Test 74

**Expected Tool:** `azmcp-functionapp-list`  
**Prompt:** Show me my Azure function apps  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.610364 | `azmcp-functionapp-list` | ✅ **EXPECTED** |
| 2 | 0.452132 | `azmcp-deploy-app-logs-get` | ❌ |
| 3 | 0.385832 | `azmcp-foundry-models-deployments-list` | ❌ |
| 4 | 0.374655 | `azmcp-appconfig-account-list` | ❌ |
| 5 | 0.372790 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.369969 | `azmcp-subscription-list` | ❌ |
| 7 | 0.368018 | `azmcp-extension-az` | ❌ |
| 8 | 0.368004 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 9 | 0.359788 | `azmcp-search-service-list` | ❌ |
| 10 | 0.358879 | `azmcp-bestpractices-get` | ❌ |
| 11 | 0.358720 | `azmcp-deploy-plan-get` | ❌ |
| 12 | 0.357329 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.353279 | `azmcp-extension-azd` | ❌ |
| 14 | 0.345104 | `azmcp-storage-blob-container-list` | ❌ |
| 15 | 0.334019 | `azmcp-role-assignment-list` | ❌ |
| 16 | 0.333136 | `azmcp-sql-db-list` | ❌ |
| 17 | 0.329125 | `azmcp-storage-account-list` | ❌ |
| 18 | 0.327870 | `azmcp-monitor-workspace-list` | ❌ |
| 19 | 0.326628 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 20 | 0.322615 | `azmcp-storage-table-list` | ❌ |

---

## Test 75

**Expected Tool:** `azmcp-functionapp-list`  
**Prompt:** What function apps do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.440801 | `azmcp-functionapp-list` | ✅ **EXPECTED** |
| 2 | 0.348106 | `azmcp-deploy-app-logs-get` | ❌ |
| 3 | 0.256927 | `azmcp-extension-az` | ❌ |
| 4 | 0.249658 | `azmcp-appconfig-account-list` | ❌ |
| 5 | 0.244782 | `azmcp-appconfig-kv-list` | ❌ |
| 6 | 0.240729 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 7 | 0.239514 | `azmcp-foundry-models-deployments-list` | ❌ |
| 8 | 0.235352 | `azmcp-extension-azd` | ❌ |
| 9 | 0.208396 | `azmcp-foundry-models-list` | ❌ |
| 10 | 0.207391 | `azmcp-quota-usage-check` | ❌ |
| 11 | 0.201390 | `azmcp-bestpractices-get` | ❌ |
| 12 | 0.195857 | `azmcp-role-assignment-list` | ❌ |
| 13 | 0.194503 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 14 | 0.185142 | `azmcp-monitor-resource-log-query` | ❌ |
| 15 | 0.184120 | `azmcp-monitor-workspace-list` | ❌ |
| 16 | 0.184051 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 17 | 0.182124 | `azmcp-storage-table-list` | ❌ |
| 18 | 0.181525 | `azmcp-storage-share-file-list` | ❌ |
| 19 | 0.179181 | `azmcp-storage-blob-details` | ❌ |
| 20 | 0.175067 | `azmcp-subscription-list` | ❌ |

---

## Test 76

**Expected Tool:** `azmcp-keyvault-certificate-create`  
**Prompt:** Create a new certificate called <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.740327 | `azmcp-keyvault-certificate-create` | ✅ **EXPECTED** |
| 2 | 0.596578 | `azmcp-keyvault-key-create` | ❌ |
| 3 | 0.590531 | `azmcp-keyvault-secret-create` | ❌ |
| 4 | 0.575960 | `azmcp-keyvault-certificate-list` | ❌ |
| 5 | 0.543043 | `azmcp-keyvault-certificate-get` | ❌ |
| 6 | 0.526698 | `azmcp-keyvault-certificate-import` | ❌ |
| 7 | 0.434682 | `azmcp-keyvault-key-list` | ❌ |
| 8 | 0.413988 | `azmcp-keyvault-secret-list` | ❌ |
| 9 | 0.394787 | `azmcp-storage-account-create` | ❌ |
| 10 | 0.330026 | `azmcp-appconfig-kv-set` | ❌ |
| 11 | 0.308667 | `azmcp-loadtesting-test-create` | ❌ |
| 12 | 0.300980 | `azmcp-storage-datalake-directory-create` | ❌ |
| 13 | 0.285184 | `azmcp-workbooks-create` | ❌ |
| 14 | 0.254339 | `azmcp-storage-account-details` | ❌ |
| 15 | 0.235260 | `azmcp-storage-blob-container-list` | ❌ |
| 16 | 0.233821 | `azmcp-storage-table-list` | ❌ |
| 17 | 0.226937 | `azmcp-storage-account-list` | ❌ |
| 18 | 0.220119 | `azmcp-subscription-list` | ❌ |
| 19 | 0.210729 | `azmcp-search-service-list` | ❌ |
| 20 | 0.208916 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 77

**Expected Tool:** `azmcp-keyvault-certificate-get`  
**Prompt:** Show me the certificate <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628076 | `azmcp-keyvault-certificate-get` | ✅ **EXPECTED** |
| 2 | 0.624397 | `azmcp-keyvault-certificate-list` | ❌ |
| 3 | 0.565206 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.539600 | `azmcp-keyvault-certificate-import` | ❌ |
| 5 | 0.493245 | `azmcp-keyvault-key-list` | ❌ |
| 6 | 0.475305 | `azmcp-keyvault-secret-list` | ❌ |
| 7 | 0.423962 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.419059 | `azmcp-keyvault-secret-create` | ❌ |
| 9 | 0.390350 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.345482 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.341067 | `azmcp-storage-account-details` | ❌ |
| 12 | 0.316889 | `azmcp-storage-blob-container-list` | ❌ |
| 13 | 0.316822 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.293501 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.293464 | `azmcp-subscription-list` | ❌ |
| 16 | 0.288471 | `azmcp-storage-blob-list` | ❌ |
| 17 | 0.276323 | `azmcp-role-assignment-list` | ❌ |
| 18 | 0.273790 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.272007 | `azmcp-quota-usage-check` | ❌ |
| 20 | 0.269854 | `azmcp-sql-db-show` | ❌ |

---

## Test 78

**Expected Tool:** `azmcp-keyvault-certificate-get`  
**Prompt:** Show me the details of the certificate <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.662324 | `azmcp-keyvault-certificate-get` | ✅ **EXPECTED** |
| 2 | 0.606534 | `azmcp-keyvault-certificate-list` | ❌ |
| 3 | 0.540155 | `azmcp-keyvault-certificate-import` | ❌ |
| 4 | 0.535157 | `azmcp-keyvault-certificate-create` | ❌ |
| 5 | 0.499272 | `azmcp-keyvault-key-list` | ❌ |
| 6 | 0.482326 | `azmcp-keyvault-secret-list` | ❌ |
| 7 | 0.432557 | `azmcp-storage-account-details` | ❌ |
| 8 | 0.416039 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.412434 | `azmcp-keyvault-secret-create` | ❌ |
| 10 | 0.411136 | `azmcp-appconfig-kv-show` | ❌ |
| 11 | 0.365386 | `azmcp-sql-db-show` | ❌ |
| 12 | 0.363192 | `azmcp-aks-cluster-get` | ❌ |
| 13 | 0.332921 | `azmcp-storage-blob-container-details` | ❌ |
| 14 | 0.316364 | `azmcp-storage-blob-container-list` | ❌ |
| 15 | 0.315096 | `azmcp-storage-table-list` | ❌ |
| 16 | 0.306107 | `azmcp-subscription-list` | ❌ |
| 17 | 0.301710 | `azmcp-servicebus-queue-details` | ❌ |
| 18 | 0.295651 | `azmcp-storage-account-list` | ❌ |
| 19 | 0.290918 | `azmcp-storage-blob-list` | ❌ |
| 20 | 0.289520 | `azmcp-role-assignment-list` | ❌ |

---

## Test 79

**Expected Tool:** `azmcp-keyvault-certificate-import`  
**Prompt:** Import the certificate in file <file_path> into the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.649993 | `azmcp-keyvault-certificate-import` | ✅ **EXPECTED** |
| 2 | 0.521183 | `azmcp-keyvault-certificate-create` | ❌ |
| 3 | 0.469706 | `azmcp-keyvault-certificate-get` | ❌ |
| 4 | 0.467097 | `azmcp-keyvault-certificate-list` | ❌ |
| 5 | 0.426231 | `azmcp-keyvault-key-create` | ❌ |
| 6 | 0.398035 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.364868 | `azmcp-keyvault-key-list` | ❌ |
| 8 | 0.337861 | `azmcp-keyvault-secret-list` | ❌ |
| 9 | 0.269560 | `azmcp-appconfig-kv-lock` | ❌ |
| 10 | 0.267356 | `azmcp-appconfig-kv-set` | ❌ |
| 11 | 0.253889 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 12 | 0.238854 | `azmcp-storage-blob-upload` | ❌ |
| 13 | 0.230685 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.224373 | `azmcp-workbooks-delete` | ❌ |
| 15 | 0.217294 | `azmcp-storage-blob-container-list` | ❌ |
| 16 | 0.214526 | `azmcp-storage-account-details` | ❌ |
| 17 | 0.200472 | `azmcp-storage-datalake-directory-create` | ❌ |
| 18 | 0.199045 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.195655 | `azmcp-storage-blob-list` | ❌ |
| 20 | 0.193594 | `azmcp-storage-account-list` | ❌ |

---

## Test 80

**Expected Tool:** `azmcp-keyvault-certificate-import`  
**Prompt:** Import a certificate into the key vault <key_vault_account_name> using the name <certificate_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.649759 | `azmcp-keyvault-certificate-import` | ✅ **EXPECTED** |
| 2 | 0.629844 | `azmcp-keyvault-certificate-create` | ❌ |
| 3 | 0.527368 | `azmcp-keyvault-certificate-list` | ❌ |
| 4 | 0.525450 | `azmcp-keyvault-certificate-get` | ❌ |
| 5 | 0.492440 | `azmcp-keyvault-key-create` | ❌ |
| 6 | 0.472521 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.399866 | `azmcp-keyvault-key-list` | ❌ |
| 8 | 0.377819 | `azmcp-keyvault-secret-list` | ❌ |
| 9 | 0.291549 | `azmcp-storage-account-create` | ❌ |
| 10 | 0.287426 | `azmcp-appconfig-kv-set` | ❌ |
| 11 | 0.265773 | `azmcp-appconfig-kv-lock` | ❌ |
| 12 | 0.238396 | `azmcp-storage-account-details` | ❌ |
| 13 | 0.234462 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.229408 | `azmcp-storage-blob-container-list` | ❌ |
| 15 | 0.227212 | `azmcp-workbooks-delete` | ❌ |
| 16 | 0.211466 | `azmcp-storage-datalake-directory-create` | ❌ |
| 17 | 0.209861 | `azmcp-storage-account-list` | ❌ |
| 18 | 0.203243 | `azmcp-storage-blob-upload` | ❌ |
| 19 | 0.197369 | `azmcp-sql-db-show` | ❌ |
| 20 | 0.196941 | `azmcp-workbooks-create` | ❌ |

---

## Test 81

**Expected Tool:** `azmcp-keyvault-certificate-list`  
**Prompt:** List all certificates in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.762015 | `azmcp-keyvault-certificate-list` | ✅ **EXPECTED** |
| 2 | 0.637437 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.608740 | `azmcp-keyvault-secret-list` | ❌ |
| 4 | 0.566460 | `azmcp-keyvault-certificate-get` | ❌ |
| 5 | 0.539624 | `azmcp-keyvault-certificate-create` | ❌ |
| 6 | 0.484660 | `azmcp-keyvault-certificate-import` | ❌ |
| 7 | 0.478100 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.453226 | `azmcp-cosmos-database-list` | ❌ |
| 9 | 0.440871 | `azmcp-storage-blob-container-list` | ❌ |
| 10 | 0.431201 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.429531 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.425556 | `azmcp-storage-account-list` | ❌ |
| 13 | 0.424818 | `azmcp-keyvault-key-create` | ❌ |
| 14 | 0.417908 | `azmcp-storage-blob-list` | ❌ |
| 15 | 0.408582 | `azmcp-subscription-list` | ❌ |
| 16 | 0.373773 | `azmcp-search-index-list` | ❌ |
| 17 | 0.368478 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 18 | 0.366071 | `azmcp-storage-account-details` | ❌ |
| 19 | 0.358938 | `azmcp-role-assignment-list` | ❌ |
| 20 | 0.357371 | `azmcp-search-service-list` | ❌ |

---

## Test 82

**Expected Tool:** `azmcp-keyvault-certificate-list`  
**Prompt:** Show me the certificates in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.660576 | `azmcp-keyvault-certificate-list` | ✅ **EXPECTED** |
| 2 | 0.570282 | `azmcp-keyvault-certificate-get` | ❌ |
| 3 | 0.540050 | `azmcp-keyvault-key-list` | ❌ |
| 4 | 0.516712 | `azmcp-keyvault-secret-list` | ❌ |
| 5 | 0.509123 | `azmcp-keyvault-certificate-create` | ❌ |
| 6 | 0.483404 | `azmcp-keyvault-certificate-import` | ❌ |
| 7 | 0.420506 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.396479 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.390169 | `azmcp-keyvault-secret-create` | ❌ |
| 10 | 0.382983 | `azmcp-storage-blob-container-list` | ❌ |
| 11 | 0.382082 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.373188 | `azmcp-storage-account-details` | ❌ |
| 13 | 0.372424 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.363182 | `azmcp-subscription-list` | ❌ |
| 15 | 0.361862 | `azmcp-storage-account-list` | ❌ |
| 16 | 0.351372 | `azmcp-storage-blob-list` | ❌ |
| 17 | 0.323177 | `azmcp-role-assignment-list` | ❌ |
| 18 | 0.317493 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.317235 | `azmcp-search-index-list` | ❌ |
| 20 | 0.300672 | `azmcp-search-service-list` | ❌ |

---

## Test 83

**Expected Tool:** `azmcp-keyvault-key-create`  
**Prompt:** Create a new key called <key_name> with the RSA type in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.676472 | `azmcp-keyvault-key-create` | ✅ **EXPECTED** |
| 2 | 0.568637 | `azmcp-keyvault-secret-create` | ❌ |
| 3 | 0.554926 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.465154 | `azmcp-keyvault-key-list` | ❌ |
| 5 | 0.420407 | `azmcp-storage-account-create` | ❌ |
| 6 | 0.416650 | `azmcp-keyvault-certificate-list` | ❌ |
| 7 | 0.412401 | `azmcp-keyvault-secret-list` | ❌ |
| 8 | 0.411918 | `azmcp-keyvault-certificate-import` | ❌ |
| 9 | 0.396991 | `azmcp-appconfig-kv-set` | ❌ |
| 10 | 0.389113 | `azmcp-keyvault-certificate-get` | ❌ |
| 11 | 0.340847 | `azmcp-appconfig-kv-lock` | ❌ |
| 12 | 0.286183 | `azmcp-storage-datalake-directory-create` | ❌ |
| 13 | 0.282421 | `azmcp-storage-account-details` | ❌ |
| 14 | 0.265659 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.261432 | `azmcp-workbooks-create` | ❌ |
| 16 | 0.260758 | `azmcp-storage-blob-container-list` | ❌ |
| 17 | 0.251731 | `azmcp-storage-table-list` | ❌ |
| 18 | 0.235162 | `azmcp-storage-blob-list` | ❌ |
| 19 | 0.223245 | `azmcp-storage-queue-message-send` | ❌ |
| 20 | 0.215807 | `azmcp-subscription-list` | ❌ |

---

## Test 84

**Expected Tool:** `azmcp-keyvault-key-list`  
**Prompt:** List all keys in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.737135 | `azmcp-keyvault-key-list` | ✅ **EXPECTED** |
| 2 | 0.649972 | `azmcp-keyvault-secret-list` | ❌ |
| 3 | 0.631528 | `azmcp-keyvault-certificate-list` | ❌ |
| 4 | 0.498767 | `azmcp-cosmos-account-list` | ❌ |
| 5 | 0.473916 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.473155 | `azmcp-storage-blob-container-list` | ❌ |
| 7 | 0.468044 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.467606 | `azmcp-keyvault-key-create` | ❌ |
| 9 | 0.461513 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.455805 | `azmcp-keyvault-certificate-get` | ❌ |
| 11 | 0.455016 | `azmcp-storage-blob-list` | ❌ |
| 12 | 0.443785 | `azmcp-cosmos-database-container-list` | ❌ |
| 13 | 0.439167 | `azmcp-appconfig-kv-list` | ❌ |
| 14 | 0.428290 | `azmcp-keyvault-secret-create` | ❌ |
| 15 | 0.427261 | `azmcp-subscription-list` | ❌ |
| 16 | 0.403964 | `azmcp-storage-account-details` | ❌ |
| 17 | 0.402742 | `azmcp-search-index-list` | ❌ |
| 18 | 0.378059 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.376452 | `azmcp-search-service-list` | ❌ |
| 20 | 0.360638 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

---

## Test 85

**Expected Tool:** `azmcp-keyvault-key-list`  
**Prompt:** Show me the keys in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609392 | `azmcp-keyvault-key-list` | ✅ **EXPECTED** |
| 2 | 0.535254 | `azmcp-keyvault-secret-list` | ❌ |
| 3 | 0.520010 | `azmcp-keyvault-certificate-list` | ❌ |
| 4 | 0.479818 | `azmcp-keyvault-certificate-get` | ❌ |
| 5 | 0.462613 | `azmcp-keyvault-key-create` | ❌ |
| 6 | 0.429515 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.421475 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.412607 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.408423 | `azmcp-keyvault-certificate-import` | ❌ |
| 10 | 0.405205 | `azmcp-appconfig-kv-show` | ❌ |
| 11 | 0.390487 | `azmcp-storage-blob-container-list` | ❌ |
| 12 | 0.382971 | `azmcp-storage-account-details` | ❌ |
| 13 | 0.375139 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.368473 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.360209 | `azmcp-storage-blob-list` | ❌ |
| 16 | 0.353593 | `azmcp-subscription-list` | ❌ |
| 17 | 0.323400 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 18 | 0.320761 | `azmcp-search-index-list` | ❌ |
| 19 | 0.312486 | `azmcp-storage-blob-container-details` | ❌ |
| 20 | 0.307809 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |

---

## Test 86

**Expected Tool:** `azmcp-keyvault-secret-create`  
**Prompt:** Create a new secret called <secret_name> with value <secret_value> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.767701 | `azmcp-keyvault-secret-create` | ✅ **EXPECTED** |
| 2 | 0.614773 | `azmcp-keyvault-key-create` | ❌ |
| 3 | 0.572297 | `azmcp-keyvault-certificate-create` | ❌ |
| 4 | 0.516683 | `azmcp-keyvault-secret-list` | ❌ |
| 5 | 0.461437 | `azmcp-appconfig-kv-set` | ❌ |
| 6 | 0.428384 | `azmcp-storage-account-create` | ❌ |
| 7 | 0.417525 | `azmcp-keyvault-key-list` | ❌ |
| 8 | 0.411481 | `azmcp-keyvault-certificate-import` | ❌ |
| 9 | 0.384262 | `azmcp-keyvault-certificate-list` | ❌ |
| 10 | 0.373932 | `azmcp-appconfig-kv-lock` | ❌ |
| 11 | 0.369946 | `azmcp-keyvault-certificate-get` | ❌ |
| 12 | 0.321535 | `azmcp-storage-datalake-directory-create` | ❌ |
| 13 | 0.288055 | `azmcp-storage-account-details` | ❌ |
| 14 | 0.287066 | `azmcp-workbooks-create` | ❌ |
| 15 | 0.275431 | `azmcp-storage-queue-message-send` | ❌ |
| 16 | 0.246820 | `azmcp-storage-blob-container-list` | ❌ |
| 17 | 0.246298 | `azmcp-storage-account-list` | ❌ |
| 18 | 0.236457 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.222749 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 20 | 0.210088 | `azmcp-subscription-list` | ❌ |

---

## Test 87

**Expected Tool:** `azmcp-keyvault-secret-list`  
**Prompt:** List all secrets in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.747440 | `azmcp-keyvault-secret-list` | ✅ **EXPECTED** |
| 2 | 0.617131 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.569911 | `azmcp-keyvault-certificate-list` | ❌ |
| 4 | 0.519133 | `azmcp-keyvault-secret-create` | ❌ |
| 5 | 0.455500 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.433610 | `azmcp-storage-blob-container-list` | ❌ |
| 7 | 0.433185 | `azmcp-cosmos-database-list` | ❌ |
| 8 | 0.417973 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.415723 | `azmcp-storage-blob-list` | ❌ |
| 10 | 0.414310 | `azmcp-keyvault-certificate-get` | ❌ |
| 11 | 0.414216 | `azmcp-storage-account-list` | ❌ |
| 12 | 0.410742 | `azmcp-keyvault-key-create` | ❌ |
| 13 | 0.410496 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.392378 | `azmcp-keyvault-certificate-create` | ❌ |
| 15 | 0.391346 | `azmcp-subscription-list` | ❌ |
| 16 | 0.364601 | `azmcp-storage-account-details` | ❌ |
| 17 | 0.355446 | `azmcp-search-index-list` | ❌ |
| 18 | 0.347416 | `azmcp-search-service-list` | ❌ |
| 19 | 0.341082 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 20 | 0.340472 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |

---

## Test 88

**Expected Tool:** `azmcp-keyvault-secret-list`  
**Prompt:** Show me the secrets in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.615460 | `azmcp-keyvault-secret-list` | ✅ **EXPECTED** |
| 2 | 0.520654 | `azmcp-keyvault-key-list` | ❌ |
| 3 | 0.502403 | `azmcp-keyvault-secret-create` | ❌ |
| 4 | 0.467743 | `azmcp-keyvault-certificate-list` | ❌ |
| 5 | 0.456355 | `azmcp-keyvault-certificate-get` | ❌ |
| 6 | 0.412474 | `azmcp-keyvault-key-create` | ❌ |
| 7 | 0.410957 | `azmcp-appconfig-kv-show` | ❌ |
| 8 | 0.409126 | `azmcp-keyvault-certificate-import` | ❌ |
| 9 | 0.395481 | `azmcp-storage-account-details` | ❌ |
| 10 | 0.385852 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.381612 | `azmcp-keyvault-certificate-create` | ❌ |
| 12 | 0.370457 | `azmcp-storage-blob-container-list` | ❌ |
| 13 | 0.345411 | `azmcp-subscription-list` | ❌ |
| 14 | 0.344682 | `azmcp-storage-blob-list` | ❌ |
| 15 | 0.344339 | `azmcp-storage-table-list` | ❌ |
| 16 | 0.341754 | `azmcp-storage-blob-container-details` | ❌ |
| 17 | 0.336315 | `azmcp-storage-account-list` | ❌ |
| 18 | 0.303769 | `azmcp-quota-usage-check` | ❌ |
| 19 | 0.301358 | `azmcp-storage-blob-details` | ❌ |
| 20 | 0.299295 | `azmcp-search-index-list` | ❌ |

---

## Test 89

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Get the configuration of AKS cluster <cluster-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661142 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.611431 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.463591 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.456804 | `azmcp-loadtesting-test-get` | ❌ |
| 5 | 0.430975 | `azmcp-postgres-server-config-get` | ❌ |
| 6 | 0.392990 | `azmcp-storage-account-details` | ❌ |
| 7 | 0.391924 | `azmcp-appconfig-kv-show` | ❌ |
| 8 | 0.390959 | `azmcp-appconfig-account-list` | ❌ |
| 9 | 0.390819 | `azmcp-appconfig-kv-list` | ❌ |
| 10 | 0.390141 | `azmcp-kusto-cluster-list` | ❌ |
| 11 | 0.367875 | `azmcp-redis-cluster-list` | ❌ |
| 12 | 0.350240 | `azmcp-sql-db-show` | ❌ |
| 13 | 0.349742 | `azmcp-keyvault-certificate-get` | ❌ |
| 14 | 0.349205 | `azmcp-loadtesting-test-create` | ❌ |
| 15 | 0.339882 | `azmcp-sql-db-list` | ❌ |
| 16 | 0.337661 | `azmcp-sql-elastic-pool-list` | ❌ |
| 17 | 0.334959 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 18 | 0.325605 | `azmcp-storage-blob-details` | ❌ |
| 19 | 0.314999 | `azmcp-quota-usage-check` | ❌ |
| 20 | 0.314207 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 90

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Show me the details of AKS cluster <cluster-name> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.667078 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.589101 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.507985 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.461466 | `azmcp-sql-db-show` | ❌ |
| 5 | 0.448783 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.422993 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 7 | 0.396636 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.385261 | `azmcp-acr-registry-repository-list` | ❌ |
| 9 | 0.384654 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.371570 | `azmcp-group-list` | ❌ |
| 11 | 0.365232 | `azmcp-sql-elastic-pool-list` | ❌ |
| 12 | 0.362332 | `azmcp-sql-db-list` | ❌ |
| 13 | 0.356690 | `azmcp-loadtesting-test-get` | ❌ |
| 14 | 0.355049 | `azmcp-storage-account-details` | ❌ |
| 15 | 0.353625 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 16 | 0.350559 | `azmcp-workbooks-list` | ❌ |
| 17 | 0.349684 | `azmcp-deploy-app-logs-get` | ❌ |
| 18 | 0.347697 | `azmcp-acr-registry-list` | ❌ |
| 19 | 0.345456 | `azmcp-redis-cache-list` | ❌ |
| 20 | 0.339152 | `azmcp-quota-usage-check` | ❌ |

---

## Test 91

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** Show me the network configuration for AKS cluster <cluster-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.567426 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.563029 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.368538 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.340349 | `azmcp-loadtesting-test-get` | ❌ |
| 5 | 0.340293 | `azmcp-kusto-cluster-list` | ❌ |
| 6 | 0.334923 | `azmcp-appconfig-account-list` | ❌ |
| 7 | 0.334881 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.314513 | `azmcp-appconfig-kv-list` | ❌ |
| 9 | 0.309738 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.296592 | `azmcp-postgres-server-config-get` | ❌ |
| 11 | 0.295183 | `azmcp-loadtesting-test-create` | ❌ |
| 12 | 0.290596 | `azmcp-deploy-app-logs-get` | ❌ |
| 13 | 0.283065 | `azmcp-storage-account-details` | ❌ |
| 14 | 0.275751 | `azmcp-sql-db-show` | ❌ |
| 15 | 0.273195 | `azmcp-monitor-workspace-list` | ❌ |
| 16 | 0.267611 | `azmcp-sql-elastic-pool-list` | ❌ |
| 17 | 0.265086 | `azmcp-sql-db-list` | ❌ |
| 18 | 0.262034 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 19 | 0.262012 | `azmcp-role-assignment-list` | ❌ |
| 20 | 0.258607 | `azmcp-virtualdesktop-hostpool-list` | ❌ |

---

## Test 92

**Expected Tool:** `azmcp-aks-cluster-get`  
**Prompt:** What are the details of my AKS cluster <cluster-name> in <resource-group>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661753 | `azmcp-aks-cluster-get` | ✅ **EXPECTED** |
| 2 | 0.578662 | `azmcp-aks-cluster-list` | ❌ |
| 3 | 0.503953 | `azmcp-kusto-cluster-get` | ❌ |
| 4 | 0.419338 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 5 | 0.418527 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.417836 | `azmcp-sql-db-show` | ❌ |
| 7 | 0.390071 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 8 | 0.380074 | `azmcp-storage-account-details` | ❌ |
| 9 | 0.372812 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.367547 | `azmcp-deploy-app-logs-get` | ❌ |
| 11 | 0.360459 | `azmcp-sql-elastic-pool-list` | ❌ |
| 12 | 0.359877 | `azmcp-acr-registry-repository-list` | ❌ |
| 13 | 0.357011 | `azmcp-loadtesting-test-get` | ❌ |
| 14 | 0.354685 | `azmcp-quota-usage-check` | ❌ |
| 15 | 0.353462 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 16 | 0.345652 | `azmcp-sql-db-list` | ❌ |
| 17 | 0.344520 | `azmcp-extension-az` | ❌ |
| 18 | 0.343973 | `azmcp-storage-blob-container-details` | ❌ |
| 19 | 0.343122 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.341974 | `azmcp-storage-account-create` | ❌ |

---

## Test 93

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** List all AKS clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.801067 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.690255 | `azmcp-kusto-cluster-list` | ❌ |
| 3 | 0.599944 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.561050 | `azmcp-aks-cluster-get` | ❌ |
| 5 | 0.549327 | `azmcp-search-service-list` | ❌ |
| 6 | 0.543684 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.515922 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.509096 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.505790 | `azmcp-functionapp-list` | ❌ |
| 10 | 0.502463 | `azmcp-subscription-list` | ❌ |
| 11 | 0.498121 | `azmcp-group-list` | ❌ |
| 12 | 0.495977 | `azmcp-postgres-server-list` | ❌ |
| 13 | 0.486591 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 14 | 0.486123 | `azmcp-redis-cache-list` | ❌ |
| 15 | 0.483713 | `azmcp-storage-account-list` | ❌ |
| 16 | 0.483658 | `azmcp-kusto-cluster-get` | ❌ |
| 17 | 0.482328 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.481469 | `azmcp-grafana-list` | ❌ |
| 19 | 0.452681 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 20 | 0.445271 | `azmcp-storage-table-list` | ❌ |

---

## Test 94

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** Show me my Azure Kubernetes Service clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.608056 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.536366 | `azmcp-aks-cluster-get` | ❌ |
| 3 | 0.492910 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.446335 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.409417 | `azmcp-kusto-cluster-get` | ❌ |
| 6 | 0.408364 | `azmcp-kusto-database-list` | ❌ |
| 7 | 0.388143 | `azmcp-search-service-list` | ❌ |
| 8 | 0.387737 | `azmcp-search-index-list` | ❌ |
| 9 | 0.371535 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.370237 | `azmcp-acr-registry-repository-list` | ❌ |
| 11 | 0.363889 | `azmcp-subscription-list` | ❌ |
| 12 | 0.362676 | `azmcp-acr-registry-list` | ❌ |
| 13 | 0.360053 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.359675 | `azmcp-storage-blob-container-list` | ❌ |
| 15 | 0.356926 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 16 | 0.355886 | `azmcp-keyvault-secret-list` | ❌ |
| 17 | 0.354872 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.349446 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.348854 | `azmcp-postgres-server-list` | ❌ |
| 20 | 0.347078 | `azmcp-quota-usage-check` | ❌ |

---

## Test 95

**Expected Tool:** `azmcp-aks-cluster-list`  
**Prompt:** What AKS clusters do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.623896 | `azmcp-aks-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.530100 | `azmcp-aks-cluster-get` | ❌ |
| 3 | 0.449602 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.416575 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.378826 | `azmcp-monitor-workspace-list` | ❌ |
| 6 | 0.377567 | `azmcp-acr-registry-repository-list` | ❌ |
| 7 | 0.364022 | `azmcp-deploy-app-logs-get` | ❌ |
| 8 | 0.345290 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 9 | 0.345196 | `azmcp-kusto-cluster-get` | ❌ |
| 10 | 0.342303 | `azmcp-extension-az` | ❌ |
| 11 | 0.341582 | `azmcp-kusto-database-list` | ❌ |
| 12 | 0.335444 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 13 | 0.334400 | `azmcp-acr-registry-list` | ❌ |
| 14 | 0.328074 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.322075 | `azmcp-sql-elastic-pool-list` | ❌ |
| 16 | 0.317238 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 17 | 0.313457 | `azmcp-storage-blob-container-list` | ❌ |
| 18 | 0.312158 | `azmcp-subscription-list` | ❌ |
| 19 | 0.311971 | `azmcp-quota-usage-check` | ❌ |
| 20 | 0.311888 | `azmcp-monitor-table-type-list` | ❌ |

---

## Test 96

**Expected Tool:** `azmcp-loadtesting-test-create`  
**Prompt:** Create a basic URL test using the following endpoint URL <test-url> that runs for 30 minutes with 45 virtual users. The test name is <sample-name> with the test id <test-id> and the load testing resource is <load-test-resource> in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.585388 | `azmcp-loadtesting-test-create` | ✅ **EXPECTED** |
| 2 | 0.531362 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.508690 | `azmcp-loadtesting-testrun-create` | ❌ |
| 4 | 0.413857 | `azmcp-loadtesting-testresource-list` | ❌ |
| 5 | 0.402698 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.399602 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.346526 | `azmcp-loadtesting-testrun-update` | ❌ |
| 8 | 0.342853 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.336804 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.323398 | `azmcp-monitor-workspace-log-query` | ❌ |
| 11 | 0.322239 | `azmcp-storage-account-create` | ❌ |
| 12 | 0.310466 | `azmcp-keyvault-certificate-create` | ❌ |
| 13 | 0.310144 | `azmcp-workbooks-create` | ❌ |
| 14 | 0.299413 | `azmcp-keyvault-key-create` | ❌ |
| 15 | 0.296991 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 16 | 0.290957 | `azmcp-quota-usage-check` | ❌ |
| 17 | 0.288940 | `azmcp-quota-region-availability-list` | ❌ |
| 18 | 0.280483 | `azmcp-storage-queue-message-send` | ❌ |
| 19 | 0.273887 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 20 | 0.261254 | `azmcp-monitor-metrics-query` | ❌ |

---

## Test 97

**Expected Tool:** `azmcp-loadtesting-test-get`  
**Prompt:** Get the load test with id <test-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.643620 | `azmcp-loadtesting-test-get` | ✅ **EXPECTED** |
| 2 | 0.608702 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.574200 | `azmcp-loadtesting-testresource-create` | ❌ |
| 4 | 0.540996 | `azmcp-loadtesting-testrun-get` | ❌ |
| 5 | 0.473718 | `azmcp-loadtesting-testrun-list` | ❌ |
| 6 | 0.473184 | `azmcp-loadtesting-testrun-create` | ❌ |
| 7 | 0.436648 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.407004 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.397460 | `azmcp-group-list` | ❌ |
| 10 | 0.379486 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 11 | 0.373110 | `azmcp-loadtesting-testrun-update` | ❌ |
| 12 | 0.370087 | `azmcp-workbooks-show` | ❌ |
| 13 | 0.365684 | `azmcp-workbooks-list` | ❌ |
| 14 | 0.360751 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 15 | 0.346498 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 16 | 0.341296 | `azmcp-quota-region-availability-list` | ❌ |
| 17 | 0.329549 | `azmcp-sql-db-show` | ❌ |
| 18 | 0.328449 | `azmcp-monitor-metrics-query` | ❌ |
| 19 | 0.322903 | `azmcp-quota-usage-check` | ❌ |
| 20 | 0.298737 | `azmcp-monitor-workspace-log-query` | ❌ |

---

## Test 98

**Expected Tool:** `azmcp-loadtesting-testresource-create`  
**Prompt:** Create a load test resource <load-test-resource-name> in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.717577 | `azmcp-loadtesting-testresource-create` | ✅ **EXPECTED** |
| 2 | 0.596828 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.514437 | `azmcp-loadtesting-test-create` | ❌ |
| 4 | 0.476662 | `azmcp-loadtesting-testrun-create` | ❌ |
| 5 | 0.447548 | `azmcp-loadtesting-test-get` | ❌ |
| 6 | 0.442167 | `azmcp-workbooks-create` | ❌ |
| 7 | 0.416885 | `azmcp-group-list` | ❌ |
| 8 | 0.394967 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.382774 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 10 | 0.376671 | `azmcp-storage-account-create` | ❌ |
| 11 | 0.375890 | `azmcp-loadtesting-testrun-get` | ❌ |
| 12 | 0.369409 | `azmcp-workbooks-list` | ❌ |
| 13 | 0.350916 | `azmcp-loadtesting-testrun-update` | ❌ |
| 14 | 0.342247 | `azmcp-redis-cluster-list` | ❌ |
| 15 | 0.341251 | `azmcp-grafana-list` | ❌ |
| 16 | 0.335706 | `azmcp-redis-cache-list` | ❌ |
| 17 | 0.328684 | `azmcp-monitor-resource-log-query` | ❌ |
| 18 | 0.326617 | `azmcp-quota-region-availability-list` | ❌ |
| 19 | 0.306434 | `azmcp-quota-usage-check` | ❌ |
| 20 | 0.298311 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 99

**Expected Tool:** `azmcp-loadtesting-testresource-list`  
**Prompt:** List all load testing resources in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.738027 | `azmcp-loadtesting-testresource-list` | ✅ **EXPECTED** |
| 2 | 0.591851 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.577408 | `azmcp-group-list` | ❌ |
| 4 | 0.565565 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.561516 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 6 | 0.526662 | `azmcp-workbooks-list` | ❌ |
| 7 | 0.515682 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.512935 | `azmcp-loadtesting-test-get` | ❌ |
| 9 | 0.511567 | `azmcp-redis-cache-list` | ❌ |
| 10 | 0.488178 | `azmcp-loadtesting-testrun-list` | ❌ |
| 11 | 0.487330 | `azmcp-grafana-list` | ❌ |
| 12 | 0.470869 | `azmcp-acr-registry-list` | ❌ |
| 13 | 0.467689 | `azmcp-loadtesting-testrun-get` | ❌ |
| 14 | 0.458800 | `azmcp-acr-registry-repository-list` | ❌ |
| 15 | 0.454667 | `azmcp-search-service-list` | ❌ |
| 16 | 0.452190 | `azmcp-monitor-workspace-list` | ❌ |
| 17 | 0.447138 | `azmcp-quota-region-availability-list` | ❌ |
| 18 | 0.437348 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.426880 | `azmcp-sql-db-list` | ❌ |
| 20 | 0.411694 | `azmcp-sql-elastic-pool-list` | ❌ |

---

## Test 100

**Expected Tool:** `azmcp-loadtesting-testrun-create`  
**Prompt:** Create a test run using the id <testrun-id> for test <test-id> in the load testing resource <load-testing-resource> in resource group <resource-group>. Use the name of test run <display-name> and description as <description>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.621803 | `azmcp-loadtesting-testrun-create` | ✅ **EXPECTED** |
| 2 | 0.592805 | `azmcp-loadtesting-testresource-create` | ❌ |
| 3 | 0.540789 | `azmcp-loadtesting-test-create` | ❌ |
| 4 | 0.530882 | `azmcp-loadtesting-testrun-update` | ❌ |
| 5 | 0.489907 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.472404 | `azmcp-loadtesting-test-get` | ❌ |
| 7 | 0.413872 | `azmcp-loadtesting-testrun-list` | ❌ |
| 8 | 0.411627 | `azmcp-loadtesting-testresource-list` | ❌ |
| 9 | 0.402120 | `azmcp-workbooks-create` | ❌ |
| 10 | 0.354620 | `azmcp-storage-account-create` | ❌ |
| 11 | 0.331310 | `azmcp-keyvault-key-create` | ❌ |
| 12 | 0.325463 | `azmcp-keyvault-secret-create` | ❌ |
| 13 | 0.314636 | `azmcp-storage-datalake-directory-create` | ❌ |
| 14 | 0.309076 | `azmcp-monitor-resource-log-query` | ❌ |
| 15 | 0.272151 | `azmcp-sql-db-show` | ❌ |
| 16 | 0.267551 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 17 | 0.260678 | `azmcp-storage-queue-message-send` | ❌ |
| 18 | 0.256035 | `azmcp-monitor-metrics-query` | ❌ |
| 19 | 0.250958 | `azmcp-monitor-workspace-log-query` | ❌ |
| 20 | 0.249643 | `azmcp-workbooks-show` | ❌ |

---

## Test 101

**Expected Tool:** `azmcp-loadtesting-testrun-get`  
**Prompt:** Get the load test run with id <testrun-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.626778 | `azmcp-loadtesting-test-get` | ❌ |
| 2 | 0.603066 | `azmcp-loadtesting-testresource-list` | ❌ |
| 3 | 0.572731 | `azmcp-loadtesting-testrun-get` | ✅ **EXPECTED** |
| 4 | 0.561944 | `azmcp-loadtesting-testresource-create` | ❌ |
| 5 | 0.535183 | `azmcp-loadtesting-testrun-create` | ❌ |
| 6 | 0.499389 | `azmcp-loadtesting-testrun-list` | ❌ |
| 7 | 0.434255 | `azmcp-loadtesting-test-create` | ❌ |
| 8 | 0.415438 | `azmcp-loadtesting-testrun-update` | ❌ |
| 9 | 0.397875 | `azmcp-group-list` | ❌ |
| 10 | 0.397370 | `azmcp-monitor-resource-log-query` | ❌ |
| 11 | 0.370196 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 12 | 0.366532 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 13 | 0.356307 | `azmcp-workbooks-list` | ❌ |
| 14 | 0.352984 | `azmcp-workbooks-show` | ❌ |
| 15 | 0.346995 | `azmcp-quota-region-availability-list` | ❌ |
| 16 | 0.330484 | `azmcp-monitor-metrics-query` | ❌ |
| 17 | 0.329148 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 18 | 0.328853 | `azmcp-sql-db-show` | ❌ |
| 19 | 0.315577 | `azmcp-quota-usage-check` | ❌ |
| 20 | 0.293719 | `azmcp-monitor-workspace-log-query` | ❌ |

---

## Test 102

**Expected Tool:** `azmcp-loadtesting-testrun-list`  
**Prompt:** Get all the load test runs for the test with id <test-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.615980 | `azmcp-loadtesting-testresource-list` | ❌ |
| 2 | 0.607935 | `azmcp-loadtesting-test-get` | ❌ |
| 3 | 0.573169 | `azmcp-loadtesting-testrun-get` | ❌ |
| 4 | 0.568929 | `azmcp-loadtesting-testrun-list` | ✅ **EXPECTED** |
| 5 | 0.535209 | `azmcp-loadtesting-testresource-create` | ❌ |
| 6 | 0.492706 | `azmcp-loadtesting-testrun-create` | ❌ |
| 7 | 0.432135 | `azmcp-group-list` | ❌ |
| 8 | 0.418036 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.410933 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 10 | 0.406509 | `azmcp-loadtesting-test-create` | ❌ |
| 11 | 0.395917 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 12 | 0.392061 | `azmcp-loadtesting-testrun-update` | ❌ |
| 13 | 0.391157 | `azmcp-workbooks-list` | ❌ |
| 14 | 0.375709 | `azmcp-monitor-metrics-query` | ❌ |
| 15 | 0.356834 | `azmcp-quota-region-availability-list` | ❌ |
| 16 | 0.341387 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 17 | 0.340635 | `azmcp-workbooks-show` | ❌ |
| 18 | 0.329469 | `azmcp-sql-db-list` | ❌ |
| 19 | 0.328048 | `azmcp-redis-cluster-list` | ❌ |
| 20 | 0.326458 | `azmcp-sql-elastic-pool-list` | ❌ |

---

## Test 103

**Expected Tool:** `azmcp-loadtesting-testrun-update`  
**Prompt:** Update a test run display name as <display-name> for the id <testrun-id> for test <test-id> in the load testing resource <load-testing-resource> in resource group <resource-group>.  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.659812 | `azmcp-loadtesting-testrun-update` | ✅ **EXPECTED** |
| 2 | 0.509199 | `azmcp-loadtesting-testrun-create` | ❌ |
| 3 | 0.455629 | `azmcp-loadtesting-testrun-get` | ❌ |
| 4 | 0.446611 | `azmcp-loadtesting-test-get` | ❌ |
| 5 | 0.422036 | `azmcp-loadtesting-testresource-create` | ❌ |
| 6 | 0.399536 | `azmcp-loadtesting-test-create` | ❌ |
| 7 | 0.384654 | `azmcp-loadtesting-testresource-list` | ❌ |
| 8 | 0.383635 | `azmcp-loadtesting-testrun-list` | ❌ |
| 9 | 0.320124 | `azmcp-workbooks-update` | ❌ |
| 10 | 0.300023 | `azmcp-workbooks-create` | ❌ |
| 11 | 0.268172 | `azmcp-workbooks-show` | ❌ |
| 12 | 0.267137 | `azmcp-appconfig-kv-set` | ❌ |
| 13 | 0.259606 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 14 | 0.257473 | `azmcp-appconfig-kv-unlock` | ❌ |
| 15 | 0.255408 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 16 | 0.251946 | `azmcp-monitor-resource-log-query` | ❌ |
| 17 | 0.237372 | `azmcp-workbooks-delete` | ❌ |
| 18 | 0.233701 | `azmcp-monitor-metrics-query` | ❌ |
| 19 | 0.232572 | `azmcp-sql-db-show` | ❌ |
| 20 | 0.227194 | `azmcp-servicebus-topic-subscription-details` | ❌ |

---

## Test 104

**Expected Tool:** `azmcp-grafana-list`  
**Prompt:** List all Azure Managed Grafana in one subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.578892 | `azmcp-grafana-list` | ✅ **EXPECTED** |
| 2 | 0.544665 | `azmcp-search-service-list` | ❌ |
| 3 | 0.513028 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.505836 | `azmcp-kusto-cluster-list` | ❌ |
| 5 | 0.498077 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 6 | 0.497223 | `azmcp-functionapp-list` | ❌ |
| 7 | 0.493758 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.492724 | `azmcp-postgres-server-list` | ❌ |
| 9 | 0.492613 | `azmcp-subscription-list` | ❌ |
| 10 | 0.491740 | `azmcp-aks-cluster-list` | ❌ |
| 11 | 0.489846 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.482772 | `azmcp-redis-cache-list` | ❌ |
| 13 | 0.479611 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 14 | 0.452683 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 15 | 0.441315 | `azmcp-group-list` | ❌ |
| 16 | 0.440263 | `azmcp-kusto-database-list` | ❌ |
| 17 | 0.438192 | `azmcp-storage-account-list` | ❌ |
| 18 | 0.431917 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.422207 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.417927 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 105

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
| 6 | 0.302301 | `azmcp-aks-cluster-get` | ❌ |
| 7 | 0.295818 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.289354 | `azmcp-workbooks-show` | ❌ |
| 9 | 0.281400 | `azmcp-storage-account-details` | ❌ |
| 10 | 0.276841 | `azmcp-kusto-cluster-get` | ❌ |
| 11 | 0.274388 | `azmcp-redis-cache-list` | ❌ |
| 12 | 0.269243 | `azmcp-sql-db-show` | ❌ |
| 13 | 0.266271 | `azmcp-foundry-models-list` | ❌ |
| 14 | 0.264527 | `azmcp-search-index-describe` | ❌ |
| 15 | 0.252041 | `azmcp-loadtesting-test-get` | ❌ |
| 16 | 0.248779 | `azmcp-grafana-list` | ❌ |
| 17 | 0.246259 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 18 | 0.245820 | `azmcp-appconfig-kv-show` | ❌ |
| 19 | 0.235780 | `azmcp-loadtesting-testrun-list` | ❌ |
| 20 | 0.225581 | `azmcp-keyvault-certificate-get` | ❌ |

---

## Test 106

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure code generation best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.649047 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.612446 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.586907 | `azmcp-deploy-iac-rules-get` | ❌ |
| 4 | 0.531727 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 5 | 0.490235 | `azmcp-deploy-plan-get` | ❌ |
| 6 | 0.447777 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 7 | 0.435166 | `azmcp-extension-az` | ❌ |
| 8 | 0.372867 | `azmcp-extension-azd` | ❌ |
| 9 | 0.353355 | `azmcp-deploy-app-logs-get` | ❌ |
| 10 | 0.351664 | `azmcp-quota-usage-check` | ❌ |
| 11 | 0.345046 | `azmcp-bicepschema-get` | ❌ |
| 12 | 0.321323 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 13 | 0.312391 | `azmcp-quota-region-availability-list` | ❌ |
| 14 | 0.289967 | `azmcp-storage-account-details` | ❌ |
| 15 | 0.260239 | `azmcp-subscription-list` | ❌ |
| 16 | 0.258775 | `azmcp-search-service-list` | ❌ |
| 17 | 0.258646 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 18 | 0.253042 | `azmcp-storage-blob-upload` | ❌ |
| 19 | 0.251882 | `azmcp-storage-queue-message-send` | ❌ |
| 20 | 0.251118 | `azmcp-storage-blob-details` | ❌ |

---

## Test 107

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure deployment best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633068 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.543356 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.541091 | `azmcp-deploy-iac-rules-get` | ❌ |
| 4 | 0.516852 | `azmcp-deploy-plan-get` | ❌ |
| 5 | 0.516443 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 6 | 0.452068 | `azmcp-extension-az` | ❌ |
| 7 | 0.424017 | `azmcp-foundry-models-deployments-list` | ❌ |
| 8 | 0.409787 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 9 | 0.392171 | `azmcp-deploy-app-logs-get` | ❌ |
| 10 | 0.366111 | `azmcp-extension-azd` | ❌ |
| 11 | 0.358593 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 12 | 0.342487 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.306627 | `azmcp-quota-region-availability-list` | ❌ |
| 14 | 0.304620 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 15 | 0.292995 | `azmcp-storage-account-details` | ❌ |
| 16 | 0.280000 | `azmcp-subscription-list` | ❌ |
| 17 | 0.277791 | `azmcp-search-service-list` | ❌ |
| 18 | 0.267567 | `azmcp-storage-blob-details` | ❌ |
| 19 | 0.259012 | `azmcp-monitor-metrics-query` | ❌ |
| 20 | 0.257356 | `azmcp-workbooks-delete` | ❌ |

---

## Test 108

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.671381 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.575535 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.518643 | `azmcp-deploy-iac-rules-get` | ❌ |
| 4 | 0.465572 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 5 | 0.455995 | `azmcp-extension-az` | ❌ |
| 6 | 0.430630 | `azmcp-deploy-plan-get` | ❌ |
| 7 | 0.399433 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 8 | 0.384057 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 9 | 0.380286 | `azmcp-deploy-app-logs-get` | ❌ |
| 10 | 0.375863 | `azmcp-quota-usage-check` | ❌ |
| 11 | 0.362465 | `azmcp-extension-azd` | ❌ |
| 12 | 0.329342 | `azmcp-quota-region-availability-list` | ❌ |
| 13 | 0.329314 | `azmcp-storage-account-details` | ❌ |
| 14 | 0.319861 | `azmcp-bicepschema-get` | ❌ |
| 15 | 0.316805 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 16 | 0.302276 | `azmcp-subscription-list` | ❌ |
| 17 | 0.293300 | `azmcp-storage-blob-details` | ❌ |
| 18 | 0.290182 | `azmcp-monitor-metrics-query` | ❌ |
| 19 | 0.287118 | `azmcp-search-service-list` | ❌ |
| 20 | 0.275399 | `azmcp-workbooks-delete` | ❌ |

---

## Test 109

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure Functions code generation best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.576108 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.553932 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.522998 | `azmcp-deploy-iac-rules-get` | ❌ |
| 4 | 0.493998 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 5 | 0.445382 | `azmcp-deploy-plan-get` | ❌ |
| 6 | 0.416803 | `azmcp-extension-az` | ❌ |
| 7 | 0.400447 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 8 | 0.393460 | `azmcp-functionapp-list` | ❌ |
| 9 | 0.368157 | `azmcp-deploy-app-logs-get` | ❌ |
| 10 | 0.348603 | `azmcp-extension-azd` | ❌ |
| 11 | 0.317494 | `azmcp-quota-usage-check` | ❌ |
| 12 | 0.278941 | `azmcp-quota-region-availability-list` | ❌ |
| 13 | 0.269946 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 14 | 0.253379 | `azmcp-storage-blob-upload` | ❌ |
| 15 | 0.241692 | `azmcp-storage-blob-details` | ❌ |
| 16 | 0.240062 | `azmcp-storage-queue-message-send` | ❌ |
| 17 | 0.238484 | `azmcp-storage-account-details` | ❌ |
| 18 | 0.219838 | `azmcp-storage-blob-container-create` | ❌ |
| 19 | 0.219607 | `azmcp-subscription-list` | ❌ |
| 20 | 0.212761 | `azmcp-search-service-list` | ❌ |

---

## Test 110

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure Functions deployment best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.553170 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.497350 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 3 | 0.495659 | `azmcp-deploy-iac-rules-get` | ❌ |
| 4 | 0.487769 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 5 | 0.474511 | `azmcp-deploy-plan-get` | ❌ |
| 6 | 0.439182 | `azmcp-foundry-models-deployments-list` | ❌ |
| 7 | 0.431008 | `azmcp-extension-az` | ❌ |
| 8 | 0.424056 | `azmcp-functionapp-list` | ❌ |
| 9 | 0.412001 | `azmcp-deploy-app-logs-get` | ❌ |
| 10 | 0.377790 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 11 | 0.321678 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 12 | 0.317931 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.277946 | `azmcp-quota-region-availability-list` | ❌ |
| 14 | 0.265176 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 15 | 0.263968 | `azmcp-storage-blob-upload` | ❌ |
| 16 | 0.256807 | `azmcp-storage-queue-message-send` | ❌ |
| 17 | 0.254398 | `azmcp-storage-blob-details` | ❌ |
| 18 | 0.246787 | `azmcp-storage-account-details` | ❌ |
| 19 | 0.244786 | `azmcp-search-service-list` | ❌ |
| 20 | 0.242229 | `azmcp-subscription-list` | ❌ |

---

## Test 111

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure Functions best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.586538 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.521120 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.487322 | `azmcp-deploy-iac-rules-get` | ❌ |
| 4 | 0.458060 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 5 | 0.444295 | `azmcp-extension-az` | ❌ |
| 6 | 0.433208 | `azmcp-functionapp-list` | ❌ |
| 7 | 0.395940 | `azmcp-deploy-app-logs-get` | ❌ |
| 8 | 0.394214 | `azmcp-deploy-plan-get` | ❌ |
| 9 | 0.363596 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 10 | 0.348542 | `azmcp-extension-azd` | ❌ |
| 11 | 0.332015 | `azmcp-quota-usage-check` | ❌ |
| 12 | 0.328838 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 13 | 0.284215 | `azmcp-quota-region-availability-list` | ❌ |
| 14 | 0.274389 | `azmcp-storage-queue-message-send` | ❌ |
| 15 | 0.269853 | `azmcp-storage-blob-details` | ❌ |
| 16 | 0.267667 | `azmcp-storage-blob-upload` | ❌ |
| 17 | 0.263108 | `azmcp-storage-account-details` | ❌ |
| 18 | 0.261619 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 19 | 0.261593 | `azmcp-monitor-metrics-query` | ❌ |
| 20 | 0.247801 | `azmcp-subscription-list` | ❌ |

---

## Test 112

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Get the latest Azure Static Web Apps best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.577758 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.526390 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.505123 | `azmcp-deploy-iac-rules-get` | ❌ |
| 4 | 0.483705 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 5 | 0.405993 | `azmcp-extension-az` | ❌ |
| 6 | 0.405143 | `azmcp-deploy-app-logs-get` | ❌ |
| 7 | 0.401209 | `azmcp-deploy-plan-get` | ❌ |
| 8 | 0.398226 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 9 | 0.355985 | `azmcp-extension-azd` | ❌ |
| 10 | 0.317161 | `azmcp-functionapp-list` | ❌ |
| 11 | 0.312174 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 12 | 0.283198 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.263368 | `azmcp-storage-account-details` | ❌ |
| 14 | 0.259417 | `azmcp-storage-blob-upload` | ❌ |
| 15 | 0.256951 | `azmcp-storage-blob-details` | ❌ |
| 16 | 0.249439 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 17 | 0.240424 | `azmcp-storage-account-create` | ❌ |
| 18 | 0.237289 | `azmcp-quota-region-availability-list` | ❌ |
| 19 | 0.223337 | `azmcp-search-service-list` | ❌ |
| 20 | 0.221706 | `azmcp-storage-blob-container-create` | ❌ |

---

## Test 113

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** What are azure function best practices?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.553494 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.487728 | `azmcp-extension-az` | ❌ |
| 3 | 0.478550 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 4 | 0.472112 | `azmcp-deploy-iac-rules-get` | ❌ |
| 5 | 0.433134 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 6 | 0.394171 | `azmcp-functionapp-list` | ❌ |
| 7 | 0.368831 | `azmcp-deploy-plan-get` | ❌ |
| 8 | 0.358703 | `azmcp-deploy-app-logs-get` | ❌ |
| 9 | 0.337024 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 10 | 0.323967 | `azmcp-extension-azd` | ❌ |
| 11 | 0.293848 | `azmcp-quota-usage-check` | ❌ |
| 12 | 0.280178 | `azmcp-storage-queue-message-send` | ❌ |
| 13 | 0.261465 | `azmcp-storage-blob-upload` | ❌ |
| 14 | 0.249260 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 15 | 0.248119 | `azmcp-monitor-resource-log-query` | ❌ |
| 16 | 0.248003 | `azmcp-workbooks-delete` | ❌ |
| 17 | 0.243935 | `azmcp-storage-blob-details` | ❌ |
| 18 | 0.223124 | `azmcp-storage-account-details` | ❌ |
| 19 | 0.222800 | `azmcp-monitor-metrics-query` | ❌ |
| 20 | 0.216551 | `azmcp-storage-account-create` | ❌ |

---

## Test 114

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Create the plan for creating a simple HTTP-triggered function app in javascript that returns a random compliment from a predefined list in a JSON response. And deploy it to azure eventually. But don't create any code until I confirm.  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.429170 | `azmcp-deploy-plan-get` | ❌ |
| 2 | 0.408233 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 3 | 0.406619 | `azmcp-extension-az` | ❌ |
| 4 | 0.360756 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 5 | 0.352369 | `azmcp-deploy-iac-rules-get` | ❌ |
| 6 | 0.345059 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 7 | 0.336439 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 8 | 0.319970 | `azmcp-loadtesting-test-create` | ❌ |
| 9 | 0.299148 | `azmcp-deploy-app-logs-get` | ❌ |
| 10 | 0.289876 | `azmcp-functionapp-list` | ❌ |
| 11 | 0.232320 | `azmcp-quota-usage-check` | ❌ |
| 12 | 0.218912 | `azmcp-workbooks-create` | ❌ |
| 13 | 0.217972 | `azmcp-storage-blob-upload` | ❌ |
| 14 | 0.213599 | `azmcp-storage-blob-details` | ❌ |
| 15 | 0.210908 | `azmcp-quota-region-availability-list` | ❌ |
| 16 | 0.201280 | `azmcp-storage-blob-container-create` | ❌ |
| 17 | 0.200324 | `azmcp-storage-account-create` | ❌ |
| 18 | 0.190533 | `azmcp-storage-queue-message-send` | ❌ |
| 19 | 0.190147 | `azmcp-storage-account-details` | ❌ |
| 20 | 0.174894 | `azmcp-subscription-list` | ❌ |

---

## Test 115

**Expected Tool:** `azmcp-bestpractices-get`  
**Prompt:** Create the plan for creating a to-do list app. And deploy it to azure as a container app. But don't create any code until I confirm.  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.497276 | `azmcp-deploy-plan-get` | ❌ |
| 2 | 0.493182 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 3 | 0.408474 | `azmcp-extension-az` | ❌ |
| 4 | 0.405146 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 5 | 0.395623 | `azmcp-deploy-iac-rules-get` | ❌ |
| 6 | 0.367259 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 7 | 0.348171 | `azmcp-deploy-app-logs-get` | ❌ |
| 8 | 0.333124 | `azmcp-bestpractices-get` | ✅ **EXPECTED** |
| 9 | 0.304256 | `azmcp-extension-azd` | ❌ |
| 10 | 0.300092 | `azmcp-loadtesting-test-create` | ❌ |
| 11 | 0.243575 | `azmcp-quota-usage-check` | ❌ |
| 12 | 0.230519 | `azmcp-storage-blob-container-create` | ❌ |
| 13 | 0.228431 | `azmcp-storage-blob-container-list` | ❌ |
| 14 | 0.226270 | `azmcp-storage-account-create` | ❌ |
| 15 | 0.218621 | `azmcp-quota-region-availability-list` | ❌ |
| 16 | 0.218293 | `azmcp-storage-blob-list` | ❌ |
| 17 | 0.209213 | `azmcp-workbooks-create` | ❌ |
| 18 | 0.207259 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.195211 | `azmcp-storage-blob-details` | ❌ |
| 20 | 0.191395 | `azmcp-storage-blob-upload` | ❌ |

---

## Test 116

**Expected Tool:** `azmcp-monitor-healthmodels-entity-gethealth`  
**Prompt:** Show me the health status of entity <entity_id> in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.477138 | `azmcp-monitor-healthmodels-entity-gethealth` | ✅ **EXPECTED** |
| 2 | 0.472094 | `azmcp-monitor-workspace-list` | ❌ |
| 3 | 0.468204 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.464012 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.459778 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 6 | 0.436971 | `azmcp-deploy-app-logs-get` | ❌ |
| 7 | 0.418755 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 8 | 0.413357 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.404140 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.380121 | `azmcp-grafana-list` | ❌ |
| 11 | 0.358432 | `azmcp-monitor-metrics-query` | ❌ |
| 12 | 0.339599 | `azmcp-aks-cluster-get` | ❌ |
| 13 | 0.337603 | `azmcp-loadtesting-testrun-get` | ❌ |
| 14 | 0.316587 | `azmcp-workbooks-show` | ❌ |
| 15 | 0.314731 | `azmcp-quota-usage-check` | ❌ |
| 16 | 0.305738 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 17 | 0.297767 | `azmcp-aks-cluster-list` | ❌ |
| 18 | 0.279273 | `azmcp-kusto-query` | ❌ |
| 19 | 0.276713 | `azmcp-loadtesting-test-get` | ❌ |
| 20 | 0.269767 | `azmcp-kusto-cluster-get` | ❌ |

---

## Test 117

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** Get metric definitions for <resource_type> <resource_name> from the namespace  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.592640 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 2 | 0.424141 | `azmcp-monitor-metrics-query` | ❌ |
| 3 | 0.332356 | `azmcp-monitor-table-type-list` | ❌ |
| 4 | 0.315310 | `azmcp-servicebus-topic-details` | ❌ |
| 5 | 0.311108 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 6 | 0.305464 | `azmcp-servicebus-queue-details` | ❌ |
| 7 | 0.304735 | `azmcp-grafana-list` | ❌ |
| 8 | 0.303453 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.297379 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 10 | 0.294124 | `azmcp-quota-region-availability-list` | ❌ |
| 11 | 0.293189 | `azmcp-search-index-describe` | ❌ |
| 12 | 0.284519 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 13 | 0.283102 | `azmcp-quota-usage-check` | ❌ |
| 14 | 0.277663 | `azmcp-loadtesting-test-get` | ❌ |
| 15 | 0.277535 | `azmcp-kusto-table-schema` | ❌ |
| 16 | 0.269984 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 17 | 0.249124 | `azmcp-aks-cluster-get` | ❌ |
| 18 | 0.248987 | `azmcp-bicepschema-get` | ❌ |
| 19 | 0.234617 | `azmcp-loadtesting-testresource-list` | ❌ |
| 20 | 0.227542 | `azmcp-appconfig-kv-list` | ❌ |

---

## Test 118

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** Show me all available metrics and their definitions for storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.611603 | `azmcp-storage-account-details` | ❌ |
| 2 | 0.587736 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 3 | 0.556726 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.542805 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.541028 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.539767 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.519808 | `azmcp-storage-blob-list` | ❌ |
| 8 | 0.459829 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.459179 | `azmcp-storage-blob-details` | ❌ |
| 10 | 0.431109 | `azmcp-appconfig-kv-show` | ❌ |
| 11 | 0.417098 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 12 | 0.414488 | `azmcp-cosmos-database-container-list` | ❌ |
| 13 | 0.406419 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.403921 | `azmcp-quota-usage-check` | ❌ |
| 15 | 0.397526 | `azmcp-appconfig-kv-list` | ❌ |
| 16 | 0.390422 | `azmcp-cosmos-database-list` | ❌ |
| 17 | 0.378187 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.359476 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.357647 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 20 | 0.343966 | `azmcp-keyvault-secret-list` | ❌ |

---

## Test 119

**Expected Tool:** `azmcp-monitor-metrics-definitions`  
**Prompt:** What metric definitions are available for the Application Insights resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633173 | `azmcp-monitor-metrics-definitions` | ✅ **EXPECTED** |
| 2 | 0.495513 | `azmcp-monitor-metrics-query` | ❌ |
| 3 | 0.380252 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 4 | 0.370848 | `azmcp-monitor-table-type-list` | ❌ |
| 5 | 0.353264 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 6 | 0.344326 | `azmcp-quota-usage-check` | ❌ |
| 7 | 0.337952 | `azmcp-monitor-resource-log-query` | ❌ |
| 8 | 0.329534 | `azmcp-loadtesting-testresource-list` | ❌ |
| 9 | 0.324002 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 10 | 0.308315 | `azmcp-monitor-workspace-log-query` | ❌ |
| 11 | 0.303296 | `azmcp-search-index-list` | ❌ |
| 12 | 0.302823 | `azmcp-monitor-table-list` | ❌ |
| 13 | 0.301966 | `azmcp-workbooks-show` | ❌ |
| 14 | 0.299167 | `azmcp-loadtesting-testrun-get` | ❌ |
| 15 | 0.291260 | `azmcp-deploy-app-logs-get` | ❌ |
| 16 | 0.286293 | `azmcp-loadtesting-testresource-create` | ❌ |
| 17 | 0.286161 | `azmcp-loadtesting-test-get` | ❌ |
| 18 | 0.284437 | `azmcp-grafana-list` | ❌ |
| 19 | 0.279929 | `azmcp-foundry-models-deployments-list` | ❌ |
| 20 | 0.278426 | `azmcp-loadtesting-test-create` | ❌ |

---

## Test 120

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Analyze the performance trends and response times for Application Insights resource <resource_name> over the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.555377 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.445153 | `azmcp-monitor-resource-log-query` | ❌ |
| 3 | 0.439684 | `azmcp-loadtesting-testrun-get` | ❌ |
| 4 | 0.417973 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 5 | 0.409107 | `azmcp-deploy-app-logs-get` | ❌ |
| 6 | 0.404582 | `azmcp-monitor-workspace-log-query` | ❌ |
| 7 | 0.388205 | `azmcp-quota-usage-check` | ❌ |
| 8 | 0.380075 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 9 | 0.341791 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 10 | 0.340642 | `azmcp-loadtesting-testrun-list` | ❌ |
| 11 | 0.339771 | `azmcp-loadtesting-testresource-list` | ❌ |
| 12 | 0.335430 | `azmcp-monitor-metrics-definitions` | ❌ |
| 13 | 0.329460 | `azmcp-loadtesting-testresource-create` | ❌ |
| 14 | 0.328475 | `azmcp-loadtesting-test-get` | ❌ |
| 15 | 0.326802 | `azmcp-workbooks-show` | ❌ |
| 16 | 0.326398 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.314675 | `azmcp-extension-azqr` | ❌ |
| 18 | 0.291424 | `azmcp-search-index-list` | ❌ |
| 19 | 0.289449 | `azmcp-workbooks-delete` | ❌ |
| 20 | 0.286251 | `azmcp-storage-blob-details` | ❌ |

---

## Test 121

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Check the availability metrics for my Application Insights resource <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.557897 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.504243 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 3 | 0.460554 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 4 | 0.455880 | `azmcp-quota-usage-check` | ❌ |
| 5 | 0.438214 | `azmcp-monitor-metrics-definitions` | ❌ |
| 6 | 0.389660 | `azmcp-monitor-resource-log-query` | ❌ |
| 7 | 0.372997 | `azmcp-deploy-app-logs-get` | ❌ |
| 8 | 0.356342 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.341482 | `azmcp-loadtesting-testrun-get` | ❌ |
| 10 | 0.339415 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 11 | 0.326932 | `azmcp-loadtesting-testresource-list` | ❌ |
| 12 | 0.311725 | `azmcp-search-index-list` | ❌ |
| 13 | 0.303889 | `azmcp-quota-region-availability-list` | ❌ |
| 14 | 0.302289 | `azmcp-loadtesting-test-get` | ❌ |
| 15 | 0.295242 | `azmcp-functionapp-list` | ❌ |
| 16 | 0.292444 | `azmcp-search-service-list` | ❌ |
| 17 | 0.288406 | `azmcp-loadtesting-testresource-create` | ❌ |
| 18 | 0.285957 | `azmcp-grafana-list` | ❌ |
| 19 | 0.285902 | `azmcp-monitor-table-type-list` | ❌ |
| 20 | 0.285500 | `azmcp-deploy-architecture-diagram-generate` | ❌ |

---

## Test 122

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Get the <aggregation_type> <metric_name> metric for <resource_type> <resource_name> over the last <time_period> with intervals  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.461204 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.389966 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.306354 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 4 | 0.300071 | `azmcp-monitor-resource-log-query` | ❌ |
| 5 | 0.298397 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 6 | 0.279641 | `azmcp-monitor-workspace-log-query` | ❌ |
| 7 | 0.275454 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.267700 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 9 | 0.263765 | `azmcp-quota-usage-check` | ❌ |
| 10 | 0.263354 | `azmcp-quota-region-availability-list` | ❌ |
| 11 | 0.259215 | `azmcp-grafana-list` | ❌ |
| 12 | 0.249865 | `azmcp-loadtesting-test-get` | ❌ |
| 13 | 0.249644 | `azmcp-storage-blob-container-details` | ❌ |
| 14 | 0.249393 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 15 | 0.248772 | `azmcp-loadtesting-testresource-list` | ❌ |
| 16 | 0.245687 | `azmcp-workbooks-show` | ❌ |
| 17 | 0.244688 | `azmcp-loadtesting-testrun-get` | ❌ |
| 18 | 0.235613 | `azmcp-kusto-table-schema` | ❌ |
| 19 | 0.224290 | `azmcp-loadtesting-testrun-list` | ❌ |
| 20 | 0.212987 | `azmcp-aks-cluster-get` | ❌ |

---

## Test 123

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Investigate error rates and failed requests for Application Insights resource <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.492129 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.413849 | `azmcp-monitor-resource-log-query` | ❌ |
| 3 | 0.410915 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 4 | 0.398992 | `azmcp-deploy-app-logs-get` | ❌ |
| 5 | 0.397292 | `azmcp-quota-usage-check` | ❌ |
| 6 | 0.368286 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.359279 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 8 | 0.354927 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.316299 | `azmcp-loadtesting-testresource-list` | ❌ |
| 10 | 0.308735 | `azmcp-monitor-metrics-definitions` | ❌ |
| 11 | 0.295922 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 12 | 0.293316 | `azmcp-loadtesting-testresource-create` | ❌ |
| 13 | 0.287546 | `azmcp-quota-region-availability-list` | ❌ |
| 14 | 0.287128 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 15 | 0.284502 | `azmcp-functionapp-list` | ❌ |
| 16 | 0.283520 | `azmcp-extension-azqr` | ❌ |
| 17 | 0.280065 | `azmcp-search-index-list` | ❌ |
| 18 | 0.274541 | `azmcp-loadtesting-test-get` | ❌ |
| 19 | 0.272677 | `azmcp-search-service-list` | ❌ |
| 20 | 0.271321 | `azmcp-workbooks-show` | ❌ |

---

## Test 124

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** Query the <metric_name> metric for <resource_type> <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.525669 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.384579 | `azmcp-monitor-metrics-definitions` | ❌ |
| 3 | 0.374009 | `azmcp-monitor-resource-log-query` | ❌ |
| 4 | 0.362133 | `azmcp-monitor-workspace-log-query` | ❌ |
| 5 | 0.299551 | `azmcp-quota-usage-check` | ❌ |
| 6 | 0.293188 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.287754 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 8 | 0.281034 | `azmcp-search-index-query` | ❌ |
| 9 | 0.272383 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.267141 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 11 | 0.264243 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 12 | 0.262808 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 13 | 0.262023 | `azmcp-grafana-list` | ❌ |
| 14 | 0.256915 | `azmcp-loadtesting-testrun-list` | ❌ |
| 15 | 0.252346 | `azmcp-servicebus-queue-details` | ❌ |
| 16 | 0.250603 | `azmcp-postgres-server-param-get` | ❌ |
| 17 | 0.246223 | `azmcp-loadtesting-test-get` | ❌ |
| 18 | 0.244197 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 19 | 0.239100 | `azmcp-kusto-query` | ❌ |
| 20 | 0.235845 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 125

**Expected Tool:** `azmcp-monitor-metrics-query`  
**Prompt:** What's the request per second rate for my Application Insights resource <resource_name> over the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.480140 | `azmcp-monitor-metrics-query` | ✅ **EXPECTED** |
| 2 | 0.378128 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 3 | 0.363412 | `azmcp-quota-usage-check` | ❌ |
| 4 | 0.348732 | `azmcp-monitor-resource-log-query` | ❌ |
| 5 | 0.341334 | `azmcp-monitor-workspace-log-query` | ❌ |
| 6 | 0.331215 | `azmcp-loadtesting-testresource-list` | ❌ |
| 7 | 0.330074 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 8 | 0.328838 | `azmcp-monitor-metrics-definitions` | ❌ |
| 9 | 0.327098 | `azmcp-loadtesting-testrun-get` | ❌ |
| 10 | 0.319343 | `azmcp-loadtesting-testresource-create` | ❌ |
| 11 | 0.292195 | `azmcp-deploy-app-logs-get` | ❌ |
| 12 | 0.278491 | `azmcp-workbooks-show` | ❌ |
| 13 | 0.277129 | `azmcp-loadtesting-test-get` | ❌ |
| 14 | 0.272179 | `azmcp-functionapp-list` | ❌ |
| 15 | 0.266918 | `azmcp-search-index-list` | ❌ |
| 16 | 0.262764 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.260709 | `azmcp-foundry-models-deployments-list` | ❌ |
| 18 | 0.258891 | `azmcp-extension-azqr` | ❌ |
| 19 | 0.254630 | `azmcp-search-service-list` | ❌ |
| 20 | 0.246652 | `azmcp-storage-queue-message-send` | ❌ |

---

## Test 126

**Expected Tool:** `azmcp-monitor-resource-log-query`  
**Prompt:** Show me the logs for the past hour for the resource <resource_name> in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584906 | `azmcp-monitor-workspace-log-query` | ❌ |
| 2 | 0.577600 | `azmcp-monitor-resource-log-query` | ✅ **EXPECTED** |
| 3 | 0.472064 | `azmcp-deploy-app-logs-get` | ❌ |
| 4 | 0.469703 | `azmcp-monitor-metrics-query` | ❌ |
| 5 | 0.443468 | `azmcp-monitor-workspace-list` | ❌ |
| 6 | 0.442971 | `azmcp-monitor-table-list` | ❌ |
| 7 | 0.392377 | `azmcp-monitor-table-type-list` | ❌ |
| 8 | 0.390022 | `azmcp-grafana-list` | ❌ |
| 9 | 0.361118 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 10 | 0.359065 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 11 | 0.352821 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 12 | 0.345341 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.333531 | `azmcp-workbooks-list` | ❌ |
| 14 | 0.320807 | `azmcp-loadtesting-testrun-get` | ❌ |
| 15 | 0.307804 | `azmcp-aks-cluster-get` | ❌ |
| 16 | 0.302745 | `azmcp-loadtesting-testresource-list` | ❌ |
| 17 | 0.299952 | `azmcp-loadtesting-test-get` | ❌ |
| 18 | 0.297077 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.296311 | `azmcp-loadtesting-testrun-list` | ❌ |
| 20 | 0.291854 | `azmcp-kusto-query` | ❌ |

---

## Test 127

**Expected Tool:** `azmcp-monitor-table-list`  
**Prompt:** List all tables in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.851075 | `azmcp-monitor-table-list` | ✅ **EXPECTED** |
| 2 | 0.725738 | `azmcp-monitor-table-type-list` | ❌ |
| 3 | 0.620445 | `azmcp-monitor-workspace-list` | ❌ |
| 4 | 0.586691 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.510996 | `azmcp-kusto-table-list` | ❌ |
| 6 | 0.502075 | `azmcp-grafana-list` | ❌ |
| 7 | 0.488557 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.436216 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.420394 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.419869 | `azmcp-kusto-database-list` | ❌ |
| 11 | 0.409449 | `azmcp-monitor-resource-log-query` | ❌ |
| 12 | 0.405953 | `azmcp-search-index-list` | ❌ |
| 13 | 0.400092 | `azmcp-workbooks-list` | ❌ |
| 14 | 0.397440 | `azmcp-kusto-table-schema` | ❌ |
| 15 | 0.378748 | `azmcp-sql-db-list` | ❌ |
| 16 | 0.375176 | `azmcp-deploy-app-logs-get` | ❌ |
| 17 | 0.374930 | `azmcp-cosmos-database-container-list` | ❌ |
| 18 | 0.366099 | `azmcp-kusto-sample` | ❌ |
| 19 | 0.365781 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.365538 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 128

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
| 6 | 0.466513 | `azmcp-kusto-table-list` | ❌ |
| 7 | 0.441635 | `azmcp-monitor-workspace-log-query` | ❌ |
| 8 | 0.427408 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.413450 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.411650 | `azmcp-kusto-table-schema` | ❌ |
| 11 | 0.403863 | `azmcp-deploy-app-logs-get` | ❌ |
| 12 | 0.376474 | `azmcp-kusto-sample` | ❌ |
| 13 | 0.376368 | `azmcp-kusto-database-list` | ❌ |
| 14 | 0.373298 | `azmcp-workbooks-list` | ❌ |
| 15 | 0.370624 | `azmcp-cosmos-database-list` | ❌ |
| 16 | 0.370200 | `azmcp-search-index-list` | ❌ |
| 17 | 0.347853 | `azmcp-cosmos-database-container-list` | ❌ |
| 18 | 0.339950 | `azmcp-sql-db-list` | ❌ |
| 19 | 0.332323 | `azmcp-kusto-cluster-list` | ❌ |
| 20 | 0.331919 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 129

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
| 6 | 0.447338 | `azmcp-kusto-table-list` | ❌ |
| 7 | 0.418517 | `azmcp-postgres-table-list` | ❌ |
| 8 | 0.416342 | `azmcp-kusto-table-schema` | ❌ |
| 9 | 0.404192 | `azmcp-monitor-metrics-definitions` | ❌ |
| 10 | 0.394213 | `azmcp-monitor-workspace-log-query` | ❌ |
| 11 | 0.380581 | `azmcp-kusto-sample` | ❌ |
| 12 | 0.371871 | `azmcp-monitor-resource-log-query` | ❌ |
| 13 | 0.369889 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.367798 | `azmcp-workbooks-list` | ❌ |
| 15 | 0.361828 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.356649 | `azmcp-search-index-list` | ❌ |
| 17 | 0.354757 | `azmcp-kusto-cluster-list` | ❌ |
| 18 | 0.354124 | `azmcp-quota-region-availability-list` | ❌ |
| 19 | 0.347919 | `azmcp-deploy-app-logs-get` | ❌ |
| 20 | 0.346304 | `azmcp-foundry-models-list` | ❌ |

---

## Test 130

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
| 6 | 0.427943 | `azmcp-kusto-table-schema` | ❌ |
| 7 | 0.421384 | `azmcp-kusto-table-list` | ❌ |
| 8 | 0.416739 | `azmcp-monitor-workspace-log-query` | ❌ |
| 9 | 0.391308 | `azmcp-kusto-sample` | ❌ |
| 10 | 0.384124 | `azmcp-monitor-resource-log-query` | ❌ |
| 11 | 0.376246 | `azmcp-monitor-metrics-definitions` | ❌ |
| 12 | 0.372991 | `azmcp-postgres-table-list` | ❌ |
| 13 | 0.367591 | `azmcp-deploy-app-logs-get` | ❌ |
| 14 | 0.352574 | `azmcp-workbooks-list` | ❌ |
| 15 | 0.348357 | `azmcp-cosmos-database-list` | ❌ |
| 16 | 0.344675 | `azmcp-search-index-list` | ❌ |
| 17 | 0.340942 | `azmcp-postgres-table-schema-get` | ❌ |
| 18 | 0.340101 | `azmcp-foundry-models-list` | ❌ |
| 19 | 0.339804 | `azmcp-kusto-cluster-list` | ❌ |
| 20 | 0.338467 | `azmcp-kusto-database-list` | ❌ |

---

## Test 131

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
| 8 | 0.513663 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.502582 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.500768 | `azmcp-workbooks-list` | ❌ |
| 11 | 0.494595 | `azmcp-group-list` | ❌ |
| 12 | 0.493918 | `azmcp-subscription-list` | ❌ |
| 13 | 0.487595 | `azmcp-functionapp-list` | ❌ |
| 14 | 0.487565 | `azmcp-storage-table-list` | ❌ |
| 15 | 0.471856 | `azmcp-redis-cluster-list` | ❌ |
| 16 | 0.470266 | `azmcp-postgres-server-list` | ❌ |
| 17 | 0.467655 | `azmcp-appconfig-account-list` | ❌ |
| 18 | 0.466729 | `azmcp-acr-registry-list` | ❌ |
| 19 | 0.448140 | `azmcp-kusto-database-list` | ❌ |
| 20 | 0.444214 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 132

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
| 6 | 0.459841 | `azmcp-deploy-app-logs-get` | ❌ |
| 7 | 0.398741 | `azmcp-search-service-list` | ❌ |
| 8 | 0.386422 | `azmcp-workbooks-list` | ❌ |
| 9 | 0.384081 | `azmcp-search-index-list` | ❌ |
| 10 | 0.383596 | `azmcp-aks-cluster-list` | ❌ |
| 11 | 0.381606 | `azmcp-monitor-resource-log-query` | ❌ |
| 12 | 0.379597 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.376990 | `azmcp-storage-blob-container-list` | ❌ |
| 14 | 0.373786 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.358029 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.354811 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 17 | 0.354276 | `azmcp-cosmos-database-list` | ❌ |
| 18 | 0.352756 | `azmcp-acr-registry-list` | ❌ |
| 19 | 0.350239 | `azmcp-loadtesting-testresource-list` | ❌ |
| 20 | 0.344457 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 133

**Expected Tool:** `azmcp-monitor-workspace-list`  
**Prompt:** Show me the Log Analytics workspaces in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.732908 | `azmcp-monitor-workspace-list` | ✅ **EXPECTED** |
| 2 | 0.601457 | `azmcp-grafana-list` | ❌ |
| 3 | 0.580175 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.521249 | `azmcp-monitor-table-type-list` | ❌ |
| 5 | 0.500464 | `azmcp-search-service-list` | ❌ |
| 6 | 0.453641 | `azmcp-deploy-app-logs-get` | ❌ |
| 7 | 0.449822 | `azmcp-monitor-workspace-log-query` | ❌ |
| 8 | 0.439259 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.435443 | `azmcp-workbooks-list` | ❌ |
| 10 | 0.428911 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.427109 | `azmcp-aks-cluster-list` | ❌ |
| 12 | 0.422824 | `azmcp-subscription-list` | ❌ |
| 13 | 0.422405 | `azmcp-loadtesting-testresource-list` | ❌ |
| 14 | 0.418619 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.413108 | `azmcp-storage-table-list` | ❌ |
| 16 | 0.411627 | `azmcp-acr-registry-list` | ❌ |
| 17 | 0.411413 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 18 | 0.404242 | `azmcp-group-list` | ❌ |
| 19 | 0.395577 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.390209 | `azmcp-functionapp-list` | ❌ |

---

## Test 134

**Expected Tool:** `azmcp-monitor-workspace-log-query`  
**Prompt:** Show me the logs for the past hour in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.581663 | `azmcp-monitor-workspace-log-query` | ✅ **EXPECTED** |
| 2 | 0.492885 | `azmcp-monitor-resource-log-query` | ❌ |
| 3 | 0.485984 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.484159 | `azmcp-deploy-app-logs-get` | ❌ |
| 5 | 0.483323 | `azmcp-monitor-workspace-list` | ❌ |
| 6 | 0.427241 | `azmcp-monitor-table-type-list` | ❌ |
| 7 | 0.374939 | `azmcp-monitor-metrics-query` | ❌ |
| 8 | 0.365704 | `azmcp-grafana-list` | ❌ |
| 9 | 0.322408 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 10 | 0.322001 | `azmcp-search-index-list` | ❌ |
| 11 | 0.318833 | `azmcp-workbooks-delete` | ❌ |
| 12 | 0.309810 | `azmcp-loadtesting-testrun-get` | ❌ |
| 13 | 0.301564 | `azmcp-quota-usage-check` | ❌ |
| 14 | 0.292300 | `azmcp-extension-az` | ❌ |
| 15 | 0.291669 | `azmcp-kusto-query` | ❌ |
| 16 | 0.288698 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.287228 | `azmcp-aks-cluster-get` | ❌ |
| 18 | 0.284162 | `azmcp-loadtesting-testrun-list` | ❌ |
| 19 | 0.283294 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 20 | 0.276315 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 135

**Expected Tool:** `azmcp-datadog-monitoredresources-list`  
**Prompt:** List all monitored resources in the Datadog resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.668827 | `azmcp-datadog-monitoredresources-list` | ✅ **EXPECTED** |
| 2 | 0.434790 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.413173 | `azmcp-monitor-metrics-query` | ❌ |
| 4 | 0.408716 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.401731 | `azmcp-grafana-list` | ❌ |
| 6 | 0.393318 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 7 | 0.386685 | `azmcp-monitor-metrics-definitions` | ❌ |
| 8 | 0.369805 | `azmcp-redis-cluster-database-list` | ❌ |
| 9 | 0.364360 | `azmcp-workbooks-list` | ❌ |
| 10 | 0.355415 | `azmcp-loadtesting-testresource-list` | ❌ |
| 11 | 0.345409 | `azmcp-postgres-database-list` | ❌ |
| 12 | 0.345298 | `azmcp-group-list` | ❌ |
| 13 | 0.330769 | `azmcp-postgres-table-list` | ❌ |
| 14 | 0.327205 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.318192 | `azmcp-sql-db-list` | ❌ |
| 16 | 0.317478 | `azmcp-quota-usage-check` | ❌ |
| 17 | 0.304097 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.302405 | `azmcp-acr-registry-repository-list` | ❌ |
| 19 | 0.296544 | `azmcp-cosmos-database-container-list` | ❌ |
| 20 | 0.294593 | `azmcp-kusto-database-list` | ❌ |

---

## Test 136

**Expected Tool:** `azmcp-datadog-monitoredresources-list`  
**Prompt:** Show me the monitored resources in the Datadog resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.624066 | `azmcp-datadog-monitoredresources-list` | ✅ **EXPECTED** |
| 2 | 0.443481 | `azmcp-monitor-metrics-query` | ❌ |
| 3 | 0.393248 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.374139 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.371017 | `azmcp-grafana-list` | ❌ |
| 6 | 0.370681 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 7 | 0.359274 | `azmcp-monitor-metrics-definitions` | ❌ |
| 8 | 0.350656 | `azmcp-quota-usage-check` | ❌ |
| 9 | 0.343214 | `azmcp-loadtesting-testresource-list` | ❌ |
| 10 | 0.342468 | `azmcp-redis-cluster-database-list` | ❌ |
| 11 | 0.319895 | `azmcp-workbooks-list` | ❌ |
| 12 | 0.316979 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 13 | 0.300073 | `azmcp-monitor-resource-log-query` | ❌ |
| 14 | 0.285253 | `azmcp-group-list` | ❌ |
| 15 | 0.285004 | `azmcp-quota-region-availability-list` | ❌ |
| 16 | 0.274589 | `azmcp-deploy-app-logs-get` | ❌ |
| 17 | 0.274464 | `azmcp-loadtesting-testrun-get` | ❌ |
| 18 | 0.270840 | `azmcp-loadtesting-testrun-list` | ❌ |
| 19 | 0.264788 | `azmcp-cosmos-database-list` | ❌ |
| 20 | 0.260738 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 137

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Check my Azure subscription for any compliance issues or recommendations  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.533164 | `azmcp-quota-usage-check` | ❌ |
| 2 | 0.476826 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 3 | 0.459005 | `azmcp-bestpractices-get` | ❌ |
| 4 | 0.442625 | `azmcp-extension-az` | ❌ |
| 5 | 0.440399 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 6 | 0.431096 | `azmcp-deploy-iac-rules-get` | ❌ |
| 7 | 0.427495 | `azmcp-search-service-list` | ❌ |
| 8 | 0.426311 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 9 | 0.423444 | `azmcp-subscription-list` | ❌ |
| 10 | 0.420585 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 11 | 0.408023 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 12 | 0.406591 | `azmcp-deploy-plan-get` | ❌ |
| 13 | 0.400363 | `azmcp-quota-region-availability-list` | ❌ |
| 14 | 0.388980 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.383400 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 16 | 0.380257 | `azmcp-deploy-app-logs-get` | ❌ |
| 17 | 0.366672 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.354364 | `azmcp-redis-cache-list` | ❌ |
| 19 | 0.351579 | `azmcp-redis-cluster-list` | ❌ |
| 20 | 0.331783 | `azmcp-storage-account-list` | ❌ |

---

## Test 138

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Provide compliance recommendations for my current Azure subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.527082 | `azmcp-bestpractices-get` | ❌ |
| 2 | 0.487939 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 3 | 0.474017 | `azmcp-extension-az` | ❌ |
| 4 | 0.473365 | `azmcp-deploy-iac-rules-get` | ❌ |
| 5 | 0.462743 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 6 | 0.448085 | `azmcp-deploy-plan-get` | ❌ |
| 7 | 0.442021 | `azmcp-quota-usage-check` | ❌ |
| 8 | 0.439040 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 9 | 0.426161 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 10 | 0.385771 | `azmcp-quota-region-availability-list` | ❌ |
| 11 | 0.382470 | `azmcp-search-service-list` | ❌ |
| 12 | 0.376158 | `azmcp-subscription-list` | ❌ |
| 13 | 0.365824 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 14 | 0.359062 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 15 | 0.338388 | `azmcp-marketplace-product-get` | ❌ |
| 16 | 0.333625 | `azmcp-monitor-workspace-list` | ❌ |
| 17 | 0.330901 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.316765 | `azmcp-redis-cluster-list` | ❌ |
| 19 | 0.310893 | `azmcp-redis-cache-list` | ❌ |
| 20 | 0.300889 | `azmcp-storage-account-details` | ❌ |

---

## Test 139

**Expected Tool:** `azmcp-extension-azqr`  
**Prompt:** Scan my Azure subscription for compliance recommendations  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.516925 | `azmcp-extension-azqr` | ✅ **EXPECTED** |
| 2 | 0.514791 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.504673 | `azmcp-quota-usage-check` | ❌ |
| 4 | 0.494872 | `azmcp-deploy-plan-get` | ❌ |
| 5 | 0.490438 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 6 | 0.472526 | `azmcp-extension-az` | ❌ |
| 7 | 0.463564 | `azmcp-deploy-iac-rules-get` | ❌ |
| 8 | 0.463172 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 9 | 0.450091 | `azmcp-search-service-list` | ❌ |
| 10 | 0.433938 | `azmcp-quota-region-availability-list` | ❌ |
| 11 | 0.423851 | `azmcp-subscription-list` | ❌ |
| 12 | 0.417356 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 13 | 0.403533 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 14 | 0.398621 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.391476 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 16 | 0.372267 | `azmcp-deploy-app-logs-get` | ❌ |
| 17 | 0.371729 | `azmcp-redis-cluster-list` | ❌ |
| 18 | 0.370619 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 19 | 0.369417 | `azmcp-redis-cache-list` | ❌ |
| 20 | 0.339864 | `azmcp-role-assignment-list` | ❌ |

---

## Test 140

**Expected Tool:** `azmcp-quota-region-availability-list`  
**Prompt:** Show me the available regions for these resource types <resource_types>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.590878 | `azmcp-quota-region-availability-list` | ✅ **EXPECTED** |
| 2 | 0.413274 | `azmcp-quota-usage-check` | ❌ |
| 3 | 0.372940 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 4 | 0.361386 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.349685 | `azmcp-monitor-table-type-list` | ❌ |
| 6 | 0.348793 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.337791 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.331074 | `azmcp-monitor-metrics-definitions` | ❌ |
| 9 | 0.328408 | `azmcp-grafana-list` | ❌ |
| 10 | 0.313240 | `azmcp-loadtesting-testresource-list` | ❌ |
| 11 | 0.310326 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 12 | 0.307143 | `azmcp-workbooks-list` | ❌ |
| 13 | 0.290125 | `azmcp-group-list` | ❌ |
| 14 | 0.287074 | `azmcp-acr-registry-list` | ❌ |
| 15 | 0.271127 | `azmcp-loadtesting-test-get` | ❌ |
| 16 | 0.265329 | `azmcp-monitor-metrics-query` | ❌ |
| 17 | 0.264358 | `azmcp-postgres-server-list` | ❌ |
| 18 | 0.246956 | `azmcp-acr-registry-repository-list` | ❌ |
| 19 | 0.236320 | `azmcp-foundry-models-list` | ❌ |
| 20 | 0.233469 | `azmcp-bicepschema-get` | ❌ |

---

## Test 141

**Expected Tool:** `azmcp-quota-usage-check`  
**Prompt:** Check usage information for <resource_type> in region <region>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609244 | `azmcp-quota-usage-check` | ✅ **EXPECTED** |
| 2 | 0.491058 | `azmcp-quota-region-availability-list` | ❌ |
| 3 | 0.384350 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 4 | 0.380135 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 5 | 0.379005 | `azmcp-redis-cache-list` | ❌ |
| 6 | 0.365791 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.342231 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.338636 | `azmcp-grafana-list` | ❌ |
| 9 | 0.337380 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.331262 | `azmcp-monitor-metrics-definitions` | ❌ |
| 11 | 0.322571 | `azmcp-workbooks-list` | ❌ |
| 12 | 0.321707 | `azmcp-monitor-resource-log-query` | ❌ |
| 13 | 0.313009 | `azmcp-storage-account-details` | ❌ |
| 14 | 0.309805 | `azmcp-loadtesting-testrun-get` | ❌ |
| 15 | 0.305628 | `azmcp-loadtesting-test-get` | ❌ |
| 16 | 0.300782 | `azmcp-aks-cluster-get` | ❌ |
| 17 | 0.280386 | `azmcp-loadtesting-testrun-list` | ❌ |
| 18 | 0.279857 | `azmcp-appconfig-kv-show` | ❌ |
| 19 | 0.270700 | `azmcp-group-list` | ❌ |
| 20 | 0.268487 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 142

**Expected Tool:** `azmcp-role-assignment-list`  
**Prompt:** List all available role assignments in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.645259 | `azmcp-role-assignment-list` | ✅ **EXPECTED** |
| 2 | 0.487393 | `azmcp-search-service-list` | ❌ |
| 3 | 0.483988 | `azmcp-group-list` | ❌ |
| 4 | 0.483240 | `azmcp-subscription-list` | ❌ |
| 5 | 0.478700 | `azmcp-grafana-list` | ❌ |
| 6 | 0.474766 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.471364 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.460125 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.452819 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.448130 | `azmcp-storage-account-list` | ❌ |
| 11 | 0.446372 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 12 | 0.441161 | `azmcp-functionapp-list` | ❌ |
| 13 | 0.430667 | `azmcp-kusto-cluster-list` | ❌ |
| 14 | 0.427950 | `azmcp-workbooks-list` | ❌ |
| 15 | 0.426624 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 16 | 0.403310 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.397565 | `azmcp-appconfig-account-list` | ❌ |
| 18 | 0.396961 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.374732 | `azmcp-loadtesting-testresource-list` | ❌ |
| 20 | 0.365607 | `azmcp-acr-registry-list` | ❌ |

---

## Test 143

**Expected Tool:** `azmcp-role-assignment-list`  
**Prompt:** Show me the available role assignments in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609705 | `azmcp-role-assignment-list` | ✅ **EXPECTED** |
| 2 | 0.456956 | `azmcp-grafana-list` | ❌ |
| 3 | 0.436866 | `azmcp-subscription-list` | ❌ |
| 4 | 0.435650 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.435287 | `azmcp-search-service-list` | ❌ |
| 6 | 0.435155 | `azmcp-monitor-workspace-list` | ❌ |
| 7 | 0.428663 | `azmcp-group-list` | ❌ |
| 8 | 0.428480 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.421627 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 10 | 0.420804 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.410380 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 12 | 0.406766 | `azmcp-quota-region-availability-list` | ❌ |
| 13 | 0.395445 | `azmcp-workbooks-list` | ❌ |
| 14 | 0.387931 | `azmcp-functionapp-list` | ❌ |
| 15 | 0.386800 | `azmcp-kusto-cluster-list` | ❌ |
| 16 | 0.383635 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.373204 | `azmcp-appconfig-account-list` | ❌ |
| 18 | 0.368511 | `azmcp-loadtesting-testresource-list` | ❌ |
| 19 | 0.353532 | `azmcp-aks-cluster-list` | ❌ |
| 20 | 0.351866 | `azmcp-marketplace-product-get` | ❌ |

---

## Test 144

**Expected Tool:** `azmcp-redis-cache-accesspolicy-list`  
**Prompt:** List all access policies in the Redis Cache <cache_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.757057 | `azmcp-redis-cache-accesspolicy-list` | ✅ **EXPECTED** |
| 2 | 0.565068 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.445125 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.377563 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.312428 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.307539 | `azmcp-keyvault-secret-list` | ❌ |
| 7 | 0.303736 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.303531 | `azmcp-appconfig-kv-list` | ❌ |
| 9 | 0.300119 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.300024 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.298380 | `azmcp-keyvault-certificate-list` | ❌ |
| 12 | 0.296657 | `azmcp-keyvault-key-list` | ❌ |
| 13 | 0.292082 | `azmcp-bestpractices-get` | ❌ |
| 14 | 0.286490 | `azmcp-acr-registry-repository-list` | ❌ |
| 15 | 0.284898 | `azmcp-appconfig-account-list` | ❌ |
| 16 | 0.284304 | `azmcp-grafana-list` | ❌ |
| 17 | 0.283583 | `azmcp-storage-blob-container-list` | ❌ |
| 18 | 0.281283 | `azmcp-storage-blob-container-details` | ❌ |
| 19 | 0.277652 | `azmcp-subscription-list` | ❌ |
| 20 | 0.274897 | `azmcp-role-assignment-list` | ❌ |

---

## Test 145

**Expected Tool:** `azmcp-redis-cache-accesspolicy-list`  
**Prompt:** Show me the access policies in the Redis Cache <cache_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713839 | `azmcp-redis-cache-accesspolicy-list` | ✅ **EXPECTED** |
| 2 | 0.523204 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.412441 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.338859 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.300045 | `azmcp-bestpractices-get` | ❌ |
| 6 | 0.288868 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.286321 | `azmcp-appconfig-kv-list` | ❌ |
| 8 | 0.280245 | `azmcp-appconfig-kv-show` | ❌ |
| 9 | 0.258045 | `azmcp-appconfig-account-list` | ❌ |
| 10 | 0.257957 | `azmcp-quota-usage-check` | ❌ |
| 11 | 0.257151 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.253484 | `azmcp-storage-table-list` | ❌ |
| 13 | 0.253209 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 14 | 0.253169 | `azmcp-loadtesting-testrun-list` | ❌ |
| 15 | 0.249917 | `azmcp-extension-az` | ❌ |
| 16 | 0.248361 | `azmcp-storage-account-details` | ❌ |
| 17 | 0.247998 | `azmcp-keyvault-secret-list` | ❌ |
| 18 | 0.246871 | `azmcp-grafana-list` | ❌ |
| 19 | 0.241891 | `azmcp-role-assignment-list` | ❌ |
| 20 | 0.232706 | `azmcp-resourcehealth-availability-status-list` | ❌ |

---

## Test 146

**Expected Tool:** `azmcp-redis-cache-list`  
**Prompt:** List all Redis Caches in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.764137 | `azmcp-redis-cache-list` | ✅ **EXPECTED** |
| 2 | 0.653933 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.501880 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 4 | 0.495048 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.472307 | `azmcp-grafana-list` | ❌ |
| 6 | 0.466141 | `azmcp-kusto-cluster-list` | ❌ |
| 7 | 0.464785 | `azmcp-redis-cluster-database-list` | ❌ |
| 8 | 0.433313 | `azmcp-search-service-list` | ❌ |
| 9 | 0.431968 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.431715 | `azmcp-appconfig-account-list` | ❌ |
| 11 | 0.423169 | `azmcp-subscription-list` | ❌ |
| 12 | 0.396295 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.393596 | `azmcp-storage-account-list` | ❌ |
| 14 | 0.381185 | `azmcp-kusto-database-list` | ❌ |
| 15 | 0.380443 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.373395 | `azmcp-group-list` | ❌ |
| 17 | 0.373274 | `azmcp-storage-table-list` | ❌ |
| 18 | 0.368719 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.361478 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.354948 | `azmcp-loadtesting-testresource-list` | ❌ |

---

## Test 147

**Expected Tool:** `azmcp-redis-cache-list`  
**Prompt:** Show me my Redis Caches  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.537914 | `azmcp-redis-cache-list` | ✅ **EXPECTED** |
| 2 | 0.450387 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 3 | 0.441120 | `azmcp-redis-cluster-list` | ❌ |
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
| 14 | 0.231294 | `azmcp-quota-usage-check` | ❌ |
| 15 | 0.225621 | `azmcp-postgres-server-config-get` | ❌ |
| 16 | 0.225079 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.224946 | `azmcp-storage-blob-container-list` | ❌ |
| 18 | 0.217990 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.211175 | `azmcp-extension-az` | ❌ |
| 20 | 0.210134 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 148

**Expected Tool:** `azmcp-redis-cache-list`  
**Prompt:** Show me the Redis Caches in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.692295 | `azmcp-redis-cache-list` | ✅ **EXPECTED** |
| 2 | 0.595717 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.461584 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 4 | 0.435009 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.427347 | `azmcp-grafana-list` | ❌ |
| 6 | 0.399276 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.383350 | `azmcp-appconfig-account-list` | ❌ |
| 8 | 0.382265 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.368495 | `azmcp-search-service-list` | ❌ |
| 10 | 0.361675 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.353476 | `azmcp-subscription-list` | ❌ |
| 12 | 0.340727 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.327246 | `azmcp-loadtesting-testresource-list` | ❌ |
| 14 | 0.320282 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.315559 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.310828 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.306356 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.304067 | `azmcp-group-list` | ❌ |
| 19 | 0.303184 | `azmcp-storage-table-list` | ❌ |
| 20 | 0.298548 | `azmcp-kusto-database-list` | ❌ |

---

## Test 149

**Expected Tool:** `azmcp-redis-cluster-database-list`  
**Prompt:** List all databases in the Redis Cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.752919 | `azmcp-redis-cluster-database-list` | ✅ **EXPECTED** |
| 2 | 0.603772 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.595065 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.548268 | `azmcp-postgres-database-list` | ❌ |
| 5 | 0.538403 | `azmcp-cosmos-database-list` | ❌ |
| 6 | 0.471385 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.458244 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.456033 | `azmcp-kusto-table-list` | ❌ |
| 9 | 0.449548 | `azmcp-sql-db-list` | ❌ |
| 10 | 0.419621 | `azmcp-postgres-table-list` | ❌ |
| 11 | 0.385544 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.379937 | `azmcp-postgres-server-list` | ❌ |
| 13 | 0.376262 | `azmcp-aks-cluster-list` | ❌ |
| 14 | 0.366236 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.355818 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 16 | 0.352225 | `azmcp-storage-table-list` | ❌ |
| 17 | 0.328081 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.325668 | `azmcp-monitor-table-list` | ❌ |
| 19 | 0.324867 | `azmcp-grafana-list` | ❌ |
| 20 | 0.317852 | `azmcp-acr-registry-repository-list` | ❌ |

---

## Test 150

**Expected Tool:** `azmcp-redis-cluster-database-list`  
**Prompt:** Show me the databases in the Redis Cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.721506 | `azmcp-redis-cluster-database-list` | ✅ **EXPECTED** |
| 2 | 0.562861 | `azmcp-redis-cluster-list` | ❌ |
| 3 | 0.537864 | `azmcp-kusto-database-list` | ❌ |
| 4 | 0.481618 | `azmcp-cosmos-database-list` | ❌ |
| 5 | 0.480274 | `azmcp-postgres-database-list` | ❌ |
| 6 | 0.434992 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.414610 | `azmcp-kusto-table-list` | ❌ |
| 8 | 0.408379 | `azmcp-sql-db-list` | ❌ |
| 9 | 0.397285 | `azmcp-kusto-cluster-list` | ❌ |
| 10 | 0.351025 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.349880 | `azmcp-postgres-table-list` | ❌ |
| 12 | 0.343275 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 13 | 0.337272 | `azmcp-postgres-server-list` | ❌ |
| 14 | 0.325416 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.318982 | `azmcp-cosmos-account-list` | ❌ |
| 16 | 0.306736 | `azmcp-storage-table-list` | ❌ |
| 17 | 0.302228 | `azmcp-kusto-sample` | ❌ |
| 18 | 0.294470 | `azmcp-kusto-table-schema` | ❌ |
| 19 | 0.292180 | `azmcp-grafana-list` | ❌ |
| 20 | 0.288275 | `azmcp-sql-db-show` | ❌ |

---

## Test 151

**Expected Tool:** `azmcp-redis-cluster-list`  
**Prompt:** List all Redis Clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.812952 | `azmcp-redis-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.679028 | `azmcp-kusto-cluster-list` | ❌ |
| 3 | 0.672160 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.588847 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.569222 | `azmcp-aks-cluster-list` | ❌ |
| 6 | 0.554298 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.527254 | `azmcp-kusto-database-list` | ❌ |
| 8 | 0.503279 | `azmcp-grafana-list` | ❌ |
| 9 | 0.467957 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.463770 | `azmcp-search-service-list` | ❌ |
| 11 | 0.457790 | `azmcp-kusto-cluster-get` | ❌ |
| 12 | 0.455613 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.445496 | `azmcp-group-list` | ❌ |
| 14 | 0.445406 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.442886 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 16 | 0.439387 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 17 | 0.436649 | `azmcp-subscription-list` | ❌ |
| 18 | 0.422049 | `azmcp-storage-account-list` | ❌ |
| 19 | 0.419126 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.419075 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 152

**Expected Tool:** `azmcp-redis-cluster-list`  
**Prompt:** Show me my Redis Clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591570 | `azmcp-redis-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.514375 | `azmcp-redis-cluster-database-list` | ❌ |
| 3 | 0.467566 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.403281 | `azmcp-kusto-cluster-list` | ❌ |
| 5 | 0.385069 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 6 | 0.368011 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.329389 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.322149 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.305629 | `azmcp-kusto-cluster-get` | ❌ |
| 10 | 0.301302 | `azmcp-aks-cluster-get` | ❌ |
| 11 | 0.295045 | `azmcp-grafana-list` | ❌ |
| 12 | 0.291684 | `azmcp-postgres-database-list` | ❌ |
| 13 | 0.272504 | `azmcp-cosmos-database-list` | ❌ |
| 14 | 0.260993 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.259662 | `azmcp-postgres-server-config-get` | ❌ |
| 16 | 0.257012 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.253862 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.252053 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 19 | 0.248676 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 20 | 0.246052 | `azmcp-monitor-workspace-list` | ❌ |

---

## Test 153

**Expected Tool:** `azmcp-redis-cluster-list`  
**Prompt:** Show me the Redis Clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.744231 | `azmcp-redis-cluster-list` | ✅ **EXPECTED** |
| 2 | 0.607614 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.580866 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.518857 | `azmcp-redis-cluster-database-list` | ❌ |
| 5 | 0.494170 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.491262 | `azmcp-aks-cluster-list` | ❌ |
| 7 | 0.456252 | `azmcp-grafana-list` | ❌ |
| 8 | 0.446556 | `azmcp-kusto-cluster-get` | ❌ |
| 9 | 0.440471 | `azmcp-kusto-database-list` | ❌ |
| 10 | 0.400256 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 11 | 0.394530 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.394483 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.393490 | `azmcp-search-service-list` | ❌ |
| 14 | 0.389814 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.372221 | `azmcp-group-list` | ❌ |
| 16 | 0.368926 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.367955 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 18 | 0.367096 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 19 | 0.363943 | `azmcp-subscription-list` | ❌ |
| 20 | 0.357200 | `azmcp-acr-registry-list` | ❌ |

---

## Test 154

**Expected Tool:** `azmcp-group-list`  
**Prompt:** List all resource groups in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.755935 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.566552 | `azmcp-workbooks-list` | ❌ |
| 3 | 0.552633 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 4 | 0.546156 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 5 | 0.545500 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.542878 | `azmcp-grafana-list` | ❌ |
| 7 | 0.530506 | `azmcp-redis-cache-list` | ❌ |
| 8 | 0.524796 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.524265 | `azmcp-search-service-list` | ❌ |
| 10 | 0.518475 | `azmcp-acr-registry-list` | ❌ |
| 11 | 0.517060 | `azmcp-loadtesting-testresource-list` | ❌ |
| 12 | 0.500858 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.491176 | `azmcp-acr-registry-repository-list` | ❌ |
| 14 | 0.486716 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.485348 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 16 | 0.479638 | `azmcp-subscription-list` | ❌ |
| 17 | 0.477024 | `azmcp-aks-cluster-list` | ❌ |
| 18 | 0.472171 | `azmcp-quota-region-availability-list` | ❌ |
| 19 | 0.460870 | `azmcp-postgres-server-list` | ❌ |
| 20 | 0.460282 | `azmcp-functionapp-list` | ❌ |

---

## Test 155

**Expected Tool:** `azmcp-group-list`  
**Prompt:** Show me my resource groups  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.529504 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.463685 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 3 | 0.459304 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 4 | 0.453960 | `azmcp-workbooks-list` | ❌ |
| 5 | 0.429014 | `azmcp-loadtesting-testresource-list` | ❌ |
| 6 | 0.426996 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.407817 | `azmcp-grafana-list` | ❌ |
| 8 | 0.391227 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.382985 | `azmcp-acr-registry-list` | ❌ |
| 10 | 0.379927 | `azmcp-acr-registry-repository-list` | ❌ |
| 11 | 0.373796 | `azmcp-quota-region-availability-list` | ❌ |
| 12 | 0.366273 | `azmcp-sql-db-list` | ❌ |
| 13 | 0.360235 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 14 | 0.350999 | `azmcp-quota-usage-check` | ❌ |
| 15 | 0.345595 | `azmcp-redis-cluster-database-list` | ❌ |
| 16 | 0.343018 | `azmcp-sql-elastic-pool-list` | ❌ |
| 17 | 0.328487 | `azmcp-loadtesting-testresource-create` | ❌ |
| 18 | 0.326141 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.325359 | `azmcp-kusto-cluster-list` | ❌ |
| 20 | 0.323258 | `azmcp-extension-azqr` | ❌ |

---

## Test 156

**Expected Tool:** `azmcp-group-list`  
**Prompt:** Show me the resource groups in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.665771 | `azmcp-group-list` | ✅ **EXPECTED** |
| 2 | 0.532656 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 3 | 0.531920 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 4 | 0.523137 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.522911 | `azmcp-workbooks-list` | ❌ |
| 6 | 0.518543 | `azmcp-loadtesting-testresource-list` | ❌ |
| 7 | 0.515905 | `azmcp-grafana-list` | ❌ |
| 8 | 0.492919 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.487740 | `azmcp-acr-registry-list` | ❌ |
| 10 | 0.475313 | `azmcp-search-service-list` | ❌ |
| 11 | 0.470658 | `azmcp-kusto-cluster-list` | ❌ |
| 12 | 0.464637 | `azmcp-quota-region-availability-list` | ❌ |
| 13 | 0.460412 | `azmcp-monitor-workspace-list` | ❌ |
| 14 | 0.451877 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 15 | 0.437393 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.435409 | `azmcp-subscription-list` | ❌ |
| 17 | 0.432994 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.429564 | `azmcp-acr-registry-repository-list` | ❌ |
| 19 | 0.423232 | `azmcp-postgres-server-list` | ❌ |
| 20 | 0.403041 | `azmcp-functionapp-list` | ❌ |

---

## Test 157

**Expected Tool:** `azmcp-resourcehealth-availability-status-get`  
**Prompt:** Get the availability status for resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.629120 | `azmcp-resourcehealth-availability-status-get` | ✅ **EXPECTED** |
| 2 | 0.538273 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 3 | 0.377586 | `azmcp-quota-usage-check` | ❌ |
| 4 | 0.349980 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.331563 | `azmcp-monitor-metrics-definitions` | ❌ |
| 6 | 0.327638 | `azmcp-redis-cache-list` | ❌ |
| 7 | 0.324331 | `azmcp-quota-region-availability-list` | ❌ |
| 8 | 0.311644 | `azmcp-monitor-metrics-query` | ❌ |
| 9 | 0.308290 | `azmcp-redis-cluster-list` | ❌ |
| 10 | 0.306616 | `azmcp-grafana-list` | ❌ |
| 11 | 0.290698 | `azmcp-workbooks-show` | ❌ |
| 12 | 0.289007 | `azmcp-storage-blob-container-details` | ❌ |
| 13 | 0.286560 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 14 | 0.283436 | `azmcp-loadtesting-test-get` | ❌ |
| 15 | 0.281508 | `azmcp-workbooks-list` | ❌ |
| 16 | 0.272483 | `azmcp-aks-cluster-get` | ❌ |
| 17 | 0.272207 | `azmcp-group-list` | ❌ |
| 18 | 0.270773 | `azmcp-loadtesting-testresource-list` | ❌ |
| 19 | 0.268110 | `azmcp-loadtesting-testrun-get` | ❌ |
| 20 | 0.244023 | `azmcp-bicepschema-get` | ❌ |

---

## Test 158

**Expected Tool:** `azmcp-resourcehealth-availability-status-get`  
**Prompt:** Show me the health status of the storage account <storage_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.546066 | `azmcp-storage-account-details` | ❌ |
| 2 | 0.533163 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.518147 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.505348 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.492853 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.489273 | `azmcp-resourcehealth-availability-status-get` | ✅ **EXPECTED** |
| 7 | 0.476995 | `azmcp-storage-blob-list` | ❌ |
| 8 | 0.466885 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 9 | 0.426780 | `azmcp-storage-account-create` | ❌ |
| 10 | 0.411283 | `azmcp-quota-usage-check` | ❌ |
| 11 | 0.405847 | `azmcp-cosmos-account-list` | ❌ |
| 12 | 0.375351 | `azmcp-cosmos-database-container-list` | ❌ |
| 13 | 0.368262 | `azmcp-appconfig-kv-show` | ❌ |
| 14 | 0.349407 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.321704 | `azmcp-deploy-app-logs-get` | ❌ |
| 16 | 0.311399 | `azmcp-appconfig-account-list` | ❌ |
| 17 | 0.306746 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.304460 | `azmcp-functionapp-list` | ❌ |
| 19 | 0.301527 | `azmcp-loadtesting-testrun-get` | ❌ |
| 20 | 0.300715 | `azmcp-aks-cluster-get` | ❌ |

---

## Test 159

**Expected Tool:** `azmcp-resourcehealth-availability-status-get`  
**Prompt:** What is the availability status of virtual machine <vm_name> in resource group <resource_group_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.577398 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 2 | 0.573290 | `azmcp-resourcehealth-availability-status-get` | ✅ **EXPECTED** |
| 3 | 0.386598 | `azmcp-quota-usage-check` | ❌ |
| 4 | 0.373883 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.348148 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 6 | 0.321304 | `azmcp-group-list` | ❌ |
| 7 | 0.318379 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.318319 | `azmcp-workbooks-list` | ❌ |
| 9 | 0.307076 | `azmcp-sql-db-show` | ❌ |
| 10 | 0.304604 | `azmcp-quota-region-availability-list` | ❌ |
| 11 | 0.302443 | `azmcp-functionapp-list` | ❌ |
| 12 | 0.300392 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 13 | 0.299346 | `azmcp-monitor-metrics-query` | ❌ |
| 14 | 0.298723 | `azmcp-monitor-metrics-definitions` | ❌ |
| 15 | 0.294423 | `azmcp-aks-cluster-get` | ❌ |
| 16 | 0.289170 | `azmcp-loadtesting-testresource-list` | ❌ |
| 17 | 0.283924 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.282460 | `azmcp-deploy-app-logs-get` | ❌ |
| 19 | 0.279417 | `azmcp-loadtesting-testresource-create` | ❌ |
| 20 | 0.276264 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 160

**Expected Tool:** `azmcp-resourcehealth-availability-status-list`  
**Prompt:** List availability status for all resources in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.737219 | `azmcp-resourcehealth-availability-status-list` | ✅ **EXPECTED** |
| 2 | 0.587526 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 3 | 0.578547 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.563557 | `azmcp-redis-cluster-list` | ❌ |
| 5 | 0.548549 | `azmcp-grafana-list` | ❌ |
| 6 | 0.540583 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 7 | 0.531356 | `azmcp-quota-region-availability-list` | ❌ |
| 8 | 0.530985 | `azmcp-group-list` | ❌ |
| 9 | 0.530242 | `azmcp-search-service-list` | ❌ |
| 10 | 0.507740 | `azmcp-monitor-workspace-list` | ❌ |
| 11 | 0.502673 | `azmcp-storage-account-list` | ❌ |
| 12 | 0.496651 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.491520 | `azmcp-subscription-list` | ❌ |
| 14 | 0.491394 | `azmcp-quota-usage-check` | ❌ |
| 15 | 0.484221 | `azmcp-loadtesting-testresource-list` | ❌ |
| 16 | 0.482623 | `azmcp-kusto-cluster-list` | ❌ |
| 17 | 0.471685 | `azmcp-functionapp-list` | ❌ |
| 18 | 0.465422 | `azmcp-aks-cluster-list` | ❌ |
| 19 | 0.457237 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.434345 | `azmcp-acr-registry-list` | ❌ |

---

## Test 161

**Expected Tool:** `azmcp-resourcehealth-availability-status-list`  
**Prompt:** Show me the health status of all my Azure resources  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.645012 | `azmcp-resourcehealth-availability-status-list` | ✅ **EXPECTED** |
| 2 | 0.582250 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 3 | 0.508208 | `azmcp-quota-usage-check` | ❌ |
| 4 | 0.473906 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 5 | 0.409342 | `azmcp-deploy-app-logs-get` | ❌ |
| 6 | 0.406706 | `azmcp-quota-region-availability-list` | ❌ |
| 7 | 0.405787 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.403377 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.401085 | `azmcp-functionapp-list` | ❌ |
| 10 | 0.400974 | `azmcp-search-service-list` | ❌ |
| 11 | 0.400509 | `azmcp-subscription-list` | ❌ |
| 12 | 0.400475 | `azmcp-monitor-metrics-query` | ❌ |
| 13 | 0.400467 | `azmcp-extension-az` | ❌ |
| 14 | 0.399945 | `azmcp-redis-cluster-list` | ❌ |
| 15 | 0.397323 | `azmcp-redis-cache-list` | ❌ |
| 16 | 0.391858 | `azmcp-bestpractices-get` | ❌ |
| 17 | 0.389539 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 18 | 0.387837 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.371879 | `azmcp-loadtesting-testresource-list` | ❌ |
| 20 | 0.366839 | `azmcp-deploy-plan-get` | ❌ |

---

## Test 162

**Expected Tool:** `azmcp-resourcehealth-availability-status-list`  
**Prompt:** What resources in resource group <resource_group_name> have health issues?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.596890 | `azmcp-resourcehealth-availability-status-list` | ✅ **EXPECTED** |
| 2 | 0.536904 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 3 | 0.427638 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 4 | 0.411111 | `azmcp-quota-usage-check` | ❌ |
| 5 | 0.370961 | `azmcp-loadtesting-testresource-list` | ❌ |
| 6 | 0.363808 | `azmcp-workbooks-list` | ❌ |
| 7 | 0.360122 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.350454 | `azmcp-group-list` | ❌ |
| 9 | 0.348923 | `azmcp-monitor-metrics-query` | ❌ |
| 10 | 0.334759 | `azmcp-redis-cache-list` | ❌ |
| 11 | 0.332886 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 12 | 0.330185 | `azmcp-extension-azqr` | ❌ |
| 13 | 0.328560 | `azmcp-extension-az` | ❌ |
| 14 | 0.321787 | `azmcp-monitor-resource-log-query` | ❌ |
| 15 | 0.319481 | `azmcp-sql-db-list` | ❌ |
| 16 | 0.317434 | `azmcp-quota-region-availability-list` | ❌ |
| 17 | 0.309414 | `azmcp-deploy-app-logs-get` | ❌ |
| 18 | 0.308680 | `azmcp-grafana-list` | ❌ |
| 19 | 0.295752 | `azmcp-monitor-metrics-definitions` | ❌ |
| 20 | 0.293645 | `azmcp-acr-registry-list` | ❌ |

---

## Test 163

**Expected Tool:** `azmcp-servicebus-queue-details`  
**Prompt:** Show me the details of service bus <service_bus_name> queue <queue_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.642876 | `azmcp-servicebus-queue-details` | ✅ **EXPECTED** |
| 2 | 0.460932 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.400870 | `azmcp-servicebus-topic-details` | ❌ |
| 4 | 0.376496 | `azmcp-storage-queue-message-send` | ❌ |
| 5 | 0.375348 | `azmcp-aks-cluster-get` | ❌ |
| 6 | 0.338738 | `azmcp-loadtesting-testrun-get` | ❌ |
| 7 | 0.337239 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.322914 | `azmcp-kusto-cluster-get` | ❌ |
| 9 | 0.316350 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.310924 | `azmcp-search-index-list` | ❌ |
| 11 | 0.308540 | `azmcp-redis-cache-list` | ❌ |
| 12 | 0.306552 | `azmcp-storage-account-details` | ❌ |
| 13 | 0.301249 | `azmcp-quota-usage-check` | ❌ |
| 14 | 0.296380 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.279042 | `azmcp-functionapp-list` | ❌ |
| 16 | 0.278090 | `azmcp-marketplace-product-get` | ❌ |
| 17 | 0.271662 | `azmcp-loadtesting-test-get` | ❌ |
| 18 | 0.266686 | `azmcp-appconfig-kv-show` | ❌ |
| 19 | 0.258399 | `azmcp-keyvault-certificate-get` | ❌ |
| 20 | 0.255819 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 164

**Expected Tool:** `azmcp-servicebus-topic-details`  
**Prompt:** Show me the details of service bus <service_bus_name> topic <topic_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591468 | `azmcp-servicebus-topic-details` | ✅ **EXPECTED** |
| 2 | 0.571708 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 3 | 0.483869 | `azmcp-servicebus-queue-details` | ❌ |
| 4 | 0.361288 | `azmcp-aks-cluster-get` | ❌ |
| 5 | 0.346967 | `azmcp-loadtesting-testrun-get` | ❌ |
| 6 | 0.339975 | `azmcp-sql-db-show` | ❌ |
| 7 | 0.335403 | `azmcp-kusto-cluster-get` | ❌ |
| 8 | 0.324870 | `azmcp-redis-cache-list` | ❌ |
| 9 | 0.317369 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.315555 | `azmcp-redis-cluster-list` | ❌ |
| 11 | 0.306500 | `azmcp-search-index-list` | ❌ |
| 12 | 0.303910 | `azmcp-search-service-list` | ❌ |
| 13 | 0.303614 | `azmcp-storage-table-list` | ❌ |
| 14 | 0.297242 | `azmcp-grafana-list` | ❌ |
| 15 | 0.295418 | `azmcp-functionapp-list` | ❌ |
| 16 | 0.294265 | `azmcp-marketplace-product-get` | ❌ |
| 17 | 0.290415 | `azmcp-monitor-workspace-list` | ❌ |
| 18 | 0.278701 | `azmcp-kusto-table-schema` | ❌ |
| 19 | 0.278599 | `azmcp-loadtesting-test-get` | ❌ |
| 20 | 0.275642 | `azmcp-loadtesting-testrun-list` | ❌ |

---

## Test 165

**Expected Tool:** `azmcp-servicebus-topic-subscription-details`  
**Prompt:** Show me the details of service bus <service_bus_name> subscription <subscription_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633187 | `azmcp-servicebus-topic-subscription-details` | ✅ **EXPECTED** |
| 2 | 0.494515 | `azmcp-servicebus-queue-details` | ❌ |
| 3 | 0.457036 | `azmcp-servicebus-topic-details` | ❌ |
| 4 | 0.449818 | `azmcp-search-service-list` | ❌ |
| 5 | 0.429506 | `azmcp-redis-cache-list` | ❌ |
| 6 | 0.426768 | `azmcp-kusto-cluster-get` | ❌ |
| 7 | 0.421009 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.409614 | `azmcp-aks-cluster-list` | ❌ |
| 9 | 0.406169 | `azmcp-functionapp-list` | ❌ |
| 10 | 0.404769 | `azmcp-redis-cluster-list` | ❌ |
| 11 | 0.396053 | `azmcp-marketplace-product-get` | ❌ |
| 12 | 0.395176 | `azmcp-grafana-list` | ❌ |
| 13 | 0.388049 | `azmcp-postgres-server-list` | ❌ |
| 14 | 0.385222 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.374135 | `azmcp-subscription-list` | ❌ |
| 16 | 0.369986 | `azmcp-appconfig-account-list` | ❌ |
| 17 | 0.368486 | `azmcp-aks-cluster-get` | ❌ |
| 18 | 0.368155 | `azmcp-kusto-cluster-list` | ❌ |
| 19 | 0.367649 | `azmcp-group-list` | ❌ |
| 20 | 0.358070 | `azmcp-cosmos-account-list` | ❌ |

---

## Test 166

**Expected Tool:** `azmcp-sql-db-list`  
**Prompt:** List all databases in the Azure SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.643186 | `azmcp-sql-db-list` | ✅ **EXPECTED** |
| 2 | 0.609178 | `azmcp-postgres-database-list` | ❌ |
| 3 | 0.602890 | `azmcp-cosmos-database-list` | ❌ |
| 4 | 0.527919 | `azmcp-kusto-database-list` | ❌ |
| 5 | 0.482736 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.474927 | `azmcp-redis-cluster-database-list` | ❌ |
| 7 | 0.466130 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.464525 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.457219 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.457181 | `azmcp-kusto-table-list` | ❌ |
| 11 | 0.456149 | `azmcp-monitor-table-list` | ❌ |
| 12 | 0.443648 | `azmcp-postgres-table-list` | ❌ |
| 13 | 0.441355 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.440528 | `azmcp-cosmos-database-container-list` | ❌ |
| 15 | 0.420957 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 16 | 0.400489 | `azmcp-keyvault-certificate-list` | ❌ |
| 17 | 0.395078 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.394649 | `azmcp-keyvault-secret-list` | ❌ |
| 19 | 0.382680 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.380402 | `azmcp-acr-registry-repository-list` | ❌ |

---

## Test 167

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
| 8 | 0.387633 | `azmcp-kusto-database-list` | ❌ |
| 9 | 0.387445 | `azmcp-postgres-server-list` | ❌ |
| 10 | 0.380428 | `azmcp-appconfig-account-list` | ❌ |
| 11 | 0.372897 | `azmcp-storage-account-details` | ❌ |
| 12 | 0.357318 | `azmcp-aks-cluster-list` | ❌ |
| 13 | 0.356861 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 14 | 0.350224 | `azmcp-storage-table-list` | ❌ |
| 15 | 0.349880 | `azmcp-cosmos-account-list` | ❌ |
| 16 | 0.347075 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.345262 | `azmcp-loadtesting-test-get` | ❌ |
| 18 | 0.342792 | `azmcp-appconfig-kv-list` | ❌ |
| 19 | 0.342471 | `azmcp-aks-cluster-get` | ❌ |
| 20 | 0.341545 | `azmcp-kusto-table-list` | ❌ |

---

## Test 168

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
| 7 | 0.354111 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 8 | 0.348228 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.341701 | `azmcp-postgres-database-list` | ❌ |
| 10 | 0.341203 | `azmcp-postgres-table-schema-get` | ❌ |
| 11 | 0.325945 | `azmcp-kusto-table-schema` | ❌ |
| 12 | 0.320147 | `azmcp-aks-cluster-get` | ❌ |
| 13 | 0.312720 | `azmcp-storage-account-details` | ❌ |
| 14 | 0.297839 | `azmcp-appconfig-kv-show` | ❌ |
| 15 | 0.294987 | `azmcp-appconfig-kv-list` | ❌ |
| 16 | 0.273351 | `azmcp-kusto-cluster-get` | ❌ |
| 17 | 0.273315 | `azmcp-cosmos-database-list` | ❌ |
| 18 | 0.263979 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.260930 | `azmcp-loadtesting-testrun-list` | ❌ |
| 20 | 0.253588 | `azmcp-kusto-table-list` | ❌ |

---

## Test 169

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
| 8 | 0.344734 | `azmcp-kusto-table-schema` | ❌ |
| 9 | 0.343310 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.339765 | `azmcp-postgres-server-list` | ❌ |
| 11 | 0.337996 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.328612 | `azmcp-sql-elastic-pool-list` | ❌ |
| 13 | 0.323484 | `azmcp-kusto-table-list` | ❌ |
| 14 | 0.300133 | `azmcp-cosmos-database-container-list` | ❌ |
| 15 | 0.299873 | `azmcp-aks-cluster-get` | ❌ |
| 16 | 0.296909 | `azmcp-kusto-database-list` | ❌ |
| 17 | 0.294910 | `azmcp-loadtesting-testrun-get` | ❌ |
| 18 | 0.285620 | `azmcp-kusto-cluster-get` | ❌ |
| 19 | 0.261790 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 20 | 0.252492 | `azmcp-kusto-sample` | ❌ |

---

## Test 170

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
| 6 | 0.431174 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.416273 | `azmcp-monitor-table-list` | ❌ |
| 8 | 0.414738 | `azmcp-postgres-database-list` | ❌ |
| 9 | 0.412061 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 10 | 0.409078 | `azmcp-monitor-table-type-list` | ❌ |
| 11 | 0.408080 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 12 | 0.394358 | `azmcp-kusto-database-list` | ❌ |
| 13 | 0.370652 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.363579 | `azmcp-kusto-cluster-list` | ❌ |
| 15 | 0.357229 | `azmcp-kusto-table-list` | ❌ |
| 16 | 0.352050 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.351647 | `azmcp-cosmos-database-container-list` | ❌ |
| 18 | 0.349479 | `azmcp-keyvault-key-list` | ❌ |
| 19 | 0.348598 | `azmcp-keyvault-secret-list` | ❌ |
| 20 | 0.331834 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 171

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
| 7 | 0.357019 | `azmcp-postgres-server-config-get` | ❌ |
| 8 | 0.354094 | `azmcp-sql-db-show` | ❌ |
| 9 | 0.343841 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 10 | 0.335615 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.334567 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 12 | 0.319836 | `azmcp-aks-cluster-list` | ❌ |
| 13 | 0.304600 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.304317 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.298907 | `azmcp-kusto-database-list` | ❌ |
| 16 | 0.298227 | `azmcp-acr-registry-list` | ❌ |
| 17 | 0.297976 | `azmcp-aks-cluster-get` | ❌ |
| 18 | 0.293905 | `azmcp-cosmos-database-container-list` | ❌ |
| 19 | 0.277055 | `azmcp-loadtesting-test-get` | ❌ |
| 20 | 0.274081 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 172

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
| 8 | 0.311370 | `azmcp-redis-cluster-list` | ❌ |
| 9 | 0.308077 | `azmcp-redis-cluster-database-list` | ❌ |
| 10 | 0.307724 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.298933 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.292566 | `azmcp-kusto-cluster-list` | ❌ |
| 13 | 0.284127 | `azmcp-kusto-database-list` | ❌ |
| 14 | 0.281680 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.272025 | `azmcp-monitor-metrics-definitions` | ❌ |
| 16 | 0.259325 | `azmcp-loadtesting-testresource-list` | ❌ |
| 17 | 0.256635 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.252920 | `azmcp-foundry-models-deployments-list` | ❌ |
| 19 | 0.249936 | `azmcp-extension-az` | ❌ |
| 20 | 0.247097 | `azmcp-grafana-list` | ❌ |

---

## Test 173

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
| 8 | 0.283286 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 9 | 0.280450 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.279198 | `azmcp-sql-db-show` | ❌ |
| 11 | 0.277773 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.258095 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.249297 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 14 | 0.249151 | `azmcp-kusto-database-list` | ❌ |
| 15 | 0.246604 | `azmcp-keyvault-secret-list` | ❌ |
| 16 | 0.245267 | `azmcp-group-list` | ❌ |
| 17 | 0.238150 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.233337 | `azmcp-cosmos-database-container-list` | ❌ |
| 19 | 0.232633 | `azmcp-loadtesting-testrun-list` | ❌ |
| 20 | 0.227804 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 174

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** Show me the Entra ID administrators configured for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.718251 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
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
| 14 | 0.193038 | `azmcp-kusto-database-list` | ❌ |
| 15 | 0.191538 | `azmcp-appconfig-kv-list` | ❌ |
| 16 | 0.188120 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.186088 | `azmcp-loadtesting-testrun-list` | ❌ |
| 18 | 0.183184 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 19 | 0.182322 | `azmcp-extension-az` | ❌ |
| 20 | 0.182237 | `azmcp-deploy-app-logs-get` | ❌ |

---

## Test 175

**Expected Tool:** `azmcp-sql-server-entra-admin-list`  
**Prompt:** What Microsoft Entra ID administrators are set up for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651306 | `azmcp-sql-server-entra-admin-list` | ✅ **EXPECTED** |
| 2 | 0.253610 | `azmcp-sql-db-list` | ❌ |
| 3 | 0.244772 | `azmcp-extension-az` | ❌ |
| 4 | 0.229560 | `azmcp-sql-elastic-pool-list` | ❌ |
| 5 | 0.228093 | `azmcp-sql-server-firewall-rule-list` | ❌ |
| 6 | 0.217698 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.205654 | `azmcp-sql-db-show` | ❌ |
| 8 | 0.198194 | `azmcp-monitor-resource-log-query` | ❌ |
| 9 | 0.189941 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 10 | 0.189581 | `azmcp-storage-table-list` | ❌ |
| 11 | 0.188753 | `azmcp-postgres-server-param-get` | ❌ |
| 12 | 0.188287 | `azmcp-deploy-plan-get` | ❌ |
| 13 | 0.182452 | `azmcp-postgres-server-config-get` | ❌ |
| 14 | 0.180995 | `azmcp-deploy-app-logs-get` | ❌ |
| 15 | 0.180555 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 16 | 0.174553 | `azmcp-deploy-iac-rules-get` | ❌ |
| 17 | 0.169286 | `azmcp-kusto-database-list` | ❌ |
| 18 | 0.165162 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 19 | 0.164022 | `azmcp-extension-azd` | ❌ |
| 20 | 0.163349 | `azmcp-bestpractices-get` | ❌ |

---

## Test 176

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
| 7 | 0.305027 | `azmcp-keyvault-secret-list` | ❌ |
| 8 | 0.304175 | `azmcp-monitor-table-list` | ❌ |
| 9 | 0.301711 | `azmcp-postgres-table-list` | ❌ |
| 10 | 0.299205 | `azmcp-postgres-server-config-get` | ❌ |
| 11 | 0.298226 | `azmcp-sql-db-show` | ❌ |
| 12 | 0.278098 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.277760 | `azmcp-functionapp-list` | ❌ |
| 14 | 0.277410 | `azmcp-keyvault-key-list` | ❌ |
| 15 | 0.276828 | `azmcp-keyvault-certificate-list` | ❌ |
| 16 | 0.270667 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.263100 | `azmcp-kusto-table-list` | ❌ |
| 18 | 0.263086 | `azmcp-bestpractices-get` | ❌ |
| 19 | 0.259932 | `azmcp-extension-az` | ❌ |
| 20 | 0.253852 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 177

**Expected Tool:** `azmcp-sql-server-firewall-rule-list`  
**Prompt:** Show me the firewall rules for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.631499 | `azmcp-sql-server-firewall-rule-list` | ✅ **EXPECTED** |
| 2 | 0.321414 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.312035 | `azmcp-postgres-server-list` | ❌ |
| 4 | 0.290374 | `azmcp-extension-az` | ❌ |
| 5 | 0.290235 | `azmcp-postgres-server-config-get` | ❌ |
| 6 | 0.287747 | `azmcp-postgres-server-param-get` | ❌ |
| 7 | 0.276175 | `azmcp-sql-db-list` | ❌ |
| 8 | 0.272586 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.272053 | `azmcp-sql-db-show` | ❌ |
| 10 | 0.255371 | `azmcp-bestpractices-get` | ❌ |
| 11 | 0.234831 | `azmcp-quota-usage-check` | ❌ |
| 12 | 0.228931 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 13 | 0.225372 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 14 | 0.208281 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 15 | 0.206818 | `azmcp-keyvault-secret-list` | ❌ |
| 16 | 0.206476 | `azmcp-deploy-iac-rules-get` | ❌ |
| 17 | 0.206039 | `azmcp-kusto-table-list` | ❌ |
| 18 | 0.197711 | `azmcp-kusto-sample` | ❌ |
| 19 | 0.189871 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.189786 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 178

**Expected Tool:** `azmcp-sql-server-firewall-rule-list`  
**Prompt:** What firewall rules are configured for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633622 | `azmcp-sql-server-firewall-rule-list` | ✅ **EXPECTED** |
| 2 | 0.311867 | `azmcp-sql-server-entra-admin-list` | ❌ |
| 3 | 0.299474 | `azmcp-extension-az` | ❌ |
| 4 | 0.277628 | `azmcp-postgres-server-config-get` | ❌ |
| 5 | 0.262028 | `azmcp-sql-db-list` | ❌ |
| 6 | 0.261404 | `azmcp-postgres-server-list` | ❌ |
| 7 | 0.261123 | `azmcp-postgres-server-param-get` | ❌ |
| 8 | 0.258402 | `azmcp-sql-elastic-pool-list` | ❌ |
| 9 | 0.247516 | `azmcp-bestpractices-get` | ❌ |
| 10 | 0.223532 | `azmcp-postgres-server-param-set` | ❌ |
| 11 | 0.220723 | `azmcp-sql-db-show` | ❌ |
| 12 | 0.205282 | `azmcp-redis-cache-accesspolicy-list` | ❌ |
| 13 | 0.202425 | `azmcp-deploy-iac-rules-get` | ❌ |
| 14 | 0.200326 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 15 | 0.186128 | `azmcp-loadtesting-test-get` | ❌ |
| 16 | 0.185378 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 17 | 0.167185 | `azmcp-keyvault-secret-list` | ❌ |
| 18 | 0.162892 | `azmcp-functionapp-list` | ❌ |
| 19 | 0.162568 | `azmcp-kusto-query` | ❌ |
| 20 | 0.161583 | `azmcp-appconfig-kv-list` | ❌ |

---

## Test 179

**Expected Tool:** `azmcp-storage-account-create`  
**Prompt:** Create a new storage account called testaccount123 in East US region  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.529714 | `azmcp-storage-account-create` | ✅ **EXPECTED** |
| 2 | 0.428712 | `azmcp-storage-account-details` | ❌ |
| 3 | 0.412893 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.412332 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.391586 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.374006 | `azmcp-loadtesting-test-create` | ❌ |
| 7 | 0.355049 | `azmcp-loadtesting-testresource-create` | ❌ |
| 8 | 0.346555 | `azmcp-storage-blob-list` | ❌ |
| 9 | 0.343651 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.325925 | `azmcp-keyvault-secret-create` | ❌ |
| 11 | 0.323501 | `azmcp-appconfig-kv-set` | ❌ |
| 12 | 0.319843 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.315043 | `azmcp-keyvault-key-create` | ❌ |
| 14 | 0.311744 | `azmcp-storage-blob-container-create` | ❌ |
| 15 | 0.308283 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 16 | 0.305188 | `azmcp-keyvault-certificate-create` | ❌ |
| 17 | 0.297236 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.289742 | `azmcp-appconfig-kv-show` | ❌ |
| 19 | 0.277805 | `azmcp-cosmos-database-container-list` | ❌ |
| 20 | 0.264217 | `azmcp-appconfig-kv-lock` | ❌ |

---

## Test 180

**Expected Tool:** `azmcp-storage-account-create`  
**Prompt:** Create a storage account with premium performance and LRS replication  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.492400 | `azmcp-storage-account-create` | ✅ **EXPECTED** |
| 2 | 0.403775 | `azmcp-storage-account-list` | ❌ |
| 3 | 0.401456 | `azmcp-storage-account-details` | ❌ |
| 4 | 0.369322 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.361412 | `azmcp-storage-table-list` | ❌ |
| 6 | 0.359300 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 7 | 0.355743 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.344343 | `azmcp-loadtesting-testresource-create` | ❌ |
| 9 | 0.329099 | `azmcp-loadtesting-test-create` | ❌ |
| 10 | 0.327972 | `azmcp-storage-blob-list` | ❌ |
| 11 | 0.315053 | `azmcp-storage-blob-upload` | ❌ |
| 12 | 0.310332 | `azmcp-workbooks-create` | ❌ |
| 13 | 0.302787 | `azmcp-extension-az` | ❌ |
| 14 | 0.284886 | `azmcp-bestpractices-get` | ❌ |
| 15 | 0.284467 | `azmcp-deploy-plan-get` | ❌ |
| 16 | 0.284385 | `azmcp-cosmos-account-list` | ❌ |
| 17 | 0.283067 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 18 | 0.281142 | `azmcp-appconfig-kv-lock` | ❌ |
| 19 | 0.280404 | `azmcp-keyvault-certificate-create` | ❌ |
| 20 | 0.279589 | `azmcp-keyvault-key-create` | ❌ |

---

## Test 181

**Expected Tool:** `azmcp-storage-account-create`  
**Prompt:** Create a new storage account with Data Lake Storage Gen2 enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628476 | `azmcp-storage-account-create` | ✅ **EXPECTED** |
| 2 | 0.453252 | `azmcp-storage-account-details` | ❌ |
| 3 | 0.444359 | `azmcp-storage-datalake-directory-create` | ❌ |
| 4 | 0.426606 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.424664 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.392966 | `azmcp-storage-blob-container-create` | ❌ |
| 7 | 0.389091 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.386262 | `azmcp-storage-table-list` | ❌ |
| 9 | 0.383927 | `azmcp-loadtesting-testresource-create` | ❌ |
| 10 | 0.380674 | `azmcp-keyvault-key-create` | ❌ |
| 11 | 0.380638 | `azmcp-loadtesting-test-create` | ❌ |
| 12 | 0.372357 | `azmcp-keyvault-certificate-create` | ❌ |
| 13 | 0.372115 | `azmcp-storage-blob-list` | ❌ |
| 14 | 0.366696 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 15 | 0.363721 | `azmcp-workbooks-create` | ❌ |
| 16 | 0.359330 | `azmcp-keyvault-secret-create` | ❌ |
| 17 | 0.321846 | `azmcp-deploy-plan-get` | ❌ |
| 18 | 0.309241 | `azmcp-appconfig-kv-set` | ❌ |
| 19 | 0.302875 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.301794 | `azmcp-deploy-app-logs-get` | ❌ |

---

## Test 182

**Expected Tool:** `azmcp-storage-account-details`  
**Prompt:** Show me the details for my storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.639321 | `azmcp-storage-account-details` | ✅ **EXPECTED** |
| 2 | 0.585303 | `azmcp-storage-blob-container-details` | ❌ |
| 3 | 0.561555 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.504386 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.503390 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.502723 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.463423 | `azmcp-storage-blob-details` | ❌ |
| 8 | 0.451457 | `azmcp-storage-account-create` | ❌ |
| 9 | 0.442858 | `azmcp-appconfig-kv-show` | ❌ |
| 10 | 0.439236 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.403478 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.395698 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.388466 | `azmcp-aks-cluster-get` | ❌ |
| 14 | 0.368567 | `azmcp-sql-db-show` | ❌ |
| 15 | 0.366978 | `azmcp-kusto-cluster-get` | ❌ |
| 16 | 0.356973 | `azmcp-cosmos-database-list` | ❌ |
| 17 | 0.353431 | `azmcp-loadtesting-testrun-get` | ❌ |
| 18 | 0.341278 | `azmcp-appconfig-kv-list` | ❌ |
| 19 | 0.333326 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.326892 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 183

**Expected Tool:** `azmcp-storage-account-details`  
**Prompt:** Get details about the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.669677 | `azmcp-storage-account-details` | ✅ **EXPECTED** |
| 2 | 0.609042 | `azmcp-storage-blob-container-details` | ❌ |
| 3 | 0.546837 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.497983 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.495839 | `azmcp-storage-account-create` | ❌ |
| 6 | 0.487376 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.483967 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.470913 | `azmcp-storage-blob-details` | ❌ |
| 9 | 0.415406 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.411833 | `azmcp-appconfig-kv-show` | ❌ |
| 11 | 0.375812 | `azmcp-quota-usage-check` | ❌ |
| 12 | 0.373609 | `azmcp-aks-cluster-get` | ❌ |
| 13 | 0.369799 | `azmcp-cosmos-database-container-list` | ❌ |
| 14 | 0.367978 | `azmcp-kusto-cluster-get` | ❌ |
| 15 | 0.355115 | `azmcp-servicebus-queue-details` | ❌ |
| 16 | 0.340462 | `azmcp-appconfig-kv-lock` | ❌ |
| 17 | 0.337080 | `azmcp-loadtesting-testrun-get` | ❌ |
| 18 | 0.330576 | `azmcp-cosmos-database-list` | ❌ |
| 19 | 0.323467 | `azmcp-keyvault-certificate-get` | ❌ |
| 20 | 0.317069 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 184

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** List all storage accounts in my subscription including their location and SKU  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.699415 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.581393 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.576347 | `azmcp-storage-account-details` | ❌ |
| 4 | 0.540735 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.536909 | `azmcp-cosmos-account-list` | ❌ |
| 6 | 0.501311 | `azmcp-subscription-list` | ❌ |
| 7 | 0.496742 | `azmcp-storage-blob-list` | ❌ |
| 8 | 0.496371 | `azmcp-quota-region-availability-list` | ❌ |
| 9 | 0.493246 | `azmcp-appconfig-account-list` | ❌ |
| 10 | 0.471507 | `azmcp-search-service-list` | ❌ |
| 11 | 0.459525 | `azmcp-functionapp-list` | ❌ |
| 12 | 0.458793 | `azmcp-monitor-workspace-list` | ❌ |
| 13 | 0.454207 | `azmcp-acr-registry-list` | ❌ |
| 14 | 0.448591 | `azmcp-storage-blob-container-details` | ❌ |
| 15 | 0.447992 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.432645 | `azmcp-kusto-cluster-list` | ❌ |
| 17 | 0.416387 | `azmcp-group-list` | ❌ |
| 18 | 0.412679 | `azmcp-grafana-list` | ❌ |
| 19 | 0.393518 | `azmcp-cosmos-database-list` | ❌ |
| 20 | 0.389849 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 185

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** Show me my storage accounts with whether hierarchical namespace (HNS) is enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.574590 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.498860 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.450677 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.448981 | `azmcp-storage-account-details` | ❌ |
| 5 | 0.424921 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.421642 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.419265 | `azmcp-storage-blob-container-details` | ❌ |
| 8 | 0.411558 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 9 | 0.379853 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 10 | 0.375553 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.367906 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.366021 | `azmcp-quota-usage-check` | ❌ |
| 13 | 0.362508 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.347173 | `azmcp-appconfig-account-list` | ❌ |
| 15 | 0.335306 | `azmcp-appconfig-kv-show` | ❌ |
| 16 | 0.330363 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.327776 | `azmcp-functionapp-list` | ❌ |
| 18 | 0.322108 | `azmcp-keyvault-key-list` | ❌ |
| 19 | 0.312384 | `azmcp-acr-registry-list` | ❌ |
| 20 | 0.299081 | `azmcp-keyvault-secret-list` | ❌ |

---

## Test 186

**Expected Tool:** `azmcp-storage-account-list`  
**Prompt:** Show me the storage accounts in my subscription and include HTTPS-only and public blob access settings  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.610470 | `azmcp-storage-account-list` | ✅ **EXPECTED** |
| 2 | 0.501033 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.499153 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.485850 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.484101 | `azmcp-storage-account-details` | ❌ |
| 6 | 0.473598 | `azmcp-cosmos-account-list` | ❌ |
| 7 | 0.454160 | `azmcp-subscription-list` | ❌ |
| 8 | 0.431468 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.425048 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 10 | 0.424322 | `azmcp-storage-blob-details` | ❌ |
| 11 | 0.418264 | `azmcp-search-service-list` | ❌ |
| 12 | 0.415080 | `azmcp-appconfig-account-list` | ❌ |
| 13 | 0.382963 | `azmcp-functionapp-list` | ❌ |
| 14 | 0.382504 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.376660 | `azmcp-appconfig-kv-show` | ❌ |
| 16 | 0.359998 | `azmcp-cosmos-database-list` | ❌ |
| 17 | 0.359051 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.353273 | `azmcp-cosmos-database-container-list` | ❌ |
| 19 | 0.342616 | `azmcp-bestpractices-get` | ❌ |
| 20 | 0.341127 | `azmcp-kusto-cluster-list` | ❌ |

---

## Test 187

**Expected Tool:** `azmcp-storage-blob-batch-set-tier`  
**Prompt:** Set access tier to Cool for multiple blobs in the container <container> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.678551 | `azmcp-storage-blob-batch-set-tier` | ✅ **EXPECTED** |
| 2 | 0.499700 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.473825 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.470051 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.422743 | `azmcp-storage-blob-container-create` | ❌ |
| 6 | 0.414750 | `azmcp-storage-blob-details` | ❌ |
| 7 | 0.378380 | `azmcp-cosmos-database-container-list` | ❌ |
| 8 | 0.374704 | `azmcp-storage-account-create` | ❌ |
| 9 | 0.370473 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.352885 | `azmcp-storage-account-details` | ❌ |
| 11 | 0.330453 | `azmcp-storage-blob-upload` | ❌ |
| 12 | 0.305741 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 13 | 0.297254 | `azmcp-appconfig-kv-lock` | ❌ |
| 14 | 0.295764 | `azmcp-appconfig-kv-unlock` | ❌ |
| 15 | 0.295532 | `azmcp-appconfig-kv-set` | ❌ |
| 16 | 0.286940 | `azmcp-acr-registry-repository-list` | ❌ |
| 17 | 0.285276 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.271887 | `azmcp-appconfig-kv-show` | ❌ |
| 19 | 0.255608 | `azmcp-extension-az` | ❌ |
| 20 | 0.251602 | `azmcp-deploy-app-logs-get` | ❌ |

---

## Test 188

**Expected Tool:** `azmcp-storage-blob-batch-set-tier`  
**Prompt:** Change the access tier to Archive for blobs file1.txt and file2.txt in the container <container> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.612943 | `azmcp-storage-blob-batch-set-tier` | ✅ **EXPECTED** |
| 2 | 0.446971 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.435696 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.417885 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.365232 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.360813 | `azmcp-storage-blob-details` | ❌ |
| 7 | 0.354128 | `azmcp-storage-account-create` | ❌ |
| 8 | 0.351892 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.351669 | `azmcp-storage-blob-container-create` | ❌ |
| 10 | 0.346809 | `azmcp-storage-account-details` | ❌ |
| 11 | 0.341857 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.301044 | `azmcp-acr-registry-repository-list` | ❌ |
| 13 | 0.293979 | `azmcp-appconfig-kv-lock` | ❌ |
| 14 | 0.280305 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.279325 | `azmcp-extension-az` | ❌ |
| 16 | 0.277737 | `azmcp-appconfig-kv-unlock` | ❌ |
| 17 | 0.267309 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 18 | 0.253981 | `azmcp-appconfig-kv-set` | ❌ |
| 19 | 0.246907 | `azmcp-deploy-iac-rules-get` | ❌ |
| 20 | 0.245593 | `azmcp-deploy-pipeline-guidance-get` | ❌ |

---

## Test 189

**Expected Tool:** `azmcp-storage-blob-container-create`  
**Prompt:** Create the storage container mycontainer in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.589093 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.523631 | `azmcp-storage-account-create` | ❌ |
| 3 | 0.513051 | `azmcp-storage-blob-container-details` | ❌ |
| 4 | 0.503269 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.451891 | `azmcp-storage-blob-container-create` | ✅ **EXPECTED** |
| 6 | 0.447784 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.420577 | `azmcp-storage-account-details` | ❌ |
| 8 | 0.395711 | `azmcp-storage-account-list` | ❌ |
| 9 | 0.387848 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.372547 | `azmcp-storage-blob-details` | ❌ |
| 11 | 0.335039 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 12 | 0.334422 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 13 | 0.326352 | `azmcp-appconfig-kv-set` | ❌ |
| 14 | 0.323215 | `azmcp-keyvault-secret-create` | ❌ |
| 15 | 0.319277 | `azmcp-keyvault-key-create` | ❌ |
| 16 | 0.305680 | `azmcp-keyvault-certificate-create` | ❌ |
| 17 | 0.297912 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 18 | 0.297384 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.292093 | `azmcp-acr-registry-repository-list` | ❌ |
| 20 | 0.287499 | `azmcp-appconfig-kv-lock` | ❌ |

---

## Test 190

**Expected Tool:** `azmcp-storage-blob-container-create`  
**Prompt:** Create the container using blob public access in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584071 | `azmcp-storage-blob-container-create` | ✅ **EXPECTED** |
| 2 | 0.536197 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.535799 | `azmcp-storage-blob-list` | ❌ |
| 4 | 0.514470 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.514087 | `azmcp-storage-account-create` | ❌ |
| 6 | 0.462925 | `azmcp-storage-blob-details` | ❌ |
| 7 | 0.415378 | `azmcp-cosmos-database-container-list` | ❌ |
| 8 | 0.412528 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 9 | 0.384127 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.380296 | `azmcp-storage-account-details` | ❌ |
| 11 | 0.355806 | `azmcp-storage-blob-upload` | ❌ |
| 12 | 0.320173 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 13 | 0.309739 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 14 | 0.285153 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.278047 | `azmcp-keyvault-secret-create` | ❌ |
| 16 | 0.275547 | `azmcp-keyvault-key-create` | ❌ |
| 17 | 0.275240 | `azmcp-acr-registry-repository-list` | ❌ |
| 18 | 0.270167 | `azmcp-appconfig-kv-set` | ❌ |
| 19 | 0.269625 | `azmcp-deploy-app-logs-get` | ❌ |
| 20 | 0.267205 | `azmcp-appconfig-kv-lock` | ❌ |

---

## Test 191

**Expected Tool:** `azmcp-storage-blob-container-create`  
**Prompt:** Create a new blob container named documents with container public access in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.512937 | `azmcp-storage-blob-container-create` | ✅ **EXPECTED** |
| 2 | 0.493348 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.492879 | `azmcp-storage-blob-list` | ❌ |
| 4 | 0.482788 | `azmcp-storage-account-create` | ❌ |
| 5 | 0.470357 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.435099 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.413633 | `azmcp-storage-blob-details` | ❌ |
| 8 | 0.380062 | `azmcp-storage-account-details` | ❌ |
| 9 | 0.378021 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.376114 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 11 | 0.375383 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.361102 | `azmcp-storage-account-list` | ❌ |
| 13 | 0.329038 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.322364 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.280806 | `azmcp-keyvault-certificate-create` | ❌ |
| 16 | 0.275617 | `azmcp-keyvault-secret-create` | ❌ |
| 17 | 0.269719 | `azmcp-acr-registry-repository-list` | ❌ |
| 18 | 0.266791 | `azmcp-appconfig-kv-set` | ❌ |
| 19 | 0.265433 | `azmcp-keyvault-key-create` | ❌ |
| 20 | 0.262633 | `azmcp-loadtesting-testresource-create` | ❌ |

---

## Test 192

**Expected Tool:** `azmcp-storage-blob-container-details`  
**Prompt:** Show me the properties of the storage container files in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661108 | `azmcp-storage-blob-container-list` | ❌ |
| 2 | 0.660853 | `azmcp-storage-blob-container-details` | ✅ **EXPECTED** |
| 3 | 0.597518 | `azmcp-storage-blob-list` | ❌ |
| 4 | 0.590054 | `azmcp-storage-account-details` | ❌ |
| 5 | 0.557007 | `azmcp-storage-blob-details` | ❌ |
| 6 | 0.518490 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.514436 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.496349 | `azmcp-cosmos-database-container-list` | ❌ |
| 9 | 0.443186 | `azmcp-storage-account-create` | ❌ |
| 10 | 0.419634 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.418341 | `azmcp-appconfig-kv-show` | ❌ |
| 12 | 0.410670 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 13 | 0.387558 | `azmcp-quota-usage-check` | ❌ |
| 14 | 0.365807 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.357446 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 16 | 0.355076 | `azmcp-appconfig-kv-list` | ❌ |
| 17 | 0.342947 | `azmcp-deploy-app-logs-get` | ❌ |
| 18 | 0.335755 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.334143 | `azmcp-keyvault-key-list` | ❌ |
| 20 | 0.332406 | `azmcp-acr-registry-repository-list` | ❌ |

---

## Test 193

**Expected Tool:** `azmcp-storage-blob-container-list`  
**Prompt:** List all blob containers in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.755653 | `azmcp-storage-blob-container-list` | ✅ **EXPECTED** |
| 2 | 0.732122 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.613933 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.556139 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.538446 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.530702 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.514299 | `azmcp-storage-blob-details` | ❌ |
| 8 | 0.471385 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.456826 | `azmcp-storage-account-details` | ❌ |
| 10 | 0.453044 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.431597 | `azmcp-storage-blob-container-create` | ❌ |
| 12 | 0.409820 | `azmcp-acr-registry-repository-list` | ❌ |
| 13 | 0.405238 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.386764 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 15 | 0.367207 | `azmcp-keyvault-key-list` | ❌ |
| 16 | 0.356416 | `azmcp-acr-registry-list` | ❌ |
| 17 | 0.351601 | `azmcp-keyvault-certificate-list` | ❌ |
| 18 | 0.351490 | `azmcp-keyvault-secret-list` | ❌ |
| 19 | 0.348288 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.343863 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 194

**Expected Tool:** `azmcp-storage-blob-container-list`  
**Prompt:** Show me the blob containers in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.693721 | `azmcp-storage-blob-container-list` | ✅ **EXPECTED** |
| 2 | 0.662645 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.562439 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.527117 | `azmcp-storage-blob-details` | ❌ |
| 5 | 0.524792 | `azmcp-storage-blob-container-details` | ❌ |
| 6 | 0.511431 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.498030 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.474385 | `azmcp-storage-account-details` | ❌ |
| 9 | 0.447709 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.431777 | `azmcp-storage-blob-container-create` | ❌ |
| 11 | 0.408418 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.403812 | `azmcp-storage-account-create` | ❌ |
| 13 | 0.372992 | `azmcp-acr-registry-repository-list` | ❌ |
| 14 | 0.353371 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 15 | 0.341157 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 16 | 0.339182 | `azmcp-acr-registry-list` | ❌ |
| 17 | 0.333773 | `azmcp-appconfig-kv-show` | ❌ |
| 18 | 0.322647 | `azmcp-keyvault-key-list` | ❌ |
| 19 | 0.322016 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.315834 | `azmcp-deploy-app-logs-get` | ❌ |

---

## Test 195

**Expected Tool:** `azmcp-storage-blob-details`  
**Prompt:** Show me the properties for blob <blob> in container <container> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.674069 | `azmcp-storage-blob-details` | ✅ **EXPECTED** |
| 2 | 0.647805 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.632103 | `azmcp-storage-blob-container-details` | ❌ |
| 4 | 0.588530 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.509477 | `azmcp-storage-account-details` | ❌ |
| 6 | 0.477946 | `azmcp-cosmos-database-container-list` | ❌ |
| 7 | 0.463244 | `azmcp-storage-blob-container-create` | ❌ |
| 8 | 0.433683 | `azmcp-storage-account-list` | ❌ |
| 9 | 0.431571 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.386482 | `azmcp-appconfig-kv-show` | ❌ |
| 11 | 0.375774 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 12 | 0.364846 | `azmcp-storage-account-create` | ❌ |
| 13 | 0.359392 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 14 | 0.349565 | `azmcp-cosmos-account-list` | ❌ |
| 15 | 0.323065 | `azmcp-cosmos-database-list` | ❌ |
| 16 | 0.318346 | `azmcp-deploy-app-logs-get` | ❌ |
| 17 | 0.303596 | `azmcp-appconfig-kv-list` | ❌ |
| 18 | 0.287289 | `azmcp-acr-registry-repository-list` | ❌ |
| 19 | 0.282995 | `azmcp-aks-cluster-get` | ❌ |
| 20 | 0.273533 | `azmcp-keyvault-key-list` | ❌ |

---

## Test 196

**Expected Tool:** `azmcp-storage-blob-details`  
**Prompt:** Get the details about blob <blob> in the container <container> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661140 | `azmcp-storage-blob-container-details` | ❌ |
| 2 | 0.646183 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.633882 | `azmcp-storage-blob-details` | ✅ **EXPECTED** |
| 4 | 0.578351 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.548693 | `azmcp-storage-account-details` | ❌ |
| 6 | 0.469552 | `azmcp-storage-blob-container-create` | ❌ |
| 7 | 0.453696 | `azmcp-cosmos-database-container-list` | ❌ |
| 8 | 0.434361 | `azmcp-storage-blob-upload` | ❌ |
| 9 | 0.411788 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.402875 | `azmcp-storage-account-create` | ❌ |
| 11 | 0.388809 | `azmcp-storage-table-list` | ❌ |
| 12 | 0.360712 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 13 | 0.359651 | `azmcp-aks-cluster-get` | ❌ |
| 14 | 0.353412 | `azmcp-kusto-cluster-get` | ❌ |
| 15 | 0.348551 | `azmcp-appconfig-kv-show` | ❌ |
| 16 | 0.319525 | `azmcp-keyvault-certificate-get` | ❌ |
| 17 | 0.319393 | `azmcp-deploy-app-logs-get` | ❌ |
| 18 | 0.313425 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.297249 | `azmcp-loadtesting-test-get` | ❌ |
| 20 | 0.276512 | `azmcp-marketplace-product-get` | ❌ |

---

## Test 197

**Expected Tool:** `azmcp-storage-blob-list`  
**Prompt:** List all blobs in the blob container <container> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.794699 | `azmcp-storage-blob-list` | ✅ **EXPECTED** |
| 2 | 0.702986 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.579070 | `azmcp-cosmos-database-container-list` | ❌ |
| 4 | 0.553087 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.537677 | `azmcp-storage-blob-details` | ❌ |
| 6 | 0.532154 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.507970 | `azmcp-storage-table-list` | ❌ |
| 8 | 0.454766 | `azmcp-storage-account-details` | ❌ |
| 9 | 0.452160 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.434835 | `azmcp-storage-blob-container-create` | ❌ |
| 11 | 0.415853 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.403314 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 13 | 0.400982 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.400483 | `azmcp-acr-registry-repository-list` | ❌ |
| 15 | 0.379851 | `azmcp-keyvault-key-list` | ❌ |
| 16 | 0.379099 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 17 | 0.369580 | `azmcp-keyvault-secret-list` | ❌ |
| 18 | 0.359099 | `azmcp-keyvault-certificate-list` | ❌ |
| 19 | 0.331545 | `azmcp-appconfig-kv-list` | ❌ |
| 20 | 0.328761 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 198

**Expected Tool:** `azmcp-storage-blob-list`  
**Prompt:** Show me the blobs in the blob container <container> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.701583 | `azmcp-storage-blob-list` | ✅ **EXPECTED** |
| 2 | 0.639383 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.556629 | `azmcp-storage-blob-details` | ❌ |
| 4 | 0.552927 | `azmcp-storage-blob-container-details` | ❌ |
| 5 | 0.533515 | `azmcp-cosmos-database-container-list` | ❌ |
| 6 | 0.456071 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.451056 | `azmcp-storage-blob-container-create` | ❌ |
| 8 | 0.447308 | `azmcp-storage-account-details` | ❌ |
| 9 | 0.446589 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.395809 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.385242 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 12 | 0.379405 | `azmcp-storage-account-create` | ❌ |
| 13 | 0.376224 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 14 | 0.353799 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.345263 | `azmcp-acr-registry-repository-list` | ❌ |
| 16 | 0.342766 | `azmcp-appconfig-kv-show` | ❌ |
| 17 | 0.339846 | `azmcp-deploy-app-logs-get` | ❌ |
| 18 | 0.300276 | `azmcp-acr-registry-list` | ❌ |
| 19 | 0.291436 | `azmcp-keyvault-key-list` | ❌ |
| 20 | 0.290270 | `azmcp-appconfig-kv-list` | ❌ |

---

## Test 199

**Expected Tool:** `azmcp-storage-blob-upload`  
**Prompt:** Upload file <local-file-path> to storage blob <blob> in container <container> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591025 | `azmcp-storage-blob-upload` | ✅ **EXPECTED** |
| 2 | 0.496899 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.448138 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.417903 | `azmcp-storage-blob-details` | ❌ |
| 5 | 0.411317 | `azmcp-storage-account-create` | ❌ |
| 6 | 0.404454 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.386937 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 8 | 0.360753 | `azmcp-storage-account-details` | ❌ |
| 9 | 0.333320 | `azmcp-storage-account-list` | ❌ |
| 10 | 0.329127 | `azmcp-storage-blob-container-create` | ❌ |
| 11 | 0.327564 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.324057 | `azmcp-appconfig-kv-set` | ❌ |
| 13 | 0.294735 | `azmcp-keyvault-certificate-import` | ❌ |
| 14 | 0.284943 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 15 | 0.277668 | `azmcp-appconfig-kv-lock` | ❌ |
| 16 | 0.273687 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 17 | 0.257861 | `azmcp-deploy-app-logs-get` | ❌ |
| 18 | 0.253515 | `azmcp-appconfig-kv-show` | ❌ |
| 19 | 0.239495 | `azmcp-foundry-models-deploy` | ❌ |
| 20 | 0.229595 | `azmcp-appconfig-kv-unlock` | ❌ |

---

## Test 200

**Expected Tool:** `azmcp-storage-blob-upload`  
**Prompt:** Upload the file <local-file-path> overwriting blob <blob> in container <container> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.636285 | `azmcp-storage-blob-upload` | ✅ **EXPECTED** |
| 2 | 0.453514 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.417423 | `azmcp-storage-blob-details` | ❌ |
| 4 | 0.404254 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.401476 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 6 | 0.376454 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.349088 | `azmcp-storage-account-create` | ❌ |
| 8 | 0.336021 | `azmcp-storage-blob-container-create` | ❌ |
| 9 | 0.326045 | `azmcp-storage-account-details` | ❌ |
| 10 | 0.307945 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.295250 | `azmcp-keyvault-certificate-import` | ❌ |
| 12 | 0.289509 | `azmcp-appconfig-kv-set` | ❌ |
| 13 | 0.283138 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 14 | 0.280293 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.262361 | `azmcp-appconfig-kv-lock` | ❌ |
| 16 | 0.253418 | `azmcp-deploy-app-logs-get` | ❌ |
| 17 | 0.241654 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 18 | 0.231418 | `azmcp-appconfig-kv-unlock` | ❌ |
| 19 | 0.223088 | `azmcp-appconfig-kv-delete` | ❌ |
| 20 | 0.216983 | `azmcp-acr-registry-repository-list` | ❌ |

---

## Test 201

**Expected Tool:** `azmcp-storage-blob-upload`  
**Prompt:** Overwrite <blob> with <local-file-name> in container <container> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.561515 | `azmcp-storage-blob-upload` | ✅ **EXPECTED** |
| 2 | 0.469043 | `azmcp-storage-blob-list` | ❌ |
| 3 | 0.429837 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.423858 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 5 | 0.420432 | `azmcp-storage-blob-details` | ❌ |
| 6 | 0.396448 | `azmcp-storage-blob-container-details` | ❌ |
| 7 | 0.362264 | `azmcp-storage-account-create` | ❌ |
| 8 | 0.356900 | `azmcp-storage-blob-container-create` | ❌ |
| 9 | 0.337105 | `azmcp-cosmos-database-container-list` | ❌ |
| 10 | 0.317867 | `azmcp-storage-account-details` | ❌ |
| 11 | 0.298033 | `azmcp-appconfig-kv-lock` | ❌ |
| 12 | 0.285479 | `azmcp-workbooks-delete` | ❌ |
| 13 | 0.284797 | `azmcp-appconfig-kv-set` | ❌ |
| 14 | 0.274924 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 15 | 0.260050 | `azmcp-appconfig-kv-unlock` | ❌ |
| 16 | 0.250215 | `azmcp-keyvault-certificate-import` | ❌ |
| 17 | 0.244022 | `azmcp-appconfig-kv-delete` | ❌ |
| 18 | 0.243240 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 19 | 0.227445 | `azmcp-appconfig-kv-show` | ❌ |
| 20 | 0.226549 | `azmcp-deploy-app-logs-get` | ❌ |

---

## Test 202

**Expected Tool:** `azmcp-storage-datalake-directory-create`  
**Prompt:** Create a new directory at the path <directory_path> in Data Lake in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.647078 | `azmcp-storage-datalake-directory-create` | ✅ **EXPECTED** |
| 2 | 0.466336 | `azmcp-storage-account-create` | ❌ |
| 3 | 0.457732 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 4 | 0.348428 | `azmcp-keyvault-secret-create` | ❌ |
| 5 | 0.345266 | `azmcp-storage-blob-container-list` | ❌ |
| 6 | 0.340453 | `azmcp-keyvault-certificate-create` | ❌ |
| 7 | 0.334133 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.326548 | `azmcp-storage-account-details` | ❌ |
| 9 | 0.303932 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.302849 | `azmcp-loadtesting-testresource-create` | ❌ |
| 11 | 0.297012 | `azmcp-loadtesting-test-create` | ❌ |
| 12 | 0.290297 | `azmcp-storage-blob-list` | ❌ |
| 13 | 0.286312 | `azmcp-storage-account-list` | ❌ |
| 14 | 0.281674 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 15 | 0.278175 | `azmcp-storage-blob-upload` | ❌ |
| 16 | 0.276608 | `azmcp-appconfig-kv-set` | ❌ |
| 17 | 0.274318 | `azmcp-storage-blob-container-details` | ❌ |
| 18 | 0.240515 | `azmcp-deploy-plan-get` | ❌ |
| 19 | 0.236486 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 20 | 0.231660 | `azmcp-cosmos-database-container-list` | ❌ |

---

## Test 203

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** List all paths in the Data Lake file system <file_system> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.749330 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.493736 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.481633 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.476422 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.463443 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.451714 | `azmcp-storage-datalake-directory-create` | ❌ |
| 7 | 0.419270 | `azmcp-cosmos-account-list` | ❌ |
| 8 | 0.413176 | `azmcp-storage-share-file-list` | ❌ |
| 9 | 0.403264 | `azmcp-storage-account-details` | ❌ |
| 10 | 0.402068 | `azmcp-cosmos-database-list` | ❌ |
| 11 | 0.390549 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.384296 | `azmcp-monitor-table-list` | ❌ |
| 13 | 0.374667 | `azmcp-keyvault-key-list` | ❌ |
| 14 | 0.359693 | `azmcp-storage-blob-container-details` | ❌ |
| 15 | 0.346725 | `azmcp-keyvault-secret-list` | ❌ |
| 16 | 0.344477 | `azmcp-functionapp-list` | ❌ |
| 17 | 0.344210 | `azmcp-keyvault-certificate-list` | ❌ |
| 18 | 0.337038 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.333557 | `azmcp-acr-registry-repository-list` | ❌ |
| 20 | 0.316167 | `azmcp-kusto-database-list` | ❌ |

---

## Test 204

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** Show me the paths in the Data Lake file system <file_system> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.712851 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.450082 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.433622 | `azmcp-storage-datalake-directory-create` | ❌ |
| 4 | 0.432085 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.418796 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.407068 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.403474 | `azmcp-storage-account-details` | ❌ |
| 8 | 0.378179 | `azmcp-storage-share-file-list` | ❌ |
| 9 | 0.372453 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.363485 | `azmcp-storage-blob-container-details` | ❌ |
| 11 | 0.347625 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.345916 | `azmcp-cosmos-database-list` | ❌ |
| 13 | 0.344810 | `azmcp-monitor-resource-log-query` | ❌ |
| 14 | 0.304870 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 15 | 0.304566 | `azmcp-deploy-app-logs-get` | ❌ |
| 16 | 0.304546 | `azmcp-keyvault-key-list` | ❌ |
| 17 | 0.289587 | `azmcp-acr-registry-repository-list` | ❌ |
| 18 | 0.288703 | `azmcp-keyvault-secret-list` | ❌ |
| 19 | 0.280498 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.280306 | `azmcp-appconfig-kv-show` | ❌ |

---

## Test 205

**Expected Tool:** `azmcp-storage-datalake-file-system-list-paths`  
**Prompt:** Recursively list all paths in the Data Lake file system <file_system> in the storage account <account> filtered by <filter_path>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.787426 | `azmcp-storage-datalake-file-system-list-paths` | ✅ **EXPECTED** |
| 2 | 0.459064 | `azmcp-storage-share-file-list` | ❌ |
| 3 | 0.422800 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.418199 | `azmcp-storage-datalake-directory-create` | ❌ |
| 5 | 0.413309 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.394456 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.393405 | `azmcp-storage-account-list` | ❌ |
| 8 | 0.358303 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.343302 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.341836 | `azmcp-storage-account-details` | ❌ |
| 11 | 0.337285 | `azmcp-cosmos-database-container-list` | ❌ |
| 12 | 0.337017 | `azmcp-monitor-resource-log-query` | ❌ |
| 13 | 0.333908 | `azmcp-acr-registry-repository-list` | ❌ |
| 14 | 0.328598 | `azmcp-functionapp-list` | ❌ |
| 15 | 0.323351 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 16 | 0.319029 | `azmcp-storage-blob-container-details` | ❌ |
| 17 | 0.314415 | `azmcp-keyvault-key-list` | ❌ |
| 18 | 0.299896 | `azmcp-keyvault-certificate-list` | ❌ |
| 19 | 0.294956 | `azmcp-keyvault-secret-list` | ❌ |
| 20 | 0.287275 | `azmcp-deploy-app-logs-get` | ❌ |

---

## Test 206

**Expected Tool:** `azmcp-storage-queue-message-send`  
**Prompt:** Send a message "Hello, World!" to the queue <queue> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.548475 | `azmcp-storage-queue-message-send` | ✅ **EXPECTED** |
| 2 | 0.410972 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.409851 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.407897 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.391050 | `azmcp-storage-account-details` | ❌ |
| 6 | 0.384229 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.371422 | `azmcp-storage-account-create` | ❌ |
| 8 | 0.344373 | `azmcp-servicebus-queue-details` | ❌ |
| 9 | 0.335989 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 10 | 0.328105 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.325517 | `azmcp-appconfig-kv-set` | ❌ |
| 12 | 0.321736 | `azmcp-appconfig-kv-show` | ❌ |
| 13 | 0.318411 | `azmcp-monitor-resource-log-query` | ❌ |
| 14 | 0.307333 | `azmcp-appconfig-kv-lock` | ❌ |
| 15 | 0.305274 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.302243 | `azmcp-storage-blob-container-details` | ❌ |
| 17 | 0.285295 | `azmcp-cosmos-database-list` | ❌ |
| 18 | 0.275623 | `azmcp-appconfig-kv-unlock` | ❌ |
| 19 | 0.258161 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.252408 | `azmcp-kusto-query` | ❌ |

---

## Test 207

**Expected Tool:** `azmcp-storage-queue-message-send`  
**Prompt:** Send a message with TTL of 3600 seconds to the queue <queue> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.632250 | `azmcp-storage-queue-message-send` | ✅ **EXPECTED** |
| 2 | 0.383344 | `azmcp-storage-table-list` | ❌ |
| 3 | 0.378460 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.373050 | `azmcp-servicebus-queue-details` | ❌ |
| 5 | 0.364909 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.363729 | `azmcp-storage-account-details` | ❌ |
| 7 | 0.360627 | `azmcp-storage-account-create` | ❌ |
| 8 | 0.347732 | `azmcp-storage-blob-list` | ❌ |
| 9 | 0.345318 | `azmcp-monitor-resource-log-query` | ❌ |
| 10 | 0.334472 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 11 | 0.315019 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 12 | 0.310258 | `azmcp-appconfig-kv-set` | ❌ |
| 13 | 0.294922 | `azmcp-appconfig-kv-lock` | ❌ |
| 14 | 0.282610 | `azmcp-appconfig-kv-show` | ❌ |
| 15 | 0.277782 | `azmcp-cosmos-account-list` | ❌ |
| 16 | 0.273178 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.261667 | `azmcp-appconfig-kv-unlock` | ❌ |
| 18 | 0.257323 | `azmcp-keyvault-secret-create` | ❌ |
| 19 | 0.239636 | `azmcp-kusto-query` | ❌ |
| 20 | 0.237022 | `azmcp-keyvault-key-create` | ❌ |

---

## Test 208

**Expected Tool:** `azmcp-storage-queue-message-send`  
**Prompt:** Add a message to the queue <queue> in storage account <account> with visibility timeout of 30 seconds  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.589481 | `azmcp-storage-queue-message-send` | ✅ **EXPECTED** |
| 2 | 0.363720 | `azmcp-storage-account-create` | ❌ |
| 3 | 0.360570 | `azmcp-servicebus-queue-details` | ❌ |
| 4 | 0.338676 | `azmcp-storage-blob-container-list` | ❌ |
| 5 | 0.325305 | `azmcp-appconfig-kv-set` | ❌ |
| 6 | 0.322546 | `azmcp-storage-table-list` | ❌ |
| 7 | 0.315059 | `azmcp-storage-account-details` | ❌ |
| 8 | 0.312701 | `azmcp-storage-account-list` | ❌ |
| 9 | 0.306160 | `azmcp-storage-blob-list` | ❌ |
| 10 | 0.297705 | `azmcp-storage-blob-container-details` | ❌ |
| 11 | 0.289437 | `azmcp-appconfig-kv-lock` | ❌ |
| 12 | 0.274469 | `azmcp-keyvault-secret-create` | ❌ |
| 13 | 0.273655 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 14 | 0.270448 | `azmcp-appconfig-kv-show` | ❌ |
| 15 | 0.262073 | `azmcp-cosmos-database-container-item-query` | ❌ |
| 16 | 0.247068 | `azmcp-cosmos-database-container-list` | ❌ |
| 17 | 0.245848 | `azmcp-appconfig-kv-unlock` | ❌ |
| 18 | 0.245751 | `azmcp-cosmos-account-list` | ❌ |
| 19 | 0.241303 | `azmcp-keyvault-key-create` | ❌ |
| 20 | 0.218627 | `azmcp-keyvault-certificate-create` | ❌ |

---

## Test 209

**Expected Tool:** `azmcp-storage-share-file-list`  
**Prompt:** List all files and directories in the File Share <share> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.634039 | `azmcp-storage-share-file-list` | ✅ **EXPECTED** |
| 2 | 0.576751 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.539851 | `azmcp-storage-table-list` | ❌ |
| 4 | 0.527523 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.520717 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.500769 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 7 | 0.474154 | `azmcp-storage-account-details` | ❌ |
| 8 | 0.433528 | `azmcp-cosmos-account-list` | ❌ |
| 9 | 0.425741 | `azmcp-storage-blob-container-details` | ❌ |
| 10 | 0.416549 | `azmcp-cosmos-database-container-list` | ❌ |
| 11 | 0.397963 | `azmcp-cosmos-database-list` | ❌ |
| 12 | 0.390273 | `azmcp-keyvault-key-list` | ❌ |
| 13 | 0.386402 | `azmcp-storage-account-create` | ❌ |
| 14 | 0.385400 | `azmcp-keyvault-secret-list` | ❌ |
| 15 | 0.373056 | `azmcp-keyvault-certificate-list` | ❌ |
| 16 | 0.372921 | `azmcp-acr-registry-repository-list` | ❌ |
| 17 | 0.366669 | `azmcp-subscription-list` | ❌ |
| 18 | 0.353596 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.341725 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.337938 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 210

**Expected Tool:** `azmcp-storage-share-file-list`  
**Prompt:** Show me the files in the File Share <share> directory <directory_path> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.547481 | `azmcp-storage-share-file-list` | ✅ **EXPECTED** |
| 2 | 0.497389 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 3 | 0.479129 | `azmcp-storage-blob-container-list` | ❌ |
| 4 | 0.452271 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.437409 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.428831 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.418034 | `azmcp-storage-account-details` | ❌ |
| 8 | 0.381807 | `azmcp-storage-blob-container-details` | ❌ |
| 9 | 0.380180 | `azmcp-storage-datalake-directory-create` | ❌ |
| 10 | 0.351906 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.342145 | `azmcp-storage-account-create` | ❌ |
| 12 | 0.341352 | `azmcp-cosmos-database-container-list` | ❌ |
| 13 | 0.328388 | `azmcp-appconfig-kv-show` | ❌ |
| 14 | 0.320573 | `azmcp-keyvault-secret-list` | ❌ |
| 15 | 0.317899 | `azmcp-cosmos-database-list` | ❌ |
| 16 | 0.315315 | `azmcp-keyvault-key-list` | ❌ |
| 17 | 0.304034 | `azmcp-appconfig-account-list` | ❌ |
| 18 | 0.303900 | `azmcp-acr-registry-repository-list` | ❌ |
| 19 | 0.301062 | `azmcp-keyvault-certificate-list` | ❌ |
| 20 | 0.296854 | `azmcp-cosmos-database-container-item-query` | ❌ |

---

## Test 211

**Expected Tool:** `azmcp-storage-share-file-list`  
**Prompt:** List files with prefix 'report' in the File Share <share> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.599458 | `azmcp-storage-share-file-list` | ✅ **EXPECTED** |
| 2 | 0.466117 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.449627 | `azmcp-storage-datalake-file-system-list-paths` | ❌ |
| 4 | 0.449412 | `azmcp-storage-table-list` | ❌ |
| 5 | 0.441187 | `azmcp-storage-account-list` | ❌ |
| 6 | 0.427859 | `azmcp-storage-blob-list` | ❌ |
| 7 | 0.423868 | `azmcp-extension-azqr` | ❌ |
| 8 | 0.390975 | `azmcp-storage-account-details` | ❌ |
| 9 | 0.378092 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.374357 | `azmcp-monitor-resource-log-query` | ❌ |
| 11 | 0.369171 | `azmcp-acr-registry-repository-list` | ❌ |
| 12 | 0.364292 | `azmcp-workbooks-list` | ❌ |
| 13 | 0.362241 | `azmcp-storage-blob-container-details` | ❌ |
| 14 | 0.339261 | `azmcp-cosmos-database-list` | ❌ |
| 15 | 0.336352 | `azmcp-cosmos-database-container-list` | ❌ |
| 16 | 0.332926 | `azmcp-keyvault-certificate-list` | ❌ |
| 17 | 0.320022 | `azmcp-keyvault-secret-list` | ❌ |
| 18 | 0.319475 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.314995 | `azmcp-keyvault-key-list` | ❌ |
| 20 | 0.313355 | `azmcp-functionapp-list` | ❌ |

---

## Test 212

**Expected Tool:** `azmcp-storage-table-list`  
**Prompt:** List all tables in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.787243 | `azmcp-storage-table-list` | ✅ **EXPECTED** |
| 2 | 0.627249 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.574921 | `azmcp-monitor-table-list` | ❌ |
| 4 | 0.566807 | `azmcp-storage-account-list` | ❌ |
| 5 | 0.559324 | `azmcp-storage-blob-list` | ❌ |
| 6 | 0.514042 | `azmcp-cosmos-database-list` | ❌ |
| 7 | 0.503638 | `azmcp-cosmos-database-container-list` | ❌ |
| 8 | 0.498181 | `azmcp-postgres-table-list` | ❌ |
| 9 | 0.497572 | `azmcp-monitor-table-type-list` | ❌ |
| 10 | 0.491995 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.485914 | `azmcp-kusto-table-list` | ❌ |
| 12 | 0.481347 | `azmcp-storage-account-details` | ❌ |
| 13 | 0.446216 | `azmcp-storage-blob-container-details` | ❌ |
| 14 | 0.404706 | `azmcp-kusto-database-list` | ❌ |
| 15 | 0.398213 | `azmcp-storage-account-create` | ❌ |
| 16 | 0.393482 | `azmcp-keyvault-key-list` | ❌ |
| 17 | 0.362894 | `azmcp-kusto-table-schema` | ❌ |
| 18 | 0.360786 | `azmcp-keyvault-certificate-list` | ❌ |
| 19 | 0.358239 | `azmcp-acr-registry-repository-list` | ❌ |
| 20 | 0.356593 | `azmcp-appconfig-account-list` | ❌ |

---

## Test 213

**Expected Tool:** `azmcp-storage-table-list`  
**Prompt:** Show me the tables in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.738095 | `azmcp-storage-table-list` | ✅ **EXPECTED** |
| 2 | 0.598419 | `azmcp-storage-blob-container-list` | ❌ |
| 3 | 0.532313 | `azmcp-storage-account-list` | ❌ |
| 4 | 0.522935 | `azmcp-storage-blob-list` | ❌ |
| 5 | 0.521785 | `azmcp-monitor-table-list` | ❌ |
| 6 | 0.495640 | `azmcp-storage-account-details` | ❌ |
| 7 | 0.480680 | `azmcp-cosmos-database-container-list` | ❌ |
| 8 | 0.479470 | `azmcp-monitor-table-type-list` | ❌ |
| 9 | 0.470860 | `azmcp-cosmos-database-list` | ❌ |
| 10 | 0.462051 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.454835 | `azmcp-storage-blob-container-details` | ❌ |
| 12 | 0.447530 | `azmcp-kusto-table-list` | ❌ |
| 13 | 0.423663 | `azmcp-postgres-table-list` | ❌ |
| 14 | 0.403799 | `azmcp-monitor-resource-log-query` | ❌ |
| 15 | 0.380772 | `azmcp-kusto-table-schema` | ❌ |
| 16 | 0.367981 | `azmcp-keyvault-key-list` | ❌ |
| 17 | 0.365924 | `azmcp-kusto-database-list` | ❌ |
| 18 | 0.362253 | `azmcp-kusto-sample` | ❌ |
| 19 | 0.355013 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.353853 | `azmcp-appconfig-kv-show` | ❌ |

---

## Test 214

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** List all subscriptions for my account  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.576184 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.512964 | `azmcp-cosmos-account-list` | ❌ |
| 3 | 0.473926 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.471653 | `azmcp-postgres-server-list` | ❌ |
| 5 | 0.470819 | `azmcp-search-service-list` | ❌ |
| 6 | 0.463732 | `azmcp-storage-account-list` | ❌ |
| 7 | 0.450999 | `azmcp-redis-cluster-list` | ❌ |
| 8 | 0.445724 | `azmcp-grafana-list` | ❌ |
| 9 | 0.436338 | `azmcp-storage-table-list` | ❌ |
| 10 | 0.431337 | `azmcp-kusto-cluster-list` | ❌ |
| 11 | 0.430280 | `azmcp-group-list` | ❌ |
| 12 | 0.406935 | `azmcp-appconfig-account-list` | ❌ |
| 13 | 0.394953 | `azmcp-aks-cluster-list` | ❌ |
| 14 | 0.393453 | `azmcp-functionapp-list` | ❌ |
| 15 | 0.388737 | `azmcp-monitor-workspace-list` | ❌ |
| 16 | 0.366860 | `azmcp-loadtesting-testresource-list` | ❌ |
| 17 | 0.364799 | `azmcp-storage-blob-container-list` | ❌ |
| 18 | 0.354245 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 19 | 0.348524 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 20 | 0.345154 | `azmcp-cosmos-database-list` | ❌ |

---

## Test 215

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** Show me my subscriptions  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.405863 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.381238 | `azmcp-postgres-server-list` | ❌ |
| 3 | 0.351864 | `azmcp-grafana-list` | ❌ |
| 4 | 0.351079 | `azmcp-redis-cache-list` | ❌ |
| 5 | 0.344860 | `azmcp-search-service-list` | ❌ |
| 6 | 0.341827 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.315604 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.308874 | `azmcp-appconfig-account-list` | ❌ |
| 9 | 0.303528 | `azmcp-cosmos-account-list` | ❌ |
| 10 | 0.297209 | `azmcp-group-list` | ❌ |
| 11 | 0.296282 | `azmcp-monitor-workspace-list` | ❌ |
| 12 | 0.285434 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 13 | 0.275417 | `azmcp-loadtesting-testresource-list` | ❌ |
| 14 | 0.274876 | `azmcp-aks-cluster-list` | ❌ |
| 15 | 0.274310 | `azmcp-storage-account-list` | ❌ |
| 16 | 0.272790 | `azmcp-marketplace-product-get` | ❌ |
| 17 | 0.258047 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 18 | 0.256330 | `azmcp-storage-table-list` | ❌ |
| 19 | 0.254958 | `azmcp-functionapp-list` | ❌ |
| 20 | 0.244501 | `azmcp-resourcehealth-availability-status-list` | ❌ |

---

## Test 216

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** What is my current subscription?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.320215 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.298352 | `azmcp-marketplace-product-get` | ❌ |
| 3 | 0.286859 | `azmcp-redis-cache-list` | ❌ |
| 4 | 0.285063 | `azmcp-search-service-list` | ❌ |
| 5 | 0.282645 | `azmcp-grafana-list` | ❌ |
| 6 | 0.279701 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.278798 | `azmcp-postgres-server-list` | ❌ |
| 8 | 0.256358 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.254815 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 10 | 0.252504 | `azmcp-loadtesting-testresource-list` | ❌ |
| 11 | 0.233143 | `azmcp-monitor-workspace-list` | ❌ |
| 12 | 0.230822 | `azmcp-kusto-cluster-get` | ❌ |
| 13 | 0.230571 | `azmcp-cosmos-account-list` | ❌ |
| 14 | 0.227020 | `azmcp-quota-region-availability-list` | ❌ |
| 15 | 0.222799 | `azmcp-appconfig-account-list` | ❌ |
| 16 | 0.218684 | `azmcp-aks-cluster-list` | ❌ |
| 17 | 0.216460 | `azmcp-group-list` | ❌ |
| 18 | 0.211120 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 19 | 0.210567 | `azmcp-storage-account-list` | ❌ |
| 20 | 0.198805 | `azmcp-functionapp-list` | ❌ |

---

## Test 217

**Expected Tool:** `azmcp-subscription-list`  
**Prompt:** What subscriptions do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.403432 | `azmcp-subscription-list` | ✅ **EXPECTED** |
| 2 | 0.354656 | `azmcp-redis-cache-list` | ❌ |
| 3 | 0.342355 | `azmcp-redis-cluster-list` | ❌ |
| 4 | 0.340339 | `azmcp-grafana-list` | ❌ |
| 5 | 0.336798 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.332531 | `azmcp-search-service-list` | ❌ |
| 7 | 0.304965 | `azmcp-kusto-cluster-list` | ❌ |
| 8 | 0.300478 | `azmcp-servicebus-topic-subscription-details` | ❌ |
| 9 | 0.294080 | `azmcp-monitor-workspace-list` | ❌ |
| 10 | 0.291826 | `azmcp-cosmos-account-list` | ❌ |
| 11 | 0.285774 | `azmcp-marketplace-product-get` | ❌ |
| 12 | 0.282326 | `azmcp-loadtesting-testresource-list` | ❌ |
| 13 | 0.281294 | `azmcp-appconfig-account-list` | ❌ |
| 14 | 0.269869 | `azmcp-group-list` | ❌ |
| 15 | 0.258468 | `azmcp-aks-cluster-list` | ❌ |
| 16 | 0.258410 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 17 | 0.257272 | `azmcp-functionapp-list` | ❌ |
| 18 | 0.254519 | `azmcp-storage-account-list` | ❌ |
| 19 | 0.236600 | `azmcp-quota-region-availability-list` | ❌ |
| 20 | 0.228634 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 218

**Expected Tool:** `azmcp-azureterraformbestpractices-get`  
**Prompt:** Fetch the Azure Terraform best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.719967 | `azmcp-azureterraformbestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.658343 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.625270 | `azmcp-deploy-iac-rules-get` | ❌ |
| 4 | 0.482936 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 5 | 0.466199 | `azmcp-deploy-plan-get` | ❌ |
| 6 | 0.459270 | `azmcp-extension-az` | ❌ |
| 7 | 0.389080 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 8 | 0.386480 | `azmcp-quota-usage-check` | ❌ |
| 9 | 0.372596 | `azmcp-deploy-app-logs-get` | ❌ |
| 10 | 0.354838 | `azmcp-bicepschema-get` | ❌ |
| 11 | 0.354086 | `azmcp-quota-region-availability-list` | ❌ |
| 12 | 0.332182 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 13 | 0.331791 | `azmcp-extension-azd` | ❌ |
| 14 | 0.306705 | `azmcp-storage-account-details` | ❌ |
| 15 | 0.303849 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 16 | 0.291957 | `azmcp-subscription-list` | ❌ |
| 17 | 0.279313 | `azmcp-monitor-metrics-query` | ❌ |
| 18 | 0.272676 | `azmcp-storage-blob-details` | ❌ |
| 19 | 0.269162 | `azmcp-workbooks-show` | ❌ |
| 20 | 0.267950 | `azmcp-redis-cluster-list` | ❌ |

---

## Test 219

**Expected Tool:** `azmcp-azureterraformbestpractices-get`  
**Prompt:** Show me the Azure Terraform best practices and generate code sample to get a secret from Azure Key Vault  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.596365 | `azmcp-azureterraformbestpractices-get` | ✅ **EXPECTED** |
| 2 | 0.551575 | `azmcp-bestpractices-get` | ❌ |
| 3 | 0.509973 | `azmcp-deploy-iac-rules-get` | ❌ |
| 4 | 0.444309 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 5 | 0.440195 | `azmcp-keyvault-secret-list` | ❌ |
| 6 | 0.439732 | `azmcp-keyvault-secret-create` | ❌ |
| 7 | 0.429033 | `azmcp-keyvault-certificate-get` | ❌ |
| 8 | 0.406462 | `azmcp-extension-az` | ❌ |
| 9 | 0.389625 | `azmcp-keyvault-key-list` | ❌ |
| 10 | 0.381489 | `azmcp-keyvault-certificate-create` | ❌ |
| 11 | 0.304955 | `azmcp-quota-usage-check` | ❌ |
| 12 | 0.300823 | `azmcp-quota-region-availability-list` | ❌ |
| 13 | 0.290442 | `azmcp-storage-account-details` | ❌ |
| 14 | 0.275098 | `azmcp-subscription-list` | ❌ |
| 15 | 0.274848 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 16 | 0.264689 | `azmcp-storage-account-create` | ❌ |
| 17 | 0.264628 | `azmcp-monitor-resource-log-query` | ❌ |
| 18 | 0.254648 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 19 | 0.253320 | `azmcp-sql-db-show` | ❌ |
| 20 | 0.251395 | `azmcp-monitor-metrics-query` | ❌ |

---

## Test 220

**Expected Tool:** `azmcp-virtualdesktop-hostpool-list`  
**Prompt:** List all host pools in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713689 | `azmcp-virtualdesktop-hostpool-list` | ✅ **EXPECTED** |
| 2 | 0.658080 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 3 | 0.566615 | `azmcp-kusto-cluster-list` | ❌ |
| 4 | 0.557529 | `azmcp-search-service-list` | ❌ |
| 5 | 0.536583 | `azmcp-redis-cluster-list` | ❌ |
| 6 | 0.535739 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 7 | 0.528358 | `azmcp-sql-elastic-pool-list` | ❌ |
| 8 | 0.527948 | `azmcp-postgres-server-list` | ❌ |
| 9 | 0.525796 | `azmcp-aks-cluster-list` | ❌ |
| 10 | 0.506591 | `azmcp-redis-cache-list` | ❌ |
| 11 | 0.505322 | `azmcp-subscription-list` | ❌ |
| 12 | 0.496297 | `azmcp-cosmos-account-list` | ❌ |
| 13 | 0.495490 | `azmcp-grafana-list` | ❌ |
| 14 | 0.492515 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.476718 | `azmcp-group-list` | ❌ |
| 16 | 0.474643 | `azmcp-functionapp-list` | ❌ |
| 17 | 0.460429 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.459250 | `azmcp-appconfig-account-list` | ❌ |
| 19 | 0.456137 | `azmcp-kusto-database-list` | ❌ |
| 20 | 0.431475 | `azmcp-datadog-monitoredresources-list` | ❌ |

---

## Test 221

**Expected Tool:** `azmcp-virtualdesktop-hostpool-sessionhost-list`  
**Prompt:** List all session hosts in host pool <hostpool_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.736121 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ✅ **EXPECTED** |
| 2 | 0.714469 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ❌ |
| 3 | 0.590273 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 4 | 0.397910 | `azmcp-sql-elastic-pool-list` | ❌ |
| 5 | 0.364696 | `azmcp-postgres-server-list` | ❌ |
| 6 | 0.337570 | `azmcp-redis-cluster-list` | ❌ |
| 7 | 0.335295 | `azmcp-monitor-workspace-list` | ❌ |
| 8 | 0.333517 | `azmcp-kusto-cluster-list` | ❌ |
| 9 | 0.332962 | `azmcp-keyvault-secret-list` | ❌ |
| 10 | 0.330896 | `azmcp-aks-cluster-list` | ❌ |
| 11 | 0.329287 | `azmcp-search-service-list` | ❌ |
| 12 | 0.328623 | `azmcp-keyvault-key-list` | ❌ |
| 13 | 0.321994 | `azmcp-subscription-list` | ❌ |
| 14 | 0.319942 | `azmcp-storage-account-list` | ❌ |
| 15 | 0.312156 | `azmcp-keyvault-certificate-list` | ❌ |
| 16 | 0.311262 | `azmcp-grafana-list` | ❌ |
| 17 | 0.302706 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.291590 | `azmcp-loadtesting-testrun-list` | ❌ |
| 19 | 0.291489 | `azmcp-appconfig-account-list` | ❌ |
| 20 | 0.289759 | `azmcp-functionapp-list` | ❌ |

---

## Test 222

**Expected Tool:** `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list`  
**Prompt:** List all user sessions on session host <sessionhost_name> in host pool <hostpool_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.811913 | `azmcp-virtualdesktop-hostpool-sessionhost-usersession-list` | ✅ **EXPECTED** |
| 2 | 0.665286 | `azmcp-virtualdesktop-hostpool-sessionhost-list` | ❌ |
| 3 | 0.512721 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 4 | 0.335608 | `azmcp-monitor-workspace-list` | ❌ |
| 5 | 0.329329 | `azmcp-sql-elastic-pool-list` | ❌ |
| 6 | 0.321881 | `azmcp-subscription-list` | ❌ |
| 7 | 0.315344 | `azmcp-loadtesting-testrun-list` | ❌ |
| 8 | 0.315067 | `azmcp-postgres-server-list` | ❌ |
| 9 | 0.303653 | `azmcp-monitor-table-list` | ❌ |
| 10 | 0.303257 | `azmcp-workbooks-list` | ❌ |
| 11 | 0.302260 | `azmcp-aks-cluster-list` | ❌ |
| 12 | 0.298842 | `azmcp-keyvault-secret-list` | ❌ |
| 13 | 0.294933 | `azmcp-grafana-list` | ❌ |
| 14 | 0.294614 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 15 | 0.277195 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 16 | 0.275366 | `azmcp-keyvault-key-list` | ❌ |
| 17 | 0.274607 | `azmcp-cosmos-account-list` | ❌ |
| 18 | 0.273811 | `azmcp-kusto-cluster-list` | ❌ |
| 19 | 0.272386 | `azmcp-loadtesting-testrun-get` | ❌ |
| 20 | 0.269970 | `azmcp-keyvault-certificate-list` | ❌ |

---

## Test 223

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
| 7 | 0.217974 | `azmcp-keyvault-key-create` | ❌ |
| 8 | 0.214818 | `azmcp-keyvault-certificate-create` | ❌ |
| 9 | 0.195255 | `azmcp-storage-account-create` | ❌ |
| 10 | 0.188137 | `azmcp-loadtesting-testresource-create` | ❌ |
| 11 | 0.172751 | `azmcp-monitor-table-list` | ❌ |
| 12 | 0.169440 | `azmcp-grafana-list` | ❌ |
| 13 | 0.148897 | `azmcp-loadtesting-test-create` | ❌ |
| 14 | 0.147365 | `azmcp-monitor-workspace-list` | ❌ |
| 15 | 0.142879 | `azmcp-storage-datalake-directory-create` | ❌ |
| 16 | 0.138518 | `azmcp-monitor-table-type-list` | ❌ |
| 17 | 0.130524 | `azmcp-loadtesting-testrun-create` | ❌ |
| 18 | 0.130339 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 19 | 0.116803 | `azmcp-loadtesting-testrun-update` | ❌ |
| 20 | 0.113882 | `azmcp-deploy-plan-get` | ❌ |

---

## Test 224

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
| 9 | 0.186665 | `azmcp-quota-region-availability-list` | ❌ |
| 10 | 0.186572 | `azmcp-monitor-workspace-log-query` | ❌ |
| 11 | 0.157219 | `azmcp-monitor-workspace-list` | ❌ |
| 12 | 0.155100 | `azmcp-monitor-metrics-query` | ❌ |
| 13 | 0.148882 | `azmcp-extension-azqr` | ❌ |
| 14 | 0.145141 | `azmcp-loadtesting-testresource-list` | ❌ |
| 15 | 0.134979 | `azmcp-loadtesting-testrun-update` | ❌ |
| 16 | 0.132504 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.131813 | `azmcp-group-list` | ❌ |
| 18 | 0.126273 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 19 | 0.122647 | `azmcp-marketplace-product-get` | ❌ |
| 20 | 0.120291 | `azmcp-loadtesting-test-get` | ❌ |

---

## Test 225

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
| 7 | 0.459976 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.454210 | `azmcp-monitor-workspace-list` | ❌ |
| 9 | 0.439945 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 10 | 0.416566 | `azmcp-monitor-table-list` | ❌ |
| 11 | 0.413409 | `azmcp-sql-db-list` | ❌ |
| 12 | 0.405963 | `azmcp-loadtesting-testresource-list` | ❌ |
| 13 | 0.405135 | `azmcp-redis-cluster-list` | ❌ |
| 14 | 0.399758 | `azmcp-acr-registry-repository-list` | ❌ |
| 15 | 0.387277 | `azmcp-virtualdesktop-hostpool-list` | ❌ |
| 16 | 0.366269 | `azmcp-functionapp-list` | ❌ |
| 17 | 0.362685 | `azmcp-acr-registry-list` | ❌ |
| 18 | 0.352940 | `azmcp-cosmos-database-list` | ❌ |
| 19 | 0.349674 | `azmcp-cosmos-account-list` | ❌ |
| 20 | 0.344606 | `azmcp-monitor-metrics-definitions` | ❌ |

---

## Test 226

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
| 7 | 0.425426 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 8 | 0.422785 | `azmcp-resourcehealth-availability-status-list` | ❌ |
| 9 | 0.421646 | `azmcp-group-list` | ❌ |
| 10 | 0.392371 | `azmcp-loadtesting-testresource-list` | ❌ |
| 11 | 0.371201 | `azmcp-redis-cluster-list` | ❌ |
| 12 | 0.363744 | `azmcp-sql-db-list` | ❌ |
| 13 | 0.362606 | `azmcp-monitor-table-list` | ❌ |
| 14 | 0.358317 | `azmcp-monitor-table-type-list` | ❌ |
| 15 | 0.350839 | `azmcp-acr-registry-repository-list` | ❌ |
| 16 | 0.338282 | `azmcp-acr-registry-list` | ❌ |
| 17 | 0.334580 | `azmcp-extension-azqr` | ❌ |
| 18 | 0.322215 | `azmcp-functionapp-list` | ❌ |
| 19 | 0.313199 | `azmcp-extension-az` | ❌ |
| 20 | 0.302515 | `azmcp-monitor-metrics-definitions` | ❌ |

---

## Test 227

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
| 7 | 0.277807 | `azmcp-quota-region-availability-list` | ❌ |
| 8 | 0.256678 | `azmcp-quota-usage-check` | ❌ |
| 9 | 0.242738 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 10 | 0.238836 | `azmcp-monitor-resource-log-query` | ❌ |
| 11 | 0.235477 | `azmcp-marketplace-product-get` | ❌ |
| 12 | 0.230558 | `azmcp-monitor-metrics-query` | ❌ |
| 13 | 0.230516 | `azmcp-monitor-metrics-definitions` | ❌ |
| 14 | 0.226385 | `azmcp-loadtesting-test-get` | ❌ |
| 15 | 0.218999 | `azmcp-loadtesting-testresource-list` | ❌ |
| 16 | 0.207693 | `azmcp-datadog-monitoredresources-list` | ❌ |
| 17 | 0.195751 | `azmcp-monitor-healthmodels-entity-gethealth` | ❌ |
| 18 | 0.195373 | `azmcp-group-list` | ❌ |
| 19 | 0.194010 | `azmcp-loadtesting-testrun-get` | ❌ |
| 20 | 0.189657 | `azmcp-extension-azqr` | ❌ |

---

## Test 228

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
| 17 | 0.146056 | `azmcp-kusto-table-schema` | ❌ |
| 18 | 0.143754 | `azmcp-loadtesting-testrun-get` | ❌ |
| 19 | 0.141559 | `azmcp-foundry-models-list` | ❌ |
| 20 | 0.138897 | `azmcp-loadtesting-testrun-list` | ❌ |

---

## Test 229

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
| 8 | 0.155789 | `azmcp-storage-blob-upload` | ❌ |
| 9 | 0.145788 | `azmcp-storage-blob-batch-set-tier` | ❌ |
| 10 | 0.144812 | `azmcp-extension-az` | ❌ |
| 11 | 0.142404 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 12 | 0.142186 | `azmcp-loadtesting-testrun-create` | ❌ |
| 13 | 0.138354 | `azmcp-appconfig-kv-set` | ❌ |
| 14 | 0.136105 | `azmcp-loadtesting-testresource-create` | ❌ |
| 15 | 0.131007 | `azmcp-postgres-database-query` | ❌ |
| 16 | 0.129973 | `azmcp-postgres-server-param-set` | ❌ |
| 17 | 0.129660 | `azmcp-deploy-iac-rules-get` | ❌ |
| 18 | 0.124878 | `azmcp-appconfig-kv-unlock` | ❌ |
| 19 | 0.121289 | `azmcp-monitor-workspace-log-query` | ❌ |
| 20 | 0.115996 | `azmcp-appconfig-kv-lock` | ❌ |

---

## Test 230

**Expected Tool:** `azmcp-bicepschema-get`  
**Prompt:** How can I use Bicep to create an Azure OpenAI service?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.485889 | `azmcp-deploy-iac-rules-get` | ❌ |
| 2 | 0.440302 | `azmcp-deploy-pipeline-guidance-get` | ❌ |
| 3 | 0.432773 | `azmcp-deploy-plan-get` | ❌ |
| 4 | 0.432409 | `azmcp-bicepschema-get` | ✅ **EXPECTED** |
| 5 | 0.401162 | `azmcp-extension-az` | ❌ |
| 6 | 0.400985 | `azmcp-foundry-models-deploy` | ❌ |
| 7 | 0.398046 | `azmcp-deploy-architecture-diagram-generate` | ❌ |
| 8 | 0.394677 | `azmcp-bestpractices-get` | ❌ |
| 9 | 0.375228 | `azmcp-azureterraformbestpractices-get` | ❌ |
| 10 | 0.363171 | `azmcp-search-index-list` | ❌ |
| 11 | 0.345030 | `azmcp-search-service-list` | ❌ |
| 12 | 0.342237 | `azmcp-foundry-models-deployments-list` | ❌ |
| 13 | 0.335700 | `azmcp-search-index-query` | ❌ |
| 14 | 0.303518 | `azmcp-search-index-describe` | ❌ |
| 15 | 0.303183 | `azmcp-quota-usage-check` | ❌ |
| 16 | 0.286416 | `azmcp-storage-account-create` | ❌ |
| 17 | 0.280207 | `azmcp-workbooks-delete` | ❌ |
| 18 | 0.275781 | `azmcp-resourcehealth-availability-status-get` | ❌ |
| 19 | 0.268139 | `azmcp-workbooks-create` | ❌ |
| 20 | 0.265709 | `azmcp-storage-blob-upload` | ❌ |

---

## Summary

**Total Prompts Tested:** 230  
**Execution Time:** 67.9446964s  

### Success Rate Metrics

**Top Choice Success:** 85.2% (196/230 tests)  

#### Confidence Level Distribution

**💪 Very High Confidence (≥0.8):** 5.7% (13/230 tests)  
**🎯 High Confidence (≥0.7):** 26.5% (61/230 tests)  
**✅ Good Confidence (≥0.6):** 60.9% (140/230 tests)  
**👍 Fair Confidence (≥0.5):** 86.1% (198/230 tests)  
**👌 Acceptable Confidence (≥0.4):** 94.8% (218/230 tests)  
**❌ Low Confidence (<0.4):** 5.2% (12/230 tests)  

#### Top Choice + Confidence Combinations

**💪 Top Choice + Very High Confidence (≥0.8):** 5.7% (13/230 tests)  
**🎯 Top Choice + High Confidence (≥0.7):** 26.5% (61/230 tests)  
**✅ Top Choice + Good Confidence (≥0.6):** 59.1% (136/230 tests)  
**👍 Top Choice + Fair Confidence (≥0.5):** 79.1% (182/230 tests)  
**👌 Top Choice + Acceptable Confidence (≥0.4):** 83.9% (193/230 tests)  

### Success Rate Analysis

🟡 **Good** - The tool selection system is performing adequately but has room for improvement.

⚠️ **Recommendation:** Tool descriptions need improvement to better match user intent (targets: ≥0.6 good, ≥0.7 high).

