// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Sql.Services.Models
{
    /// <summary>
    /// A class representing the ElasticPool properties model.
    /// An elastic pool properties.
    /// </summary>
    internal sealed class SqlElasticPoolProperties
    {
        /// <summary> The state of the elastic pool. </summary>
        public string? State { get; set; }
        /// <summary> The creation date of the elastic pool (ISO8601 format). </summary>
        [JsonPropertyName("creationDate")]
        public DateTimeOffset? CreatedOn { get; set; }
        /// <summary> The storage limit for the database elastic pool in bytes. </summary>
        public long? MaxSizeBytes { get; set; }
        /// <summary> Minimal capacity that serverless pool will not shrink below, if not paused. </summary>
        public double? MinCapacity { get; set; }
        /// <summary> The per database settings for the elastic pool. </summary>
        public SqlElasticPoolPerDatabaseSettings? PerDatabaseSettings { get; set; }
        /// <summary> Whether or not this elastic pool is zone redundant, which means the replicas of this elastic pool will be spread across multiple availability zones. </summary>
        [JsonPropertyName("zoneRedundant")]
        public bool? IsZoneRedundant { get; set; }
        /// <summary> The license type to apply for this elastic pool. </summary>
        public string? LicenseType { get; set; }
        /// <summary> Maintenance configuration id assigned to the elastic pool. This configuration defines the period when the maintenance updates will will occur. </summary>
        public string? MaintenanceConfigurationId { get; set; }
        /// <summary> The number of secondary replicas associated with the Business Critical, Premium, or Hyperscale edition elastic pool that are used to provide high availability. Applicable only to Hyperscale elastic pools. </summary>
        public int? HighAvailabilityReplicaCount { get; set; }
        /// <summary> Time in minutes after which elastic pool is automatically paused. A value of -1 means that automatic pause is disabled. </summary>
        public int? AutoPauseDelay { get; set; }
        /// <summary> Type of enclave requested on the elastic pool. </summary>
        public string? PreferredEnclaveType { get; set; }
        /// <summary> Specifies the availability zone the pool's primary replica is pinned to. </summary>
        public string? AvailabilityZone { get; set; }
    }
}
