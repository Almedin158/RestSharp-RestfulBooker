using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public abstract class AbstractRequest
    {
        public abstract RestRequest Build();
        protected virtual void WithUrl(string url, RestRequest restRequest)
        {
            restRequest.Resource = url;
        }

        protected virtual void WithEndpoint(string endpoint, RestRequest restRequest)
        {
            restRequest.Resource += endpoint;
        }

        protected virtual void WithHeaders(Dictionary<string, string> header, RestRequest restRequest)
        {
            foreach (var key in header.Keys)
            {
                restRequest.AddOrUpdateHeader(key, header[key]);
            }
        }

        protected virtual void WithObjectBody(object objectBody, RestRequest restRequest)
        {
            restRequest.AddBody(objectBody);
        }

        protected virtual void WithJsonBody(string jsonBody, RestRequest restRequest)
        {
            restRequest.AddJsonBody(jsonBody);
        }

        protected virtual void WithQueryParameters(Dictionary<string,string> queryParameters, RestRequest restRequest)
        {
            foreach (var key in queryParameters.Keys)
            {
                restRequest.AddQueryParameter(key, queryParameters[key]);
            }
        }
        protected virtual void WithUrlSegments(Dictionary<string, string> urlSegments, RestRequest restRequest)
        {
            foreach (var key in urlSegments.Keys)
            {
                restRequest.AddUrlSegment(key, urlSegments[key]);
            }
        }

        protected virtual void WithParameters(Dictionary<string, string> urlSegments, RestRequest restRequest)
        {
            foreach (var key in urlSegments.Keys)
            {
                restRequest.AddParameter(key, urlSegments[key]);
            }
        }
    }
}