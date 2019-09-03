using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityWebApp.Models
{
    public class AuthToken
    {
        public string access_token { get; set; }
        public string username { get; set; }
    }
}