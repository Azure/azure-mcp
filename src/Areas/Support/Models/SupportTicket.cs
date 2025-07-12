// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Support.Models;

public class SupportTicket
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? Severity { get; set; }
    public string? ServiceName { get; set; }
    public string? ProblemClassification { get; set; }
    public string? CreatedDate { get; set; }
    public string? ModifiedDate { get; set; }
    public ContactInformation? ContactDetails { get; set; }
}

public class ContactInformation
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PreferredContactMethod { get; set; }
    public string? PrimaryEmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PreferredTimeZone { get; set; }
    public string? Country { get; set; }
    public string? PreferredSupportLanguage { get; set; }
}
