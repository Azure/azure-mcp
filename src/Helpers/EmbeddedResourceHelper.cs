// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;

namespace AzureMcp.Helpers
{
    public static class EmbeddedResourceHelper
    {
        /// <summary>
        /// Reads the content of an embedded resource file as a string.
        /// </summary>
        /// <param name="assembly">The assembly containing the embedded resource.</param>
        /// <param name="resourceName">The full name of the embedded resource.</param>
        /// <returns>The content of the embedded resource as a string.</returns>
        public static string ReadEmbeddedResource(Assembly assembly, string resourceName)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Unable to load embedded resource: {resourceName}");

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
