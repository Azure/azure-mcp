// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Sql.Options;
using AzureMcp.Commands;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Sql.Commands;

public abstract class BaseElasticPoolCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>(ILogger<BaseSqlCommand<TOptions>> logger)
    : BaseSqlCommand<TOptions>(logger) where TOptions : BaseElasticPoolOptions, new()
{
}
