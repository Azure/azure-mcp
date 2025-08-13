// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Sql.Services.Models
{
    /// <summary>
    /// A class representing the SqlDatabase properties model.
    /// A database resource properties.
    /// </summary>
    internal sealed class SqlDatabaseProperties
    {
        /// <summary> Specifies the mode of database creation. </summary>
        public string? CreateMode { get; set; }
        /// <summary> The collation of the database. </summary>
        public string? Collation { get; set; }
        /// <summary> The max size of the database expressed in bytes. </summary>
        public long? MaxSizeBytes { get; set; }
        /// <summary> The name of the sample schema to apply when creating this database. </summary>
        public string? SampleName { get; set; }
        /// <summary> The resource identifier of the elastic pool containing this database. </summary>
        public string? ElasticPoolId { get; set; }
        /// <summary> The resource identifier of the source database associated with create operation of this database. </summary>
        public string? SourceDatabaseId { get; set; }
        /// <summary> The status of the database. </summary>
        public string? Status { get; set; }
        /// <summary> The ID of the database. </summary>
        public Guid? DatabaseId { get; set; }
        /// <summary> The creation date of the database (ISO8601 format). </summary>
        [JsonPropertyName("creationDate")]
        public DateTimeOffset? CreatedOn { get; set; }
        /// <summary> The current service level objective name of the database. </summary>
        public string? CurrentServiceObjectiveName { get; set; }
        /// <summary> The requested service level objective name of the database. </summary>
        public string? RequestedServiceObjectiveName { get; set; }
        /// <summary> The default secondary region for this database. </summary>
        public string? DefaultSecondaryLocation { get; set; }
        /// <summary> Failover Group resource identifier that this database belongs to. </summary>
        public string? FailoverGroupId { get; set; }
        /// <summary> Specifies the point in time (ISO8601 format) of the source database that will be restored to create the new database. </summary>
        [JsonPropertyName("restorePointInTime")]
        public DateTimeOffset? RestorePointInTime { get; set; }
        /// <summary> Specifies the time that the database was deleted. </summary>
        [JsonPropertyName("sourceDatabaseDeletionDate")]
        public DateTimeOffset? SourceDatabaseDeletedOn { get; set; }
        /// <summary> The resource identifier of the recovery point associated with create operation of this database. </summary>
        public string? RecoveryServicesRecoveryPointId { get; set; }
        /// <summary> The resource identifier of the long term retention backup associated with create operation of this database. </summary>
        public string? LongTermRetentionBackupResourceId { get; set; }
        /// <summary> The resource identifier of the recoverable database associated with create operation of this database. </summary>
        public string? RecoverableDatabaseId { get; set; }
        /// <summary> The resource identifier of the restorable dropped database associated with create operation of this database. </summary>
        public string? RestorableDroppedDatabaseId { get; set; }
        /// <summary> Collation of the metadata catalog. </summary>
        public string? CatalogCollation { get; set; }
        /// <summary> Whether or not this database is zone redundant, which means the replicas of this database will be spread across multiple availability zones. </summary>
        public bool? IsZoneRedundant { get; set; }
        /// <summary> The license type to apply for this database. `LicenseIncluded` if you need a license, or `BasePrice` if you have a license and are eligible for the Azure Hybrid Benefit. </summary>
        public string? LicenseType { get; set; }
        /// <summary> The max log size for this database. </summary>
        public long? MaxLogSizeBytes { get; set; }
        /// <summary> This records the earliest start date and time that restore is available for this database (ISO8601 format). </summary>
        [JsonPropertyName("earliestRestoreDate")]
        public DateTimeOffset? EarliestRestoreOn { get; set; }
        /// <summary> The state of read-only routing. If enabled, connections that have application intent set to readonly in their connection string may be routed to a readonly secondary replica in the same region. Not applicable to a Hyperscale database within an elastic pool. </summary>
        public string? ReadScale { get; set; }
        /// <summary> The number of secondary replicas associated with the Business Critical, Premium, or Hyperscale edition database that are used to provide high availability. Not applicable to a Hyperscale database within an elastic pool. </summary>
        public int? HighAvailabilityReplicaCount { get; set; }
        /// <summary> The secondary type of the database if it is a secondary.  Valid values are Geo, Named and Standby. </summary>
        public string? SecondaryType { get; set; }
        /// <summary> The name and tier of the SKU. </summary>
        public SqlSku? CurrentSku { get; set; }
        /// <summary> Time in minutes after which database is automatically paused. A value of -1 means that automatic pause is disabled. </summary>
        public int? AutoPauseDelay { get; set; }
        /// <summary> The storage account type used to store backups for this database. </summary>
        public string? CurrentBackupStorageRedundancy { get; set; }
        /// <summary> The storage account type to be used to store backups for this database. </summary>
        public string? RequestedBackupStorageRedundancy { get; set; }
        /// <summary> Minimal capacity that database will always have allocated, if not paused. </summary>
        public double? MinCapacity { get; set; }
        /// <summary> The date when database was paused by user configuration or action(ISO8601 format). Null if the database is ready. </summary>
        [JsonPropertyName("pausedDate")]
        public DateTimeOffset? PausedOn { get; set; }
        /// <summary> The date when database was resumed by user action or database login (ISO8601 format). Null if the database is paused. </summary>
        [JsonPropertyName("resumedDate")]
        public DateTimeOffset? ResumedOn { get; set; }
        /// <summary> Maintenance configuration id assigned to the database. This configuration defines the period when the maintenance updates will occur. </summary>
        public string? MaintenanceConfigurationId { get; set; }
        /// <summary> Whether or not this database is a ledger database, which means all tables in the database are ledger tables. Note: the value of this property cannot be changed after the database has been created. </summary>
        public bool? IsLedgerOn { get; set; }
        /// <summary> Infra encryption is enabled for this database. </summary>
        public bool? IsInfraEncryptionEnabled { get; set; }
        /// <summary> The Client id used for cross tenant per database CMK scenario. </summary>
        public Guid? FederatedClientId { get; set; }
        /// <summary> The resource ids of the user assigned identities to use. </summary>
        public IDictionary<string, SqlDatabaseKey>? Keys { get; set; }
        /// <summary> The azure key vault URI of the database if it's configured with per Database Customer Managed Keys. </summary>
        public string? EncryptionProtector { get; set; }
        /// <summary> Type of enclave requested on the database i.e. Default or VBS enclaves. </summary>
        public string? PreferredEnclaveType { get; set; }
        /// <summary> Whether or not the database uses free monthly limits. Allowed on one database in a subscription. </summary>
        public bool? UseFreeLimit { get; set; }
        /// <summary>
        /// Specifies the behavior when monthly free limits are exhausted for the free database.
        ///
        /// AutoPause: The database will be auto paused upon exhaustion of free limits for remainder of the month.
        ///
        /// BillForUsage: The database will continue to be online upon exhaustion of free limits and any overage will be billed.
        /// </summary>
        public string? FreeLimitExhaustionBehavior { get; set; }
        /// <summary>
        /// The resource identifier of the source associated with the create operation of this database.
        ///
        /// This property is only supported for DataWarehouse edition and allows to restore across subscriptions.
        ///
        /// When sourceResourceId is specified, sourceDatabaseId, recoverableDatabaseId, restorableDroppedDatabaseId and sourceDatabaseDeletionDate must not be specified and CreateMode must be PointInTimeRestore, Restore or Recover.
        ///
        /// When createMode is PointInTimeRestore, sourceResourceId must be the resource ID of the existing database or existing sql pool, and restorePointInTime must be specified.
        ///
        /// When createMode is Restore, sourceResourceId must be the resource ID of restorable dropped database or restorable dropped sql pool.
        ///
        /// When createMode is Recover, sourceResourceId must be the resource ID of recoverable database or recoverable sql pool.
        ///
        /// When source subscription belongs to a different tenant than target subscription, “x-ms-authorization-auxiliary” header must contain authentication token for the source tenant. For more details about “x-ms-authorization-auxiliary” header see https://docs.microsoft.com/en-us/azure/azure-resource-manager/management/authenticate-multi-tenant
        /// </summary>
        public string? SourceResourceId { get; set; }
        /// <summary>
        /// Whether or not customer controlled manual cutover needs to be done during Update Database operation to Hyperscale tier.
        ///
        /// This property is only applicable when scaling database from Business Critical/General Purpose/Premium/Standard tier to Hyperscale tier.
        ///
        /// When manualCutover is specified, the scaling operation will wait for user input to trigger cutover to Hyperscale database.
        ///
        /// To trigger cutover, please provide 'performCutover' parameter when the Scaling operation is in Waiting state.
        /// </summary>
        public bool? ManualCutover { get; set; }
        /// <summary>
        /// To trigger customer controlled manual cutover during the wait state while Scaling operation is in progress.
        ///
        /// This property parameter is only applicable for scaling operations that are initiated along with 'manualCutover' parameter.
        ///
        /// This property is only applicable when scaling database from Business Critical/General Purpose/Premium/Standard tier to Hyperscale tier is already in progress.
        ///
        /// When performCutover is specified, the scaling operation will trigger cutover and perform role-change to Hyperscale database.
        /// </summary>
        public bool? PerformCutover { get; set; }
        /// <summary> Specifies the availability zone the database is pinned to. </summary>
        public string? AvailabilityZone { get; set; }
        /// <summary> The flag to enable or disable auto rotation of database encryption protector AKV key. </summary>
        public bool? EncryptionProtectorAutoRotation { get; set; }
    }
}
