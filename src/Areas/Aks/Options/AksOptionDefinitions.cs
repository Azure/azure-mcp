// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Aks.Options;

public static class AksOptionDefinitions
{
    public const string ClusterName = "cluster-name";

    public static readonly Option<string> Cluster = new(
        $"--{ClusterName}",
        "AKS Cluster name."
    )
    {
        IsRequired = true
    };
}
