// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Reflection;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace AzureMcp.Extensions;

public static class OpenTelemetryExtensions
{
    public static void ConfigureOpenTelemetry(this IServiceCollection services)
    {
        if (!TryEnableAzureMonitor(services))
        {
            TryEnableOtlp(services);
        }
    }

    private static bool TryEnableOtlp(this IServiceCollection services)
    {
        // TODO (lmolkova): find a better env var name to avoid double-negation since we want an explicit opt-in
        // OTEL_SDK_DISABLED is a standard env var, but the default is `false` -
        // https://opentelemetry.io/docs/specs/otel/configuration/sdk-enviroment-variables/#general-sdk-configuration

        var otelDisabled = Environment.GetEnvironmentVariable("OTEL_SDK_DISABLED");
        if (string.IsNullOrEmpty(otelDisabled) || !bool.TryParse(otelDisabled, out var isDisabled) || isDisabled)
        {
            return false;
        }

        services.AddOpenTelemetry()
            .ConfigureResource(r => r.AddService("azmcp", serviceVersion: Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString()))
            .WithTracing(
                b => b.AddSource("*")
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation(o =>
                {
                    // TODO (lmolkova) necessary because of https://github.com/Azure/azure-sdk-for-net/issues/37446
                    o.FilterHttpRequestMessage = _ => Activity.Current?.Parent?.Source?.Name != "Azure.Core.Http" && Activity.Current?.Source?.Name != "System.Net.Http";
                }))
            .WithMetrics(b => b.AddMeter("*")
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation())
            .WithLogging()
            .UseOtlpExporter();
        return true;
    }

    private static bool TryEnableAzureMonitor(this IServiceCollection services)
    {
        var appInsightsConnectionString = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING");
        if (!string.IsNullOrEmpty(appInsightsConnectionString))
        {
            services
                .ConfigureOpenTelemetryTracerProvider((_, b) => b.AddSource("*"))
                .ConfigureOpenTelemetryMeterProvider((_, b) => b.AddMeter("*"))
                .AddOpenTelemetry()
                    .ConfigureResource(r => r.AddService("azmcp", serviceVersion: Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString()))
                    .UseAzureMonitor();
            return true;
        }

        return false;
    }
}
