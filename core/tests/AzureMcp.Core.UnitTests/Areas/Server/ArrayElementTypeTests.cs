// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections;
using AzureMcp.Areas.Server.Commands;
using Xunit;

namespace AzureMcp.Tests.Commands.Server;

public class ArrayElementTypeTests
{
    [Theory]
    [InlineData(typeof(string[]), typeof(string))]
    [InlineData(typeof(int[]), typeof(int))]
    [InlineData(typeof(object[]), typeof(object))]
    public void GetArrayElementType_WithArrayTypes_ReturnsElementType(Type arrayType, Type expectedElementType)
    {
        // Act
        var result = TypeToJsonTypeMapper.GetArrayElementType(arrayType);

        // Assert
        Assert.Equal(expectedElementType, result);
    }

    [Theory]
    [InlineData(typeof(List<string>), typeof(string))]
    [InlineData(typeof(List<int>), typeof(int))]
    [InlineData(typeof(IList<object>), typeof(object))]
    [InlineData(typeof(IEnumerable<bool>), typeof(bool))]
    [InlineData(typeof(ICollection<DateTime>), typeof(DateTime))]
    public void GetArrayElementType_WithGenericCollectionTypes_ReturnsElementType(Type collectionType, Type expectedElementType)
    {
        // Act
        var result = TypeToJsonTypeMapper.GetArrayElementType(collectionType);

        // Assert
        Assert.Equal(expectedElementType, result);
    }

    [Theory]
    [InlineData(typeof(ArrayList), typeof(object))]
    [InlineData(typeof(IEnumerable), typeof(object))]
    public void GetArrayElementType_WithNonGenericCollectionTypes_ReturnsObject(Type collectionType, Type expectedElementType)
    {
        // Act
        var result = TypeToJsonTypeMapper.GetArrayElementType(collectionType);

        // Assert
        Assert.Equal(expectedElementType, result);
    }

    [Theory]
    [InlineData(typeof(string))]
    [InlineData(typeof(int))]
    [InlineData(typeof(object))]
    public void GetArrayElementType_WithNonCollectionTypes_ReturnsNull(Type nonCollectionType)
    {
        // Act
        var result = TypeToJsonTypeMapper.GetArrayElementType(nonCollectionType);

        // Assert
        Assert.Null(result);
    }
}
