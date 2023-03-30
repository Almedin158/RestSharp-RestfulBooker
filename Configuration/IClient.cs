using RestSharp;

namespace Configuration
{
    public interface IClient:IDisposable
    {
        RestClient GetClient();
    }
}
