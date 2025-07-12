// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Support;
using AzureMcp.Areas.Support.Models;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;

namespace AzureMcp.Areas.Support.Services;

public class SupportService(ISubscriptionService subscriptionService, ITenantService tenantService, ISupportFilterProcessor filterProcessor)
    : BaseAzureService(tenantService), ISupportService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ISupportFilterProcessor _filterProcessor = filterProcessor ?? throw new ArgumentNullException(nameof(filterProcessor));

    public async Task<List<SupportTicket>> ListSupportTickets(
        string subscription,
        string? filter = null,
        int? top = null,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);
        
        // Process filter using new filtering architecture
        string? processedFilter = null;
        if (!string.IsNullOrEmpty(filter))
        {
            var filterContext = new FilterContext(tenantId, retryPolicy);

            var filterResult = await _filterProcessor.ProcessFilterAsync(filter, filterContext);
            if (!filterResult.IsSuccess)
            {
                throw new ArgumentException($"Filter processing failed: {filterResult.ErrorMessage}");
            }
            processedFilter = string.IsNullOrEmpty(filterResult.ProcessedFilter) ? null : filterResult.ProcessedFilter;
        }

        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenantId, retryPolicy);
            var supportTickets = subscriptionResource.GetSubscriptionSupportTickets();
            
            var tickets = new List<SupportTicket>();
            
            // Use native Azure SDK filtering capabilities
            var asyncEnumerable = supportTickets.GetAllAsync(top: top, filter: processedFilter);

            await foreach (var ticketResource in asyncEnumerable)
            {
                var ticket = new SupportTicket
                {
                    Id = ticketResource.Id?.ToString(),
                    Name = ticketResource.Data?.Name,
                    Title = ticketResource.Data?.Title,
                    Description = ticketResource.Data?.Description,
                    Status = ticketResource.Data?.Status?.ToString(),
                    Severity = ticketResource.Data?.Severity.ToString(),
                    ServiceName = ticketResource.Data?.ServiceDisplayName,
                    ProblemClassification = ticketResource.Data?.ProblemClassificationDisplayName,
                    CreatedDate = ticketResource.Data?.CreatedOn?.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    ModifiedDate = ticketResource.Data?.ModifiedOn?.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };

                // Add contact information if available
                if (ticketResource.Data?.ContactDetails != null)
                {
                    ticket.ContactDetails = new ContactInformation
                    {
                        FirstName = ticketResource.Data.ContactDetails.FirstName,
                        LastName = ticketResource.Data.ContactDetails.LastName,
                        PreferredContactMethod = ticketResource.Data.ContactDetails.PreferredContactMethod.ToString(),
                        PrimaryEmailAddress = ticketResource.Data.ContactDetails.PrimaryEmailAddress,
                        PhoneNumber = ticketResource.Data.ContactDetails.PhoneNumber,
                        PreferredTimeZone = ticketResource.Data.ContactDetails.PreferredTimeZone,
                        Country = ticketResource.Data.ContactDetails.Country,
                        PreferredSupportLanguage = ticketResource.Data.ContactDetails.PreferredSupportLanguage
                    };
                }

                tickets.Add(ticket);
            }

            return tickets;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to list support tickets: {ex.Message}", ex);
        }
    }
}
