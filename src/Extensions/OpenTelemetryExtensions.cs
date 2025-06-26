// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using Azure.Monitor.OpenTelemetry.Exporter;
using AzureMcp.Configuration;
using AzureMcp.Helpers;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace AzureMcp.Extensions;

public static class OpenTelemetryExtensions
{
    private const string DefaultAppInsights = "InstrumentationKey=21e003c0-efee-4d3f-8a98-1868515aa2c9;IngestionEndpoint=https://centralus-2.in.applicationinsights.azure.com/;LiveEndpoint=https://centralus.livediagnostics.monitor.azure.com/;ApplicationId=f14f6a2d-6405-4f88-bd58-056f25fe274f";

    public static void ConfigureOpenTelemetry(this IServiceCollection services)
    {
        if (!TryEnableAzureMonitor(services))
        {
            TryEnableOtlp(services);
        }
    }

    private static bool TryEnableAzureMonitor(this IServiceCollection services)
    {
        var isDebugging = Debugger.IsAttached;
        if (isDebugging)
        {
            services.AddSingleton(sp =>
            {
                var forwarder = new AzureEventSourceLogForwarder(sp.GetRequiredService<ILoggerFactory>());
                forwarder.Start();
                return forwarder;
            });
        }

        var appInsightsConnectionString = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING");

        if (string.IsNullOrEmpty(appInsightsConnectionString))
        {
            appInsightsConnectionString = DefaultAppInsights;
        }

        services.ConfigureOpenTelemetryTracerProvider((sp, builder) =>
        {
            var serverConfig = sp.GetRequiredService<IOptions<AzureMcpServerConfiguration>>();
            if (!serverConfig.Value.IsTelemetryEnabled)
            {
                return;
            }

            builder.AddSource(serverConfig.Value.Name);
        });

        services.AddOpenTelemetry()
            .ConfigureResource(r =>
            {
                var version = Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString();
                var address = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(x => x.OperationalStatus == OperationalStatus.Up)
                    .Select(x => x.GetPhysicalAddress().ToString())
                    .FirstOrDefault(string.Empty);
                var attributes = new List<KeyValuePair<string, object>>()
                {
                    new(TelemetryConstants.TagName.MacAddressHash, Sha256Helper.GetHashedValue(address))
                };

                r.AddService("azmcp", version)
                    .AddTelemetrySdk()
                    .AddAttributes(attributes);
            })
            .UseAzureMonitorExporter(options =>
            {
                options.EnableLiveMetrics = isDebugging;
                options.Diagnostics.IsLoggingEnabled = isDebugging;
                options.Diagnostics.IsLoggingContentEnabled = isDebugging;
                options.ConnectionString = appInsightsConnectionString;
            });

        return true;
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

}
