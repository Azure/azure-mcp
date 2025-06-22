// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using AzureMcp.Areas.Marketplace.Commands;
using AzureMcp.Areas.Marketplace.Options.Product;
using AzureMcp.Areas.Marketplace.Services;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Marketplace.Commands.Product;

public sealed class ProductGetCommand(ILogger<ProductGetCommand> logger) : SubscriptionCommand<ProductGetOptions>
{
    private const string CommandTitle = "Get Marketplace Product";
    private readonly ILogger<ProductGetCommand> _logger = logger;

    // Define options from OptionDefinitions
    private readonly Option<string> _productIdOption = OptionDefinitions.Marketplace.ProductId;
    private readonly Option<bool> _includeStopSoldPlansOption = OptionDefinitions.Marketplace.IncludeStopSoldPlans;
    private readonly Option<string> _languageOption = OptionDefinitions.Marketplace.Language;
    private readonly Option<string> _marketOption = OptionDefinitions.Marketplace.Market;
    private readonly Option<bool> _lookupOfferInTenantLevelOption = OptionDefinitions.Marketplace.LookupOfferInTenantLevel;
    private readonly Option<bool> _includeHiddenPlansOption = OptionDefinitions.Marketplace.IncludeHiddenPlans;
    private readonly Option<string> _planIdOption = OptionDefinitions.Marketplace.PlanId;
    private readonly Option<string> _skuIdOption = OptionDefinitions.Marketplace.SkuId;
    private readonly Option<bool> _includeServiceInstructionTemplatesOption = OptionDefinitions.Marketplace.IncludeServiceInstructionTemplates;
    private readonly Option<string> _partnerTenantIdOption = OptionDefinitions.Marketplace.PartnerTenantId;
    private readonly Option<string> _pricingAudienceOption = OptionDefinitions.Marketplace.PricingAudience;
    private readonly Option<string> _objectIdOption = OptionDefinitions.Marketplace.ObjectId;
    private readonly Option<string> _altSecIdOption = OptionDefinitions.Marketplace.AltSecId;
    public override string Name => "get";

    public override string Description =>
        $"""
        Retrieves a single private product (offer) for a given subscription from Azure Marketplace.
        Returns detailed information about the specified marketplace product including plans, pricing, and metadata.
        
        Required options:
        - --{OptionDefinitions.Common.SubscriptionName}: Azure subscription ID
        - --{OptionDefinitions.Marketplace.ProductIdName}: Product ID to retrieve
        
        Optional filtering options:
        - --{OptionDefinitions.Marketplace.LanguageName}: Product language (default: en)
        - --{OptionDefinitions.Marketplace.MarketName}: Product market (default: US)
        - --{OptionDefinitions.Marketplace.PlanIdName}: Filter by specific plan ID
        - --{OptionDefinitions.Marketplace.SkuIdName}: Filter by specific SKU ID
        
        Optional inclusion options:
        - --{OptionDefinitions.Marketplace.IncludeStopSoldPlansName}: Include stop-sold plans
        - --{OptionDefinitions.Marketplace.IncludeHiddenPlansName}: Include hidden plans
        - --{OptionDefinitions.Marketplace.IncludeServiceInstructionTemplatesName}: Include service instruction templates
        - --{OptionDefinitions.Marketplace.LookupOfferInTenantLevelName}: Check tenant private audience
        
        Optional header options:
        - --{OptionDefinitions.Marketplace.PartnerTenantIdName}: Partner tenant ID
        - --{OptionDefinitions.Marketplace.PricingAudienceName}: Pricing audience
        - --{OptionDefinitions.Marketplace.ObjectIdName}: AAD user ID
        - --{OptionDefinitions.Marketplace.AltSecIdName}: Alternate Security ID (for MSA users)
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_productIdOption);
        command.AddOption(_includeStopSoldPlansOption);
        command.AddOption(_languageOption);
        command.AddOption(_marketOption);
        command.AddOption(_lookupOfferInTenantLevelOption);
        command.AddOption(_includeHiddenPlansOption);
        command.AddOption(_planIdOption);
        command.AddOption(_skuIdOption);
        command.AddOption(_includeServiceInstructionTemplatesOption);
        command.AddOption(_partnerTenantIdOption);
        command.AddOption(_pricingAudienceOption);
        command.AddOption(_objectIdOption);
        command.AddOption(_altSecIdOption);
    }

    protected override ProductGetOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ProductId = parseResult.GetValueForOption(_productIdOption);
        options.IncludeStopSoldPlans = parseResult.GetValueForOption(_includeStopSoldPlansOption);
        options.Language = parseResult.GetValueForOption(_languageOption);
        options.Market = parseResult.GetValueForOption(_marketOption);
        options.LookupOfferInTenantLevel = parseResult.GetValueForOption(_lookupOfferInTenantLevelOption);
        options.IncludeHiddenPlans = parseResult.GetValueForOption(_includeHiddenPlansOption);
        options.PlanId = parseResult.GetValueForOption(_planIdOption);
        options.SkuId = parseResult.GetValueForOption(_skuIdOption);
        options.IncludeServiceInstructionTemplates = parseResult.GetValueForOption(_includeServiceInstructionTemplatesOption);
        options.PartnerTenantId = parseResult.GetValueForOption(_partnerTenantIdOption);
        options.PricingAudience = parseResult.GetValueForOption(_pricingAudienceOption);
        options.ObjectId = parseResult.GetValueForOption(_objectIdOption);
        options.AltSecId = parseResult.GetValueForOption(_altSecIdOption);
        return options;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            // Required validation step
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            // Get the marketplace service from DI
            var marketplaceService = context.GetService<IMarketplaceService>();

            // Call service operation with required parameters
            var result = await marketplaceService.GetProduct(
                options.ProductId!,
                options.Subscription!,
                options.IncludeStopSoldPlans,
                options.Language,
                options.Market,
                options.LookupOfferInTenantLevel,
                options.IncludeHiddenPlans,
                options.PlanId,
                options.SkuId,
                options.IncludeServiceInstructionTemplates,
                options.PartnerTenantId,
                options.PricingAudience,
                options.ObjectId,
                options.AltSecId,
                options.Tenant,
                options.RetryPolicy);

            // Set results
            context.Response.Results = result != null ?
                ResponseResult.Create(
                    new ProductGetCommandResult(result),
                    MarketplaceJsonContext.Default.ProductGetCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            // Log error with all relevant context
            _logger.LogError(ex,
                "Error getting marketplace product. ProductId: {ProductId}, Subscription: {Subscription}, Options: {@Options}",
                options.ProductId, options.Subscription, options);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    // Implementation-specific error handling
    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        HttpRequestException httpEx when httpEx.Message.Contains("404") =>
            "Marketplace product not found. Verify the product ID exists and you have access to it.",
        HttpRequestException httpEx when httpEx.Message.Contains("403") =>
            "Authorization failed accessing the marketplace product. Verify your permissions and subscription access.",
        HttpRequestException httpEx when httpEx.Message.Contains("401") =>
            "Authentication failed. Please run 'az login' to sign in or verify your credentials.",
        HttpRequestException httpEx =>
            $"Service unavailable or connectivity issues. Details: {httpEx.Message}",
        ArgumentException argEx =>
            $"Invalid parameter provided. Details: {argEx.Message}",
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        HttpRequestException httpEx when httpEx.Message.Contains("404") => 404,
        HttpRequestException httpEx when httpEx.Message.Contains("403") => 403,
        HttpRequestException httpEx when httpEx.Message.Contains("401") => 401,
        HttpRequestException => 503,
        ArgumentException => 400,
        _ => base.GetStatusCode(ex)
    };

    // Strongly-typed result record
    internal record ProductGetCommandResult(JsonNode Product);
}
