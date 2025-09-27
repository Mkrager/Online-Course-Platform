using System.Text.Json;

namespace OnlineCoursePlatform.App.Helpers
{
    public static class JsonErrorHelper
    {
        public static string GetErrorMessage(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return "Unknown error";

            try
            {
                using JsonDocument doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.ValueKind == JsonValueKind.Array)
                {
                    var messages = root.EnumerateArray()
                        .Where(e => e.ValueKind == JsonValueKind.String)
                        .Select(e => e.GetString())
                        .Where(msg => !string.IsNullOrWhiteSpace(msg))
                        .ToList();

                    return string.Join("; ", messages);
                }

                if (root.ValueKind == JsonValueKind.Object)
                {
                    if (root.TryGetProperty("errors", out var errorsProp))
                    {
                        var messages = new List<string>();
                        foreach (var error in errorsProp.EnumerateObject())
                        {
                            foreach (var msg in error.Value.EnumerateArray())
                            {
                                if (msg.ValueKind == JsonValueKind.String)
                                    messages.Add(msg.GetString());
                            }
                        }
                        return string.Join("; ", messages);
                    }

                    if (root.TryGetProperty("error", out var errorProp))
                        return errorProp.ToString();

                    if (root.TryGetProperty("message", out var messageProp))
                        return messageProp.GetString();

                    if (root.TryGetProperty("detail", out var detailProp))
                        return detailProp.GetString();

                    if (root.TryGetProperty("title", out var titleProp))
                        return titleProp.GetString();
                }

                if (root.ValueKind == JsonValueKind.String)
                {
                    return root.GetString();
                }

                return json;
            }
            catch (JsonException)
            {
                return json.Trim('"');
            }
            catch
            {
                return "Unexpected error occurred.";
            }
        }
    }
}