using System;
using System.Collections.Generic;
using System.Text;

namespace Cyf.JWTAuthentication
{
    public class JWTOptions
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string SecurityKey { get; set; }

    }
}
