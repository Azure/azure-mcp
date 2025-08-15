// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace AzureMcp.Sql.Services.Models
{
    /// <summary> Per database settings of an elastic pool. </summary>
    internal sealed class SqlElasticPoolPerDatabaseSettings
    {
        /// <summary> The minimum capacity all databases are guaranteed. </summary>
        public double? MinCapacity { get; set; }
        /// <summary> The maximum capacity any one database can consume. </summary>
        public double? MaxCapacity { get; set; }
    }
}
