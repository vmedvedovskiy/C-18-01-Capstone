using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C_18_01_Capstone.Web.Infrastructure
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }
}