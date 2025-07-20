using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ToolSelection.Models;
using ToolSelection.Services;
using ToolSelection.VectorDb;

namespace ToolSelection;

class Program
{
    private static readonly HttpClient HttpClient = new();

    static async Task Main(string[] args)
    {
        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        try
        {
            // Check if we're in CI mode (skip if credentials are missing)
            var isCiMode = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BUILD_BUILDID")) ||
                          !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITHUB_ACTIONS")) ||
                          args.Contains("--ci");

            // Load environment variables from .env file if it exists
            await LoadDotEnvFile();

            // Get configuration values
            var baseEndpoint = Environment.GetEnvironmentVariable("AOAI_ENDPOINT");
            if (string.IsNullOrEmpty(baseEndpoint))
            {
                if (isCiMode)
                {
                    Console.WriteLine("⏭️  Skipping tool selection analysis in CI - AOAI_ENDPOINT not configured");
                    Environment.Exit(0);
                }
                throw new InvalidOperationException("AOAI_ENDPOINT environment variable is required");
            }

            // Construct the full Azure OpenAI embeddings endpoint
            const string deploymentName = "text-embedding-3-large";
            const string apiVersion = "2024-02-01";

            // Remove trailing slash if present
            if (baseEndpoint.EndsWith("/"))
            {
                baseEndpoint = baseEndpoint.TrimEnd('/');
            }

            var endpoint = $"{baseEndpoint}/openai/deployments/{deploymentName}/embeddings?api-version={apiVersion}";

            var apiKey = await GetApiKeyAsync(isCiMode);
            if (apiKey == null && isCiMode)
            {
                Console.WriteLine("⏭️  Skipping tool selection analysis in CI - API key not available");
                Environment.Exit(0);
            }

            var embeddingService = new EmbeddingService(HttpClient, endpoint, apiKey!);

            // Load tools from JSON file
            var listToolsResult = await LoadToolsFromJsonAsync("list-tools.json", isCiMode);
            if (listToolsResult == null && isCiMode)
            {
                Console.WriteLine("⏭️  Skipping tool selection analysis in CI - tools data not available");
                Environment.Exit(0);
            }

            // Create vector database
            var db = new VectorDB(new CosineSimilarity());
            var stopwatch = Stopwatch.StartNew();

            await PopulateDatabaseAsync(db, listToolsResult!.Tools, embeddingService);
            stopwatch.Stop();

            var toolCount = db.Count;
            var executionTime = stopwatch.Elapsed;

            // Check if output should use markdown format
            var useMarkdown = IsMarkdownOutput();

            // Determine output file path
            var outputFilePath = useMarkdown ? "results.md" : "results.txt";

            // Add CI-friendly output
            if (isCiMode)
            {
                Console.WriteLine("🔍 Running tool selection analysis...");
                Console.WriteLine($"✅ Loaded {toolCount} tools in {executionTime.TotalSeconds:F2}s");
            }

            // Create or overwrite the output file
            using var writer = new StreamWriter(outputFilePath, false);

            if (useMarkdown)
            {
                await writer.WriteLineAsync("# Tool Selection Analysis Setup");
                await writer.WriteLineAsync();
                await writer.WriteLineAsync($"**Setup completed:** {DateTime.Now:yyyy-MM-dd HH:mm:ss}  ");
                await writer.WriteLineAsync($"**Tool count:** {toolCount}  ");
                await writer.WriteLineAsync($"**Database setup time:** {executionTime.TotalSeconds:F7}s  ");
                await writer.WriteLineAsync();
                await writer.WriteLineAsync("---");
                await writer.WriteLineAsync();
            }

            // Load prompts from JSON file
            var toolNameAndPrompts = await LoadPromptsFromJsonAsync("prompts.json", isCiMode);
            if (toolNameAndPrompts == null && isCiMode)
            {
                Console.WriteLine("⏭️  Skipping prompt testing in CI - prompts data not available");
                // Still write basic setup info to output file
                if (useMarkdown)
                {
                    await writer.WriteLineAsync("# Tool Selection Analysis Setup");
                    await writer.WriteLineAsync();
                    await writer.WriteLineAsync($"**Setup completed:** {DateTime.Now:yyyy-MM-dd HH:mm:ss}  ");
                    await writer.WriteLineAsync($"**Tool count:** {toolCount}  ");
                    await writer.WriteLineAsync($"**Database setup time:** {executionTime.TotalSeconds:F7}s  ");
                    await writer.WriteLineAsync();
                    await writer.WriteLineAsync("*Note: Prompt testing skipped in CI environment*");
                }
                else
                {
                    await writer.WriteLineAsync($"Tool count={toolCount}, Execution time={executionTime.TotalSeconds:F7}s");
                    await writer.WriteLineAsync("Note: Prompt testing skipped in CI environment");
                }
                return;
            }

            await RunPromptsAsync(db, toolNameAndPrompts!, embeddingService, executionTime, writer, isCiMode);

            // Print summary to console for immediate feedback
            if (isCiMode)
            {
                Console.WriteLine($"🎯 Tool selection analysis completed");
                Console.WriteLine($"📊 Results written to: {Path.GetFullPath(outputFilePath)}");
            }
            else
            {
                Console.WriteLine($"Tool count={toolCount}, Execution time={executionTime.TotalSeconds:F7}s");
                Console.WriteLine($"Results written to: {Path.GetFullPath(outputFilePath)}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");

            // In CI mode, provide more helpful error information
            var isCiMode = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BUILD_BUILDID")) ||
                          !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITHUB_ACTIONS")) ||
                          args.Contains("--ci");

            if (isCiMode)
            {
                Console.WriteLine("💡 Tip: This test requires Azure OpenAI configuration to run fully");
                Console.WriteLine("   Set AOAI_ENDPOINT and TEXT_EMBEDDING_API_KEY environment variables");
                Console.WriteLine("   or add test data files (list-tools.json, prompts.json) to skip API calls");
            }

            Environment.Exit(1);
        }
    }

    private static bool IsMarkdownOutput()
    {
        return string.Equals(Environment.GetEnvironmentVariable("output"), "md", StringComparison.OrdinalIgnoreCase);
    }

    private static async Task<string?> GetApiKeyAsync(bool isCiMode = false)
    {
        var apiKey = Environment.GetEnvironmentVariable("TEXT_EMBEDDING_API_KEY");
        if (!string.IsNullOrEmpty(apiKey))
        {
            return apiKey;
        }

        // Try to read from file as fallback
        var keyFilePath = "api-key.txt";
        if (File.Exists(keyFilePath))
        {
            return (await File.ReadAllTextAsync(keyFilePath)).Trim();
        }

        if (isCiMode)
        {
            return null; // Let caller handle this gracefully
        }

        throw new InvalidOperationException("API key not found. Please set TEXT_EMBEDDING_API_KEY environment variable or create api-key.txt file");
    }

    private static async Task LoadDotEnvFile()
    {
        var envFilePath = ".env";
        if (!File.Exists(envFilePath))
        {
            Console.WriteLine("No .env file found or error loading it");
            return;
        }

        var lines = await File.ReadAllLinesAsync(envFilePath);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                continue;

            var parts = line.Split('=', 2);
            if (parts.Length == 2)
            {
                Environment.SetEnvironmentVariable(parts[0].Trim(), parts[1].Trim());
            }
        }
    }

    private static async Task<ListToolsResult?> LoadToolsFromJsonAsync(string filePath, bool isCiMode = false)
    {
        if (!File.Exists(filePath))
        {
            if (isCiMode)
            {
                return null; // Let caller handle this gracefully
            }
            throw new FileNotFoundException($"Tools file not found: {filePath}");
        }

        var json = await File.ReadAllTextAsync(filePath);

        // Process the JSON
        if (json.StartsWith('\'') && json.EndsWith('\''))
        {
            json = json[1..^1]; // Remove first and last characters (quotes)
            json = json.Replace("\\'", "'"); // Convert \' --> '
            json = json.Replace("\\\\\"", "'"); // Convert \\" --> '
        }

        var result = JsonSerializer.Deserialize<ListToolsResult>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result ?? throw new InvalidOperationException("Failed to deserialize tools JSON");
    }

    private static async Task<Dictionary<string, List<string>>?> LoadPromptsFromJsonAsync(string filePath, bool isCiMode = false)
    {
        if (!File.Exists(filePath))
        {
            if (isCiMode)
            {
                return null; // Let caller handle this gracefully
            }
            throw new FileNotFoundException($"Prompts file not found: {filePath}");
        }

        var json = await File.ReadAllTextAsync(filePath);
        var prompts = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);
        return prompts ?? throw new InvalidOperationException($"Failed to parse prompts JSON from {filePath}");
    }

    private static async Task PopulateDatabaseAsync(VectorDB db, List<Tool> tools, EmbeddingService embeddingService)
    {
        const int threshold = 2;

        if (tools.Count > threshold)
        {
            int half = tools.Count / 2;
            var leftTask = Task.Run(() => PopulateDatabaseAsync(db, tools.Take(half).ToList(), embeddingService));
            await PopulateDatabaseAsync(db, tools.Skip(half).ToList(), embeddingService);
            await leftTask;
            return;
        }

        foreach (var tool in tools)
        {
            var input = tool.Description ?? "";
            var vector = await embeddingService.CreateEmbeddingsAsync(input);
            db.Upsert(new Entry(tool.Name, tool, vector));
        }
    }

    private static async Task RunPromptsAsync(VectorDB db, Dictionary<string, List<string>> toolNameWithPrompts, EmbeddingService embeddingService, TimeSpan databaseSetupTime, StreamWriter writer, bool isCiMode = false)
    {
        var stopwatch = Stopwatch.StartNew();
        int promptCount = 0;

        // Check if output should use markdown format
        var useMarkdown = IsMarkdownOutput();

        if (useMarkdown)
        {
            // Output markdown format
            await writer.WriteLineAsync("# Tool Selection Analysis Results");
            await writer.WriteLineAsync();
            await writer.WriteLineAsync($"**Analysis Date:** {DateTime.Now:yyyy-MM-dd HH:mm:ss}  ");
            await writer.WriteLineAsync($"**Total Tools:** {db.Count}  ");
            await writer.WriteLineAsync();
            await writer.WriteLineAsync("## Table of Contents");
            await writer.WriteLineAsync();

            // Generate TOC
            int toolIndex = 1;
            foreach (var (toolName, prompts) in toolNameWithPrompts)
            {
                foreach (var _ in prompts)
                {
                    await writer.WriteLineAsync($"- [Test {toolIndex}: {toolName}](#test-{toolIndex})");
                    toolIndex++;
                }
            }
            await writer.WriteLineAsync();
            await writer.WriteLineAsync("---");
            await writer.WriteLineAsync();
        }
        else
        {
            await writer.WriteLineAsync($"Tool count={db.Count}, Execution time={databaseSetupTime.TotalSeconds:F7}s");
            await writer.WriteLineAsync();
        }

        int testNumber = 1;
        foreach (var (toolName, prompts) in toolNameWithPrompts)
        {
            foreach (var prompt in prompts)
            {
                promptCount++;

                if (useMarkdown)
                {
                    // Markdown format
                    await writer.WriteLineAsync($"## Test {testNumber}");
                    await writer.WriteLineAsync();
                    await writer.WriteLineAsync($"**Expected Tool:** `{toolName}`  ");
                    await writer.WriteLineAsync($"**Prompt:** {prompt}  ");
                    await writer.WriteLineAsync();
                    await writer.WriteLineAsync("### Results");
                    await writer.WriteLineAsync();
                    await writer.WriteLineAsync("| Rank | Score | Tool | Status |");
                    await writer.WriteLineAsync("|------|-------|------|--------|");
                }
                else
                {
                    // Original terminal format
                    await writer.WriteLineAsync($"\nPrompt: {prompt}");
                    await writer.WriteLineAsync($"Expected tool: {toolName}");
                }

                var vector = await embeddingService.CreateEmbeddingsAsync(prompt);
                var queryResults = db.Query(vector, new QueryOptions(TopK: 10));

                for (int i = 0; i < queryResults.Count; i++)
                {
                    var qr = queryResults[i];
                    if (useMarkdown)
                    {
                        var status = qr.Entry.Id == toolName ? "✅ **EXPECTED**" : "❌";
                        await writer.WriteLineAsync($"| {i + 1} | {qr.Score:F6} | `{qr.Entry.Id}` | {status} |");
                    }
                    else
                    {
                        var note = qr.Entry.Id == toolName ? "*** EXPECTED ***" : "";
                        await writer.WriteLineAsync($"   {qr.Score:F6}   {qr.Entry.Id,-50}     {note}");
                    }
                }

                if (useMarkdown)
                {
                    await writer.WriteLineAsync();
                    await writer.WriteLineAsync("---");
                    await writer.WriteLineAsync();
                }

                testNumber++;
            }
        }

        stopwatch.Stop();

        if (useMarkdown)
        {
            await writer.WriteLineAsync("## Summary");
            await writer.WriteLineAsync();
            await writer.WriteLineAsync($"**Total Prompts Tested:** {promptCount}  ");
            await writer.WriteLineAsync($"**Execution Time:** {stopwatch.Elapsed.TotalSeconds:F7}s  ");
            await writer.WriteLineAsync();

            // Calculate success rate
            var successfulTests = await CalculateSuccessRateAsync(db, toolNameWithPrompts, embeddingService);
            var successRate = (double)successfulTests / promptCount * 100;
            await writer.WriteLineAsync($"**Success Rate:** {successRate:F1}% ({successfulTests}/{promptCount} tests passed)  ");
            await writer.WriteLineAsync();

            await writer.WriteLineAsync("### Success Rate Analysis");
            await writer.WriteLineAsync();
            if (successRate >= 90)
            {
                await writer.WriteLineAsync("🟢 **Excellent** - The tool selection system is performing very well.");
            }
            else if (successRate >= 75)
            {
                await writer.WriteLineAsync("🟡 **Good** - The tool selection system is performing adequately but has room for improvement.");
            }
            else if (successRate >= 50)
            {
                await writer.WriteLineAsync("🟠 **Fair** - The tool selection system needs significant improvement.");
            }
            else
            {
                await writer.WriteLineAsync("🔴 **Poor** - The tool selection system requires major improvements.");
            }
            await writer.WriteLineAsync();
        }
        else
        {
            // Calculate success rate for regular format too
            var successfulTests = await CalculateSuccessRateAsync(db, toolNameWithPrompts, embeddingService);
            var successRate = (double)successfulTests / promptCount * 100;

            await writer.WriteLineAsync($"\n\nPrompt count={promptCount}, Execution time={stopwatch.Elapsed.TotalSeconds:F7}s");
            await writer.WriteLineAsync($"Success rate={successRate:F1}% ({successfulTests}/{promptCount} tests passed)");
        }

        // Calculate success rate for console output
        var successfulTestsForConsole = await CalculateSuccessRateAsync(db, toolNameWithPrompts, embeddingService);
        var successRateForConsole = (double)successfulTestsForConsole / promptCount * 100;

        // Print summary to console for feedback
        if (isCiMode)
        {
            Console.WriteLine($"🧪 Tested {promptCount} prompts with {successRateForConsole:F1}% success rate");
        }
        else
        {
            Console.WriteLine($"Prompt count={promptCount}, Execution time={stopwatch.Elapsed.TotalSeconds:F7}s");
        }
    }

    private static async Task<int> CalculateSuccessRateAsync(VectorDB db, Dictionary<string, List<string>> toolNameWithPrompts, EmbeddingService embeddingService)
    {
        int successfulTests = 0;
        foreach (var (toolName, prompts) in toolNameWithPrompts)
        {
            foreach (var prompt in prompts)
            {
                var vector = await embeddingService.CreateEmbeddingsAsync(prompt);
                var queryResults = db.Query(vector, new QueryOptions(TopK: 1));
                if (queryResults.Count > 0 && queryResults[0].Entry.Id == toolName)
                {
                    successfulTests++;
                }
            }
        }
        return successfulTests;
    }
}
