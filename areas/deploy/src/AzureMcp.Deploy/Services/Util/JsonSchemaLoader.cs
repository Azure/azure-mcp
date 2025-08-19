// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using AzureMcp.Core.Helpers;

namespace AzureMcp.Deploy.Services.Util;

public static class JsonSchemaLoader
{
    public static string LoadAppTopologyJsonSchema()
    {
        return LoadFileText("deploy-app-topology-schema.json");
    }

    private static string LoadFileText(string resourceFileName)
    {
        Assembly assembly = typeof(JsonSchemaLoader).Assembly;
        string resourceName = EmbeddedResourceHelper.FindEmbeddedResource(assembly, resourceFileName);
        return EmbeddedResourceHelper.ReadEmbeddedResource(assembly, resourceName);
    }
}
