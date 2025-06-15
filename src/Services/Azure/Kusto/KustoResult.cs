namespace AzureMcp.Services.Azure.Kusto;

public class KustoResult : IDisposable
{
    public JsonDocument JsonDocument { get; private set; }

    public static KustoResult FromHttpResponseMessage(HttpResponseMessage response)
    {
        var ret = new KustoResult();
        var stream = response.Content.ReadAsStream();
        var jsonDocument = JsonDocument.Parse(stream);
        ret.JsonDocument = jsonDocument;

        // TODO: Surface out various properties of the result, such as ClientRequestId etc.
        return ret;
    }

    public void Dispose()
    {
    }
}
