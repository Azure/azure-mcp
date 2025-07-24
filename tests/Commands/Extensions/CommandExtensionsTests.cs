// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.Text.Json;
using AzureMcp.Commands;
using Xunit;

namespace AzureMcp.Tests.Commands.Extensions;

[Trait("Area", "Core")]
public class CommandExtensionsTests
{
    [Fact]
    public void ParseFromDictionary_WithNullArguments_ReturnsEmptyParseResult()
    {
        // Arrange
        var command = new Command("test", "Test command");

        // Act
        var result = command.ParseFromDictionary(null);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ParseFromDictionary_WithEmptyArguments_ReturnsEmptyParseResult()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var arguments = new Dictionary<string, JsonElement>();

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ParseFromDictionary_WithStringArgument_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--name", "Name option");
        command.AddOption(option);

        var arguments = new Dictionary<string, JsonElement>
        {
            ["name"] = JsonSerializer.SerializeToElement("test-value")
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("test-value", value);
    }

    [Fact]
    public void ParseFromDictionary_WithStringContainingQuotes_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--query", "Query option");
        command.AddOption(option);

        var arguments = new Dictionary<string, JsonElement>
        {
            ["query"] = JsonSerializer.SerializeToElement("SalesTable | parse ClassName with * 'jsonField': ' value '' *")
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("SalesTable | parse ClassName with * 'jsonField': ' value '' *", value);
    }

    [Fact]
    public void ParseFromDictionary_WithBooleanArguments_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var trueOption = new Option<bool>("--enabled", "Enabled option");
        var falseOption = new Option<bool>("--disabled", "Disabled option");
        command.AddOption(trueOption);
        command.AddOption(falseOption);

        var arguments = new Dictionary<string, JsonElement>
        {
            ["enabled"] = JsonSerializer.SerializeToElement(true),
            ["disabled"] = JsonSerializer.SerializeToElement(false)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        Assert.True(result.GetValueForOption(trueOption));
        Assert.False(result.GetValueForOption(falseOption));
    }

    [Fact]
    public void ParseFromDictionary_WithNumericArguments_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var intOption = new Option<int>("--count", "Count option");
        var doubleOption = new Option<double>("--rate", "Rate option");
        command.AddOption(intOption);
        command.AddOption(doubleOption);

        var arguments = new Dictionary<string, JsonElement>
        {
            ["count"] = JsonSerializer.SerializeToElement(42),
            ["rate"] = JsonSerializer.SerializeToElement(3.14)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        Assert.Equal(42, result.GetValueForOption(intOption));
        Assert.Equal(3.14, result.GetValueForOption(doubleOption));
    }
    [Fact]
    public void ParseFromDictionary_WithArrayArgument_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--items", "Items option");
        command.AddOption(option);

        var arguments = new Dictionary<string, JsonElement>
        {
            ["items"] = JsonSerializer.SerializeToElement(new[] { "item1", "item2", "item3" })
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("item1 item2 item3", value); // Array is joined with spaces
    }

    [Fact]
    public void ParseFromDictionary_WithNullValue_SkipsOption()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--name", "Name option");
        command.AddOption(option);

        var arguments = new Dictionary<string, JsonElement>
        {
            ["name"] = JsonSerializer.SerializeToElement<string?>(null)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Null(value);
    }
    [Fact]
    public void ParseFromDictionary_WithCaseInsensitiveOptionNames_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--subscription", "Subscription option");
        command.AddOption(option);

        var arguments = new Dictionary<string, JsonElement>
        {
            ["SUBSCRIPTION"] = JsonSerializer.SerializeToElement("test-sub")
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("test-sub", value);
    }

    [Fact]
    public void ParseFromDictionary_WithUnknownOption_IgnoresOption()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--known", "Known option");
        command.AddOption(option);

        var arguments = new Dictionary<string, JsonElement>
        {
            ["known"] = JsonSerializer.SerializeToElement("known-value"),
            ["unknown"] = JsonSerializer.SerializeToElement("unknown-value")
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("known-value", value);
    }

    [Fact]
    public void ParseFromDictionary_WithComplexJsonString_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--json-data", "JSON data option");
        command.AddOption(option);

        var jsonString = "{\"key\": \"value with 'single' and \\\"double\\\" quotes\"}";
        var arguments = new Dictionary<string, JsonElement>
        {
            ["json-data"] = JsonSerializer.SerializeToElement(jsonString)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal(jsonString, value);
    }

    [Fact]
    public void ParseFromDictionary_WithSingleQuotesInValues_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test");
        var queryOption = new Option<string>("--query") { IsRequired = true };
        var nameOption = new Option<string>("--name") { IsRequired = false };
        command.AddOption(queryOption);
        command.AddOption(nameOption);

        var arguments = new Dictionary<string, JsonElement>
        {
            { "query", JsonSerializer.SerializeToElement("SELECT * FROM table WHERE column = 'value'") },
            { "name", JsonSerializer.SerializeToElement("O'Connor's Database") }
        };

        // Act
        var result = command.ParseFromDictionary(arguments);        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        Assert.Equal("SELECT * FROM table WHERE column = 'value'", result.GetValueForOption(queryOption));
        Assert.Equal("O'Connor's Database", result.GetValueForOption(nameOption));
    }

    [Fact]
    public void ParseFromDictionary_WithDoubleQuotesInValues_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test");
        var messageOption = new Option<string>("--message") { IsRequired = true };
        var titleOption = new Option<string>("--title") { IsRequired = false };
        command.AddOption(messageOption);
        command.AddOption(titleOption);

        var arguments = new Dictionary<string, JsonElement>
        {
            { "message", JsonSerializer.SerializeToElement("He said \"Hello World\" to everyone") },
            { "title", JsonSerializer.SerializeToElement("The \"Best\" Solution") }
        };

        // Act
        var result = command.ParseFromDictionary(arguments);        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        Assert.Equal("He said \"Hello World\" to everyone", result.GetValueForOption(messageOption));
        Assert.Equal("The \"Best\" Solution", result.GetValueForOption(titleOption));
    }

    [Fact]
    public void ParseFromDictionary_WithMixedQuotesInValues_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test");
        var scriptOption = new Option<string>("--script") { IsRequired = true };
        command.AddOption(scriptOption);

        var arguments = new Dictionary<string, JsonElement>
        {
            { "script", JsonSerializer.SerializeToElement("echo \"User's home: '$HOME'\" && echo 'Path: \"$PATH\"'") }
        };

        // Act
        var result = command.ParseFromDictionary(arguments);        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        Assert.Equal("echo \"User's home: '$HOME'\" && echo 'Path: \"$PATH\"'", result.GetValueForOption(scriptOption));
    }

    [Fact]
    public void ParseFromDictionary_WithArrayContainingMixedStringAndNumeric_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--mixed-items", "Mixed items option");
        command.AddOption(option);

        var mixedArray = new object[] { "item1", 42, "item2", 3.14, "item3" };
        var arguments = new Dictionary<string, JsonElement>
        {
            ["mixed-items"] = JsonSerializer.SerializeToElement(mixedArray)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("'[\"item1\",42,\"item2\",3.14,\"item3\"]'", value);
    }

    [Fact]
    public void ParseFromDictionary_WithArrayContainingBooleans_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--bool-items", "Boolean items option");
        command.AddOption(option);

        var boolArray = new object[] { true, false, "string", true };
        var arguments = new Dictionary<string, JsonElement>
        {
            ["bool-items"] = JsonSerializer.SerializeToElement(boolArray)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("'[true,false,\"string\",true]'", value);
    }

    [Fact]
    public void ParseFromDictionary_WithArrayContainingObjects_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--object-items", "Object items option");
        command.AddOption(option);

        var objectArray = new object[] 
        { 
            "string-item",
            new { name = "test", value = 123 },
            "another-string"
        };
        var arguments = new Dictionary<string, JsonElement>
        {
            ["object-items"] = JsonSerializer.SerializeToElement(objectArray)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("'[\"string-item\",{\"name\":\"test\",\"value\":123},\"another-string\"]'", value);
    }

    [Fact]
    public void ParseFromDictionary_WithArrayContainingNestedArrays_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--nested-arrays", "Nested arrays option");
        command.AddOption(option);

        var nestedArrays = new object[] 
        { 
            "item1",
            new[] { "nested1", "nested2" },
            "item2"
        };
        var arguments = new Dictionary<string, JsonElement>
        {
            ["nested-arrays"] = JsonSerializer.SerializeToElement(nestedArrays)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("'[\"item1\",[\"nested1\",\"nested2\"],\"item2\"]'", value);
    }

    [Fact]
    public void ParseFromDictionary_WithArrayContainingMixedComplexTypes_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--complex-items", "Complex items option");
        command.AddOption(option);

        var complexArray = new object[] 
        { 
            "string",
            42,
            true,
            new { key = "value", number = 123 },
            new[] { "a", "b" },
            false
        };
        var arguments = new Dictionary<string, JsonElement>
        {
            ["complex-items"] = JsonSerializer.SerializeToElement(complexArray)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("'[\"string\",42,true,{\"key\":\"value\",\"number\":123},[\"a\",\"b\"],false]'", value);
    }

    [Fact]
    public void ParseFromDictionary_WithArrayContainingNulls_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--null-items", "Null items option");
        command.AddOption(option);

        var arrayWithNulls = new object?[] { "item1", null, "item2", null };
        var arguments = new Dictionary<string, JsonElement>
        {
            ["null-items"] = JsonSerializer.SerializeToElement(arrayWithNulls)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("'[\"item1\",null,\"item2\",null]'", value);
    }

    [Fact]
    public void ParseFromDictionary_WithArrayContainingEmptyObjectsAndArrays_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--empty-items", "Empty items option");
        command.AddOption(option);

        var emptyStructures = new object[] 
        { 
            "item",
            new { },  // empty object
            new object[] { },  // empty array
            "final"
        };
        var arguments = new Dictionary<string, JsonElement>
        {
            ["empty-items"] = JsonSerializer.SerializeToElement(emptyStructures)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.Equal("'[\"item\",{},[],\"final\"]'", value);
    }

    [Fact]
    public void ParseFromDictionary_WithArrayContainingComplexNestedStructures_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--complex-nested", "Complex nested option");
        command.AddOption(option);

        var complexNested = new object[] 
        { 
            "start",
            new 
            { 
                array = new[] { 1, 2, 3 },
                nested = new { inner = "value" }
            },
            new object[] { "a", new { x = 1 }, "b" }
        };
        var arguments = new Dictionary<string, JsonElement>
        {
            ["complex-nested"] = JsonSerializer.SerializeToElement(complexNested)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.NotNull(value);
        Assert.Equal("'[\"start\",{\"array\":[1,2,3],\"nested\":{\"inner\":\"value\"}},[\"a\",{\"x\":1},\"b\"]]'", value);
    }

    [Fact]
    public void ParseFromDictionary_WithObjectContainingComplexNestedStructures_ParsesCorrectly()
    {
        // Arrange
        var command = new Command("test", "Test command");
        var option = new Option<string>("--complex-nested", "Complex nested option");
        command.AddOption(option);

        var complexNested = new
        {
            array = new[] { 1, 2, 3 },
            nested = new { inner = "value" }
        };

        var arguments = new Dictionary<string, JsonElement>
        {
            ["complex-nested"] = JsonSerializer.SerializeToElement(complexNested)
        };

        // Act
        var result = command.ParseFromDictionary(arguments);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var value = result.GetValueForOption(option);
        Assert.NotNull(value);
        Assert.Equal("'{\"array\":[1,2,3],\"nested\":{\"inner\":\"value\"}}'", value);
    }
}
