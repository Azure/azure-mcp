using AzureMcp.Services.Interfaces;
using AzureMcp.Arguments;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace AzureMcp.Services.Azure.Foundry;

public class FoundryService : IFoundryService
{
    private static readonly HttpClient HttpClient = new HttpClient();

    public async Task<List<object>> ListModels(
        RetryPolicyArguments? retryPolicy = null)
    {
        // Fetch models from foundry catalog
        string foundryUrl = "https://api.catalog.azureml.ms/asset-gallery/v1.0/models";
        var body = new
        {
            filters = new[]
            {
                new { field = "azureOffers", values = new[] { "standard-paygo" }, @operator = "eq" },
                new { field = "freePlayground", values = new[] { "true" }, @operator = "eq" },
                new { field = "labels", values = new[] { "latest" }, @operator = "eq" }
            }
        };

        var jsonBody = JsonSerializer.Serialize(body);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync(foundryUrl, content);

        Console.WriteLine($"Response: {response.StatusCode}");
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var resJson = JsonSerializer.Deserialize<JsonElement>(responseContent);

            var textModels = new List<object>();

            foreach (var summary in resJson.GetProperty("summaries").EnumerateArray())
            {
                var modelLimits = summary.GetProperty("modelLimits");
                if (modelLimits.GetProperty("supportedOutputModalities").EnumerateArray().Any(m => m.GetString() == "text") &&
                    modelLimits.GetProperty("supportedInputModalities").EnumerateArray().Any(m => m.GetString() == "text"))
                {
                    var textModel = new
                    {
                        name = summary.GetProperty("name").GetString(),
                        inference_model_name = summary.GetProperty("publisher").GetString().Replace(" ", "-") + "/" + summary.GetProperty("name").GetString(),
                        summary = summary.GetProperty("summary").GetString()
                    };
                    textModels.Add(textModel);
                }
            }

            return textModels;
        }
        else
        {
            throw new Exception($"Failed to fetch models: {response.ReasonPhrase}");
        }
    }

    public async Task<string> GetModelGuidance(
        string inferenceModelName,
        RetryPolicyArguments? retryPolicy = null)
    {
        try
        {

            string guidanceFilePath = Path.Combine(AppContext.BaseDirectory, "Services/Azure/Foundry/GuidanceDoc.md");
            string guidance = await File.ReadAllTextAsync(guidanceFilePath);
            
            // Replace placeholder with model name
            guidance = guidance.Replace("{{inference_model_name}}", inferenceModelName);
            
            return guidance;
        }
        catch (Exception e)
        {
            // If file reading fails, return a default message
            return $"Guidance file not found. Please check the path. Error: {e.Message}";
        }
    }
}