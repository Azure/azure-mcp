# Tool Selection Analysis Setup

**Setup completed:** 2025-08-20 17:08:04  
**Tool count:** 118  
**Database setup time:** 1.3234649s  

---

# Tool Selection Analysis Results

**Analysis Date:** 2025-08-20 17:08:04  
**Tool count:** 118  

## Table of Contents

- [Test 1: azmcp_foundry_models_deploy](#test-1)
- [Test 2: azmcp_foundry_models_deployments_list](#test-2)
- [Test 3: azmcp_foundry_models_deployments_list](#test-3)
- [Test 4: azmcp_foundry_models_list](#test-4)
- [Test 5: azmcp_foundry_models_list](#test-5)
- [Test 6: azmcp_search_index_describe](#test-6)
- [Test 7: azmcp_search_index_list](#test-7)
- [Test 8: azmcp_search_index_list](#test-8)
- [Test 9: azmcp_search_index_query](#test-9)
- [Test 10: azmcp_search_service_list](#test-10)
- [Test 11: azmcp_search_service_list](#test-11)
- [Test 12: azmcp_search_service_list](#test-12)
- [Test 13: azmcp_appconfig_account_list](#test-13)
- [Test 14: azmcp_appconfig_account_list](#test-14)
- [Test 15: azmcp_appconfig_account_list](#test-15)
- [Test 16: azmcp_appconfig_kv_delete](#test-16)
- [Test 17: azmcp_appconfig_kv_list](#test-17)
- [Test 18: azmcp_appconfig_kv_list](#test-18)
- [Test 19: azmcp_appconfig_kv_lock](#test-19)
- [Test 20: azmcp_appconfig_kv_set](#test-20)
- [Test 21: azmcp_appconfig_kv_show](#test-21)
- [Test 22: azmcp_appconfig_kv_unlock](#test-22)
- [Test 23: azmcp_extension_az](#test-23)
- [Test 24: azmcp_extension_az](#test-24)
- [Test 25: azmcp_extension_az](#test-25)
- [Test 26: azmcp_acr_registry_list](#test-26)
- [Test 27: azmcp_acr_registry_list](#test-27)
- [Test 28: azmcp_acr_registry_list](#test-28)
- [Test 29: azmcp_acr_registry_list](#test-29)
- [Test 30: azmcp_acr_registry_list](#test-30)
- [Test 31: azmcp_acr_registry_repository_list](#test-31)
- [Test 32: azmcp_acr_registry_repository_list](#test-32)
- [Test 33: azmcp_acr_registry_repository_list](#test-33)
- [Test 34: azmcp_acr_registry_repository_list](#test-34)
- [Test 35: azmcp_cosmos_account_list](#test-35)
- [Test 36: azmcp_cosmos_account_list](#test-36)
- [Test 37: azmcp_cosmos_account_list](#test-37)
- [Test 38: azmcp_cosmos_database_container_item_query](#test-38)
- [Test 39: azmcp_cosmos_database_container_list](#test-39)
- [Test 40: azmcp_cosmos_database_container_list](#test-40)
- [Test 41: azmcp_cosmos_database_list](#test-41)
- [Test 42: azmcp_cosmos_database_list](#test-42)
- [Test 43: azmcp_kusto_cluster_get](#test-43)
- [Test 44: azmcp_kusto_cluster_list](#test-44)
- [Test 45: azmcp_kusto_cluster_list](#test-45)
- [Test 46: azmcp_kusto_cluster_list](#test-46)
- [Test 47: azmcp_kusto_database_list](#test-47)
- [Test 48: azmcp_kusto_database_list](#test-48)
- [Test 49: azmcp_kusto_query](#test-49)
- [Test 50: azmcp_kusto_sample](#test-50)
- [Test 51: azmcp_kusto_table_list](#test-51)
- [Test 52: azmcp_kusto_table_list](#test-52)
- [Test 53: azmcp_kusto_table_schema](#test-53)
- [Test 54: azmcp_postgres_database_list](#test-54)
- [Test 55: azmcp_postgres_database_list](#test-55)
- [Test 56: azmcp_postgres_database_query](#test-56)
- [Test 57: azmcp_postgres_server_config_get](#test-57)
- [Test 58: azmcp_postgres_server_list](#test-58)
- [Test 59: azmcp_postgres_server_list](#test-59)
- [Test 60: azmcp_postgres_server_list](#test-60)
- [Test 61: azmcp_postgres_server_param](#test-61)
- [Test 62: azmcp_postgres_server_param_set](#test-62)
- [Test 63: azmcp_postgres_table_list](#test-63)
- [Test 64: azmcp_postgres_table_list](#test-64)
- [Test 65: azmcp_postgres_table_schema_get](#test-65)
- [Test 66: azmcp_extension_azd](#test-66)
- [Test 67: azmcp_extension_azd](#test-67)
- [Test 68: azmcp_deploy_app_logs_get](#test-68)
- [Test 69: azmcp_deploy_architecture_diagram_generate](#test-69)
- [Test 70: azmcp_deploy_iac_rules_get](#test-70)
- [Test 71: azmcp_deploy_pipeline_guidance_get](#test-71)
- [Test 72: azmcp_deploy_plan_get](#test-72)
- [Test 73: azmcp_functionapp_list](#test-73)
- [Test 74: azmcp_functionapp_list](#test-74)
- [Test 75: azmcp_functionapp_list](#test-75)
- [Test 76: azmcp_keyvault_certificate_create](#test-76)
- [Test 77: azmcp_keyvault_certificate_get](#test-77)
- [Test 78: azmcp_keyvault_certificate_get](#test-78)
- [Test 79: azmcp_keyvault_certificate_import](#test-79)
- [Test 80: azmcp_keyvault_certificate_import](#test-80)
- [Test 81: azmcp_keyvault_certificate_list](#test-81)
- [Test 82: azmcp_keyvault_certificate_list](#test-82)
- [Test 83: azmcp_keyvault_key_create](#test-83)
- [Test 84: azmcp_keyvault_key_list](#test-84)
- [Test 85: azmcp_keyvault_key_list](#test-85)
- [Test 86: azmcp_keyvault_secret_create](#test-86)
- [Test 87: azmcp_keyvault_secret_list](#test-87)
- [Test 88: azmcp_keyvault_secret_list](#test-88)
- [Test 89: azmcp_aks_cluster_get](#test-89)
- [Test 90: azmcp_aks_cluster_get](#test-90)
- [Test 91: azmcp_aks_cluster_get](#test-91)
- [Test 92: azmcp_aks_cluster_get](#test-92)
- [Test 93: azmcp_aks_cluster_list](#test-93)
- [Test 94: azmcp_aks_cluster_list](#test-94)
- [Test 95: azmcp_aks_cluster_list](#test-95)
- [Test 96: azmcp_loadtesting_test_create](#test-96)
- [Test 97: azmcp_loadtesting_test_get](#test-97)
- [Test 98: azmcp_loadtesting_testresource_create](#test-98)
- [Test 99: azmcp_loadtesting_testresource_list](#test-99)
- [Test 100: azmcp_loadtesting_testrun_create](#test-100)
- [Test 101: azmcp_loadtesting_testrun_get](#test-101)
- [Test 102: azmcp_loadtesting_testrun_list](#test-102)
- [Test 103: azmcp_loadtesting_testrun_update](#test-103)
- [Test 104: azmcp_grafana_list](#test-104)
- [Test 105: azmcp_marketplace_product_get](#test-105)
- [Test 106: azmcp_bestpractices_get](#test-106)
- [Test 107: azmcp_bestpractices_get](#test-107)
- [Test 108: azmcp_bestpractices_get](#test-108)
- [Test 109: azmcp_bestpractices_get](#test-109)
- [Test 110: azmcp_bestpractices_get](#test-110)
- [Test 111: azmcp_bestpractices_get](#test-111)
- [Test 112: azmcp_bestpractices_get](#test-112)
- [Test 113: azmcp_bestpractices_get](#test-113)
- [Test 114: azmcp_bestpractices_get](#test-114)
- [Test 115: azmcp_bestpractices_get](#test-115)
- [Test 116: azmcp_monitor_healthmodels_entity_gethealth](#test-116)
- [Test 117: azmcp_monitor_metrics_definitions](#test-117)
- [Test 118: azmcp_monitor_metrics_definitions](#test-118)
- [Test 119: azmcp_monitor_metrics_definitions](#test-119)
- [Test 120: azmcp_monitor_metrics_query](#test-120)
- [Test 121: azmcp_monitor_metrics_query](#test-121)
- [Test 122: azmcp_monitor_metrics_query](#test-122)
- [Test 123: azmcp_monitor_metrics_query](#test-123)
- [Test 124: azmcp_monitor_metrics_query](#test-124)
- [Test 125: azmcp_monitor_metrics_query](#test-125)
- [Test 126: azmcp_monitor_resource_log_query](#test-126)
- [Test 127: azmcp_monitor_table_list](#test-127)
- [Test 128: azmcp_monitor_table_list](#test-128)
- [Test 129: azmcp_monitor_table_type_list](#test-129)
- [Test 130: azmcp_monitor_table_type_list](#test-130)
- [Test 131: azmcp_monitor_workspace_list](#test-131)
- [Test 132: azmcp_monitor_workspace_list](#test-132)
- [Test 133: azmcp_monitor_workspace_list](#test-133)
- [Test 134: azmcp_monitor_workspace_log_query](#test-134)
- [Test 135: azmcp_datadog_monitoredresources_list](#test-135)
- [Test 136: azmcp_datadog_monitoredresources_list](#test-136)
- [Test 137: azmcp_extension_azqr](#test-137)
- [Test 138: azmcp_extension_azqr](#test-138)
- [Test 139: azmcp_extension_azqr](#test-139)
- [Test 140: azmcp_quota_region_availability_list](#test-140)
- [Test 141: azmcp_quota_usage_check](#test-141)
- [Test 142: azmcp_role_assignment_list](#test-142)
- [Test 143: azmcp_role_assignment_list](#test-143)
- [Test 144: azmcp_redis_cache_accesspolicy_list](#test-144)
- [Test 145: azmcp_redis_cache_accesspolicy_list](#test-145)
- [Test 146: azmcp_redis_cache_list](#test-146)
- [Test 147: azmcp_redis_cache_list](#test-147)
- [Test 148: azmcp_redis_cache_list](#test-148)
- [Test 149: azmcp_redis_cluster_database_list](#test-149)
- [Test 150: azmcp_redis_cluster_database_list](#test-150)
- [Test 151: azmcp_redis_cluster_list](#test-151)
- [Test 152: azmcp_redis_cluster_list](#test-152)
- [Test 153: azmcp_redis_cluster_list](#test-153)
- [Test 154: azmcp_group_list](#test-154)
- [Test 155: azmcp_group_list](#test-155)
- [Test 156: azmcp_group_list](#test-156)
- [Test 157: azmcp_resourcehealth_availability-status_get](#test-157)
- [Test 158: azmcp_resourcehealth_availability-status_get](#test-158)
- [Test 159: azmcp_resourcehealth_availability-status_get](#test-159)
- [Test 160: azmcp_resourcehealth_availability-status_list](#test-160)
- [Test 161: azmcp_resourcehealth_availability-status_list](#test-161)
- [Test 162: azmcp_resourcehealth_availability-status_list](#test-162)
- [Test 163: azmcp_servicebus_queue_details](#test-163)
- [Test 164: azmcp_servicebus_topic_details](#test-164)
- [Test 165: azmcp_servicebus_topic_subscription_details](#test-165)
- [Test 166: azmcp_sql_db_list](#test-166)
- [Test 167: azmcp_sql_db_list](#test-167)
- [Test 168: azmcp_sql_db_show](#test-168)
- [Test 169: azmcp_sql_db_show](#test-169)
- [Test 170: azmcp_sql_elastic-pool_list](#test-170)
- [Test 171: azmcp_sql_elastic-pool_list](#test-171)
- [Test 172: azmcp_sql_elastic-pool_list](#test-172)
- [Test 173: azmcp_sql_server_entra-admin_list](#test-173)
- [Test 174: azmcp_sql_server_entra-admin_list](#test-174)
- [Test 175: azmcp_sql_server_entra-admin_list](#test-175)
- [Test 176: azmcp_sql_server_firewall-rule_list](#test-176)
- [Test 177: azmcp_sql_server_firewall-rule_list](#test-177)
- [Test 178: azmcp_sql_server_firewall-rule_list](#test-178)
- [Test 179: azmcp_storage_account_create](#test-179)
- [Test 180: azmcp_storage_account_create](#test-180)
- [Test 181: azmcp_storage_account_create](#test-181)
- [Test 182: azmcp_storage_account_details](#test-182)
- [Test 183: azmcp_storage_account_details](#test-183)
- [Test 184: azmcp_storage_account_list](#test-184)
- [Test 185: azmcp_storage_account_list](#test-185)
- [Test 186: azmcp_storage_account_list](#test-186)
- [Test 187: azmcp_storage_blob_batch_set-tier](#test-187)
- [Test 188: azmcp_storage_blob_batch_set-tier](#test-188)
- [Test 189: azmcp_storage_blob_container_create](#test-189)
- [Test 190: azmcp_storage_blob_container_create](#test-190)
- [Test 191: azmcp_storage_blob_container_create](#test-191)
- [Test 192: azmcp_storage_blob_container_details](#test-192)
- [Test 193: azmcp_storage_blob_container_list](#test-193)
- [Test 194: azmcp_storage_blob_container_list](#test-194)
- [Test 195: azmcp_storage_blob_details](#test-195)
- [Test 196: azmcp_storage_blob_details](#test-196)
- [Test 197: azmcp_storage_blob_list](#test-197)
- [Test 198: azmcp_storage_blob_list](#test-198)
- [Test 199: azmcp_storage_blob_upload](#test-199)
- [Test 200: azmcp_storage_blob_upload](#test-200)
- [Test 201: azmcp_storage_blob_upload](#test-201)
- [Test 202: azmcp_storage_datalake_directory_create](#test-202)
- [Test 203: azmcp_storage_datalake_file-system_list-paths](#test-203)
- [Test 204: azmcp_storage_datalake_file-system_list-paths](#test-204)
- [Test 205: azmcp_storage_datalake_file-system_list-paths](#test-205)
- [Test 206: azmcp_storage_queue_message_send](#test-206)
- [Test 207: azmcp_storage_queue_message_send](#test-207)
- [Test 208: azmcp_storage_queue_message_send](#test-208)
- [Test 209: azmcp_storage_share_file_list](#test-209)
- [Test 210: azmcp_storage_share_file_list](#test-210)
- [Test 211: azmcp_storage_share_file_list](#test-211)
- [Test 212: azmcp_storage_table_list](#test-212)
- [Test 213: azmcp_storage_table_list](#test-213)
- [Test 214: azmcp_subscription_list](#test-214)
- [Test 215: azmcp_subscription_list](#test-215)
- [Test 216: azmcp_subscription_list](#test-216)
- [Test 217: azmcp_subscription_list](#test-217)
- [Test 218: azmcp_azureterraformbestpractices_get](#test-218)
- [Test 219: azmcp_azureterraformbestpractices_get](#test-219)
- [Test 220: azmcp_virtualdesktop_hostpool_list](#test-220)
- [Test 221: azmcp_virtualdesktop_hostpool_sessionhost_list](#test-221)
- [Test 222: azmcp_virtualdesktop_hostpool_sessionhost_usersession-list](#test-222)
- [Test 223: azmcp_workbooks_create](#test-223)
- [Test 224: azmcp_workbooks_delete](#test-224)
- [Test 225: azmcp_workbooks_list](#test-225)
- [Test 226: azmcp_workbooks_list](#test-226)
- [Test 227: azmcp_workbooks_show](#test-227)
- [Test 228: azmcp_workbooks_show](#test-228)
- [Test 229: azmcp_workbooks_update](#test-229)
- [Test 230: azmcp_bicepschema_get](#test-230)

---

## Test 1

**Expected Tool:** `azmcp_foundry_models_deploy`  
**Prompt:** Deploy a GPT4o instance on my resource <resource-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.313400 | `azmcp_foundry_models_deploy` | ✅ **EXPECTED** |
| 2 | 0.274011 | `azmcp_deploy_plan_get` | ❌ |
| 3 | 0.269513 | `azmcp_loadtesting_testresource_create` | ❌ |
| 4 | 0.268967 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 5 | 0.234071 | `azmcp_deploy_iac_rules_get` | ❌ |
| 6 | 0.222504 | `azmcp_grafana_list` | ❌ |
| 7 | 0.222478 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 8 | 0.221635 | `azmcp_workbooks_create` | ❌ |
| 9 | 0.219081 | `azmcp_storage_account_create` | ❌ |
| 10 | 0.218848 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 11 | 0.215293 | `azmcp_loadtesting_testrun_create` | ❌ |
| 12 | 0.215098 | `azmcp_monitor_resource_log_query` | ❌ |
| 13 | 0.208401 | `azmcp_loadtesting_test_create` | ❌ |
| 14 | 0.208124 | `azmcp_quota_region_availability_list` | ❌ |
| 15 | 0.207601 | `azmcp_quota_usage_check` | ❌ |
| 16 | 0.206600 | `azmcp_bestpractices_get` | ❌ |
| 17 | 0.204420 | `azmcp_postgres_server_param_set` | ❌ |
| 18 | 0.195615 | `azmcp_workbooks_list` | ❌ |
| 19 | 0.192420 | `azmcp_monitor_metrics_query` | ❌ |
| 20 | 0.190106 | `azmcp_redis_cluster_list` | ❌ |

---

## Test 2

**Expected Tool:** `azmcp_foundry_models_deployments_list`  
**Prompt:** List all AI Foundry model deployments  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.559508 | `azmcp_foundry_models_deployments_list` | ✅ **EXPECTED** |
| 2 | 0.549636 | `azmcp_foundry_models_list` | ❌ |
| 3 | 0.533239 | `azmcp_foundry_models_deploy` | ❌ |
| 4 | 0.404693 | `azmcp_search_service_list` | ❌ |
| 5 | 0.387176 | `azmcp_search_index_list` | ❌ |
| 6 | 0.368173 | `azmcp_deploy_plan_get` | ❌ |
| 7 | 0.334867 | `azmcp_grafana_list` | ❌ |
| 8 | 0.318854 | `azmcp_postgres_server_list` | ❌ |
| 9 | 0.312247 | `azmcp_loadtesting_testrun_list` | ❌ |
| 10 | 0.310280 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 11 | 0.306178 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 12 | 0.302262 | `azmcp_monitor_table_type_list` | ❌ |
| 13 | 0.301302 | `azmcp_redis_cluster_list` | ❌ |
| 14 | 0.300018 | `azmcp_deploy_app_logs_get` | ❌ |
| 15 | 0.295359 | `azmcp_functionapp_list` | ❌ |
| 16 | 0.289486 | `azmcp_monitor_workspace_list` | ❌ |
| 17 | 0.288248 | `azmcp_redis_cache_list` | ❌ |
| 18 | 0.285916 | `azmcp_quota_region_availability_list` | ❌ |
| 19 | 0.284874 | `azmcp_monitor_table_list` | ❌ |
| 20 | 0.273892 | `azmcp_subscription_list` | ❌ |

---

## Test 3

**Expected Tool:** `azmcp_foundry_models_deployments_list`  
**Prompt:** Show me all AI Foundry model deployments  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.518221 | `azmcp_foundry_models_list` | ❌ |
| 2 | 0.503424 | `azmcp_foundry_models_deploy` | ❌ |
| 3 | 0.488885 | `azmcp_foundry_models_deployments_list` | ✅ **EXPECTED** |
| 4 | 0.360908 | `azmcp_search_service_list` | ❌ |
| 5 | 0.337032 | `azmcp_search_index_list` | ❌ |
| 6 | 0.328814 | `azmcp_deploy_plan_get` | ❌ |
| 7 | 0.305997 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 8 | 0.301514 | `azmcp_deploy_app_logs_get` | ❌ |
| 9 | 0.286814 | `azmcp_grafana_list` | ❌ |
| 10 | 0.272816 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 11 | 0.265906 | `azmcp_extension_azd` | ❌ |
| 12 | 0.259989 | `azmcp_loadtesting_testrun_list` | ❌ |
| 13 | 0.254926 | `azmcp_postgres_server_list` | ❌ |
| 14 | 0.250392 | `azmcp_redis_cluster_list` | ❌ |
| 15 | 0.246893 | `azmcp_quota_region_availability_list` | ❌ |
| 16 | 0.243133 | `azmcp_monitor_table_type_list` | ❌ |
| 17 | 0.238612 | `azmcp_search_index_describe` | ❌ |
| 18 | 0.234075 | `azmcp_redis_cache_list` | ❌ |
| 19 | 0.233993 | `azmcp_quota_usage_check` | ❌ |
| 20 | 0.232536 | `azmcp_monitor_workspace_list` | ❌ |

---

## Test 4

**Expected Tool:** `azmcp_foundry_models_list`  
**Prompt:** List all AI Foundry models  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.560022 | `azmcp_foundry_models_list` | ✅ **EXPECTED** |
| 2 | 0.401146 | `azmcp_foundry_models_deploy` | ❌ |
| 3 | 0.355031 | `azmcp_search_service_list` | ❌ |
| 4 | 0.346909 | `azmcp_foundry_models_deployments_list` | ❌ |
| 5 | 0.337429 | `azmcp_search_index_list` | ❌ |
| 6 | 0.298648 | `azmcp_monitor_table_type_list` | ❌ |
| 7 | 0.285437 | `azmcp_postgres_table_list` | ❌ |
| 8 | 0.277883 | `azmcp_grafana_list` | ❌ |
| 9 | 0.273026 | `azmcp_monitor_table_list` | ❌ |
| 10 | 0.252297 | `azmcp_postgres_database_list` | ❌ |
| 11 | 0.248620 | `azmcp_redis_cache_list` | ❌ |
| 12 | 0.247768 | `azmcp_monitor_workspace_list` | ❌ |
| 13 | 0.245193 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 14 | 0.245005 | `azmcp_loadtesting_testrun_list` | ❌ |
| 15 | 0.243429 | `azmcp_postgres_server_list` | ❌ |
| 16 | 0.242198 | `azmcp_redis_cluster_list` | ❌ |
| 17 | 0.240253 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 18 | 0.231110 | `azmcp_monitor_metrics_definitions` | ❌ |
| 19 | 0.229457 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 20 | 0.225944 | `azmcp_keyvault_certificate_list` | ❌ |

---

## Test 5

**Expected Tool:** `azmcp_foundry_models_list`  
**Prompt:** Show me the available AI Foundry models  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.574818 | `azmcp_foundry_models_list` | ✅ **EXPECTED** |
| 2 | 0.430513 | `azmcp_foundry_models_deploy` | ❌ |
| 3 | 0.356899 | `azmcp_foundry_models_deployments_list` | ❌ |
| 4 | 0.309590 | `azmcp_search_service_list` | ❌ |
| 5 | 0.287991 | `azmcp_search_index_list` | ❌ |
| 6 | 0.266937 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 7 | 0.245943 | `azmcp_quota_region_availability_list` | ❌ |
| 8 | 0.244697 | `azmcp_monitor_table_type_list` | ❌ |
| 9 | 0.243617 | `azmcp_deploy_plan_get` | ❌ |
| 10 | 0.240256 | `azmcp_monitor_metrics_definitions` | ❌ |
| 11 | 0.237407 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 12 | 0.233079 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 13 | 0.231148 | `azmcp_grafana_list` | ❌ |
| 14 | 0.227966 | `azmcp_deploy_app_logs_get` | ❌ |
| 15 | 0.212188 | `azmcp_search_index_describe` | ❌ |
| 16 | 0.205424 | `azmcp_quota_usage_check` | ❌ |
| 17 | 0.203036 | `azmcp_search_index_query` | ❌ |
| 18 | 0.200160 | `azmcp_monitor_workspace_list` | ❌ |
| 19 | 0.199386 | `azmcp_redis_cluster_list` | ❌ |
| 20 | 0.196419 | `azmcp_resourcehealth_availability-status_list` | ❌ |

---

## Test 6

**Expected Tool:** `azmcp_search_index_describe`  
**Prompt:** Show me the details of the index <index-name> in Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.618178 | `azmcp_search_index_list` | ❌ |
| 2 | 0.597678 | `azmcp_search_index_describe` | ✅ **EXPECTED** |
| 3 | 0.465274 | `azmcp_search_index_query` | ❌ |
| 4 | 0.436730 | `azmcp_search_service_list` | ❌ |
| 5 | 0.393556 | `azmcp_aks_cluster_get` | ❌ |
| 6 | 0.389306 | `azmcp_loadtesting_testrun_get` | ❌ |
| 7 | 0.358315 | `azmcp_kusto_cluster_get` | ❌ |
| 8 | 0.356252 | `azmcp_sql_db_show` | ❌ |
| 9 | 0.338129 | `azmcp_storage_account_details` | ❌ |
| 10 | 0.330038 | `azmcp_kusto_table_schema` | ❌ |
| 11 | 0.327134 | `azmcp_workbooks_show` | ❌ |
| 12 | 0.326847 | `azmcp_storage_table_list` | ❌ |
| 13 | 0.326590 | `azmcp_storage_blob_container_details` | ❌ |
| 14 | 0.325015 | `azmcp_cosmos_account_list` | ❌ |
| 15 | 0.322423 | `azmcp_monitor_table_list` | ❌ |
| 16 | 0.320890 | `azmcp_foundry_models_deployments_list` | ❌ |
| 17 | 0.316039 | `azmcp_appconfig_kv_show` | ❌ |
| 18 | 0.313076 | `azmcp_keyvault_certificate_get` | ❌ |
| 19 | 0.312439 | `azmcp_aks_cluster_list` | ❌ |
| 20 | 0.312237 | `azmcp_cosmos_database_list` | ❌ |

---

## Test 7

**Expected Tool:** `azmcp_search_index_list`  
**Prompt:** List all indexes in the Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.796644 | `azmcp_search_index_list` | ✅ **EXPECTED** |
| 2 | 0.561102 | `azmcp_search_service_list` | ❌ |
| 3 | 0.518943 | `azmcp_search_index_describe` | ❌ |
| 4 | 0.455689 | `azmcp_search_index_query` | ❌ |
| 5 | 0.439452 | `azmcp_monitor_table_list` | ❌ |
| 6 | 0.416404 | `azmcp_cosmos_database_list` | ❌ |
| 7 | 0.409307 | `azmcp_cosmos_account_list` | ❌ |
| 8 | 0.406485 | `azmcp_monitor_table_type_list` | ❌ |
| 9 | 0.392400 | `azmcp_storage_table_list` | ❌ |
| 10 | 0.382784 | `azmcp_keyvault_key_list` | ❌ |
| 11 | 0.378750 | `azmcp_kusto_database_list` | ❌ |
| 12 | 0.378324 | `azmcp_monitor_workspace_list` | ❌ |
| 13 | 0.375372 | `azmcp_foundry_models_deployments_list` | ❌ |
| 14 | 0.369551 | `azmcp_keyvault_certificate_list` | ❌ |
| 15 | 0.368931 | `azmcp_kusto_cluster_list` | ❌ |
| 16 | 0.367388 | `azmcp_redis_cache_list` | ❌ |
| 17 | 0.362546 | `azmcp_keyvault_secret_list` | ❌ |
| 18 | 0.361922 | `azmcp_foundry_models_list` | ❌ |
| 19 | 0.360722 | `azmcp_redis_cluster_list` | ❌ |
| 20 | 0.349633 | `azmcp_cosmos_database_container_list` | ❌ |

---

## Test 8

**Expected Tool:** `azmcp_search_index_list`  
**Prompt:** Show me the indexes in the Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.750042 | `azmcp_search_index_list` | ✅ **EXPECTED** |
| 2 | 0.512453 | `azmcp_search_index_describe` | ❌ |
| 3 | 0.497599 | `azmcp_search_service_list` | ❌ |
| 4 | 0.443812 | `azmcp_search_index_query` | ❌ |
| 5 | 0.401807 | `azmcp_monitor_table_list` | ❌ |
| 6 | 0.382692 | `azmcp_monitor_table_type_list` | ❌ |
| 7 | 0.372639 | `azmcp_cosmos_account_list` | ❌ |
| 8 | 0.370330 | `azmcp_cosmos_database_list` | ❌ |
| 9 | 0.353839 | `azmcp_storage_table_list` | ❌ |
| 10 | 0.351788 | `azmcp_foundry_models_deployments_list` | ❌ |
| 11 | 0.350043 | `azmcp_kusto_database_list` | ❌ |
| 12 | 0.347600 | `azmcp_monitor_workspace_list` | ❌ |
| 13 | 0.341728 | `azmcp_foundry_models_list` | ❌ |
| 14 | 0.332289 | `azmcp_kusto_cluster_list` | ❌ |
| 15 | 0.331224 | `azmcp_keyvault_key_list` | ❌ |
| 16 | 0.330437 | `azmcp_kusto_table_list` | ❌ |
| 17 | 0.328039 | `azmcp_redis_cluster_list` | ❌ |
| 18 | 0.327223 | `azmcp_monitor_metrics_definitions` | ❌ |
| 19 | 0.324069 | `azmcp_redis_cache_list` | ❌ |
| 20 | 0.323041 | `azmcp_cosmos_database_container_list` | ❌ |

---

## Test 9

**Expected Tool:** `azmcp_search_index_query`  
**Prompt:** Search for instances of <search_term> in the index <index-name> in Cognitive Search service <service-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.552944 | `azmcp_search_index_list` | ❌ |
| 2 | 0.522558 | `azmcp_search_index_query` | ✅ **EXPECTED** |
| 3 | 0.492637 | `azmcp_search_index_describe` | ❌ |
| 4 | 0.463356 | `azmcp_search_service_list` | ❌ |
| 5 | 0.327095 | `azmcp_kusto_query` | ❌ |
| 6 | 0.322009 | `azmcp_monitor_workspace_log_query` | ❌ |
| 7 | 0.311044 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 8 | 0.298026 | `azmcp_monitor_resource_log_query` | ❌ |
| 9 | 0.290809 | `azmcp_monitor_metrics_query` | ❌ |
| 10 | 0.288242 | `azmcp_foundry_models_deployments_list` | ❌ |
| 11 | 0.283532 | `azmcp_foundry_models_list` | ❌ |
| 12 | 0.269913 | `azmcp_monitor_table_list` | ❌ |
| 13 | 0.254226 | `azmcp_storage_table_list` | ❌ |
| 14 | 0.248402 | `azmcp_monitor_table_type_list` | ❌ |
| 15 | 0.244844 | `azmcp_kusto_sample` | ❌ |
| 16 | 0.243984 | `azmcp_cosmos_account_list` | ❌ |
| 17 | 0.235536 | `azmcp_cosmos_database_container_list` | ❌ |
| 18 | 0.232713 | `azmcp_loadtesting_testrun_get` | ❌ |
| 19 | 0.229137 | `azmcp_cosmos_database_list` | ❌ |
| 20 | 0.228078 | `azmcp_kusto_table_list` | ❌ |

---

## Test 10

**Expected Tool:** `azmcp_search_service_list`  
**Prompt:** List all Cognitive Search services in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.745450 | `azmcp_search_service_list` | ✅ **EXPECTED** |
| 2 | 0.608251 | `azmcp_search_index_list` | ❌ |
| 3 | 0.500455 | `azmcp_redis_cache_list` | ❌ |
| 4 | 0.494338 | `azmcp_monitor_workspace_list` | ❌ |
| 5 | 0.493011 | `azmcp_redis_cluster_list` | ❌ |
| 6 | 0.492228 | `azmcp_cosmos_account_list` | ❌ |
| 7 | 0.486066 | `azmcp_postgres_server_list` | ❌ |
| 8 | 0.482464 | `azmcp_grafana_list` | ❌ |
| 9 | 0.477471 | `azmcp_subscription_list` | ❌ |
| 10 | 0.470384 | `azmcp_kusto_cluster_list` | ❌ |
| 11 | 0.467684 | `azmcp_functionapp_list` | ❌ |
| 12 | 0.454460 | `azmcp_foundry_models_deployments_list` | ❌ |
| 13 | 0.450857 | `azmcp_aks_cluster_list` | ❌ |
| 14 | 0.441643 | `azmcp_storage_account_list` | ❌ |
| 15 | 0.427817 | `azmcp_group_list` | ❌ |
| 16 | 0.425463 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 17 | 0.418396 | `azmcp_quota_region_availability_list` | ❌ |
| 18 | 0.417472 | `azmcp_appconfig_account_list` | ❌ |
| 19 | 0.414984 | `azmcp_foundry_models_list` | ❌ |
| 20 | 0.408684 | `azmcp_loadtesting_testresource_list` | ❌ |

---

## Test 11

**Expected Tool:** `azmcp_search_service_list`  
**Prompt:** Show me the Cognitive Search services in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.644213 | `azmcp_search_service_list` | ✅ **EXPECTED** |
| 2 | 0.525315 | `azmcp_search_index_list` | ❌ |
| 3 | 0.426031 | `azmcp_monitor_workspace_list` | ❌ |
| 4 | 0.412158 | `azmcp_cosmos_account_list` | ❌ |
| 5 | 0.408456 | `azmcp_redis_cluster_list` | ❌ |
| 6 | 0.400229 | `azmcp_redis_cache_list` | ❌ |
| 7 | 0.399822 | `azmcp_grafana_list` | ❌ |
| 8 | 0.397883 | `azmcp_foundry_models_deployments_list` | ❌ |
| 9 | 0.393708 | `azmcp_subscription_list` | ❌ |
| 10 | 0.390559 | `azmcp_foundry_models_list` | ❌ |
| 11 | 0.384559 | `azmcp_postgres_server_list` | ❌ |
| 12 | 0.382145 | `azmcp_functionapp_list` | ❌ |
| 13 | 0.376962 | `azmcp_quota_region_availability_list` | ❌ |
| 14 | 0.376950 | `azmcp_search_index_describe` | ❌ |
| 15 | 0.376089 | `azmcp_kusto_cluster_list` | ❌ |
| 16 | 0.374692 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 17 | 0.362740 | `azmcp_aks_cluster_list` | ❌ |
| 18 | 0.362307 | `azmcp_marketplace_product_get` | ❌ |
| 19 | 0.360230 | `azmcp_loadtesting_testresource_list` | ❌ |
| 20 | 0.348165 | `azmcp_appconfig_account_list` | ❌ |

---

## Test 12

**Expected Tool:** `azmcp_search_service_list`  
**Prompt:** Show me my Cognitive Search services  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.488099 | `azmcp_search_index_list` | ❌ |
| 2 | 0.482308 | `azmcp_search_service_list` | ✅ **EXPECTED** |
| 3 | 0.351773 | `azmcp_search_index_describe` | ❌ |
| 4 | 0.344699 | `azmcp_foundry_models_deployments_list` | ❌ |
| 5 | 0.336174 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 6 | 0.329777 | `azmcp_search_index_query` | ❌ |
| 7 | 0.322540 | `azmcp_foundry_models_list` | ❌ |
| 8 | 0.290214 | `azmcp_cosmos_account_list` | ❌ |
| 9 | 0.283366 | `azmcp_redis_cluster_list` | ❌ |
| 10 | 0.281247 | `azmcp_monitor_workspace_list` | ❌ |
| 11 | 0.278531 | `azmcp_redis_cache_list` | ❌ |
| 12 | 0.276660 | `azmcp_extension_az` | ❌ |
| 13 | 0.274081 | `azmcp_monitor_table_type_list` | ❌ |
| 14 | 0.272173 | `azmcp_monitor_table_list` | ❌ |
| 15 | 0.266990 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 16 | 0.266987 | `azmcp_cosmos_database_list` | ❌ |
| 17 | 0.264394 | `azmcp_grafana_list` | ❌ |
| 18 | 0.261090 | `azmcp_aks_cluster_list` | ❌ |
| 19 | 0.257982 | `azmcp_deploy_app_logs_get` | ❌ |
| 20 | 0.256627 | `azmcp_deploy_plan_get` | ❌ |

---

## Test 13

**Expected Tool:** `azmcp_appconfig_account_list`  
**Prompt:** List all App Configuration stores in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.786360 | `azmcp_appconfig_account_list` | ✅ **EXPECTED** |
| 2 | 0.635561 | `azmcp_appconfig_kv_list` | ❌ |
| 3 | 0.492146 | `azmcp_redis_cache_list` | ❌ |
| 4 | 0.491380 | `azmcp_postgres_server_list` | ❌ |
| 5 | 0.473554 | `azmcp_redis_cluster_list` | ❌ |
| 6 | 0.460620 | `azmcp_functionapp_list` | ❌ |
| 7 | 0.458540 | `azmcp_storage_account_list` | ❌ |
| 8 | 0.442214 | `azmcp_grafana_list` | ❌ |
| 9 | 0.441656 | `azmcp_cosmos_account_list` | ❌ |
| 10 | 0.429305 | `azmcp_search_service_list` | ❌ |
| 11 | 0.427658 | `azmcp_subscription_list` | ❌ |
| 12 | 0.427460 | `azmcp_appconfig_kv_show` | ❌ |
| 13 | 0.420794 | `azmcp_kusto_cluster_list` | ❌ |
| 14 | 0.408642 | `azmcp_monitor_workspace_list` | ❌ |
| 15 | 0.404636 | `azmcp_storage_table_list` | ❌ |
| 16 | 0.396940 | `azmcp_aks_cluster_list` | ❌ |
| 17 | 0.387414 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.387179 | `azmcp_appconfig_kv_delete` | ❌ |
| 19 | 0.385938 | `azmcp_sql_db_list` | ❌ |
| 20 | 0.380818 | `azmcp_quota_region_availability_list` | ❌ |

---

## Test 14

**Expected Tool:** `azmcp_appconfig_account_list`  
**Prompt:** Show me the App Configuration stores in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.634978 | `azmcp_appconfig_account_list` | ✅ **EXPECTED** |
| 2 | 0.533437 | `azmcp_appconfig_kv_list` | ❌ |
| 3 | 0.425610 | `azmcp_appconfig_kv_show` | ❌ |
| 4 | 0.372456 | `azmcp_postgres_server_list` | ❌ |
| 5 | 0.368731 | `azmcp_redis_cache_list` | ❌ |
| 6 | 0.368086 | `azmcp_functionapp_list` | ❌ |
| 7 | 0.359567 | `azmcp_postgres_server_config_get` | ❌ |
| 8 | 0.356514 | `azmcp_redis_cluster_list` | ❌ |
| 9 | 0.354747 | `azmcp_appconfig_kv_delete` | ❌ |
| 10 | 0.348603 | `azmcp_appconfig_kv_set` | ❌ |
| 11 | 0.341263 | `azmcp_grafana_list` | ❌ |
| 12 | 0.331058 | `azmcp_storage_account_list` | ❌ |
| 13 | 0.325885 | `azmcp_subscription_list` | ❌ |
| 14 | 0.321968 | `azmcp_appconfig_kv_unlock` | ❌ |
| 15 | 0.320517 | `azmcp_marketplace_product_get` | ❌ |
| 16 | 0.317667 | `azmcp_search_service_list` | ❌ |
| 17 | 0.316132 | `azmcp_appconfig_kv_lock` | ❌ |
| 18 | 0.296589 | `azmcp_storage_table_list` | ❌ |
| 19 | 0.292874 | `azmcp_monitor_workspace_list` | ❌ |
| 20 | 0.288739 | `azmcp_servicebus_topic_subscription_details` | ❌ |

---

## Test 15

**Expected Tool:** `azmcp_appconfig_account_list`  
**Prompt:** Show me my App Configuration stores  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.565435 | `azmcp_appconfig_account_list` | ✅ **EXPECTED** |
| 2 | 0.564705 | `azmcp_appconfig_kv_list` | ❌ |
| 3 | 0.414689 | `azmcp_appconfig_kv_show` | ❌ |
| 4 | 0.355916 | `azmcp_postgres_server_config_get` | ❌ |
| 5 | 0.348661 | `azmcp_appconfig_kv_delete` | ❌ |
| 6 | 0.327234 | `azmcp_appconfig_kv_set` | ❌ |
| 7 | 0.308131 | `azmcp_appconfig_kv_unlock` | ❌ |
| 8 | 0.302405 | `azmcp_appconfig_kv_lock` | ❌ |
| 9 | 0.244080 | `azmcp_loadtesting_testrun_list` | ❌ |
| 10 | 0.237881 | `azmcp_loadtesting_test_get` | ❌ |
| 11 | 0.236404 | `azmcp_deploy_app_logs_get` | ❌ |
| 12 | 0.235204 | `azmcp_storage_account_details` | ❌ |
| 13 | 0.233357 | `azmcp_postgres_server_list` | ❌ |
| 14 | 0.231649 | `azmcp_redis_cache_list` | ❌ |
| 15 | 0.230170 | `azmcp_storage_blob_container_list` | ❌ |
| 16 | 0.221405 | `azmcp_postgres_database_list` | ❌ |
| 17 | 0.216109 | `azmcp_redis_cluster_list` | ❌ |
| 18 | 0.214205 | `azmcp_storage_account_list` | ❌ |
| 19 | 0.209941 | `azmcp_sql_db_list` | ❌ |
| 20 | 0.208606 | `azmcp_storage_blob_container_details` | ❌ |

---

## Test 16

**Expected Tool:** `azmcp_appconfig_kv_delete`  
**Prompt:** Delete the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.618230 | `azmcp_appconfig_kv_delete` | ✅ **EXPECTED** |
| 2 | 0.486603 | `azmcp_appconfig_kv_list` | ❌ |
| 3 | 0.444837 | `azmcp_appconfig_kv_unlock` | ❌ |
| 4 | 0.443983 | `azmcp_appconfig_kv_lock` | ❌ |
| 5 | 0.424242 | `azmcp_appconfig_kv_set` | ❌ |
| 6 | 0.399466 | `azmcp_appconfig_kv_show` | ❌ |
| 7 | 0.392028 | `azmcp_appconfig_account_list` | ❌ |
| 8 | 0.264769 | `azmcp_workbooks_delete` | ❌ |
| 9 | 0.262025 | `azmcp_keyvault_key_create` | ❌ |
| 10 | 0.248707 | `azmcp_keyvault_key_list` | ❌ |
| 11 | 0.240391 | `azmcp_keyvault_secret_create` | ❌ |
| 12 | 0.194805 | `azmcp_postgres_server_config_get` | ❌ |
| 13 | 0.175546 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.173109 | `azmcp_postgres_server_param_set` | ❌ |
| 15 | 0.155809 | `azmcp_storage_account_details` | ❌ |
| 16 | 0.145061 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 17 | 0.141074 | `azmcp_postgres_server_param_get` | ❌ |
| 18 | 0.137696 | `azmcp_servicebus_topic_subscription_details` | ❌ |
| 19 | 0.135817 | `azmcp_redis_cache_list` | ❌ |
| 20 | 0.131859 | `azmcp_sql_db_list` | ❌ |

---

## Test 17

**Expected Tool:** `azmcp_appconfig_kv_list`  
**Prompt:** List all key-value settings in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.730852 | `azmcp_appconfig_kv_list` | ✅ **EXPECTED** |
| 2 | 0.595054 | `azmcp_appconfig_kv_show` | ❌ |
| 3 | 0.557810 | `azmcp_appconfig_account_list` | ❌ |
| 4 | 0.530884 | `azmcp_appconfig_kv_set` | ❌ |
| 5 | 0.482784 | `azmcp_appconfig_kv_unlock` | ❌ |
| 6 | 0.464635 | `azmcp_appconfig_kv_delete` | ❌ |
| 7 | 0.438315 | `azmcp_appconfig_kv_lock` | ❌ |
| 8 | 0.377534 | `azmcp_postgres_server_config_get` | ❌ |
| 9 | 0.374538 | `azmcp_keyvault_key_list` | ❌ |
| 10 | 0.338294 | `azmcp_keyvault_secret_list` | ❌ |
| 11 | 0.329461 | `azmcp_loadtesting_testrun_list` | ❌ |
| 12 | 0.296908 | `azmcp_storage_account_details` | ❌ |
| 13 | 0.296084 | `azmcp_postgres_table_list` | ❌ |
| 14 | 0.292091 | `azmcp_redis_cache_list` | ❌ |
| 15 | 0.279679 | `azmcp_storage_table_list` | ❌ |
| 16 | 0.275400 | `azmcp_storage_blob_container_list` | ❌ |
| 17 | 0.267026 | `azmcp_postgres_database_list` | ❌ |
| 18 | 0.264833 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 19 | 0.263496 | `azmcp_monitor_table_list` | ❌ |
| 20 | 0.258800 | `azmcp_subscription_list` | ❌ |

---

## Test 18

**Expected Tool:** `azmcp_appconfig_kv_list`  
**Prompt:** Show me the key-value settings in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.682275 | `azmcp_appconfig_kv_list` | ✅ **EXPECTED** |
| 2 | 0.606545 | `azmcp_appconfig_kv_show` | ❌ |
| 3 | 0.522426 | `azmcp_appconfig_account_list` | ❌ |
| 4 | 0.512945 | `azmcp_appconfig_kv_set` | ❌ |
| 5 | 0.490384 | `azmcp_appconfig_kv_unlock` | ❌ |
| 6 | 0.468503 | `azmcp_appconfig_kv_delete` | ❌ |
| 7 | 0.458896 | `azmcp_appconfig_kv_lock` | ❌ |
| 8 | 0.370520 | `azmcp_postgres_server_config_get` | ❌ |
| 9 | 0.316879 | `azmcp_loadtesting_test_get` | ❌ |
| 10 | 0.296442 | `azmcp_storage_account_details` | ❌ |
| 11 | 0.294904 | `azmcp_keyvault_key_list` | ❌ |
| 12 | 0.282379 | `azmcp_loadtesting_testrun_list` | ❌ |
| 13 | 0.258688 | `azmcp_postgres_server_param_get` | ❌ |
| 14 | 0.248138 | `azmcp_storage_blob_container_details` | ❌ |
| 15 | 0.247879 | `azmcp_storage_blob_details` | ❌ |
| 16 | 0.243655 | `azmcp_postgres_server_param_set` | ❌ |
| 17 | 0.236389 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 18 | 0.233375 | `azmcp_redis_cache_list` | ❌ |
| 19 | 0.228684 | `azmcp_storage_blob_container_list` | ❌ |
| 20 | 0.225853 | `azmcp_storage_table_list` | ❌ |

---

## Test 19

**Expected Tool:** `azmcp_appconfig_kv_lock`  
**Prompt:** Lock the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.646614 | `azmcp_appconfig_kv_lock` | ✅ **EXPECTED** |
| 2 | 0.518065 | `azmcp_appconfig_kv_unlock` | ❌ |
| 3 | 0.508804 | `azmcp_appconfig_kv_list` | ❌ |
| 4 | 0.445551 | `azmcp_appconfig_kv_set` | ❌ |
| 5 | 0.431516 | `azmcp_appconfig_kv_delete` | ❌ |
| 6 | 0.423650 | `azmcp_appconfig_kv_show` | ❌ |
| 7 | 0.373656 | `azmcp_appconfig_account_list` | ❌ |
| 8 | 0.251298 | `azmcp_keyvault_key_create` | ❌ |
| 9 | 0.238540 | `azmcp_keyvault_secret_create` | ❌ |
| 10 | 0.238242 | `azmcp_postgres_server_param_set` | ❌ |
| 11 | 0.211331 | `azmcp_postgres_server_config_get` | ❌ |
| 12 | 0.208143 | `azmcp_keyvault_key_list` | ❌ |
| 13 | 0.191570 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.160992 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 15 | 0.154529 | `azmcp_postgres_server_param_get` | ❌ |
| 16 | 0.150689 | `azmcp_storage_account_details` | ❌ |
| 17 | 0.144377 | `azmcp_servicebus_queue_details` | ❌ |
| 18 | 0.135403 | `azmcp_storage_blob_container_details` | ❌ |
| 19 | 0.123426 | `azmcp_search_index_describe` | ❌ |
| 20 | 0.116471 | `azmcp_postgres_table_schema_get` | ❌ |

---

## Test 20

**Expected Tool:** `azmcp_appconfig_kv_set`  
**Prompt:** Set the key <key_name> in App Configuration store <app_config_store_name> to <value>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609635 | `azmcp_appconfig_kv_set` | ✅ **EXPECTED** |
| 2 | 0.541850 | `azmcp_appconfig_kv_lock` | ❌ |
| 3 | 0.518499 | `azmcp_appconfig_kv_list` | ❌ |
| 4 | 0.511219 | `azmcp_appconfig_kv_unlock` | ❌ |
| 5 | 0.507170 | `azmcp_appconfig_kv_show` | ❌ |
| 6 | 0.505571 | `azmcp_appconfig_kv_delete` | ❌ |
| 7 | 0.377919 | `azmcp_appconfig_account_list` | ❌ |
| 8 | 0.346927 | `azmcp_postgres_server_param_set` | ❌ |
| 9 | 0.311419 | `azmcp_keyvault_secret_create` | ❌ |
| 10 | 0.305955 | `azmcp_keyvault_key_create` | ❌ |
| 11 | 0.221138 | `azmcp_loadtesting_test_create` | ❌ |
| 12 | 0.208947 | `azmcp_postgres_server_config_get` | ❌ |
| 13 | 0.206836 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.201343 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 15 | 0.182031 | `azmcp_storage_account_details` | ❌ |
| 16 | 0.167006 | `azmcp_postgres_server_param_get` | ❌ |
| 17 | 0.136975 | `azmcp_storage_queue_message_send` | ❌ |
| 18 | 0.124233 | `azmcp_servicebus_queue_details` | ❌ |
| 19 | 0.123491 | `azmcp_storage_table_list` | ❌ |
| 20 | 0.122352 | `azmcp_search_index_describe` | ❌ |

---

## Test 21

**Expected Tool:** `azmcp_appconfig_kv_show`  
**Prompt:** Show the content for the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.603216 | `azmcp_appconfig_kv_list` | ❌ |
| 2 | 0.561508 | `azmcp_appconfig_kv_show` | ✅ **EXPECTED** |
| 3 | 0.448912 | `azmcp_appconfig_kv_set` | ❌ |
| 4 | 0.441713 | `azmcp_appconfig_kv_delete` | ❌ |
| 5 | 0.437432 | `azmcp_appconfig_account_list` | ❌ |
| 6 | 0.433846 | `azmcp_appconfig_kv_lock` | ❌ |
| 7 | 0.427588 | `azmcp_appconfig_kv_unlock` | ❌ |
| 8 | 0.301919 | `azmcp_keyvault_key_list` | ❌ |
| 9 | 0.291448 | `azmcp_postgres_server_config_get` | ❌ |
| 10 | 0.276985 | `azmcp_loadtesting_test_get` | ❌ |
| 11 | 0.260104 | `azmcp_keyvault_secret_list` | ❌ |
| 12 | 0.239998 | `azmcp_storage_account_details` | ❌ |
| 13 | 0.221806 | `azmcp_storage_blob_container_details` | ❌ |
| 14 | 0.217856 | `azmcp_postgres_server_param_get` | ❌ |
| 15 | 0.206401 | `azmcp_redis_cache_list` | ❌ |
| 16 | 0.205556 | `azmcp_storage_table_list` | ❌ |
| 17 | 0.205368 | `azmcp_storage_blob_container_list` | ❌ |
| 18 | 0.193413 | `azmcp_storage_blob_list` | ❌ |
| 19 | 0.191809 | `azmcp_storage_blob_details` | ❌ |
| 20 | 0.185986 | `azmcp_redis_cache_accesspolicy_list` | ❌ |

---

## Test 22

**Expected Tool:** `azmcp_appconfig_kv_unlock`  
**Prompt:** Unlock the key <key_name> in App Configuration store <app_config_store_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.603597 | `azmcp_appconfig_kv_unlock` | ✅ **EXPECTED** |
| 2 | 0.552244 | `azmcp_appconfig_kv_lock` | ❌ |
| 3 | 0.541557 | `azmcp_appconfig_kv_list` | ❌ |
| 4 | 0.476496 | `azmcp_appconfig_kv_delete` | ❌ |
| 5 | 0.435759 | `azmcp_appconfig_kv_show` | ❌ |
| 6 | 0.425488 | `azmcp_appconfig_kv_set` | ❌ |
| 7 | 0.409406 | `azmcp_appconfig_account_list` | ❌ |
| 8 | 0.268062 | `azmcp_keyvault_key_create` | ❌ |
| 9 | 0.259620 | `azmcp_keyvault_key_list` | ❌ |
| 10 | 0.252815 | `azmcp_keyvault_secret_create` | ❌ |
| 11 | 0.225350 | `azmcp_postgres_server_config_get` | ❌ |
| 12 | 0.185141 | `azmcp_postgres_server_param_set` | ❌ |
| 13 | 0.179313 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.169443 | `azmcp_storage_account_details` | ❌ |
| 15 | 0.159767 | `azmcp_postgres_server_param_get` | ❌ |
| 16 | 0.148832 | `azmcp_storage_blob_container_details` | ❌ |
| 17 | 0.145474 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 18 | 0.143564 | `azmcp_servicebus_queue_details` | ❌ |
| 19 | 0.132436 | `azmcp_search_index_describe` | ❌ |
| 20 | 0.131107 | `azmcp_workbooks_delete` | ❌ |

---

## Test 23

**Expected Tool:** `azmcp_extension_az`  
**Prompt:** Create a Storage account with name <storage_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628750 | `azmcp_storage_account_create` | ❌ |
| 2 | 0.472375 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.455521 | `azmcp_storage_account_details` | ❌ |
| 4 | 0.444381 | `azmcp_storage_account_list` | ❌ |
| 5 | 0.429618 | `azmcp_storage_table_list` | ❌ |
| 6 | 0.403063 | `azmcp_keyvault_secret_create` | ❌ |
| 7 | 0.396132 | `azmcp_storage_blob_list` | ❌ |
| 8 | 0.386765 | `azmcp_keyvault_key_create` | ❌ |
| 9 | 0.376271 | `azmcp_storage_blob_container_details` | ❌ |
| 10 | 0.374437 | `azmcp_keyvault_certificate_create` | ❌ |
| 11 | 0.352805 | `azmcp_appconfig_kv_set` | ❌ |
| 12 | 0.337708 | `azmcp_storage_datalake_directory_create` | ❌ |
| 13 | 0.333768 | `azmcp_storage_blob_container_create` | ❌ |
| 14 | 0.329895 | `azmcp_loadtesting_testresource_create` | ❌ |
| 15 | 0.327875 | `azmcp_workbooks_create` | ❌ |
| 16 | 0.325736 | `azmcp_loadtesting_test_create` | ❌ |
| 17 | 0.318516 | `azmcp_cosmos_database_container_list` | ❌ |
| 18 | 0.311829 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 19 | 0.303766 | `azmcp_cosmos_account_list` | ❌ |
| 20 | 0.303151 | `azmcp_appconfig_kv_lock` | ❌ |

---

## Test 24

**Expected Tool:** `azmcp_extension_az`  
**Prompt:** List all virtual machines in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.577373 | `azmcp_search_service_list` | ❌ |
| 2 | 0.531767 | `azmcp_subscription_list` | ❌ |
| 3 | 0.530948 | `azmcp_postgres_server_list` | ❌ |
| 4 | 0.500615 | `azmcp_redis_cache_list` | ❌ |
| 5 | 0.499251 | `azmcp_kusto_cluster_list` | ❌ |
| 6 | 0.496186 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.491307 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 8 | 0.484103 | `azmcp_monitor_workspace_list` | ❌ |
| 9 | 0.482576 | `azmcp_grafana_list` | ❌ |
| 10 | 0.476816 | `azmcp_aks_cluster_list` | ❌ |
| 11 | 0.473774 | `azmcp_cosmos_account_list` | ❌ |
| 12 | 0.473455 | `azmcp_functionapp_list` | ❌ |
| 13 | 0.468411 | `azmcp_virtualdesktop_hostpool_sessionhost_list` | ❌ |
| 14 | 0.454007 | `azmcp_group_list` | ❌ |
| 15 | 0.453201 | `azmcp_storage_account_list` | ❌ |
| 16 | 0.435557 | `azmcp_quota_region_availability_list` | ❌ |
| 17 | 0.430029 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 18 | 0.411045 | `azmcp_appconfig_account_list` | ❌ |
| 19 | 0.407261 | `azmcp_acr_registry_list` | ❌ |
| 20 | 0.385092 | `azmcp_loadtesting_testresource_list` | ❌ |

---

## Test 25

**Expected Tool:** `azmcp_extension_az`  
**Prompt:** Show me the details of the storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.632334 | `azmcp_storage_account_details` | ❌ |
| 2 | 0.565873 | `azmcp_storage_blob_container_details` | ❌ |
| 3 | 0.559925 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.513935 | `azmcp_storage_account_list` | ❌ |
| 5 | 0.509806 | `azmcp_storage_table_list` | ❌ |
| 6 | 0.495892 | `azmcp_storage_blob_list` | ❌ |
| 7 | 0.476522 | `azmcp_storage_account_create` | ❌ |
| 8 | 0.434946 | `azmcp_storage_blob_details` | ❌ |
| 9 | 0.433899 | `azmcp_cosmos_account_list` | ❌ |
| 10 | 0.433255 | `azmcp_appconfig_kv_show` | ❌ |
| 11 | 0.417590 | `azmcp_cosmos_database_container_list` | ❌ |
| 12 | 0.377441 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.371852 | `azmcp_sql_db_show` | ❌ |
| 14 | 0.367600 | `azmcp_aks_cluster_get` | ❌ |
| 15 | 0.360310 | `azmcp_cosmos_database_list` | ❌ |
| 16 | 0.347005 | `azmcp_loadtesting_testrun_get` | ❌ |
| 17 | 0.337702 | `azmcp_kusto_cluster_get` | ❌ |
| 18 | 0.326919 | `azmcp_keyvault_key_list` | ❌ |
| 19 | 0.325659 | `azmcp_appconfig_account_list` | ❌ |
| 20 | 0.323346 | `azmcp_functionapp_list` | ❌ |

---

## Test 26

**Expected Tool:** `azmcp_acr_registry_list`  
**Prompt:** List all Azure Container Registries in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.743568 | `azmcp_acr_registry_list` | ✅ **EXPECTED** |
| 2 | 0.711580 | `azmcp_acr_registry_repository_list` | ❌ |
| 3 | 0.528620 | `azmcp_search_service_list` | ❌ |
| 4 | 0.526587 | `azmcp_aks_cluster_list` | ❌ |
| 5 | 0.525768 | `azmcp_storage_blob_container_list` | ❌ |
| 6 | 0.515937 | `azmcp_subscription_list` | ❌ |
| 7 | 0.514293 | `azmcp_cosmos_account_list` | ❌ |
| 8 | 0.509371 | `azmcp_monitor_workspace_list` | ❌ |
| 9 | 0.503032 | `azmcp_kusto_cluster_list` | ❌ |
| 10 | 0.500893 | `azmcp_storage_account_list` | ❌ |
| 11 | 0.490776 | `azmcp_appconfig_account_list` | ❌ |
| 12 | 0.483500 | `azmcp_cosmos_database_container_list` | ❌ |
| 13 | 0.482499 | `azmcp_storage_table_list` | ❌ |
| 14 | 0.482236 | `azmcp_redis_cluster_list` | ❌ |
| 15 | 0.481761 | `azmcp_redis_cache_list` | ❌ |
| 16 | 0.480869 | `azmcp_group_list` | ❌ |
| 17 | 0.473498 | `azmcp_functionapp_list` | ❌ |
| 18 | 0.469958 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 19 | 0.462353 | `azmcp_quota_region_availability_list` | ❌ |
| 20 | 0.460523 | `azmcp_sql_db_list` | ❌ |

---

## Test 27

**Expected Tool:** `azmcp_acr_registry_list`  
**Prompt:** Show me my Azure Container Registries  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.586014 | `azmcp_acr_registry_list` | ✅ **EXPECTED** |
| 2 | 0.563636 | `azmcp_acr_registry_repository_list` | ❌ |
| 3 | 0.457032 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.415552 | `azmcp_cosmos_database_container_list` | ❌ |
| 5 | 0.376444 | `azmcp_storage_blob_list` | ❌ |
| 6 | 0.362031 | `azmcp_storage_blob_container_details` | ❌ |
| 7 | 0.359177 | `azmcp_deploy_app_logs_get` | ❌ |
| 8 | 0.356019 | `azmcp_aks_cluster_list` | ❌ |
| 9 | 0.353379 | `azmcp_subscription_list` | ❌ |
| 10 | 0.349526 | `azmcp_cosmos_database_list` | ❌ |
| 11 | 0.349291 | `azmcp_sql_db_list` | ❌ |
| 12 | 0.344750 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.344071 | `azmcp_cosmos_account_list` | ❌ |
| 14 | 0.339252 | `azmcp_appconfig_account_list` | ❌ |
| 15 | 0.338380 | `azmcp_storage_table_list` | ❌ |
| 16 | 0.337371 | `azmcp_keyvault_certificate_list` | ❌ |
| 17 | 0.334637 | `azmcp_extension_az` | ❌ |
| 18 | 0.333740 | `azmcp_monitor_workspace_list` | ❌ |
| 19 | 0.328377 | `azmcp_redis_cache_list` | ❌ |
| 20 | 0.328301 | `azmcp_quota_region_availability_list` | ❌ |

---

## Test 28

**Expected Tool:** `azmcp_acr_registry_list`  
**Prompt:** Show me the container registries in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.637130 | `azmcp_acr_registry_list` | ✅ **EXPECTED** |
| 2 | 0.563476 | `azmcp_acr_registry_repository_list` | ❌ |
| 3 | 0.474000 | `azmcp_redis_cache_list` | ❌ |
| 4 | 0.471804 | `azmcp_redis_cluster_list` | ❌ |
| 5 | 0.464679 | `azmcp_storage_blob_container_list` | ❌ |
| 6 | 0.463742 | `azmcp_postgres_server_list` | ❌ |
| 7 | 0.463435 | `azmcp_search_service_list` | ❌ |
| 8 | 0.452938 | `azmcp_kusto_cluster_list` | ❌ |
| 9 | 0.451266 | `azmcp_monitor_workspace_list` | ❌ |
| 10 | 0.443939 | `azmcp_appconfig_account_list` | ❌ |
| 11 | 0.440464 | `azmcp_subscription_list` | ❌ |
| 12 | 0.435835 | `azmcp_grafana_list` | ❌ |
| 13 | 0.432469 | `azmcp_storage_account_list` | ❌ |
| 14 | 0.431745 | `azmcp_cosmos_database_container_list` | ❌ |
| 15 | 0.430308 | `azmcp_cosmos_account_list` | ❌ |
| 16 | 0.430100 | `azmcp_aks_cluster_list` | ❌ |
| 17 | 0.409093 | `azmcp_storage_table_list` | ❌ |
| 18 | 0.404664 | `azmcp_group_list` | ❌ |
| 19 | 0.398556 | `azmcp_quota_region_availability_list` | ❌ |
| 20 | 0.394148 | `azmcp_kusto_database_list` | ❌ |

---

## Test 29

**Expected Tool:** `azmcp_acr_registry_list`  
**Prompt:** List container registries in resource group <resource_group_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.654318 | `azmcp_acr_registry_repository_list` | ❌ |
| 2 | 0.633938 | `azmcp_acr_registry_list` | ✅ **EXPECTED** |
| 3 | 0.454929 | `azmcp_group_list` | ❌ |
| 4 | 0.454003 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 5 | 0.452130 | `azmcp_storage_blob_container_list` | ❌ |
| 6 | 0.446008 | `azmcp_cosmos_database_container_list` | ❌ |
| 7 | 0.428000 | `azmcp_workbooks_list` | ❌ |
| 8 | 0.423541 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 9 | 0.411316 | `azmcp_redis_cluster_list` | ❌ |
| 10 | 0.409133 | `azmcp_sql_db_list` | ❌ |
| 11 | 0.392315 | `azmcp_storage_blob_list` | ❌ |
| 12 | 0.388773 | `azmcp_redis_cache_list` | ❌ |
| 13 | 0.372510 | `azmcp_sql_elastic-pool_list` | ❌ |
| 14 | 0.370359 | `azmcp_redis_cluster_database_list` | ❌ |
| 15 | 0.366704 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 16 | 0.356119 | `azmcp_kusto_cluster_list` | ❌ |
| 17 | 0.354145 | `azmcp_cosmos_database_list` | ❌ |
| 18 | 0.352336 | `azmcp_loadtesting_testresource_list` | ❌ |
| 19 | 0.351362 | `azmcp_aks_cluster_list` | ❌ |
| 20 | 0.342481 | `azmcp_cosmos_account_list` | ❌ |

---

## Test 30

**Expected Tool:** `azmcp_acr_registry_list`  
**Prompt:** Show me the container registries in resource group <resource_group_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.639391 | `azmcp_acr_registry_list` | ✅ **EXPECTED** |
| 2 | 0.637972 | `azmcp_acr_registry_repository_list` | ❌ |
| 3 | 0.449649 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 4 | 0.445741 | `azmcp_group_list` | ❌ |
| 5 | 0.440715 | `azmcp_storage_blob_container_list` | ❌ |
| 6 | 0.416353 | `azmcp_cosmos_database_container_list` | ❌ |
| 7 | 0.413975 | `azmcp_sql_db_list` | ❌ |
| 8 | 0.406554 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 9 | 0.400209 | `azmcp_workbooks_list` | ❌ |
| 10 | 0.378353 | `azmcp_redis_cluster_list` | ❌ |
| 11 | 0.373837 | `azmcp_sql_elastic-pool_list` | ❌ |
| 12 | 0.371881 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 13 | 0.368887 | `azmcp_storage_blob_list` | ❌ |
| 14 | 0.367734 | `azmcp_redis_cache_list` | ❌ |
| 15 | 0.358674 | `azmcp_monitor_workspace_list` | ❌ |
| 16 | 0.354807 | `azmcp_loadtesting_testresource_list` | ❌ |
| 17 | 0.351411 | `azmcp_cosmos_database_list` | ❌ |
| 18 | 0.344148 | `azmcp_kusto_cluster_list` | ❌ |
| 19 | 0.342840 | `azmcp_aks_cluster_list` | ❌ |
| 20 | 0.341763 | `azmcp_kusto_database_list` | ❌ |

---

## Test 31

**Expected Tool:** `azmcp_acr_registry_repository_list`  
**Prompt:** List all container registry repositories in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.626482 | `azmcp_acr_registry_repository_list` | ✅ **EXPECTED** |
| 2 | 0.617504 | `azmcp_acr_registry_list` | ❌ |
| 3 | 0.510435 | `azmcp_redis_cache_list` | ❌ |
| 4 | 0.499632 | `azmcp_storage_blob_container_list` | ❌ |
| 5 | 0.495567 | `azmcp_postgres_server_list` | ❌ |
| 6 | 0.492550 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.480676 | `azmcp_search_service_list` | ❌ |
| 8 | 0.475629 | `azmcp_kusto_cluster_list` | ❌ |
| 9 | 0.461777 | `azmcp_cosmos_database_container_list` | ❌ |
| 10 | 0.461369 | `azmcp_grafana_list` | ❌ |
| 11 | 0.461173 | `azmcp_storage_account_list` | ❌ |
| 12 | 0.456838 | `azmcp_appconfig_account_list` | ❌ |
| 13 | 0.449239 | `azmcp_cosmos_account_list` | ❌ |
| 14 | 0.448225 | `azmcp_monitor_workspace_list` | ❌ |
| 15 | 0.440083 | `azmcp_subscription_list` | ❌ |
| 16 | 0.437543 | `azmcp_aks_cluster_list` | ❌ |
| 17 | 0.434157 | `azmcp_storage_blob_list` | ❌ |
| 18 | 0.430939 | `azmcp_group_list` | ❌ |
| 19 | 0.423301 | `azmcp_storage_table_list` | ❌ |
| 20 | 0.414463 | `azmcp_kusto_database_list` | ❌ |

---

## Test 32

**Expected Tool:** `azmcp_acr_registry_repository_list`  
**Prompt:** Show me my container registry repositories  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.546333 | `azmcp_acr_registry_repository_list` | ✅ **EXPECTED** |
| 2 | 0.469295 | `azmcp_acr_registry_list` | ❌ |
| 3 | 0.453590 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.407973 | `azmcp_cosmos_database_container_list` | ❌ |
| 5 | 0.387046 | `azmcp_storage_blob_container_details` | ❌ |
| 6 | 0.384208 | `azmcp_storage_blob_list` | ❌ |
| 7 | 0.308650 | `azmcp_cosmos_database_list` | ❌ |
| 8 | 0.308274 | `azmcp_storage_blob_container_create` | ❌ |
| 9 | 0.302635 | `azmcp_redis_cache_list` | ❌ |
| 10 | 0.294699 | `azmcp_storage_blob_details` | ❌ |
| 11 | 0.293421 | `azmcp_storage_table_list` | ❌ |
| 12 | 0.292610 | `azmcp_storage_account_list` | ❌ |
| 13 | 0.292155 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 14 | 0.290148 | `azmcp_redis_cluster_list` | ❌ |
| 15 | 0.283716 | `azmcp_appconfig_account_list` | ❌ |
| 16 | 0.283390 | `azmcp_kusto_database_list` | ❌ |
| 17 | 0.282581 | `azmcp_sql_db_list` | ❌ |
| 18 | 0.276498 | `azmcp_cosmos_account_list` | ❌ |
| 19 | 0.272919 | `azmcp_aks_cluster_list` | ❌ |
| 20 | 0.269524 | `azmcp_keyvault_secret_list` | ❌ |

---

## Test 33

**Expected Tool:** `azmcp_acr_registry_repository_list`  
**Prompt:** List repositories in the container registry <registry_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.674296 | `azmcp_acr_registry_repository_list` | ✅ **EXPECTED** |
| 2 | 0.541779 | `azmcp_acr_registry_list` | ❌ |
| 3 | 0.456225 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.433927 | `azmcp_cosmos_database_container_list` | ❌ |
| 5 | 0.408817 | `azmcp_storage_blob_list` | ❌ |
| 6 | 0.359617 | `azmcp_cosmos_database_list` | ❌ |
| 7 | 0.355328 | `azmcp_redis_cache_list` | ❌ |
| 8 | 0.351007 | `azmcp_redis_cluster_database_list` | ❌ |
| 9 | 0.347437 | `azmcp_postgres_database_list` | ❌ |
| 10 | 0.347084 | `azmcp_kusto_database_list` | ❌ |
| 11 | 0.346850 | `azmcp_storage_table_list` | ❌ |
| 12 | 0.340933 | `azmcp_storage_blob_container_details` | ❌ |
| 13 | 0.340014 | `azmcp_redis_cluster_list` | ❌ |
| 14 | 0.338666 | `azmcp_keyvault_secret_list` | ❌ |
| 15 | 0.337688 | `azmcp_keyvault_certificate_list` | ❌ |
| 16 | 0.332834 | `azmcp_keyvault_key_list` | ❌ |
| 17 | 0.332785 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 18 | 0.332704 | `azmcp_sql_db_list` | ❌ |
| 19 | 0.332544 | `azmcp_monitor_workspace_list` | ❌ |
| 20 | 0.330046 | `azmcp_kusto_cluster_list` | ❌ |

---

## Test 34

**Expected Tool:** `azmcp_acr_registry_repository_list`  
**Prompt:** Show me the repositories in the container registry <registry_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.600780 | `azmcp_acr_registry_repository_list` | ✅ **EXPECTED** |
| 2 | 0.501842 | `azmcp_acr_registry_list` | ❌ |
| 3 | 0.430025 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.418623 | `azmcp_cosmos_database_container_list` | ❌ |
| 5 | 0.368174 | `azmcp_storage_blob_list` | ❌ |
| 6 | 0.341511 | `azmcp_redis_cache_list` | ❌ |
| 7 | 0.333318 | `azmcp_cosmos_database_list` | ❌ |
| 8 | 0.325373 | `azmcp_storage_blob_container_details` | ❌ |
| 9 | 0.324104 | `azmcp_redis_cluster_list` | ❌ |
| 10 | 0.318706 | `azmcp_kusto_database_list` | ❌ |
| 11 | 0.316614 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 12 | 0.315414 | `azmcp_redis_cluster_database_list` | ❌ |
| 13 | 0.314960 | `azmcp_storage_table_list` | ❌ |
| 14 | 0.311684 | `azmcp_monitor_workspace_list` | ❌ |
| 15 | 0.306052 | `azmcp_sql_db_list` | ❌ |
| 16 | 0.304949 | `azmcp_keyvault_certificate_list` | ❌ |
| 17 | 0.303931 | `azmcp_kusto_cluster_list` | ❌ |
| 18 | 0.300101 | `azmcp_cosmos_account_list` | ❌ |
| 19 | 0.299629 | `azmcp_appconfig_account_list` | ❌ |
| 20 | 0.295411 | `azmcp_postgres_database_list` | ❌ |

---

## Test 35

**Expected Tool:** `azmcp_cosmos_account_list`  
**Prompt:** List all cosmosdb accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.818357 | `azmcp_cosmos_account_list` | ✅ **EXPECTED** |
| 2 | 0.668480 | `azmcp_cosmos_database_list` | ❌ |
| 3 | 0.615268 | `azmcp_cosmos_database_container_list` | ❌ |
| 4 | 0.606794 | `azmcp_storage_account_list` | ❌ |
| 5 | 0.588682 | `azmcp_storage_table_list` | ❌ |
| 6 | 0.587691 | `azmcp_subscription_list` | ❌ |
| 7 | 0.557870 | `azmcp_search_service_list` | ❌ |
| 8 | 0.530755 | `azmcp_storage_blob_container_list` | ❌ |
| 9 | 0.528992 | `azmcp_monitor_workspace_list` | ❌ |
| 10 | 0.516914 | `azmcp_kusto_cluster_list` | ❌ |
| 11 | 0.514460 | `azmcp_functionapp_list` | ❌ |
| 12 | 0.502428 | `azmcp_kusto_database_list` | ❌ |
| 13 | 0.502199 | `azmcp_redis_cluster_list` | ❌ |
| 14 | 0.499161 | `azmcp_redis_cache_list` | ❌ |
| 15 | 0.497679 | `azmcp_appconfig_account_list` | ❌ |
| 16 | 0.486978 | `azmcp_group_list` | ❌ |
| 17 | 0.483046 | `azmcp_grafana_list` | ❌ |
| 18 | 0.474934 | `azmcp_postgres_server_list` | ❌ |
| 19 | 0.472743 | `azmcp_storage_blob_list` | ❌ |
| 20 | 0.472589 | `azmcp_aks_cluster_list` | ❌ |

---

## Test 36

**Expected Tool:** `azmcp_cosmos_account_list`  
**Prompt:** Show me my cosmosdb accounts  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.665447 | `azmcp_cosmos_account_list` | ✅ **EXPECTED** |
| 2 | 0.605357 | `azmcp_cosmos_database_list` | ❌ |
| 3 | 0.571613 | `azmcp_cosmos_database_container_list` | ❌ |
| 4 | 0.473546 | `azmcp_storage_blob_container_list` | ❌ |
| 5 | 0.467671 | `azmcp_storage_table_list` | ❌ |
| 6 | 0.443558 | `azmcp_storage_account_list` | ❌ |
| 7 | 0.443455 | `azmcp_storage_account_details` | ❌ |
| 8 | 0.436283 | `azmcp_subscription_list` | ❌ |
| 9 | 0.431496 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 10 | 0.407809 | `azmcp_storage_blob_list` | ❌ |
| 11 | 0.390141 | `azmcp_kusto_database_list` | ❌ |
| 12 | 0.386145 | `azmcp_monitor_workspace_list` | ❌ |
| 13 | 0.383985 | `azmcp_appconfig_account_list` | ❌ |
| 14 | 0.381323 | `azmcp_sql_db_list` | ❌ |
| 15 | 0.379496 | `azmcp_appconfig_kv_show` | ❌ |
| 16 | 0.373667 | `azmcp_redis_cluster_list` | ❌ |
| 17 | 0.367942 | `azmcp_quota_usage_check` | ❌ |
| 18 | 0.358376 | `azmcp_keyvault_key_list` | ❌ |
| 19 | 0.355823 | `azmcp_functionapp_list` | ❌ |
| 20 | 0.354362 | `azmcp_keyvault_certificate_list` | ❌ |

---

## Test 37

**Expected Tool:** `azmcp_cosmos_account_list`  
**Prompt:** Show me the cosmosdb accounts in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.752494 | `azmcp_cosmos_account_list` | ✅ **EXPECTED** |
| 2 | 0.605125 | `azmcp_cosmos_database_list` | ❌ |
| 3 | 0.566249 | `azmcp_cosmos_database_container_list` | ❌ |
| 4 | 0.548156 | `azmcp_storage_account_list` | ❌ |
| 5 | 0.546327 | `azmcp_subscription_list` | ❌ |
| 6 | 0.535227 | `azmcp_storage_table_list` | ❌ |
| 7 | 0.513709 | `azmcp_search_service_list` | ❌ |
| 8 | 0.488035 | `azmcp_monitor_workspace_list` | ❌ |
| 9 | 0.483799 | `azmcp_storage_blob_container_list` | ❌ |
| 10 | 0.466414 | `azmcp_redis_cluster_list` | ❌ |
| 11 | 0.462048 | `azmcp_functionapp_list` | ❌ |
| 12 | 0.457584 | `azmcp_appconfig_account_list` | ❌ |
| 13 | 0.456219 | `azmcp_redis_cache_list` | ❌ |
| 14 | 0.455017 | `azmcp_kusto_cluster_list` | ❌ |
| 15 | 0.453626 | `azmcp_kusto_database_list` | ❌ |
| 16 | 0.443558 | `azmcp_storage_account_details` | ❌ |
| 17 | 0.441136 | `azmcp_grafana_list` | ❌ |
| 18 | 0.438277 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 19 | 0.433094 | `azmcp_postgres_server_list` | ❌ |
| 20 | 0.429432 | `azmcp_aks_cluster_list` | ❌ |

---

## Test 38

**Expected Tool:** `azmcp_cosmos_database_container_item_query`  
**Prompt:** Show me the items that contain the word <search_term> in the container <container_name> in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.605253 | `azmcp_cosmos_database_container_list` | ❌ |
| 2 | 0.566854 | `azmcp_cosmos_database_container_item_query` | ✅ **EXPECTED** |
| 3 | 0.477874 | `azmcp_cosmos_database_list` | ❌ |
| 4 | 0.469244 | `azmcp_storage_blob_container_list` | ❌ |
| 5 | 0.447757 | `azmcp_cosmos_account_list` | ❌ |
| 6 | 0.417506 | `azmcp_storage_blob_list` | ❌ |
| 7 | 0.408739 | `azmcp_storage_blob_container_details` | ❌ |
| 8 | 0.398979 | `azmcp_search_service_list` | ❌ |
| 9 | 0.386227 | `azmcp_search_index_list` | ❌ |
| 10 | 0.384335 | `azmcp_storage_table_list` | ❌ |
| 11 | 0.378151 | `azmcp_kusto_query` | ❌ |
| 12 | 0.351331 | `azmcp_kusto_table_list` | ❌ |
| 13 | 0.340982 | `azmcp_monitor_table_list` | ❌ |
| 14 | 0.335256 | `azmcp_sql_db_list` | ❌ |
| 15 | 0.334389 | `azmcp_kusto_database_list` | ❌ |
| 16 | 0.334381 | `azmcp_storage_blob_details` | ❌ |
| 17 | 0.331041 | `azmcp_kusto_sample` | ❌ |
| 18 | 0.326439 | `azmcp_monitor_resource_log_query` | ❌ |
| 19 | 0.308694 | `azmcp_acr_registry_repository_list` | ❌ |
| 20 | 0.302962 | `azmcp_appconfig_kv_show` | ❌ |

---

## Test 39

**Expected Tool:** `azmcp_cosmos_database_container_list`  
**Prompt:** List all the containers in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.852832 | `azmcp_cosmos_database_container_list` | ✅ **EXPECTED** |
| 2 | 0.690158 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.681044 | `azmcp_cosmos_database_list` | ❌ |
| 4 | 0.630659 | `azmcp_cosmos_account_list` | ❌ |
| 5 | 0.561245 | `azmcp_storage_blob_list` | ❌ |
| 6 | 0.535260 | `azmcp_storage_table_list` | ❌ |
| 7 | 0.527459 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 8 | 0.473516 | `azmcp_storage_blob_container_details` | ❌ |
| 9 | 0.448957 | `azmcp_kusto_database_list` | ❌ |
| 10 | 0.441485 | `azmcp_storage_account_list` | ❌ |
| 11 | 0.439770 | `azmcp_sql_db_list` | ❌ |
| 12 | 0.427614 | `azmcp_kusto_table_list` | ❌ |
| 13 | 0.424294 | `azmcp_redis_cluster_database_list` | ❌ |
| 14 | 0.421534 | `azmcp_acr_registry_repository_list` | ❌ |
| 15 | 0.411663 | `azmcp_monitor_table_list` | ❌ |
| 16 | 0.405887 | `azmcp_storage_account_details` | ❌ |
| 17 | 0.392929 | `azmcp_postgres_database_list` | ❌ |
| 18 | 0.378694 | `azmcp_keyvault_certificate_list` | ❌ |
| 19 | 0.372115 | `azmcp_kusto_cluster_list` | ❌ |
| 20 | 0.368447 | `azmcp_keyvault_key_list` | ❌ |

---

## Test 40

**Expected Tool:** `azmcp_cosmos_database_container_list`  
**Prompt:** Show me the containers in the database <database_name> for the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.789395 | `azmcp_cosmos_database_container_list` | ✅ **EXPECTED** |
| 2 | 0.614220 | `azmcp_cosmos_database_list` | ❌ |
| 3 | 0.611374 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.562062 | `azmcp_cosmos_account_list` | ❌ |
| 5 | 0.521532 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 6 | 0.474816 | `azmcp_storage_blob_list` | ❌ |
| 7 | 0.471019 | `azmcp_storage_table_list` | ❌ |
| 8 | 0.449542 | `azmcp_storage_blob_container_details` | ❌ |
| 9 | 0.398064 | `azmcp_kusto_database_list` | ❌ |
| 10 | 0.397755 | `azmcp_sql_db_list` | ❌ |
| 11 | 0.395513 | `azmcp_kusto_table_list` | ❌ |
| 12 | 0.394078 | `azmcp_storage_account_details` | ❌ |
| 13 | 0.386806 | `azmcp_redis_cluster_database_list` | ❌ |
| 14 | 0.372499 | `azmcp_storage_blob_details` | ❌ |
| 15 | 0.362485 | `azmcp_storage_account_list` | ❌ |
| 16 | 0.355640 | `azmcp_acr_registry_repository_list` | ❌ |
| 17 | 0.345665 | `azmcp_sql_db_show` | ❌ |
| 18 | 0.319603 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 19 | 0.318540 | `azmcp_appconfig_kv_show` | ❌ |
| 20 | 0.314917 | `azmcp_kusto_table_schema` | ❌ |

---

## Test 41

**Expected Tool:** `azmcp_cosmos_database_list`  
**Prompt:** List all the databases in the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.815683 | `azmcp_cosmos_database_list` | ✅ **EXPECTED** |
| 2 | 0.668515 | `azmcp_cosmos_account_list` | ❌ |
| 3 | 0.665298 | `azmcp_cosmos_database_container_list` | ❌ |
| 4 | 0.571319 | `azmcp_kusto_database_list` | ❌ |
| 5 | 0.555134 | `azmcp_storage_table_list` | ❌ |
| 6 | 0.548066 | `azmcp_sql_db_list` | ❌ |
| 7 | 0.526046 | `azmcp_redis_cluster_database_list` | ❌ |
| 8 | 0.501477 | `azmcp_postgres_database_list` | ❌ |
| 9 | 0.500364 | `azmcp_storage_blob_container_list` | ❌ |
| 10 | 0.471453 | `azmcp_kusto_table_list` | ❌ |
| 11 | 0.459194 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 12 | 0.456262 | `azmcp_storage_account_list` | ❌ |
| 13 | 0.450854 | `azmcp_monitor_table_list` | ❌ |
| 14 | 0.439548 | `azmcp_storage_blob_list` | ❌ |
| 15 | 0.405809 | `azmcp_keyvault_key_list` | ❌ |
| 16 | 0.401638 | `azmcp_subscription_list` | ❌ |
| 17 | 0.398020 | `azmcp_keyvault_certificate_list` | ❌ |
| 18 | 0.396808 | `azmcp_search_index_list` | ❌ |
| 19 | 0.388992 | `azmcp_keyvault_secret_list` | ❌ |
| 20 | 0.387534 | `azmcp_acr_registry_repository_list` | ❌ |

---

## Test 42

**Expected Tool:** `azmcp_cosmos_database_list`  
**Prompt:** Show me the databases in the cosmosdb account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.749370 | `azmcp_cosmos_database_list` | ✅ **EXPECTED** |
| 2 | 0.624759 | `azmcp_cosmos_database_container_list` | ❌ |
| 3 | 0.614572 | `azmcp_cosmos_account_list` | ❌ |
| 4 | 0.524837 | `azmcp_kusto_database_list` | ❌ |
| 5 | 0.505363 | `azmcp_storage_table_list` | ❌ |
| 6 | 0.498206 | `azmcp_sql_db_list` | ❌ |
| 7 | 0.497414 | `azmcp_redis_cluster_database_list` | ❌ |
| 8 | 0.456313 | `azmcp_storage_blob_container_list` | ❌ |
| 9 | 0.449759 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 10 | 0.447875 | `azmcp_postgres_database_list` | ❌ |
| 11 | 0.437993 | `azmcp_kusto_table_list` | ❌ |
| 12 | 0.400098 | `azmcp_storage_account_list` | ❌ |
| 13 | 0.396280 | `azmcp_monitor_table_list` | ❌ |
| 14 | 0.384707 | `azmcp_storage_blob_list` | ❌ |
| 15 | 0.377400 | `azmcp_storage_account_details` | ❌ |
| 16 | 0.361429 | `azmcp_monitor_table_type_list` | ❌ |
| 17 | 0.344445 | `azmcp_keyvault_key_list` | ❌ |
| 18 | 0.342424 | `azmcp_acr_registry_repository_list` | ❌ |
| 19 | 0.339516 | `azmcp_kusto_cluster_list` | ❌ |
| 20 | 0.335852 | `azmcp_datadog_monitoredresources_list` | ❌ |

---

## Test 43

**Expected Tool:** `azmcp_kusto_cluster_get`  
**Prompt:** Show me the details of the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.482148 | `azmcp_kusto_cluster_get` | ✅ **EXPECTED** |
| 2 | 0.464523 | `azmcp_aks_cluster_get` | ❌ |
| 3 | 0.457669 | `azmcp_redis_cluster_list` | ❌ |
| 4 | 0.416762 | `azmcp_redis_cluster_database_list` | ❌ |
| 5 | 0.364174 | `azmcp_loadtesting_testrun_get` | ❌ |
| 6 | 0.363309 | `azmcp_aks_cluster_list` | ❌ |
| 7 | 0.344871 | `azmcp_sql_db_show` | ❌ |
| 8 | 0.344590 | `azmcp_kusto_database_list` | ❌ |
| 9 | 0.332639 | `azmcp_kusto_cluster_list` | ❌ |
| 10 | 0.326472 | `azmcp_redis_cache_list` | ❌ |
| 11 | 0.318754 | `azmcp_kusto_query` | ❌ |
| 12 | 0.314617 | `azmcp_kusto_table_schema` | ❌ |
| 13 | 0.304033 | `azmcp_storage_blob_container_details` | ❌ |
| 14 | 0.301024 | `azmcp_grafana_list` | ❌ |
| 15 | 0.300008 | `azmcp_kusto_table_list` | ❌ |
| 16 | 0.289673 | `azmcp_storage_account_details` | ❌ |
| 17 | 0.288707 | `azmcp_quota_usage_check` | ❌ |
| 18 | 0.284522 | `azmcp_servicebus_queue_details` | ❌ |
| 19 | 0.269678 | `azmcp_sql_db_list` | ❌ |
| 20 | 0.249991 | `azmcp_storage_blob_details` | ❌ |

---

## Test 44

**Expected Tool:** `azmcp_kusto_cluster_list`  
**Prompt:** List all Data Explorer clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651218 | `azmcp_kusto_cluster_list` | ✅ **EXPECTED** |
| 2 | 0.644037 | `azmcp_redis_cluster_list` | ❌ |
| 3 | 0.549093 | `azmcp_kusto_database_list` | ❌ |
| 4 | 0.535599 | `azmcp_aks_cluster_list` | ❌ |
| 5 | 0.509396 | `azmcp_grafana_list` | ❌ |
| 6 | 0.505912 | `azmcp_redis_cache_list` | ❌ |
| 7 | 0.492107 | `azmcp_postgres_server_list` | ❌ |
| 8 | 0.487882 | `azmcp_search_service_list` | ❌ |
| 9 | 0.487634 | `azmcp_monitor_workspace_list` | ❌ |
| 10 | 0.486159 | `azmcp_kusto_cluster_get` | ❌ |
| 11 | 0.460255 | `azmcp_cosmos_account_list` | ❌ |
| 12 | 0.458754 | `azmcp_redis_cluster_database_list` | ❌ |
| 13 | 0.451500 | `azmcp_kusto_table_list` | ❌ |
| 14 | 0.428236 | `azmcp_storage_table_list` | ❌ |
| 15 | 0.427759 | `azmcp_subscription_list` | ❌ |
| 16 | 0.411791 | `azmcp_group_list` | ❌ |
| 17 | 0.407832 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 18 | 0.404929 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 19 | 0.402530 | `azmcp_storage_account_list` | ❌ |
| 20 | 0.395458 | `azmcp_cosmos_database_list` | ❌ |

---

## Test 45

**Expected Tool:** `azmcp_kusto_cluster_list`  
**Prompt:** Show me my Data Explorer clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.437363 | `azmcp_redis_cluster_list` | ❌ |
| 2 | 0.391087 | `azmcp_redis_cluster_database_list` | ❌ |
| 3 | 0.386126 | `azmcp_kusto_cluster_list` | ✅ **EXPECTED** |
| 4 | 0.359551 | `azmcp_kusto_database_list` | ❌ |
| 5 | 0.341784 | `azmcp_kusto_cluster_get` | ❌ |
| 6 | 0.338691 | `azmcp_aks_cluster_list` | ❌ |
| 7 | 0.314734 | `azmcp_aks_cluster_get` | ❌ |
| 8 | 0.303083 | `azmcp_grafana_list` | ❌ |
| 9 | 0.292838 | `azmcp_redis_cache_list` | ❌ |
| 10 | 0.287768 | `azmcp_kusto_sample` | ❌ |
| 11 | 0.285603 | `azmcp_kusto_query` | ❌ |
| 12 | 0.283331 | `azmcp_kusto_table_list` | ❌ |
| 13 | 0.270931 | `azmcp_monitor_table_list` | ❌ |
| 14 | 0.264130 | `azmcp_monitor_workspace_list` | ❌ |
| 15 | 0.264112 | `azmcp_monitor_table_type_list` | ❌ |
| 16 | 0.263226 | `azmcp_quota_usage_check` | ❌ |
| 17 | 0.261960 | `azmcp_postgres_server_list` | ❌ |
| 18 | 0.260320 | `azmcp_deploy_app_logs_get` | ❌ |
| 19 | 0.255960 | `azmcp_postgres_database_list` | ❌ |
| 20 | 0.240130 | `azmcp_redis_cache_accesspolicy_list` | ❌ |

---

## Test 46

**Expected Tool:** `azmcp_kusto_cluster_list`  
**Prompt:** Show me the Data Explorer clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584053 | `azmcp_redis_cluster_list` | ❌ |
| 2 | 0.549797 | `azmcp_kusto_cluster_list` | ✅ **EXPECTED** |
| 3 | 0.470832 | `azmcp_aks_cluster_list` | ❌ |
| 4 | 0.469570 | `azmcp_kusto_cluster_get` | ❌ |
| 5 | 0.464294 | `azmcp_kusto_database_list` | ❌ |
| 6 | 0.462945 | `azmcp_grafana_list` | ❌ |
| 7 | 0.446124 | `azmcp_redis_cache_list` | ❌ |
| 8 | 0.440413 | `azmcp_monitor_workspace_list` | ❌ |
| 9 | 0.432048 | `azmcp_postgres_server_list` | ❌ |
| 10 | 0.421307 | `azmcp_search_service_list` | ❌ |
| 11 | 0.396253 | `azmcp_redis_cluster_database_list` | ❌ |
| 12 | 0.392541 | `azmcp_kusto_table_list` | ❌ |
| 13 | 0.386776 | `azmcp_cosmos_account_list` | ❌ |
| 14 | 0.377490 | `azmcp_kusto_query` | ❌ |
| 15 | 0.371088 | `azmcp_subscription_list` | ❌ |
| 16 | 0.368890 | `azmcp_quota_usage_check` | ❌ |
| 17 | 0.366262 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 18 | 0.365972 | `azmcp_storage_table_list` | ❌ |
| 19 | 0.365323 | `azmcp_quota_region_availability_list` | ❌ |
| 20 | 0.347845 | `azmcp_aks_cluster_get` | ❌ |

---

## Test 47

**Expected Tool:** `azmcp_kusto_database_list`  
**Prompt:** List all databases in the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628129 | `azmcp_redis_cluster_database_list` | ❌ |
| 2 | 0.610646 | `azmcp_kusto_database_list` | ✅ **EXPECTED** |
| 3 | 0.553218 | `azmcp_postgres_database_list` | ❌ |
| 4 | 0.549673 | `azmcp_cosmos_database_list` | ❌ |
| 5 | 0.474354 | `azmcp_kusto_table_list` | ❌ |
| 6 | 0.461496 | `azmcp_sql_db_list` | ❌ |
| 7 | 0.459180 | `azmcp_redis_cluster_list` | ❌ |
| 8 | 0.434330 | `azmcp_postgres_table_list` | ❌ |
| 9 | 0.431669 | `azmcp_kusto_cluster_list` | ❌ |
| 10 | 0.404095 | `azmcp_monitor_table_list` | ❌ |
| 11 | 0.396060 | `azmcp_cosmos_database_container_list` | ❌ |
| 12 | 0.379966 | `azmcp_storage_table_list` | ❌ |
| 13 | 0.375535 | `azmcp_cosmos_account_list` | ❌ |
| 14 | 0.363663 | `azmcp_postgres_server_list` | ❌ |
| 15 | 0.357739 | `azmcp_monitor_table_type_list` | ❌ |
| 16 | 0.350201 | `azmcp_aks_cluster_list` | ❌ |
| 17 | 0.340735 | `azmcp_redis_cache_list` | ❌ |
| 18 | 0.334270 | `azmcp_grafana_list` | ❌ |
| 19 | 0.320622 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 20 | 0.318850 | `azmcp_kusto_query` | ❌ |

---

## Test 48

**Expected Tool:** `azmcp_kusto_database_list`  
**Prompt:** Show me the databases in the Data Explorer cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.597975 | `azmcp_redis_cluster_database_list` | ❌ |
| 2 | 0.558503 | `azmcp_kusto_database_list` | ✅ **EXPECTED** |
| 3 | 0.497144 | `azmcp_cosmos_database_list` | ❌ |
| 4 | 0.486732 | `azmcp_postgres_database_list` | ❌ |
| 5 | 0.440064 | `azmcp_kusto_table_list` | ❌ |
| 6 | 0.427251 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.422588 | `azmcp_sql_db_list` | ❌ |
| 8 | 0.383664 | `azmcp_kusto_cluster_list` | ❌ |
| 9 | 0.368013 | `azmcp_postgres_table_list` | ❌ |
| 10 | 0.362905 | `azmcp_cosmos_database_container_list` | ❌ |
| 11 | 0.359378 | `azmcp_monitor_table_list` | ❌ |
| 12 | 0.338777 | `azmcp_storage_table_list` | ❌ |
| 13 | 0.336400 | `azmcp_monitor_table_type_list` | ❌ |
| 14 | 0.336104 | `azmcp_cosmos_account_list` | ❌ |
| 15 | 0.334803 | `azmcp_kusto_table_schema` | ❌ |
| 16 | 0.330351 | `azmcp_postgres_server_list` | ❌ |
| 17 | 0.313195 | `azmcp_redis_cache_list` | ❌ |
| 18 | 0.311076 | `azmcp_aks_cluster_list` | ❌ |
| 19 | 0.309809 | `azmcp_kusto_sample` | ❌ |
| 20 | 0.305756 | `azmcp_kusto_query` | ❌ |

---

## Test 49

**Expected Tool:** `azmcp_kusto_query`  
**Prompt:** Show me all items that contain the word <search_term> in the Data Explorer table <table_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.381341 | `azmcp_kusto_query` | ✅ **EXPECTED** |
| 2 | 0.363468 | `azmcp_kusto_sample` | ❌ |
| 3 | 0.349203 | `azmcp_monitor_table_list` | ❌ |
| 4 | 0.345731 | `azmcp_redis_cluster_list` | ❌ |
| 5 | 0.334848 | `azmcp_kusto_table_list` | ❌ |
| 6 | 0.319169 | `azmcp_redis_cluster_database_list` | ❌ |
| 7 | 0.318978 | `azmcp_kusto_table_schema` | ❌ |
| 8 | 0.315082 | `azmcp_monitor_table_type_list` | ❌ |
| 9 | 0.308266 | `azmcp_kusto_database_list` | ❌ |
| 10 | 0.304060 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 11 | 0.302980 | `azmcp_postgres_table_list` | ❌ |
| 12 | 0.300459 | `azmcp_storage_table_list` | ❌ |
| 13 | 0.300410 | `azmcp_search_service_list` | ❌ |
| 14 | 0.295012 | `azmcp_search_index_list` | ❌ |
| 15 | 0.292165 | `azmcp_kusto_cluster_list` | ❌ |
| 16 | 0.279886 | `azmcp_monitor_resource_log_query` | ❌ |
| 17 | 0.263830 | `azmcp_grafana_list` | ❌ |
| 18 | 0.263292 | `azmcp_kusto_cluster_get` | ❌ |
| 19 | 0.258032 | `azmcp_aks_cluster_list` | ❌ |
| 20 | 0.257840 | `azmcp_postgres_database_list` | ❌ |

---

## Test 50

**Expected Tool:** `azmcp_kusto_sample`  
**Prompt:** Show me a data sample from the Data Explorer table <table_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.537154 | `azmcp_kusto_sample` | ✅ **EXPECTED** |
| 2 | 0.419463 | `azmcp_kusto_table_schema` | ❌ |
| 3 | 0.391423 | `azmcp_kusto_table_list` | ❌ |
| 4 | 0.377056 | `azmcp_redis_cluster_database_list` | ❌ |
| 5 | 0.364611 | `azmcp_postgres_table_schema_get` | ❌ |
| 6 | 0.361845 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.343671 | `azmcp_monitor_table_type_list` | ❌ |
| 8 | 0.341674 | `azmcp_monitor_table_list` | ❌ |
| 9 | 0.337281 | `azmcp_kusto_database_list` | ❌ |
| 10 | 0.329933 | `azmcp_storage_table_list` | ❌ |
| 11 | 0.319239 | `azmcp_kusto_query` | ❌ |
| 12 | 0.318189 | `azmcp_postgres_table_list` | ❌ |
| 13 | 0.310196 | `azmcp_kusto_cluster_get` | ❌ |
| 14 | 0.285941 | `azmcp_kusto_cluster_list` | ❌ |
| 15 | 0.267689 | `azmcp_aks_cluster_get` | ❌ |
| 16 | 0.257683 | `azmcp_monitor_resource_log_query` | ❌ |
| 17 | 0.254555 | `azmcp_postgres_database_list` | ❌ |
| 18 | 0.249706 | `azmcp_aks_cluster_list` | ❌ |
| 19 | 0.243553 | `azmcp_postgres_server_list` | ❌ |
| 20 | 0.240744 | `azmcp_grafana_list` | ❌ |

---

## Test 51

**Expected Tool:** `azmcp_kusto_table_list`  
**Prompt:** List all tables in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591680 | `azmcp_kusto_table_list` | ✅ **EXPECTED** |
| 2 | 0.585497 | `azmcp_postgres_table_list` | ❌ |
| 3 | 0.550397 | `azmcp_monitor_table_list` | ❌ |
| 4 | 0.521478 | `azmcp_kusto_database_list` | ❌ |
| 5 | 0.520836 | `azmcp_redis_cluster_database_list` | ❌ |
| 6 | 0.517346 | `azmcp_storage_table_list` | ❌ |
| 7 | 0.475596 | `azmcp_postgres_database_list` | ❌ |
| 8 | 0.464608 | `azmcp_monitor_table_type_list` | ❌ |
| 9 | 0.449635 | `azmcp_kusto_table_schema` | ❌ |
| 10 | 0.436528 | `azmcp_cosmos_database_list` | ❌ |
| 11 | 0.429369 | `azmcp_redis_cluster_list` | ❌ |
| 12 | 0.412129 | `azmcp_kusto_sample` | ❌ |
| 13 | 0.410540 | `azmcp_kusto_cluster_list` | ❌ |
| 14 | 0.385042 | `azmcp_postgres_table_schema_get` | ❌ |
| 15 | 0.380749 | `azmcp_cosmos_database_container_list` | ❌ |
| 16 | 0.362036 | `azmcp_sql_db_list` | ❌ |
| 17 | 0.349460 | `azmcp_postgres_server_list` | ❌ |
| 18 | 0.337157 | `azmcp_kusto_query` | ❌ |
| 19 | 0.330240 | `azmcp_aks_cluster_list` | ❌ |
| 20 | 0.330005 | `azmcp_grafana_list` | ❌ |

---

## Test 52

**Expected Tool:** `azmcp_kusto_table_list`  
**Prompt:** Show me the tables in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.549885 | `azmcp_kusto_table_list` | ✅ **EXPECTED** |
| 2 | 0.523432 | `azmcp_postgres_table_list` | ❌ |
| 3 | 0.494108 | `azmcp_redis_cluster_database_list` | ❌ |
| 4 | 0.490717 | `azmcp_monitor_table_list` | ❌ |
| 5 | 0.475412 | `azmcp_kusto_database_list` | ❌ |
| 6 | 0.466302 | `azmcp_storage_table_list` | ❌ |
| 7 | 0.466212 | `azmcp_kusto_table_schema` | ❌ |
| 8 | 0.431964 | `azmcp_monitor_table_type_list` | ❌ |
| 9 | 0.425623 | `azmcp_kusto_sample` | ❌ |
| 10 | 0.421413 | `azmcp_postgres_database_list` | ❌ |
| 11 | 0.403445 | `azmcp_redis_cluster_list` | ❌ |
| 12 | 0.402646 | `azmcp_postgres_table_schema_get` | ❌ |
| 13 | 0.391081 | `azmcp_cosmos_database_list` | ❌ |
| 14 | 0.367187 | `azmcp_kusto_cluster_list` | ❌ |
| 15 | 0.348891 | `azmcp_cosmos_database_container_list` | ❌ |
| 16 | 0.335264 | `azmcp_sql_db_list` | ❌ |
| 17 | 0.330383 | `azmcp_kusto_query` | ❌ |
| 18 | 0.326690 | `azmcp_postgres_server_list` | ❌ |
| 19 | 0.314766 | `azmcp_kusto_cluster_get` | ❌ |
| 20 | 0.300527 | `azmcp_aks_cluster_list` | ❌ |

---

## Test 53

**Expected Tool:** `azmcp_kusto_table_schema`  
**Prompt:** Show me the schema for table <table_name> in the Data Explorer database <database_name> in cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.588151 | `azmcp_kusto_table_schema` | ✅ **EXPECTED** |
| 2 | 0.564311 | `azmcp_postgres_table_schema_get` | ❌ |
| 3 | 0.437466 | `azmcp_kusto_table_list` | ❌ |
| 4 | 0.432585 | `azmcp_kusto_sample` | ❌ |
| 5 | 0.413686 | `azmcp_monitor_table_list` | ❌ |
| 6 | 0.398632 | `azmcp_redis_cluster_database_list` | ❌ |
| 7 | 0.387660 | `azmcp_postgres_table_list` | ❌ |
| 8 | 0.366346 | `azmcp_monitor_table_type_list` | ❌ |
| 9 | 0.366081 | `azmcp_kusto_database_list` | ❌ |
| 10 | 0.357533 | `azmcp_storage_table_list` | ❌ |
| 11 | 0.345263 | `azmcp_redis_cluster_list` | ❌ |
| 12 | 0.314580 | `azmcp_kusto_cluster_get` | ❌ |
| 13 | 0.309145 | `azmcp_postgres_database_list` | ❌ |
| 14 | 0.308550 | `azmcp_sql_db_show` | ❌ |
| 15 | 0.298243 | `azmcp_kusto_query` | ❌ |
| 16 | 0.294840 | `azmcp_cosmos_database_list` | ❌ |
| 17 | 0.282712 | `azmcp_kusto_cluster_list` | ❌ |
| 18 | 0.275795 | `azmcp_cosmos_database_container_list` | ❌ |
| 19 | 0.273998 | `azmcp_aks_cluster_get` | ❌ |
| 20 | 0.273625 | `azmcp_sql_db_list` | ❌ |

---

## Test 54

**Expected Tool:** `azmcp_postgres_database_list`  
**Prompt:** List all PostgreSQL databases in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.815617 | `azmcp_postgres_database_list` | ✅ **EXPECTED** |
| 2 | 0.644014 | `azmcp_postgres_table_list` | ❌ |
| 3 | 0.622790 | `azmcp_postgres_server_list` | ❌ |
| 4 | 0.542685 | `azmcp_postgres_server_config_get` | ❌ |
| 5 | 0.490904 | `azmcp_postgres_server_param_get` | ❌ |
| 6 | 0.453436 | `azmcp_sql_db_list` | ❌ |
| 7 | 0.444410 | `azmcp_redis_cluster_database_list` | ❌ |
| 8 | 0.435828 | `azmcp_cosmos_database_list` | ❌ |
| 9 | 0.418348 | `azmcp_postgres_database_query` | ❌ |
| 10 | 0.414679 | `azmcp_postgres_server_param_set` | ❌ |
| 11 | 0.413602 | `azmcp_postgres_table_schema_get` | ❌ |
| 12 | 0.407877 | `azmcp_kusto_database_list` | ❌ |
| 13 | 0.319946 | `azmcp_kusto_table_list` | ❌ |
| 14 | 0.293787 | `azmcp_cosmos_database_container_list` | ❌ |
| 15 | 0.292441 | `azmcp_cosmos_account_list` | ❌ |
| 16 | 0.289334 | `azmcp_grafana_list` | ❌ |
| 17 | 0.252438 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 18 | 0.249563 | `azmcp_kusto_cluster_list` | ❌ |
| 19 | 0.245546 | `azmcp_acr_registry_repository_list` | ❌ |
| 20 | 0.245456 | `azmcp_group_list` | ❌ |

---

## Test 55

**Expected Tool:** `azmcp_postgres_database_list`  
**Prompt:** Show me the PostgreSQL databases in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.760033 | `azmcp_postgres_database_list` | ✅ **EXPECTED** |
| 2 | 0.589783 | `azmcp_postgres_server_list` | ❌ |
| 3 | 0.585891 | `azmcp_postgres_table_list` | ❌ |
| 4 | 0.552660 | `azmcp_postgres_server_config_get` | ❌ |
| 5 | 0.495629 | `azmcp_postgres_server_param_get` | ❌ |
| 6 | 0.433860 | `azmcp_redis_cluster_database_list` | ❌ |
| 7 | 0.430589 | `azmcp_postgres_table_schema_get` | ❌ |
| 8 | 0.426839 | `azmcp_postgres_database_query` | ❌ |
| 9 | 0.416937 | `azmcp_sql_db_list` | ❌ |
| 10 | 0.412972 | `azmcp_postgres_server_param_set` | ❌ |
| 11 | 0.385475 | `azmcp_cosmos_database_list` | ❌ |
| 12 | 0.365997 | `azmcp_kusto_database_list` | ❌ |
| 13 | 0.281529 | `azmcp_kusto_table_list` | ❌ |
| 14 | 0.261442 | `azmcp_cosmos_database_container_list` | ❌ |
| 15 | 0.257971 | `azmcp_grafana_list` | ❌ |
| 16 | 0.247726 | `azmcp_cosmos_account_list` | ❌ |
| 17 | 0.235403 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.227995 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 19 | 0.222503 | `azmcp_kusto_table_schema` | ❌ |
| 20 | 0.212647 | `azmcp_appconfig_account_list` | ❌ |

---

## Test 56

**Expected Tool:** `azmcp_postgres_database_query`  
**Prompt:** Show me all items that contain the word <search_term> in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.546211 | `azmcp_postgres_database_list` | ❌ |
| 2 | 0.503267 | `azmcp_postgres_table_list` | ❌ |
| 3 | 0.466599 | `azmcp_postgres_server_list` | ❌ |
| 4 | 0.415817 | `azmcp_postgres_database_query` | ✅ **EXPECTED** |
| 5 | 0.403969 | `azmcp_postgres_server_param_get` | ❌ |
| 6 | 0.403924 | `azmcp_postgres_server_config_get` | ❌ |
| 7 | 0.380446 | `azmcp_postgres_table_schema_get` | ❌ |
| 8 | 0.354323 | `azmcp_postgres_server_param_set` | ❌ |
| 9 | 0.301808 | `azmcp_sql_db_list` | ❌ |
| 10 | 0.277622 | `azmcp_sql_db_show` | ❌ |
| 11 | 0.264914 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 12 | 0.262356 | `azmcp_cosmos_database_list` | ❌ |
| 13 | 0.262160 | `azmcp_kusto_query` | ❌ |
| 14 | 0.254174 | `azmcp_kusto_table_list` | ❌ |
| 15 | 0.248628 | `azmcp_cosmos_database_container_list` | ❌ |
| 16 | 0.244295 | `azmcp_kusto_database_list` | ❌ |
| 17 | 0.236363 | `azmcp_grafana_list` | ❌ |
| 18 | 0.218677 | `azmcp_kusto_table_schema` | ❌ |
| 19 | 0.217855 | `azmcp_kusto_sample` | ❌ |
| 20 | 0.189002 | `azmcp_foundry_models_list` | ❌ |

---

## Test 57

**Expected Tool:** `azmcp_postgres_server_config_get`  
**Prompt:** Show me the configuration of PostgreSQL server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.756593 | `azmcp_postgres_server_config_get` | ✅ **EXPECTED** |
| 2 | 0.599471 | `azmcp_postgres_server_param_get` | ❌ |
| 3 | 0.535229 | `azmcp_postgres_server_param_set` | ❌ |
| 4 | 0.535049 | `azmcp_postgres_database_list` | ❌ |
| 5 | 0.518574 | `azmcp_postgres_server_list` | ❌ |
| 6 | 0.463172 | `azmcp_postgres_table_list` | ❌ |
| 7 | 0.431476 | `azmcp_postgres_table_schema_get` | ❌ |
| 8 | 0.394675 | `azmcp_postgres_database_query` | ❌ |
| 9 | 0.269224 | `azmcp_appconfig_kv_list` | ❌ |
| 10 | 0.269018 | `azmcp_sql_db_list` | ❌ |
| 11 | 0.261733 | `azmcp_sql_db_show` | ❌ |
| 12 | 0.235724 | `azmcp_loadtesting_testrun_list` | ❌ |
| 13 | 0.222849 | `azmcp_appconfig_account_list` | ❌ |
| 14 | 0.222599 | `azmcp_loadtesting_test_get` | ❌ |
| 15 | 0.208314 | `azmcp_appconfig_kv_show` | ❌ |
| 16 | 0.174936 | `azmcp_aks_cluster_get` | ❌ |
| 17 | 0.168322 | `azmcp_kusto_table_schema` | ❌ |
| 18 | 0.160792 | `azmcp_grafana_list` | ❌ |
| 19 | 0.156381 | `azmcp_aks_cluster_list` | ❌ |
| 20 | 0.154250 | `azmcp_appconfig_kv_unlock` | ❌ |

---

## Test 58

**Expected Tool:** `azmcp_postgres_server_list`  
**Prompt:** List all PostgreSQL servers in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.900023 | `azmcp_postgres_server_list` | ✅ **EXPECTED** |
| 2 | 0.640733 | `azmcp_postgres_database_list` | ❌ |
| 3 | 0.565914 | `azmcp_postgres_table_list` | ❌ |
| 4 | 0.538997 | `azmcp_postgres_server_config_get` | ❌ |
| 5 | 0.507621 | `azmcp_postgres_server_param_get` | ❌ |
| 6 | 0.483663 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.472458 | `azmcp_grafana_list` | ❌ |
| 8 | 0.453841 | `azmcp_kusto_cluster_list` | ❌ |
| 9 | 0.446509 | `azmcp_redis_cache_list` | ❌ |
| 10 | 0.430475 | `azmcp_search_service_list` | ❌ |
| 11 | 0.406903 | `azmcp_monitor_workspace_list` | ❌ |
| 12 | 0.406617 | `azmcp_cosmos_account_list` | ❌ |
| 13 | 0.401097 | `azmcp_sql_db_list` | ❌ |
| 14 | 0.398262 | `azmcp_aks_cluster_list` | ❌ |
| 15 | 0.397428 | `azmcp_kusto_database_list` | ❌ |
| 16 | 0.389191 | `azmcp_appconfig_account_list` | ❌ |
| 17 | 0.373699 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.365995 | `azmcp_group_list` | ❌ |
| 19 | 0.351894 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 20 | 0.346981 | `azmcp_cosmos_database_list` | ❌ |

---

## Test 59

**Expected Tool:** `azmcp_postgres_server_list`  
**Prompt:** Show me my PostgreSQL servers  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.674327 | `azmcp_postgres_server_list` | ✅ **EXPECTED** |
| 2 | 0.607062 | `azmcp_postgres_database_list` | ❌ |
| 3 | 0.576349 | `azmcp_postgres_server_config_get` | ❌ |
| 4 | 0.522996 | `azmcp_postgres_table_list` | ❌ |
| 5 | 0.506171 | `azmcp_postgres_server_param_get` | ❌ |
| 6 | 0.409406 | `azmcp_postgres_database_query` | ❌ |
| 7 | 0.400088 | `azmcp_postgres_server_param_set` | ❌ |
| 8 | 0.372955 | `azmcp_postgres_table_schema_get` | ❌ |
| 9 | 0.318087 | `azmcp_redis_cluster_database_list` | ❌ |
| 10 | 0.305360 | `azmcp_sql_server_entra-admin_list` | ❌ |
| 11 | 0.274763 | `azmcp_grafana_list` | ❌ |
| 12 | 0.260533 | `azmcp_cosmos_database_list` | ❌ |
| 13 | 0.253264 | `azmcp_kusto_database_list` | ❌ |
| 14 | 0.245117 | `azmcp_aks_cluster_list` | ❌ |
| 15 | 0.241835 | `azmcp_kusto_cluster_list` | ❌ |
| 16 | 0.239500 | `azmcp_appconfig_account_list` | ❌ |
| 17 | 0.229842 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.227547 | `azmcp_cosmos_account_list` | ❌ |
| 19 | 0.219274 | `azmcp_cosmos_database_container_list` | ❌ |
| 20 | 0.218726 | `azmcp_datadog_monitoredresources_list` | ❌ |

---

## Test 60

**Expected Tool:** `azmcp_postgres_server_list`  
**Prompt:** Show me the PostgreSQL servers in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.832155 | `azmcp_postgres_server_list` | ✅ **EXPECTED** |
| 2 | 0.579232 | `azmcp_postgres_database_list` | ❌ |
| 3 | 0.531804 | `azmcp_postgres_server_config_get` | ❌ |
| 4 | 0.514445 | `azmcp_postgres_table_list` | ❌ |
| 5 | 0.505869 | `azmcp_postgres_server_param_get` | ❌ |
| 6 | 0.452608 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.444127 | `azmcp_grafana_list` | ❌ |
| 8 | 0.414695 | `azmcp_redis_cache_list` | ❌ |
| 9 | 0.411590 | `azmcp_search_service_list` | ❌ |
| 10 | 0.410719 | `azmcp_postgres_database_query` | ❌ |
| 11 | 0.403538 | `azmcp_kusto_cluster_list` | ❌ |
| 12 | 0.399866 | `azmcp_postgres_table_schema_get` | ❌ |
| 13 | 0.376954 | `azmcp_cosmos_account_list` | ❌ |
| 14 | 0.362650 | `azmcp_kusto_database_list` | ❌ |
| 15 | 0.362557 | `azmcp_appconfig_account_list` | ❌ |
| 16 | 0.359889 | `azmcp_aks_cluster_list` | ❌ |
| 17 | 0.358409 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.334101 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 19 | 0.325681 | `azmcp_group_list` | ❌ |
| 20 | 0.311765 | `azmcp_marketplace_product_get` | ❌ |

---

## Test 61

**Expected Tool:** `azmcp_postgres_server_param`  
**Prompt:** Show me if the parameter my PostgreSQL server <server> has replication enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.594753 | `azmcp_postgres_server_param_get` | ❌ |
| 2 | 0.539671 | `azmcp_postgres_server_config_get` | ❌ |
| 3 | 0.489693 | `azmcp_postgres_server_list` | ❌ |
| 4 | 0.480826 | `azmcp_postgres_server_param_set` | ❌ |
| 5 | 0.451871 | `azmcp_postgres_database_list` | ❌ |
| 6 | 0.357606 | `azmcp_postgres_table_list` | ❌ |
| 7 | 0.330875 | `azmcp_postgres_table_schema_get` | ❌ |
| 8 | 0.305351 | `azmcp_postgres_database_query` | ❌ |
| 9 | 0.227987 | `azmcp_redis_cluster_database_list` | ❌ |
| 10 | 0.207560 | `azmcp_sql_db_list` | ❌ |
| 11 | 0.185273 | `azmcp_appconfig_kv_list` | ❌ |
| 12 | 0.174107 | `azmcp_grafana_list` | ❌ |
| 13 | 0.169190 | `azmcp_appconfig_account_list` | ❌ |
| 14 | 0.158090 | `azmcp_cosmos_database_list` | ❌ |
| 15 | 0.155785 | `azmcp_appconfig_kv_show` | ❌ |
| 16 | 0.145346 | `azmcp_loadtesting_testrun_list` | ❌ |
| 17 | 0.145056 | `azmcp_kusto_database_list` | ❌ |
| 18 | 0.142288 | `azmcp_aks_cluster_list` | ❌ |
| 19 | 0.140139 | `azmcp_cosmos_account_list` | ❌ |
| 20 | 0.138625 | `azmcp_datadog_monitoredresources_list` | ❌ |

---

## Test 62

**Expected Tool:** `azmcp_postgres_server_param_set`  
**Prompt:** Enable replication for my PostgreSQL server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.488474 | `azmcp_postgres_server_config_get` | ❌ |
| 2 | 0.469794 | `azmcp_postgres_server_list` | ❌ |
| 3 | 0.464562 | `azmcp_postgres_server_param_set` | ✅ **EXPECTED** |
| 4 | 0.447011 | `azmcp_postgres_server_param_get` | ❌ |
| 5 | 0.440760 | `azmcp_postgres_database_list` | ❌ |
| 6 | 0.354049 | `azmcp_postgres_table_list` | ❌ |
| 7 | 0.341624 | `azmcp_postgres_database_query` | ❌ |
| 8 | 0.317484 | `azmcp_postgres_table_schema_get` | ❌ |
| 9 | 0.184982 | `azmcp_redis_cluster_database_list` | ❌ |
| 10 | 0.176538 | `azmcp_sql_db_list` | ❌ |
| 11 | 0.133385 | `azmcp_kusto_sample` | ❌ |
| 12 | 0.127120 | `azmcp_kusto_database_list` | ❌ |
| 13 | 0.123491 | `azmcp_kusto_table_schema` | ❌ |
| 14 | 0.118089 | `azmcp_cosmos_database_list` | ❌ |
| 15 | 0.114978 | `azmcp_kusto_cluster_get` | ❌ |
| 16 | 0.113841 | `azmcp_grafana_list` | ❌ |
| 17 | 0.112605 | `azmcp_deploy_plan_get` | ❌ |
| 18 | 0.108485 | `azmcp_kusto_table_list` | ❌ |
| 19 | 0.102847 | `azmcp_extension_azqr` | ❌ |
| 20 | 0.102298 | `azmcp_loadtesting_testrun_list` | ❌ |

---

## Test 63

**Expected Tool:** `azmcp_postgres_table_list`  
**Prompt:** List all tables in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.789883 | `azmcp_postgres_table_list` | ✅ **EXPECTED** |
| 2 | 0.750580 | `azmcp_postgres_database_list` | ❌ |
| 3 | 0.574930 | `azmcp_postgres_server_list` | ❌ |
| 4 | 0.519820 | `azmcp_postgres_table_schema_get` | ❌ |
| 5 | 0.501400 | `azmcp_postgres_server_config_get` | ❌ |
| 6 | 0.449190 | `azmcp_postgres_database_query` | ❌ |
| 7 | 0.432813 | `azmcp_kusto_table_list` | ❌ |
| 8 | 0.430171 | `azmcp_postgres_server_param_get` | ❌ |
| 9 | 0.394396 | `azmcp_monitor_table_list` | ❌ |
| 10 | 0.386992 | `azmcp_redis_cluster_database_list` | ❌ |
| 11 | 0.380821 | `azmcp_storage_table_list` | ❌ |
| 12 | 0.373673 | `azmcp_cosmos_database_list` | ❌ |
| 13 | 0.352211 | `azmcp_kusto_database_list` | ❌ |
| 14 | 0.308203 | `azmcp_kusto_table_schema` | ❌ |
| 15 | 0.299785 | `azmcp_cosmos_database_container_list` | ❌ |
| 16 | 0.257808 | `azmcp_grafana_list` | ❌ |
| 17 | 0.256245 | `azmcp_kusto_sample` | ❌ |
| 18 | 0.249162 | `azmcp_cosmos_account_list` | ❌ |
| 19 | 0.236931 | `azmcp_appconfig_kv_list` | ❌ |
| 20 | 0.229889 | `azmcp_kusto_cluster_list` | ❌ |

---

## Test 64

**Expected Tool:** `azmcp_postgres_table_list`  
**Prompt:** Show me the tables in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.736083 | `azmcp_postgres_table_list` | ✅ **EXPECTED** |
| 2 | 0.690112 | `azmcp_postgres_database_list` | ❌ |
| 3 | 0.558357 | `azmcp_postgres_table_schema_get` | ❌ |
| 4 | 0.543331 | `azmcp_postgres_server_list` | ❌ |
| 5 | 0.521570 | `azmcp_postgres_server_config_get` | ❌ |
| 6 | 0.464929 | `azmcp_postgres_database_query` | ❌ |
| 7 | 0.447184 | `azmcp_postgres_server_param_get` | ❌ |
| 8 | 0.390392 | `azmcp_kusto_table_list` | ❌ |
| 9 | 0.371151 | `azmcp_redis_cluster_database_list` | ❌ |
| 10 | 0.371036 | `azmcp_postgres_server_param_set` | ❌ |
| 11 | 0.360749 | `azmcp_monitor_table_list` | ❌ |
| 12 | 0.334843 | `azmcp_kusto_table_schema` | ❌ |
| 13 | 0.315781 | `azmcp_cosmos_database_list` | ❌ |
| 14 | 0.307262 | `azmcp_kusto_database_list` | ❌ |
| 15 | 0.272906 | `azmcp_kusto_sample` | ❌ |
| 16 | 0.266178 | `azmcp_cosmos_database_container_list` | ❌ |
| 17 | 0.243542 | `azmcp_grafana_list` | ❌ |
| 18 | 0.207521 | `azmcp_cosmos_account_list` | ❌ |
| 19 | 0.205697 | `azmcp_appconfig_kv_list` | ❌ |
| 20 | 0.202950 | `azmcp_datadog_monitoredresources_list` | ❌ |

---

## Test 65

**Expected Tool:** `azmcp_postgres_table_schema_get`  
**Prompt:** Show me the schema of table <table> in the PostgreSQL database <database> in server <server>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.714877 | `azmcp_postgres_table_schema_get` | ✅ **EXPECTED** |
| 2 | 0.597846 | `azmcp_postgres_table_list` | ❌ |
| 3 | 0.574230 | `azmcp_postgres_database_list` | ❌ |
| 4 | 0.508082 | `azmcp_postgres_server_config_get` | ❌ |
| 5 | 0.475665 | `azmcp_kusto_table_schema` | ❌ |
| 6 | 0.443816 | `azmcp_postgres_server_param_get` | ❌ |
| 7 | 0.442553 | `azmcp_postgres_server_list` | ❌ |
| 8 | 0.427530 | `azmcp_postgres_database_query` | ❌ |
| 9 | 0.362687 | `azmcp_postgres_server_param_set` | ❌ |
| 10 | 0.336037 | `azmcp_sql_db_show` | ❌ |
| 11 | 0.322766 | `azmcp_kusto_table_list` | ❌ |
| 12 | 0.312465 | `azmcp_monitor_table_list` | ❌ |
| 13 | 0.303748 | `azmcp_kusto_sample` | ❌ |
| 14 | 0.253353 | `azmcp_cosmos_database_list` | ❌ |
| 15 | 0.239225 | `azmcp_kusto_database_list` | ❌ |
| 16 | 0.212206 | `azmcp_cosmos_database_container_list` | ❌ |
| 17 | 0.201673 | `azmcp_grafana_list` | ❌ |
| 18 | 0.185124 | `azmcp_appconfig_kv_list` | ❌ |
| 19 | 0.183782 | `azmcp_bicepschema_get` | ❌ |
| 20 | 0.167077 | `azmcp_cosmos_database_container_item_query` | ❌ |

---

## Test 66

**Expected Tool:** `azmcp_extension_azd`  
**Prompt:** Create a To-Do list web application that uses NodeJS and MongoDB  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.241366 | `azmcp_extension_az` | ❌ |
| 2 | 0.203706 | `azmcp_deploy_iac_rules_get` | ❌ |
| 3 | 0.200024 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 4 | 0.196585 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 5 | 0.190019 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 6 | 0.187620 | `azmcp_deploy_plan_get` | ❌ |
| 7 | 0.185433 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 8 | 0.181543 | `azmcp_redis_cluster_database_list` | ❌ |
| 9 | 0.177946 | `azmcp_cosmos_database_list` | ❌ |
| 10 | 0.173269 | `azmcp_extension_azd` | ✅ **EXPECTED** |
| 11 | 0.165761 | `azmcp_postgres_table_list` | ❌ |
| 12 | 0.159142 | `azmcp_deploy_app_logs_get` | ❌ |
| 13 | 0.148122 | `azmcp_postgres_database_list` | ❌ |
| 14 | 0.145985 | `azmcp_storage_share_file_list` | ❌ |
| 15 | 0.145027 | `azmcp_redis_cluster_list` | ❌ |
| 16 | 0.138516 | `azmcp_storage_blob_container_create` | ❌ |
| 17 | 0.137936 | `azmcp_postgres_database_query` | ❌ |
| 18 | 0.129433 | `azmcp_sql_db_list` | ❌ |
| 19 | 0.126407 | `azmcp_storage_blob_list` | ❌ |
| 20 | 0.126141 | `azmcp_postgres_server_list` | ❌ |

---

## Test 67

**Expected Tool:** `azmcp_extension_azd`  
**Prompt:** Deploy my web application to Azure App Service  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.544719 | `azmcp_deploy_plan_get` | ❌ |
| 2 | 0.489853 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 3 | 0.441305 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 4 | 0.437357 | `azmcp_foundry_models_deploy` | ❌ |
| 5 | 0.421067 | `azmcp_deploy_app_logs_get` | ❌ |
| 6 | 0.394023 | `azmcp_deploy_iac_rules_get` | ❌ |
| 7 | 0.364145 | `azmcp_extension_azd` | ✅ **EXPECTED** |
| 8 | 0.361106 | `azmcp_foundry_models_deployments_list` | ❌ |
| 9 | 0.356426 | `azmcp_extension_az` | ❌ |
| 10 | 0.345356 | `azmcp_functionapp_list` | ❌ |
| 11 | 0.308550 | `azmcp_storage_blob_upload` | ❌ |
| 12 | 0.299738 | `azmcp_search_index_list` | ❌ |
| 13 | 0.297374 | `azmcp_workbooks_delete` | ❌ |
| 14 | 0.275067 | `azmcp_quota_usage_check` | ❌ |
| 15 | 0.273452 | `azmcp_search_service_list` | ❌ |
| 16 | 0.250828 | `azmcp_storage_queue_message_send` | ❌ |
| 17 | 0.249133 | `azmcp_storage_account_details` | ❌ |
| 18 | 0.244902 | `azmcp_sql_db_list` | ❌ |
| 19 | 0.239027 | `azmcp_storage_account_create` | ❌ |
| 20 | 0.237105 | `azmcp_resourcehealth_availability-status_get` | ❌ |

---

## Test 68

**Expected Tool:** `azmcp_deploy_app_logs_get`  
**Prompt:** Show me the log of the application deployed by azd  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.686701 | `azmcp_deploy_app_logs_get` | ✅ **EXPECTED** |
| 2 | 0.486709 | `azmcp_extension_azd` | ❌ |
| 3 | 0.471692 | `azmcp_deploy_plan_get` | ❌ |
| 4 | 0.404890 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 5 | 0.392565 | `azmcp_deploy_iac_rules_get` | ❌ |
| 6 | 0.389603 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 7 | 0.341175 | `azmcp_monitor_resource_log_query` | ❌ |
| 8 | 0.340723 | `azmcp_extension_az` | ❌ |
| 9 | 0.334992 | `azmcp_quota_usage_check` | ❌ |
| 10 | 0.328168 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 11 | 0.327028 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 12 | 0.325553 | `azmcp_extension_azqr` | ❌ |
| 13 | 0.307291 | `azmcp_sql_db_show` | ❌ |
| 14 | 0.299854 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 15 | 0.288973 | `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list` | ❌ |
| 16 | 0.284418 | `azmcp_sql_db_list` | ❌ |
| 17 | 0.281761 | `azmcp_monitor_workspace_list` | ❌ |
| 18 | 0.275999 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 19 | 0.275276 | `azmcp_search_index_list` | ❌ |
| 20 | 0.270537 | `azmcp_storage_account_details` | ❌ |

---

## Test 69

**Expected Tool:** `azmcp_deploy_architecture_diagram_generate`  
**Prompt:** Generate the azure architecture diagram for this application  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.680640 | `azmcp_deploy_architecture_diagram_generate` | ✅ **EXPECTED** |
| 2 | 0.562521 | `azmcp_deploy_plan_get` | ❌ |
| 3 | 0.497193 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 4 | 0.435921 | `azmcp_deploy_iac_rules_get` | ❌ |
| 5 | 0.417940 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 6 | 0.393602 | `azmcp_bestpractices_get` | ❌ |
| 7 | 0.371127 | `azmcp_deploy_app_logs_get` | ❌ |
| 8 | 0.343117 | `azmcp_quota_usage_check` | ❌ |
| 9 | 0.323166 | `azmcp_extension_az` | ❌ |
| 10 | 0.322230 | `azmcp_extension_azqr` | ❌ |
| 11 | 0.321444 | `azmcp_extension_azd` | ❌ |
| 12 | 0.263521 | `azmcp_quota_region_availability_list` | ❌ |
| 13 | 0.263337 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 14 | 0.244720 | `azmcp_subscription_list` | ❌ |
| 15 | 0.242778 | `azmcp_storage_account_details` | ❌ |
| 16 | 0.239647 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 17 | 0.235433 | `azmcp_search_index_list` | ❌ |
| 18 | 0.234774 | `azmcp_search_service_list` | ❌ |
| 19 | 0.224830 | `azmcp_workbooks_delete` | ❌ |
| 20 | 0.221064 | `azmcp_role_assignment_list` | ❌ |

---

## Test 70

**Expected Tool:** `azmcp_deploy_iac_rules_get`  
**Prompt:** Show me the rules to generate bicep scripts  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.529092 | `azmcp_deploy_iac_rules_get` | ✅ **EXPECTED** |
| 2 | 0.428630 | `azmcp_bestpractices_get` | ❌ |
| 3 | 0.420630 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 4 | 0.415286 | `azmcp_extension_az` | ❌ |
| 5 | 0.404829 | `azmcp_bicepschema_get` | ❌ |
| 6 | 0.341436 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 7 | 0.304788 | `azmcp_deploy_plan_get` | ❌ |
| 8 | 0.266851 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 9 | 0.265367 | `azmcp_sql_server_firewall-rule_list` | ❌ |
| 10 | 0.243314 | `azmcp_extension_azd` | ❌ |
| 11 | 0.223979 | `azmcp_extension_azqr` | ❌ |
| 12 | 0.219521 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.201288 | `azmcp_quota_region_availability_list` | ❌ |
| 14 | 0.199541 | `azmcp_storage_share_file_list` | ❌ |
| 15 | 0.196151 | `azmcp_storage_blob_details` | ❌ |
| 16 | 0.188615 | `azmcp_role_assignment_list` | ❌ |
| 17 | 0.183296 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 18 | 0.175772 | `azmcp_storage_table_list` | ❌ |
| 19 | 0.168251 | `azmcp_redis_cache_list` | ❌ |
| 20 | 0.167343 | `azmcp_sql_server_entra-admin_list` | ❌ |

---

## Test 71

**Expected Tool:** `azmcp_deploy_pipeline_guidance_get`  
**Prompt:** How can I create a CI/CD pipeline to deploy this app to Azure?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.638841 | `azmcp_deploy_pipeline_guidance_get` | ✅ **EXPECTED** |
| 2 | 0.499242 | `azmcp_deploy_plan_get` | ❌ |
| 3 | 0.448918 | `azmcp_deploy_iac_rules_get` | ❌ |
| 4 | 0.386921 | `azmcp_extension_az` | ❌ |
| 5 | 0.375202 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 6 | 0.373363 | `azmcp_deploy_app_logs_get` | ❌ |
| 7 | 0.346892 | `azmcp_extension_azd` | ❌ |
| 8 | 0.338440 | `azmcp_foundry_models_deploy` | ❌ |
| 9 | 0.327522 | `azmcp_bestpractices_get` | ❌ |
| 10 | 0.327410 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 11 | 0.230335 | `azmcp_storage_blob_upload` | ❌ |
| 12 | 0.230063 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.214450 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.198364 | `azmcp_storage_queue_message_send` | ❌ |
| 15 | 0.194866 | `azmcp_workbooks_delete` | ❌ |
| 16 | 0.163763 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 17 | 0.163702 | `azmcp_storage_datalake_directory_create` | ❌ |
| 18 | 0.163337 | `azmcp_monitor_resource_log_query` | ❌ |
| 19 | 0.161544 | `azmcp_workbooks_create` | ❌ |
| 20 | 0.160898 | `azmcp_resourcehealth_availability-status_get` | ❌ |

---

## Test 72

**Expected Tool:** `azmcp_deploy_plan_get`  
**Prompt:** Create a plan to deploy this application to azure  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.688055 | `azmcp_deploy_plan_get` | ✅ **EXPECTED** |
| 2 | 0.587903 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 3 | 0.499385 | `azmcp_deploy_iac_rules_get` | ❌ |
| 4 | 0.498575 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 5 | 0.428036 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 6 | 0.421875 | `azmcp_bestpractices_get` | ❌ |
| 7 | 0.416245 | `azmcp_extension_az` | ❌ |
| 8 | 0.413718 | `azmcp_loadtesting_test_create` | ❌ |
| 9 | 0.393518 | `azmcp_deploy_app_logs_get` | ❌ |
| 10 | 0.378091 | `azmcp_extension_azd` | ❌ |
| 11 | 0.312839 | `azmcp_quota_usage_check` | ❌ |
| 12 | 0.279583 | `azmcp_workbooks_delete` | ❌ |
| 13 | 0.277163 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.252696 | `azmcp_workbooks_create` | ❌ |
| 15 | 0.242496 | `azmcp_quota_region_availability_list` | ❌ |
| 16 | 0.242042 | `azmcp_storage_account_details` | ❌ |
| 17 | 0.237090 | `azmcp_storage_blob_upload` | ❌ |
| 18 | 0.229583 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 19 | 0.227009 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 20 | 0.226962 | `azmcp_sql_db_list` | ❌ |

---

## Test 73

**Expected Tool:** `azmcp_functionapp_list`  
**Prompt:** List all function apps in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.782226 | `azmcp_functionapp_list` | ✅ **EXPECTED** |
| 2 | 0.547255 | `azmcp_search_service_list` | ❌ |
| 3 | 0.516618 | `azmcp_cosmos_account_list` | ❌ |
| 4 | 0.516217 | `azmcp_appconfig_account_list` | ❌ |
| 5 | 0.489561 | `azmcp_storage_account_list` | ❌ |
| 6 | 0.485259 | `azmcp_subscription_list` | ❌ |
| 7 | 0.474425 | `azmcp_kusto_cluster_list` | ❌ |
| 8 | 0.465575 | `azmcp_group_list` | ❌ |
| 9 | 0.464557 | `azmcp_monitor_workspace_list` | ❌ |
| 10 | 0.455388 | `azmcp_postgres_server_list` | ❌ |
| 11 | 0.454709 | `azmcp_aks_cluster_list` | ❌ |
| 12 | 0.451429 | `azmcp_storage_table_list` | ❌ |
| 13 | 0.445099 | `azmcp_redis_cache_list` | ❌ |
| 14 | 0.442614 | `azmcp_redis_cluster_list` | ❌ |
| 15 | 0.432144 | `azmcp_grafana_list` | ❌ |
| 16 | 0.431611 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 17 | 0.414796 | `azmcp_foundry_models_deployments_list` | ❌ |
| 18 | 0.411904 | `azmcp_sql_db_list` | ❌ |
| 19 | 0.411581 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 20 | 0.411426 | `azmcp_acr_registry_list` | ❌ |

---

## Test 74

**Expected Tool:** `azmcp_functionapp_list`  
**Prompt:** Show me my Azure function apps  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.610288 | `azmcp_functionapp_list` | ✅ **EXPECTED** |
| 2 | 0.452132 | `azmcp_deploy_app_logs_get` | ❌ |
| 3 | 0.385832 | `azmcp_foundry_models_deployments_list` | ❌ |
| 4 | 0.374655 | `azmcp_appconfig_account_list` | ❌ |
| 5 | 0.372790 | `azmcp_cosmos_account_list` | ❌ |
| 6 | 0.369681 | `azmcp_subscription_list` | ❌ |
| 7 | 0.368018 | `azmcp_extension_az` | ❌ |
| 8 | 0.368004 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 9 | 0.359788 | `azmcp_search_service_list` | ❌ |
| 10 | 0.358879 | `azmcp_bestpractices_get` | ❌ |
| 11 | 0.358720 | `azmcp_deploy_plan_get` | ❌ |
| 12 | 0.357329 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.353279 | `azmcp_extension_azd` | ❌ |
| 14 | 0.345104 | `azmcp_storage_blob_container_list` | ❌ |
| 15 | 0.334019 | `azmcp_role_assignment_list` | ❌ |
| 16 | 0.333136 | `azmcp_sql_db_list` | ❌ |
| 17 | 0.329125 | `azmcp_storage_account_list` | ❌ |
| 18 | 0.327907 | `azmcp_monitor_workspace_list` | ❌ |
| 19 | 0.326628 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 20 | 0.322615 | `azmcp_storage_table_list` | ❌ |

---

## Test 75

**Expected Tool:** `azmcp_functionapp_list`  
**Prompt:** What function apps do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.440824 | `azmcp_functionapp_list` | ✅ **EXPECTED** |
| 2 | 0.348106 | `azmcp_deploy_app_logs_get` | ❌ |
| 3 | 0.256927 | `azmcp_extension_az` | ❌ |
| 4 | 0.249658 | `azmcp_appconfig_account_list` | ❌ |
| 5 | 0.244782 | `azmcp_appconfig_kv_list` | ❌ |
| 6 | 0.240729 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 7 | 0.239514 | `azmcp_foundry_models_deployments_list` | ❌ |
| 8 | 0.235352 | `azmcp_extension_azd` | ❌ |
| 9 | 0.208396 | `azmcp_foundry_models_list` | ❌ |
| 10 | 0.207391 | `azmcp_quota_usage_check` | ❌ |
| 11 | 0.201390 | `azmcp_bestpractices_get` | ❌ |
| 12 | 0.195857 | `azmcp_role_assignment_list` | ❌ |
| 13 | 0.194503 | `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list` | ❌ |
| 14 | 0.185142 | `azmcp_monitor_resource_log_query` | ❌ |
| 15 | 0.184266 | `azmcp_monitor_workspace_list` | ❌ |
| 16 | 0.184051 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 17 | 0.182124 | `azmcp_storage_table_list` | ❌ |
| 18 | 0.181525 | `azmcp_storage_share_file_list` | ❌ |
| 19 | 0.179181 | `azmcp_storage_blob_details` | ❌ |
| 20 | 0.175281 | `azmcp_subscription_list` | ❌ |

---

## Test 76

**Expected Tool:** `azmcp_keyvault_certificate_create`  
**Prompt:** Create a new certificate called <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.740325 | `azmcp_keyvault_certificate_create` | ✅ **EXPECTED** |
| 2 | 0.595854 | `azmcp_keyvault_key_create` | ❌ |
| 3 | 0.590518 | `azmcp_keyvault_secret_create` | ❌ |
| 4 | 0.575761 | `azmcp_keyvault_certificate_list` | ❌ |
| 5 | 0.543043 | `azmcp_keyvault_certificate_get` | ❌ |
| 6 | 0.526698 | `azmcp_keyvault_certificate_import` | ❌ |
| 7 | 0.434757 | `azmcp_keyvault_key_list` | ❌ |
| 8 | 0.413783 | `azmcp_keyvault_secret_list` | ❌ |
| 9 | 0.394768 | `azmcp_storage_account_create` | ❌ |
| 10 | 0.330026 | `azmcp_appconfig_kv_set` | ❌ |
| 11 | 0.308667 | `azmcp_loadtesting_test_create` | ❌ |
| 12 | 0.300980 | `azmcp_storage_datalake_directory_create` | ❌ |
| 13 | 0.285184 | `azmcp_workbooks_create` | ❌ |
| 14 | 0.254339 | `azmcp_storage_account_details` | ❌ |
| 15 | 0.235260 | `azmcp_storage_blob_container_list` | ❌ |
| 16 | 0.233821 | `azmcp_storage_table_list` | ❌ |
| 17 | 0.226937 | `azmcp_storage_account_list` | ❌ |
| 18 | 0.219479 | `azmcp_subscription_list` | ❌ |
| 19 | 0.210729 | `azmcp_search_service_list` | ❌ |
| 20 | 0.208916 | `azmcp_virtualdesktop_hostpool_list` | ❌ |

---

## Test 77

**Expected Tool:** `azmcp_keyvault_certificate_get`  
**Prompt:** Show me the certificate <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.627979 | `azmcp_keyvault_certificate_get` | ✅ **EXPECTED** |
| 2 | 0.624457 | `azmcp_keyvault_certificate_list` | ❌ |
| 3 | 0.565069 | `azmcp_keyvault_certificate_create` | ❌ |
| 4 | 0.539554 | `azmcp_keyvault_certificate_import` | ❌ |
| 5 | 0.493550 | `azmcp_keyvault_key_list` | ❌ |
| 6 | 0.474934 | `azmcp_keyvault_secret_list` | ❌ |
| 7 | 0.423728 | `azmcp_keyvault_key_create` | ❌ |
| 8 | 0.418860 | `azmcp_keyvault_secret_create` | ❌ |
| 9 | 0.390699 | `azmcp_appconfig_kv_show` | ❌ |
| 10 | 0.346167 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.341449 | `azmcp_storage_account_details` | ❌ |
| 12 | 0.317250 | `azmcp_storage_blob_container_list` | ❌ |
| 13 | 0.317177 | `azmcp_storage_table_list` | ❌ |
| 14 | 0.293954 | `azmcp_storage_account_list` | ❌ |
| 15 | 0.293421 | `azmcp_subscription_list` | ❌ |
| 16 | 0.288844 | `azmcp_storage_blob_list` | ❌ |
| 17 | 0.276581 | `azmcp_role_assignment_list` | ❌ |
| 18 | 0.273791 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 19 | 0.271911 | `azmcp_quota_usage_check` | ❌ |
| 20 | 0.269735 | `azmcp_sql_db_show` | ❌ |

---

## Test 78

**Expected Tool:** `azmcp_keyvault_certificate_get`  
**Prompt:** Show me the details of the certificate <certificate_name> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.662324 | `azmcp_keyvault_certificate_get` | ✅ **EXPECTED** |
| 2 | 0.606625 | `azmcp_keyvault_certificate_list` | ❌ |
| 3 | 0.540155 | `azmcp_keyvault_certificate_import` | ❌ |
| 4 | 0.535228 | `azmcp_keyvault_certificate_create` | ❌ |
| 5 | 0.499373 | `azmcp_keyvault_key_list` | ❌ |
| 6 | 0.482024 | `azmcp_keyvault_secret_list` | ❌ |
| 7 | 0.432557 | `azmcp_storage_account_details` | ❌ |
| 8 | 0.415722 | `azmcp_keyvault_key_create` | ❌ |
| 9 | 0.412430 | `azmcp_keyvault_secret_create` | ❌ |
| 10 | 0.411136 | `azmcp_appconfig_kv_show` | ❌ |
| 11 | 0.365386 | `azmcp_sql_db_show` | ❌ |
| 12 | 0.363228 | `azmcp_aks_cluster_get` | ❌ |
| 13 | 0.332921 | `azmcp_storage_blob_container_details` | ❌ |
| 14 | 0.316364 | `azmcp_storage_blob_container_list` | ❌ |
| 15 | 0.315096 | `azmcp_storage_table_list` | ❌ |
| 16 | 0.305778 | `azmcp_subscription_list` | ❌ |
| 17 | 0.301710 | `azmcp_servicebus_queue_details` | ❌ |
| 18 | 0.295651 | `azmcp_storage_account_list` | ❌ |
| 19 | 0.290918 | `azmcp_storage_blob_list` | ❌ |
| 20 | 0.289520 | `azmcp_role_assignment_list` | ❌ |

---

## Test 79

**Expected Tool:** `azmcp_keyvault_certificate_import`  
**Prompt:** Import the certificate in file <file_path> into the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.649993 | `azmcp_keyvault_certificate_import` | ✅ **EXPECTED** |
| 2 | 0.521193 | `azmcp_keyvault_certificate_create` | ❌ |
| 3 | 0.469706 | `azmcp_keyvault_certificate_get` | ❌ |
| 4 | 0.466980 | `azmcp_keyvault_certificate_list` | ❌ |
| 5 | 0.426600 | `azmcp_keyvault_key_create` | ❌ |
| 6 | 0.398021 | `azmcp_keyvault_secret_create` | ❌ |
| 7 | 0.364981 | `azmcp_keyvault_key_list` | ❌ |
| 8 | 0.337918 | `azmcp_keyvault_secret_list` | ❌ |
| 9 | 0.269560 | `azmcp_appconfig_kv_lock` | ❌ |
| 10 | 0.267356 | `azmcp_appconfig_kv_set` | ❌ |
| 11 | 0.253889 | `azmcp_storage_datalake_file-system_list-paths` | ❌ |
| 12 | 0.238854 | `azmcp_storage_blob_upload` | ❌ |
| 13 | 0.230787 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.224373 | `azmcp_workbooks_delete` | ❌ |
| 15 | 0.217294 | `azmcp_storage_blob_container_list` | ❌ |
| 16 | 0.214526 | `azmcp_storage_account_details` | ❌ |
| 17 | 0.200472 | `azmcp_storage_datalake_directory_create` | ❌ |
| 18 | 0.199045 | `azmcp_storage_table_list` | ❌ |
| 19 | 0.195655 | `azmcp_storage_blob_list` | ❌ |
| 20 | 0.193594 | `azmcp_storage_account_list` | ❌ |

---

## Test 80

**Expected Tool:** `azmcp_keyvault_certificate_import`  
**Prompt:** Import a certificate into the key vault <key_vault_account_name> using the name <certificate_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.649676 | `azmcp_keyvault_certificate_import` | ✅ **EXPECTED** |
| 2 | 0.629856 | `azmcp_keyvault_certificate_create` | ❌ |
| 3 | 0.527382 | `azmcp_keyvault_certificate_list` | ❌ |
| 4 | 0.525743 | `azmcp_keyvault_certificate_get` | ❌ |
| 5 | 0.491898 | `azmcp_keyvault_key_create` | ❌ |
| 6 | 0.472207 | `azmcp_keyvault_secret_create` | ❌ |
| 7 | 0.399956 | `azmcp_keyvault_key_list` | ❌ |
| 8 | 0.377945 | `azmcp_keyvault_secret_list` | ❌ |
| 9 | 0.291302 | `azmcp_storage_account_create` | ❌ |
| 10 | 0.287107 | `azmcp_appconfig_kv_set` | ❌ |
| 11 | 0.265369 | `azmcp_appconfig_kv_lock` | ❌ |
| 12 | 0.238392 | `azmcp_storage_account_details` | ❌ |
| 13 | 0.234376 | `azmcp_storage_table_list` | ❌ |
| 14 | 0.229343 | `azmcp_storage_blob_container_list` | ❌ |
| 15 | 0.227232 | `azmcp_workbooks_delete` | ❌ |
| 16 | 0.211454 | `azmcp_storage_datalake_directory_create` | ❌ |
| 17 | 0.209618 | `azmcp_storage_account_list` | ❌ |
| 18 | 0.203150 | `azmcp_storage_blob_upload` | ❌ |
| 19 | 0.197598 | `azmcp_sql_db_show` | ❌ |
| 20 | 0.196937 | `azmcp_workbooks_create` | ❌ |

---

## Test 81

**Expected Tool:** `azmcp_keyvault_certificate_list`  
**Prompt:** List all certificates in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.761947 | `azmcp_keyvault_certificate_list` | ✅ **EXPECTED** |
| 2 | 0.637509 | `azmcp_keyvault_key_list` | ❌ |
| 3 | 0.608600 | `azmcp_keyvault_secret_list` | ❌ |
| 4 | 0.566460 | `azmcp_keyvault_certificate_get` | ❌ |
| 5 | 0.539642 | `azmcp_keyvault_certificate_create` | ❌ |
| 6 | 0.484660 | `azmcp_keyvault_certificate_import` | ❌ |
| 7 | 0.478100 | `azmcp_cosmos_account_list` | ❌ |
| 8 | 0.453226 | `azmcp_cosmos_database_list` | ❌ |
| 9 | 0.440871 | `azmcp_storage_blob_container_list` | ❌ |
| 10 | 0.431201 | `azmcp_cosmos_database_container_list` | ❌ |
| 11 | 0.429531 | `azmcp_storage_table_list` | ❌ |
| 12 | 0.425556 | `azmcp_storage_account_list` | ❌ |
| 13 | 0.424379 | `azmcp_keyvault_key_create` | ❌ |
| 14 | 0.417908 | `azmcp_storage_blob_list` | ❌ |
| 15 | 0.408042 | `azmcp_subscription_list` | ❌ |
| 16 | 0.373773 | `azmcp_search_index_list` | ❌ |
| 17 | 0.368478 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 18 | 0.366071 | `azmcp_storage_account_details` | ❌ |
| 19 | 0.358938 | `azmcp_role_assignment_list` | ❌ |
| 20 | 0.357371 | `azmcp_search_service_list` | ❌ |

---

## Test 82

**Expected Tool:** `azmcp_keyvault_certificate_list`  
**Prompt:** Show me the certificates in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.660547 | `azmcp_keyvault_certificate_list` | ✅ **EXPECTED** |
| 2 | 0.570282 | `azmcp_keyvault_certificate_get` | ❌ |
| 3 | 0.540152 | `azmcp_keyvault_key_list` | ❌ |
| 4 | 0.516386 | `azmcp_keyvault_secret_list` | ❌ |
| 5 | 0.509150 | `azmcp_keyvault_certificate_create` | ❌ |
| 6 | 0.483404 | `azmcp_keyvault_certificate_import` | ❌ |
| 7 | 0.420506 | `azmcp_cosmos_account_list` | ❌ |
| 8 | 0.396055 | `azmcp_keyvault_key_create` | ❌ |
| 9 | 0.390164 | `azmcp_keyvault_secret_create` | ❌ |
| 10 | 0.382983 | `azmcp_storage_blob_container_list` | ❌ |
| 11 | 0.382082 | `azmcp_cosmos_database_list` | ❌ |
| 12 | 0.373188 | `azmcp_storage_account_details` | ❌ |
| 13 | 0.372424 | `azmcp_storage_table_list` | ❌ |
| 14 | 0.362782 | `azmcp_subscription_list` | ❌ |
| 15 | 0.361862 | `azmcp_storage_account_list` | ❌ |
| 16 | 0.351372 | `azmcp_storage_blob_list` | ❌ |
| 17 | 0.323177 | `azmcp_role_assignment_list` | ❌ |
| 18 | 0.317493 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 19 | 0.317235 | `azmcp_search_index_list` | ❌ |
| 20 | 0.300672 | `azmcp_search_service_list` | ❌ |

---

## Test 83

**Expected Tool:** `azmcp_keyvault_key_create`  
**Prompt:** Create a new key called <key_name> with the RSA type in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.676449 | `azmcp_keyvault_key_create` | ✅ **EXPECTED** |
| 2 | 0.569354 | `azmcp_keyvault_secret_create` | ❌ |
| 3 | 0.555855 | `azmcp_keyvault_certificate_create` | ❌ |
| 4 | 0.465804 | `azmcp_keyvault_key_list` | ❌ |
| 5 | 0.420495 | `azmcp_storage_account_create` | ❌ |
| 6 | 0.417481 | `azmcp_keyvault_certificate_list` | ❌ |
| 7 | 0.413115 | `azmcp_keyvault_secret_list` | ❌ |
| 8 | 0.412593 | `azmcp_keyvault_certificate_import` | ❌ |
| 9 | 0.397032 | `azmcp_appconfig_kv_set` | ❌ |
| 10 | 0.389770 | `azmcp_keyvault_certificate_get` | ❌ |
| 11 | 0.340635 | `azmcp_appconfig_kv_lock` | ❌ |
| 12 | 0.287171 | `azmcp_storage_datalake_directory_create` | ❌ |
| 13 | 0.282294 | `azmcp_storage_account_details` | ❌ |
| 14 | 0.265798 | `azmcp_storage_account_list` | ❌ |
| 15 | 0.261777 | `azmcp_workbooks_create` | ❌ |
| 16 | 0.260941 | `azmcp_storage_blob_container_list` | ❌ |
| 17 | 0.252001 | `azmcp_storage_table_list` | ❌ |
| 18 | 0.235534 | `azmcp_storage_blob_list` | ❌ |
| 19 | 0.223440 | `azmcp_storage_queue_message_send` | ❌ |
| 20 | 0.215770 | `azmcp_subscription_list` | ❌ |

---

## Test 84

**Expected Tool:** `azmcp_keyvault_key_list`  
**Prompt:** List all keys in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.737152 | `azmcp_keyvault_key_list` | ✅ **EXPECTED** |
| 2 | 0.650126 | `azmcp_keyvault_secret_list` | ❌ |
| 3 | 0.631730 | `azmcp_keyvault_certificate_list` | ❌ |
| 4 | 0.498767 | `azmcp_cosmos_account_list` | ❌ |
| 5 | 0.473916 | `azmcp_storage_table_list` | ❌ |
| 6 | 0.473155 | `azmcp_storage_blob_container_list` | ❌ |
| 7 | 0.468044 | `azmcp_cosmos_database_list` | ❌ |
| 8 | 0.467326 | `azmcp_keyvault_key_create` | ❌ |
| 9 | 0.461513 | `azmcp_storage_account_list` | ❌ |
| 10 | 0.455805 | `azmcp_keyvault_certificate_get` | ❌ |
| 11 | 0.455016 | `azmcp_storage_blob_list` | ❌ |
| 12 | 0.443785 | `azmcp_cosmos_database_container_list` | ❌ |
| 13 | 0.439167 | `azmcp_appconfig_kv_list` | ❌ |
| 14 | 0.428288 | `azmcp_keyvault_secret_create` | ❌ |
| 15 | 0.426909 | `azmcp_subscription_list` | ❌ |
| 16 | 0.403964 | `azmcp_storage_account_details` | ❌ |
| 17 | 0.402742 | `azmcp_search_index_list` | ❌ |
| 18 | 0.378059 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 19 | 0.376452 | `azmcp_search_service_list` | ❌ |
| 20 | 0.360638 | `azmcp_storage_datalake_file-system_list-paths` | ❌ |

---

## Test 85

**Expected Tool:** `azmcp_keyvault_key_list`  
**Prompt:** Show me the keys in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609457 | `azmcp_keyvault_key_list` | ✅ **EXPECTED** |
| 2 | 0.535090 | `azmcp_keyvault_secret_list` | ❌ |
| 3 | 0.520114 | `azmcp_keyvault_certificate_list` | ❌ |
| 4 | 0.479818 | `azmcp_keyvault_certificate_get` | ❌ |
| 5 | 0.462249 | `azmcp_keyvault_key_create` | ❌ |
| 6 | 0.429508 | `azmcp_keyvault_secret_create` | ❌ |
| 7 | 0.421475 | `azmcp_cosmos_account_list` | ❌ |
| 8 | 0.412669 | `azmcp_keyvault_certificate_create` | ❌ |
| 9 | 0.408423 | `azmcp_keyvault_certificate_import` | ❌ |
| 10 | 0.405205 | `azmcp_appconfig_kv_show` | ❌ |
| 11 | 0.390487 | `azmcp_storage_blob_container_list` | ❌ |
| 12 | 0.382971 | `azmcp_storage_account_details` | ❌ |
| 13 | 0.375139 | `azmcp_storage_table_list` | ❌ |
| 14 | 0.368473 | `azmcp_storage_account_list` | ❌ |
| 15 | 0.360209 | `azmcp_storage_blob_list` | ❌ |
| 16 | 0.353390 | `azmcp_subscription_list` | ❌ |
| 17 | 0.323400 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 18 | 0.320761 | `azmcp_search_index_list` | ❌ |
| 19 | 0.312486 | `azmcp_storage_blob_container_details` | ❌ |
| 20 | 0.307809 | `azmcp_storage_datalake_file-system_list-paths` | ❌ |

---

## Test 86

**Expected Tool:** `azmcp_keyvault_secret_create`  
**Prompt:** Create a new secret called <secret_name> with value <secret_value> in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.767585 | `azmcp_keyvault_secret_create` | ✅ **EXPECTED** |
| 2 | 0.613500 | `azmcp_keyvault_key_create` | ❌ |
| 3 | 0.572054 | `azmcp_keyvault_certificate_create` | ❌ |
| 4 | 0.516262 | `azmcp_keyvault_secret_list` | ❌ |
| 5 | 0.461799 | `azmcp_appconfig_kv_set` | ❌ |
| 6 | 0.428163 | `azmcp_storage_account_create` | ❌ |
| 7 | 0.417377 | `azmcp_keyvault_key_list` | ❌ |
| 8 | 0.411525 | `azmcp_keyvault_certificate_import` | ❌ |
| 9 | 0.383834 | `azmcp_keyvault_certificate_list` | ❌ |
| 10 | 0.374109 | `azmcp_appconfig_kv_lock` | ❌ |
| 11 | 0.369443 | `azmcp_keyvault_certificate_get` | ❌ |
| 12 | 0.321343 | `azmcp_storage_datalake_directory_create` | ❌ |
| 13 | 0.287749 | `azmcp_storage_account_details` | ❌ |
| 14 | 0.286967 | `azmcp_workbooks_create` | ❌ |
| 15 | 0.275426 | `azmcp_storage_queue_message_send` | ❌ |
| 16 | 0.246571 | `azmcp_storage_blob_container_list` | ❌ |
| 17 | 0.245987 | `azmcp_storage_account_list` | ❌ |
| 18 | 0.236281 | `azmcp_storage_table_list` | ❌ |
| 19 | 0.223103 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 20 | 0.209501 | `azmcp_subscription_list` | ❌ |

---

## Test 87

**Expected Tool:** `azmcp_keyvault_secret_list`  
**Prompt:** List all secrets in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.747383 | `azmcp_keyvault_secret_list` | ✅ **EXPECTED** |
| 2 | 0.617171 | `azmcp_keyvault_key_list` | ❌ |
| 3 | 0.569958 | `azmcp_keyvault_certificate_list` | ❌ |
| 4 | 0.519155 | `azmcp_keyvault_secret_create` | ❌ |
| 5 | 0.455500 | `azmcp_cosmos_account_list` | ❌ |
| 6 | 0.433610 | `azmcp_storage_blob_container_list` | ❌ |
| 7 | 0.433185 | `azmcp_cosmos_database_list` | ❌ |
| 8 | 0.417973 | `azmcp_cosmos_database_container_list` | ❌ |
| 9 | 0.415723 | `azmcp_storage_blob_list` | ❌ |
| 10 | 0.414310 | `azmcp_keyvault_certificate_get` | ❌ |
| 11 | 0.414216 | `azmcp_storage_account_list` | ❌ |
| 12 | 0.410496 | `azmcp_storage_table_list` | ❌ |
| 13 | 0.409822 | `azmcp_keyvault_key_create` | ❌ |
| 14 | 0.392414 | `azmcp_keyvault_certificate_create` | ❌ |
| 15 | 0.391082 | `azmcp_subscription_list` | ❌ |
| 16 | 0.364601 | `azmcp_storage_account_details` | ❌ |
| 17 | 0.355446 | `azmcp_search_index_list` | ❌ |
| 18 | 0.347416 | `azmcp_search_service_list` | ❌ |
| 19 | 0.341082 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 20 | 0.340472 | `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list` | ❌ |

---

## Test 88

**Expected Tool:** `azmcp_keyvault_secret_list`  
**Prompt:** Show me the secrets in the key vault <key_vault_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.615144 | `azmcp_keyvault_secret_list` | ✅ **EXPECTED** |
| 2 | 0.520748 | `azmcp_keyvault_key_list` | ❌ |
| 3 | 0.502421 | `azmcp_keyvault_secret_create` | ❌ |
| 4 | 0.467928 | `azmcp_keyvault_certificate_list` | ❌ |
| 5 | 0.456355 | `azmcp_keyvault_certificate_get` | ❌ |
| 6 | 0.411604 | `azmcp_keyvault_key_create` | ❌ |
| 7 | 0.410957 | `azmcp_appconfig_kv_show` | ❌ |
| 8 | 0.409126 | `azmcp_keyvault_certificate_import` | ❌ |
| 9 | 0.395481 | `azmcp_storage_account_details` | ❌ |
| 10 | 0.385852 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.381659 | `azmcp_keyvault_certificate_create` | ❌ |
| 12 | 0.370457 | `azmcp_storage_blob_container_list` | ❌ |
| 13 | 0.345256 | `azmcp_subscription_list` | ❌ |
| 14 | 0.344682 | `azmcp_storage_blob_list` | ❌ |
| 15 | 0.344339 | `azmcp_storage_table_list` | ❌ |
| 16 | 0.341754 | `azmcp_storage_blob_container_details` | ❌ |
| 17 | 0.336315 | `azmcp_storage_account_list` | ❌ |
| 18 | 0.303769 | `azmcp_quota_usage_check` | ❌ |
| 19 | 0.301358 | `azmcp_storage_blob_details` | ❌ |
| 20 | 0.299295 | `azmcp_search_index_list` | ❌ |

---

## Test 89

**Expected Tool:** `azmcp_aks_cluster_get`  
**Prompt:** Get the configuration of AKS cluster <cluster-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.660869 | `azmcp_aks_cluster_get` | ✅ **EXPECTED** |
| 2 | 0.611113 | `azmcp_aks_cluster_list` | ❌ |
| 3 | 0.463682 | `azmcp_kusto_cluster_get` | ❌ |
| 4 | 0.456804 | `azmcp_loadtesting_test_get` | ❌ |
| 5 | 0.430975 | `azmcp_postgres_server_config_get` | ❌ |
| 6 | 0.392990 | `azmcp_storage_account_details` | ❌ |
| 7 | 0.391924 | `azmcp_appconfig_kv_show` | ❌ |
| 8 | 0.390959 | `azmcp_appconfig_account_list` | ❌ |
| 9 | 0.390819 | `azmcp_appconfig_kv_list` | ❌ |
| 10 | 0.390141 | `azmcp_kusto_cluster_list` | ❌ |
| 11 | 0.367841 | `azmcp_redis_cluster_list` | ❌ |
| 12 | 0.350240 | `azmcp_sql_db_show` | ❌ |
| 13 | 0.349742 | `azmcp_keyvault_certificate_get` | ❌ |
| 14 | 0.349205 | `azmcp_loadtesting_test_create` | ❌ |
| 15 | 0.339882 | `azmcp_sql_db_list` | ❌ |
| 16 | 0.337661 | `azmcp_sql_elastic-pool_list` | ❌ |
| 17 | 0.334959 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 18 | 0.325605 | `azmcp_storage_blob_details` | ❌ |
| 19 | 0.314999 | `azmcp_quota_usage_check` | ❌ |
| 20 | 0.314207 | `azmcp_virtualdesktop_hostpool_list` | ❌ |

---

## Test 90

**Expected Tool:** `azmcp_aks_cluster_get`  
**Prompt:** Show me the details of AKS cluster <cluster-name> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.666849 | `azmcp_aks_cluster_get` | ✅ **EXPECTED** |
| 2 | 0.589287 | `azmcp_aks_cluster_list` | ❌ |
| 3 | 0.508226 | `azmcp_kusto_cluster_get` | ❌ |
| 4 | 0.461466 | `azmcp_sql_db_show` | ❌ |
| 5 | 0.448796 | `azmcp_redis_cluster_list` | ❌ |
| 6 | 0.422993 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 7 | 0.396636 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 8 | 0.385261 | `azmcp_acr_registry_repository_list` | ❌ |
| 9 | 0.384654 | `azmcp_kusto_cluster_list` | ❌ |
| 10 | 0.371570 | `azmcp_group_list` | ❌ |
| 11 | 0.365232 | `azmcp_sql_elastic-pool_list` | ❌ |
| 12 | 0.362332 | `azmcp_sql_db_list` | ❌ |
| 13 | 0.356690 | `azmcp_loadtesting_test_get` | ❌ |
| 14 | 0.355049 | `azmcp_storage_account_details` | ❌ |
| 15 | 0.353625 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 16 | 0.350559 | `azmcp_workbooks_list` | ❌ |
| 17 | 0.349684 | `azmcp_deploy_app_logs_get` | ❌ |
| 18 | 0.347735 | `azmcp_acr_registry_list` | ❌ |
| 19 | 0.345523 | `azmcp_redis_cache_list` | ❌ |
| 20 | 0.339152 | `azmcp_quota_usage_check` | ❌ |

---

## Test 91

**Expected Tool:** `azmcp_aks_cluster_get`  
**Prompt:** Show me the network configuration for AKS cluster <cluster-name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.567273 | `azmcp_aks_cluster_get` | ✅ **EXPECTED** |
| 2 | 0.563140 | `azmcp_aks_cluster_list` | ❌ |
| 3 | 0.368584 | `azmcp_kusto_cluster_get` | ❌ |
| 4 | 0.340349 | `azmcp_loadtesting_test_get` | ❌ |
| 5 | 0.340293 | `azmcp_kusto_cluster_list` | ❌ |
| 6 | 0.334923 | `azmcp_appconfig_account_list` | ❌ |
| 7 | 0.334860 | `azmcp_redis_cluster_list` | ❌ |
| 8 | 0.314513 | `azmcp_appconfig_kv_list` | ❌ |
| 9 | 0.309738 | `azmcp_appconfig_kv_show` | ❌ |
| 10 | 0.296592 | `azmcp_postgres_server_config_get` | ❌ |
| 11 | 0.295183 | `azmcp_loadtesting_test_create` | ❌ |
| 12 | 0.290596 | `azmcp_deploy_app_logs_get` | ❌ |
| 13 | 0.283065 | `azmcp_storage_account_details` | ❌ |
| 14 | 0.275751 | `azmcp_sql_db_show` | ❌ |
| 15 | 0.273186 | `azmcp_monitor_workspace_list` | ❌ |
| 16 | 0.267611 | `azmcp_sql_elastic-pool_list` | ❌ |
| 17 | 0.265086 | `azmcp_sql_db_list` | ❌ |
| 18 | 0.262034 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 19 | 0.262012 | `azmcp_role_assignment_list` | ❌ |
| 20 | 0.258607 | `azmcp_virtualdesktop_hostpool_list` | ❌ |

---

## Test 92

**Expected Tool:** `azmcp_aks_cluster_get`  
**Prompt:** What are the details of my AKS cluster <cluster-name> in <resource-group>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661426 | `azmcp_aks_cluster_get` | ✅ **EXPECTED** |
| 2 | 0.578250 | `azmcp_aks_cluster_list` | ❌ |
| 3 | 0.503925 | `azmcp_kusto_cluster_get` | ❌ |
| 4 | 0.419338 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 5 | 0.418518 | `azmcp_redis_cluster_list` | ❌ |
| 6 | 0.417836 | `azmcp_sql_db_show` | ❌ |
| 7 | 0.390071 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 8 | 0.380074 | `azmcp_storage_account_details` | ❌ |
| 9 | 0.372812 | `azmcp_kusto_cluster_list` | ❌ |
| 10 | 0.367547 | `azmcp_deploy_app_logs_get` | ❌ |
| 11 | 0.360459 | `azmcp_sql_elastic-pool_list` | ❌ |
| 12 | 0.359877 | `azmcp_acr_registry_repository_list` | ❌ |
| 13 | 0.357011 | `azmcp_loadtesting_test_get` | ❌ |
| 14 | 0.354685 | `azmcp_quota_usage_check` | ❌ |
| 15 | 0.353462 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 16 | 0.345652 | `azmcp_sql_db_list` | ❌ |
| 17 | 0.344520 | `azmcp_extension_az` | ❌ |
| 18 | 0.343973 | `azmcp_storage_blob_container_details` | ❌ |
| 19 | 0.343078 | `azmcp_functionapp_list` | ❌ |
| 20 | 0.342039 | `azmcp_storage_account_create` | ❌ |

---

## Test 93

**Expected Tool:** `azmcp_aks_cluster_list`  
**Prompt:** List all AKS clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.800633 | `azmcp_aks_cluster_list` | ✅ **EXPECTED** |
| 2 | 0.690255 | `azmcp_kusto_cluster_list` | ❌ |
| 3 | 0.599940 | `azmcp_redis_cluster_list` | ❌ |
| 4 | 0.560861 | `azmcp_aks_cluster_get` | ❌ |
| 5 | 0.549327 | `azmcp_search_service_list` | ❌ |
| 6 | 0.543665 | `azmcp_monitor_workspace_list` | ❌ |
| 7 | 0.515922 | `azmcp_cosmos_account_list` | ❌ |
| 8 | 0.509202 | `azmcp_kusto_database_list` | ❌ |
| 9 | 0.505723 | `azmcp_functionapp_list` | ❌ |
| 10 | 0.502401 | `azmcp_subscription_list` | ❌ |
| 11 | 0.498121 | `azmcp_group_list` | ❌ |
| 12 | 0.495977 | `azmcp_postgres_server_list` | ❌ |
| 13 | 0.486591 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 14 | 0.486142 | `azmcp_redis_cache_list` | ❌ |
| 15 | 0.483713 | `azmcp_storage_account_list` | ❌ |
| 16 | 0.483592 | `azmcp_kusto_cluster_get` | ❌ |
| 17 | 0.482355 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.481469 | `azmcp_grafana_list` | ❌ |
| 19 | 0.452681 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 20 | 0.445271 | `azmcp_storage_table_list` | ❌ |

---

## Test 94

**Expected Tool:** `azmcp_aks_cluster_list`  
**Prompt:** Show me my Azure Kubernetes Service clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.607967 | `azmcp_aks_cluster_list` | ✅ **EXPECTED** |
| 2 | 0.536412 | `azmcp_aks_cluster_get` | ❌ |
| 3 | 0.492910 | `azmcp_kusto_cluster_list` | ❌ |
| 4 | 0.446270 | `azmcp_redis_cluster_list` | ❌ |
| 5 | 0.409711 | `azmcp_kusto_cluster_get` | ❌ |
| 6 | 0.408385 | `azmcp_kusto_database_list` | ❌ |
| 7 | 0.388143 | `azmcp_search_service_list` | ❌ |
| 8 | 0.387737 | `azmcp_search_index_list` | ❌ |
| 9 | 0.371569 | `azmcp_monitor_workspace_list` | ❌ |
| 10 | 0.370237 | `azmcp_acr_registry_repository_list` | ❌ |
| 11 | 0.363804 | `azmcp_subscription_list` | ❌ |
| 12 | 0.362727 | `azmcp_acr_registry_list` | ❌ |
| 13 | 0.360053 | `azmcp_cosmos_account_list` | ❌ |
| 14 | 0.359675 | `azmcp_storage_blob_container_list` | ❌ |
| 15 | 0.356926 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 16 | 0.355622 | `azmcp_keyvault_secret_list` | ❌ |
| 17 | 0.354900 | `azmcp_keyvault_key_list` | ❌ |
| 18 | 0.349446 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 19 | 0.348854 | `azmcp_postgres_server_list` | ❌ |
| 20 | 0.347078 | `azmcp_quota_usage_check` | ❌ |

---

## Test 95

**Expected Tool:** `azmcp_aks_cluster_list`  
**Prompt:** What AKS clusters do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.623948 | `azmcp_aks_cluster_list` | ✅ **EXPECTED** |
| 2 | 0.530023 | `azmcp_aks_cluster_get` | ❌ |
| 3 | 0.449602 | `azmcp_kusto_cluster_list` | ❌ |
| 4 | 0.416564 | `azmcp_redis_cluster_list` | ❌ |
| 5 | 0.378849 | `azmcp_monitor_workspace_list` | ❌ |
| 6 | 0.377567 | `azmcp_acr_registry_repository_list` | ❌ |
| 7 | 0.364022 | `azmcp_deploy_app_logs_get` | ❌ |
| 8 | 0.345290 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 9 | 0.345241 | `azmcp_kusto_cluster_get` | ❌ |
| 10 | 0.342303 | `azmcp_extension_az` | ❌ |
| 11 | 0.341581 | `azmcp_kusto_database_list` | ❌ |
| 12 | 0.335444 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 13 | 0.334494 | `azmcp_acr_registry_list` | ❌ |
| 14 | 0.328074 | `azmcp_cosmos_account_list` | ❌ |
| 15 | 0.322075 | `azmcp_sql_elastic-pool_list` | ❌ |
| 16 | 0.317238 | `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list` | ❌ |
| 17 | 0.313457 | `azmcp_storage_blob_container_list` | ❌ |
| 18 | 0.312354 | `azmcp_subscription_list` | ❌ |
| 19 | 0.311971 | `azmcp_quota_usage_check` | ❌ |
| 20 | 0.311888 | `azmcp_monitor_table_type_list` | ❌ |

---

## Test 96

**Expected Tool:** `azmcp_loadtesting_test_create`  
**Prompt:** Create a basic URL test using the following endpoint URL <test-url> that runs for 30 minutes with 45 virtual users. The test name is <sample-name> with the test id <test-id> and the load testing resource is <load-test-resource> in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.585388 | `azmcp_loadtesting_test_create` | ✅ **EXPECTED** |
| 2 | 0.531362 | `azmcp_loadtesting_testresource_create` | ❌ |
| 3 | 0.508690 | `azmcp_loadtesting_testrun_create` | ❌ |
| 4 | 0.413857 | `azmcp_loadtesting_testresource_list` | ❌ |
| 5 | 0.402698 | `azmcp_loadtesting_testrun_get` | ❌ |
| 6 | 0.399602 | `azmcp_loadtesting_test_get` | ❌ |
| 7 | 0.346526 | `azmcp_loadtesting_testrun_update` | ❌ |
| 8 | 0.342853 | `azmcp_loadtesting_testrun_list` | ❌ |
| 9 | 0.336804 | `azmcp_monitor_resource_log_query` | ❌ |
| 10 | 0.323398 | `azmcp_monitor_workspace_log_query` | ❌ |
| 11 | 0.322205 | `azmcp_storage_account_create` | ❌ |
| 12 | 0.310490 | `azmcp_keyvault_certificate_create` | ❌ |
| 13 | 0.310144 | `azmcp_workbooks_create` | ❌ |
| 14 | 0.299453 | `azmcp_keyvault_key_create` | ❌ |
| 15 | 0.296991 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 16 | 0.290957 | `azmcp_quota_usage_check` | ❌ |
| 17 | 0.288940 | `azmcp_quota_region_availability_list` | ❌ |
| 18 | 0.280483 | `azmcp_storage_queue_message_send` | ❌ |
| 19 | 0.273887 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 20 | 0.261254 | `azmcp_monitor_metrics_query` | ❌ |

---

## Test 97

**Expected Tool:** `azmcp_loadtesting_test_get`  
**Prompt:** Get the load test with id <test-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.643912 | `azmcp_loadtesting_test_get` | ✅ **EXPECTED** |
| 2 | 0.608881 | `azmcp_loadtesting_testresource_list` | ❌ |
| 3 | 0.574394 | `azmcp_loadtesting_testresource_create` | ❌ |
| 4 | 0.540975 | `azmcp_loadtesting_testrun_get` | ❌ |
| 5 | 0.473733 | `azmcp_loadtesting_testrun_list` | ❌ |
| 6 | 0.473323 | `azmcp_loadtesting_testrun_create` | ❌ |
| 7 | 0.436995 | `azmcp_loadtesting_test_create` | ❌ |
| 8 | 0.407086 | `azmcp_monitor_resource_log_query` | ❌ |
| 9 | 0.397437 | `azmcp_group_list` | ❌ |
| 10 | 0.379345 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 11 | 0.373229 | `azmcp_loadtesting_testrun_update` | ❌ |
| 12 | 0.369895 | `azmcp_workbooks_show` | ❌ |
| 13 | 0.365569 | `azmcp_workbooks_list` | ❌ |
| 14 | 0.360732 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 15 | 0.346372 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 16 | 0.341360 | `azmcp_quota_region_availability_list` | ❌ |
| 17 | 0.329344 | `azmcp_sql_db_show` | ❌ |
| 18 | 0.328339 | `azmcp_monitor_metrics_query` | ❌ |
| 19 | 0.322886 | `azmcp_quota_usage_check` | ❌ |
| 20 | 0.298792 | `azmcp_monitor_workspace_log_query` | ❌ |

---

## Test 98

**Expected Tool:** `azmcp_loadtesting_testresource_create`  
**Prompt:** Create a load test resource <load-test-resource-name> in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.717577 | `azmcp_loadtesting_testresource_create` | ✅ **EXPECTED** |
| 2 | 0.596685 | `azmcp_loadtesting_testresource_list` | ❌ |
| 3 | 0.514343 | `azmcp_loadtesting_test_create` | ❌ |
| 4 | 0.476575 | `azmcp_loadtesting_testrun_create` | ❌ |
| 5 | 0.447320 | `azmcp_loadtesting_test_get` | ❌ |
| 6 | 0.442110 | `azmcp_workbooks_create` | ❌ |
| 7 | 0.416690 | `azmcp_group_list` | ❌ |
| 8 | 0.394862 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 9 | 0.382706 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 10 | 0.376820 | `azmcp_storage_account_create` | ❌ |
| 11 | 0.375749 | `azmcp_loadtesting_testrun_get` | ❌ |
| 12 | 0.369260 | `azmcp_workbooks_list` | ❌ |
| 13 | 0.350810 | `azmcp_loadtesting_testrun_update` | ❌ |
| 14 | 0.342068 | `azmcp_redis_cluster_list` | ❌ |
| 15 | 0.341080 | `azmcp_grafana_list` | ❌ |
| 16 | 0.335547 | `azmcp_redis_cache_list` | ❌ |
| 17 | 0.328634 | `azmcp_monitor_resource_log_query` | ❌ |
| 18 | 0.326427 | `azmcp_quota_region_availability_list` | ❌ |
| 19 | 0.306391 | `azmcp_quota_usage_check` | ❌ |
| 20 | 0.298217 | `azmcp_monitor_workspace_list` | ❌ |

---

## Test 99

**Expected Tool:** `azmcp_loadtesting_testresource_list`  
**Prompt:** List all load testing resources in the resource group <resource-group> in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.738027 | `azmcp_loadtesting_testresource_list` | ✅ **EXPECTED** |
| 2 | 0.591851 | `azmcp_loadtesting_testresource_create` | ❌ |
| 3 | 0.577408 | `azmcp_group_list` | ❌ |
| 4 | 0.565565 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 5 | 0.561516 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 6 | 0.526662 | `azmcp_workbooks_list` | ❌ |
| 7 | 0.515624 | `azmcp_redis_cluster_list` | ❌ |
| 8 | 0.512935 | `azmcp_loadtesting_test_get` | ❌ |
| 9 | 0.511607 | `azmcp_redis_cache_list` | ❌ |
| 10 | 0.488178 | `azmcp_loadtesting_testrun_list` | ❌ |
| 11 | 0.487330 | `azmcp_grafana_list` | ❌ |
| 12 | 0.470899 | `azmcp_acr_registry_list` | ❌ |
| 13 | 0.467689 | `azmcp_loadtesting_testrun_get` | ❌ |
| 14 | 0.458800 | `azmcp_acr_registry_repository_list` | ❌ |
| 15 | 0.454667 | `azmcp_search_service_list` | ❌ |
| 16 | 0.452214 | `azmcp_monitor_workspace_list` | ❌ |
| 17 | 0.447138 | `azmcp_quota_region_availability_list` | ❌ |
| 18 | 0.437348 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 19 | 0.426880 | `azmcp_sql_db_list` | ❌ |
| 20 | 0.411694 | `azmcp_sql_elastic-pool_list` | ❌ |

---

## Test 100

**Expected Tool:** `azmcp_loadtesting_testrun_create`  
**Prompt:** Create a test run using the id <testrun-id> for test <test-id> in the load testing resource <load-testing-resource> in resource group <resource-group>. Use the name of test run <display-name> and description as <description>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.621803 | `azmcp_loadtesting_testrun_create` | ✅ **EXPECTED** |
| 2 | 0.592805 | `azmcp_loadtesting_testresource_create` | ❌ |
| 3 | 0.540789 | `azmcp_loadtesting_test_create` | ❌ |
| 4 | 0.530882 | `azmcp_loadtesting_testrun_update` | ❌ |
| 5 | 0.489907 | `azmcp_loadtesting_testrun_get` | ❌ |
| 6 | 0.472404 | `azmcp_loadtesting_test_get` | ❌ |
| 7 | 0.413872 | `azmcp_loadtesting_testrun_list` | ❌ |
| 8 | 0.411627 | `azmcp_loadtesting_testresource_list` | ❌ |
| 9 | 0.402120 | `azmcp_workbooks_create` | ❌ |
| 10 | 0.354629 | `azmcp_storage_account_create` | ❌ |
| 11 | 0.331019 | `azmcp_keyvault_key_create` | ❌ |
| 12 | 0.325430 | `azmcp_keyvault_secret_create` | ❌ |
| 13 | 0.314636 | `azmcp_storage_datalake_directory_create` | ❌ |
| 14 | 0.309076 | `azmcp_monitor_resource_log_query` | ❌ |
| 15 | 0.272151 | `azmcp_sql_db_show` | ❌ |
| 16 | 0.267551 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 17 | 0.260678 | `azmcp_storage_queue_message_send` | ❌ |
| 18 | 0.256035 | `azmcp_monitor_metrics_query` | ❌ |
| 19 | 0.250958 | `azmcp_monitor_workspace_log_query` | ❌ |
| 20 | 0.249568 | `azmcp_workbooks_show` | ❌ |

---

## Test 101

**Expected Tool:** `azmcp_loadtesting_testrun_get`  
**Prompt:** Get the load test run with id <testrun-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.626557 | `azmcp_loadtesting_test_get` | ❌ |
| 2 | 0.602806 | `azmcp_loadtesting_testresource_list` | ❌ |
| 3 | 0.572496 | `azmcp_loadtesting_testrun_get` | ✅ **EXPECTED** |
| 4 | 0.561726 | `azmcp_loadtesting_testresource_create` | ❌ |
| 5 | 0.534984 | `azmcp_loadtesting_testrun_create` | ❌ |
| 6 | 0.499290 | `azmcp_loadtesting_testrun_list` | ❌ |
| 7 | 0.433870 | `azmcp_loadtesting_test_create` | ❌ |
| 8 | 0.415178 | `azmcp_loadtesting_testrun_update` | ❌ |
| 9 | 0.397683 | `azmcp_group_list` | ❌ |
| 10 | 0.397591 | `azmcp_monitor_resource_log_query` | ❌ |
| 11 | 0.370479 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 12 | 0.366376 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 13 | 0.356117 | `azmcp_workbooks_list` | ❌ |
| 14 | 0.352407 | `azmcp_workbooks_show` | ❌ |
| 15 | 0.347046 | `azmcp_quota_region_availability_list` | ❌ |
| 16 | 0.330318 | `azmcp_monitor_metrics_query` | ❌ |
| 17 | 0.329118 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 18 | 0.328796 | `azmcp_sql_db_show` | ❌ |
| 19 | 0.315653 | `azmcp_quota_usage_check` | ❌ |
| 20 | 0.293592 | `azmcp_monitor_workspace_log_query` | ❌ |

---

## Test 102

**Expected Tool:** `azmcp_loadtesting_testrun_list`  
**Prompt:** Get all the load test runs for the test with id <test-id> in the load test resource <test-resource> in resource group <resource-group>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.615977 | `azmcp_loadtesting_testresource_list` | ❌ |
| 2 | 0.607935 | `azmcp_loadtesting_test_get` | ❌ |
| 3 | 0.573158 | `azmcp_loadtesting_testrun_get` | ❌ |
| 4 | 0.568920 | `azmcp_loadtesting_testrun_list` | ✅ **EXPECTED** |
| 5 | 0.535207 | `azmcp_loadtesting_testresource_create` | ❌ |
| 6 | 0.492700 | `azmcp_loadtesting_testrun_create` | ❌ |
| 7 | 0.432149 | `azmcp_group_list` | ❌ |
| 8 | 0.418023 | `azmcp_monitor_resource_log_query` | ❌ |
| 9 | 0.410933 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 10 | 0.406508 | `azmcp_loadtesting_test_create` | ❌ |
| 11 | 0.395915 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 12 | 0.392066 | `azmcp_loadtesting_testrun_update` | ❌ |
| 13 | 0.391147 | `azmcp_workbooks_list` | ❌ |
| 14 | 0.375701 | `azmcp_monitor_metrics_query` | ❌ |
| 15 | 0.356833 | `azmcp_quota_region_availability_list` | ❌ |
| 16 | 0.341385 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 17 | 0.340477 | `azmcp_workbooks_show` | ❌ |
| 18 | 0.329464 | `azmcp_sql_db_list` | ❌ |
| 19 | 0.328011 | `azmcp_redis_cluster_list` | ❌ |
| 20 | 0.326457 | `azmcp_sql_elastic-pool_list` | ❌ |

---

## Test 103

**Expected Tool:** `azmcp_loadtesting_testrun_update`  
**Prompt:** Update a test run display name as <display-name> for the id <testrun-id> for test <test-id> in the load testing resource <load-testing-resource> in resource group <resource-group>.  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.659812 | `azmcp_loadtesting_testrun_update` | ✅ **EXPECTED** |
| 2 | 0.509199 | `azmcp_loadtesting_testrun_create` | ❌ |
| 3 | 0.455629 | `azmcp_loadtesting_testrun_get` | ❌ |
| 4 | 0.446611 | `azmcp_loadtesting_test_get` | ❌ |
| 5 | 0.422036 | `azmcp_loadtesting_testresource_create` | ❌ |
| 6 | 0.399536 | `azmcp_loadtesting_test_create` | ❌ |
| 7 | 0.384654 | `azmcp_loadtesting_testresource_list` | ❌ |
| 8 | 0.383635 | `azmcp_loadtesting_testrun_list` | ❌ |
| 9 | 0.320124 | `azmcp_workbooks_update` | ❌ |
| 10 | 0.300023 | `azmcp_workbooks_create` | ❌ |
| 11 | 0.268086 | `azmcp_workbooks_show` | ❌ |
| 12 | 0.267137 | `azmcp_appconfig_kv_set` | ❌ |
| 13 | 0.259606 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 14 | 0.256023 | `azmcp_appconfig_kv_unlock` | ❌ |
| 15 | 0.255408 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 16 | 0.251946 | `azmcp_monitor_resource_log_query` | ❌ |
| 17 | 0.237372 | `azmcp_workbooks_delete` | ❌ |
| 18 | 0.233701 | `azmcp_monitor_metrics_query` | ❌ |
| 19 | 0.232572 | `azmcp_sql_db_show` | ❌ |
| 20 | 0.227194 | `azmcp_servicebus_topic_subscription_details` | ❌ |

---

## Test 104

**Expected Tool:** `azmcp_grafana_list`  
**Prompt:** List all Azure Managed Grafana in one subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.578892 | `azmcp_grafana_list` | ✅ **EXPECTED** |
| 2 | 0.544665 | `azmcp_search_service_list` | ❌ |
| 3 | 0.513083 | `azmcp_monitor_workspace_list` | ❌ |
| 4 | 0.505836 | `azmcp_kusto_cluster_list` | ❌ |
| 5 | 0.498077 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 6 | 0.497110 | `azmcp_functionapp_list` | ❌ |
| 7 | 0.493645 | `azmcp_redis_cluster_list` | ❌ |
| 8 | 0.492724 | `azmcp_postgres_server_list` | ❌ |
| 9 | 0.492210 | `azmcp_subscription_list` | ❌ |
| 10 | 0.490597 | `azmcp_aks_cluster_list` | ❌ |
| 11 | 0.489846 | `azmcp_cosmos_account_list` | ❌ |
| 12 | 0.482789 | `azmcp_redis_cache_list` | ❌ |
| 13 | 0.479611 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 14 | 0.452683 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 15 | 0.441315 | `azmcp_group_list` | ❌ |
| 16 | 0.440392 | `azmcp_kusto_database_list` | ❌ |
| 17 | 0.438192 | `azmcp_storage_account_list` | ❌ |
| 18 | 0.431917 | `azmcp_storage_table_list` | ❌ |
| 19 | 0.422236 | `azmcp_acr_registry_list` | ❌ |
| 20 | 0.417927 | `azmcp_appconfig_account_list` | ❌ |

---

## Test 105

**Expected Tool:** `azmcp_marketplace_product_get`  
**Prompt:** Get details about marketplace product <product_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.528164 | `azmcp_marketplace_product_get` | ✅ **EXPECTED** |
| 2 | 0.353256 | `azmcp_servicebus_topic_subscription_details` | ❌ |
| 3 | 0.330935 | `azmcp_servicebus_queue_details` | ❌ |
| 4 | 0.323704 | `azmcp_servicebus_topic_details` | ❌ |
| 5 | 0.322443 | `azmcp_loadtesting_testrun_get` | ❌ |
| 6 | 0.302335 | `azmcp_aks_cluster_get` | ❌ |
| 7 | 0.295818 | `azmcp_storage_blob_container_details` | ❌ |
| 8 | 0.289430 | `azmcp_workbooks_show` | ❌ |
| 9 | 0.281400 | `azmcp_storage_account_details` | ❌ |
| 10 | 0.276826 | `azmcp_kusto_cluster_get` | ❌ |
| 11 | 0.274403 | `azmcp_redis_cache_list` | ❌ |
| 12 | 0.269243 | `azmcp_sql_db_show` | ❌ |
| 13 | 0.266271 | `azmcp_foundry_models_list` | ❌ |
| 14 | 0.264527 | `azmcp_search_index_describe` | ❌ |
| 15 | 0.252041 | `azmcp_loadtesting_test_get` | ❌ |
| 16 | 0.248779 | `azmcp_grafana_list` | ❌ |
| 17 | 0.246259 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 18 | 0.245820 | `azmcp_appconfig_kv_show` | ❌ |
| 19 | 0.235780 | `azmcp_loadtesting_testrun_list` | ❌ |
| 20 | 0.225581 | `azmcp_keyvault_certificate_get` | ❌ |

---

## Test 106

**Expected Tool:** `azmcp_bestpractices_get`  
**Prompt:** Get the latest Azure code generation best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.649047 | `azmcp_bestpractices_get` | ✅ **EXPECTED** |
| 2 | 0.612446 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 3 | 0.586907 | `azmcp_deploy_iac_rules_get` | ❌ |
| 4 | 0.531727 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 5 | 0.490235 | `azmcp_deploy_plan_get` | ❌ |
| 6 | 0.447777 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 7 | 0.435166 | `azmcp_extension_az` | ❌ |
| 8 | 0.372867 | `azmcp_extension_azd` | ❌ |
| 9 | 0.353355 | `azmcp_deploy_app_logs_get` | ❌ |
| 10 | 0.351664 | `azmcp_quota_usage_check` | ❌ |
| 11 | 0.345046 | `azmcp_bicepschema_get` | ❌ |
| 12 | 0.321323 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 13 | 0.312391 | `azmcp_quota_region_availability_list` | ❌ |
| 14 | 0.289967 | `azmcp_storage_account_details` | ❌ |
| 15 | 0.259833 | `azmcp_subscription_list` | ❌ |
| 16 | 0.258775 | `azmcp_search_service_list` | ❌ |
| 17 | 0.258646 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 18 | 0.253042 | `azmcp_storage_blob_upload` | ❌ |
| 19 | 0.251882 | `azmcp_storage_queue_message_send` | ❌ |
| 20 | 0.251118 | `azmcp_storage_blob_details` | ❌ |

---

## Test 107

**Expected Tool:** `azmcp_bestpractices_get`  
**Prompt:** Get the latest Azure deployment best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633068 | `azmcp_bestpractices_get` | ✅ **EXPECTED** |
| 2 | 0.543356 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 3 | 0.541091 | `azmcp_deploy_iac_rules_get` | ❌ |
| 4 | 0.516852 | `azmcp_deploy_plan_get` | ❌ |
| 5 | 0.516443 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 6 | 0.452068 | `azmcp_extension_az` | ❌ |
| 7 | 0.424017 | `azmcp_foundry_models_deployments_list` | ❌ |
| 8 | 0.409787 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 9 | 0.392171 | `azmcp_deploy_app_logs_get` | ❌ |
| 10 | 0.366111 | `azmcp_extension_azd` | ❌ |
| 11 | 0.358593 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 12 | 0.342487 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.306627 | `azmcp_quota_region_availability_list` | ❌ |
| 14 | 0.304620 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 15 | 0.292995 | `azmcp_storage_account_details` | ❌ |
| 16 | 0.279585 | `azmcp_subscription_list` | ❌ |
| 17 | 0.277791 | `azmcp_search_service_list` | ❌ |
| 18 | 0.267567 | `azmcp_storage_blob_details` | ❌ |
| 19 | 0.259012 | `azmcp_monitor_metrics_query` | ❌ |
| 20 | 0.257356 | `azmcp_workbooks_delete` | ❌ |

---

## Test 108

**Expected Tool:** `azmcp_bestpractices_get`  
**Prompt:** Get the latest Azure best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.671381 | `azmcp_bestpractices_get` | ✅ **EXPECTED** |
| 2 | 0.575535 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 3 | 0.518643 | `azmcp_deploy_iac_rules_get` | ❌ |
| 4 | 0.465572 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 5 | 0.455995 | `azmcp_extension_az` | ❌ |
| 6 | 0.430630 | `azmcp_deploy_plan_get` | ❌ |
| 7 | 0.399433 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 8 | 0.384057 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 9 | 0.380286 | `azmcp_deploy_app_logs_get` | ❌ |
| 10 | 0.375863 | `azmcp_quota_usage_check` | ❌ |
| 11 | 0.362465 | `azmcp_extension_azd` | ❌ |
| 12 | 0.329342 | `azmcp_quota_region_availability_list` | ❌ |
| 13 | 0.329314 | `azmcp_storage_account_details` | ❌ |
| 14 | 0.319861 | `azmcp_bicepschema_get` | ❌ |
| 15 | 0.316805 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 16 | 0.301857 | `azmcp_subscription_list` | ❌ |
| 17 | 0.293300 | `azmcp_storage_blob_details` | ❌ |
| 18 | 0.290182 | `azmcp_monitor_metrics_query` | ❌ |
| 19 | 0.287118 | `azmcp_search_service_list` | ❌ |
| 20 | 0.275399 | `azmcp_workbooks_delete` | ❌ |

---

## Test 109

**Expected Tool:** `azmcp_bestpractices_get`  
**Prompt:** Get the latest Azure Functions code generation best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.576108 | `azmcp_bestpractices_get` | ✅ **EXPECTED** |
| 2 | 0.553932 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 3 | 0.522998 | `azmcp_deploy_iac_rules_get` | ❌ |
| 4 | 0.493998 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 5 | 0.445382 | `azmcp_deploy_plan_get` | ❌ |
| 6 | 0.416803 | `azmcp_extension_az` | ❌ |
| 7 | 0.400447 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 8 | 0.393373 | `azmcp_functionapp_list` | ❌ |
| 9 | 0.368157 | `azmcp_deploy_app_logs_get` | ❌ |
| 10 | 0.348603 | `azmcp_extension_azd` | ❌ |
| 11 | 0.317494 | `azmcp_quota_usage_check` | ❌ |
| 12 | 0.278941 | `azmcp_quota_region_availability_list` | ❌ |
| 13 | 0.269946 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 14 | 0.253379 | `azmcp_storage_blob_upload` | ❌ |
| 15 | 0.241692 | `azmcp_storage_blob_details` | ❌ |
| 16 | 0.240062 | `azmcp_storage_queue_message_send` | ❌ |
| 17 | 0.238484 | `azmcp_storage_account_details` | ❌ |
| 18 | 0.219838 | `azmcp_storage_blob_container_create` | ❌ |
| 19 | 0.219298 | `azmcp_subscription_list` | ❌ |
| 20 | 0.212761 | `azmcp_search_service_list` | ❌ |

---

## Test 110

**Expected Tool:** `azmcp_bestpractices_get`  
**Prompt:** Get the latest Azure Functions deployment best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.553170 | `azmcp_bestpractices_get` | ✅ **EXPECTED** |
| 2 | 0.497350 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 3 | 0.495659 | `azmcp_deploy_iac_rules_get` | ❌ |
| 4 | 0.487769 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 5 | 0.474511 | `azmcp_deploy_plan_get` | ❌ |
| 6 | 0.439182 | `azmcp_foundry_models_deployments_list` | ❌ |
| 7 | 0.431008 | `azmcp_extension_az` | ❌ |
| 8 | 0.423965 | `azmcp_functionapp_list` | ❌ |
| 9 | 0.412001 | `azmcp_deploy_app_logs_get` | ❌ |
| 10 | 0.377790 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 11 | 0.321678 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 12 | 0.317931 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.277946 | `azmcp_quota_region_availability_list` | ❌ |
| 14 | 0.265176 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 15 | 0.263968 | `azmcp_storage_blob_upload` | ❌ |
| 16 | 0.256807 | `azmcp_storage_queue_message_send` | ❌ |
| 17 | 0.254398 | `azmcp_storage_blob_details` | ❌ |
| 18 | 0.246787 | `azmcp_storage_account_details` | ❌ |
| 19 | 0.244786 | `azmcp_search_service_list` | ❌ |
| 20 | 0.241860 | `azmcp_subscription_list` | ❌ |

---

## Test 111

**Expected Tool:** `azmcp_bestpractices_get`  
**Prompt:** Get the latest Azure Functions best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.586538 | `azmcp_bestpractices_get` | ✅ **EXPECTED** |
| 2 | 0.521120 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 3 | 0.487322 | `azmcp_deploy_iac_rules_get` | ❌ |
| 4 | 0.458060 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 5 | 0.444295 | `azmcp_extension_az` | ❌ |
| 6 | 0.433103 | `azmcp_functionapp_list` | ❌ |
| 7 | 0.395940 | `azmcp_deploy_app_logs_get` | ❌ |
| 8 | 0.394214 | `azmcp_deploy_plan_get` | ❌ |
| 9 | 0.363596 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 10 | 0.348542 | `azmcp_extension_azd` | ❌ |
| 11 | 0.332015 | `azmcp_quota_usage_check` | ❌ |
| 12 | 0.328838 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 13 | 0.284215 | `azmcp_quota_region_availability_list` | ❌ |
| 14 | 0.274389 | `azmcp_storage_queue_message_send` | ❌ |
| 15 | 0.269853 | `azmcp_storage_blob_details` | ❌ |
| 16 | 0.267667 | `azmcp_storage_blob_upload` | ❌ |
| 17 | 0.263108 | `azmcp_storage_account_details` | ❌ |
| 18 | 0.261619 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 19 | 0.261593 | `azmcp_monitor_metrics_query` | ❌ |
| 20 | 0.247460 | `azmcp_subscription_list` | ❌ |

---

## Test 112

**Expected Tool:** `azmcp_bestpractices_get`  
**Prompt:** Get the latest Azure Static Web Apps best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.577758 | `azmcp_bestpractices_get` | ✅ **EXPECTED** |
| 2 | 0.526390 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 3 | 0.505123 | `azmcp_deploy_iac_rules_get` | ❌ |
| 4 | 0.483705 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 5 | 0.405993 | `azmcp_extension_az` | ❌ |
| 6 | 0.405143 | `azmcp_deploy_app_logs_get` | ❌ |
| 7 | 0.401209 | `azmcp_deploy_plan_get` | ❌ |
| 8 | 0.398226 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 9 | 0.355985 | `azmcp_extension_azd` | ❌ |
| 10 | 0.317187 | `azmcp_functionapp_list` | ❌ |
| 11 | 0.312174 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 12 | 0.283198 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.263368 | `azmcp_storage_account_details` | ❌ |
| 14 | 0.259417 | `azmcp_storage_blob_upload` | ❌ |
| 15 | 0.256951 | `azmcp_storage_blob_details` | ❌ |
| 16 | 0.249439 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 17 | 0.240282 | `azmcp_storage_account_create` | ❌ |
| 18 | 0.237289 | `azmcp_quota_region_availability_list` | ❌ |
| 19 | 0.223337 | `azmcp_search_service_list` | ❌ |
| 20 | 0.221706 | `azmcp_storage_blob_container_create` | ❌ |

---

## Test 113

**Expected Tool:** `azmcp_bestpractices_get`  
**Prompt:** What are azure function best practices?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.553494 | `azmcp_bestpractices_get` | ✅ **EXPECTED** |
| 2 | 0.487728 | `azmcp_extension_az` | ❌ |
| 3 | 0.478550 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 4 | 0.472112 | `azmcp_deploy_iac_rules_get` | ❌ |
| 5 | 0.433134 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 6 | 0.394092 | `azmcp_functionapp_list` | ❌ |
| 7 | 0.368831 | `azmcp_deploy_plan_get` | ❌ |
| 8 | 0.358703 | `azmcp_deploy_app_logs_get` | ❌ |
| 9 | 0.337024 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 10 | 0.323967 | `azmcp_extension_azd` | ❌ |
| 11 | 0.293848 | `azmcp_quota_usage_check` | ❌ |
| 12 | 0.280178 | `azmcp_storage_queue_message_send` | ❌ |
| 13 | 0.261465 | `azmcp_storage_blob_upload` | ❌ |
| 14 | 0.249260 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 15 | 0.248119 | `azmcp_monitor_resource_log_query` | ❌ |
| 16 | 0.248003 | `azmcp_workbooks_delete` | ❌ |
| 17 | 0.243935 | `azmcp_storage_blob_details` | ❌ |
| 18 | 0.223124 | `azmcp_storage_account_details` | ❌ |
| 19 | 0.222800 | `azmcp_monitor_metrics_query` | ❌ |
| 20 | 0.216440 | `azmcp_storage_account_create` | ❌ |

---

## Test 114

**Expected Tool:** `azmcp_bestpractices_get`  
**Prompt:** Create the plan for creating a simple HTTP-triggered function app in javascript that returns a random compliment from a predefined list in a JSON response. And deploy it to azure eventually. But don't create any code until I confirm.  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.429170 | `azmcp_deploy_plan_get` | ❌ |
| 2 | 0.408233 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 3 | 0.406619 | `azmcp_extension_az` | ❌ |
| 4 | 0.360756 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 5 | 0.352369 | `azmcp_deploy_iac_rules_get` | ❌ |
| 6 | 0.345059 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 7 | 0.336439 | `azmcp_bestpractices_get` | ✅ **EXPECTED** |
| 8 | 0.319970 | `azmcp_loadtesting_test_create` | ❌ |
| 9 | 0.299148 | `azmcp_deploy_app_logs_get` | ❌ |
| 10 | 0.289885 | `azmcp_functionapp_list` | ❌ |
| 11 | 0.232320 | `azmcp_quota_usage_check` | ❌ |
| 12 | 0.218912 | `azmcp_workbooks_create` | ❌ |
| 13 | 0.217972 | `azmcp_storage_blob_upload` | ❌ |
| 14 | 0.213599 | `azmcp_storage_blob_details` | ❌ |
| 15 | 0.210908 | `azmcp_quota_region_availability_list` | ❌ |
| 16 | 0.201280 | `azmcp_storage_blob_container_create` | ❌ |
| 17 | 0.200212 | `azmcp_storage_account_create` | ❌ |
| 18 | 0.190533 | `azmcp_storage_queue_message_send` | ❌ |
| 19 | 0.190147 | `azmcp_storage_account_details` | ❌ |
| 20 | 0.174633 | `azmcp_subscription_list` | ❌ |

---

## Test 115

**Expected Tool:** `azmcp_bestpractices_get`  
**Prompt:** Create the plan for creating a to-do list app. And deploy it to azure as a container app. But don't create any code until I confirm.  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.497276 | `azmcp_deploy_plan_get` | ❌ |
| 2 | 0.493182 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 3 | 0.408474 | `azmcp_extension_az` | ❌ |
| 4 | 0.405146 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 5 | 0.395623 | `azmcp_deploy_iac_rules_get` | ❌ |
| 6 | 0.367259 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 7 | 0.348171 | `azmcp_deploy_app_logs_get` | ❌ |
| 8 | 0.333124 | `azmcp_bestpractices_get` | ✅ **EXPECTED** |
| 9 | 0.304256 | `azmcp_extension_azd` | ❌ |
| 10 | 0.300092 | `azmcp_loadtesting_test_create` | ❌ |
| 11 | 0.243575 | `azmcp_quota_usage_check` | ❌ |
| 12 | 0.230519 | `azmcp_storage_blob_container_create` | ❌ |
| 13 | 0.228431 | `azmcp_storage_blob_container_list` | ❌ |
| 14 | 0.226128 | `azmcp_storage_account_create` | ❌ |
| 15 | 0.218621 | `azmcp_quota_region_availability_list` | ❌ |
| 16 | 0.218293 | `azmcp_storage_blob_list` | ❌ |
| 17 | 0.209213 | `azmcp_workbooks_create` | ❌ |
| 18 | 0.207259 | `azmcp_storage_table_list` | ❌ |
| 19 | 0.195211 | `azmcp_storage_blob_details` | ❌ |
| 20 | 0.191395 | `azmcp_storage_blob_upload` | ❌ |

---

## Test 116

**Expected Tool:** `azmcp_monitor_healthmodels_entity_gethealth`  
**Prompt:** Show me the health status of entity <entity_id> in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.477061 | `azmcp_monitor_healthmodels_entity_gethealth` | ✅ **EXPECTED** |
| 2 | 0.472216 | `azmcp_monitor_workspace_list` | ❌ |
| 3 | 0.468304 | `azmcp_monitor_table_list` | ❌ |
| 4 | 0.464057 | `azmcp_monitor_workspace_log_query` | ❌ |
| 5 | 0.459634 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 6 | 0.436999 | `azmcp_deploy_app_logs_get` | ❌ |
| 7 | 0.418652 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 8 | 0.413424 | `azmcp_monitor_table_type_list` | ❌ |
| 9 | 0.404147 | `azmcp_monitor_resource_log_query` | ❌ |
| 10 | 0.380105 | `azmcp_grafana_list` | ❌ |
| 11 | 0.358400 | `azmcp_monitor_metrics_query` | ❌ |
| 12 | 0.339260 | `azmcp_aks_cluster_get` | ❌ |
| 13 | 0.337555 | `azmcp_loadtesting_testrun_get` | ❌ |
| 14 | 0.316622 | `azmcp_workbooks_show` | ❌ |
| 15 | 0.314648 | `azmcp_quota_usage_check` | ❌ |
| 16 | 0.305729 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 17 | 0.297730 | `azmcp_aks_cluster_list` | ❌ |
| 18 | 0.279275 | `azmcp_kusto_query` | ❌ |
| 19 | 0.276666 | `azmcp_loadtesting_test_get` | ❌ |
| 20 | 0.269772 | `azmcp_kusto_cluster_get` | ❌ |

---

## Test 117

**Expected Tool:** `azmcp_monitor_metrics_definitions`  
**Prompt:** Get metric definitions for <resource_type> <resource_name> from the namespace  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.592640 | `azmcp_monitor_metrics_definitions` | ✅ **EXPECTED** |
| 2 | 0.424141 | `azmcp_monitor_metrics_query` | ❌ |
| 3 | 0.332356 | `azmcp_monitor_table_type_list` | ❌ |
| 4 | 0.315310 | `azmcp_servicebus_topic_details` | ❌ |
| 5 | 0.311108 | `azmcp_servicebus_topic_subscription_details` | ❌ |
| 6 | 0.305464 | `azmcp_servicebus_queue_details` | ❌ |
| 7 | 0.304735 | `azmcp_grafana_list` | ❌ |
| 8 | 0.303453 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 9 | 0.297379 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 10 | 0.294124 | `azmcp_quota_region_availability_list` | ❌ |
| 11 | 0.293189 | `azmcp_search_index_describe` | ❌ |
| 12 | 0.284519 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 13 | 0.283102 | `azmcp_quota_usage_check` | ❌ |
| 14 | 0.277663 | `azmcp_loadtesting_test_get` | ❌ |
| 15 | 0.277566 | `azmcp_kusto_table_schema` | ❌ |
| 16 | 0.269984 | `azmcp_monitor_healthmodels_entity_gethealth` | ❌ |
| 17 | 0.249144 | `azmcp_aks_cluster_get` | ❌ |
| 18 | 0.248987 | `azmcp_bicepschema_get` | ❌ |
| 19 | 0.234617 | `azmcp_loadtesting_testresource_list` | ❌ |
| 20 | 0.227542 | `azmcp_appconfig_kv_list` | ❌ |

---

## Test 118

**Expected Tool:** `azmcp_monitor_metrics_definitions`  
**Prompt:** Show me all available metrics and their definitions for storage account <account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.611603 | `azmcp_storage_account_details` | ❌ |
| 2 | 0.587736 | `azmcp_monitor_metrics_definitions` | ✅ **EXPECTED** |
| 3 | 0.556726 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.542805 | `azmcp_storage_table_list` | ❌ |
| 5 | 0.541028 | `azmcp_storage_account_list` | ❌ |
| 6 | 0.539767 | `azmcp_storage_blob_container_details` | ❌ |
| 7 | 0.519808 | `azmcp_storage_blob_list` | ❌ |
| 8 | 0.459829 | `azmcp_cosmos_account_list` | ❌ |
| 9 | 0.459179 | `azmcp_storage_blob_details` | ❌ |
| 10 | 0.431109 | `azmcp_appconfig_kv_show` | ❌ |
| 11 | 0.417098 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 12 | 0.414488 | `azmcp_cosmos_database_container_list` | ❌ |
| 13 | 0.406424 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.403921 | `azmcp_quota_usage_check` | ❌ |
| 15 | 0.397526 | `azmcp_appconfig_kv_list` | ❌ |
| 16 | 0.390422 | `azmcp_cosmos_database_list` | ❌ |
| 17 | 0.378213 | `azmcp_keyvault_key_list` | ❌ |
| 18 | 0.359476 | `azmcp_appconfig_account_list` | ❌ |
| 19 | 0.357647 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 20 | 0.343879 | `azmcp_keyvault_secret_list` | ❌ |

---

## Test 119

**Expected Tool:** `azmcp_monitor_metrics_definitions`  
**Prompt:** What metric definitions are available for the Application Insights resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633162 | `azmcp_monitor_metrics_definitions` | ✅ **EXPECTED** |
| 2 | 0.495578 | `azmcp_monitor_metrics_query` | ❌ |
| 3 | 0.380281 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 4 | 0.370862 | `azmcp_monitor_table_type_list` | ❌ |
| 5 | 0.353289 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 6 | 0.344383 | `azmcp_quota_usage_check` | ❌ |
| 7 | 0.338061 | `azmcp_monitor_resource_log_query` | ❌ |
| 8 | 0.329499 | `azmcp_loadtesting_testresource_list` | ❌ |
| 9 | 0.324088 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 10 | 0.308351 | `azmcp_monitor_workspace_log_query` | ❌ |
| 11 | 0.303337 | `azmcp_search_index_list` | ❌ |
| 12 | 0.302842 | `azmcp_monitor_table_list` | ❌ |
| 13 | 0.301984 | `azmcp_workbooks_show` | ❌ |
| 14 | 0.299133 | `azmcp_loadtesting_testrun_get` | ❌ |
| 15 | 0.291325 | `azmcp_deploy_app_logs_get` | ❌ |
| 16 | 0.286295 | `azmcp_loadtesting_testresource_create` | ❌ |
| 17 | 0.286144 | `azmcp_loadtesting_test_get` | ❌ |
| 18 | 0.284418 | `azmcp_grafana_list` | ❌ |
| 19 | 0.279924 | `azmcp_foundry_models_deployments_list` | ❌ |
| 20 | 0.278440 | `azmcp_loadtesting_test_create` | ❌ |

---

## Test 120

**Expected Tool:** `azmcp_monitor_metrics_query`  
**Prompt:** Analyze the performance trends and response times for Application Insights resource <resource_name> over the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.555377 | `azmcp_monitor_metrics_query` | ✅ **EXPECTED** |
| 2 | 0.445153 | `azmcp_monitor_resource_log_query` | ❌ |
| 3 | 0.439684 | `azmcp_loadtesting_testrun_get` | ❌ |
| 4 | 0.417973 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 5 | 0.409107 | `azmcp_deploy_app_logs_get` | ❌ |
| 6 | 0.404582 | `azmcp_monitor_workspace_log_query` | ❌ |
| 7 | 0.388205 | `azmcp_quota_usage_check` | ❌ |
| 8 | 0.380075 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 9 | 0.341791 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 10 | 0.340642 | `azmcp_loadtesting_testrun_list` | ❌ |
| 11 | 0.339771 | `azmcp_loadtesting_testresource_list` | ❌ |
| 12 | 0.335430 | `azmcp_monitor_metrics_definitions` | ❌ |
| 13 | 0.329460 | `azmcp_loadtesting_testresource_create` | ❌ |
| 14 | 0.328475 | `azmcp_loadtesting_test_get` | ❌ |
| 15 | 0.326805 | `azmcp_workbooks_show` | ❌ |
| 16 | 0.326398 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 17 | 0.314675 | `azmcp_extension_azqr` | ❌ |
| 18 | 0.291424 | `azmcp_search_index_list` | ❌ |
| 19 | 0.289449 | `azmcp_workbooks_delete` | ❌ |
| 20 | 0.286251 | `azmcp_storage_blob_details` | ❌ |

---

## Test 121

**Expected Tool:** `azmcp_monitor_metrics_query`  
**Prompt:** Check the availability metrics for my Application Insights resource <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.557830 | `azmcp_monitor_metrics_query` | ✅ **EXPECTED** |
| 2 | 0.504303 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 3 | 0.460611 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 4 | 0.455904 | `azmcp_quota_usage_check` | ❌ |
| 5 | 0.438233 | `azmcp_monitor_metrics_definitions` | ❌ |
| 6 | 0.389662 | `azmcp_monitor_resource_log_query` | ❌ |
| 7 | 0.372998 | `azmcp_deploy_app_logs_get` | ❌ |
| 8 | 0.356326 | `azmcp_monitor_workspace_log_query` | ❌ |
| 9 | 0.341525 | `azmcp_loadtesting_testrun_get` | ❌ |
| 10 | 0.339388 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 11 | 0.326899 | `azmcp_loadtesting_testresource_list` | ❌ |
| 12 | 0.311770 | `azmcp_search_index_list` | ❌ |
| 13 | 0.303909 | `azmcp_quota_region_availability_list` | ❌ |
| 14 | 0.302312 | `azmcp_loadtesting_test_get` | ❌ |
| 15 | 0.295353 | `azmcp_functionapp_list` | ❌ |
| 16 | 0.292483 | `azmcp_search_service_list` | ❌ |
| 17 | 0.288372 | `azmcp_loadtesting_testresource_create` | ❌ |
| 18 | 0.285955 | `azmcp_grafana_list` | ❌ |
| 19 | 0.285950 | `azmcp_monitor_table_type_list` | ❌ |
| 20 | 0.285521 | `azmcp_deploy_architecture_diagram_generate` | ❌ |

---

## Test 122

**Expected Tool:** `azmcp_monitor_metrics_query`  
**Prompt:** Get the <aggregation_type> <metric_name> metric for <resource_type> <resource_name> over the last <time_period> with intervals  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.462006 | `azmcp_monitor_metrics_query` | ✅ **EXPECTED** |
| 2 | 0.390813 | `azmcp_monitor_metrics_definitions` | ❌ |
| 3 | 0.305866 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 4 | 0.300074 | `azmcp_monitor_resource_log_query` | ❌ |
| 5 | 0.297979 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 6 | 0.280274 | `azmcp_monitor_workspace_log_query` | ❌ |
| 7 | 0.274315 | `azmcp_monitor_table_type_list` | ❌ |
| 8 | 0.267023 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 9 | 0.263920 | `azmcp_quota_usage_check` | ❌ |
| 10 | 0.262934 | `azmcp_quota_region_availability_list` | ❌ |
| 11 | 0.259045 | `azmcp_grafana_list` | ❌ |
| 12 | 0.249831 | `azmcp_loadtesting_test_get` | ❌ |
| 13 | 0.249350 | `azmcp_storage_blob_container_details` | ❌ |
| 14 | 0.249012 | `azmcp_monitor_healthmodels_entity_gethealth` | ❌ |
| 15 | 0.248190 | `azmcp_loadtesting_testresource_list` | ❌ |
| 16 | 0.246265 | `azmcp_workbooks_show` | ❌ |
| 17 | 0.244621 | `azmcp_loadtesting_testrun_get` | ❌ |
| 18 | 0.235344 | `azmcp_kusto_table_schema` | ❌ |
| 19 | 0.223724 | `azmcp_loadtesting_testrun_list` | ❌ |
| 20 | 0.213121 | `azmcp_aks_cluster_get` | ❌ |

---

## Test 123

**Expected Tool:** `azmcp_monitor_metrics_query`  
**Prompt:** Investigate error rates and failed requests for Application Insights resource <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.492138 | `azmcp_monitor_metrics_query` | ✅ **EXPECTED** |
| 2 | 0.413826 | `azmcp_monitor_resource_log_query` | ❌ |
| 3 | 0.411016 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 4 | 0.398988 | `azmcp_deploy_app_logs_get` | ❌ |
| 5 | 0.397335 | `azmcp_quota_usage_check` | ❌ |
| 6 | 0.368342 | `azmcp_loadtesting_testrun_get` | ❌ |
| 7 | 0.359340 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 8 | 0.354940 | `azmcp_monitor_workspace_log_query` | ❌ |
| 9 | 0.316302 | `azmcp_loadtesting_testresource_list` | ❌ |
| 10 | 0.308767 | `azmcp_monitor_metrics_definitions` | ❌ |
| 11 | 0.295918 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 12 | 0.293311 | `azmcp_loadtesting_testresource_create` | ❌ |
| 13 | 0.287528 | `azmcp_quota_region_availability_list` | ❌ |
| 14 | 0.287126 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 15 | 0.284577 | `azmcp_functionapp_list` | ❌ |
| 16 | 0.283523 | `azmcp_extension_azqr` | ❌ |
| 17 | 0.280087 | `azmcp_search_index_list` | ❌ |
| 18 | 0.274550 | `azmcp_loadtesting_test_get` | ❌ |
| 19 | 0.272721 | `azmcp_search_service_list` | ❌ |
| 20 | 0.271283 | `azmcp_workbooks_show` | ❌ |

---

## Test 124

**Expected Tool:** `azmcp_monitor_metrics_query`  
**Prompt:** Query the <metric_name> metric for <resource_type> <resource_name> for the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.525492 | `azmcp_monitor_metrics_query` | ✅ **EXPECTED** |
| 2 | 0.384283 | `azmcp_monitor_metrics_definitions` | ❌ |
| 3 | 0.373862 | `azmcp_monitor_resource_log_query` | ❌ |
| 4 | 0.362064 | `azmcp_monitor_workspace_log_query` | ❌ |
| 5 | 0.299323 | `azmcp_quota_usage_check` | ❌ |
| 6 | 0.293023 | `azmcp_loadtesting_testrun_get` | ❌ |
| 7 | 0.287485 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 8 | 0.280961 | `azmcp_search_index_query` | ❌ |
| 9 | 0.272319 | `azmcp_monitor_table_type_list` | ❌ |
| 10 | 0.266709 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 11 | 0.264059 | `azmcp_monitor_healthmodels_entity_gethealth` | ❌ |
| 12 | 0.262512 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 13 | 0.261916 | `azmcp_grafana_list` | ❌ |
| 14 | 0.256828 | `azmcp_loadtesting_testrun_list` | ❌ |
| 15 | 0.252351 | `azmcp_servicebus_queue_details` | ❌ |
| 16 | 0.250676 | `azmcp_postgres_server_param_get` | ❌ |
| 17 | 0.246091 | `azmcp_loadtesting_test_get` | ❌ |
| 18 | 0.244224 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 19 | 0.239123 | `azmcp_kusto_query` | ❌ |
| 20 | 0.235618 | `azmcp_loadtesting_testresource_list` | ❌ |

---

## Test 125

**Expected Tool:** `azmcp_monitor_metrics_query`  
**Prompt:** What's the request per second rate for my Application Insights resource <resource_name> over the last <time_period>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.480140 | `azmcp_monitor_metrics_query` | ✅ **EXPECTED** |
| 2 | 0.378128 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 3 | 0.363412 | `azmcp_quota_usage_check` | ❌ |
| 4 | 0.348732 | `azmcp_monitor_resource_log_query` | ❌ |
| 5 | 0.341334 | `azmcp_monitor_workspace_log_query` | ❌ |
| 6 | 0.331215 | `azmcp_loadtesting_testresource_list` | ❌ |
| 7 | 0.330074 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 8 | 0.328838 | `azmcp_monitor_metrics_definitions` | ❌ |
| 9 | 0.327098 | `azmcp_loadtesting_testrun_get` | ❌ |
| 10 | 0.319343 | `azmcp_loadtesting_testresource_create` | ❌ |
| 11 | 0.292195 | `azmcp_deploy_app_logs_get` | ❌ |
| 12 | 0.278452 | `azmcp_workbooks_show` | ❌ |
| 13 | 0.277129 | `azmcp_loadtesting_test_get` | ❌ |
| 14 | 0.272266 | `azmcp_functionapp_list` | ❌ |
| 15 | 0.266918 | `azmcp_search_index_list` | ❌ |
| 16 | 0.262764 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 17 | 0.260709 | `azmcp_foundry_models_deployments_list` | ❌ |
| 18 | 0.258891 | `azmcp_extension_azqr` | ❌ |
| 19 | 0.254630 | `azmcp_search_service_list` | ❌ |
| 20 | 0.246652 | `azmcp_storage_queue_message_send` | ❌ |

---

## Test 126

**Expected Tool:** `azmcp_monitor_resource_log_query`  
**Prompt:** Show me the logs for the past hour for the resource <resource_name> in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.585055 | `azmcp_monitor_workspace_log_query` | ❌ |
| 2 | 0.577987 | `azmcp_monitor_resource_log_query` | ✅ **EXPECTED** |
| 3 | 0.472373 | `azmcp_deploy_app_logs_get` | ❌ |
| 4 | 0.469889 | `azmcp_monitor_metrics_query` | ❌ |
| 5 | 0.443837 | `azmcp_monitor_workspace_list` | ❌ |
| 6 | 0.443034 | `azmcp_monitor_table_list` | ❌ |
| 7 | 0.392551 | `azmcp_monitor_table_type_list` | ❌ |
| 8 | 0.390323 | `azmcp_grafana_list` | ❌ |
| 9 | 0.361373 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 10 | 0.359288 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 11 | 0.352924 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 12 | 0.345267 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.333774 | `azmcp_workbooks_list` | ❌ |
| 14 | 0.320831 | `azmcp_loadtesting_testrun_get` | ❌ |
| 15 | 0.308156 | `azmcp_aks_cluster_get` | ❌ |
| 16 | 0.303050 | `azmcp_loadtesting_testresource_list` | ❌ |
| 17 | 0.300342 | `azmcp_loadtesting_test_get` | ❌ |
| 18 | 0.297225 | `azmcp_aks_cluster_list` | ❌ |
| 19 | 0.296543 | `azmcp_loadtesting_testrun_list` | ❌ |
| 20 | 0.291773 | `azmcp_kusto_query` | ❌ |

---

## Test 127

**Expected Tool:** `azmcp_monitor_table_list`  
**Prompt:** List all tables in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.851075 | `azmcp_monitor_table_list` | ✅ **EXPECTED** |
| 2 | 0.725738 | `azmcp_monitor_table_type_list` | ❌ |
| 3 | 0.620384 | `azmcp_monitor_workspace_list` | ❌ |
| 4 | 0.586691 | `azmcp_storage_table_list` | ❌ |
| 5 | 0.511123 | `azmcp_kusto_table_list` | ❌ |
| 6 | 0.502075 | `azmcp_grafana_list` | ❌ |
| 7 | 0.488557 | `azmcp_postgres_table_list` | ❌ |
| 8 | 0.436216 | `azmcp_monitor_workspace_log_query` | ❌ |
| 9 | 0.420394 | `azmcp_cosmos_database_list` | ❌ |
| 10 | 0.419859 | `azmcp_kusto_database_list` | ❌ |
| 11 | 0.409449 | `azmcp_monitor_resource_log_query` | ❌ |
| 12 | 0.405953 | `azmcp_search_index_list` | ❌ |
| 13 | 0.400092 | `azmcp_workbooks_list` | ❌ |
| 14 | 0.397408 | `azmcp_kusto_table_schema` | ❌ |
| 15 | 0.378748 | `azmcp_sql_db_list` | ❌ |
| 16 | 0.375176 | `azmcp_deploy_app_logs_get` | ❌ |
| 17 | 0.374930 | `azmcp_cosmos_database_container_list` | ❌ |
| 18 | 0.366099 | `azmcp_kusto_sample` | ❌ |
| 19 | 0.365781 | `azmcp_cosmos_account_list` | ❌ |
| 20 | 0.365538 | `azmcp_kusto_cluster_list` | ❌ |

---

## Test 128

**Expected Tool:** `azmcp_monitor_table_list`  
**Prompt:** Show me the tables in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.798460 | `azmcp_monitor_table_list` | ✅ **EXPECTED** |
| 2 | 0.701122 | `azmcp_monitor_table_type_list` | ❌ |
| 3 | 0.599889 | `azmcp_monitor_workspace_list` | ❌ |
| 4 | 0.532887 | `azmcp_storage_table_list` | ❌ |
| 5 | 0.487237 | `azmcp_grafana_list` | ❌ |
| 6 | 0.466630 | `azmcp_kusto_table_list` | ❌ |
| 7 | 0.441635 | `azmcp_monitor_workspace_log_query` | ❌ |
| 8 | 0.427408 | `azmcp_postgres_table_list` | ❌ |
| 9 | 0.413450 | `azmcp_monitor_resource_log_query` | ❌ |
| 10 | 0.411590 | `azmcp_kusto_table_schema` | ❌ |
| 11 | 0.403863 | `azmcp_deploy_app_logs_get` | ❌ |
| 12 | 0.376474 | `azmcp_kusto_sample` | ❌ |
| 13 | 0.376338 | `azmcp_kusto_database_list` | ❌ |
| 14 | 0.373298 | `azmcp_workbooks_list` | ❌ |
| 15 | 0.370624 | `azmcp_cosmos_database_list` | ❌ |
| 16 | 0.370200 | `azmcp_search_index_list` | ❌ |
| 17 | 0.347853 | `azmcp_cosmos_database_container_list` | ❌ |
| 18 | 0.339950 | `azmcp_sql_db_list` | ❌ |
| 19 | 0.332323 | `azmcp_kusto_cluster_list` | ❌ |
| 20 | 0.331919 | `azmcp_cosmos_account_list` | ❌ |

---

## Test 129

**Expected Tool:** `azmcp_monitor_table_type_list`  
**Prompt:** List all available table types in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.881524 | `azmcp_monitor_table_type_list` | ✅ **EXPECTED** |
| 2 | 0.765702 | `azmcp_monitor_table_list` | ❌ |
| 3 | 0.569929 | `azmcp_monitor_workspace_list` | ❌ |
| 4 | 0.525469 | `azmcp_storage_table_list` | ❌ |
| 5 | 0.477280 | `azmcp_grafana_list` | ❌ |
| 6 | 0.447435 | `azmcp_kusto_table_list` | ❌ |
| 7 | 0.418517 | `azmcp_postgres_table_list` | ❌ |
| 8 | 0.416351 | `azmcp_kusto_table_schema` | ❌ |
| 9 | 0.404192 | `azmcp_monitor_metrics_definitions` | ❌ |
| 10 | 0.394213 | `azmcp_monitor_workspace_log_query` | ❌ |
| 11 | 0.380581 | `azmcp_kusto_sample` | ❌ |
| 12 | 0.371871 | `azmcp_monitor_resource_log_query` | ❌ |
| 13 | 0.369889 | `azmcp_cosmos_database_list` | ❌ |
| 14 | 0.367798 | `azmcp_workbooks_list` | ❌ |
| 15 | 0.361820 | `azmcp_kusto_database_list` | ❌ |
| 16 | 0.356649 | `azmcp_search_index_list` | ❌ |
| 17 | 0.354757 | `azmcp_kusto_cluster_list` | ❌ |
| 18 | 0.354124 | `azmcp_quota_region_availability_list` | ❌ |
| 19 | 0.347919 | `azmcp_deploy_app_logs_get` | ❌ |
| 20 | 0.346304 | `azmcp_foundry_models_list` | ❌ |

---

## Test 130

**Expected Tool:** `azmcp_monitor_table_type_list`  
**Prompt:** Show me the available table types in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.843138 | `azmcp_monitor_table_type_list` | ✅ **EXPECTED** |
| 2 | 0.736837 | `azmcp_monitor_table_list` | ❌ |
| 3 | 0.576747 | `azmcp_monitor_workspace_list` | ❌ |
| 4 | 0.502460 | `azmcp_storage_table_list` | ❌ |
| 5 | 0.475734 | `azmcp_grafana_list` | ❌ |
| 6 | 0.427934 | `azmcp_kusto_table_schema` | ❌ |
| 7 | 0.421484 | `azmcp_kusto_table_list` | ❌ |
| 8 | 0.416739 | `azmcp_monitor_workspace_log_query` | ❌ |
| 9 | 0.391308 | `azmcp_kusto_sample` | ❌ |
| 10 | 0.384124 | `azmcp_monitor_resource_log_query` | ❌ |
| 11 | 0.376246 | `azmcp_monitor_metrics_definitions` | ❌ |
| 12 | 0.372991 | `azmcp_postgres_table_list` | ❌ |
| 13 | 0.367591 | `azmcp_deploy_app_logs_get` | ❌ |
| 14 | 0.352574 | `azmcp_workbooks_list` | ❌ |
| 15 | 0.348357 | `azmcp_cosmos_database_list` | ❌ |
| 16 | 0.344675 | `azmcp_search_index_list` | ❌ |
| 17 | 0.340942 | `azmcp_postgres_table_schema_get` | ❌ |
| 18 | 0.340101 | `azmcp_foundry_models_list` | ❌ |
| 19 | 0.339804 | `azmcp_kusto_cluster_list` | ❌ |
| 20 | 0.338446 | `azmcp_kusto_database_list` | ❌ |

---

## Test 131

**Expected Tool:** `azmcp_monitor_workspace_list`  
**Prompt:** List all Log Analytics workspaces in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.813923 | `azmcp_monitor_workspace_list` | ✅ **EXPECTED** |
| 2 | 0.680201 | `azmcp_grafana_list` | ❌ |
| 3 | 0.660135 | `azmcp_monitor_table_list` | ❌ |
| 4 | 0.588276 | `azmcp_search_service_list` | ❌ |
| 5 | 0.583213 | `azmcp_monitor_table_type_list` | ❌ |
| 6 | 0.530433 | `azmcp_kusto_cluster_list` | ❌ |
| 7 | 0.517493 | `azmcp_cosmos_account_list` | ❌ |
| 8 | 0.512817 | `azmcp_aks_cluster_list` | ❌ |
| 9 | 0.502582 | `azmcp_storage_account_list` | ❌ |
| 10 | 0.500768 | `azmcp_workbooks_list` | ❌ |
| 11 | 0.494595 | `azmcp_group_list` | ❌ |
| 12 | 0.493730 | `azmcp_subscription_list` | ❌ |
| 13 | 0.487596 | `azmcp_functionapp_list` | ❌ |
| 14 | 0.487565 | `azmcp_storage_table_list` | ❌ |
| 15 | 0.471758 | `azmcp_redis_cluster_list` | ❌ |
| 16 | 0.470266 | `azmcp_postgres_server_list` | ❌ |
| 17 | 0.467655 | `azmcp_appconfig_account_list` | ❌ |
| 18 | 0.466748 | `azmcp_acr_registry_list` | ❌ |
| 19 | 0.448201 | `azmcp_kusto_database_list` | ❌ |
| 20 | 0.444214 | `azmcp_loadtesting_testresource_list` | ❌ |

---

## Test 132

**Expected Tool:** `azmcp_monitor_workspace_list`  
**Prompt:** Show me my Log Analytics workspaces  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.656249 | `azmcp_monitor_workspace_list` | ✅ **EXPECTED** |
| 2 | 0.585436 | `azmcp_monitor_table_list` | ❌ |
| 3 | 0.531083 | `azmcp_monitor_table_type_list` | ❌ |
| 4 | 0.518254 | `azmcp_grafana_list` | ❌ |
| 5 | 0.462960 | `azmcp_monitor_workspace_log_query` | ❌ |
| 6 | 0.459841 | `azmcp_deploy_app_logs_get` | ❌ |
| 7 | 0.398741 | `azmcp_search_service_list` | ❌ |
| 8 | 0.386422 | `azmcp_workbooks_list` | ❌ |
| 9 | 0.384081 | `azmcp_search_index_list` | ❌ |
| 10 | 0.383336 | `azmcp_aks_cluster_list` | ❌ |
| 11 | 0.381606 | `azmcp_monitor_resource_log_query` | ❌ |
| 12 | 0.379597 | `azmcp_storage_table_list` | ❌ |
| 13 | 0.376990 | `azmcp_storage_blob_container_list` | ❌ |
| 14 | 0.373786 | `azmcp_cosmos_account_list` | ❌ |
| 15 | 0.358029 | `azmcp_kusto_cluster_list` | ❌ |
| 16 | 0.354811 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 17 | 0.354276 | `azmcp_cosmos_database_list` | ❌ |
| 18 | 0.352809 | `azmcp_acr_registry_list` | ❌ |
| 19 | 0.350239 | `azmcp_loadtesting_testresource_list` | ❌ |
| 20 | 0.344457 | `azmcp_appconfig_account_list` | ❌ |

---

## Test 133

**Expected Tool:** `azmcp_monitor_workspace_list`  
**Prompt:** Show me the Log Analytics workspaces in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.733016 | `azmcp_monitor_workspace_list` | ✅ **EXPECTED** |
| 2 | 0.601481 | `azmcp_grafana_list` | ❌ |
| 3 | 0.580261 | `azmcp_monitor_table_list` | ❌ |
| 4 | 0.521316 | `azmcp_monitor_table_type_list` | ❌ |
| 5 | 0.500498 | `azmcp_search_service_list` | ❌ |
| 6 | 0.453702 | `azmcp_deploy_app_logs_get` | ❌ |
| 7 | 0.449975 | `azmcp_monitor_workspace_log_query` | ❌ |
| 8 | 0.439297 | `azmcp_kusto_cluster_list` | ❌ |
| 9 | 0.435475 | `azmcp_workbooks_list` | ❌ |
| 10 | 0.428945 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.426611 | `azmcp_aks_cluster_list` | ❌ |
| 12 | 0.422707 | `azmcp_subscription_list` | ❌ |
| 13 | 0.422379 | `azmcp_loadtesting_testresource_list` | ❌ |
| 14 | 0.418638 | `azmcp_storage_account_list` | ❌ |
| 15 | 0.413155 | `azmcp_storage_table_list` | ❌ |
| 16 | 0.411648 | `azmcp_acr_registry_list` | ❌ |
| 17 | 0.411448 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 18 | 0.404177 | `azmcp_group_list` | ❌ |
| 19 | 0.395576 | `azmcp_appconfig_account_list` | ❌ |
| 20 | 0.390235 | `azmcp_functionapp_list` | ❌ |

---

## Test 134

**Expected Tool:** `azmcp_monitor_workspace_log_query`  
**Prompt:** Show me the logs for the past hour in the Log Analytics workspace <workspace_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.581722 | `azmcp_monitor_workspace_log_query` | ✅ **EXPECTED** |
| 2 | 0.492849 | `azmcp_monitor_resource_log_query` | ❌ |
| 3 | 0.486023 | `azmcp_monitor_table_list` | ❌ |
| 4 | 0.484069 | `azmcp_deploy_app_logs_get` | ❌ |
| 5 | 0.483339 | `azmcp_monitor_workspace_list` | ❌ |
| 6 | 0.427301 | `azmcp_monitor_table_type_list` | ❌ |
| 7 | 0.374890 | `azmcp_monitor_metrics_query` | ❌ |
| 8 | 0.365653 | `azmcp_grafana_list` | ❌ |
| 9 | 0.322380 | `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list` | ❌ |
| 10 | 0.321962 | `azmcp_search_index_list` | ❌ |
| 11 | 0.318758 | `azmcp_workbooks_delete` | ❌ |
| 12 | 0.309778 | `azmcp_loadtesting_testrun_get` | ❌ |
| 13 | 0.301448 | `azmcp_quota_usage_check` | ❌ |
| 14 | 0.292218 | `azmcp_extension_az` | ❌ |
| 15 | 0.291654 | `azmcp_kusto_query` | ❌ |
| 16 | 0.288599 | `azmcp_aks_cluster_list` | ❌ |
| 17 | 0.287289 | `azmcp_aks_cluster_get` | ❌ |
| 18 | 0.284142 | `azmcp_loadtesting_testrun_list` | ❌ |
| 19 | 0.283216 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 20 | 0.276256 | `azmcp_cosmos_account_list` | ❌ |

---

## Test 135

**Expected Tool:** `azmcp_datadog_monitoredresources_list`  
**Prompt:** List all monitored resources in the Datadog resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.668827 | `azmcp_datadog_monitoredresources_list` | ✅ **EXPECTED** |
| 2 | 0.434813 | `azmcp_redis_cache_list` | ❌ |
| 3 | 0.413173 | `azmcp_monitor_metrics_query` | ❌ |
| 4 | 0.408658 | `azmcp_redis_cluster_list` | ❌ |
| 5 | 0.401731 | `azmcp_grafana_list` | ❌ |
| 6 | 0.393318 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 7 | 0.386685 | `azmcp_monitor_metrics_definitions` | ❌ |
| 8 | 0.369805 | `azmcp_redis_cluster_database_list` | ❌ |
| 9 | 0.364360 | `azmcp_workbooks_list` | ❌ |
| 10 | 0.355415 | `azmcp_loadtesting_testresource_list` | ❌ |
| 11 | 0.345409 | `azmcp_postgres_database_list` | ❌ |
| 12 | 0.345298 | `azmcp_group_list` | ❌ |
| 13 | 0.330769 | `azmcp_postgres_table_list` | ❌ |
| 14 | 0.327205 | `azmcp_cosmos_database_list` | ❌ |
| 15 | 0.318192 | `azmcp_sql_db_list` | ❌ |
| 16 | 0.317478 | `azmcp_quota_usage_check` | ❌ |
| 17 | 0.304097 | `azmcp_cosmos_account_list` | ❌ |
| 18 | 0.302405 | `azmcp_acr_registry_repository_list` | ❌ |
| 19 | 0.296544 | `azmcp_cosmos_database_container_list` | ❌ |
| 20 | 0.294543 | `azmcp_kusto_database_list` | ❌ |

---

## Test 136

**Expected Tool:** `azmcp_datadog_monitoredresources_list`  
**Prompt:** Show me the monitored resources in the Datadog resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.624066 | `azmcp_datadog_monitoredresources_list` | ✅ **EXPECTED** |
| 2 | 0.443481 | `azmcp_monitor_metrics_query` | ❌ |
| 3 | 0.393227 | `azmcp_redis_cache_list` | ❌ |
| 4 | 0.374071 | `azmcp_redis_cluster_list` | ❌ |
| 5 | 0.371017 | `azmcp_grafana_list` | ❌ |
| 6 | 0.370681 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 7 | 0.359274 | `azmcp_monitor_metrics_definitions` | ❌ |
| 8 | 0.350656 | `azmcp_quota_usage_check` | ❌ |
| 9 | 0.343214 | `azmcp_loadtesting_testresource_list` | ❌ |
| 10 | 0.342468 | `azmcp_redis_cluster_database_list` | ❌ |
| 11 | 0.319895 | `azmcp_workbooks_list` | ❌ |
| 12 | 0.316979 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 13 | 0.300073 | `azmcp_monitor_resource_log_query` | ❌ |
| 14 | 0.285253 | `azmcp_group_list` | ❌ |
| 15 | 0.285004 | `azmcp_quota_region_availability_list` | ❌ |
| 16 | 0.274589 | `azmcp_deploy_app_logs_get` | ❌ |
| 17 | 0.274464 | `azmcp_loadtesting_testrun_get` | ❌ |
| 18 | 0.270840 | `azmcp_loadtesting_testrun_list` | ❌ |
| 19 | 0.264788 | `azmcp_cosmos_database_list` | ❌ |
| 20 | 0.260738 | `azmcp_cosmos_account_list` | ❌ |

---

## Test 137

**Expected Tool:** `azmcp_extension_azqr`  
**Prompt:** Check my Azure subscription for any compliance issues or recommendations  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.533164 | `azmcp_quota_usage_check` | ❌ |
| 2 | 0.476826 | `azmcp_extension_azqr` | ✅ **EXPECTED** |
| 3 | 0.459005 | `azmcp_bestpractices_get` | ❌ |
| 4 | 0.442625 | `azmcp_extension_az` | ❌ |
| 5 | 0.440399 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 6 | 0.431096 | `azmcp_deploy_iac_rules_get` | ❌ |
| 7 | 0.427495 | `azmcp_search_service_list` | ❌ |
| 8 | 0.426311 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 9 | 0.423237 | `azmcp_subscription_list` | ❌ |
| 10 | 0.420585 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 11 | 0.408023 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 12 | 0.406591 | `azmcp_deploy_plan_get` | ❌ |
| 13 | 0.400363 | `azmcp_quota_region_availability_list` | ❌ |
| 14 | 0.389098 | `azmcp_monitor_workspace_list` | ❌ |
| 15 | 0.383400 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 16 | 0.380257 | `azmcp_deploy_app_logs_get` | ❌ |
| 17 | 0.366672 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 18 | 0.354341 | `azmcp_redis_cache_list` | ❌ |
| 19 | 0.351428 | `azmcp_redis_cluster_list` | ❌ |
| 20 | 0.331783 | `azmcp_storage_account_list` | ❌ |

---

## Test 138

**Expected Tool:** `azmcp_extension_azqr`  
**Prompt:** Provide compliance recommendations for my current Azure subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.527082 | `azmcp_bestpractices_get` | ❌ |
| 2 | 0.487939 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 3 | 0.474017 | `azmcp_extension_az` | ❌ |
| 4 | 0.473365 | `azmcp_deploy_iac_rules_get` | ❌ |
| 5 | 0.462743 | `azmcp_extension_azqr` | ✅ **EXPECTED** |
| 6 | 0.448085 | `azmcp_deploy_plan_get` | ❌ |
| 7 | 0.442021 | `azmcp_quota_usage_check` | ❌ |
| 8 | 0.439040 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 9 | 0.426161 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 10 | 0.385771 | `azmcp_quota_region_availability_list` | ❌ |
| 11 | 0.382470 | `azmcp_search_service_list` | ❌ |
| 12 | 0.375770 | `azmcp_subscription_list` | ❌ |
| 13 | 0.365824 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 14 | 0.359062 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 15 | 0.338359 | `azmcp_marketplace_product_get` | ❌ |
| 16 | 0.333723 | `azmcp_monitor_workspace_list` | ❌ |
| 17 | 0.330901 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 18 | 0.316612 | `azmcp_redis_cluster_list` | ❌ |
| 19 | 0.310888 | `azmcp_redis_cache_list` | ❌ |
| 20 | 0.300889 | `azmcp_storage_account_details` | ❌ |

---

## Test 139

**Expected Tool:** `azmcp_extension_azqr`  
**Prompt:** Scan my Azure subscription for compliance recommendations  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.516925 | `azmcp_extension_azqr` | ✅ **EXPECTED** |
| 2 | 0.514791 | `azmcp_bestpractices_get` | ❌ |
| 3 | 0.504673 | `azmcp_quota_usage_check` | ❌ |
| 4 | 0.494872 | `azmcp_deploy_plan_get` | ❌ |
| 5 | 0.490438 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 6 | 0.472526 | `azmcp_extension_az` | ❌ |
| 7 | 0.463564 | `azmcp_deploy_iac_rules_get` | ❌ |
| 8 | 0.463172 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 9 | 0.450091 | `azmcp_search_service_list` | ❌ |
| 10 | 0.433938 | `azmcp_quota_region_availability_list` | ❌ |
| 11 | 0.423512 | `azmcp_subscription_list` | ❌ |
| 12 | 0.417356 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 13 | 0.403533 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 14 | 0.398727 | `azmcp_monitor_workspace_list` | ❌ |
| 15 | 0.391476 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 16 | 0.372267 | `azmcp_deploy_app_logs_get` | ❌ |
| 17 | 0.371590 | `azmcp_redis_cluster_list` | ❌ |
| 18 | 0.370619 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 19 | 0.369448 | `azmcp_redis_cache_list` | ❌ |
| 20 | 0.339864 | `azmcp_role_assignment_list` | ❌ |

---

## Test 140

**Expected Tool:** `azmcp_quota_region_availability_list`  
**Prompt:** Show me the available regions for these resource types <resource_types>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.590878 | `azmcp_quota_region_availability_list` | ✅ **EXPECTED** |
| 2 | 0.413274 | `azmcp_quota_usage_check` | ❌ |
| 3 | 0.372940 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 4 | 0.361386 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 5 | 0.349685 | `azmcp_monitor_table_type_list` | ❌ |
| 6 | 0.348742 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.337839 | `azmcp_redis_cache_list` | ❌ |
| 8 | 0.331074 | `azmcp_monitor_metrics_definitions` | ❌ |
| 9 | 0.328408 | `azmcp_grafana_list` | ❌ |
| 10 | 0.313240 | `azmcp_loadtesting_testresource_list` | ❌ |
| 11 | 0.310326 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 12 | 0.307143 | `azmcp_workbooks_list` | ❌ |
| 13 | 0.290125 | `azmcp_group_list` | ❌ |
| 14 | 0.287104 | `azmcp_acr_registry_list` | ❌ |
| 15 | 0.271127 | `azmcp_loadtesting_test_get` | ❌ |
| 16 | 0.265329 | `azmcp_monitor_metrics_query` | ❌ |
| 17 | 0.264358 | `azmcp_postgres_server_list` | ❌ |
| 18 | 0.246956 | `azmcp_acr_registry_repository_list` | ❌ |
| 19 | 0.236320 | `azmcp_foundry_models_list` | ❌ |
| 20 | 0.233469 | `azmcp_bicepschema_get` | ❌ |

---

## Test 141

**Expected Tool:** `azmcp_quota_usage_check`  
**Prompt:** Check usage information for <resource_type> in region <region>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609244 | `azmcp_quota_usage_check` | ✅ **EXPECTED** |
| 2 | 0.491058 | `azmcp_quota_region_availability_list` | ❌ |
| 3 | 0.384350 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 4 | 0.380135 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 5 | 0.379029 | `azmcp_redis_cache_list` | ❌ |
| 6 | 0.365684 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.342231 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 8 | 0.338636 | `azmcp_grafana_list` | ❌ |
| 9 | 0.337380 | `azmcp_storage_blob_container_details` | ❌ |
| 10 | 0.331262 | `azmcp_monitor_metrics_definitions` | ❌ |
| 11 | 0.322571 | `azmcp_workbooks_list` | ❌ |
| 12 | 0.321707 | `azmcp_monitor_resource_log_query` | ❌ |
| 13 | 0.313009 | `azmcp_storage_account_details` | ❌ |
| 14 | 0.309805 | `azmcp_loadtesting_testrun_get` | ❌ |
| 15 | 0.305628 | `azmcp_loadtesting_test_get` | ❌ |
| 16 | 0.300710 | `azmcp_aks_cluster_get` | ❌ |
| 17 | 0.280386 | `azmcp_loadtesting_testrun_list` | ❌ |
| 18 | 0.279857 | `azmcp_appconfig_kv_show` | ❌ |
| 19 | 0.270700 | `azmcp_group_list` | ❌ |
| 20 | 0.268487 | `azmcp_loadtesting_testresource_list` | ❌ |

---

## Test 142

**Expected Tool:** `azmcp_role_assignment_list`  
**Prompt:** List all available role assignments in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.645259 | `azmcp_role_assignment_list` | ✅ **EXPECTED** |
| 2 | 0.487393 | `azmcp_search_service_list` | ❌ |
| 3 | 0.483988 | `azmcp_group_list` | ❌ |
| 4 | 0.483125 | `azmcp_subscription_list` | ❌ |
| 5 | 0.478700 | `azmcp_grafana_list` | ❌ |
| 6 | 0.474796 | `azmcp_redis_cache_list` | ❌ |
| 7 | 0.471364 | `azmcp_cosmos_account_list` | ❌ |
| 8 | 0.460029 | `azmcp_redis_cluster_list` | ❌ |
| 9 | 0.452829 | `azmcp_monitor_workspace_list` | ❌ |
| 10 | 0.448130 | `azmcp_storage_account_list` | ❌ |
| 11 | 0.446372 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 12 | 0.441100 | `azmcp_functionapp_list` | ❌ |
| 13 | 0.430667 | `azmcp_kusto_cluster_list` | ❌ |
| 14 | 0.427950 | `azmcp_workbooks_list` | ❌ |
| 15 | 0.426624 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 16 | 0.403310 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 17 | 0.397565 | `azmcp_appconfig_account_list` | ❌ |
| 18 | 0.396044 | `azmcp_aks_cluster_list` | ❌ |
| 19 | 0.374732 | `azmcp_loadtesting_testresource_list` | ❌ |
| 20 | 0.365611 | `azmcp_acr_registry_list` | ❌ |

---

## Test 143

**Expected Tool:** `azmcp_role_assignment_list`  
**Prompt:** Show me the available role assignments in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609705 | `azmcp_role_assignment_list` | ✅ **EXPECTED** |
| 2 | 0.456956 | `azmcp_grafana_list` | ❌ |
| 3 | 0.436747 | `azmcp_subscription_list` | ❌ |
| 4 | 0.435642 | `azmcp_redis_cache_list` | ❌ |
| 5 | 0.435287 | `azmcp_search_service_list` | ❌ |
| 6 | 0.435201 | `azmcp_monitor_workspace_list` | ❌ |
| 7 | 0.428663 | `azmcp_group_list` | ❌ |
| 8 | 0.428370 | `azmcp_redis_cluster_list` | ❌ |
| 9 | 0.421627 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 10 | 0.420804 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.410380 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 12 | 0.406766 | `azmcp_quota_region_availability_list` | ❌ |
| 13 | 0.395445 | `azmcp_workbooks_list` | ❌ |
| 14 | 0.387873 | `azmcp_functionapp_list` | ❌ |
| 15 | 0.386800 | `azmcp_kusto_cluster_list` | ❌ |
| 16 | 0.383635 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 17 | 0.373204 | `azmcp_appconfig_account_list` | ❌ |
| 18 | 0.368511 | `azmcp_loadtesting_testresource_list` | ❌ |
| 19 | 0.352751 | `azmcp_aks_cluster_list` | ❌ |
| 20 | 0.351814 | `azmcp_marketplace_product_get` | ❌ |

---

## Test 144

**Expected Tool:** `azmcp_redis_cache_accesspolicy_list`  
**Prompt:** List all access policies in the Redis Cache <cache_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.757057 | `azmcp_redis_cache_accesspolicy_list` | ✅ **EXPECTED** |
| 2 | 0.565047 | `azmcp_redis_cache_list` | ❌ |
| 3 | 0.445073 | `azmcp_redis_cluster_list` | ❌ |
| 4 | 0.377563 | `azmcp_redis_cluster_database_list` | ❌ |
| 5 | 0.312428 | `azmcp_cosmos_account_list` | ❌ |
| 6 | 0.307158 | `azmcp_keyvault_secret_list` | ❌ |
| 7 | 0.303736 | `azmcp_storage_table_list` | ❌ |
| 8 | 0.303531 | `azmcp_appconfig_kv_list` | ❌ |
| 9 | 0.300119 | `azmcp_storage_account_list` | ❌ |
| 10 | 0.300024 | `azmcp_cosmos_database_list` | ❌ |
| 11 | 0.298615 | `azmcp_keyvault_certificate_list` | ❌ |
| 12 | 0.296717 | `azmcp_keyvault_key_list` | ❌ |
| 13 | 0.292082 | `azmcp_bestpractices_get` | ❌ |
| 14 | 0.286490 | `azmcp_acr_registry_repository_list` | ❌ |
| 15 | 0.284898 | `azmcp_appconfig_account_list` | ❌ |
| 16 | 0.284304 | `azmcp_grafana_list` | ❌ |
| 17 | 0.283583 | `azmcp_storage_blob_container_list` | ❌ |
| 18 | 0.281283 | `azmcp_storage_blob_container_details` | ❌ |
| 19 | 0.277696 | `azmcp_subscription_list` | ❌ |
| 20 | 0.274897 | `azmcp_role_assignment_list` | ❌ |

---

## Test 145

**Expected Tool:** `azmcp_redis_cache_accesspolicy_list`  
**Prompt:** Show me the access policies in the Redis Cache <cache_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713839 | `azmcp_redis_cache_accesspolicy_list` | ✅ **EXPECTED** |
| 2 | 0.523153 | `azmcp_redis_cache_list` | ❌ |
| 3 | 0.412377 | `azmcp_redis_cluster_list` | ❌ |
| 4 | 0.338859 | `azmcp_redis_cluster_database_list` | ❌ |
| 5 | 0.300045 | `azmcp_bestpractices_get` | ❌ |
| 6 | 0.288868 | `azmcp_storage_blob_container_details` | ❌ |
| 7 | 0.286321 | `azmcp_appconfig_kv_list` | ❌ |
| 8 | 0.280245 | `azmcp_appconfig_kv_show` | ❌ |
| 9 | 0.258045 | `azmcp_appconfig_account_list` | ❌ |
| 10 | 0.257957 | `azmcp_quota_usage_check` | ❌ |
| 11 | 0.257151 | `azmcp_cosmos_account_list` | ❌ |
| 12 | 0.253484 | `azmcp_storage_table_list` | ❌ |
| 13 | 0.253209 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 14 | 0.253169 | `azmcp_loadtesting_testrun_list` | ❌ |
| 15 | 0.249917 | `azmcp_extension_az` | ❌ |
| 16 | 0.248361 | `azmcp_storage_account_details` | ❌ |
| 17 | 0.247559 | `azmcp_keyvault_secret_list` | ❌ |
| 18 | 0.246871 | `azmcp_grafana_list` | ❌ |
| 19 | 0.241891 | `azmcp_role_assignment_list` | ❌ |
| 20 | 0.232706 | `azmcp_resourcehealth_availability-status_list` | ❌ |

---

## Test 146

**Expected Tool:** `azmcp_redis_cache_list`  
**Prompt:** List all Redis Caches in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.764063 | `azmcp_redis_cache_list` | ✅ **EXPECTED** |
| 2 | 0.653924 | `azmcp_redis_cluster_list` | ❌ |
| 3 | 0.501880 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 4 | 0.495048 | `azmcp_postgres_server_list` | ❌ |
| 5 | 0.472307 | `azmcp_grafana_list` | ❌ |
| 6 | 0.466141 | `azmcp_kusto_cluster_list` | ❌ |
| 7 | 0.464785 | `azmcp_redis_cluster_database_list` | ❌ |
| 8 | 0.433313 | `azmcp_search_service_list` | ❌ |
| 9 | 0.431968 | `azmcp_cosmos_account_list` | ❌ |
| 10 | 0.431715 | `azmcp_appconfig_account_list` | ❌ |
| 11 | 0.423145 | `azmcp_subscription_list` | ❌ |
| 12 | 0.396367 | `azmcp_monitor_workspace_list` | ❌ |
| 13 | 0.393596 | `azmcp_storage_account_list` | ❌ |
| 14 | 0.381343 | `azmcp_kusto_database_list` | ❌ |
| 15 | 0.379377 | `azmcp_aks_cluster_list` | ❌ |
| 16 | 0.373395 | `azmcp_group_list` | ❌ |
| 17 | 0.373274 | `azmcp_storage_table_list` | ❌ |
| 18 | 0.368719 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 19 | 0.361464 | `azmcp_acr_registry_list` | ❌ |
| 20 | 0.354948 | `azmcp_loadtesting_testresource_list` | ❌ |

---

## Test 147

**Expected Tool:** `azmcp_redis_cache_list`  
**Prompt:** Show me my Redis Caches  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.537885 | `azmcp_redis_cache_list` | ✅ **EXPECTED** |
| 2 | 0.450387 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 3 | 0.441104 | `azmcp_redis_cluster_list` | ❌ |
| 4 | 0.401235 | `azmcp_redis_cluster_database_list` | ❌ |
| 5 | 0.283598 | `azmcp_postgres_database_list` | ❌ |
| 6 | 0.265858 | `azmcp_appconfig_kv_list` | ❌ |
| 7 | 0.262106 | `azmcp_postgres_server_list` | ❌ |
| 8 | 0.257556 | `azmcp_appconfig_account_list` | ❌ |
| 9 | 0.252070 | `azmcp_grafana_list` | ❌ |
| 10 | 0.246445 | `azmcp_cosmos_database_list` | ❌ |
| 11 | 0.236096 | `azmcp_postgres_table_list` | ❌ |
| 12 | 0.233781 | `azmcp_cosmos_account_list` | ❌ |
| 13 | 0.231390 | `azmcp_loadtesting_testrun_list` | ❌ |
| 14 | 0.231294 | `azmcp_quota_usage_check` | ❌ |
| 15 | 0.225621 | `azmcp_postgres_server_config_get` | ❌ |
| 16 | 0.225079 | `azmcp_cosmos_database_container_list` | ❌ |
| 17 | 0.224946 | `azmcp_storage_blob_container_list` | ❌ |
| 18 | 0.217990 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 19 | 0.211175 | `azmcp_extension_az` | ❌ |
| 20 | 0.210134 | `azmcp_kusto_cluster_list` | ❌ |

---

## Test 148

**Expected Tool:** `azmcp_redis_cache_list`  
**Prompt:** Show me the Redis Caches in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.692210 | `azmcp_redis_cache_list` | ✅ **EXPECTED** |
| 2 | 0.595721 | `azmcp_redis_cluster_list` | ❌ |
| 3 | 0.461603 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 4 | 0.434924 | `azmcp_postgres_server_list` | ❌ |
| 5 | 0.427325 | `azmcp_grafana_list` | ❌ |
| 6 | 0.399303 | `azmcp_redis_cluster_database_list` | ❌ |
| 7 | 0.383383 | `azmcp_appconfig_account_list` | ❌ |
| 8 | 0.382294 | `azmcp_kusto_cluster_list` | ❌ |
| 9 | 0.368549 | `azmcp_search_service_list` | ❌ |
| 10 | 0.361735 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.353487 | `azmcp_subscription_list` | ❌ |
| 12 | 0.340858 | `azmcp_monitor_workspace_list` | ❌ |
| 13 | 0.327206 | `azmcp_loadtesting_testresource_list` | ❌ |
| 14 | 0.320309 | `azmcp_storage_account_list` | ❌ |
| 15 | 0.314669 | `azmcp_aks_cluster_list` | ❌ |
| 16 | 0.310802 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 17 | 0.306356 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.304064 | `azmcp_group_list` | ❌ |
| 19 | 0.303249 | `azmcp_storage_table_list` | ❌ |
| 20 | 0.298785 | `azmcp_kusto_database_list` | ❌ |

---

## Test 149

**Expected Tool:** `azmcp_redis_cluster_database_list`  
**Prompt:** List all databases in the Redis Cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.752919 | `azmcp_redis_cluster_database_list` | ✅ **EXPECTED** |
| 2 | 0.603780 | `azmcp_redis_cluster_list` | ❌ |
| 3 | 0.594994 | `azmcp_kusto_database_list` | ❌ |
| 4 | 0.548268 | `azmcp_postgres_database_list` | ❌ |
| 5 | 0.538403 | `azmcp_cosmos_database_list` | ❌ |
| 6 | 0.471359 | `azmcp_redis_cache_list` | ❌ |
| 7 | 0.458244 | `azmcp_kusto_cluster_list` | ❌ |
| 8 | 0.456133 | `azmcp_kusto_table_list` | ❌ |
| 9 | 0.449548 | `azmcp_sql_db_list` | ❌ |
| 10 | 0.419621 | `azmcp_postgres_table_list` | ❌ |
| 11 | 0.385544 | `azmcp_cosmos_database_container_list` | ❌ |
| 12 | 0.379937 | `azmcp_postgres_server_list` | ❌ |
| 13 | 0.376197 | `azmcp_aks_cluster_list` | ❌ |
| 14 | 0.366236 | `azmcp_cosmos_account_list` | ❌ |
| 15 | 0.355818 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 16 | 0.352225 | `azmcp_storage_table_list` | ❌ |
| 17 | 0.328081 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 18 | 0.325668 | `azmcp_monitor_table_list` | ❌ |
| 19 | 0.324867 | `azmcp_grafana_list` | ❌ |
| 20 | 0.317852 | `azmcp_acr_registry_repository_list` | ❌ |

---

## Test 150

**Expected Tool:** `azmcp_redis_cluster_database_list`  
**Prompt:** Show me the databases in the Redis Cluster <cluster_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.721506 | `azmcp_redis_cluster_database_list` | ✅ **EXPECTED** |
| 2 | 0.562860 | `azmcp_redis_cluster_list` | ❌ |
| 3 | 0.537788 | `azmcp_kusto_database_list` | ❌ |
| 4 | 0.481618 | `azmcp_cosmos_database_list` | ❌ |
| 5 | 0.480274 | `azmcp_postgres_database_list` | ❌ |
| 6 | 0.434940 | `azmcp_redis_cache_list` | ❌ |
| 7 | 0.414701 | `azmcp_kusto_table_list` | ❌ |
| 8 | 0.408379 | `azmcp_sql_db_list` | ❌ |
| 9 | 0.397285 | `azmcp_kusto_cluster_list` | ❌ |
| 10 | 0.351025 | `azmcp_cosmos_database_container_list` | ❌ |
| 11 | 0.349880 | `azmcp_postgres_table_list` | ❌ |
| 12 | 0.343275 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 13 | 0.337272 | `azmcp_postgres_server_list` | ❌ |
| 14 | 0.325606 | `azmcp_aks_cluster_list` | ❌ |
| 15 | 0.318982 | `azmcp_cosmos_account_list` | ❌ |
| 16 | 0.306736 | `azmcp_storage_table_list` | ❌ |
| 17 | 0.302228 | `azmcp_kusto_sample` | ❌ |
| 18 | 0.294393 | `azmcp_kusto_table_schema` | ❌ |
| 19 | 0.292180 | `azmcp_grafana_list` | ❌ |
| 20 | 0.288275 | `azmcp_sql_db_show` | ❌ |

---

## Test 151

**Expected Tool:** `azmcp_redis_cluster_list`  
**Prompt:** List all Redis Clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.812960 | `azmcp_redis_cluster_list` | ✅ **EXPECTED** |
| 2 | 0.679028 | `azmcp_kusto_cluster_list` | ❌ |
| 3 | 0.672104 | `azmcp_redis_cache_list` | ❌ |
| 4 | 0.588847 | `azmcp_redis_cluster_database_list` | ❌ |
| 5 | 0.568464 | `azmcp_aks_cluster_list` | ❌ |
| 6 | 0.554298 | `azmcp_postgres_server_list` | ❌ |
| 7 | 0.527406 | `azmcp_kusto_database_list` | ❌ |
| 8 | 0.503279 | `azmcp_grafana_list` | ❌ |
| 9 | 0.467957 | `azmcp_cosmos_account_list` | ❌ |
| 10 | 0.463770 | `azmcp_search_service_list` | ❌ |
| 11 | 0.457600 | `azmcp_kusto_cluster_get` | ❌ |
| 12 | 0.455649 | `azmcp_monitor_workspace_list` | ❌ |
| 13 | 0.445496 | `azmcp_group_list` | ❌ |
| 14 | 0.445406 | `azmcp_appconfig_account_list` | ❌ |
| 15 | 0.442886 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 16 | 0.439387 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 17 | 0.436494 | `azmcp_subscription_list` | ❌ |
| 18 | 0.422049 | `azmcp_storage_account_list` | ❌ |
| 19 | 0.419137 | `azmcp_acr_registry_list` | ❌ |
| 20 | 0.419075 | `azmcp_datadog_monitoredresources_list` | ❌ |

---

## Test 152

**Expected Tool:** `azmcp_redis_cluster_list`  
**Prompt:** Show me my Redis Clusters  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591593 | `azmcp_redis_cluster_list` | ✅ **EXPECTED** |
| 2 | 0.514375 | `azmcp_redis_cluster_database_list` | ❌ |
| 3 | 0.467519 | `azmcp_redis_cache_list` | ❌ |
| 4 | 0.403281 | `azmcp_kusto_cluster_list` | ❌ |
| 5 | 0.385069 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 6 | 0.367833 | `azmcp_aks_cluster_list` | ❌ |
| 7 | 0.329389 | `azmcp_postgres_server_list` | ❌ |
| 8 | 0.322157 | `azmcp_kusto_database_list` | ❌ |
| 9 | 0.305874 | `azmcp_kusto_cluster_get` | ❌ |
| 10 | 0.301294 | `azmcp_aks_cluster_get` | ❌ |
| 11 | 0.295045 | `azmcp_grafana_list` | ❌ |
| 12 | 0.291684 | `azmcp_postgres_database_list` | ❌ |
| 13 | 0.272504 | `azmcp_cosmos_database_list` | ❌ |
| 14 | 0.260993 | `azmcp_appconfig_account_list` | ❌ |
| 15 | 0.259662 | `azmcp_postgres_server_config_get` | ❌ |
| 16 | 0.257012 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 17 | 0.253862 | `azmcp_cosmos_account_list` | ❌ |
| 18 | 0.252053 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 19 | 0.248676 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 20 | 0.246114 | `azmcp_monitor_workspace_list` | ❌ |

---

## Test 153

**Expected Tool:** `azmcp_redis_cluster_list`  
**Prompt:** Show me the Redis Clusters in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.744239 | `azmcp_redis_cluster_list` | ✅ **EXPECTED** |
| 2 | 0.607511 | `azmcp_redis_cache_list` | ❌ |
| 3 | 0.580866 | `azmcp_kusto_cluster_list` | ❌ |
| 4 | 0.518857 | `azmcp_redis_cluster_database_list` | ❌ |
| 5 | 0.494170 | `azmcp_postgres_server_list` | ❌ |
| 6 | 0.490638 | `azmcp_aks_cluster_list` | ❌ |
| 7 | 0.456252 | `azmcp_grafana_list` | ❌ |
| 8 | 0.446568 | `azmcp_kusto_cluster_get` | ❌ |
| 9 | 0.440660 | `azmcp_kusto_database_list` | ❌ |
| 10 | 0.400256 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 11 | 0.394550 | `azmcp_monitor_workspace_list` | ❌ |
| 12 | 0.394530 | `azmcp_cosmos_account_list` | ❌ |
| 13 | 0.393490 | `azmcp_search_service_list` | ❌ |
| 14 | 0.389814 | `azmcp_appconfig_account_list` | ❌ |
| 15 | 0.372221 | `azmcp_group_list` | ❌ |
| 16 | 0.368926 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 17 | 0.367955 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 18 | 0.367096 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 19 | 0.363787 | `azmcp_subscription_list` | ❌ |
| 20 | 0.357211 | `azmcp_acr_registry_list` | ❌ |

---

## Test 154

**Expected Tool:** `azmcp_group_list`  
**Prompt:** List all resource groups in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.755935 | `azmcp_group_list` | ✅ **EXPECTED** |
| 2 | 0.566552 | `azmcp_workbooks_list` | ❌ |
| 3 | 0.552633 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 4 | 0.546156 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 5 | 0.545480 | `azmcp_redis_cluster_list` | ❌ |
| 6 | 0.542878 | `azmcp_grafana_list` | ❌ |
| 7 | 0.530600 | `azmcp_redis_cache_list` | ❌ |
| 8 | 0.524796 | `azmcp_kusto_cluster_list` | ❌ |
| 9 | 0.524265 | `azmcp_search_service_list` | ❌ |
| 10 | 0.518520 | `azmcp_acr_registry_list` | ❌ |
| 11 | 0.517060 | `azmcp_loadtesting_testresource_list` | ❌ |
| 12 | 0.500861 | `azmcp_monitor_workspace_list` | ❌ |
| 13 | 0.491176 | `azmcp_acr_registry_repository_list` | ❌ |
| 14 | 0.486716 | `azmcp_cosmos_account_list` | ❌ |
| 15 | 0.485348 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 16 | 0.479545 | `azmcp_subscription_list` | ❌ |
| 17 | 0.475951 | `azmcp_aks_cluster_list` | ❌ |
| 18 | 0.472171 | `azmcp_quota_region_availability_list` | ❌ |
| 19 | 0.460870 | `azmcp_postgres_server_list` | ❌ |
| 20 | 0.460239 | `azmcp_functionapp_list` | ❌ |

---

## Test 155

**Expected Tool:** `azmcp_group_list`  
**Prompt:** Show me my resource groups  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.529504 | `azmcp_group_list` | ✅ **EXPECTED** |
| 2 | 0.463685 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 3 | 0.459304 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 4 | 0.453960 | `azmcp_workbooks_list` | ❌ |
| 5 | 0.429014 | `azmcp_loadtesting_testresource_list` | ❌ |
| 6 | 0.426935 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.407817 | `azmcp_grafana_list` | ❌ |
| 8 | 0.391278 | `azmcp_redis_cache_list` | ❌ |
| 9 | 0.383058 | `azmcp_acr_registry_list` | ❌ |
| 10 | 0.379927 | `azmcp_acr_registry_repository_list` | ❌ |
| 11 | 0.373796 | `azmcp_quota_region_availability_list` | ❌ |
| 12 | 0.366273 | `azmcp_sql_db_list` | ❌ |
| 13 | 0.360235 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 14 | 0.350999 | `azmcp_quota_usage_check` | ❌ |
| 15 | 0.345595 | `azmcp_redis_cluster_database_list` | ❌ |
| 16 | 0.343018 | `azmcp_sql_elastic-pool_list` | ❌ |
| 17 | 0.328487 | `azmcp_loadtesting_testresource_create` | ❌ |
| 18 | 0.325631 | `azmcp_aks_cluster_list` | ❌ |
| 19 | 0.325359 | `azmcp_kusto_cluster_list` | ❌ |
| 20 | 0.323258 | `azmcp_extension_azqr` | ❌ |

---

## Test 156

**Expected Tool:** `azmcp_group_list`  
**Prompt:** Show me the resource groups in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.665771 | `azmcp_group_list` | ✅ **EXPECTED** |
| 2 | 0.532656 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 3 | 0.531920 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 4 | 0.523088 | `azmcp_redis_cluster_list` | ❌ |
| 5 | 0.522911 | `azmcp_workbooks_list` | ❌ |
| 6 | 0.518543 | `azmcp_loadtesting_testresource_list` | ❌ |
| 7 | 0.515905 | `azmcp_grafana_list` | ❌ |
| 8 | 0.492945 | `azmcp_redis_cache_list` | ❌ |
| 9 | 0.487780 | `azmcp_acr_registry_list` | ❌ |
| 10 | 0.475313 | `azmcp_search_service_list` | ❌ |
| 11 | 0.470658 | `azmcp_kusto_cluster_list` | ❌ |
| 12 | 0.464637 | `azmcp_quota_region_availability_list` | ❌ |
| 13 | 0.460428 | `azmcp_monitor_workspace_list` | ❌ |
| 14 | 0.451877 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 15 | 0.436604 | `azmcp_aks_cluster_list` | ❌ |
| 16 | 0.435360 | `azmcp_subscription_list` | ❌ |
| 17 | 0.432994 | `azmcp_cosmos_account_list` | ❌ |
| 18 | 0.429564 | `azmcp_acr_registry_repository_list` | ❌ |
| 19 | 0.423232 | `azmcp_postgres_server_list` | ❌ |
| 20 | 0.402983 | `azmcp_functionapp_list` | ❌ |

---

## Test 157

**Expected Tool:** `azmcp_resourcehealth_availability-status_get`  
**Prompt:** Get the availability status for resource <resource_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.629120 | `azmcp_resourcehealth_availability-status_get` | ✅ **EXPECTED** |
| 2 | 0.538273 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 3 | 0.377586 | `azmcp_quota_usage_check` | ❌ |
| 4 | 0.349980 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 5 | 0.331563 | `azmcp_monitor_metrics_definitions` | ❌ |
| 6 | 0.327691 | `azmcp_redis_cache_list` | ❌ |
| 7 | 0.324331 | `azmcp_quota_region_availability_list` | ❌ |
| 8 | 0.311644 | `azmcp_monitor_metrics_query` | ❌ |
| 9 | 0.308238 | `azmcp_redis_cluster_list` | ❌ |
| 10 | 0.306616 | `azmcp_grafana_list` | ❌ |
| 11 | 0.290771 | `azmcp_workbooks_show` | ❌ |
| 12 | 0.289007 | `azmcp_storage_blob_container_details` | ❌ |
| 13 | 0.286560 | `azmcp_monitor_healthmodels_entity_gethealth` | ❌ |
| 14 | 0.283436 | `azmcp_loadtesting_test_get` | ❌ |
| 15 | 0.281508 | `azmcp_workbooks_list` | ❌ |
| 16 | 0.272387 | `azmcp_aks_cluster_get` | ❌ |
| 17 | 0.272207 | `azmcp_group_list` | ❌ |
| 18 | 0.270773 | `azmcp_loadtesting_testresource_list` | ❌ |
| 19 | 0.268110 | `azmcp_loadtesting_testrun_get` | ❌ |
| 20 | 0.244023 | `azmcp_bicepschema_get` | ❌ |

---

## Test 158

**Expected Tool:** `azmcp_resourcehealth_availability-status_get`  
**Prompt:** Show me the health status of the storage account <storage_account_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.546066 | `azmcp_storage_account_details` | ❌ |
| 2 | 0.533163 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.518147 | `azmcp_storage_account_list` | ❌ |
| 4 | 0.505348 | `azmcp_storage_blob_container_details` | ❌ |
| 5 | 0.492853 | `azmcp_storage_table_list` | ❌ |
| 6 | 0.489273 | `azmcp_resourcehealth_availability-status_get` | ✅ **EXPECTED** |
| 7 | 0.476995 | `azmcp_storage_blob_list` | ❌ |
| 8 | 0.466885 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 9 | 0.426949 | `azmcp_storage_account_create` | ❌ |
| 10 | 0.411283 | `azmcp_quota_usage_check` | ❌ |
| 11 | 0.405847 | `azmcp_cosmos_account_list` | ❌ |
| 12 | 0.375351 | `azmcp_cosmos_database_container_list` | ❌ |
| 13 | 0.368262 | `azmcp_appconfig_kv_show` | ❌ |
| 14 | 0.349407 | `azmcp_cosmos_database_list` | ❌ |
| 15 | 0.321704 | `azmcp_deploy_app_logs_get` | ❌ |
| 16 | 0.311399 | `azmcp_appconfig_account_list` | ❌ |
| 17 | 0.306817 | `azmcp_keyvault_key_list` | ❌ |
| 18 | 0.304575 | `azmcp_functionapp_list` | ❌ |
| 19 | 0.301527 | `azmcp_loadtesting_testrun_get` | ❌ |
| 20 | 0.300500 | `azmcp_aks_cluster_get` | ❌ |

---

## Test 159

**Expected Tool:** `azmcp_resourcehealth_availability-status_get`  
**Prompt:** What is the availability status of virtual machine <vm_name> in resource group <resource_group_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.577398 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 2 | 0.573290 | `azmcp_resourcehealth_availability-status_get` | ✅ **EXPECTED** |
| 3 | 0.386598 | `azmcp_quota_usage_check` | ❌ |
| 4 | 0.373883 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 5 | 0.348148 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 6 | 0.321304 | `azmcp_group_list` | ❌ |
| 7 | 0.318379 | `azmcp_sql_db_list` | ❌ |
| 8 | 0.318319 | `azmcp_workbooks_list` | ❌ |
| 9 | 0.307076 | `azmcp_sql_db_show` | ❌ |
| 10 | 0.304604 | `azmcp_quota_region_availability_list` | ❌ |
| 11 | 0.302483 | `azmcp_functionapp_list` | ❌ |
| 12 | 0.300392 | `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list` | ❌ |
| 13 | 0.299346 | `azmcp_monitor_metrics_query` | ❌ |
| 14 | 0.298723 | `azmcp_monitor_metrics_definitions` | ❌ |
| 15 | 0.294197 | `azmcp_aks_cluster_get` | ❌ |
| 16 | 0.289170 | `azmcp_loadtesting_testresource_list` | ❌ |
| 17 | 0.283955 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.282460 | `azmcp_deploy_app_logs_get` | ❌ |
| 19 | 0.279417 | `azmcp_loadtesting_testresource_create` | ❌ |
| 20 | 0.276264 | `azmcp_loadtesting_test_get` | ❌ |

---

## Test 160

**Expected Tool:** `azmcp_resourcehealth_availability-status_list`  
**Prompt:** List availability status for all resources in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.737219 | `azmcp_resourcehealth_availability-status_list` | ✅ **EXPECTED** |
| 2 | 0.587526 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 3 | 0.578620 | `azmcp_redis_cache_list` | ❌ |
| 4 | 0.563455 | `azmcp_redis_cluster_list` | ❌ |
| 5 | 0.548549 | `azmcp_grafana_list` | ❌ |
| 6 | 0.540583 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 7 | 0.531356 | `azmcp_quota_region_availability_list` | ❌ |
| 8 | 0.530985 | `azmcp_group_list` | ❌ |
| 9 | 0.530242 | `azmcp_search_service_list` | ❌ |
| 10 | 0.507801 | `azmcp_monitor_workspace_list` | ❌ |
| 11 | 0.502673 | `azmcp_storage_account_list` | ❌ |
| 12 | 0.496651 | `azmcp_cosmos_account_list` | ❌ |
| 13 | 0.491394 | `azmcp_quota_usage_check` | ❌ |
| 14 | 0.491359 | `azmcp_subscription_list` | ❌ |
| 15 | 0.484221 | `azmcp_loadtesting_testresource_list` | ❌ |
| 16 | 0.482623 | `azmcp_kusto_cluster_list` | ❌ |
| 17 | 0.471670 | `azmcp_functionapp_list` | ❌ |
| 18 | 0.464514 | `azmcp_aks_cluster_list` | ❌ |
| 19 | 0.457237 | `azmcp_appconfig_account_list` | ❌ |
| 20 | 0.434346 | `azmcp_acr_registry_list` | ❌ |

---

## Test 161

**Expected Tool:** `azmcp_resourcehealth_availability-status_list`  
**Prompt:** Show me the health status of all my Azure resources  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.644982 | `azmcp_resourcehealth_availability-status_list` | ✅ **EXPECTED** |
| 2 | 0.582272 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 3 | 0.508252 | `azmcp_quota_usage_check` | ❌ |
| 4 | 0.473905 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 5 | 0.409363 | `azmcp_deploy_app_logs_get` | ❌ |
| 6 | 0.406709 | `azmcp_quota_region_availability_list` | ❌ |
| 7 | 0.405790 | `azmcp_sql_db_list` | ❌ |
| 8 | 0.403087 | `azmcp_aks_cluster_list` | ❌ |
| 9 | 0.401059 | `azmcp_functionapp_list` | ❌ |
| 10 | 0.400948 | `azmcp_search_service_list` | ❌ |
| 11 | 0.400553 | `azmcp_monitor_metrics_query` | ❌ |
| 12 | 0.400427 | `azmcp_extension_az` | ❌ |
| 13 | 0.400033 | `azmcp_subscription_list` | ❌ |
| 14 | 0.399782 | `azmcp_redis_cluster_list` | ❌ |
| 15 | 0.397368 | `azmcp_redis_cache_list` | ❌ |
| 16 | 0.391814 | `azmcp_bestpractices_get` | ❌ |
| 17 | 0.389504 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 18 | 0.387835 | `azmcp_cosmos_account_list` | ❌ |
| 19 | 0.371846 | `azmcp_loadtesting_testresource_list` | ❌ |
| 20 | 0.366824 | `azmcp_deploy_plan_get` | ❌ |

---

## Test 162

**Expected Tool:** `azmcp_resourcehealth_availability-status_list`  
**Prompt:** What resources in resource group <resource_group_name> have health issues?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.596890 | `azmcp_resourcehealth_availability-status_list` | ✅ **EXPECTED** |
| 2 | 0.536904 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 3 | 0.427638 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 4 | 0.411111 | `azmcp_quota_usage_check` | ❌ |
| 5 | 0.370961 | `azmcp_loadtesting_testresource_list` | ❌ |
| 6 | 0.363808 | `azmcp_workbooks_list` | ❌ |
| 7 | 0.360039 | `azmcp_redis_cluster_list` | ❌ |
| 8 | 0.350454 | `azmcp_group_list` | ❌ |
| 9 | 0.348923 | `azmcp_monitor_metrics_query` | ❌ |
| 10 | 0.334774 | `azmcp_redis_cache_list` | ❌ |
| 11 | 0.332886 | `azmcp_monitor_healthmodels_entity_gethealth` | ❌ |
| 12 | 0.330185 | `azmcp_extension_azqr` | ❌ |
| 13 | 0.328560 | `azmcp_extension_az` | ❌ |
| 14 | 0.321787 | `azmcp_monitor_resource_log_query` | ❌ |
| 15 | 0.319481 | `azmcp_sql_db_list` | ❌ |
| 16 | 0.317434 | `azmcp_quota_region_availability_list` | ❌ |
| 17 | 0.309414 | `azmcp_deploy_app_logs_get` | ❌ |
| 18 | 0.308680 | `azmcp_grafana_list` | ❌ |
| 19 | 0.295752 | `azmcp_monitor_metrics_definitions` | ❌ |
| 20 | 0.293688 | `azmcp_acr_registry_list` | ❌ |

---

## Test 163

**Expected Tool:** `azmcp_servicebus_queue_details`  
**Prompt:** Show me the details of service bus <service_bus_name> queue <queue_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.642876 | `azmcp_servicebus_queue_details` | ✅ **EXPECTED** |
| 2 | 0.460932 | `azmcp_servicebus_topic_subscription_details` | ❌ |
| 3 | 0.400870 | `azmcp_servicebus_topic_details` | ❌ |
| 4 | 0.376496 | `azmcp_storage_queue_message_send` | ❌ |
| 5 | 0.375386 | `azmcp_aks_cluster_get` | ❌ |
| 6 | 0.338738 | `azmcp_loadtesting_testrun_get` | ❌ |
| 7 | 0.337239 | `azmcp_sql_db_show` | ❌ |
| 8 | 0.323046 | `azmcp_kusto_cluster_get` | ❌ |
| 9 | 0.316350 | `azmcp_storage_blob_container_details` | ❌ |
| 10 | 0.310924 | `azmcp_search_index_list` | ❌ |
| 11 | 0.308567 | `azmcp_redis_cache_list` | ❌ |
| 12 | 0.306552 | `azmcp_storage_account_details` | ❌ |
| 13 | 0.301249 | `azmcp_quota_usage_check` | ❌ |
| 14 | 0.296266 | `azmcp_aks_cluster_list` | ❌ |
| 15 | 0.279102 | `azmcp_functionapp_list` | ❌ |
| 16 | 0.278054 | `azmcp_marketplace_product_get` | ❌ |
| 17 | 0.271662 | `azmcp_loadtesting_test_get` | ❌ |
| 18 | 0.266686 | `azmcp_appconfig_kv_show` | ❌ |
| 19 | 0.258399 | `azmcp_keyvault_certificate_get` | ❌ |
| 20 | 0.255819 | `azmcp_cosmos_account_list` | ❌ |

---

## Test 164

**Expected Tool:** `azmcp_servicebus_topic_details`  
**Prompt:** Show me the details of service bus <service_bus_name> topic <topic_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591649 | `azmcp_servicebus_topic_details` | ✅ **EXPECTED** |
| 2 | 0.571861 | `azmcp_servicebus_topic_subscription_details` | ❌ |
| 3 | 0.483976 | `azmcp_servicebus_queue_details` | ❌ |
| 4 | 0.361354 | `azmcp_aks_cluster_get` | ❌ |
| 5 | 0.347044 | `azmcp_loadtesting_testrun_get` | ❌ |
| 6 | 0.340036 | `azmcp_sql_db_show` | ❌ |
| 7 | 0.335558 | `azmcp_kusto_cluster_get` | ❌ |
| 8 | 0.324869 | `azmcp_redis_cache_list` | ❌ |
| 9 | 0.317265 | `azmcp_aks_cluster_list` | ❌ |
| 10 | 0.315561 | `azmcp_redis_cluster_list` | ❌ |
| 11 | 0.306601 | `azmcp_search_index_list` | ❌ |
| 12 | 0.303980 | `azmcp_search_service_list` | ❌ |
| 13 | 0.303663 | `azmcp_storage_table_list` | ❌ |
| 14 | 0.297323 | `azmcp_grafana_list` | ❌ |
| 15 | 0.295591 | `azmcp_functionapp_list` | ❌ |
| 16 | 0.294364 | `azmcp_marketplace_product_get` | ❌ |
| 17 | 0.290611 | `azmcp_monitor_workspace_list` | ❌ |
| 18 | 0.278717 | `azmcp_kusto_table_schema` | ❌ |
| 19 | 0.278644 | `azmcp_loadtesting_test_get` | ❌ |
| 20 | 0.275724 | `azmcp_loadtesting_testrun_list` | ❌ |

---

## Test 165

**Expected Tool:** `azmcp_servicebus_topic_subscription_details`  
**Prompt:** Show me the details of service bus <service_bus_name> subscription <subscription_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633187 | `azmcp_servicebus_topic_subscription_details` | ✅ **EXPECTED** |
| 2 | 0.494515 | `azmcp_servicebus_queue_details` | ❌ |
| 3 | 0.457036 | `azmcp_servicebus_topic_details` | ❌ |
| 4 | 0.449818 | `azmcp_search_service_list` | ❌ |
| 5 | 0.429458 | `azmcp_redis_cache_list` | ❌ |
| 6 | 0.426573 | `azmcp_kusto_cluster_get` | ❌ |
| 7 | 0.421009 | `azmcp_sql_db_show` | ❌ |
| 8 | 0.408907 | `azmcp_aks_cluster_list` | ❌ |
| 9 | 0.406192 | `azmcp_functionapp_list` | ❌ |
| 10 | 0.404739 | `azmcp_redis_cluster_list` | ❌ |
| 11 | 0.395984 | `azmcp_marketplace_product_get` | ❌ |
| 12 | 0.395176 | `azmcp_grafana_list` | ❌ |
| 13 | 0.388049 | `azmcp_postgres_server_list` | ❌ |
| 14 | 0.385283 | `azmcp_monitor_workspace_list` | ❌ |
| 15 | 0.374364 | `azmcp_subscription_list` | ❌ |
| 16 | 0.369986 | `azmcp_appconfig_account_list` | ❌ |
| 17 | 0.368411 | `azmcp_aks_cluster_get` | ❌ |
| 18 | 0.368155 | `azmcp_kusto_cluster_list` | ❌ |
| 19 | 0.367649 | `azmcp_group_list` | ❌ |
| 20 | 0.358070 | `azmcp_cosmos_account_list` | ❌ |

---

## Test 166

**Expected Tool:** `azmcp_sql_db_list`  
**Prompt:** List all databases in the Azure SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.643186 | `azmcp_sql_db_list` | ✅ **EXPECTED** |
| 2 | 0.609178 | `azmcp_postgres_database_list` | ❌ |
| 3 | 0.602890 | `azmcp_cosmos_database_list` | ❌ |
| 4 | 0.527896 | `azmcp_kusto_database_list` | ❌ |
| 5 | 0.482736 | `azmcp_sql_elastic-pool_list` | ❌ |
| 6 | 0.474927 | `azmcp_redis_cluster_database_list` | ❌ |
| 7 | 0.466130 | `azmcp_storage_table_list` | ❌ |
| 8 | 0.464525 | `azmcp_sql_db_show` | ❌ |
| 9 | 0.457314 | `azmcp_kusto_table_list` | ❌ |
| 10 | 0.457219 | `azmcp_postgres_server_list` | ❌ |
| 11 | 0.456149 | `azmcp_monitor_table_list` | ❌ |
| 12 | 0.443648 | `azmcp_postgres_table_list` | ❌ |
| 13 | 0.441355 | `azmcp_cosmos_account_list` | ❌ |
| 14 | 0.440528 | `azmcp_cosmos_database_container_list` | ❌ |
| 15 | 0.420957 | `azmcp_sql_server_entra-admin_list` | ❌ |
| 16 | 0.400769 | `azmcp_keyvault_certificate_list` | ❌ |
| 17 | 0.395109 | `azmcp_keyvault_key_list` | ❌ |
| 18 | 0.394626 | `azmcp_keyvault_secret_list` | ❌ |
| 19 | 0.382762 | `azmcp_functionapp_list` | ❌ |
| 20 | 0.380402 | `azmcp_acr_registry_repository_list` | ❌ |

---

## Test 167

**Expected Tool:** `azmcp_sql_db_list`  
**Prompt:** Show me all the databases configuration details in the Azure SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.609322 | `azmcp_sql_db_list` | ✅ **EXPECTED** |
| 2 | 0.524274 | `azmcp_sql_db_show` | ❌ |
| 3 | 0.471862 | `azmcp_postgres_database_list` | ❌ |
| 4 | 0.461650 | `azmcp_cosmos_database_list` | ❌ |
| 5 | 0.458742 | `azmcp_postgres_server_config_get` | ❌ |
| 6 | 0.454316 | `azmcp_sql_elastic-pool_list` | ❌ |
| 7 | 0.394366 | `azmcp_redis_cluster_database_list` | ❌ |
| 8 | 0.387645 | `azmcp_kusto_database_list` | ❌ |
| 9 | 0.387445 | `azmcp_postgres_server_list` | ❌ |
| 10 | 0.380428 | `azmcp_appconfig_account_list` | ❌ |
| 11 | 0.372897 | `azmcp_storage_account_details` | ❌ |
| 12 | 0.356861 | `azmcp_sql_server_entra-admin_list` | ❌ |
| 13 | 0.356661 | `azmcp_aks_cluster_list` | ❌ |
| 14 | 0.350224 | `azmcp_storage_table_list` | ❌ |
| 15 | 0.349880 | `azmcp_cosmos_account_list` | ❌ |
| 16 | 0.347075 | `azmcp_cosmos_database_container_list` | ❌ |
| 17 | 0.345262 | `azmcp_loadtesting_test_get` | ❌ |
| 18 | 0.342792 | `azmcp_appconfig_kv_list` | ❌ |
| 19 | 0.342284 | `azmcp_aks_cluster_get` | ❌ |
| 20 | 0.341681 | `azmcp_kusto_table_list` | ❌ |

---

## Test 168

**Expected Tool:** `azmcp_sql_db_show`  
**Prompt:** Get the configuration details for the SQL database <database_name> on server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.593150 | `azmcp_postgres_server_config_get` | ❌ |
| 2 | 0.528136 | `azmcp_sql_db_show` | ✅ **EXPECTED** |
| 3 | 0.465693 | `azmcp_sql_db_list` | ❌ |
| 4 | 0.446682 | `azmcp_postgres_server_param_get` | ❌ |
| 5 | 0.374073 | `azmcp_sql_server_firewall-rule_list` | ❌ |
| 6 | 0.371766 | `azmcp_loadtesting_test_get` | ❌ |
| 7 | 0.354111 | `azmcp_sql_server_entra-admin_list` | ❌ |
| 8 | 0.348228 | `azmcp_sql_elastic-pool_list` | ❌ |
| 9 | 0.341701 | `azmcp_postgres_database_list` | ❌ |
| 10 | 0.341203 | `azmcp_postgres_table_schema_get` | ❌ |
| 11 | 0.325945 | `azmcp_kusto_table_schema` | ❌ |
| 12 | 0.320054 | `azmcp_aks_cluster_get` | ❌ |
| 13 | 0.312720 | `azmcp_storage_account_details` | ❌ |
| 14 | 0.297839 | `azmcp_appconfig_kv_show` | ❌ |
| 15 | 0.294987 | `azmcp_appconfig_kv_list` | ❌ |
| 16 | 0.273566 | `azmcp_kusto_cluster_get` | ❌ |
| 17 | 0.273315 | `azmcp_cosmos_database_list` | ❌ |
| 18 | 0.263979 | `azmcp_appconfig_account_list` | ❌ |
| 19 | 0.260930 | `azmcp_loadtesting_testrun_list` | ❌ |
| 20 | 0.253680 | `azmcp_kusto_table_list` | ❌ |

---

## Test 169

**Expected Tool:** `azmcp_sql_db_show`  
**Prompt:** Show me the details of SQL database <database_name> in server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.530095 | `azmcp_sql_db_show` | ✅ **EXPECTED** |
| 2 | 0.440073 | `azmcp_sql_db_list` | ❌ |
| 3 | 0.421862 | `azmcp_postgres_database_list` | ❌ |
| 4 | 0.375668 | `azmcp_postgres_server_config_get` | ❌ |
| 5 | 0.361500 | `azmcp_redis_cluster_database_list` | ❌ |
| 6 | 0.357119 | `azmcp_postgres_server_param_get` | ❌ |
| 7 | 0.351744 | `azmcp_postgres_table_schema_get` | ❌ |
| 8 | 0.344694 | `azmcp_kusto_table_schema` | ❌ |
| 9 | 0.343310 | `azmcp_postgres_table_list` | ❌ |
| 10 | 0.339765 | `azmcp_postgres_server_list` | ❌ |
| 11 | 0.337996 | `azmcp_cosmos_database_list` | ❌ |
| 12 | 0.328612 | `azmcp_sql_elastic-pool_list` | ❌ |
| 13 | 0.323587 | `azmcp_kusto_table_list` | ❌ |
| 14 | 0.300133 | `azmcp_cosmos_database_container_list` | ❌ |
| 15 | 0.299814 | `azmcp_aks_cluster_get` | ❌ |
| 16 | 0.296827 | `azmcp_kusto_database_list` | ❌ |
| 17 | 0.294910 | `azmcp_loadtesting_testrun_get` | ❌ |
| 18 | 0.285843 | `azmcp_kusto_cluster_get` | ❌ |
| 19 | 0.261790 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 20 | 0.252492 | `azmcp_kusto_sample` | ❌ |

---

## Test 170

**Expected Tool:** `azmcp_sql_elastic-pool_list`  
**Prompt:** List all elastic pools in SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.686435 | `azmcp_sql_elastic-pool_list` | ✅ **EXPECTED** |
| 2 | 0.502376 | `azmcp_sql_db_list` | ❌ |
| 3 | 0.458302 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 4 | 0.434570 | `azmcp_postgres_server_list` | ❌ |
| 5 | 0.431871 | `azmcp_sql_server_entra-admin_list` | ❌ |
| 6 | 0.431174 | `azmcp_cosmos_database_list` | ❌ |
| 7 | 0.416273 | `azmcp_monitor_table_list` | ❌ |
| 8 | 0.414738 | `azmcp_postgres_database_list` | ❌ |
| 9 | 0.412061 | `azmcp_sql_server_firewall-rule_list` | ❌ |
| 10 | 0.409078 | `azmcp_monitor_table_type_list` | ❌ |
| 11 | 0.408080 | `azmcp_virtualdesktop_hostpool_sessionhost_list` | ❌ |
| 12 | 0.394337 | `azmcp_kusto_database_list` | ❌ |
| 13 | 0.370652 | `azmcp_cosmos_account_list` | ❌ |
| 14 | 0.363579 | `azmcp_kusto_cluster_list` | ❌ |
| 15 | 0.357347 | `azmcp_kusto_table_list` | ❌ |
| 16 | 0.351647 | `azmcp_cosmos_database_container_list` | ❌ |
| 17 | 0.351294 | `azmcp_aks_cluster_list` | ❌ |
| 18 | 0.349497 | `azmcp_keyvault_key_list` | ❌ |
| 19 | 0.348636 | `azmcp_keyvault_secret_list` | ❌ |
| 20 | 0.331929 | `azmcp_keyvault_certificate_list` | ❌ |

---

## Test 171

**Expected Tool:** `azmcp_sql_elastic-pool_list`  
**Prompt:** Show me the elastic pools configured for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.616579 | `azmcp_sql_elastic-pool_list` | ✅ **EXPECTED** |
| 2 | 0.457163 | `azmcp_sql_db_list` | ❌ |
| 3 | 0.389072 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 4 | 0.385834 | `azmcp_sql_server_entra-admin_list` | ❌ |
| 5 | 0.378556 | `azmcp_postgres_server_list` | ❌ |
| 6 | 0.357655 | `azmcp_sql_server_firewall-rule_list` | ❌ |
| 7 | 0.357019 | `azmcp_postgres_server_config_get` | ❌ |
| 8 | 0.354094 | `azmcp_sql_db_show` | ❌ |
| 9 | 0.343841 | `azmcp_virtualdesktop_hostpool_sessionhost_list` | ❌ |
| 10 | 0.335615 | `azmcp_cosmos_database_list` | ❌ |
| 11 | 0.334567 | `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list` | ❌ |
| 12 | 0.318796 | `azmcp_aks_cluster_list` | ❌ |
| 13 | 0.304600 | `azmcp_cosmos_account_list` | ❌ |
| 14 | 0.304317 | `azmcp_appconfig_account_list` | ❌ |
| 15 | 0.298907 | `azmcp_kusto_database_list` | ❌ |
| 16 | 0.298264 | `azmcp_acr_registry_list` | ❌ |
| 17 | 0.297857 | `azmcp_aks_cluster_get` | ❌ |
| 18 | 0.293905 | `azmcp_cosmos_database_container_list` | ❌ |
| 19 | 0.277055 | `azmcp_loadtesting_test_get` | ❌ |
| 20 | 0.274081 | `azmcp_kusto_cluster_list` | ❌ |

---

## Test 172

**Expected Tool:** `azmcp_sql_elastic-pool_list`  
**Prompt:** What elastic pools are available in my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.602478 | `azmcp_sql_elastic-pool_list` | ✅ **EXPECTED** |
| 2 | 0.397670 | `azmcp_sql_db_list` | ❌ |
| 3 | 0.378527 | `azmcp_monitor_table_type_list` | ❌ |
| 4 | 0.367742 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 5 | 0.344799 | `azmcp_postgres_server_list` | ❌ |
| 6 | 0.322365 | `azmcp_virtualdesktop_hostpool_sessionhost_list` | ❌ |
| 7 | 0.316044 | `azmcp_sql_server_entra-admin_list` | ❌ |
| 8 | 0.311302 | `azmcp_redis_cluster_list` | ❌ |
| 9 | 0.308077 | `azmcp_redis_cluster_database_list` | ❌ |
| 10 | 0.307724 | `azmcp_storage_table_list` | ❌ |
| 11 | 0.298933 | `azmcp_cosmos_database_list` | ❌ |
| 12 | 0.292566 | `azmcp_kusto_cluster_list` | ❌ |
| 13 | 0.284157 | `azmcp_kusto_database_list` | ❌ |
| 14 | 0.281680 | `azmcp_cosmos_account_list` | ❌ |
| 15 | 0.272025 | `azmcp_monitor_metrics_definitions` | ❌ |
| 16 | 0.259325 | `azmcp_loadtesting_testresource_list` | ❌ |
| 17 | 0.256675 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.252920 | `azmcp_foundry_models_deployments_list` | ❌ |
| 19 | 0.249936 | `azmcp_extension_az` | ❌ |
| 20 | 0.247097 | `azmcp_grafana_list` | ❌ |

---

## Test 173

**Expected Tool:** `azmcp_sql_server_entra-admin_list`  
**Prompt:** List Microsoft Entra ID administrators for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.788356 | `azmcp_sql_server_entra-admin_list` | ✅ **EXPECTED** |
| 2 | 0.407432 | `azmcp_sql_server_firewall-rule_list` | ❌ |
| 3 | 0.376055 | `azmcp_sql_db_list` | ❌ |
| 4 | 0.365636 | `azmcp_postgres_server_list` | ❌ |
| 5 | 0.328968 | `azmcp_sql_elastic-pool_list` | ❌ |
| 6 | 0.328737 | `azmcp_role_assignment_list` | ❌ |
| 7 | 0.312627 | `azmcp_postgres_database_list` | ❌ |
| 8 | 0.283286 | `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list` | ❌ |
| 9 | 0.280450 | `azmcp_cosmos_database_list` | ❌ |
| 10 | 0.279198 | `azmcp_sql_db_show` | ❌ |
| 11 | 0.277773 | `azmcp_storage_table_list` | ❌ |
| 12 | 0.258095 | `azmcp_cosmos_account_list` | ❌ |
| 13 | 0.249297 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 14 | 0.249153 | `azmcp_kusto_database_list` | ❌ |
| 15 | 0.246677 | `azmcp_keyvault_secret_list` | ❌ |
| 16 | 0.245267 | `azmcp_group_list` | ❌ |
| 17 | 0.238164 | `azmcp_keyvault_key_list` | ❌ |
| 18 | 0.233337 | `azmcp_cosmos_database_container_list` | ❌ |
| 19 | 0.232633 | `azmcp_loadtesting_testrun_list` | ❌ |
| 20 | 0.228028 | `azmcp_keyvault_certificate_list` | ❌ |

---

## Test 174

**Expected Tool:** `azmcp_sql_server_entra-admin_list`  
**Prompt:** Show me the Entra ID administrators configured for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.718251 | `azmcp_sql_server_entra-admin_list` | ✅ **EXPECTED** |
| 2 | 0.315966 | `azmcp_sql_db_list` | ❌ |
| 3 | 0.311085 | `azmcp_postgres_server_list` | ❌ |
| 4 | 0.309059 | `azmcp_sql_server_firewall-rule_list` | ❌ |
| 5 | 0.303560 | `azmcp_postgres_server_config_get` | ❌ |
| 6 | 0.268897 | `azmcp_sql_elastic-pool_list` | ❌ |
| 7 | 0.266264 | `azmcp_postgres_server_param_get` | ❌ |
| 8 | 0.250838 | `azmcp_sql_db_show` | ❌ |
| 9 | 0.249616 | `azmcp_postgres_database_list` | ❌ |
| 10 | 0.228064 | `azmcp_role_assignment_list` | ❌ |
| 11 | 0.214529 | `azmcp_cosmos_database_list` | ❌ |
| 12 | 0.197679 | `azmcp_cosmos_database_container_list` | ❌ |
| 13 | 0.194313 | `azmcp_appconfig_account_list` | ❌ |
| 14 | 0.193050 | `azmcp_kusto_database_list` | ❌ |
| 15 | 0.191538 | `azmcp_appconfig_kv_list` | ❌ |
| 16 | 0.188120 | `azmcp_cosmos_account_list` | ❌ |
| 17 | 0.186088 | `azmcp_loadtesting_testrun_list` | ❌ |
| 18 | 0.183184 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 19 | 0.182322 | `azmcp_extension_az` | ❌ |
| 20 | 0.182237 | `azmcp_deploy_app_logs_get` | ❌ |

---

## Test 175

**Expected Tool:** `azmcp_sql_server_entra-admin_list`  
**Prompt:** What Microsoft Entra ID administrators are set up for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.651306 | `azmcp_sql_server_entra-admin_list` | ✅ **EXPECTED** |
| 2 | 0.253610 | `azmcp_sql_db_list` | ❌ |
| 3 | 0.244772 | `azmcp_extension_az` | ❌ |
| 4 | 0.229560 | `azmcp_sql_elastic-pool_list` | ❌ |
| 5 | 0.228093 | `azmcp_sql_server_firewall-rule_list` | ❌ |
| 6 | 0.217698 | `azmcp_postgres_server_list` | ❌ |
| 7 | 0.205654 | `azmcp_sql_db_show` | ❌ |
| 8 | 0.198194 | `azmcp_monitor_resource_log_query` | ❌ |
| 9 | 0.189941 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 10 | 0.189581 | `azmcp_storage_table_list` | ❌ |
| 11 | 0.188753 | `azmcp_postgres_server_param_get` | ❌ |
| 12 | 0.188287 | `azmcp_deploy_plan_get` | ❌ |
| 13 | 0.182452 | `azmcp_postgres_server_config_get` | ❌ |
| 14 | 0.180995 | `azmcp_deploy_app_logs_get` | ❌ |
| 15 | 0.180555 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 16 | 0.174553 | `azmcp_deploy_iac_rules_get` | ❌ |
| 17 | 0.169345 | `azmcp_kusto_database_list` | ❌ |
| 18 | 0.165162 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 19 | 0.164022 | `azmcp_extension_azd` | ❌ |
| 20 | 0.163349 | `azmcp_bestpractices_get` | ❌ |

---

## Test 176

**Expected Tool:** `azmcp_sql_server_firewall-rule_list`  
**Prompt:** List all firewall rules for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.732275 | `azmcp_sql_server_firewall-rule_list` | ✅ **EXPECTED** |
| 2 | 0.397092 | `azmcp_sql_server_entra-admin_list` | ❌ |
| 3 | 0.385148 | `azmcp_postgres_server_list` | ❌ |
| 4 | 0.359228 | `azmcp_sql_db_list` | ❌ |
| 5 | 0.347004 | `azmcp_sql_elastic-pool_list` | ❌ |
| 6 | 0.327808 | `azmcp_postgres_database_list` | ❌ |
| 7 | 0.305056 | `azmcp_keyvault_secret_list` | ❌ |
| 8 | 0.304175 | `azmcp_monitor_table_list` | ❌ |
| 9 | 0.301711 | `azmcp_postgres_table_list` | ❌ |
| 10 | 0.299205 | `azmcp_postgres_server_config_get` | ❌ |
| 11 | 0.298226 | `azmcp_sql_db_show` | ❌ |
| 12 | 0.278098 | `azmcp_cosmos_database_list` | ❌ |
| 13 | 0.277803 | `azmcp_functionapp_list` | ❌ |
| 14 | 0.277466 | `azmcp_keyvault_key_list` | ❌ |
| 15 | 0.277086 | `azmcp_keyvault_certificate_list` | ❌ |
| 16 | 0.270667 | `azmcp_cosmos_account_list` | ❌ |
| 17 | 0.263181 | `azmcp_kusto_table_list` | ❌ |
| 18 | 0.263086 | `azmcp_bestpractices_get` | ❌ |
| 19 | 0.259932 | `azmcp_extension_az` | ❌ |
| 20 | 0.253852 | `azmcp_cosmos_database_container_list` | ❌ |

---

## Test 177

**Expected Tool:** `azmcp_sql_server_firewall-rule_list`  
**Prompt:** Show me the firewall rules for SQL server <server_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.631499 | `azmcp_sql_server_firewall-rule_list` | ✅ **EXPECTED** |
| 2 | 0.321414 | `azmcp_sql_server_entra-admin_list` | ❌ |
| 3 | 0.312035 | `azmcp_postgres_server_list` | ❌ |
| 4 | 0.290374 | `azmcp_extension_az` | ❌ |
| 5 | 0.290235 | `azmcp_postgres_server_config_get` | ❌ |
| 6 | 0.287747 | `azmcp_postgres_server_param_get` | ❌ |
| 7 | 0.276175 | `azmcp_sql_db_list` | ❌ |
| 8 | 0.272586 | `azmcp_sql_elastic-pool_list` | ❌ |
| 9 | 0.272053 | `azmcp_sql_db_show` | ❌ |
| 10 | 0.255371 | `azmcp_bestpractices_get` | ❌ |
| 11 | 0.234831 | `azmcp_quota_usage_check` | ❌ |
| 12 | 0.228931 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 13 | 0.225372 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 14 | 0.208281 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 15 | 0.206701 | `azmcp_keyvault_secret_list` | ❌ |
| 16 | 0.206476 | `azmcp_deploy_iac_rules_get` | ❌ |
| 17 | 0.206114 | `azmcp_kusto_table_list` | ❌ |
| 18 | 0.197711 | `azmcp_kusto_sample` | ❌ |
| 19 | 0.189871 | `azmcp_cosmos_account_list` | ❌ |
| 20 | 0.189786 | `azmcp_cosmos_database_list` | ❌ |

---

## Test 178

**Expected Tool:** `azmcp_sql_server_firewall-rule_list`  
**Prompt:** What firewall rules are configured for my SQL server <server_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.633622 | `azmcp_sql_server_firewall-rule_list` | ✅ **EXPECTED** |
| 2 | 0.311867 | `azmcp_sql_server_entra-admin_list` | ❌ |
| 3 | 0.299474 | `azmcp_extension_az` | ❌ |
| 4 | 0.277628 | `azmcp_postgres_server_config_get` | ❌ |
| 5 | 0.262028 | `azmcp_sql_db_list` | ❌ |
| 6 | 0.261404 | `azmcp_postgres_server_list` | ❌ |
| 7 | 0.261123 | `azmcp_postgres_server_param_get` | ❌ |
| 8 | 0.258402 | `azmcp_sql_elastic-pool_list` | ❌ |
| 9 | 0.247516 | `azmcp_bestpractices_get` | ❌ |
| 10 | 0.223532 | `azmcp_postgres_server_param_set` | ❌ |
| 11 | 0.220723 | `azmcp_sql_db_show` | ❌ |
| 12 | 0.205282 | `azmcp_redis_cache_accesspolicy_list` | ❌ |
| 13 | 0.202425 | `azmcp_deploy_iac_rules_get` | ❌ |
| 14 | 0.200326 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 15 | 0.186128 | `azmcp_loadtesting_test_get` | ❌ |
| 16 | 0.185378 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 17 | 0.167047 | `azmcp_keyvault_secret_list` | ❌ |
| 18 | 0.162904 | `azmcp_functionapp_list` | ❌ |
| 19 | 0.162568 | `azmcp_kusto_query` | ❌ |
| 20 | 0.161583 | `azmcp_appconfig_kv_list` | ❌ |

---

## Test 179

**Expected Tool:** `azmcp_storage_account_create`  
**Prompt:** Create a new storage account called testaccount123 in East US region  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.529799 | `azmcp_storage_account_create` | ✅ **EXPECTED** |
| 2 | 0.428712 | `azmcp_storage_account_details` | ❌ |
| 3 | 0.412893 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.412332 | `azmcp_storage_account_list` | ❌ |
| 5 | 0.391586 | `azmcp_storage_table_list` | ❌ |
| 6 | 0.374006 | `azmcp_loadtesting_test_create` | ❌ |
| 7 | 0.355049 | `azmcp_loadtesting_testresource_create` | ❌ |
| 8 | 0.346555 | `azmcp_storage_blob_list` | ❌ |
| 9 | 0.343651 | `azmcp_storage_blob_container_details` | ❌ |
| 10 | 0.325914 | `azmcp_keyvault_secret_create` | ❌ |
| 11 | 0.323501 | `azmcp_appconfig_kv_set` | ❌ |
| 12 | 0.319843 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.315241 | `azmcp_keyvault_key_create` | ❌ |
| 14 | 0.311744 | `azmcp_storage_blob_container_create` | ❌ |
| 15 | 0.308283 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 16 | 0.305196 | `azmcp_keyvault_certificate_create` | ❌ |
| 17 | 0.297236 | `azmcp_cosmos_account_list` | ❌ |
| 18 | 0.289742 | `azmcp_appconfig_kv_show` | ❌ |
| 19 | 0.277805 | `azmcp_cosmos_database_container_list` | ❌ |
| 20 | 0.264217 | `azmcp_appconfig_kv_lock` | ❌ |

---

## Test 180

**Expected Tool:** `azmcp_storage_account_create`  
**Prompt:** Create a storage account with premium performance and LRS replication  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.492373 | `azmcp_storage_account_create` | ✅ **EXPECTED** |
| 2 | 0.403775 | `azmcp_storage_account_list` | ❌ |
| 3 | 0.401456 | `azmcp_storage_account_details` | ❌ |
| 4 | 0.369322 | `azmcp_storage_blob_container_list` | ❌ |
| 5 | 0.361412 | `azmcp_storage_table_list` | ❌ |
| 6 | 0.359300 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 7 | 0.355743 | `azmcp_storage_blob_container_details` | ❌ |
| 8 | 0.344343 | `azmcp_loadtesting_testresource_create` | ❌ |
| 9 | 0.329099 | `azmcp_loadtesting_test_create` | ❌ |
| 10 | 0.327972 | `azmcp_storage_blob_list` | ❌ |
| 11 | 0.315053 | `azmcp_storage_blob_upload` | ❌ |
| 12 | 0.310332 | `azmcp_workbooks_create` | ❌ |
| 13 | 0.302787 | `azmcp_extension_az` | ❌ |
| 14 | 0.284886 | `azmcp_bestpractices_get` | ❌ |
| 15 | 0.284467 | `azmcp_deploy_plan_get` | ❌ |
| 16 | 0.284385 | `azmcp_cosmos_account_list` | ❌ |
| 17 | 0.283067 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 18 | 0.281142 | `azmcp_appconfig_kv_lock` | ❌ |
| 19 | 0.280326 | `azmcp_keyvault_certificate_create` | ❌ |
| 20 | 0.278858 | `azmcp_keyvault_key_create` | ❌ |

---

## Test 181

**Expected Tool:** `azmcp_storage_account_create`  
**Prompt:** Create a new storage account with Data Lake Storage Gen2 enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.628313 | `azmcp_storage_account_create` | ✅ **EXPECTED** |
| 2 | 0.453252 | `azmcp_storage_account_details` | ❌ |
| 3 | 0.444359 | `azmcp_storage_datalake_directory_create` | ❌ |
| 4 | 0.426606 | `azmcp_storage_blob_container_list` | ❌ |
| 5 | 0.424664 | `azmcp_storage_account_list` | ❌ |
| 6 | 0.392966 | `azmcp_storage_blob_container_create` | ❌ |
| 7 | 0.389091 | `azmcp_storage_blob_container_details` | ❌ |
| 8 | 0.386262 | `azmcp_storage_table_list` | ❌ |
| 9 | 0.383927 | `azmcp_loadtesting_testresource_create` | ❌ |
| 10 | 0.380638 | `azmcp_loadtesting_test_create` | ❌ |
| 11 | 0.380503 | `azmcp_keyvault_key_create` | ❌ |
| 12 | 0.372371 | `azmcp_keyvault_certificate_create` | ❌ |
| 13 | 0.372115 | `azmcp_storage_blob_list` | ❌ |
| 14 | 0.366696 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 15 | 0.363721 | `azmcp_workbooks_create` | ❌ |
| 16 | 0.359310 | `azmcp_keyvault_secret_create` | ❌ |
| 17 | 0.321846 | `azmcp_deploy_plan_get` | ❌ |
| 18 | 0.309241 | `azmcp_appconfig_kv_set` | ❌ |
| 19 | 0.302875 | `azmcp_cosmos_account_list` | ❌ |
| 20 | 0.301794 | `azmcp_deploy_app_logs_get` | ❌ |

---

## Test 182

**Expected Tool:** `azmcp_storage_account_details`  
**Prompt:** Show me the details for my storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.639321 | `azmcp_storage_account_details` | ✅ **EXPECTED** |
| 2 | 0.585303 | `azmcp_storage_blob_container_details` | ❌ |
| 3 | 0.561555 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.504386 | `azmcp_storage_table_list` | ❌ |
| 5 | 0.503390 | `azmcp_storage_account_list` | ❌ |
| 6 | 0.502723 | `azmcp_storage_blob_list` | ❌ |
| 7 | 0.463423 | `azmcp_storage_blob_details` | ❌ |
| 8 | 0.451686 | `azmcp_storage_account_create` | ❌ |
| 9 | 0.442858 | `azmcp_appconfig_kv_show` | ❌ |
| 10 | 0.439236 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.403478 | `azmcp_cosmos_database_container_list` | ❌ |
| 12 | 0.395698 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.388425 | `azmcp_aks_cluster_get` | ❌ |
| 14 | 0.368567 | `azmcp_sql_db_show` | ❌ |
| 15 | 0.367049 | `azmcp_kusto_cluster_get` | ❌ |
| 16 | 0.356973 | `azmcp_cosmos_database_list` | ❌ |
| 17 | 0.353431 | `azmcp_loadtesting_testrun_get` | ❌ |
| 18 | 0.341278 | `azmcp_appconfig_kv_list` | ❌ |
| 19 | 0.333326 | `azmcp_appconfig_account_list` | ❌ |
| 20 | 0.326892 | `azmcp_loadtesting_test_get` | ❌ |

---

## Test 183

**Expected Tool:** `azmcp_storage_account_details`  
**Prompt:** Get details about the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.669635 | `azmcp_storage_account_details` | ✅ **EXPECTED** |
| 2 | 0.609075 | `azmcp_storage_blob_container_details` | ❌ |
| 3 | 0.546794 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.497975 | `azmcp_storage_account_list` | ❌ |
| 5 | 0.495948 | `azmcp_storage_account_create` | ❌ |
| 6 | 0.487329 | `azmcp_storage_blob_list` | ❌ |
| 7 | 0.483947 | `azmcp_storage_table_list` | ❌ |
| 8 | 0.470882 | `azmcp_storage_blob_details` | ❌ |
| 9 | 0.415410 | `azmcp_cosmos_account_list` | ❌ |
| 10 | 0.411808 | `azmcp_appconfig_kv_show` | ❌ |
| 11 | 0.375790 | `azmcp_quota_usage_check` | ❌ |
| 12 | 0.373470 | `azmcp_aks_cluster_get` | ❌ |
| 13 | 0.369755 | `azmcp_cosmos_database_container_list` | ❌ |
| 14 | 0.368023 | `azmcp_kusto_cluster_get` | ❌ |
| 15 | 0.355094 | `azmcp_servicebus_queue_details` | ❌ |
| 16 | 0.340406 | `azmcp_appconfig_kv_lock` | ❌ |
| 17 | 0.337077 | `azmcp_loadtesting_testrun_get` | ❌ |
| 18 | 0.330558 | `azmcp_cosmos_database_list` | ❌ |
| 19 | 0.323339 | `azmcp_keyvault_certificate_get` | ❌ |
| 20 | 0.317027 | `azmcp_loadtesting_test_get` | ❌ |

---

## Test 184

**Expected Tool:** `azmcp_storage_account_list`  
**Prompt:** List all storage accounts in my subscription including their location and SKU  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.699415 | `azmcp_storage_account_list` | ✅ **EXPECTED** |
| 2 | 0.581393 | `azmcp_storage_table_list` | ❌ |
| 3 | 0.576347 | `azmcp_storage_account_details` | ❌ |
| 4 | 0.540735 | `azmcp_storage_blob_container_list` | ❌ |
| 5 | 0.536909 | `azmcp_cosmos_account_list` | ❌ |
| 6 | 0.501088 | `azmcp_subscription_list` | ❌ |
| 7 | 0.496742 | `azmcp_storage_blob_list` | ❌ |
| 8 | 0.496371 | `azmcp_quota_region_availability_list` | ❌ |
| 9 | 0.493246 | `azmcp_appconfig_account_list` | ❌ |
| 10 | 0.471507 | `azmcp_search_service_list` | ❌ |
| 11 | 0.459591 | `azmcp_functionapp_list` | ❌ |
| 12 | 0.458775 | `azmcp_monitor_workspace_list` | ❌ |
| 13 | 0.454195 | `azmcp_acr_registry_list` | ❌ |
| 14 | 0.448591 | `azmcp_storage_blob_container_details` | ❌ |
| 15 | 0.447139 | `azmcp_aks_cluster_list` | ❌ |
| 16 | 0.432645 | `azmcp_kusto_cluster_list` | ❌ |
| 17 | 0.416387 | `azmcp_group_list` | ❌ |
| 18 | 0.412679 | `azmcp_grafana_list` | ❌ |
| 19 | 0.393518 | `azmcp_cosmos_database_list` | ❌ |
| 20 | 0.389849 | `azmcp_cosmos_database_container_list` | ❌ |

---

## Test 185

**Expected Tool:** `azmcp_storage_account_list`  
**Prompt:** Show me my storage accounts with whether hierarchical namespace (HNS) is enabled  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.574590 | `azmcp_storage_account_list` | ✅ **EXPECTED** |
| 2 | 0.498860 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.450677 | `azmcp_storage_table_list` | ❌ |
| 4 | 0.448981 | `azmcp_storage_account_details` | ❌ |
| 5 | 0.424921 | `azmcp_storage_blob_list` | ❌ |
| 6 | 0.421642 | `azmcp_cosmos_account_list` | ❌ |
| 7 | 0.419265 | `azmcp_storage_blob_container_details` | ❌ |
| 8 | 0.411558 | `azmcp_storage_datalake_file-system_list-paths` | ❌ |
| 9 | 0.379853 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 10 | 0.375553 | `azmcp_cosmos_database_container_list` | ❌ |
| 11 | 0.367906 | `azmcp_cosmos_database_list` | ❌ |
| 12 | 0.366021 | `azmcp_quota_usage_check` | ❌ |
| 13 | 0.362578 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.347173 | `azmcp_appconfig_account_list` | ❌ |
| 15 | 0.335306 | `azmcp_appconfig_kv_show` | ❌ |
| 16 | 0.330185 | `azmcp_aks_cluster_list` | ❌ |
| 17 | 0.327819 | `azmcp_functionapp_list` | ❌ |
| 18 | 0.322188 | `azmcp_keyvault_key_list` | ❌ |
| 19 | 0.312384 | `azmcp_acr_registry_list` | ❌ |
| 20 | 0.299214 | `azmcp_keyvault_secret_list` | ❌ |

---

## Test 186

**Expected Tool:** `azmcp_storage_account_list`  
**Prompt:** Show me the storage accounts in my subscription and include HTTPS-only and public blob access settings  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.610470 | `azmcp_storage_account_list` | ✅ **EXPECTED** |
| 2 | 0.501033 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.499153 | `azmcp_storage_table_list` | ❌ |
| 4 | 0.485850 | `azmcp_storage_blob_list` | ❌ |
| 5 | 0.484101 | `azmcp_storage_account_details` | ❌ |
| 6 | 0.473598 | `azmcp_cosmos_account_list` | ❌ |
| 7 | 0.453933 | `azmcp_subscription_list` | ❌ |
| 8 | 0.431468 | `azmcp_storage_blob_container_details` | ❌ |
| 9 | 0.425048 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 10 | 0.424322 | `azmcp_storage_blob_details` | ❌ |
| 11 | 0.418264 | `azmcp_search_service_list` | ❌ |
| 12 | 0.415080 | `azmcp_appconfig_account_list` | ❌ |
| 13 | 0.383040 | `azmcp_functionapp_list` | ❌ |
| 14 | 0.382069 | `azmcp_aks_cluster_list` | ❌ |
| 15 | 0.376660 | `azmcp_appconfig_kv_show` | ❌ |
| 16 | 0.359998 | `azmcp_cosmos_database_list` | ❌ |
| 17 | 0.359053 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.353273 | `azmcp_cosmos_database_container_list` | ❌ |
| 19 | 0.342616 | `azmcp_bestpractices_get` | ❌ |
| 20 | 0.341127 | `azmcp_kusto_cluster_list` | ❌ |

---

## Test 187

**Expected Tool:** `azmcp_storage_blob_batch_set-tier`  
**Prompt:** Set access tier to Cool for multiple blobs in the container <container> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.678551 | `azmcp_storage_blob_batch_set-tier` | ✅ **EXPECTED** |
| 2 | 0.499700 | `azmcp_storage_blob_list` | ❌ |
| 3 | 0.473825 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.470051 | `azmcp_storage_blob_container_details` | ❌ |
| 5 | 0.422743 | `azmcp_storage_blob_container_create` | ❌ |
| 6 | 0.414750 | `azmcp_storage_blob_details` | ❌ |
| 7 | 0.378380 | `azmcp_cosmos_database_container_list` | ❌ |
| 8 | 0.374820 | `azmcp_storage_account_create` | ❌ |
| 9 | 0.370473 | `azmcp_storage_account_list` | ❌ |
| 10 | 0.352885 | `azmcp_storage_account_details` | ❌ |
| 11 | 0.330453 | `azmcp_storage_blob_upload` | ❌ |
| 12 | 0.305741 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 13 | 0.297254 | `azmcp_appconfig_kv_lock` | ❌ |
| 14 | 0.295717 | `azmcp_appconfig_kv_unlock` | ❌ |
| 15 | 0.295532 | `azmcp_appconfig_kv_set` | ❌ |
| 16 | 0.286940 | `azmcp_acr_registry_repository_list` | ❌ |
| 17 | 0.285276 | `azmcp_cosmos_account_list` | ❌ |
| 18 | 0.271887 | `azmcp_appconfig_kv_show` | ❌ |
| 19 | 0.255608 | `azmcp_extension_az` | ❌ |
| 20 | 0.251602 | `azmcp_deploy_app_logs_get` | ❌ |

---

## Test 188

**Expected Tool:** `azmcp_storage_blob_batch_set-tier`  
**Prompt:** Change the access tier to Archive for blobs file1.txt and file2.txt in the container <container> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.612911 | `azmcp_storage_blob_batch_set-tier` | ✅ **EXPECTED** |
| 2 | 0.447019 | `azmcp_storage_blob_list` | ❌ |
| 3 | 0.435796 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.417870 | `azmcp_storage_blob_container_details` | ❌ |
| 5 | 0.365299 | `azmcp_storage_account_list` | ❌ |
| 6 | 0.360837 | `azmcp_storage_blob_details` | ❌ |
| 7 | 0.354265 | `azmcp_storage_account_create` | ❌ |
| 8 | 0.351903 | `azmcp_cosmos_database_container_list` | ❌ |
| 9 | 0.351609 | `azmcp_storage_blob_container_create` | ❌ |
| 10 | 0.346891 | `azmcp_storage_account_details` | ❌ |
| 11 | 0.341916 | `azmcp_storage_table_list` | ❌ |
| 12 | 0.301192 | `azmcp_acr_registry_repository_list` | ❌ |
| 13 | 0.293976 | `azmcp_appconfig_kv_lock` | ❌ |
| 14 | 0.280332 | `azmcp_cosmos_account_list` | ❌ |
| 15 | 0.279466 | `azmcp_extension_az` | ❌ |
| 16 | 0.276691 | `azmcp_appconfig_kv_unlock` | ❌ |
| 17 | 0.267299 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 18 | 0.253992 | `azmcp_appconfig_kv_set` | ❌ |
| 19 | 0.246935 | `azmcp_deploy_iac_rules_get` | ❌ |
| 20 | 0.245645 | `azmcp_deploy_pipeline_guidance_get` | ❌ |

---

## Test 189

**Expected Tool:** `azmcp_storage_blob_container_create`  
**Prompt:** Create the storage container mycontainer in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.589093 | `azmcp_storage_blob_container_list` | ❌ |
| 2 | 0.523813 | `azmcp_storage_account_create` | ❌ |
| 3 | 0.513051 | `azmcp_storage_blob_container_details` | ❌ |
| 4 | 0.503269 | `azmcp_storage_blob_list` | ❌ |
| 5 | 0.451891 | `azmcp_storage_blob_container_create` | ✅ **EXPECTED** |
| 6 | 0.447784 | `azmcp_cosmos_database_container_list` | ❌ |
| 7 | 0.420577 | `azmcp_storage_account_details` | ❌ |
| 8 | 0.395711 | `azmcp_storage_account_list` | ❌ |
| 9 | 0.387848 | `azmcp_storage_table_list` | ❌ |
| 10 | 0.372547 | `azmcp_storage_blob_details` | ❌ |
| 11 | 0.335039 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 12 | 0.334422 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 13 | 0.326352 | `azmcp_appconfig_kv_set` | ❌ |
| 14 | 0.323203 | `azmcp_keyvault_secret_create` | ❌ |
| 15 | 0.318855 | `azmcp_keyvault_key_create` | ❌ |
| 16 | 0.305656 | `azmcp_keyvault_certificate_create` | ❌ |
| 17 | 0.297912 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 18 | 0.297384 | `azmcp_cosmos_account_list` | ❌ |
| 19 | 0.292093 | `azmcp_acr_registry_repository_list` | ❌ |
| 20 | 0.287499 | `azmcp_appconfig_kv_lock` | ❌ |

---

## Test 190

**Expected Tool:** `azmcp_storage_blob_container_create`  
**Prompt:** Create the container using blob public access in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.584071 | `azmcp_storage_blob_container_create` | ✅ **EXPECTED** |
| 2 | 0.536197 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.535799 | `azmcp_storage_blob_list` | ❌ |
| 4 | 0.514470 | `azmcp_storage_blob_container_details` | ❌ |
| 5 | 0.514344 | `azmcp_storage_account_create` | ❌ |
| 6 | 0.462925 | `azmcp_storage_blob_details` | ❌ |
| 7 | 0.415378 | `azmcp_cosmos_database_container_list` | ❌ |
| 8 | 0.412528 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 9 | 0.384127 | `azmcp_storage_account_list` | ❌ |
| 10 | 0.380296 | `azmcp_storage_account_details` | ❌ |
| 11 | 0.355806 | `azmcp_storage_blob_upload` | ❌ |
| 12 | 0.320173 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 13 | 0.309739 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 14 | 0.285153 | `azmcp_cosmos_account_list` | ❌ |
| 15 | 0.278042 | `azmcp_keyvault_secret_create` | ❌ |
| 16 | 0.275240 | `azmcp_acr_registry_repository_list` | ❌ |
| 17 | 0.275199 | `azmcp_keyvault_key_create` | ❌ |
| 18 | 0.270167 | `azmcp_appconfig_kv_set` | ❌ |
| 19 | 0.269625 | `azmcp_deploy_app_logs_get` | ❌ |
| 20 | 0.267205 | `azmcp_appconfig_kv_lock` | ❌ |

---

## Test 191

**Expected Tool:** `azmcp_storage_blob_container_create`  
**Prompt:** Create a new blob container named documents with container public access in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.512866 | `azmcp_storage_blob_container_create` | ✅ **EXPECTED** |
| 2 | 0.493048 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.492581 | `azmcp_storage_blob_list` | ❌ |
| 4 | 0.482680 | `azmcp_storage_account_create` | ❌ |
| 5 | 0.470054 | `azmcp_storage_blob_container_details` | ❌ |
| 6 | 0.434904 | `azmcp_cosmos_database_container_list` | ❌ |
| 7 | 0.413324 | `azmcp_storage_blob_details` | ❌ |
| 8 | 0.379508 | `azmcp_storage_account_details` | ❌ |
| 9 | 0.378057 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 10 | 0.376175 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 11 | 0.375083 | `azmcp_storage_table_list` | ❌ |
| 12 | 0.360575 | `azmcp_storage_account_list` | ❌ |
| 13 | 0.328630 | `azmcp_cosmos_account_list` | ❌ |
| 14 | 0.322076 | `azmcp_cosmos_database_list` | ❌ |
| 15 | 0.280564 | `azmcp_keyvault_certificate_create` | ❌ |
| 16 | 0.275379 | `azmcp_keyvault_secret_create` | ❌ |
| 17 | 0.269414 | `azmcp_acr_registry_repository_list` | ❌ |
| 18 | 0.266548 | `azmcp_appconfig_kv_set` | ❌ |
| 19 | 0.264875 | `azmcp_keyvault_key_create` | ❌ |
| 20 | 0.262501 | `azmcp_loadtesting_testresource_create` | ❌ |

---

## Test 192

**Expected Tool:** `azmcp_storage_blob_container_details`  
**Prompt:** Show me the properties of the storage container files in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661108 | `azmcp_storage_blob_container_list` | ❌ |
| 2 | 0.660853 | `azmcp_storage_blob_container_details` | ✅ **EXPECTED** |
| 3 | 0.597518 | `azmcp_storage_blob_list` | ❌ |
| 4 | 0.590054 | `azmcp_storage_account_details` | ❌ |
| 5 | 0.557007 | `azmcp_storage_blob_details` | ❌ |
| 6 | 0.518490 | `azmcp_storage_table_list` | ❌ |
| 7 | 0.514436 | `azmcp_storage_account_list` | ❌ |
| 8 | 0.496349 | `azmcp_cosmos_database_container_list` | ❌ |
| 9 | 0.443350 | `azmcp_storage_account_create` | ❌ |
| 10 | 0.419634 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.418341 | `azmcp_appconfig_kv_show` | ❌ |
| 12 | 0.410670 | `azmcp_storage_datalake_file-system_list-paths` | ❌ |
| 13 | 0.387558 | `azmcp_quota_usage_check` | ❌ |
| 14 | 0.365807 | `azmcp_cosmos_database_list` | ❌ |
| 15 | 0.357446 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 16 | 0.355076 | `azmcp_appconfig_kv_list` | ❌ |
| 17 | 0.342947 | `azmcp_deploy_app_logs_get` | ❌ |
| 18 | 0.335755 | `azmcp_appconfig_account_list` | ❌ |
| 19 | 0.334213 | `azmcp_keyvault_key_list` | ❌ |
| 20 | 0.332406 | `azmcp_acr_registry_repository_list` | ❌ |

---

## Test 193

**Expected Tool:** `azmcp_storage_blob_container_list`  
**Prompt:** List all blob containers in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.755653 | `azmcp_storage_blob_container_list` | ✅ **EXPECTED** |
| 2 | 0.732122 | `azmcp_storage_blob_list` | ❌ |
| 3 | 0.613933 | `azmcp_cosmos_database_container_list` | ❌ |
| 4 | 0.556139 | `azmcp_storage_account_list` | ❌ |
| 5 | 0.538446 | `azmcp_storage_blob_container_details` | ❌ |
| 6 | 0.530702 | `azmcp_storage_table_list` | ❌ |
| 7 | 0.514299 | `azmcp_storage_blob_details` | ❌ |
| 8 | 0.471385 | `azmcp_cosmos_account_list` | ❌ |
| 9 | 0.456826 | `azmcp_storage_account_details` | ❌ |
| 10 | 0.453044 | `azmcp_cosmos_database_list` | ❌ |
| 11 | 0.431597 | `azmcp_storage_blob_container_create` | ❌ |
| 12 | 0.409820 | `azmcp_acr_registry_repository_list` | ❌ |
| 13 | 0.405415 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.386764 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 15 | 0.367238 | `azmcp_keyvault_key_list` | ❌ |
| 16 | 0.356400 | `azmcp_acr_registry_list` | ❌ |
| 17 | 0.351868 | `azmcp_keyvault_certificate_list` | ❌ |
| 18 | 0.351399 | `azmcp_keyvault_secret_list` | ❌ |
| 19 | 0.348288 | `azmcp_appconfig_account_list` | ❌ |
| 20 | 0.343863 | `azmcp_cosmos_database_container_item_query` | ❌ |

---

## Test 194

**Expected Tool:** `azmcp_storage_blob_container_list`  
**Prompt:** Show me the blob containers in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.692609 | `azmcp_storage_blob_container_list` | ✅ **EXPECTED** |
| 2 | 0.662587 | `azmcp_storage_blob_list` | ❌ |
| 3 | 0.561214 | `azmcp_cosmos_database_container_list` | ❌ |
| 4 | 0.527221 | `azmcp_storage_blob_details` | ❌ |
| 5 | 0.523580 | `azmcp_storage_blob_container_details` | ❌ |
| 6 | 0.510630 | `azmcp_storage_account_list` | ❌ |
| 7 | 0.496616 | `azmcp_storage_table_list` | ❌ |
| 8 | 0.473405 | `azmcp_storage_account_details` | ❌ |
| 9 | 0.446475 | `azmcp_cosmos_account_list` | ❌ |
| 10 | 0.431168 | `azmcp_storage_blob_container_create` | ❌ |
| 11 | 0.406810 | `azmcp_cosmos_database_list` | ❌ |
| 12 | 0.403088 | `azmcp_storage_account_create` | ❌ |
| 13 | 0.372190 | `azmcp_acr_registry_repository_list` | ❌ |
| 14 | 0.352578 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 15 | 0.340048 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 16 | 0.338712 | `azmcp_acr_registry_list` | ❌ |
| 17 | 0.332977 | `azmcp_appconfig_kv_show` | ❌ |
| 18 | 0.321504 | `azmcp_keyvault_key_list` | ❌ |
| 19 | 0.321478 | `azmcp_appconfig_account_list` | ❌ |
| 20 | 0.315760 | `azmcp_deploy_app_logs_get` | ❌ |

---

## Test 195

**Expected Tool:** `azmcp_storage_blob_details`  
**Prompt:** Show me the properties for blob <blob> in container <container> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.674069 | `azmcp_storage_blob_details` | ✅ **EXPECTED** |
| 2 | 0.647805 | `azmcp_storage_blob_list` | ❌ |
| 3 | 0.632103 | `azmcp_storage_blob_container_details` | ❌ |
| 4 | 0.588530 | `azmcp_storage_blob_container_list` | ❌ |
| 5 | 0.509477 | `azmcp_storage_account_details` | ❌ |
| 6 | 0.477946 | `azmcp_cosmos_database_container_list` | ❌ |
| 7 | 0.463244 | `azmcp_storage_blob_container_create` | ❌ |
| 8 | 0.433683 | `azmcp_storage_account_list` | ❌ |
| 9 | 0.431571 | `azmcp_storage_table_list` | ❌ |
| 10 | 0.386482 | `azmcp_appconfig_kv_show` | ❌ |
| 11 | 0.375774 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 12 | 0.364973 | `azmcp_storage_account_create` | ❌ |
| 13 | 0.359392 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 14 | 0.349565 | `azmcp_cosmos_account_list` | ❌ |
| 15 | 0.323065 | `azmcp_cosmos_database_list` | ❌ |
| 16 | 0.318346 | `azmcp_deploy_app_logs_get` | ❌ |
| 17 | 0.303596 | `azmcp_appconfig_kv_list` | ❌ |
| 18 | 0.287289 | `azmcp_acr_registry_repository_list` | ❌ |
| 19 | 0.283098 | `azmcp_aks_cluster_get` | ❌ |
| 20 | 0.273607 | `azmcp_keyvault_key_list` | ❌ |

---

## Test 196

**Expected Tool:** `azmcp_storage_blob_details`  
**Prompt:** Get the details about blob <blob> in the container <container> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.661140 | `azmcp_storage_blob_container_details` | ❌ |
| 2 | 0.646183 | `azmcp_storage_blob_list` | ❌ |
| 3 | 0.633882 | `azmcp_storage_blob_details` | ✅ **EXPECTED** |
| 4 | 0.578351 | `azmcp_storage_blob_container_list` | ❌ |
| 5 | 0.548693 | `azmcp_storage_account_details` | ❌ |
| 6 | 0.469552 | `azmcp_storage_blob_container_create` | ❌ |
| 7 | 0.453696 | `azmcp_cosmos_database_container_list` | ❌ |
| 8 | 0.434361 | `azmcp_storage_blob_upload` | ❌ |
| 9 | 0.411788 | `azmcp_storage_account_list` | ❌ |
| 10 | 0.402989 | `azmcp_storage_account_create` | ❌ |
| 11 | 0.388809 | `azmcp_storage_table_list` | ❌ |
| 12 | 0.360712 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 13 | 0.359655 | `azmcp_aks_cluster_get` | ❌ |
| 14 | 0.353461 | `azmcp_kusto_cluster_get` | ❌ |
| 15 | 0.348551 | `azmcp_appconfig_kv_show` | ❌ |
| 16 | 0.319525 | `azmcp_keyvault_certificate_get` | ❌ |
| 17 | 0.319393 | `azmcp_deploy_app_logs_get` | ❌ |
| 18 | 0.313425 | `azmcp_cosmos_account_list` | ❌ |
| 19 | 0.297249 | `azmcp_loadtesting_test_get` | ❌ |
| 20 | 0.276513 | `azmcp_marketplace_product_get` | ❌ |

---

## Test 197

**Expected Tool:** `azmcp_storage_blob_list`  
**Prompt:** List all blobs in the blob container <container> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.794699 | `azmcp_storage_blob_list` | ✅ **EXPECTED** |
| 2 | 0.702986 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.579070 | `azmcp_cosmos_database_container_list` | ❌ |
| 4 | 0.553087 | `azmcp_storage_blob_container_details` | ❌ |
| 5 | 0.537677 | `azmcp_storage_blob_details` | ❌ |
| 6 | 0.532154 | `azmcp_storage_account_list` | ❌ |
| 7 | 0.507970 | `azmcp_storage_table_list` | ❌ |
| 8 | 0.454766 | `azmcp_storage_account_details` | ❌ |
| 9 | 0.452160 | `azmcp_cosmos_account_list` | ❌ |
| 10 | 0.434835 | `azmcp_storage_blob_container_create` | ❌ |
| 11 | 0.415853 | `azmcp_cosmos_database_list` | ❌ |
| 12 | 0.403314 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 13 | 0.401192 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.400483 | `azmcp_acr_registry_repository_list` | ❌ |
| 15 | 0.379874 | `azmcp_keyvault_key_list` | ❌ |
| 16 | 0.379099 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 17 | 0.369455 | `azmcp_keyvault_secret_list` | ❌ |
| 18 | 0.359325 | `azmcp_keyvault_certificate_list` | ❌ |
| 19 | 0.331545 | `azmcp_appconfig_kv_list` | ❌ |
| 20 | 0.328761 | `azmcp_appconfig_account_list` | ❌ |

---

## Test 198

**Expected Tool:** `azmcp_storage_blob_list`  
**Prompt:** Show me the blobs in the blob container <container> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.701583 | `azmcp_storage_blob_list` | ✅ **EXPECTED** |
| 2 | 0.639383 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.556629 | `azmcp_storage_blob_details` | ❌ |
| 4 | 0.552927 | `azmcp_storage_blob_container_details` | ❌ |
| 5 | 0.533515 | `azmcp_cosmos_database_container_list` | ❌ |
| 6 | 0.456071 | `azmcp_storage_table_list` | ❌ |
| 7 | 0.451056 | `azmcp_storage_blob_container_create` | ❌ |
| 8 | 0.447308 | `azmcp_storage_account_details` | ❌ |
| 9 | 0.446589 | `azmcp_storage_account_list` | ❌ |
| 10 | 0.395809 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.385242 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 12 | 0.379620 | `azmcp_storage_account_create` | ❌ |
| 13 | 0.376224 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 14 | 0.353799 | `azmcp_cosmos_database_list` | ❌ |
| 15 | 0.345263 | `azmcp_acr_registry_repository_list` | ❌ |
| 16 | 0.342766 | `azmcp_appconfig_kv_show` | ❌ |
| 17 | 0.339846 | `azmcp_deploy_app_logs_get` | ❌ |
| 18 | 0.300295 | `azmcp_acr_registry_list` | ❌ |
| 19 | 0.291502 | `azmcp_keyvault_key_list` | ❌ |
| 20 | 0.290270 | `azmcp_appconfig_kv_list` | ❌ |

---

## Test 199

**Expected Tool:** `azmcp_storage_blob_upload`  
**Prompt:** Upload file <local-file-path> to storage blob <blob> in container <container> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.591091 | `azmcp_storage_blob_upload` | ✅ **EXPECTED** |
| 2 | 0.496856 | `azmcp_storage_blob_list` | ❌ |
| 3 | 0.448020 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.417832 | `azmcp_storage_blob_details` | ❌ |
| 5 | 0.411355 | `azmcp_storage_account_create` | ❌ |
| 6 | 0.404327 | `azmcp_storage_blob_container_details` | ❌ |
| 7 | 0.386894 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 8 | 0.360680 | `azmcp_storage_account_details` | ❌ |
| 9 | 0.333281 | `azmcp_storage_account_list` | ❌ |
| 10 | 0.329014 | `azmcp_storage_blob_container_create` | ❌ |
| 11 | 0.327416 | `azmcp_cosmos_database_container_list` | ❌ |
| 12 | 0.324049 | `azmcp_appconfig_kv_set` | ❌ |
| 13 | 0.294744 | `azmcp_keyvault_certificate_import` | ❌ |
| 14 | 0.284896 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 15 | 0.277659 | `azmcp_appconfig_kv_lock` | ❌ |
| 16 | 0.273638 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 17 | 0.257845 | `azmcp_deploy_app_logs_get` | ❌ |
| 18 | 0.253430 | `azmcp_appconfig_kv_show` | ❌ |
| 19 | 0.239522 | `azmcp_foundry_models_deploy` | ❌ |
| 20 | 0.230237 | `azmcp_appconfig_kv_unlock` | ❌ |

---

## Test 200

**Expected Tool:** `azmcp_storage_blob_upload`  
**Prompt:** Upload the file <local-file-path> overwriting blob <blob> in container <container> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.636370 | `azmcp_storage_blob_upload` | ✅ **EXPECTED** |
| 2 | 0.453867 | `azmcp_storage_blob_list` | ❌ |
| 3 | 0.417803 | `azmcp_storage_blob_details` | ❌ |
| 4 | 0.404408 | `azmcp_storage_blob_container_list` | ❌ |
| 5 | 0.401754 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 6 | 0.376562 | `azmcp_storage_blob_container_details` | ❌ |
| 7 | 0.349288 | `azmcp_storage_account_create` | ❌ |
| 8 | 0.336157 | `azmcp_storage_blob_container_create` | ❌ |
| 9 | 0.326216 | `azmcp_storage_account_details` | ❌ |
| 10 | 0.307892 | `azmcp_cosmos_database_container_list` | ❌ |
| 11 | 0.294655 | `azmcp_keyvault_certificate_import` | ❌ |
| 12 | 0.289470 | `azmcp_appconfig_kv_set` | ❌ |
| 13 | 0.283047 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 14 | 0.280564 | `azmcp_storage_account_list` | ❌ |
| 15 | 0.262630 | `azmcp_appconfig_kv_lock` | ❌ |
| 16 | 0.252940 | `azmcp_deploy_app_logs_get` | ❌ |
| 17 | 0.241713 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 18 | 0.231320 | `azmcp_appconfig_kv_unlock` | ❌ |
| 19 | 0.223013 | `azmcp_appconfig_kv_delete` | ❌ |
| 20 | 0.216845 | `azmcp_acr_registry_repository_list` | ❌ |

---

## Test 201

**Expected Tool:** `azmcp_storage_blob_upload`  
**Prompt:** Overwrite <blob> with <local-file-name> in container <container> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.560916 | `azmcp_storage_blob_upload` | ✅ **EXPECTED** |
| 2 | 0.468674 | `azmcp_storage_blob_list` | ❌ |
| 3 | 0.429583 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.423304 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 5 | 0.420381 | `azmcp_storage_blob_details` | ❌ |
| 6 | 0.396362 | `azmcp_storage_blob_container_details` | ❌ |
| 7 | 0.362282 | `azmcp_storage_account_create` | ❌ |
| 8 | 0.356621 | `azmcp_storage_blob_container_create` | ❌ |
| 9 | 0.337352 | `azmcp_cosmos_database_container_list` | ❌ |
| 10 | 0.317931 | `azmcp_storage_account_details` | ❌ |
| 11 | 0.298414 | `azmcp_appconfig_kv_lock` | ❌ |
| 12 | 0.285263 | `azmcp_workbooks_delete` | ❌ |
| 13 | 0.284871 | `azmcp_appconfig_kv_set` | ❌ |
| 14 | 0.275165 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 15 | 0.258787 | `azmcp_appconfig_kv_unlock` | ❌ |
| 16 | 0.250262 | `azmcp_keyvault_certificate_import` | ❌ |
| 17 | 0.244155 | `azmcp_appconfig_kv_delete` | ❌ |
| 18 | 0.242960 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 19 | 0.227524 | `azmcp_appconfig_kv_show` | ❌ |
| 20 | 0.226492 | `azmcp_deploy_app_logs_get` | ❌ |

---

## Test 202

**Expected Tool:** `azmcp_storage_datalake_directory_create`  
**Prompt:** Create a new directory at the path <directory_path> in Data Lake in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.647078 | `azmcp_storage_datalake_directory_create` | ✅ **EXPECTED** |
| 2 | 0.466296 | `azmcp_storage_account_create` | ❌ |
| 3 | 0.457732 | `azmcp_storage_datalake_file-system_list-paths` | ❌ |
| 4 | 0.348412 | `azmcp_keyvault_secret_create` | ❌ |
| 5 | 0.345266 | `azmcp_storage_blob_container_list` | ❌ |
| 6 | 0.340387 | `azmcp_keyvault_certificate_create` | ❌ |
| 7 | 0.333862 | `azmcp_keyvault_key_create` | ❌ |
| 8 | 0.326548 | `azmcp_storage_account_details` | ❌ |
| 9 | 0.303932 | `azmcp_storage_table_list` | ❌ |
| 10 | 0.302849 | `azmcp_loadtesting_testresource_create` | ❌ |
| 11 | 0.297012 | `azmcp_loadtesting_test_create` | ❌ |
| 12 | 0.290297 | `azmcp_storage_blob_list` | ❌ |
| 13 | 0.286312 | `azmcp_storage_account_list` | ❌ |
| 14 | 0.281674 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 15 | 0.278175 | `azmcp_storage_blob_upload` | ❌ |
| 16 | 0.276608 | `azmcp_appconfig_kv_set` | ❌ |
| 17 | 0.274318 | `azmcp_storage_blob_container_details` | ❌ |
| 18 | 0.240515 | `azmcp_deploy_plan_get` | ❌ |
| 19 | 0.236486 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 20 | 0.231660 | `azmcp_cosmos_database_container_list` | ❌ |

---

## Test 203

**Expected Tool:** `azmcp_storage_datalake_file-system_list-paths`  
**Prompt:** List all paths in the Data Lake file system <file_system> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.749274 | `azmcp_storage_datalake_file-system_list-paths` | ✅ **EXPECTED** |
| 2 | 0.493866 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.481743 | `azmcp_storage_table_list` | ❌ |
| 4 | 0.476495 | `azmcp_storage_blob_list` | ❌ |
| 5 | 0.463545 | `azmcp_storage_account_list` | ❌ |
| 6 | 0.451626 | `azmcp_storage_datalake_directory_create` | ❌ |
| 7 | 0.419381 | `azmcp_cosmos_account_list` | ❌ |
| 8 | 0.413096 | `azmcp_storage_share_file_list` | ❌ |
| 9 | 0.403391 | `azmcp_storage_account_details` | ❌ |
| 10 | 0.402145 | `azmcp_cosmos_database_list` | ❌ |
| 11 | 0.390655 | `azmcp_cosmos_database_container_list` | ❌ |
| 12 | 0.384290 | `azmcp_monitor_table_list` | ❌ |
| 13 | 0.374731 | `azmcp_keyvault_key_list` | ❌ |
| 14 | 0.359718 | `azmcp_storage_blob_container_details` | ❌ |
| 15 | 0.346881 | `azmcp_keyvault_secret_list` | ❌ |
| 16 | 0.344649 | `azmcp_functionapp_list` | ❌ |
| 17 | 0.344309 | `azmcp_keyvault_certificate_list` | ❌ |
| 18 | 0.337104 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 19 | 0.333592 | `azmcp_acr_registry_repository_list` | ❌ |
| 20 | 0.316166 | `azmcp_kusto_database_list` | ❌ |

---

## Test 204

**Expected Tool:** `azmcp_storage_datalake_file-system_list-paths`  
**Prompt:** Show me the paths in the Data Lake file system <file_system> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.712851 | `azmcp_storage_datalake_file-system_list-paths` | ✅ **EXPECTED** |
| 2 | 0.450082 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.433622 | `azmcp_storage_datalake_directory_create` | ❌ |
| 4 | 0.432085 | `azmcp_storage_table_list` | ❌ |
| 5 | 0.418796 | `azmcp_storage_blob_list` | ❌ |
| 6 | 0.407068 | `azmcp_storage_account_list` | ❌ |
| 7 | 0.403474 | `azmcp_storage_account_details` | ❌ |
| 8 | 0.378179 | `azmcp_storage_share_file_list` | ❌ |
| 9 | 0.372453 | `azmcp_cosmos_account_list` | ❌ |
| 10 | 0.363485 | `azmcp_storage_blob_container_details` | ❌ |
| 11 | 0.347625 | `azmcp_cosmos_database_container_list` | ❌ |
| 12 | 0.345916 | `azmcp_cosmos_database_list` | ❌ |
| 13 | 0.344810 | `azmcp_monitor_resource_log_query` | ❌ |
| 14 | 0.304870 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 15 | 0.304578 | `azmcp_keyvault_key_list` | ❌ |
| 16 | 0.304566 | `azmcp_deploy_app_logs_get` | ❌ |
| 17 | 0.289587 | `azmcp_acr_registry_repository_list` | ❌ |
| 18 | 0.288675 | `azmcp_keyvault_secret_list` | ❌ |
| 19 | 0.280591 | `azmcp_functionapp_list` | ❌ |
| 20 | 0.280306 | `azmcp_appconfig_kv_show` | ❌ |

---

## Test 205

**Expected Tool:** `azmcp_storage_datalake_file-system_list-paths`  
**Prompt:** Recursively list all paths in the Data Lake file system <file_system> in the storage account <account> filtered by <filter_path>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.787456 | `azmcp_storage_datalake_file-system_list-paths` | ✅ **EXPECTED** |
| 2 | 0.459075 | `azmcp_storage_share_file_list` | ❌ |
| 3 | 0.422930 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.418352 | `azmcp_storage_datalake_directory_create` | ❌ |
| 5 | 0.413386 | `azmcp_storage_blob_list` | ❌ |
| 6 | 0.394644 | `azmcp_storage_table_list` | ❌ |
| 7 | 0.393544 | `azmcp_storage_account_list` | ❌ |
| 8 | 0.358385 | `azmcp_cosmos_account_list` | ❌ |
| 9 | 0.343389 | `azmcp_cosmos_database_list` | ❌ |
| 10 | 0.341977 | `azmcp_storage_account_details` | ❌ |
| 11 | 0.337381 | `azmcp_cosmos_database_container_list` | ❌ |
| 12 | 0.337260 | `azmcp_monitor_resource_log_query` | ❌ |
| 13 | 0.334045 | `azmcp_acr_registry_repository_list` | ❌ |
| 14 | 0.328857 | `azmcp_functionapp_list` | ❌ |
| 15 | 0.323476 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 16 | 0.319212 | `azmcp_storage_blob_container_details` | ❌ |
| 17 | 0.314548 | `azmcp_keyvault_key_list` | ❌ |
| 18 | 0.300027 | `azmcp_keyvault_certificate_list` | ❌ |
| 19 | 0.295164 | `azmcp_keyvault_secret_list` | ❌ |
| 20 | 0.287501 | `azmcp_deploy_app_logs_get` | ❌ |

---

## Test 206

**Expected Tool:** `azmcp_storage_queue_message_send`  
**Prompt:** Send a message "Hello, World!" to the queue <queue> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.548475 | `azmcp_storage_queue_message_send` | ✅ **EXPECTED** |
| 2 | 0.410972 | `azmcp_storage_table_list` | ❌ |
| 3 | 0.409851 | `azmcp_storage_account_list` | ❌ |
| 4 | 0.407897 | `azmcp_storage_blob_container_list` | ❌ |
| 5 | 0.391050 | `azmcp_storage_account_details` | ❌ |
| 6 | 0.384229 | `azmcp_storage_blob_list` | ❌ |
| 7 | 0.371446 | `azmcp_storage_account_create` | ❌ |
| 8 | 0.344373 | `azmcp_servicebus_queue_details` | ❌ |
| 9 | 0.335989 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 10 | 0.328105 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.325517 | `azmcp_appconfig_kv_set` | ❌ |
| 12 | 0.321736 | `azmcp_appconfig_kv_show` | ❌ |
| 13 | 0.318411 | `azmcp_monitor_resource_log_query` | ❌ |
| 14 | 0.307333 | `azmcp_appconfig_kv_lock` | ❌ |
| 15 | 0.305274 | `azmcp_cosmos_database_container_list` | ❌ |
| 16 | 0.302243 | `azmcp_storage_blob_container_details` | ❌ |
| 17 | 0.285295 | `azmcp_cosmos_database_list` | ❌ |
| 18 | 0.276137 | `azmcp_appconfig_kv_unlock` | ❌ |
| 19 | 0.258161 | `azmcp_appconfig_account_list` | ❌ |
| 20 | 0.252408 | `azmcp_kusto_query` | ❌ |

---

## Test 207

**Expected Tool:** `azmcp_storage_queue_message_send`  
**Prompt:** Send a message with TTL of 3600 seconds to the queue <queue> in storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.632250 | `azmcp_storage_queue_message_send` | ✅ **EXPECTED** |
| 2 | 0.383344 | `azmcp_storage_table_list` | ❌ |
| 3 | 0.378460 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.373050 | `azmcp_servicebus_queue_details` | ❌ |
| 5 | 0.364909 | `azmcp_storage_account_list` | ❌ |
| 6 | 0.363729 | `azmcp_storage_account_details` | ❌ |
| 7 | 0.360708 | `azmcp_storage_account_create` | ❌ |
| 8 | 0.347732 | `azmcp_storage_blob_list` | ❌ |
| 9 | 0.345318 | `azmcp_monitor_resource_log_query` | ❌ |
| 10 | 0.334472 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 11 | 0.315019 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 12 | 0.310258 | `azmcp_appconfig_kv_set` | ❌ |
| 13 | 0.294922 | `azmcp_appconfig_kv_lock` | ❌ |
| 14 | 0.282610 | `azmcp_appconfig_kv_show` | ❌ |
| 15 | 0.277782 | `azmcp_cosmos_account_list` | ❌ |
| 16 | 0.273178 | `azmcp_cosmos_database_container_list` | ❌ |
| 17 | 0.261862 | `azmcp_appconfig_kv_unlock` | ❌ |
| 18 | 0.257294 | `azmcp_keyvault_secret_create` | ❌ |
| 19 | 0.239636 | `azmcp_kusto_query` | ❌ |
| 20 | 0.237165 | `azmcp_keyvault_key_create` | ❌ |

---

## Test 208

**Expected Tool:** `azmcp_storage_queue_message_send`  
**Prompt:** Add a message to the queue <queue> in storage account <account> with visibility timeout of 30 seconds  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.589375 | `azmcp_storage_queue_message_send` | ✅ **EXPECTED** |
| 2 | 0.363657 | `azmcp_storage_account_create` | ❌ |
| 3 | 0.360404 | `azmcp_servicebus_queue_details` | ❌ |
| 4 | 0.338557 | `azmcp_storage_blob_container_list` | ❌ |
| 5 | 0.325195 | `azmcp_appconfig_kv_set` | ❌ |
| 6 | 0.322451 | `azmcp_storage_table_list` | ❌ |
| 7 | 0.314983 | `azmcp_storage_account_details` | ❌ |
| 8 | 0.312553 | `azmcp_storage_account_list` | ❌ |
| 9 | 0.306006 | `azmcp_storage_blob_list` | ❌ |
| 10 | 0.297757 | `azmcp_storage_blob_container_details` | ❌ |
| 11 | 0.289447 | `azmcp_appconfig_kv_lock` | ❌ |
| 12 | 0.274267 | `azmcp_keyvault_secret_create` | ❌ |
| 13 | 0.273466 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 14 | 0.270509 | `azmcp_appconfig_kv_show` | ❌ |
| 15 | 0.261945 | `azmcp_cosmos_database_container_item_query` | ❌ |
| 16 | 0.247025 | `azmcp_appconfig_kv_unlock` | ❌ |
| 17 | 0.246970 | `azmcp_cosmos_database_container_list` | ❌ |
| 18 | 0.245721 | `azmcp_cosmos_account_list` | ❌ |
| 19 | 0.241325 | `azmcp_keyvault_key_create` | ❌ |
| 20 | 0.218512 | `azmcp_keyvault_certificate_create` | ❌ |

---

## Test 209

**Expected Tool:** `azmcp_storage_share_file_list`  
**Prompt:** List all files and directories in the File Share <share> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.634059 | `azmcp_storage_share_file_list` | ✅ **EXPECTED** |
| 2 | 0.576726 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.539799 | `azmcp_storage_table_list` | ❌ |
| 4 | 0.527513 | `azmcp_storage_blob_list` | ❌ |
| 5 | 0.520636 | `azmcp_storage_account_list` | ❌ |
| 6 | 0.500715 | `azmcp_storage_datalake_file-system_list-paths` | ❌ |
| 7 | 0.474102 | `azmcp_storage_account_details` | ❌ |
| 8 | 0.433432 | `azmcp_cosmos_account_list` | ❌ |
| 9 | 0.425767 | `azmcp_storage_blob_container_details` | ❌ |
| 10 | 0.416508 | `azmcp_cosmos_database_container_list` | ❌ |
| 11 | 0.397910 | `azmcp_cosmos_database_list` | ❌ |
| 12 | 0.390184 | `azmcp_keyvault_key_list` | ❌ |
| 13 | 0.386494 | `azmcp_storage_account_create` | ❌ |
| 14 | 0.385248 | `azmcp_keyvault_secret_list` | ❌ |
| 15 | 0.372976 | `azmcp_keyvault_certificate_list` | ❌ |
| 16 | 0.372840 | `azmcp_acr_registry_repository_list` | ❌ |
| 17 | 0.366437 | `azmcp_subscription_list` | ❌ |
| 18 | 0.353512 | `azmcp_appconfig_account_list` | ❌ |
| 19 | 0.341644 | `azmcp_functionapp_list` | ❌ |
| 20 | 0.337893 | `azmcp_datadog_monitoredresources_list` | ❌ |

---

## Test 210

**Expected Tool:** `azmcp_storage_share_file_list`  
**Prompt:** Show me the files in the File Share <share> directory <directory_path> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.547481 | `azmcp_storage_share_file_list` | ✅ **EXPECTED** |
| 2 | 0.497389 | `azmcp_storage_datalake_file-system_list-paths` | ❌ |
| 3 | 0.479129 | `azmcp_storage_blob_container_list` | ❌ |
| 4 | 0.452271 | `azmcp_storage_table_list` | ❌ |
| 5 | 0.437409 | `azmcp_storage_blob_list` | ❌ |
| 6 | 0.428831 | `azmcp_storage_account_list` | ❌ |
| 7 | 0.418034 | `azmcp_storage_account_details` | ❌ |
| 8 | 0.381807 | `azmcp_storage_blob_container_details` | ❌ |
| 9 | 0.380180 | `azmcp_storage_datalake_directory_create` | ❌ |
| 10 | 0.351906 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.342269 | `azmcp_storage_account_create` | ❌ |
| 12 | 0.341352 | `azmcp_cosmos_database_container_list` | ❌ |
| 13 | 0.328388 | `azmcp_appconfig_kv_show` | ❌ |
| 14 | 0.320423 | `azmcp_keyvault_secret_list` | ❌ |
| 15 | 0.317899 | `azmcp_cosmos_database_list` | ❌ |
| 16 | 0.315366 | `azmcp_keyvault_key_list` | ❌ |
| 17 | 0.304034 | `azmcp_appconfig_account_list` | ❌ |
| 18 | 0.303900 | `azmcp_acr_registry_repository_list` | ❌ |
| 19 | 0.301155 | `azmcp_keyvault_certificate_list` | ❌ |
| 20 | 0.296854 | `azmcp_cosmos_database_container_item_query` | ❌ |

---

## Test 211

**Expected Tool:** `azmcp_storage_share_file_list`  
**Prompt:** List files with prefix 'report' in the File Share <share> in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.599458 | `azmcp_storage_share_file_list` | ✅ **EXPECTED** |
| 2 | 0.466117 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.449627 | `azmcp_storage_datalake_file-system_list-paths` | ❌ |
| 4 | 0.449412 | `azmcp_storage_table_list` | ❌ |
| 5 | 0.441187 | `azmcp_storage_account_list` | ❌ |
| 6 | 0.427859 | `azmcp_storage_blob_list` | ❌ |
| 7 | 0.423868 | `azmcp_extension_azqr` | ❌ |
| 8 | 0.390975 | `azmcp_storage_account_details` | ❌ |
| 9 | 0.378092 | `azmcp_cosmos_account_list` | ❌ |
| 10 | 0.374357 | `azmcp_monitor_resource_log_query` | ❌ |
| 11 | 0.369171 | `azmcp_acr_registry_repository_list` | ❌ |
| 12 | 0.364292 | `azmcp_workbooks_list` | ❌ |
| 13 | 0.362241 | `azmcp_storage_blob_container_details` | ❌ |
| 14 | 0.339261 | `azmcp_cosmos_database_list` | ❌ |
| 15 | 0.336352 | `azmcp_cosmos_database_container_list` | ❌ |
| 16 | 0.333170 | `azmcp_keyvault_certificate_list` | ❌ |
| 17 | 0.319853 | `azmcp_keyvault_secret_list` | ❌ |
| 18 | 0.319475 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 19 | 0.315033 | `azmcp_keyvault_key_list` | ❌ |
| 20 | 0.313394 | `azmcp_functionapp_list` | ❌ |

---

## Test 212

**Expected Tool:** `azmcp_storage_table_list`  
**Prompt:** List all tables in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.787243 | `azmcp_storage_table_list` | ✅ **EXPECTED** |
| 2 | 0.627249 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.574921 | `azmcp_monitor_table_list` | ❌ |
| 4 | 0.566807 | `azmcp_storage_account_list` | ❌ |
| 5 | 0.559324 | `azmcp_storage_blob_list` | ❌ |
| 6 | 0.514042 | `azmcp_cosmos_database_list` | ❌ |
| 7 | 0.503638 | `azmcp_cosmos_database_container_list` | ❌ |
| 8 | 0.498181 | `azmcp_postgres_table_list` | ❌ |
| 9 | 0.497572 | `azmcp_monitor_table_type_list` | ❌ |
| 10 | 0.491995 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.486049 | `azmcp_kusto_table_list` | ❌ |
| 12 | 0.481347 | `azmcp_storage_account_details` | ❌ |
| 13 | 0.446216 | `azmcp_storage_blob_container_details` | ❌ |
| 14 | 0.404701 | `azmcp_kusto_database_list` | ❌ |
| 15 | 0.398367 | `azmcp_storage_account_create` | ❌ |
| 16 | 0.393528 | `azmcp_keyvault_key_list` | ❌ |
| 17 | 0.362914 | `azmcp_kusto_table_schema` | ❌ |
| 18 | 0.361090 | `azmcp_keyvault_certificate_list` | ❌ |
| 19 | 0.358239 | `azmcp_acr_registry_repository_list` | ❌ |
| 20 | 0.356593 | `azmcp_appconfig_account_list` | ❌ |

---

## Test 213

**Expected Tool:** `azmcp_storage_table_list`  
**Prompt:** Show me the tables in the storage account <account>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.738095 | `azmcp_storage_table_list` | ✅ **EXPECTED** |
| 2 | 0.598419 | `azmcp_storage_blob_container_list` | ❌ |
| 3 | 0.532313 | `azmcp_storage_account_list` | ❌ |
| 4 | 0.522935 | `azmcp_storage_blob_list` | ❌ |
| 5 | 0.521785 | `azmcp_monitor_table_list` | ❌ |
| 6 | 0.495640 | `azmcp_storage_account_details` | ❌ |
| 7 | 0.480680 | `azmcp_cosmos_database_container_list` | ❌ |
| 8 | 0.479470 | `azmcp_monitor_table_type_list` | ❌ |
| 9 | 0.470860 | `azmcp_cosmos_database_list` | ❌ |
| 10 | 0.462051 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.454835 | `azmcp_storage_blob_container_details` | ❌ |
| 12 | 0.447645 | `azmcp_kusto_table_list` | ❌ |
| 13 | 0.423663 | `azmcp_postgres_table_list` | ❌ |
| 14 | 0.403799 | `azmcp_monitor_resource_log_query` | ❌ |
| 15 | 0.380764 | `azmcp_kusto_table_schema` | ❌ |
| 16 | 0.368050 | `azmcp_keyvault_key_list` | ❌ |
| 17 | 0.365922 | `azmcp_kusto_database_list` | ❌ |
| 18 | 0.362253 | `azmcp_kusto_sample` | ❌ |
| 19 | 0.355013 | `azmcp_appconfig_account_list` | ❌ |
| 20 | 0.353853 | `azmcp_appconfig_kv_show` | ❌ |

---

## Test 214

**Expected Tool:** `azmcp_subscription_list`  
**Prompt:** List all subscriptions for my account  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.576055 | `azmcp_subscription_list` | ✅ **EXPECTED** |
| 2 | 0.512964 | `azmcp_cosmos_account_list` | ❌ |
| 3 | 0.473852 | `azmcp_redis_cache_list` | ❌ |
| 4 | 0.471653 | `azmcp_postgres_server_list` | ❌ |
| 5 | 0.470819 | `azmcp_search_service_list` | ❌ |
| 6 | 0.463732 | `azmcp_storage_account_list` | ❌ |
| 7 | 0.450973 | `azmcp_redis_cluster_list` | ❌ |
| 8 | 0.445724 | `azmcp_grafana_list` | ❌ |
| 9 | 0.436338 | `azmcp_storage_table_list` | ❌ |
| 10 | 0.431337 | `azmcp_kusto_cluster_list` | ❌ |
| 11 | 0.430280 | `azmcp_group_list` | ❌ |
| 12 | 0.406935 | `azmcp_appconfig_account_list` | ❌ |
| 13 | 0.394076 | `azmcp_aks_cluster_list` | ❌ |
| 14 | 0.393372 | `azmcp_functionapp_list` | ❌ |
| 15 | 0.388766 | `azmcp_monitor_workspace_list` | ❌ |
| 16 | 0.366860 | `azmcp_loadtesting_testresource_list` | ❌ |
| 17 | 0.364799 | `azmcp_storage_blob_container_list` | ❌ |
| 18 | 0.354245 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 19 | 0.348524 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 20 | 0.345154 | `azmcp_cosmos_database_list` | ❌ |

---

## Test 215

**Expected Tool:** `azmcp_subscription_list`  
**Prompt:** Show me my subscriptions  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.405723 | `azmcp_subscription_list` | ✅ **EXPECTED** |
| 2 | 0.381238 | `azmcp_postgres_server_list` | ❌ |
| 3 | 0.351864 | `azmcp_grafana_list` | ❌ |
| 4 | 0.350951 | `azmcp_redis_cache_list` | ❌ |
| 5 | 0.344860 | `azmcp_search_service_list` | ❌ |
| 6 | 0.341813 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.315604 | `azmcp_kusto_cluster_list` | ❌ |
| 8 | 0.308874 | `azmcp_appconfig_account_list` | ❌ |
| 9 | 0.303528 | `azmcp_cosmos_account_list` | ❌ |
| 10 | 0.297209 | `azmcp_group_list` | ❌ |
| 11 | 0.296357 | `azmcp_monitor_workspace_list` | ❌ |
| 12 | 0.285434 | `azmcp_servicebus_topic_subscription_details` | ❌ |
| 13 | 0.275417 | `azmcp_loadtesting_testresource_list` | ❌ |
| 14 | 0.274552 | `azmcp_aks_cluster_list` | ❌ |
| 15 | 0.274310 | `azmcp_storage_account_list` | ❌ |
| 16 | 0.272704 | `azmcp_marketplace_product_get` | ❌ |
| 17 | 0.258047 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 18 | 0.256330 | `azmcp_storage_table_list` | ❌ |
| 19 | 0.254901 | `azmcp_functionapp_list` | ❌ |
| 20 | 0.244501 | `azmcp_resourcehealth_availability-status_list` | ❌ |

---

## Test 216

**Expected Tool:** `azmcp_subscription_list`  
**Prompt:** What is my current subscription?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.319958 | `azmcp_subscription_list` | ✅ **EXPECTED** |
| 2 | 0.298284 | `azmcp_marketplace_product_get` | ❌ |
| 3 | 0.286711 | `azmcp_redis_cache_list` | ❌ |
| 4 | 0.285063 | `azmcp_search_service_list` | ❌ |
| 5 | 0.282645 | `azmcp_grafana_list` | ❌ |
| 6 | 0.279702 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.278798 | `azmcp_postgres_server_list` | ❌ |
| 8 | 0.256358 | `azmcp_kusto_cluster_list` | ❌ |
| 9 | 0.254815 | `azmcp_servicebus_topic_subscription_details` | ❌ |
| 10 | 0.252504 | `azmcp_loadtesting_testresource_list` | ❌ |
| 11 | 0.233227 | `azmcp_monitor_workspace_list` | ❌ |
| 12 | 0.230571 | `azmcp_cosmos_account_list` | ❌ |
| 13 | 0.230324 | `azmcp_kusto_cluster_get` | ❌ |
| 14 | 0.227020 | `azmcp_quota_region_availability_list` | ❌ |
| 15 | 0.222799 | `azmcp_appconfig_account_list` | ❌ |
| 16 | 0.217279 | `azmcp_aks_cluster_list` | ❌ |
| 17 | 0.216460 | `azmcp_group_list` | ❌ |
| 18 | 0.211120 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 19 | 0.210567 | `azmcp_storage_account_list` | ❌ |
| 20 | 0.198778 | `azmcp_functionapp_list` | ❌ |

---

## Test 217

**Expected Tool:** `azmcp_subscription_list`  
**Prompt:** What subscriptions do I have?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.403229 | `azmcp_subscription_list` | ✅ **EXPECTED** |
| 2 | 0.354504 | `azmcp_redis_cache_list` | ❌ |
| 3 | 0.342318 | `azmcp_redis_cluster_list` | ❌ |
| 4 | 0.340339 | `azmcp_grafana_list` | ❌ |
| 5 | 0.336798 | `azmcp_postgres_server_list` | ❌ |
| 6 | 0.332531 | `azmcp_search_service_list` | ❌ |
| 7 | 0.304965 | `azmcp_kusto_cluster_list` | ❌ |
| 8 | 0.300478 | `azmcp_servicebus_topic_subscription_details` | ❌ |
| 9 | 0.294198 | `azmcp_monitor_workspace_list` | ❌ |
| 10 | 0.291826 | `azmcp_cosmos_account_list` | ❌ |
| 11 | 0.285695 | `azmcp_marketplace_product_get` | ❌ |
| 12 | 0.282326 | `azmcp_loadtesting_testresource_list` | ❌ |
| 13 | 0.281294 | `azmcp_appconfig_account_list` | ❌ |
| 14 | 0.269869 | `azmcp_group_list` | ❌ |
| 15 | 0.258410 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 16 | 0.257366 | `azmcp_aks_cluster_list` | ❌ |
| 17 | 0.257227 | `azmcp_functionapp_list` | ❌ |
| 18 | 0.254519 | `azmcp_storage_account_list` | ❌ |
| 19 | 0.236600 | `azmcp_quota_region_availability_list` | ❌ |
| 20 | 0.228634 | `azmcp_datadog_monitoredresources_list` | ❌ |

---

## Test 218

**Expected Tool:** `azmcp_azureterraformbestpractices_get`  
**Prompt:** Fetch the Azure Terraform best practices  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.719967 | `azmcp_azureterraformbestpractices_get` | ✅ **EXPECTED** |
| 2 | 0.658343 | `azmcp_bestpractices_get` | ❌ |
| 3 | 0.625270 | `azmcp_deploy_iac_rules_get` | ❌ |
| 4 | 0.482936 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 5 | 0.466199 | `azmcp_deploy_plan_get` | ❌ |
| 6 | 0.459270 | `azmcp_extension_az` | ❌ |
| 7 | 0.389080 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 8 | 0.386480 | `azmcp_quota_usage_check` | ❌ |
| 9 | 0.372596 | `azmcp_deploy_app_logs_get` | ❌ |
| 10 | 0.354838 | `azmcp_bicepschema_get` | ❌ |
| 11 | 0.354086 | `azmcp_quota_region_availability_list` | ❌ |
| 12 | 0.332182 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 13 | 0.331791 | `azmcp_extension_azd` | ❌ |
| 14 | 0.306705 | `azmcp_storage_account_details` | ❌ |
| 15 | 0.303849 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 16 | 0.291466 | `azmcp_subscription_list` | ❌ |
| 17 | 0.279313 | `azmcp_monitor_metrics_query` | ❌ |
| 18 | 0.272676 | `azmcp_storage_blob_details` | ❌ |
| 19 | 0.269205 | `azmcp_workbooks_show` | ❌ |
| 20 | 0.267814 | `azmcp_redis_cluster_list` | ❌ |

---

## Test 219

**Expected Tool:** `azmcp_azureterraformbestpractices_get`  
**Prompt:** Show me the Azure Terraform best practices and generate code sample to get a secret from Azure Key Vault  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.596382 | `azmcp_azureterraformbestpractices_get` | ✅ **EXPECTED** |
| 2 | 0.551618 | `azmcp_bestpractices_get` | ❌ |
| 3 | 0.510004 | `azmcp_deploy_iac_rules_get` | ❌ |
| 4 | 0.444297 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 5 | 0.439717 | `azmcp_keyvault_secret_list` | ❌ |
| 6 | 0.439555 | `azmcp_keyvault_secret_create` | ❌ |
| 7 | 0.428888 | `azmcp_keyvault_certificate_get` | ❌ |
| 8 | 0.406432 | `azmcp_extension_az` | ❌ |
| 9 | 0.389551 | `azmcp_keyvault_key_list` | ❌ |
| 10 | 0.381423 | `azmcp_keyvault_certificate_create` | ❌ |
| 11 | 0.304912 | `azmcp_quota_usage_check` | ❌ |
| 12 | 0.300776 | `azmcp_quota_region_availability_list` | ❌ |
| 13 | 0.290398 | `azmcp_storage_account_details` | ❌ |
| 14 | 0.274806 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 15 | 0.274538 | `azmcp_subscription_list` | ❌ |
| 16 | 0.264788 | `azmcp_storage_account_create` | ❌ |
| 17 | 0.264572 | `azmcp_monitor_resource_log_query` | ❌ |
| 18 | 0.254651 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 19 | 0.253309 | `azmcp_sql_db_show` | ❌ |
| 20 | 0.251371 | `azmcp_monitor_metrics_query` | ❌ |

---

## Test 220

**Expected Tool:** `azmcp_virtualdesktop_hostpool_list`  
**Prompt:** List all host pools in my subscription  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.713689 | `azmcp_virtualdesktop_hostpool_list` | ✅ **EXPECTED** |
| 2 | 0.658080 | `azmcp_virtualdesktop_hostpool_sessionhost_list` | ❌ |
| 3 | 0.566615 | `azmcp_kusto_cluster_list` | ❌ |
| 4 | 0.557529 | `azmcp_search_service_list` | ❌ |
| 5 | 0.536542 | `azmcp_redis_cluster_list` | ❌ |
| 6 | 0.535739 | `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list` | ❌ |
| 7 | 0.528358 | `azmcp_sql_elastic-pool_list` | ❌ |
| 8 | 0.527948 | `azmcp_postgres_server_list` | ❌ |
| 9 | 0.524454 | `azmcp_aks_cluster_list` | ❌ |
| 10 | 0.506608 | `azmcp_redis_cache_list` | ❌ |
| 11 | 0.505116 | `azmcp_subscription_list` | ❌ |
| 12 | 0.496297 | `azmcp_cosmos_account_list` | ❌ |
| 13 | 0.495490 | `azmcp_grafana_list` | ❌ |
| 14 | 0.492508 | `azmcp_monitor_workspace_list` | ❌ |
| 15 | 0.476718 | `azmcp_group_list` | ❌ |
| 16 | 0.474660 | `azmcp_functionapp_list` | ❌ |
| 17 | 0.460388 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.459250 | `azmcp_appconfig_account_list` | ❌ |
| 19 | 0.456279 | `azmcp_kusto_database_list` | ❌ |
| 20 | 0.431475 | `azmcp_datadog_monitoredresources_list` | ❌ |

---

## Test 221

**Expected Tool:** `azmcp_virtualdesktop_hostpool_sessionhost_list`  
**Prompt:** List all session hosts in host pool <hostpool_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.736121 | `azmcp_virtualdesktop_hostpool_sessionhost_list` | ✅ **EXPECTED** |
| 2 | 0.714469 | `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list` | ❌ |
| 3 | 0.590273 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 4 | 0.397910 | `azmcp_sql_elastic-pool_list` | ❌ |
| 5 | 0.364696 | `azmcp_postgres_server_list` | ❌ |
| 6 | 0.337530 | `azmcp_redis_cluster_list` | ❌ |
| 7 | 0.335308 | `azmcp_monitor_workspace_list` | ❌ |
| 8 | 0.333517 | `azmcp_kusto_cluster_list` | ❌ |
| 9 | 0.333087 | `azmcp_keyvault_secret_list` | ❌ |
| 10 | 0.330208 | `azmcp_aks_cluster_list` | ❌ |
| 11 | 0.329287 | `azmcp_search_service_list` | ❌ |
| 12 | 0.328607 | `azmcp_keyvault_key_list` | ❌ |
| 13 | 0.321841 | `azmcp_subscription_list` | ❌ |
| 14 | 0.319942 | `azmcp_storage_account_list` | ❌ |
| 15 | 0.312101 | `azmcp_keyvault_certificate_list` | ❌ |
| 16 | 0.311262 | `azmcp_grafana_list` | ❌ |
| 17 | 0.302706 | `azmcp_cosmos_account_list` | ❌ |
| 18 | 0.291590 | `azmcp_loadtesting_testrun_list` | ❌ |
| 19 | 0.291489 | `azmcp_appconfig_account_list` | ❌ |
| 20 | 0.289854 | `azmcp_functionapp_list` | ❌ |

---

## Test 222

**Expected Tool:** `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list`  
**Prompt:** List all user sessions on session host <sessionhost_name> in host pool <hostpool_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.812659 | `azmcp_virtualdesktop_hostpool_sessionhost_usersession-list` | ✅ **EXPECTED** |
| 2 | 0.666731 | `azmcp_virtualdesktop_hostpool_sessionhost_list` | ❌ |
| 3 | 0.513570 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 4 | 0.336415 | `azmcp_monitor_workspace_list` | ❌ |
| 5 | 0.329666 | `azmcp_sql_elastic-pool_list` | ❌ |
| 6 | 0.324603 | `azmcp_subscription_list` | ❌ |
| 7 | 0.316500 | `azmcp_loadtesting_testrun_list` | ❌ |
| 8 | 0.316295 | `azmcp_postgres_server_list` | ❌ |
| 9 | 0.305300 | `azmcp_monitor_table_list` | ❌ |
| 10 | 0.304432 | `azmcp_aks_cluster_list` | ❌ |
| 11 | 0.304414 | `azmcp_workbooks_list` | ❌ |
| 12 | 0.299917 | `azmcp_keyvault_secret_list` | ❌ |
| 13 | 0.297624 | `azmcp_search_service_list` | ❌ |
| 14 | 0.295899 | `azmcp_grafana_list` | ❌ |
| 15 | 0.278813 | `azmcp_cosmos_account_list` | ❌ |
| 16 | 0.278222 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 17 | 0.276413 | `azmcp_keyvault_key_list` | ❌ |
| 18 | 0.276391 | `azmcp_kusto_cluster_list` | ❌ |
| 19 | 0.272231 | `azmcp_loadtesting_testrun_get` | ❌ |
| 20 | 0.270990 | `azmcp_keyvault_certificate_list` | ❌ |

---

## Test 223

**Expected Tool:** `azmcp_workbooks_create`  
**Prompt:** Create a new workbook named <workbook_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.552212 | `azmcp_workbooks_create` | ✅ **EXPECTED** |
| 2 | 0.433162 | `azmcp_workbooks_update` | ❌ |
| 3 | 0.365579 | `azmcp_workbooks_delete` | ❌ |
| 4 | 0.361394 | `azmcp_workbooks_show` | ❌ |
| 5 | 0.328113 | `azmcp_workbooks_list` | ❌ |
| 6 | 0.239806 | `azmcp_keyvault_secret_create` | ❌ |
| 7 | 0.217264 | `azmcp_keyvault_key_create` | ❌ |
| 8 | 0.214762 | `azmcp_keyvault_certificate_create` | ❌ |
| 9 | 0.195157 | `azmcp_storage_account_create` | ❌ |
| 10 | 0.188137 | `azmcp_loadtesting_testresource_create` | ❌ |
| 11 | 0.172751 | `azmcp_monitor_table_list` | ❌ |
| 12 | 0.169440 | `azmcp_grafana_list` | ❌ |
| 13 | 0.148897 | `azmcp_loadtesting_test_create` | ❌ |
| 14 | 0.147350 | `azmcp_monitor_workspace_list` | ❌ |
| 15 | 0.142879 | `azmcp_storage_datalake_directory_create` | ❌ |
| 16 | 0.138518 | `azmcp_monitor_table_type_list` | ❌ |
| 17 | 0.130524 | `azmcp_loadtesting_testrun_create` | ❌ |
| 18 | 0.130339 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 19 | 0.116803 | `azmcp_loadtesting_testrun_update` | ❌ |
| 20 | 0.113882 | `azmcp_deploy_plan_get` | ❌ |

---

## Test 224

**Expected Tool:** `azmcp_workbooks_delete`  
**Prompt:** Delete the workbook with resource ID <workbook_resource_id>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.624673 | `azmcp_workbooks_delete` | ✅ **EXPECTED** |
| 2 | 0.518729 | `azmcp_workbooks_show` | ❌ |
| 3 | 0.432454 | `azmcp_workbooks_create` | ❌ |
| 4 | 0.425569 | `azmcp_workbooks_list` | ❌ |
| 5 | 0.390355 | `azmcp_workbooks_update` | ❌ |
| 6 | 0.273939 | `azmcp_grafana_list` | ❌ |
| 7 | 0.198585 | `azmcp_appconfig_kv_delete` | ❌ |
| 8 | 0.193231 | `azmcp_monitor_resource_log_query` | ❌ |
| 9 | 0.186665 | `azmcp_quota_region_availability_list` | ❌ |
| 10 | 0.186572 | `azmcp_monitor_workspace_log_query` | ❌ |
| 11 | 0.157159 | `azmcp_monitor_workspace_list` | ❌ |
| 12 | 0.155100 | `azmcp_monitor_metrics_query` | ❌ |
| 13 | 0.148882 | `azmcp_extension_azqr` | ❌ |
| 14 | 0.145141 | `azmcp_loadtesting_testresource_list` | ❌ |
| 15 | 0.134979 | `azmcp_loadtesting_testrun_update` | ❌ |
| 16 | 0.132504 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 17 | 0.131813 | `azmcp_group_list` | ❌ |
| 18 | 0.126273 | `azmcp_monitor_healthmodels_entity_gethealth` | ❌ |
| 19 | 0.122644 | `azmcp_marketplace_product_get` | ❌ |
| 20 | 0.120291 | `azmcp_loadtesting_test_get` | ❌ |

---

## Test 225

**Expected Tool:** `azmcp_workbooks_list`  
**Prompt:** List all workbooks in my resource group <resource_group_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.772431 | `azmcp_workbooks_list` | ✅ **EXPECTED** |
| 2 | 0.562485 | `azmcp_workbooks_create` | ❌ |
| 3 | 0.532656 | `azmcp_workbooks_show` | ❌ |
| 4 | 0.516739 | `azmcp_grafana_list` | ❌ |
| 5 | 0.490216 | `azmcp_workbooks_delete` | ❌ |
| 6 | 0.488600 | `azmcp_group_list` | ❌ |
| 7 | 0.459976 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 8 | 0.454161 | `azmcp_monitor_workspace_list` | ❌ |
| 9 | 0.439945 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 10 | 0.416566 | `azmcp_monitor_table_list` | ❌ |
| 11 | 0.413409 | `azmcp_sql_db_list` | ❌ |
| 12 | 0.405963 | `azmcp_loadtesting_testresource_list` | ❌ |
| 13 | 0.405064 | `azmcp_redis_cluster_list` | ❌ |
| 14 | 0.399758 | `azmcp_acr_registry_repository_list` | ❌ |
| 15 | 0.387277 | `azmcp_virtualdesktop_hostpool_list` | ❌ |
| 16 | 0.366244 | `azmcp_functionapp_list` | ❌ |
| 17 | 0.362740 | `azmcp_acr_registry_list` | ❌ |
| 18 | 0.352940 | `azmcp_cosmos_database_list` | ❌ |
| 19 | 0.349674 | `azmcp_cosmos_account_list` | ❌ |
| 20 | 0.344606 | `azmcp_monitor_metrics_definitions` | ❌ |

---

## Test 226

**Expected Tool:** `azmcp_workbooks_list`  
**Prompt:** What workbooks do I have in resource group <resource_group_name>?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.708612 | `azmcp_workbooks_list` | ✅ **EXPECTED** |
| 2 | 0.570259 | `azmcp_workbooks_create` | ❌ |
| 3 | 0.540066 | `azmcp_workbooks_show` | ❌ |
| 4 | 0.488377 | `azmcp_workbooks_delete` | ❌ |
| 5 | 0.472378 | `azmcp_grafana_list` | ❌ |
| 6 | 0.428000 | `azmcp_monitor_workspace_list` | ❌ |
| 7 | 0.425426 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 8 | 0.422785 | `azmcp_resourcehealth_availability-status_list` | ❌ |
| 9 | 0.421646 | `azmcp_group_list` | ❌ |
| 10 | 0.392371 | `azmcp_loadtesting_testresource_list` | ❌ |
| 11 | 0.371128 | `azmcp_redis_cluster_list` | ❌ |
| 12 | 0.363744 | `azmcp_sql_db_list` | ❌ |
| 13 | 0.362606 | `azmcp_monitor_table_list` | ❌ |
| 14 | 0.358317 | `azmcp_monitor_table_type_list` | ❌ |
| 15 | 0.350839 | `azmcp_acr_registry_repository_list` | ❌ |
| 16 | 0.338334 | `azmcp_acr_registry_list` | ❌ |
| 17 | 0.334580 | `azmcp_extension_azqr` | ❌ |
| 18 | 0.322219 | `azmcp_functionapp_list` | ❌ |
| 19 | 0.313199 | `azmcp_extension_az` | ❌ |
| 20 | 0.302515 | `azmcp_monitor_metrics_definitions` | ❌ |

---

## Test 227

**Expected Tool:** `azmcp_workbooks_show`  
**Prompt:** Get information about the workbook with resource ID <workbook_resource_id>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.697705 | `azmcp_workbooks_show` | ✅ **EXPECTED** |
| 2 | 0.498390 | `azmcp_workbooks_create` | ❌ |
| 3 | 0.494708 | `azmcp_workbooks_list` | ❌ |
| 4 | 0.457252 | `azmcp_workbooks_delete` | ❌ |
| 5 | 0.419105 | `azmcp_workbooks_update` | ❌ |
| 6 | 0.353546 | `azmcp_grafana_list` | ❌ |
| 7 | 0.277807 | `azmcp_quota_region_availability_list` | ❌ |
| 8 | 0.256678 | `azmcp_quota_usage_check` | ❌ |
| 9 | 0.242738 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 10 | 0.238836 | `azmcp_monitor_resource_log_query` | ❌ |
| 11 | 0.235446 | `azmcp_marketplace_product_get` | ❌ |
| 12 | 0.230558 | `azmcp_monitor_metrics_query` | ❌ |
| 13 | 0.230516 | `azmcp_monitor_metrics_definitions` | ❌ |
| 14 | 0.226385 | `azmcp_loadtesting_test_get` | ❌ |
| 15 | 0.218999 | `azmcp_loadtesting_testresource_list` | ❌ |
| 16 | 0.207693 | `azmcp_datadog_monitoredresources_list` | ❌ |
| 17 | 0.195751 | `azmcp_monitor_healthmodels_entity_gethealth` | ❌ |
| 18 | 0.195373 | `azmcp_group_list` | ❌ |
| 19 | 0.194010 | `azmcp_loadtesting_testrun_get` | ❌ |
| 20 | 0.189657 | `azmcp_extension_azqr` | ❌ |

---

## Test 228

**Expected Tool:** `azmcp_workbooks_show`  
**Prompt:** Show me the workbook with display name <workbook_display_name>  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.469638 | `azmcp_workbooks_show` | ✅ **EXPECTED** |
| 2 | 0.455158 | `azmcp_workbooks_create` | ❌ |
| 3 | 0.437638 | `azmcp_workbooks_update` | ❌ |
| 4 | 0.424338 | `azmcp_workbooks_list` | ❌ |
| 5 | 0.371623 | `azmcp_workbooks_delete` | ❌ |
| 6 | 0.292898 | `azmcp_grafana_list` | ❌ |
| 7 | 0.266530 | `azmcp_monitor_table_list` | ❌ |
| 8 | 0.239914 | `azmcp_monitor_workspace_list` | ❌ |
| 9 | 0.227383 | `azmcp_monitor_table_type_list` | ❌ |
| 10 | 0.176481 | `azmcp_role_assignment_list` | ❌ |
| 11 | 0.175814 | `azmcp_appconfig_kv_show` | ❌ |
| 12 | 0.174513 | `azmcp_loadtesting_testrun_update` | ❌ |
| 13 | 0.174123 | `azmcp_storage_table_list` | ❌ |
| 14 | 0.165774 | `azmcp_cosmos_database_list` | ❌ |
| 15 | 0.154760 | `azmcp_cosmos_database_container_list` | ❌ |
| 16 | 0.149678 | `azmcp_cosmos_account_list` | ❌ |
| 17 | 0.146054 | `azmcp_kusto_table_schema` | ❌ |
| 18 | 0.143754 | `azmcp_loadtesting_testrun_get` | ❌ |
| 19 | 0.141559 | `azmcp_foundry_models_list` | ❌ |
| 20 | 0.138897 | `azmcp_loadtesting_testrun_list` | ❌ |

---

## Test 229

**Expected Tool:** `azmcp_workbooks_update`  
**Prompt:** Update the workbook <workbook_resource_id> with a new text step  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.469915 | `azmcp_workbooks_update` | ✅ **EXPECTED** |
| 2 | 0.382651 | `azmcp_workbooks_create` | ❌ |
| 3 | 0.362468 | `azmcp_workbooks_show` | ❌ |
| 4 | 0.351698 | `azmcp_workbooks_delete` | ❌ |
| 5 | 0.276727 | `azmcp_loadtesting_testrun_update` | ❌ |
| 6 | 0.262873 | `azmcp_workbooks_list` | ❌ |
| 7 | 0.170118 | `azmcp_grafana_list` | ❌ |
| 8 | 0.155789 | `azmcp_storage_blob_upload` | ❌ |
| 9 | 0.145788 | `azmcp_storage_blob_batch_set-tier` | ❌ |
| 10 | 0.144812 | `azmcp_extension_az` | ❌ |
| 11 | 0.142404 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 12 | 0.142186 | `azmcp_loadtesting_testrun_create` | ❌ |
| 13 | 0.138354 | `azmcp_appconfig_kv_set` | ❌ |
| 14 | 0.136105 | `azmcp_loadtesting_testresource_create` | ❌ |
| 15 | 0.131007 | `azmcp_postgres_database_query` | ❌ |
| 16 | 0.129973 | `azmcp_postgres_server_param_set` | ❌ |
| 17 | 0.129660 | `azmcp_deploy_iac_rules_get` | ❌ |
| 18 | 0.124925 | `azmcp_appconfig_kv_unlock` | ❌ |
| 19 | 0.121289 | `azmcp_monitor_workspace_log_query` | ❌ |
| 20 | 0.115996 | `azmcp_appconfig_kv_lock` | ❌ |

---

## Test 230

**Expected Tool:** `azmcp_bicepschema_get`  
**Prompt:** How can I use Bicep to create an Azure OpenAI service?  

### Results

| Rank | Score | Tool | Status |
|------|-------|------|--------|
| 1 | 0.485889 | `azmcp_deploy_iac_rules_get` | ❌ |
| 2 | 0.440302 | `azmcp_deploy_pipeline_guidance_get` | ❌ |
| 3 | 0.432773 | `azmcp_deploy_plan_get` | ❌ |
| 4 | 0.432409 | `azmcp_bicepschema_get` | ✅ **EXPECTED** |
| 5 | 0.401162 | `azmcp_extension_az` | ❌ |
| 6 | 0.400985 | `azmcp_foundry_models_deploy` | ❌ |
| 7 | 0.398046 | `azmcp_deploy_architecture_diagram_generate` | ❌ |
| 8 | 0.394677 | `azmcp_bestpractices_get` | ❌ |
| 9 | 0.375228 | `azmcp_azureterraformbestpractices_get` | ❌ |
| 10 | 0.363171 | `azmcp_search_index_list` | ❌ |
| 11 | 0.345030 | `azmcp_search_service_list` | ❌ |
| 12 | 0.342237 | `azmcp_foundry_models_deployments_list` | ❌ |
| 13 | 0.335700 | `azmcp_search_index_query` | ❌ |
| 14 | 0.303518 | `azmcp_search_index_describe` | ❌ |
| 15 | 0.303183 | `azmcp_quota_usage_check` | ❌ |
| 16 | 0.286560 | `azmcp_storage_account_create` | ❌ |
| 17 | 0.280207 | `azmcp_workbooks_delete` | ❌ |
| 18 | 0.275781 | `azmcp_resourcehealth_availability-status_get` | ❌ |
| 19 | 0.268139 | `azmcp_workbooks_create` | ❌ |
| 20 | 0.265709 | `azmcp_storage_blob_upload` | ❌ |

---

## Summary

**Total Prompts Tested:** 230  
**Execution Time:** 27.9808008s  

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

