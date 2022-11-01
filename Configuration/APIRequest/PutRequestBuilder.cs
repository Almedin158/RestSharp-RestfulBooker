using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.APIRequest
{
    public class PutRequestBuilder : AbstractRequest
    {
        RestRequest _restRequest;

        public PutRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Put
            };
        }

        public override RestRequest Build()
        {
            return _restRequest;
        }
        public PutRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        public PutRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            WithHeaders(headers, _restRequest);
            return this;
        }

        public PutRequestBuilder WithObjectBody(object objectBody)
        {
            WithObjectBody(objectBody, _restRequest);
            return this;
        }

        public PutRequestBuilder WithJsonBody(string jsonBody)
        {
            WithJsonBody(jsonBody, _restRequest);
            return this;
        }

        public PutRequestBuilder WithParameters(Dictionary<string, string> wwwForm)
        {
            WithParameters(wwwForm, _restRequest);
            return this;
        }

        public PutRequestBuilder WithUrlSegments(Dictionary<string, string> urlSegments)
        {
            WithUrlSegments(urlSegments, _restRequest);
            return this;
        }
    }
}
