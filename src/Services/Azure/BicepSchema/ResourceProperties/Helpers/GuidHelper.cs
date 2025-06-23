﻿// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;

namespace AzureMcp.Services.Azure.BicepSchema.ResourceProperties.Helpers;
public static class GuidHelper
{
    public static Guid GenerateDeterministicGuid(params string[] strings)
    {
        string concatenatedString = string.Join("", strings);
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(concatenatedString));
        var guid = new Guid([.. hash.Take(16)]);

        return guid;
    }
}
