﻿// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

using Azure.Bicep.Types.Concrete;
using AzureMcp.Services.Azure.BicepSchema.Support;

namespace AzureMcp.Services.Azure.BicepSchema.ResourceProperties.Extensions;
public static class BicepTypeExtensions
{
    public static string ToTypeString(this TypeBase typeBase)
    {
        return typeBase switch
        {
            StringLiteralType => "string",
            StringType => "string",
            BooleanType => "bool",
            NullType => "null",
            AnyType => "any",
            IntegerType => "int",
            UnionType => "union",
            ObjectType objectType => objectType.Name,
            DiscriminatedObjectType discriminatedObjectType => $"{discriminatedObjectType.Name} - {discriminatedObjectType.Discriminator}",
            ArrayType arrayType => arrayType.ItemType.Type.ToTypeString() + "[]", // Not used. We always flatten the type inside the arrays
            _ => typeBase.ToJson()
        };
    }

    public static string ToValueConstraint(this TypeBase typeBase)
    {
        return typeBase switch
        {
            StringLiteralType stringLiteralType => stringLiteralType.Value,
            UnionType unionType => string.Join(" | ", unionType.Elements.Select(e => e.Type.ToValueConstraint())),
            ObjectType objectType => string.Empty,
            DiscriminatedObjectType discriminatedObjectType => string.Empty,
            _ => typeBase.ToJsonIfNotEmpty()
        };
    }

    public static string ToJsonIfNotEmpty(this TypeBase typeBase)
    {
        string json = typeBase.ToJson();
        if (json == "{}")
        {
            return string.Empty;
        }

        return json;
    }
}
