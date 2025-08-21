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
        /// <summary> The per database settings for the elastic pool. </summary>
        public SqlElasticPoolPerDatabaseSettings? PerDatabaseSettings { get; set; }
        /// <summary> Whether or not this elastic pool is zone redundant, which means the replicas of this elastic pool will be spread across multiple availability zones. </summary>
        [JsonPropertyName("zoneRedundant")]
        public bool? IsZoneRedundant { get; set; }
        /// <summary> The license type to apply for this elastic pool. </summary>
        public string? LicenseType { get; set; }
    }
}
