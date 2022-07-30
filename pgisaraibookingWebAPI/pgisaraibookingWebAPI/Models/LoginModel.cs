using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pgisaraibookingWebAPI.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }
    }
}