// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Support.Options.Ticket;

public class TicketListOptions : BaseSupportOptions
{
    public string? Filter { get; set; }
    public int? Top { get; set; }
}
