// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Sql.Commands;

namespace AzureMcp.Sql.Services.Models
{
    /// <summary>
    /// A class representing the ElasticPool data model.
    /// An elastic pool.
    /// </summary>
    internal sealed class SqlElasticPoolData
    {
        /// <summary> The resource ID for the resource. </summary>
        [JsonPropertyName("id")]
        public string? ResourceId { get; set; }
        /// <summary> The type of the resource. </summary>
        [JsonPropertyName("type")]
        public string? ResourceType { get; set; }
        /// <summary> The name of the resource. </summary>
        [JsonPropertyName("name")]
        public string? ResourceName { get; set; }
        /// <summary> The location of the resource. </summary>
        public string? Location { get; set; }
        /// <summary> The database SKU. </summary>
        public SqlSku? Sku { get; set; }
        /// <summary> Kind of elastic pool. This is metadata used for the Azure portal experience. </summary>
        public string? Kind { get; set; }
        /// <summary> The tags of the resource. </summary>
        public IDictionary<string, string>? Tags { get; set; }
        /// <summary> The properties of elastic pool. </summary>
        public SqlElasticPoolProperties? Properties { get; set; }

        // Read the JSON response content and create a model instance from it.
        public static SqlElasticPoolData? FromJson(JsonElement source)
        {
            return JsonSerializer.Deserialize<SqlElasticPoolData>(source, SqlJsonContext.Default.SqlElasticPoolData);
        }
    }
}
