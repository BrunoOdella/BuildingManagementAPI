﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Out
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public LoginResponse(string token)
        {
            Token = token;
        }
    }
}
