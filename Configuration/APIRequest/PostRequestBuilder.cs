using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.APIRequest
{
    public class PostRequestBuilder : AbstractRequest
    {
        RestRequest _restRequest;

        public PostRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Post
            };
        }

        public override RestRequest Build()
        {
            return _restRequest;
        }
        public PostRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        public PostRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            WithHeaders(headers, _restRequest);
            return this;
        }

        public PostRequestBuilder WithObjectBody(object objectBody)
        {
            WithObjectBody(objectBody, _restRequest);
            return this;
        }

        public PostRequestBuilder WithJsonBody(string jsonBody)
        {
            WithJsonBody(jsonBody,_restRequest);
            return this;
        }

        public PostRequestBuilder WithParameters(Dictionary<string, string> wwwForm)
        {
            WithParameters(wwwForm,_restRequest);
            return this;
        }

        public PostRequestBuilder WithUrlSegments(Dictionary<string, string> urlSegments)
        {
            WithUrlSegments(urlSegments, _restRequest);
            return this;
        }
    }
}
