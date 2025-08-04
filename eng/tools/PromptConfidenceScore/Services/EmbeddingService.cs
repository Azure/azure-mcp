using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToolSelection.Models;

namespace ToolSelection.Services;

public class EmbeddingService
{
    private readonly HttpClient _httpClient;
    private readonly string _endpoint;
    private readonly string _apiKey;

    public EmbeddingService(HttpClient httpClient, string endpoint, string apiKey)
    {
        _httpClient = httpClient;
        _endpoint = endpoint;
        _apiKey = apiKey;
    }

    public async Task<float[]> CreateEmbeddingsAsync(string input)
    {
        var requestBody = new EmbeddingRequest
        {
            Input = new[] { input }
        };

        var json = JsonSerializer.Serialize(requestBody, SourceGenerationContext.Default.EmbeddingRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, _endpoint)
        {
            Content = content
        };

        request.Headers.Add("api-key", _apiKey);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var embeddingResponse = JsonSerializer.Deserialize(responseContent, SourceGenerationContext.Default.EmbeddingResponse);

        if (embeddingResponse?.Error != null)
        {
            throw new InvalidOperationException($"API error: {embeddingResponse.Error.Type} - {embeddingResponse.Error.Message}");
        }

        if (embeddingResponse?.Data == null || embeddingResponse.Data.Length == 0)
        {
            throw new InvalidOperationException($"No embedding data returned from API. Response: {responseContent}");
        }

        return embeddingResponse.Data[0].Embedding;
    }

    private class EmbeddingResponse
    {
        [JsonPropertyName("data")]
        public EmbeddingData[]? Data { get; set; }

        [JsonPropertyName("error")]
        public ApiError? Error { get; set; }
    }

    private class EmbeddingData
    {
        [JsonPropertyName("embedding")]
        public required float[] Embedding { get; set; }
    }

    private class ApiError
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
