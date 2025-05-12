using System.Text.Json;
using Xunit;
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Tests.Client.Helpers
{
    public class LiveTestSettingsFixture : IAsyncLifetime
    {
        public LiveTestSettings Settings { get; private set; } = new();

        public virtual async ValueTask InitializeAsync()
        {
            var testSettingsFileName = ".testsettings.json";
            var directory = Path.GetDirectoryName(typeof(CommandTests).Assembly.Location);
            while (!string.IsNullOrEmpty(directory))
            {
                var testSettingsFilePath = Path.Combine(directory, testSettingsFileName);
                if (File.Exists(testSettingsFilePath))
                {
                    var content = await File.ReadAllTextAsync(testSettingsFilePath);

                    Settings = JsonSerializer.Deserialize<LiveTestSettings>(content)
                        ?? throw new Exception("Unable to deserialize live test settings");

                    Settings.SettingsDirectory = directory;

                    return;
                }

                directory = Path.GetDirectoryName(directory);
            }

            throw new FileNotFoundException($"Test settings file '{testSettingsFileName}' not found in the assembly directory or its parent directories.");
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
