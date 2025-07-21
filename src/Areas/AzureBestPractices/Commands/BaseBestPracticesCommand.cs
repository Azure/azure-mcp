// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using AzureMcp.Commands;
using AzureMcp.Helpers;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AzureBestPractices.Commands;

public abstract class BaseBestPracticesCommand<T> : BaseCommand where T : BaseBestPracticesCommand<T>
{
    protected readonly ILogger<T> _logger;
    private static readonly Dictionary<string, string> s_bestPracticesCache = new();

    protected BaseBestPracticesCommand(ILogger<T> logger)
    {
        _logger = logger;
    }

    protected abstract string ResourceFileName { get; }

    public abstract override string Title { get; }

    protected string GetBestPracticesText()
    {
        if (!s_bestPracticesCache.TryGetValue(ResourceFileName, out string? bestPractices))
        {
            bestPractices = LoadBestPracticesText();
            s_bestPracticesCache[ResourceFileName] = bestPractices;
        }
        return bestPractices;
    }

    private string LoadBestPracticesText()
    {
        Assembly assembly = typeof(T).Assembly;
        string resourceName = EmbeddedResourceHelper.FindEmbeddedResource(assembly, ResourceFileName);
        return EmbeddedResourceHelper.ReadEmbeddedResource(assembly, resourceName);
    }

    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var bestPractices = GetBestPracticesText();
        context.Response.Status = 200;
        context.Response.Results = ResponseResult.Create(new List<string> { bestPractices }, JsonSourceGenerationContext.Default.ListString);
        context.Response.Message = string.Empty;
        return Task.FromResult(context.Response);
    }
}
