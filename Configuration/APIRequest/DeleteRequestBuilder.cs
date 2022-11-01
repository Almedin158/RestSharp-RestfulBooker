using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.APIRequest
{
    public class DeleteRequestBuilder : AbstractRequest
    {
        private readonly RestRequest _restRequest;

        public DeleteRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Delete
            };
        }

        public override RestRequest Build()
        {
            return _restRequest;
        }

        public DeleteRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        public DeleteRequestBuilder WithEndpoint(string endpoint)
        {
            WithEndpoint(endpoint, _restRequest);
            return this;
        }

        public DeleteRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            WithHeaders(headers, _restRequest);
            return this;
        }

        public DeleteRequestBuilder WithQueryParameters(Dictionary<string, string> queryParameters)
        {
            WithQueryParameters(queryParameters, _restRequest);
            return this;
        }

        public DeleteRequestBuilder WithUrlSegments(Dictionary<string, string> urlSegments)
        {
            WithUrlSegments(urlSegments, _restRequest);
            return this;
        }
    }
}
