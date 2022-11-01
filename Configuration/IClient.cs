using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public interface IClient:IDisposable
    {
        RestClient GetClient();
    }
}
