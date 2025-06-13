
namespace AzureMcp.Models.Monitor
{
    public class LogsQueryTable
    {
        public List<Dictionary<string, object>> Rows { get; set; } = new List<Dictionary<string, object>>();
    }
}
