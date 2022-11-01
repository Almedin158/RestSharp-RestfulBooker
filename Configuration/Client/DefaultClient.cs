using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Client
{
    public class DefaultClient : IClient
    {
        private RestClient _restClient;
        private readonly RestClientOptions _restClientOptions;

        public DefaultClient()
        {
            _restClientOptions = new RestClientOptions();
        }

        public void Dispose()
        {
            _restClient.Dispose();
        }

        public RestClient GetClient()
        {
            _restClientOptions.ThrowOnDeserializationError = true;
            _restClient = new RestClient(_restClientOptions);
            return _restClient;
        }
    }
}
