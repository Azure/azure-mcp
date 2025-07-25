// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Sql.Options;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Sql.Commands;

public abstract class BaseElasticPoolCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>(ILogger<BaseSqlCommand<TOptions>> logger)
    : BaseSqlCommand<TOptions>(logger) where TOptions : BaseElasticPoolOptions, new()
{
}
