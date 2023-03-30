using RestSharp;

namespace Configuration.APIRequest
{
    public class PatchRequestBuilder : AbstractRequest
    {
        RestRequest _restRequest;

        public PatchRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Patch
            };
        }

        public override RestRequest Build()
        {
            return _restRequest;
        }
        public PatchRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        public PatchRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            WithHeaders(headers, _restRequest);
            return this;
        }

        public PatchRequestBuilder WithObjectBody(object objectBody)
        {
            WithObjectBody(objectBody, _restRequest);
            return this;
        }

        public PatchRequestBuilder WithJsonBody(string jsonBody)
        {
            WithJsonBody(jsonBody, _restRequest);
            return this;
        }

        public PatchRequestBuilder WithParameters(Dictionary<string, string> wwwForm)
        {
            WithParameters(wwwForm, _restRequest);
            return this;
        }

        public PatchRequestBuilder WithUrlSegments(Dictionary<string, string> urlSegments)
        {
            WithUrlSegments(urlSegments, _restRequest);
            return this;
        }
    }

}
