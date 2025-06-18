// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections;

namespace AzureMcp.Areas.Server.Commands;

/// <summary>
/// Gets the JSON object type based on its C# type.
/// </summary>
public static class TypeToJsonTypeMapper
{
    private static readonly Dictionary<Type, string> s_typeToJsonMap = new()
    {
        // String types
        { typeof(string), "string" },
        { typeof(char), "string" },
        { typeof(Guid), "string" },
        { typeof(DateTime), "string" },
        { typeof(DateTimeOffset), "string" },
        { typeof(TimeSpan), "string" },
        { typeof(Uri), "string" },
        
        // Number types
        { typeof(int), "integer" },
        { typeof(uint), "integer" },
        { typeof(long), "integer" },
        { typeof(ulong), "integer" },
        { typeof(short), "integer" },
        { typeof(ushort), "integer" },
        { typeof(byte), "integer" },
        { typeof(sbyte), "integer" },

        { typeof(float), "number" },
        { typeof(double), "number" },
        { typeof(decimal), "number" },
        
        // Boolean
        { typeof(bool), "boolean" },
        
        // Arrays and collections
        { typeof(Array), "array" },
        
        // Object
        { typeof(object), "object" }
    };

    /// <summary>
    /// Gets the JSON type name based on the given type.  If <paramref name="type"/> is null, then "null" is returned.
    /// </summary>
    /// <param name="type">Type to get JSON type name from.</param>
    /// <returns></returns>
    public static string ToJsonType(this Type? type)
    {
        if (type == null)
        {
            return "null";
        }

        if (s_typeToJsonMap.TryGetValue(type, out string? jsonType) && jsonType != null)
        {
            return jsonType;
        }

        if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
        {
            return typeof(IDictionary).IsAssignableFrom(type) ? "object" : "array";
        }

        if (type.IsEnum)
        {
            return "integer";
        }

        return "object";
    }
}
