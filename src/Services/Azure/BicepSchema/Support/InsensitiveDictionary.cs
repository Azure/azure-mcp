﻿// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

namespace AzureMcp.Services.Azure.BicepSchema.Support;

public class InsensitiveDictionary<TValue> : Dictionary<string, TValue>
{
    public static readonly InsensitiveDictionary<TValue> Empty = [];

    public InsensitiveDictionary()
        : base((IEqualityComparer<string>?)StringComparer.InvariantCultureIgnoreCase)
    {
    }

    public InsensitiveDictionary(int capacity)
        : base(capacity, (IEqualityComparer<string>?)StringComparer.InvariantCultureIgnoreCase)
    {
    }

    public InsensitiveDictionary(IDictionary<string, TValue> dictionary)
        : base(dictionary, (IEqualityComparer<string>?)StringComparer.InvariantCultureIgnoreCase)
    {
    }
}
