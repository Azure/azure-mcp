// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace AzureMcp.Areas.BicepSchema.Services.Support;

public static class JsonExtensions
{
    public static readonly JsonSerializerSettings ObjectSerializationSettings = new JsonSerializerSettings
    {
        MaxDepth = 512,
        TypeNameHandling = TypeNameHandling.None,
        DateParseHandling = DateParseHandling.None,
        DateTimeZoneHandling = DateTimeZoneHandling.Utc,
        NullValueHandling = NullValueHandling.Ignore,
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        Converters =
        [
            new StringEnumConverter
            {
                NamingStrategy = new CamelCaseNamingStrategy(),
            },
        ]
    };

    public static readonly JsonSerializer JsonObjectTypeSerializer = JsonSerializer.Create(ObjectSerializationSettings);

    public static string ToJson(this object value)
    {
        StringWriter sb = new StringWriter();
        WriteJson(sb, value);
        return sb.ToString();
    }

    private static void WriteJson(TextWriter writer, object value)
    {
        using JsonTextWriter jsonTextWriter = new JsonTextWriter(writer);
        jsonTextWriter.Formatting = JsonObjectTypeSerializer.Formatting;
        jsonTextWriter.CloseOutput = false;
        JsonObjectTypeSerializer.Serialize(jsonTextWriter, value);
    }
}
