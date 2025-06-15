using System.Text.Json.Nodes;
using Azure.Core;

namespace AzureMcp.Services.Azure.Kusto;

public class KustoClient
{
    #region Private Members
    private readonly string _clusterUri;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TokenCredential _tokenCredential;
    private readonly static string s_application = "AzureMCP";
    private readonly static string s_clientRequestIdPrefix = "AzMcp";
    private readonly static string s_default_scope = "https://kusto.dev.kusto.windows.net" + "/.default";
    #endregion

    #region Public Methods
    public KustoClient(string clusterUri, IHttpClientFactory httpClientFactory, TokenCredential tokenCredential)
    {
        _clusterUri = clusterUri;
        _httpClientFactory = httpClientFactory;
        _tokenCredential = tokenCredential;
    }

    public async Task<KustoResult> ExecuteQueryAsync(string database, string text, CancellationToken cancellationToken)
    {
        var uri = _clusterUri + "/v1/rest/query";
        var httpRequest = await GenerateHttpRequestMessage(uri, database, text, cancellationToken).ConfigureAwait(false);

        using var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(_clusterUri);
        return await SendRequestAsync(httpClient, httpRequest, cancellationToken).ConfigureAwait(false);        
    }

    public async Task<KustoResult> ExecuteControlCommandAsync(string database, string text, CancellationToken cancellationToken)
    {
        var uri = _clusterUri + "/v1/rest/mgmt";
        var httpRequest = await GenerateHttpRequestMessage(uri, database, text, cancellationToken).ConfigureAwait(false);

        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(_clusterUri);
        return await SendRequestAsync(httpClient, httpRequest, cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region Private Methods
    private async Task<HttpRequestMessage> GenerateHttpRequestMessage(string uri, string database, string text, CancellationToken cancellationToken = default)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, uri);

        // Auth info
        var scopes = new string[]
        {
            s_default_scope
        };
        var clientRequestId = s_clientRequestIdPrefix + Guid.NewGuid().ToString();
        var tokenRequestContext = new TokenRequestContext(scopes, clientRequestId);
        var accessToken = await _tokenCredential.GetTokenAsync(tokenRequestContext, cancellationToken);
        httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken.Token);

        httpRequest.Headers.Add("x-ms-client-request-id", clientRequestId);
        httpRequest.Headers.Add("x-ms-app", s_application);
        httpRequest.Headers.Add("x-ms-client-version", "Kusto.Client.Light"); // Kusto.Dotnet.Client:{13.0.2}|Runtime:{.NETv4.8.1/CLRv4.0.30319.42000/4.8.9300.0_built_by:_NET481REL1LAST_C}
        httpRequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        // Body
        var body = new JsonObject();
        body.Add("db", database);
        body.Add("csl", text);
        var properties = new JsonObject();
        properties.Add("ClientRequestId", clientRequestId); // TODO: Also add this as a header?
        body.Add("properties", properties);
        var bodyStr = body.ToJsonString();
        httpRequest.Content = new StringContent(bodyStr);
        httpRequest.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json", "utf-8");
        return httpRequest;
    }

    private async Task<KustoResult> SendRequestAsync(HttpClient httpClient, HttpRequestMessage httpRequest, CancellationToken cancellationToken = default)
    {
        var httpCompletionOption = new HttpCompletionOption();
        var httpResponse = await httpClient.SendAsync(httpRequest, httpCompletionOption, cancellationToken);
        if (!httpResponse.IsSuccessStatusCode)
        {
            string errorContent = await httpResponse.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Request failed with status code {httpResponse.StatusCode}: {errorContent}");
        }
        return KustoResult.FromHttpResponseMessage(httpResponse);
    }
    #endregion
}
