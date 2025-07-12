// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Support.Commands.Ticket;
using AzureMcp.Areas.Support.Models;

namespace AzureMcp.Areas.Support.Commands;

[JsonSerializable(typeof(TicketListCommand.TicketListCommandResult))]
[JsonSerializable(typeof(SupportTicket))]
[JsonSerializable(typeof(ContactInformation))]
[JsonSerializable(typeof(FilterContext))]
[JsonSerializable(typeof(FilterProcessResult))]
[JsonSerializable(typeof(PropertyProcessResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class SupportJsonContext : JsonSerializerContext;
