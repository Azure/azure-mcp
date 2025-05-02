// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments;
using AzureMcp.Models.Command;

namespace AzureMcp.Models.Argument;

/// <summary>
/// Typed argument definition for a specific argument class
/// </summary>
/// <typeparam name="TArgs">The type of the arguments class</typeparam>
public class ArgumentBuilder<TArgs> : ArgumentDefinition<string> where TArgs : GlobalArguments
{
    /// <summary>
    /// Function to access the current value of this argument from the arguments object
    /// </summary>
    public Func<TArgs, string> ValueAccessor { get; set; } = _ => string.Empty;

    /// <summary>
    /// Creates a new instance of ArgumentBuilder with the specified name and description
    /// </summary>
    public static ArgumentBuilder<TArgs> Create(string name, string description)
    {
        return new ArgumentBuilder<TArgs>(name, description);
    }

    private ArgumentBuilder(string name, string description) : base(name, description)
    {
    }

    /// <summary>
    /// Sets whether this argument is required
    /// </summary>
    public ArgumentBuilder<TArgs> WithIsRequired(bool required)
    {
        Required = required;
        return this;
    }

    /// <summary>
    /// Sets the value accessor for this argument
    /// </summary>
    public ArgumentBuilder<TArgs> WithValueAccessor(Func<TArgs, string> valueAccessor)
    {
        ValueAccessor = valueAccessor;
        return this;
    }

    /// <summary>
    /// Sets the default value for this argument
    /// </summary>
    public ArgumentBuilder<TArgs> WithDefaultValue(string defaultValue)
    {
        DefaultValue = defaultValue;
        return this;
    }
}