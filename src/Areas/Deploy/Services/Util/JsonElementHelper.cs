namespace Areas.Server.Commands.Tools.DeployTools.Util;

public static class JsonElementHelper
{
    public static string GetStringSafe(this JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.String => element.GetString() ?? string.Empty,
            JsonValueKind.Undefined => string.Empty,
            JsonValueKind.Null => string.Empty,
            _ => string.Empty
        };
    }
}
