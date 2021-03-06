﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebDtos
{
    public class LoginResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }

    public class LoginRequest
    {
        public string username;
        public string password;
        public string grant_type = "password";
    }
}
