// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Sql.Services.Models
{
    /// <summary> Database level key used for encryption at rest. </summary>
    internal sealed class SqlDatabaseKey
    {
        /// <summary> The database key type. Only supported value is 'AzureKeyVault'. </summary>
        [JsonPropertyName("type")]
        public string? KeyType { get; set; }
        /// <summary> Thumbprint of the database key. </summary>
        public string? Thumbprint { get; set; }
        /// <summary> The database key creation date. </summary>
        [JsonPropertyName("creationDate")]
        public DateTimeOffset? CreatedOn { get; set; }
        /// <summary> Subregion of the server key. </summary>
        public string? Subregion { get; set; }
        /// <summary> The database key's version. </summary>
        public string? KeyVersion { get; set; }
    }
}
