// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using AzureMcp.Areas.Server.Commands;
using Xunit;

namespace AzureMcp.Tests.Commands.Server;

public class OptionTypeTests
{
    [Fact]
    public void Option_List_String_ValueType_Should_Be_Array()
    {
        // Arrange
        var option = new Option<List<string>>("--test", "Test option");

        // Act
        var jsonType = option.ValueType.ToJsonType();

        // Assert
        Assert.Equal("array", jsonType);
    }

    [Fact]
    public void GetArrayElementType_Should_Return_String_For_List_String()
    {
        // Arrange
        var listType = typeof(List<string>);

        // Act
        var result = TypeToJsonTypeMapper.GetArrayOrCollectionElementType(listType);

        // Assert
        Assert.Equal(typeof(string), result);
    }

    [Fact]
    public void CreateOptionSchema_Should_Return_Correct_Schema_For_Array_Type()
    {
        // Arrange
        var listType = typeof(List<string>);
        var description = "A list of strings";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(listType, description);

        // Assert
        Assert.Equal("array", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.NotNull(result["items"]);
        Assert.Equal("string", result["items"]?["type"]?.ToString());
    }

    [Fact]
    public void CreateOptionSchema_Should_Return_Correct_Schema_For_String_Type()
    {
        // Arrange
        var stringType = typeof(string);
        var description = "A string value";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(stringType, description);

        // Assert
        Assert.Equal("string", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]); // Should not have items for non-array types
    }

    [Fact]
    public void CreateOptionSchema_Should_Return_Correct_Schema_For_Integer_Type()
    {
        // Arrange
        var intType = typeof(int);
        var description = "An integer value";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(intType, description);

        // Assert
        Assert.Equal("integer", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]); // Should not have items for non-array types
    }

    [Fact]
    public void CreateOptionSchema_Should_Return_Correct_Schema_For_Boolean_Type()
    {
        // Arrange
        var boolType = typeof(bool);
        var description = "A boolean value";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(boolType, description);

        // Assert
        Assert.Equal("boolean", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]); // Should not have items for non-array types
    }

    [Fact]
    public void CreateOptionSchema_Should_Return_Correct_Schema_For_Number_Type()
    {
        // Arrange
        var doubleType = typeof(double);
        var description = "A number value";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(doubleType, description);

        // Assert
        Assert.Equal("number", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]); // Should not have items for non-array types
    }

    [Fact]
    public void CreateOptionSchema_Should_Return_Correct_Schema_For_Object_Type()
    {
        // Arrange
        var objectType = typeof(object);
        var description = "An object value";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(objectType, description);

        // Assert
        Assert.Equal("object", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]); // Should not have items for non-array types
    }

    [Fact]
    public void CreateOptionSchema_Should_Handle_Null_Description()
    {
        // Arrange
        var stringType = typeof(string);
        string? description = null;

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(stringType, description);

        // Assert
        Assert.Equal("string", result["type"]?.ToString());
        Assert.Equal(string.Empty, result["description"]?.ToString()); // Should default to empty string
        Assert.Null(result["items"]);
    }

    [Fact]
    public void CreateOptionSchema_Should_Return_Correct_Schema_For_Integer_Array()
    {
        // Arrange
        var intArrayType = typeof(int[]);
        var description = "An array of integers";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(intArrayType, description);

        // Assert
        Assert.Equal("array", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.NotNull(result["items"]);
        Assert.Equal("integer", result["items"]?["type"]?.ToString());
    }

    [Fact]
    public void CreateOptionSchema_Should_Return_Correct_Schema_For_Guid_Type()
    {
        // Arrange
        var guidType = typeof(Guid);
        var description = "A GUID value";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(guidType, description);

        // Assert
        Assert.Equal("string", result["type"]?.ToString()); // GUIDs are serialized as strings
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]);
    }

    [Theory]
    [InlineData(typeof(char), "string")]
    [InlineData(typeof(DateTime), "string")]
    [InlineData(typeof(TimeSpan), "string")]
    [InlineData(typeof(uint), "integer")]
    [InlineData(typeof(long), "integer")]
    [InlineData(typeof(float), "number")]
    [InlineData(typeof(decimal), "number")]
    public void CreateOptionSchema_Should_Return_Correct_Schema_For_Various_Types(Type type, string expectedJsonType)
    {
        // Arrange
        var description = $"A {type.Name} value";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(type, description);

        // Assert
        Assert.Equal(expectedJsonType, result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]); // Non-array types should not have items
    }

    [Theory]
    [InlineData(typeof(int?), "integer")]
    [InlineData(typeof(bool?), "boolean")]
    [InlineData(typeof(DateTime?), "string")]
    [InlineData(typeof(double?), "number")]
    public void CreateOptionSchema_Should_Handle_Nullable_Types(Type nullableType, string expectedJsonType)
    {
        // Arrange
        var description = $"A nullable {nullableType.Name} value";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(nullableType, description);

        // Assert
        Assert.Equal(expectedJsonType, result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]); // Nullable types should not have items
    }

    [Fact]
    public void CreateOptionSchema_Should_Throw_ArgumentNullException_For_Null_Type()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => TypeToJsonTypeMapper.CreateOptionSchema(null!, "description"));
    }

    [Fact]
    public void GetArrayElementType_Should_Throw_ArgumentNullException_For_Null_Type()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => TypeToJsonTypeMapper.GetArrayOrCollectionElementType(null!));
    }

    [Fact]
    public void CreateOptionSchema_Should_Handle_Nested_Array_Types()
    {
        // Arrange
        var nestedArrayType = typeof(List<List<string>>);
        var description = "A nested array";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(nestedArrayType, description);

        // Assert
        Assert.Equal("array", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.NotNull(result["items"]);
        Assert.Equal("array", result["items"]?["type"]?.ToString()); // Inner type should also be array
    }

    [Fact]
    public void CreateOptionSchema_Should_Handle_Dictionary_Types()
    {
        // Arrange
        var dictType = typeof(Dictionary<string, object>);
        var description = "A dictionary";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(dictType, description);

        // Assert
        Assert.Equal("object", result["type"]?.ToString()); // Dictionaries are objects in JSON
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]); // Objects don't have items
    }

    public enum TestEnum
    {
        Value1,
        Value2
    }

    [Fact]
    public void CreateOptionSchema_Should_Handle_Enum_Types()
    {
        // Arrange
        var enumType = typeof(TestEnum);
        var description = "An enum value";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(enumType, description);

        // Assert
        Assert.Equal("integer", result["type"]?.ToString()); // Enums are integers in JSON
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]); // Enums should not have items
    }

    [Fact]
    public void CreateOptionSchema_Should_Handle_Deeply_Nested_Arrays()
    {
        // Arrange
        var deeplyNestedType = typeof(List<List<List<int>>>);
        var description = "A deeply nested array";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(deeplyNestedType, description);

        // Assert
        Assert.Equal("array", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        
        // Check first level of nesting
        Assert.NotNull(result["items"]);
        Assert.Equal("array", result["items"]?["type"]?.ToString());
        
        // Check second level of nesting
        Assert.NotNull(result["items"]?["items"]);
        Assert.Equal("array", result["items"]?["items"]?["type"]?.ToString());
        
        // Check third level (final element type)
        Assert.NotNull(result["items"]?["items"]?["items"]);
        Assert.Equal("integer", result["items"]?["items"]?["items"]?["type"]?.ToString());
    }

    [Fact]
    public void CreateOptionSchema_Should_Handle_Mixed_Array_Types()
    {
        // Arrange
        var mixedArrayType = typeof(List<int[]>); // List containing arrays
        var description = "A list of integer arrays";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(mixedArrayType, description);

        // Assert
        Assert.Equal("array", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        
        // Check inner array type
        Assert.NotNull(result["items"]);
        Assert.Equal("array", result["items"]?["type"]?.ToString());
        
        // Check final element type
        Assert.NotNull(result["items"]?["items"]);
        Assert.Equal("integer", result["items"]?["items"]?["type"]?.ToString());
    }

    [Theory]
    [InlineData(typeof(List<int?>), "integer")]
    [InlineData(typeof(bool?[]), "boolean")]
    [InlineData(typeof(IEnumerable<DateTime?>), "string")]
    public void CreateOptionSchema_Should_Handle_Arrays_With_Nullable_Elements(Type arrayWithNullableType, string expectedElementType)
    {
        // Arrange
        var description = "An array with nullable elements";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(arrayWithNullableType, description);

        // Assert
        Assert.Equal("array", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.NotNull(result["items"]);
        Assert.Equal(expectedElementType, result["items"]?["type"]?.ToString());
    }

    [Theory]
    [InlineData(typeof(HashSet<string>), "string")]
    [InlineData(typeof(Queue<int>), "integer")]
    [InlineData(typeof(Stack<bool>), "boolean")]
    [InlineData(typeof(ISet<double>), "number")]
    public void CreateOptionSchema_Should_Handle_Other_Collection_Types(Type collectionType, string expectedElementType)
    {
        // Arrange
        var description = $"A {collectionType.Name} collection";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(collectionType, description);

        // Assert
        Assert.Equal("array", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.NotNull(result["items"]);
        Assert.Equal(expectedElementType, result["items"]?["type"]?.ToString());
    }

    [Fact]
    public void CreateOptionSchema_Should_Handle_Jagged_Arrays()
    {
        // Arrange
        var jaggedArrayType = typeof(int[][]);
        var description = "A jagged array";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(jaggedArrayType, description);

        // Assert
        Assert.Equal("array", result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        
        // Check inner array type
        Assert.NotNull(result["items"]);
        Assert.Equal("array", result["items"]?["type"]?.ToString());
        
        // Check final element type
        Assert.NotNull(result["items"]?["items"]);
        Assert.Equal("integer", result["items"]?["items"]?["type"]?.ToString());
    }

    [Theory]
    [InlineData(typeof(Dictionary<string, List<int>>))]
    [InlineData(typeof(IDictionary<int, string[]>))]
    [InlineData(typeof(SortedDictionary<string, bool>))]
    public void CreateOptionSchema_Should_Handle_Complex_Dictionary_Types(Type dictionaryType)
    {
        // Arrange
        var description = "A complex dictionary";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(dictionaryType, description);

        // Assert
        Assert.Equal("object", result["type"]?.ToString()); // Dictionaries are objects
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]); // Objects don't have items schema
    }

    [Theory]
    [InlineData(typeof(TestEnum?), "integer")]
    [InlineData(typeof(ConsoleColor?), "integer")]
    [InlineData(typeof(DayOfWeek?), "integer")]
    public void CreateOptionSchema_Should_Handle_Nullable_Enums(Type nullableEnumType, string expectedType)
    {
        // Arrange
        var description = "A nullable enum";

        // Act
        var result = TypeToJsonTypeMapper.CreateOptionSchema(nullableEnumType, description);

        // Assert
        Assert.Equal(expectedType, result["type"]?.ToString());
        Assert.Equal(description, result["description"]?.ToString());
        Assert.Null(result["items"]);
    }
}
