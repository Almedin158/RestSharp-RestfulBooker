using RestSharp;

namespace Configuration.APIRequest
{
    public class GetRequestBuilder:AbstractRequest
    {
        private readonly RestRequest _restRequest;

        public GetRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Get
            };
        }

        public override RestRequest Build()
        {
            return _restRequest;
        }

        public GetRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        public GetRequestBuilder WithEndpoint(string endpoint)
        {
            WithEndpoint(endpoint, _restRequest);
            return this;
        }

        public GetRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            WithHeaders(headers, _restRequest);
            return this;
        }

        public GetRequestBuilder WithQueryParameters(Dictionary<string,string> queryParameters)
        {
            WithQueryParameters(queryParameters, _restRequest);
            return this;
        }

        public GetRequestBuilder WithUrlSegments(Dictionary<string,string> urlSegments)
        {
            WithUrlSegments(urlSegments, _restRequest);
            return this;
        }
    }
}
