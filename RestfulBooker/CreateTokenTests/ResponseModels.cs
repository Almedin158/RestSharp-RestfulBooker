using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBooker.LoginTests
{
    public class AuthResponse
    {
        public string token { get; set; }
        public List<string> errors { get; set; }
        public string reason { get; set; }
    }
}
