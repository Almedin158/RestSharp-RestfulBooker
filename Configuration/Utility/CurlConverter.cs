using RestSharp;

public static class CurlConverter
{
    /// <summary>
    /// Generates a CURL request based on a RestSharp request, for this to work, make sure the URL is added via the request resources instead of setting it via the client.
    /// Another issue, it only works with Json body, does not work with object body
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string ConvertToCurl(RestRequest request)
    {
        var client = new RestClient(request.Resource);
        var method = request.Method.ToString();
        var url = client.BuildUri(request).ToString();
        var curl = $"curl -X {method} \"{url}\"";

        foreach (var param in request.Parameters)
        {
            if (param.Type == ParameterType.RequestBody)
            {
                curl += $" -d '{param.Value}'";
            }
            else if (param.Type == ParameterType.HttpHeader)
            {
                curl += $" -H '{param.Name}: {param.Value}'";
            }
        }

        return curl;
    }
}