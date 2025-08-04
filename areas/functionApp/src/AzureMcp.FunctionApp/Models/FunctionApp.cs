// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.FunctionApp.Models;

public class FunctionAppModel
{
    /// <summary> Name of function app resource. </summary>
    public string? Name { get; set; }

    /// <summary> ID of the Azure subscription containing the function app resource. </summary>
    public string? SubscriptionId {get; set; }

    /// <summary> Name of the resource group containing the function app resource. </summary>
    public string? ResourceGroupName { get; set; }

    /// <summary> Azure geolocation where the function app resource resides. </summary>
    public string? Location { get; set; }

    /// <summary> App service plan name used by the function app resource. </summary>
    public string? AppServicePlanName { get; set; }

    /// <summary> Status of the function app resource. </summary>
    public string? Status { get; set; }

    /// <summary> Default host name of the function app resource. </summary>
    public string? DefaultHostName { get; set; }

    /// <summary> Resource tags associated with the function app resource. </summary>
    public IDictionary<string, string>? Tags { get; set; }
}
