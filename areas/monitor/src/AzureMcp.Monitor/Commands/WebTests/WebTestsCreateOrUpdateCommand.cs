// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Models.Monitor.WebTests;
using AzureMcp.Monitor.Options;
using AzureMcp.Monitor.Options.WebTests;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Monitor.Commands.WebTests;

public sealed class WebTestsCreateOrUpdateCommand(ILogger<WebTestsCreateOrUpdateCommand> logger) : BaseMonitorWebTestsCommand<WebTestsCreateOrUpdateOptions>
{
    private const string _commandTitle = "Create or updates a web test in Azure Monitor";
    private const string _commandName = "createorupdate";

    private readonly ILogger<WebTestsCreateOrUpdateCommand> _logger = logger;

    private readonly Option<string> _resourceNameOption = MonitorOptionDefinitions.WebTest.WebTestResourceName;
    private readonly Option<string> _appInsightsComponentIdOption = MonitorOptionDefinitions.WebTest.AppInsightsComponentId;
    private readonly Option<string> _resourceLocationOption = MonitorOptionDefinitions.WebTest.ResourceLocation;
    private readonly Option<string> _locationsOption = MonitorOptionDefinitions.WebTest.Locations;
    private readonly Option<string> _requestUrlOption = MonitorOptionDefinitions.WebTest.RequestUrl;

    private readonly Option<string> _webTestNameOption = MonitorOptionDefinitions.WebTest.WebTestName;
    private readonly Option<string> _descriptionOption = MonitorOptionDefinitions.WebTest.Description;
    private readonly Option<bool> _enabledOption = MonitorOptionDefinitions.WebTest.Enabled;
    private readonly Option<int> _expectedStatusCodeOption = MonitorOptionDefinitions.WebTest.ExpectedStatusCode;
    private readonly Option<bool> _followRedirectsOption = MonitorOptionDefinitions.WebTest.FollowRedirects;
    private readonly Option<int> _frequencyInSecondsOption = MonitorOptionDefinitions.WebTest.FrequencyInSeconds;
    private readonly Option<string> _headersOption = MonitorOptionDefinitions.WebTest.Headers;
    private readonly Option<string> _httpVerbOption = MonitorOptionDefinitions.WebTest.HttpVerb;
    private readonly Option<bool> _ignoreStatusCodeOption = MonitorOptionDefinitions.WebTest.IgnoreStatusCode;
    private readonly Option<bool> _parseRequestsOption = MonitorOptionDefinitions.WebTest.ParseRequests;
    private readonly Option<string> _requestBodyOption = MonitorOptionDefinitions.WebTest.RequestBody;
    private readonly Option<bool> _retryEnabledOption = MonitorOptionDefinitions.WebTest.RetryEnabled;
    private readonly Option<bool> _sslCheckOption = MonitorOptionDefinitions.WebTest.SslCheck;
    private readonly Option<int> _sslLifetimeCheckInDaysOption = MonitorOptionDefinitions.WebTest.SslLifetimeCheckInDays;
    private readonly Option<int> _timeoutInSecondsOption = MonitorOptionDefinitions.WebTest.TimeoutInSeconds;

    public override string Name => _commandName;

    public override string Description =>
        """
        Creates a new standard web test in Azure Monitor or updates an existing one. Ping/Multstep web tests are deprecated and are not supported.
        Returns the created/updated web test details.
        """;

    public override string Title => _commandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = false };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);

        // Add required options
        command.AddOption(_resourceNameOption);
        command.AddOption(_resourceGroupOption);
        command.AddOption(_appInsightsComponentIdOption);
        command.AddOption(_resourceLocationOption);
        command.AddOption(_locationsOption);
        command.AddOption(_requestUrlOption);

        // Add optional options
        command.AddOption(_webTestNameOption);
        command.AddOption(_descriptionOption);
        command.AddOption(_enabledOption);
        command.AddOption(_expectedStatusCodeOption);
        command.AddOption(_followRedirectsOption);
        command.AddOption(_frequencyInSecondsOption);
        command.AddOption(_headersOption);
        command.AddOption(_httpVerbOption);
        command.AddOption(_ignoreStatusCodeOption);
        command.AddOption(_parseRequestsOption);
        command.AddOption(_requestBodyOption);
        command.AddOption(_retryEnabledOption);
        command.AddOption(_sslCheckOption);
        command.AddOption(_sslLifetimeCheckInDaysOption);
        command.AddOption(_timeoutInSecondsOption);
    }

    protected override WebTestsCreateOrUpdateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);

        options.ResourceName = parseResult.GetValueForOption(_resourceNameOption)!;
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption)!;
        options.AppInsightsComponentId = parseResult.GetValueForOption(_appInsightsComponentIdOption)!;
        options.Location = parseResult.GetValueForOption(_resourceLocationOption)!;
        options.Locations = parseResult.GetValueForOption(_locationsOption)!;
        options.RequestUrl = parseResult.GetValueForOption(_requestUrlOption)!;

        options.WebTestName = parseResult.GetValueForOption(_webTestNameOption);
        options.Description = parseResult.GetValueForOption(_descriptionOption);
        options.Enabled = parseResult.GetValueForOption(_enabledOption);
        options.ExpectedStatusCode = parseResult.GetValueForOption(_expectedStatusCodeOption);
        options.FollowRedirects = parseResult.GetValueForOption(_followRedirectsOption);
        options.FrequencyInSeconds = parseResult.GetValueForOption(_frequencyInSecondsOption);
        options.Headers = parseResult.GetValueForOption(_headersOption);
        options.HttpVerb = parseResult.GetValueForOption(_httpVerbOption);
        options.IgnoreStatusCode = parseResult.GetValueForOption(_ignoreStatusCodeOption);
        options.ParseRequests = parseResult.GetValueForOption(_parseRequestsOption);
        options.RequestBody = parseResult.GetValueForOption(_requestBodyOption);
        options.RetryEnabled = parseResult.GetValueForOption(_retryEnabledOption);
        options.SslCheck = parseResult.GetValueForOption(_sslCheckOption);
        options.SslLifetimeCheckInDays = parseResult.GetValueForOption(_sslLifetimeCheckInDaysOption);
        options.TimeoutInSeconds = parseResult.GetValueForOption(_timeoutInSecondsOption);

        return options;
    }

    public override ValidationResult Validate(CommandResult commandResult, CommandResponse? response)
    {
        if (response == null)
        {
            return new ValidationResult { IsValid = false };
        }

        var baseValidation = base.Validate(commandResult, response);
        if (!baseValidation.IsValid)
        {
            return baseValidation;
        }

        var locations = commandResult.GetValueForOption(_locationsOption);
        if (locations == null || locations.Length == 0)
        {
            response.Status = 400;
            response.Message = "The locations option is required and must include at least one location.";
            return new ValidationResult { IsValid = false };
        }

        var requestUrl = commandResult.GetValueForOption(_requestUrlOption);
        if (!Uri.TryCreate(requestUrl, UriKind.Absolute, out _))
        {
            response.Status = 400;
            response.Message = "The request url option must be a valid absolute URL.";
            return new ValidationResult { IsValid = false };
        }

        var timeoutInSeconds = commandResult.GetValueForOption(_timeoutInSecondsOption);
        if (timeoutInSeconds > 120)
        {
            response.Status = 400;
            response.Message = "The timeout cannot be greater than 2 minutes.";
            return new ValidationResult { IsValid = false };
        }

        return new ValidationResult { IsValid = true };
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var locationsArray = options.Locations!.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var headersDictionary = options.Headers?
                .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split('=', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                .Where(x => x.Length == 2)
                .ToDictionary(x => x[0], x => x[1]) ?? new Dictionary<string, string>(0);

            var monitorWebTestService = context.GetService<IMonitorWebTestService>();
            var webTest = await monitorWebTestService.CreateOrUpdateWebTest(
                subscription: options.Subscription!,
                resourceGroup: options.ResourceGroup!,
                resourceName: options.ResourceName!,
                appInsightsComponentId: options.AppInsightsComponentId!,
                location: options.Location!,
                locations: locationsArray,
                requestUrl: options.RequestUrl!,
                webTestName: options.WebTestName,
                description: options.Description,
                enabled: options.Enabled,
                expectedStatusCode: options.ExpectedStatusCode,
                followRedirects: options.FollowRedirects,
                frequencyInSeconds: options.FrequencyInSeconds,
                headers: headersDictionary,
                httpVerb: options.HttpVerb,
                ignoreStatusCode: options.IgnoreStatusCode,
                parseRequests: options.ParseRequests,
                requestBody: options.RequestBody,
                retryEnabled: options.RetryEnabled,
                sslCheck: options.SslCheck,
                sslLifetimeCheckInDays: options.SslLifetimeCheckInDays,
                timeoutInSeconds: options.TimeoutInSeconds,
                tenant: options.Tenant,
                retryPolicy: options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new WebTestsCreateCommandResult(webTest),
                MonitorJsonContext.Default.WebTestsCreateCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating web test '{WebTestName}' in resource group '{ResourceGroup}'",
                options.WebTestName ?? options.ResourceName, options.ResourceGroup);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record WebTestsCreateCommandResult(WebTestDetailedInfo WebTest);
}
